using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Test
{
    public partial class App : Form
    {
        private string path;
        private string[] appItems;
        private string[] myItems;
        private int cur;

        public App()
        {
            InitializeComponent();
        }

        private void ButtonChoice_Click(object sender, EventArgs e)
        {
            ButtonForward.Enabled = false;
            ButtonBack.Enabled = false;
            Text = "Test";
            appItems = null;
            myItems = null;
            textBoxApp.Text = "";
            textBoxMy.Text = "";
            textBoxSource.Text = "";
            richTextBoxApp.Clear();
            richTextBoxMy.Clear();
            richTextBoxSource.Clear();

            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                    path = dialog.SelectedPath;

            if (path != null)
            {
                string[] dir = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

                if (dir.Length == 2 && dir[0].Contains("app") && dir[1].Contains("my"))
                {
                    SetSettings(dir[0], dir[1]);
                }
                if (dir.Length == 3 && dir[0].Contains("app") && dir[2].Contains("my"))
                {

                    SetSettings(dir[0], dir[2]);
                }

            }
        }

        private void SetSettings(string app, string my)
        {
            appItems = Directory.GetFiles(app, "*");
            myItems = Directory.GetFiles(my, "*");

            if (appItems.Length == myItems.Length)
            {
                ButtonForward.Enabled = true;
                ButtonBack.Enabled = true;
                Text = "Test - " + path;
                cur = -1;
            }
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            if (cur > 0) cur--;
            if (cur < 0) cur = 0;
            Checker(cur);
        }

        private void ButtonForward_Click(object sender, EventArgs e)
        {
            if (cur < appItems.Length - 1) cur++;
            Checker(cur);
        }

        private void Checker(int cur)
        {
            richTextBoxMy.Clear();
            richTextBoxApp.Clear();
            richTextBoxSource.Clear();

            try
            {
                string processPath = path + "\\Lexer.exe";
                string arguments = appItems[cur];

                textBoxApp.Text = arguments;
                textBoxMy.Text = myItems[cur];
                textBoxSource.Text = arguments;

                PrintSourceResult(arguments);
                PrintMyResult(myItems[cur]);

                Process ps = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = processPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                ps.ErrorDataReceived += Printer;
                ps.OutputDataReceived += Printer;

                ps.Start();
                ps.BeginErrorReadLine();
                ps.BeginOutputReadLine();


                void Printer(object sender, DataReceivedEventArgs e)
                {
                    Trace.WriteLine(e.Data);

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        richTextBoxApp.AppendText(e.Data + "\n");
                    }));
                }

            }
            catch (Exception e)
            {
                richTextBoxApp.Clear();
                richTextBoxApp.AppendText(e.ToString());
            }
        }

        private void PrintSourceResult(string arguments)
        {
            try
            {
                StreamReader sr = File.OpenText(arguments);
                string temp;

                while ((temp = sr.ReadLine()) != null)
                {
                    richTextBoxSource.AppendText(temp + "\n");
                }
            }
            catch (Exception e)
            {
                richTextBoxSource.Clear();
                richTextBoxSource.AppendText(e.ToString());
            }
        }

        private void PrintMyResult(string myItem)
        {
            try
            {
                StreamReader sr = File.OpenText(myItem);
                string temp;

                while ((temp = sr.ReadLine()) != null)
                {
                    richTextBoxMy.AppendText(temp + "\n");
                }
            }
            catch (Exception e)
            {
                richTextBoxMy.Clear();
                richTextBoxMy.AppendText(e.ToString());
            }
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left && ButtonBack.Enabled)
            {
                ButtonBack_Click(null, null);
            }
            else if (keyData == Keys.Right && ButtonForward.Enabled)
            {
                ButtonForward_Click(null, null);
            }
               
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
