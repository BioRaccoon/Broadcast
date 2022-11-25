using AForge.Video;
using AForge.Video.DirectShow;
using Broadcast.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace Broadcast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice localVideoCaptureDevice1;
        VideoCaptureDevice localVideoCaptureDevice2;
        VideoCaptureDevice liveVideoCaptureDevice;
        MJPEGStream localStream1;
        MJPEGStream localStream2;
        MJPEGStream liveStream;
        Recorder screenRecorder;
        //AudioRecorder audioRecorder;

        String broadcastFolder = "";
        String playlistFolder = "";
        String resourcesFolder = "";
        String recordsFolder = "";
        String dateNow = "";
        String url1 = "";
        String url2 = "";

        private void pictureBox1_NewFrame(object sender, NewFrameEventArgs eventargs)
        {
            pictureBox1.Image = (Bitmap)eventargs.Frame.Clone();
        }

        private void pictureBox2_NewFrame(object sender, NewFrameEventArgs eventargs)
        {
            pictureBox2.Image = (Bitmap)eventargs.Frame.Clone();
        }

        private void LiveAreaCamera_NewFrame(object sender, NewFrameEventArgs eventargs)
        {
            liveAreaVideo.Image = (Bitmap)eventargs.Frame.Clone();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            defaultControlVideo();
            setFoldersNames();
            setFormControlsProperties();
            fillListBoxes();
        }

        private void fillListBoxes()
        {
            CamerasBox.Items.Clear();
            videoFiles.Items.Clear();
            broadcastPlaylist.Items.Clear();
            recordsList.Items.Clear();
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                CamerasBox.Items.Add(filterInfo.Name);

            }
            string[] resourcesFiles = Directory.GetFiles(resourcesFolder);
            foreach (string file in resourcesFiles)
            {
                videoFiles.Items.Add(Path.GetFileName(file));
            }
            string[] playlistFiles = Directory.GetFiles(playlistFolder);
            foreach (string file in playlistFiles)
            {
                broadcastPlaylist.Items.Add(Path.GetFileName(file));
            }
            string[] recordsFiles = Directory.GetFiles(recordsFolder);
            foreach (string file in recordsFiles)
            {
                recordsList.Items.Add(Path.GetFileName(file));
            }
        }

        private void setFormControlsProperties()
        {
            pictureBox1.AllowDrop = true;
            pictureBox2.AllowDrop = true;
            transpCtrl1.AllowDrop = true;
            transpCtrl2.AllowDrop = true;
            transpCtrl3.AllowDrop = true;
            transpCtrl4.AllowDrop = true;
            transpCtrl5.AllowDrop = true;
            transpCtrl6.AllowDrop = true;
            transpCtrl7.AllowDrop = true;
            transpCtrl8.AllowDrop = true;
            transpCtrl9.AllowDrop = true;
            transpCtrl10.AllowDrop = true;
            transpCtrlLive.SendToBack();
            axWindowsMediaPlayerLive.SendToBack();
            liveWebBrowser.DocumentText = string.Format("", "");
            axWindowsMediaPlayerLive.uiMode = "none";
            axWindowsMediaPlayerLive.settings.mute = true;
            axWindowsMediaPlayerLive.stretchToFit = true;
            axWindowsMediaPlayerLive.settings.autoStart = true;
            axWindowsMediaPlayerLive.settings.setMode("loop", true);
        }

        private void setFoldersNames()
        {
            string broadcastDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var charsToRemove = new string[] { "\\bin", "\\Release", "\\Debug" };
            foreach (var c in charsToRemove)
            {
                broadcastDirectory = broadcastDirectory.Replace(c, string.Empty);
            }

            broadcastFolder = broadcastDirectory;
            resourcesFolder = broadcastDirectory + "Resources\\VIDEOSINDES1\\";
            playlistFolder = broadcastDirectory + "Resources\\Playlist\\";
            recordsFolder = broadcastDirectory + "Resources\\Records\\";
        }

        private void defaultControlVideo()
        {
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.mute = true;
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);

            axWindowsMediaPlayer2.uiMode = "none";
            axWindowsMediaPlayer2.settings.mute = true;
            axWindowsMediaPlayer2.stretchToFit = true;
            axWindowsMediaPlayer2.settings.autoStart = true;
            axWindowsMediaPlayer2.settings.setMode("loop", true);

            axWindowsMediaPlayer3.uiMode = "none";
            axWindowsMediaPlayer3.settings.mute = true;
            axWindowsMediaPlayer3.stretchToFit = true;
            axWindowsMediaPlayer3.settings.autoStart = true;
            axWindowsMediaPlayer3.settings.setMode("loop", true);

            axWindowsMediaPlayer4.uiMode = "none";
            axWindowsMediaPlayer4.settings.mute = true;
            axWindowsMediaPlayer4.stretchToFit = true;
            axWindowsMediaPlayer4.settings.autoStart = true;
            axWindowsMediaPlayer4.settings.setMode("loop", true);

            axWindowsMediaPlayer5.uiMode = "none";
            axWindowsMediaPlayer5.settings.mute = true;
            axWindowsMediaPlayer5.stretchToFit = true;
            axWindowsMediaPlayer5.settings.autoStart = true;
            axWindowsMediaPlayer5.settings.setMode("loop", true);

            axWindowsMediaPlayer6.uiMode = "none";
            axWindowsMediaPlayer6.settings.mute = true;
            axWindowsMediaPlayer6.stretchToFit = true;
            axWindowsMediaPlayer6.settings.autoStart = true;
            axWindowsMediaPlayer6.settings.setMode("loop", true);

            axWindowsMediaPlayer7.uiMode = "none";
            axWindowsMediaPlayer7.settings.mute = true;
            axWindowsMediaPlayer7.stretchToFit = true;
            axWindowsMediaPlayer7.settings.autoStart = true;
            axWindowsMediaPlayer7.settings.setMode("loop", true);

            axWindowsMediaPlayer8.uiMode = "none";
            axWindowsMediaPlayer8.settings.mute = true;
            axWindowsMediaPlayer8.stretchToFit = true;
            axWindowsMediaPlayer8.settings.autoStart = true;
            axWindowsMediaPlayer8.settings.setMode("loop", true);
        }

        private void liveButton66_Click(object sender, EventArgs e)
        {
            if (localStream1 != null && localStream1.IsRunning)
            {
                localStream1.Stop();
                pictureBox1.Image = Resources.no_signal;
                liveButton4.BackColor = Color.Red;
            }
            if (liveButton2.BackColor == Color.Red)
            {
                if (filterInfoCollection.Count > 0)
                {
                    localVideoCaptureDevice1 = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
                    localVideoCaptureDevice1.NewFrame += pictureBox1_NewFrame;
                    localVideoCaptureDevice1.NewFrame += new NewFrameEventHandler(pictureBox1_NewFrame);
                    localVideoCaptureDevice1.Start();
                    liveButton2.BackColor = Color.Lime;
                }
            }
            else
            {
                localVideoCaptureDevice1.Stop();
                pictureBox1.Image = Resources.no_signal;
                liveButton2.BackColor = Color.Red;
            }
        }

        private void startLocalVideoCapturingDevice1(string source)
        {
            if (source != null)
            {
                localVideoCaptureDevice1 = new VideoCaptureDevice(source);
                localVideoCaptureDevice1.NewFrame += pictureBox1_NewFrame;
                localVideoCaptureDevice1.NewFrame += new NewFrameEventHandler(pictureBox1_NewFrame);
                localVideoCaptureDevice1.Start();
            }
        }

        private void startLocalStream1(string source)
        {
            if (source != null)
            {
                localStream1 = new MJPEGStream(source);
                localStream1.NewFrame += new NewFrameEventHandler(pictureBox1_NewFrame);
                localStream1.Start();
            }
        }

        private void startLocalVideoCapturingDevice2(string source)
        {
            if (source != null)
            {
                localVideoCaptureDevice2 = new VideoCaptureDevice(source);
                localVideoCaptureDevice2.NewFrame += pictureBox2_NewFrame;
                localVideoCaptureDevice2.NewFrame += new NewFrameEventHandler(pictureBox2_NewFrame);
                localVideoCaptureDevice2.Start();
            }
        }

        private void startLocalStream2(string source)
        {
            if (source != null)
            {
                localStream2 = new MJPEGStream(source);
                localStream2.NewFrame += new NewFrameEventHandler(pictureBox2_NewFrame);
                localStream2.Start();
            }
        }

        private void disableAllButtons()
        {
            liveButton1.BackColor = Color.Red;
            liveButton2.BackColor = Color.Red;
            liveButton3.BackColor = Color.Red;
            liveButton4.BackColor = Color.Red;
            liveButton5.BackColor = Color.Red;
            liveButton6.BackColor = Color.Red;
            liveButton7.BackColor = Color.Red;
            liveButton8.BackColor = Color.Red;
            liveButton9.BackColor = Color.Red;
            liveButton10.BackColor = Color.Red;
            liveButton11.BackColor = Color.Red;
            liveButton12.BackColor = Color.Red;
            playlistModeButton.BackColor = Color.Red;
        }

        private void liveButton1_Click(object sender, EventArgs e)
        {
            if (liveButton1.BackColor == Color.Red)
            {
                if (localVideoCaptureDevice1 != null && localVideoCaptureDevice1.IsRunning)
                {
                    if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
                    {
                        liveVideoCaptureDevice.Stop();
                        startLocalVideoCapturingDevices(liveVideoCaptureDevice.Source);
                    }
                    else if (liveStream != null && liveStream.IsRunning)
                    {
                        liveStream.Stop();
                        startLocalVideoCapturingDevices(liveStream.Source);
                    }
                    disableAllButtons();
                    axWindowsMediaPlayerLive.URL = "";
                    transpCtrlLive.SendToBack();
                    axWindowsMediaPlayerLive.SendToBack();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    liveVideoCaptureDevice = localVideoCaptureDevice1;
                    liveVideoCaptureDevice.NewFrame += LiveAreaCamera_NewFrame;
                    liveVideoCaptureDevice.NewFrame += new NewFrameEventHandler(LiveAreaCamera_NewFrame);
                    liveVideoCaptureDevice.Start();
                    liveButton1.BackColor = Color.Lime;
                }
                else if (localStream1 != null && localStream1.IsRunning)
                {
                    if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
                    {
                        liveVideoCaptureDevice.Stop();
                        startLocalVideoCapturingDevices(liveVideoCaptureDevice.Source);
                    }
                    else if (liveStream != null && liveStream.IsRunning)
                    {
                        liveStream.Stop();
                        startLocalVideoCapturingDevices(liveStream.Source);
                    }
                    disableAllButtons();
                    axWindowsMediaPlayerLive.URL = "";
                    transpCtrlLive.SendToBack();
                    axWindowsMediaPlayerLive.SendToBack();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    liveStream = localStream1;
                    liveStream.NewFrame += new NewFrameEventHandler(LiveAreaCamera_NewFrame);
                    liveStream.Start();
                    liveButton1.BackColor = Color.Lime;
                }
            }
            else
            {
                if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
                {
                    liveVideoCaptureDevice.Stop();
                    startLocalVideoCapturingDevices(liveVideoCaptureDevice.Source);

                }
                else if (liveStream != null && liveStream.IsRunning)
                {
                    liveStream.Stop();
                    startLocalVideoCapturingDevices(liveStream.Source);
                }
                liveAreaVideo.Image = Resources.no_signal;
                liveButton1.BackColor = Color.Red;
            }
        }

        private void startLocalVideoCapturingDevices(string liveAreaSource)
        {
            if (localVideoCaptureDevice1 != null && localVideoCaptureDevice1.Source == liveAreaSource)
            {
                startLocalVideoCapturingDevice1(localVideoCaptureDevice1.Source);
            }
            if (localVideoCaptureDevice2 != null && localVideoCaptureDevice2.Source == liveAreaSource)
            {
                startLocalVideoCapturingDevice2(localVideoCaptureDevice2.Source);
            }
            if (localStream1 != null && localStream1.Source == liveAreaSource)
            {
                startLocalStream1(localStream1.Source);
            }
            if (localStream2 != null && localStream2.Source == liveAreaSource)
            {
                startLocalStream2(localStream2.Source);
            }
        }

        private void liveButton2_Click(object sender, EventArgs e)
        {
            if (liveButton2.BackColor == Color.Red)
            {
                if (localVideoCaptureDevice2 != null && localVideoCaptureDevice2.IsRunning)
                {
                    if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
                    {
                        liveVideoCaptureDevice.Stop();
                        startLocalVideoCapturingDevices(liveVideoCaptureDevice.Source);
                    }
                    else if (liveStream != null && liveStream.IsRunning)
                    {
                        liveStream.Stop();
                        startLocalVideoCapturingDevices(liveStream.Source);
                    }
                    disableAllButtons();
                    axWindowsMediaPlayerLive.URL = "";
                    transpCtrlLive.SendToBack();
                    axWindowsMediaPlayerLive.SendToBack();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    liveVideoCaptureDevice = localVideoCaptureDevice2;
                    liveVideoCaptureDevice.NewFrame += LiveAreaCamera_NewFrame;
                    liveVideoCaptureDevice.NewFrame += new NewFrameEventHandler(LiveAreaCamera_NewFrame);
                    liveVideoCaptureDevice.Start();
                    liveButton2.BackColor = Color.Lime;
                }
                else if (localStream2 != null && localStream2.IsRunning)
                {
                    if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
                    {
                        liveVideoCaptureDevice.Stop();
                        startLocalVideoCapturingDevices(liveVideoCaptureDevice.Source);
                    }
                    else if (liveStream != null && liveStream.IsRunning)
                    {
                        liveStream.Stop();
                        startLocalVideoCapturingDevices(liveStream.Source);
                    }
                    disableAllButtons();
                    axWindowsMediaPlayerLive.URL = "";
                    transpCtrlLive.SendToBack();
                    axWindowsMediaPlayerLive.SendToBack();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    liveStream = localStream2;
                    liveStream.NewFrame += new NewFrameEventHandler(LiveAreaCamera_NewFrame);
                    liveStream.Start();
                    liveButton2.BackColor = Color.Lime;
                }
            }
            else
            {
                if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
                {
                    liveVideoCaptureDevice.Stop();
                    startLocalVideoCapturingDevices(liveVideoCaptureDevice.Source);

                }
                else if (liveStream != null && liveStream.IsRunning)
                {
                    liveStream.Stop();
                    startLocalVideoCapturingDevices(liveStream.Source);

                }
                liveAreaVideo.Image = Resources.no_signal;
                liveButton2.BackColor = Color.Red;
            }
        }

        private void liveButton3_Click(object sender, EventArgs e)
        {
            if (liveButton3.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer1.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer1.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                    liveButton3.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton3.BackColor = Color.Red;
            }
        }

        private void liveButton4_Click(object sender, EventArgs e)
        {
            if (liveButton4.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer2.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer2.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer2.Ctlcontrols.currentPosition;
                    liveButton4.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton4.BackColor = Color.Red;
            }
        }

        private void liveButton5_Click(object sender, EventArgs e)
        {
            if (liveButton5.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer3.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer3.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer3.Ctlcontrols.currentPosition;
                    liveButton5.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton5.BackColor = Color.Red;
            }
        }

        private void liveButton6_Click(object sender, EventArgs e)
        {
            if (liveButton6.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer4.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer4.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer4.Ctlcontrols.currentPosition;
                    liveButton6.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton6.BackColor = Color.Red;
            }
        }

        private void liveButton7_Click(object sender, EventArgs e)
        {
            if (liveButton7.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer5.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer5.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer5.Ctlcontrols.currentPosition;
                    liveButton7.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton7.BackColor = Color.Red;
            }
        }

        private void liveButton8_Click(object sender, EventArgs e)
        {
            if (liveButton8.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer6.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer6.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer6.Ctlcontrols.currentPosition;
                    liveButton8.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton8.BackColor = Color.Red;
            }
        }

        private void liveButton9_Click(object sender, EventArgs e)
        {
            if (liveButton9.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer7.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer7.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer7.Ctlcontrols.currentPosition;
                    liveButton9.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton9.BackColor = Color.Red;
            }
        }

        private void liveButton10_Click(object sender, EventArgs e)
        {
            if (liveButton10.BackColor == Color.Red)
            {
                if (axWindowsMediaPlayer8.URL != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                    axWindowsMediaPlayerLive.URL = axWindowsMediaPlayer8.URL;
                    axWindowsMediaPlayerLive.Ctlcontrols.currentPosition = axWindowsMediaPlayer8.Ctlcontrols.currentPosition;
                    liveButton10.BackColor = Color.Lime;
                }
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                liveButton10.BackColor = Color.Red;
            }
        }

        private void liveButton11_Click(object sender, EventArgs e)
        {
            if (liveButton11.BackColor == Color.Red)
            {
                if (url1 != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    axWindowsMediaPlayerLive.URL = "";
                    transpCtrlLive.SendToBack();
                    axWindowsMediaPlayerLive.SendToBack();
                    liveWebBrowser.BringToFront();

                    var embed = "<html><head>" +
                 "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/>" +
                 "</head><body>" +
                 "<iframe width=\"495\" height=\"280\" src=\"{0}\"" +
                 "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
                 "</body></html>";
                    string liveURL1 = url1.Replace("&mute=1&controls=0", "");
                    liveWebBrowser.DocumentText = string.Format(embed, liveURL1);
                    liveButton11.BackColor = Color.Lime;
                }
            }
            else
            {
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.DocumentText = string.Format("", "");
                liveButton11.BackColor = Color.Red;
            }
        }

        private void liveButton12_Click(object sender, EventArgs e)
        {
            if (liveButton12.BackColor == Color.Red)
            {
                if (url2 != "")
                {
                    disableAllButtons();
                    disableLivePicture();
                    axWindowsMediaPlayerLive.URL = "";
                    transpCtrlLive.SendToBack();
                    axWindowsMediaPlayerLive.SendToBack();
                    liveWebBrowser.BringToFront();

                    var embed = "<html><head>" +
                  "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/>" +
                  "</head><body>" +
                  "<iframe width=\"495\" height=\"280\" src=\"{0}\"" +
                  "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
                  "</body></html>";
                    string liveURL2 = url2.Replace("&mute=1&controls=0", "");
                    liveWebBrowser.DocumentText = string.Format(embed, liveURL2);
                    liveButton12.BackColor = Color.Lime;
                }
            }
            else
            {
                liveWebBrowser.SendToBack(); liveWebBrowser.DocumentText = string.Format("", "");
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                liveWebBrowser.DocumentText = string.Format("", "");
                liveButton12.BackColor = Color.Red;
            }
        }

        private void disableLivePicture()
        {
            if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
            {
                liveVideoCaptureDevice.Stop();
                startLocalVideoCapturingDevices(liveVideoCaptureDevice.Source);

            }
            else if (liveStream != null && liveStream.IsRunning)
            {
                liveStream.Stop();
                startLocalVideoCapturingDevices(liveStream.Source);

            }
            liveAreaVideo.SendToBack();
            liveAreaVideo.Image = Resources.no_signal;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (localVideoCaptureDevice1 != null && localVideoCaptureDevice1.IsRunning)
            {
                localVideoCaptureDevice1.Stop();
            }
            if (localVideoCaptureDevice2 != null && localVideoCaptureDevice2.IsRunning)
            {
                localVideoCaptureDevice2.Stop();
            }
            if (liveVideoCaptureDevice != null && liveVideoCaptureDevice.IsRunning)
            {
                liveVideoCaptureDevice.Stop();
            }
            if (localStream1 != null && localStream1.IsRunning)
            {
                localStream1.Stop();
            }
            if (localStream2 != null && localStream2.IsRunning)
            {
                localStream2.Stop();
            }
            if (liveStream != null && liveStream.IsRunning)
            {
                liveStream.Stop();
            }
        }

        /*private void axWindowsMediaPlayer10_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if(axWindowsMediaPlayer10.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer10.Ctlcontrols.currentPosition=0;
            }
        }*/

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (videoFiles.SelectedItem != null)
            {
                videoFiles.DoDragDrop(resourcesFolder + videoFiles.SelectedItem, DragDropEffects.Copy);
            }
        }

        private void pictureBox2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pictureBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (localVideoCaptureDevice2 != null && localVideoCaptureDevice2.IsRunning)
            {
                localVideoCaptureDevice2.Stop();
            }
            else if (localStream2 != null && localStream2.IsRunning)
            {
                localStream2.Stop();
            }

            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as String[];
                if (fileNames.Length > 0)
                {
                    pictureBox2.Image = Image.FromFile(fileNames[0]);
                    return;
                }
            }
            data = e.Data.GetData(DataFormats.Bitmap);
            if (data != null)
            {
                if (localVideoCaptureDevice1 != null && localVideoCaptureDevice1.IsRunning)
                {
                    localVideoCaptureDevice2 = localVideoCaptureDevice1;
                    localVideoCaptureDevice2.NewFrame += pictureBox2_NewFrame;
                    localVideoCaptureDevice2.NewFrame += new NewFrameEventHandler(pictureBox2_NewFrame);
                    localVideoCaptureDevice2.Start();
                    return;
                }
                else if (localStream1 != null && localStream1.IsRunning)
                {
                    localStream2 = localStream1;
                    localStream2.NewFrame += new NewFrameEventHandler(pictureBox2_NewFrame);
                    localStream2.Start();
                    return;
                }
                else
                {
                    pictureBox2.Image = (Image)data;
                    return;
                }
            }
            data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                IPAddress ip;
                IPAddress.TryParse(data + "", out ip);
                if (ip != null)
                {
                    localStream2 = new MJPEGStream("http://" + data + ":8080/videofeed");
                    localStream2.NewFrame += new NewFrameEventHandler(pictureBox2_NewFrame);
                    localStream2.Start();
                }
                else
                {
                    foreach (FilterInfo item in filterInfoCollection)
                    {
                        if (item.Name == data.ToString())
                        {
                            localVideoCaptureDevice2 = new VideoCaptureDevice(item.MonikerString);
                            localVideoCaptureDevice2.NewFrame += pictureBox2_NewFrame;
                            localVideoCaptureDevice2.NewFrame += new NewFrameEventHandler(pictureBox2_NewFrame);
                            localVideoCaptureDevice2.Start();
                            return;
                        }
                    }
                }
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {

            if (localVideoCaptureDevice1 != null && localVideoCaptureDevice1.IsRunning)
            {
                localVideoCaptureDevice1.Stop();
            }
            else if (localStream1 != null && localStream1.IsRunning)
            {
                localStream1.Stop();
            }

            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as String[];
                if (fileNames.Length > 0)
                {
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
                    return;
                }
            }
            data = e.Data.GetData(DataFormats.Bitmap);
            if (data != null)
            {
                if (localVideoCaptureDevice2 != null && localVideoCaptureDevice2.IsRunning)
                {
                    localVideoCaptureDevice1 = localVideoCaptureDevice2;
                    localVideoCaptureDevice1.NewFrame += pictureBox1_NewFrame;
                    localVideoCaptureDevice1.NewFrame += new NewFrameEventHandler(pictureBox1_NewFrame);
                    localVideoCaptureDevice1.Start();
                    return;
                }
                else if (localStream2 != null && localStream2.IsRunning)
                {
                    localStream1 = localStream2;
                    localStream1.NewFrame += new NewFrameEventHandler(pictureBox1_NewFrame);
                    localStream1.Start();
                    return;
                }
                else
                {
                    pictureBox1.Image = (Image)data;
                    return;
                }
            }
            data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                IPAddress ip;
                IPAddress.TryParse(data + "", out ip);
                if (ip != null)
                {
                    localStream1 = new MJPEGStream("http://" + data + ":8080/videofeed");
                    localStream1.NewFrame += new NewFrameEventHandler(pictureBox1_NewFrame);
                    localStream1.Start();
                }
                else
                {
                    foreach (FilterInfo item in filterInfoCollection)
                    {
                        if (item.Name == data.ToString())
                        {
                            localVideoCaptureDevice1 = new VideoCaptureDevice(item.MonikerString);
                            localVideoCaptureDevice1.NewFrame += pictureBox1_NewFrame;
                            localVideoCaptureDevice1.NewFrame += new NewFrameEventHandler(pictureBox1_NewFrame);
                            localVideoCaptureDevice1.Start();
                            return;
                        }
                    }
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //  webBrowser1.DocumentText = "";
        }

        private void transpCtrl1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer1.URL = data + "";
                axWindowsMediaPlayer1.settings.mute = true;

            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer1.URL = fileNames[0];
                        axWindowsMediaPlayer1.settings.mute = true;
                    }
                }
            }
        }

        /*string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        foreach(string file in fileList)
        {
        MessageBox.Show(file);
        }*/

        private void transpCtrl2_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer2.URL = data + "";
                axWindowsMediaPlayer2.settings.mute = true;
            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer2.URL = fileNames[0];
                        axWindowsMediaPlayer2.settings.mute = true;
                    }
                }
            }
        }

        private void transpCtrl3_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer3.URL = data + "";
                axWindowsMediaPlayer3.settings.mute = true;
            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer3.URL = fileNames[0];
                        axWindowsMediaPlayer3.settings.mute = true;
                    }
                }
            }
        }

        private void transpCtrl4_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer4.URL = data + "";
                axWindowsMediaPlayer4.settings.mute = true;
            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer4.URL = fileNames[0];
                        axWindowsMediaPlayer4.settings.mute = true;
                    }
                }
            }
        }

        private void transpCtrl5_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer5.URL = data + "";
                axWindowsMediaPlayer5.settings.mute = true;
            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer5.URL = fileNames[0];
                        axWindowsMediaPlayer5.settings.mute = true;
                    }
                }
            }
        }

        private void transpCtrl6_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer6.URL = data + "";
                axWindowsMediaPlayer6.settings.mute = true;
            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer6.URL = fileNames[0];
                        axWindowsMediaPlayer6.settings.mute = true;
                    }
                }
            }
        }

        private void transpCtrl7_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer7.URL = data + "";
                axWindowsMediaPlayer7.settings.mute = true;
            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer7.URL = fileNames[0];
                        axWindowsMediaPlayer7.settings.mute = true;
                    }
                }
            }
        }

        private void transpCtrl8_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                axWindowsMediaPlayer8.URL = data + "";
                axWindowsMediaPlayer8.settings.mute = true;
            }
            else
            {
                data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    var fileNames = data as String[];
                    if (fileNames.Length > 0)
                    {
                        axWindowsMediaPlayer8.URL = fileNames[0];
                        axWindowsMediaPlayer8.settings.mute = true;
                    }
                }
            }
        }

        private void transpCtrl9_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                var embed = "<html><head>" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
                "</head><body>" +
                "<iframe width=\"120\" height=\"120\" src=\"{0}\"" +
                "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
                "</body></html>";
                var videoId = getVideoID(data + "");
                url1 = "https://www.youtube.com/embed/" + videoId + "?autoplay=1&loop=1&mute=1&controls=0";
                webBrowser1.DocumentText = string.Format(embed, url1);
                webBrowser1.BringToFront();
            }
        }

        private void transpCtrl10_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                var embed = "<html><head>" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
                "</head><body>" +
                "<iframe width=\"120\" height=\"120\" src=\"{0}\"" +
                "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
                "</body></html>";
                var videoId = getVideoID(data + "");
                url2 = "https://www.youtube.com/embed/" + videoId + "?autoplay=1&loop=1&mute=1&controls=0";
                webBrowser2.DocumentText = string.Format(embed, url2);
                webBrowser2.BringToFront();
            }
        }

        public string getVideoID(string url)
        {
            var uri = new Uri(url);

            // you can check host here => uri.Host <= "www.youtube.com"

            var query = HttpUtility.ParseQueryString(uri.Query);

            var videoId = string.Empty;

            if (query.AllKeys.Contains("v"))
            {
                videoId = query["v"];
            }
            else
            {
                videoId = uri.Segments.Last();
            }
            return videoId;
        }

        private void transpCtrl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl4_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl5_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl6_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl7_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl8_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl9_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void transpCtrl10_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void removeVideoFileButton_Click(object sender, EventArgs e)
        {
            videoFiles.Items.Remove(videoFiles.SelectedItem);
        }

        private void removeURLVideoButton_Click(object sender, EventArgs e)
        {
            urlVideos.Items.Remove(urlVideos.SelectedItem);
        }

        private void removeCameraButton_Click(object sender, EventArgs e)
        {
            CamerasBox.Items.Remove(CamerasBox.SelectedItem);
        }

        private void removeRecordFileButton_Click(object sender, EventArgs e)
        {
            recordsList.Items.Remove(recordsList.SelectedItem);
        }

        private void removePlaylistFileButton_Click(object sender, EventArgs e)
        {
            broadcastPlaylist.Items.Remove(broadcastPlaylist.SelectedItem);
        }

        private void addCameraButton_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            buttonOk.SetBounds(228, 160, 160, 60);
            buttonCancel.SetBounds(400, 160, 160, 60);
            label.AutoSize = true;
            form.Text = "IP Address";
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                CamerasBox.Items.Add(textBox.Text);
            }
;
        }

        private void CamerasBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (CamerasBox.SelectedItem != null)
            {
                CamerasBox.DoDragDrop(CamerasBox.SelectedItem, DragDropEffects.Copy);
            }
        }

        private void playlistModeButton_Click(object sender, EventArgs e)
        {
            if (playlistModeButton.BackColor == Color.Red)
            {
                disableAllButtons();
                disableLivePicture();
                var pl = axWindowsMediaPlayerLive.playlistCollection.newPlaylist("MyPlaylist");
                axWindowsMediaPlayerLive.URL = "";
                string[] allfiles = Directory.GetFiles(playlistFolder);
                foreach (string file in allfiles)
                {
                    pl.appendItem(axWindowsMediaPlayerLive.newMedia(file));
                }
                axWindowsMediaPlayerLive.currentPlaylist = pl;
                axWindowsMediaPlayerLive.Ctlcontrols.play();
                playlistModeButton.BackColor = Color.Lime;
            }
            else
            {
                axWindowsMediaPlayerLive.URL = "";
                transpCtrlLive.SendToBack();
                axWindowsMediaPlayerLive.SendToBack();
                playlistModeButton.BackColor = Color.Red;
            }
        }

        private void addURLVideoButton_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            buttonOk.SetBounds(228, 160, 160, 60);
            buttonCancel.SetBounds(400, 160, 160, 60);
            label.AutoSize = true;
            form.Text = "URL Video";
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                urlVideos.Items.Add(textBox.Text);
            }
        }

        private void addVideoFileButton_Click(object sender, EventArgs e)
        {
            string filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Video files (*.mp4)|*.mp4|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                try
                {
                    File.Copy(filePath, resourcesFolder + Path.GetFileName(filePath), true);
                }
                catch { }
                videoFiles.Items.Add(Path.GetFileName(filePath));
            }
        }

        private void addRecordFileButton_Click(object sender, EventArgs e)
        {
            string filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Video files (*.mp4)|*.mp4|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                try
                {
                    File.Copy(filePath, recordsFolder + Path.GetFileName(filePath), true);
                }
                catch { }
                recordsList.Items.Add(Path.GetFileName(filePath));
            }
        }

        private void addPlaylistFileButton_Click(object sender, EventArgs e)
        {
            string filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Video files (*.mp4)|*.mp4|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                try
                {
                    File.Copy(filePath, playlistFolder + Path.GetFileName(filePath), true);
                }
                catch { }
                broadcastPlaylist.Items.Add(Path.GetFileName(filePath));
            }
        }

        private void recordScreenButton_Click(object sender, EventArgs e)
        {
            if (recordScreenButton.BackColor == Color.Red)
            {
                dateNow = DateTime.Now.ToString();
                var charsToRemove = new string[] { "/", ":" };
                foreach (var c in charsToRemove)
                {
                    dateNow = dateNow.Replace(c, "-");
                }
                screenRecorder = new Recorder(new ScreenRecorder(recordsFolder + dateNow + ".mp4", 30, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, 60));
                //audioRecorder = new AudioRecorder(recordsFolder + dateNow + ".wav");
                recordScreenButton.BackColor = Color.Lime;
            }
            else
            {
                screenRecorder.Dispose();
                //audioRecorder.Dispose();
                recordsList.Items.Clear();
                string[] recordsFiles = Directory.GetFiles(recordsFolder);
                foreach (string file in recordsFiles)
                {
                    recordsList.Items.Add(Path.GetFileName(file));
                }
                recordScreenButton.BackColor = Color.Red;
            }
        }

        private void urlVideos_MouseDown(object sender, MouseEventArgs e)
        {
            if (urlVideos.SelectedItem != null)
            {
                urlVideos.DoDragDrop(urlVideos.SelectedItem, DragDropEffects.Copy);
            }
        }

        private void recordsList_MouseDown(object sender, MouseEventArgs e)
        {
            if (recordsList.SelectedItem != null)
            {
                recordsList.DoDragDrop(recordsFolder + recordsList.SelectedItem, DragDropEffects.Copy);
            }
        }

        private void broadcastPlaylist_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void broadcastPlaylist_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            if (data != null)
            {
                File.Copy(data + "", playlistFolder + Path.GetFileName(data + ""), true);
                broadcastPlaylist.Items.Clear();
                string[] broadcastPlaylistFiles = Directory.GetFiles(playlistFolder);
                foreach (string file in broadcastPlaylistFiles)
                {
                    broadcastPlaylist.Items.Add(Path.GetFileName(file));
                }
            }
        }

        private void customizeChannelButton_Click(object sender, EventArgs e)
        {
            Form2 customizationForm = new Form2(this);
            customizationForm.ShowDialog();
        }
    }
}
