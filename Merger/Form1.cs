using System;
using System.Diagnostics;

namespace Merger
{
    public partial class Form1 : Form
    {
        private string InPath = string.Empty;
        private string OutPath = string.Empty;
        private string sepIni = @"/*********************************************************************************";
        private string sepFin = @"*********************************************************************************/";
        private string breakLine = Environment.NewLine;
        private string tab = "\t";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GetFiles();
        }

        private void GetFiles()
        {
            string[] lst = Directory.GetFiles(InPath);
            foreach(var sFile in lst)
            {
                textBoxLog.Text += sFile + Environment.NewLine;
            }
        }

        private void buttonMerge_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Seleccione la ruta de origen antes de comenzar");
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Seleccione la ruta destino antes de comenzar");
                return;
            }

            Merge();
        }

        private void Merge()
        {
            string FileName = string.Empty;
            string Content = string.Empty;
            try
            {
                List<string> Fls = FilesUtils.GetFileContentByLine(InPath);
                foreach(string Fl in Fls)
                {
                    if(!string.IsNullOrEmpty(Fl))
                    {
                        FileName = FilesUtils.GetFileName(Fl);
                        WriteLog(FileName, "Trabajando", "", 1);
                        Content = GetheaderFile(FileName) + FilesUtils.GetFileContent(Fl);
                        FilesUtils.WriteFile(OutPath, Content);
                        WriteLog(FileName, "", Content, 3);
                        WriteLog(FileName, "", Content, 1000);
                    }
                }
                Process.Start("explorer.exe", OutPath.Replace(FilesUtils.GetFileName(OutPath), ""));
            }
            catch (Exception ex) 
            {
                WriteLog(FileName, "Error", ex.Message, 2);
                FilesUtils.DeleteFile(OutPath);
            }

        }
        private string GetheaderFile(string FileName)
        {
            string headerFile = string.Empty;
            headerFile = string.Format(@"{2}{0}{2}{4}{4}{1}{2}{3}{2}", sepIni, FileName, breakLine, sepFin, tab);
            return headerFile;
        }
        /// <summary>
        /// Fuction to write a log of process
        /// </summary>
        /// <param name="FileName"> File Name</param>
        /// <param name="Event">Event in process</param>
        /// <param name="Des">Descrition of process or fail</param>
        /// <param name="Op">Option to print in case of error or process header</param>
        private void WriteLog(string FileName, string Event, string Des, int Op)
        {
            switch(Op)
            {
                case 1://Comentary
                    textBoxLog.ForeColor = Color.Black;
                    textBoxLog.Text += string.Format("{0}{3}{5}{5}{2} en {1}{3}{4}{3}", sepIni, FileName, Event, breakLine, sepFin, tab);
                    break;
                case 2://Fail
                    textBoxLog.ForeColor = Color.Red;
                    textBoxLog.Text += string.Format("{0}{4}{6}{6}{2} en {1}{4}{5}{4}{3}", sepIni, FileName, Event, Des, breakLine, sepFin, tab);
                    break;
                case 3://process
                    textBoxLog.ForeColor = Color.Blue;
                    textBoxLog.Text += string.Format("{0}{1}", Des, breakLine);
                    break;
                default:
                    textBoxLog.Text += breakLine;
                    textBoxLog.Text += breakLine;
                    break;
            }
        }

        private void buttoSave_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
                OutPath = openFileDialog1.FileName;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string FullFileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                InPath = openFileDialog1.FileName;
                textBox2.Text = InPath;
                FullFileName = FilesUtils.GetFileName(InPath);
                OutPath = FilesUtils.GetFileName(InPath.Replace(FullFileName, ""), "Ejecuciones", "sql");
                textBox3.Text = OutPath;
            }
        }
    }
}