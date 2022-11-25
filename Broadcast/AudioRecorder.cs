using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast
{
    public class AudioRecorder
    {
        // Create class-level accessible variables to store the audio screenRecorder and capturer instance
        private WaveFileWriter RecordedAudioWriter = null;
        private WasapiLoopbackCapture CaptureInstance = null;

        public AudioRecorder(string outputFolder)
        {
            // Define the output wav file of the recorded audio
            string outputFilePath = outputFolder;

            // Redefine the capturer instance with a new instance of the LoopbackCapture class
            this.CaptureInstance = new WasapiLoopbackCapture();

            // Redefine the audio writer instance with the given configuration
            this.RecordedAudioWriter = new WaveFileWriter(outputFilePath, CaptureInstance.WaveFormat);

            // When the capturer receives audio, start writing the buffer into the mentioned file
            this.CaptureInstance.DataAvailable += (s, a) =>
            {
                this.RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            // When the Capturer Stops
            this.CaptureInstance.RecordingStopped += (s, a) =>
            {
                this.RecordedAudioWriter.Dispose();
                this.RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };


            // Start recording !
            this.CaptureInstance.StartRecording();
        }

        public void Dispose()
        {
            // Stop recording !
            this.CaptureInstance.StopRecording();
        }
    }
}
