using Ionic.Crc;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace LevelImposter
{
    class MapApplier
    {
        public BackgroundWorker backgroundWorker = new BackgroundWorker();
        public ProgressBar      progressBar;
        public string           gameLocation = "";
        public MapData          loadedMap;

        public MapApplier()
        {
            // Background Worker
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.Apply);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MapApplied);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressReport);
        }

        public void Apply(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }
        public void Apply(object sender, DoWorkEventArgs e)
        {
            // Find Game
            backgroundWorker.ReportProgress(0);
            string gameDirectory = GameFinder.FindDir();
            if (gameDirectory == null)
            {
                e.Result = 1;
                return;
            }

            // Check Version
            if (!VersionCheck.IsCurrent())
            {
                SystemSounds.Hand.Play();
                DialogResult result = MessageBox.Show("Warning: Invalid Version" + Environment.NewLine + Environment.NewLine +
                    "This version of Level Imposter is designed for Among Us " + VersionCheck.AMONGUS_VERSION + ". Either update LevelImposter or continue at your own risk.","",MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    e.Result = 4;
                    return;
                }
            }

            // Check Map
            MapData map = MapLoader.loadedMap;
            if (map.Equals(default(MapData)))
            {
                e.Result = 2;
                return;
            }

            // Clear Polus
            MapWriter mapWriter = new MapWriter();
            mapWriter.ClearPolus();

            // Load All Assets
            for (var i = 0; i < map.assets.Length; i++)
            {
                backgroundWorker.ReportProgress((int)(((double)i / map.assets.Length) * 100));
                mapWriter.AddAsset(map.assets[i]);
            }

            // Export
            backgroundWorker.ReportProgress(-1);
            try
            {
                mapWriter.Export();
            }
            catch
            {
                e.Result = 3;
                return;
            }

            // Return
            backgroundWorker.ReportProgress(100);
            e.Result = 0;
        }

        public void MapApplied(object sender, RunWorkerCompletedEventArgs e)
        {
            switch(e.Result)
            {
                case 0:
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show("Done!" + Environment.NewLine + Environment.NewLine + "To use, open Among Us, create a lobby in Polus, and ensure all players joining the lobby are using the same custom map.");
                    break;
                case 1:
                    // Browse for Among Us Location has been Cancelled
                    break;
                case 2:
                    MessageBox.Show("Error: No Map Loaded" + Environment.NewLine + Environment.NewLine + "You must select a custom map to apply first!");
                    break;
                case 3:
                    MessageBox.Show("Error: IOException" + Environment.NewLine + Environment.NewLine + "Among Us is most likely being updated or downloaded, please wait for the download to finish before using Level Imposter.");
                    break;
                case 4:
                    // Cancel due to invalid version
                    break;
                default:
                    MessageBox.Show("Error: Unknown" + Environment.NewLine + Environment.NewLine + "I don't know what happened, this error shouldn't be possible. Restart LevelImposter or contact a developer.");
                    break;
            }
        }

        private void ProgressReport(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
                progressBar.Style = ProgressBarStyle.Marquee;
            else
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = e.ProgressPercentage;
            }
        }
    }
}
