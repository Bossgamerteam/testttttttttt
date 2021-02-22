using Ionic.Crc;
using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace LevelImposter
{
    class MapLoader
    {
        public BackgroundWorker backgroundWorker = new BackgroundWorker();
        public OpenFileDialog   browseDialog = new OpenFileDialog();
        public Label            mapLabel;
        public Button           applyButton;
        public ProgressBar      progressBar;
        public string           mapLocation = "";
        public static MapData   loadedMap;

        public MapLoader()
        {
            // Background Worker
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.LoadMap);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MapLoaded);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressReport);

            // Browse Dialog
            browseDialog.FileName = "map.zip";
            browseDialog.Filter = "Zip Files (*.zip)|*.zip|All files (*.*|*.*";
            browseDialog.Title = "Select Map";
        }

        public void Browse(object sender, EventArgs e)
        {
            DialogResult result = browseDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mapLocation = browseDialog.FileName;
                applyButton.Enabled = false;
                mapLabel.Text = "Loading Map...";
                backgroundWorker.RunWorkerAsync();
            }
        }

        public void LoadMap(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(0);

            ZipFile zip = ZipFile.Read(mapLocation);
            if (!zip.ContainsEntry("properties.json"))
            {
                e.Result = 1;
                return;
            }
            ZipEntry zipProperties = zip["properties.json"];
            CrcCalculatorStream streamOut = zipProperties.OpenReader();
            StreamReader streamReader = new StreamReader(streamOut);
            loadedMap = JsonConvert.DeserializeObject<MapData>(streamReader.ReadToEnd());

            backgroundWorker.ReportProgress(100);
            e.Result = 0;
        }

        public void MapLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            switch(e.Result)
            {
                case 0:
                    mapLabel.Text = loadedMap.name;
                    applyButton.Enabled = true;
                    break;
                case 1:
                    mapLabel.Text = "Invalid Map";
                    MessageBox.Show("Error: Invalid Map" + Environment.NewLine + Environment.NewLine + "The file you selected is not a valid Among Us map.");
                    break;
                default:
                    mapLabel.Text = "Unknown Error";
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
