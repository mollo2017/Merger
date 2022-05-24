using System;

namespace Merger
{
    public partial class Form1 : Form
    {
        private string InPath = string.Empty;
        private string OutPath = string.Empty;
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
                    FileName = FilesUtils.GetFileName(Fl);
                    WriteLog(FileName, "Trabajando", "", 2);
                    Content = FilesUtils.GetFileContent(Fl);
                    FilesUtils.WriteFile(OutPath, Content);
                    WriteLog(FileName, "", Content, 3);
                    WriteLog(FileName, "", Content, 1000);
                }
            }
            catch (Exception ex) 
            {
                WriteLog(FileName, "Error", ex.Message, 2);
            }
        }

        private void WriteLog(string FileName, string Event, string Des, int Op)
        {
            string sep = @"//*********************************************************************************";
            switch(Op)
            {
                case 1:
                    textBoxLog.ForeColor = Color.Black;
                    textBoxLog.Text += string.Format("{0}\n\t\t{2} en {1}\n{0}\n", sep, FileName, Event);
                    break;
                case 2:
                    textBoxLog.ForeColor = Color.Red;
                    textBoxLog.Text += string.Format("{0}\n\t\t{2} en {1}\n{0}\n{3}", sep, FileName, Event, Des);
                    break;
                case 3:
                    textBoxLog.Text += (Des + "\n");
                    break;
                default:
                    textBoxLog.Text += "\n\n";
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