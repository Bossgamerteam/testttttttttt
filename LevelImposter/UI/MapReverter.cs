using Ionic.Crc;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace LevelImposter
{
    class MapReverter
    {
        public BackgroundWorker backgroundWorker = new BackgroundWorker();
        public OpenFileDialog   browseDialog = new OpenFileDialog();
        public ProgressBar      progressBar;
        public string           gameLocation = "";

        public MapReverter()
        {
            // Background Worker
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.Revert);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GameReverted);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressReport);

            // Browse Dialog
            browseDialog.FileName = "Among Us.exe";
            browseDialog.Filter = "Exe Files (*.exe)|*.exe|All files (*.*)|*.*";
            browseDialog.Title = "Select Among Us Location";
        }

        public void Browse(object sender, EventArgs e)
        {
            DialogResult result = browseDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                gameLocation = browseDialog.FileName;
                backgroundWorker.RunWorkerAsync();
            }
        }

        public void Revert(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }
        public void Revert(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(0);

            // TODO Reverter

            backgroundWorker.ReportProgress(100);
            e.Result = 0;
        }

        public void GameReverted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch(e.Result)
            {
                case 0:
                    MessageBox.Show("Done!" + Environment.NewLine + Environment.NewLine + "To use, open Among Us, create a lobby in Polus, and ensure all players joining the lobby are using the same custom map.");
                    break;
                default:
                    MessageBox.Show("Error: Unknown" + Environment.NewLine + Environment.NewLine + "I don't know what happened, this error shouldn't be possible. Restart LevelImposter or contact a developer.");
                    break;
            }
        }

        private void ProgressReport(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
