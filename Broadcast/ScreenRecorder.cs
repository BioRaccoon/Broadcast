﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using SharpAvi;
using SharpAvi.Codecs;
using SharpAvi.Output;
using System.Windows.Forms;

namespace Broadcast
{
    public class ScreenRecorder
    {
        public ScreenRecorder(string filename, int FrameRate, FourCC Encoder, int Quality)
        {
            FileName = filename;
            FramesPerSecond = FrameRate;
            Codec = Encoder;
            this.Quality = Quality;

            var scope = new System.Management.ManagementScope();
            var query = new System.Management.ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");
            UInt32 maxHResolution = 0;
            UInt32 maxVResolution = 0;
            UInt32 maxHForMaxVResolution = 0;
            using (var searcher = new System.Management.ManagementObjectSearcher(scope, query))
            {
                var results = searcher.Get();
                foreach (var item in results)
                {
                    if ((UInt32)item["HorizontalResolution"] >= maxHResolution)
                    {
                        maxHResolution = (UInt32)item["HorizontalResolution"];
                        if ((UInt32)item["VerticalResolution"] > maxVResolution || maxHForMaxVResolution != maxHResolution)
                        {
                            maxVResolution = (UInt32)item["VerticalResolution"];
                            maxHForMaxVResolution = (UInt32)item["HorizontalResolution"];
                        }
                    }
                }
            }
            //MessageBox.Show(maxHResolution + " x " + maxVResolution);

            Height = checked((int)maxVResolution);
            Width = checked((int)maxHResolution);
        }

        string FileName;
        public int FramesPerSecond, Quality;
        FourCC Codec;

        public int Height { get; private set; }
        public int Width { get; private set; }

        public AviWriter CreateAviWriter()
        {
            return new AviWriter(FileName)
            {
                FramesPerSecond = FramesPerSecond,
                EmitIndex1 = true,
            };
        }

        public IAviVideoStream CreateVideoStream(AviWriter writer)
        {
            // Select encoder type based on FOURCC of codec
            if (Codec == KnownFourCCs.Codecs.Uncompressed)
                return writer.AddUncompressedVideoStream(Width, Height);
            else if (Codec == KnownFourCCs.Codecs.MotionJpeg)
                return writer.AddMotionJpegVideoStream(Width, Height, Quality);
            else
            {
                return writer.AddMpeg4VideoStream(Width, Height, (double)writer.FramesPerSecond,
                    // It seems that all tested MPEG-4 VfW codecs ignore the quality affecting parameters passed through VfW API
                    // They only respect the settings from their own configuration dialogs, and Mpeg4VideoEncoder currently has no support for this
                    quality: Quality,
                    codec: Codec,
                    // Most of VfW codecs expect single-threaded use, so we wrap this encoder to special wrapper
                    // Thus all calls to the encoder (including its instantiation) will be invoked on a single thread although encoding (and writing) is performed asynchronously
                    forceSingleThreadedAccess: true);
            }
        }
    }
    public class Recorder : IDisposable
    {
        #region Fields
        AviWriter writer;
        ScreenRecorder Params;
        IAviVideoStream videoStream;
        Thread screenThread;
        ManualResetEvent stopThread = new ManualResetEvent(false);
        #endregion

        public Recorder(ScreenRecorder Params)
        {
            this.Params = Params;

            // Create AVI writer and specify FPS
            writer = Params.CreateAviWriter();

            // Create video stream
            videoStream = Params.CreateVideoStream(writer);
            // Set only name. Other properties were when creating stream, 
            // either explicitly by arguments or implicitly by the encoder used
            videoStream.Name = "Captura";

            screenThread = new Thread(RecordScreen)
            {
                Name = typeof(Recorder).Name + ".RecordScreen",
                IsBackground = true
            };

            screenThread.Start();
        }

        public void Dispose()
        {
            stopThread.Set();
            screenThread.Join();

            // Close writer: the remaining data is written to a file and file is closed
            writer.Close();

            stopThread.Dispose();
        }

        void RecordScreen()
        {
            var frameInterval = TimeSpan.FromSeconds(1 / (double)writer.FramesPerSecond);
            var buffer = new byte[Params.Width * Params.Height * 4];
            Task videoWriteTask = null;
            var timeTillNextFrame = TimeSpan.Zero;

            while (!stopThread.WaitOne(timeTillNextFrame))
            {
                var timestamp = DateTime.Now;

                Screenshot(buffer);

                // Wait for the previous frame is written
                videoWriteTask?.Wait();

                // Start asynchronous (encoding and) writing of the new frame
                videoWriteTask = videoStream.WriteFrameAsync(true, buffer, 0, buffer.Length);

                timeTillNextFrame = timestamp + frameInterval - DateTime.Now;
                if (timeTillNextFrame < TimeSpan.Zero)
                    timeTillNextFrame = TimeSpan.Zero;
            }

            // Wait for the last frame is written
            videoWriteTask?.Wait();
        }

        public void Screenshot(byte[] Buffer)
        {
            using (var BMP = new Bitmap(Params.Width, Params.Height))
            {
                using (var g = Graphics.FromImage(BMP))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, new Size(Params.Width, Params.Height), CopyPixelOperation.SourceCopy);

                    g.Flush();

                    var bits = BMP.LockBits(new Rectangle(0, 0, Params.Width, Params.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                    Marshal.Copy(bits.Scan0, Buffer, 0, Buffer.Length);
                    BMP.UnlockBits(bits);
                }
            }
        }
    }
}