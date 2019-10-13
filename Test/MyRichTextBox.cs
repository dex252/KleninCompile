using System.Windows.Forms;

namespace Test
{
    class MyRichTextBox : RichTextBox
    {
        protected override bool DoubleBuffered { get; set; } = true;
    }
}
