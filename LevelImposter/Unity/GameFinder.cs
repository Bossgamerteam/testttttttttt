using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelImposter
{
    class GameFinder
    {
        public static OpenFileDialog browseDialog = new OpenFileDialog();
        public static string directory = "C:/Program Files (x86)/Steam/steamapps/common/Among Us/Among Us_Data/";

        public static string FindDir()
        {
            browseDialog.FileName = "Among Us.exe";
            browseDialog.Filter = "Exe Files (*.exe)|*.exe|All files (*.*)|*.*";
            browseDialog.Title = "Select Among Us Location";

            if (!File.Exists(Path.Combine(directory, "sharedassets0.assets")))
            {
                MessageBox.Show(
                    "Error: Could not find Among Us installation." + Environment.NewLine +
                    "Please select the location of your Among Us.exe."
                );

                DialogResult result = browseDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    directory = Path.Combine(Path.GetDirectoryName(browseDialog.FileName), "Among Us_Data");
                }
                else
                {
                    return null;
                }
            }

            return directory;
        }
    }
}
