using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Test
{
    public partial class App : Form
    {
        private string path;
        private string[] appItems;
        private string[] myItems;
        private int cur;
        public delegate void myDelegate();
        public myDelegate Delegate;

        public App()
        {
            InitializeComponent();
            Delegate = new myDelegate(CheckStatusControl);

            try
            {
                path = @"C:\Users\Slava\Desktop\tester";
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
            catch(Exception e)
            {

            }
        }

        private void ButtonChoice_Click(object sender, EventArgs e)
        {
            StatusControl.BackColor = System.Drawing.Color.Gainsboro;
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
            StatusControl.BackColor = System.Drawing.Color.Gainsboro;
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
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8
                    }
                };

                ps.EnableRaisingEvents = true;
                ps.ErrorDataReceived += Printer;
                ps.OutputDataReceived += Printer;

                ps.Exited += (sender, e) =>
                {
                    Thread.Sleep(200);
                    Invoke(Delegate);
                };

                ps.Start();
                ps.BeginErrorReadLine();
                ps.BeginOutputReadLine();
            }
            catch (Exception e)
            {
                richTextBoxApp.Clear();
                richTextBoxApp.AppendText(e.ToString());
            }
        }

        private void CheckStatusControl()
        {
            var myTextBox = richTextBoxMy.Text;
            var appTextBox = richTextBoxApp.Text;

            appTextBox = appTextBox.Replace('\n', ' ');
            appTextBox = appTextBox.Replace('\t', ' ');
            myTextBox = myTextBox.Replace("\n", " ");
            myTextBox = myTextBox.Replace("\t", " ");

            var myText = myTextBox.Split(' ').ToList();
            var appText = appTextBox.Split(' ').ToList();

            myText.RemoveAll(x => x == "");
            appText.RemoveAll(x => x == "");

            if (myText.Count == 0 && appText.Count == 0) StatusControl.BackColor = System.Drawing.Color.Green;
            else if (myText.Count == appText.Count)
            {
                StatusControl.BackColor = System.Drawing.Color.Green;

                for (int i = 0; i < myText.Count; i++)
                {
                    if (myText[i] != appText[i])
                    {
                        StatusControl.BackColor = System.Drawing.Color.Red;
                    }
                }

            }
            else
            {
                StatusControl.BackColor = System.Drawing.Color.Red;
            }



        }

        private void Printer(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data);

            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (e.Data != null) richTextBoxApp.AppendText(e.Data + "\n");
            }));
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
