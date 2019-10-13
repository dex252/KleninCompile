namespace Test
{
    partial class App
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ButtonBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonForward = new System.Windows.Forms.ToolStripButton();
            this.ButtonChoice = new System.Windows.Forms.ToolStripButton();
            this.richTextBoxApp = new MyRichTextBox();
            this.textBoxApp = new System.Windows.Forms.TextBox();
            this.textBoxMy = new System.Windows.Forms.TextBox();
            this.richTextBoxMy = new MyRichTextBox();
            this.richTextBoxSource = new MyRichTextBox();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonBack,
            this.toolStripSeparator1,
            this.ButtonForward,
            this.ButtonChoice});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1264, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ButtonBack
            // 
            this.ButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonBack.Enabled = false;
            this.ButtonBack.Image = ((System.Drawing.Image)(resources.GetObject("ButtonBack.Image")));
            this.ButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Size = new System.Drawing.Size(23, 22);
            this.ButtonBack.Text = "toolStripButton1";
            this.ButtonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonForward
            // 
            this.ButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonForward.Enabled = false;
            this.ButtonForward.Image = ((System.Drawing.Image)(resources.GetObject("ButtonForward.Image")));
            this.ButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonForward.Name = "ButtonForward";
            this.ButtonForward.Size = new System.Drawing.Size(23, 22);
            this.ButtonForward.Text = "toolStripButton2";
            this.ButtonForward.Click += new System.EventHandler(this.ButtonForward_Click);
            // 
            // ButtonChoice
            // 
            this.ButtonChoice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButtonChoice.Image = ((System.Drawing.Image)(resources.GetObject("ButtonChoice.Image")));
            this.ButtonChoice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonChoice.Name = "ButtonChoice";
            this.ButtonChoice.Size = new System.Drawing.Size(171, 22);
            this.ButtonChoice.Text = "Выбрать папку для проверки";
            this.ButtonChoice.Click += new System.EventHandler(this.ButtonChoice_Click);
            // 
            // richTextBoxApp
            // 
            this.richTextBoxApp.Location = new System.Drawing.Point(6, 54);
            this.richTextBoxApp.Name = "richTextBoxApp";
            this.richTextBoxApp.ReadOnly = true;
            this.richTextBoxApp.Size = new System.Drawing.Size(410, 700);
            this.richTextBoxApp.TabIndex = 2;
            this.richTextBoxApp.Text = "";
            // 
            // textBoxApp
            // 
            this.textBoxApp.Location = new System.Drawing.Point(6, 28);
            this.textBoxApp.Name = "textBoxApp";
            this.textBoxApp.ReadOnly = true;
            this.textBoxApp.Size = new System.Drawing.Size(410, 20);
            this.textBoxApp.TabIndex = 4;
            // 
            // textBoxMy
            // 
            this.textBoxMy.Location = new System.Drawing.Point(842, 28);
            this.textBoxMy.Name = "textBoxMy";
            this.textBoxMy.ReadOnly = true;
            this.textBoxMy.Size = new System.Drawing.Size(410, 20);
            this.textBoxMy.TabIndex = 5;
            // 
            // richTextBoxMy
            // 
            this.richTextBoxMy.Location = new System.Drawing.Point(842, 54);
            this.richTextBoxMy.Name = "richTextBoxMy";
            this.richTextBoxMy.ReadOnly = true;
            this.richTextBoxMy.Size = new System.Drawing.Size(410, 700);
            this.richTextBoxMy.TabIndex = 6;
            this.richTextBoxMy.Text = "";
            // 
            // richTextBoxSource
            // 
            this.richTextBoxSource.Location = new System.Drawing.Point(422, 54);
            this.richTextBoxSource.Name = "richTextBoxSource";
            this.richTextBoxSource.ReadOnly = true;
            this.richTextBoxSource.Size = new System.Drawing.Size(410, 700);
            this.richTextBoxSource.TabIndex = 8;
            this.richTextBoxSource.Text = "";
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(422, 28);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.ReadOnly = true;
            this.textBoxSource.Size = new System.Drawing.Size(410, 20);
            this.textBoxSource.TabIndex = 9;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.richTextBoxSource);
            this.Controls.Add(this.richTextBoxMy);
            this.Controls.Add(this.textBoxMy);
            this.Controls.Add(this.textBoxApp);
            this.Controls.Add(this.richTextBoxApp);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "App";
            this.Text = "Test";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ButtonBack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ButtonForward;
        private MyRichTextBox richTextBoxApp;
        private System.Windows.Forms.TextBox textBoxApp;
        private System.Windows.Forms.TextBox textBoxMy;
        private MyRichTextBox richTextBoxMy;
        private System.Windows.Forms.ToolStripButton ButtonChoice;
        private MyRichTextBox richTextBoxSource;
        private System.Windows.Forms.TextBox textBoxSource;
    }
}

