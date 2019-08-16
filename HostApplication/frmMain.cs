using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXTextControl.Parallel;
using TXTextControl.Parallel.DataContainer;

namespace HostApplication
{
    public partial class frmMain : Form
    {
        string sTemplateFolder = Application.StartupPath + "\\documents";
        string sResultsFolder = Application.StartupPath + "\\results";

        public frmMain()
        {
            InitializeComponent();

            // initial directory path values
            folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\documents";
            tbResultsFolder.Text = sResultsFolder;
            tbTemplateFolder.Text = sTemplateFolder;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            // create a new merge data object
            Report report = new Report()
            {
                Company = "Text Control, LLC",
                Firstname = "Alan",
                Name = "Tate",
                AddressLine = "6999 Shannon Willod Rd",
                City = "Charlotte",
                Sender = "William Smith",
                ZIP = "28226"
            };

            // read all templates
            String[] files = Directory.GetFiles(sTemplateFolder);

            // parallel for loop over all files
            Parallel.For(0, files.Length,
                         index => {
                             MergeDocument(files[index], report); // merge template
                         });
        }

        private void MergeDocument(string Filename, object Data)
        {
            // create a new PassingObject that is used to send
            // data to the ProcessingApplication using pipes
            PassingObject dataObject = new PassingObject()
            {
                Data = JsonConvert.SerializeObject(Data),
                Document = File.ReadAllBytes(Filename)
            };

            // call the processing app and pass the data object
            ReturningObject returnObject = ParallelProcessing.CallProcessingApp(dataObject);

            // create destination folder if it doesn't exists
            Directory.CreateDirectory(sResultsFolder);

            // write the returned byte array as a file
            File.WriteAllBytes(sResultsFolder + "\\" + Path.GetFileNameWithoutExtension(Filename) + ".pdf", returnObject.Document);
        }

        private void btnOpenTemplateFolder_Click(object sender, EventArgs e)
        {
            // set the template folder
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                sTemplateFolder = tbTemplateFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnOpenResultsFolder_Click(object sender, EventArgs e)
        {
            // set the results folder
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                sResultsFolder = tbResultsFolder.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
