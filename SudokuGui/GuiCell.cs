using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game;

namespace SudokuGui
{
    public partial class GuiCell : UserControl
    {
        private Board Board { get; set; }
        private int X { get; set; }
        private int Y { get; set; }
        public GuiCell(int x, int y, Board board)
        {
            InitializeComponent();
            X = x;
            Y = y;
            Board = board;
        }

        public event EventHandler UpdateGui;


        private delegate void TextChangedDelegate(object sender, EventArgs e);
        private delegate void SetValuesDelegate(object sender, string values);

        public void TextChanged(object sender, EventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)sender;
            if (richTextBox.InvokeRequired)
            {
                TextChangedDelegate textChangedDelegate = new TextChangedDelegate(TextChanged);
                richTextBox.Invoke(textChangedDelegate, new object[] { sender, e });
            }
            else
            {
                string text = richTextBox.Text;

                int value;
                int.TryParse(text, out value);

                if (value > 0 && value < 10)
                {
                    Board.SetValue(X, Y, value);
                    richTextBox.ReadOnly = true;
                    richTextBox.MouseLeave -= new System.EventHandler(this.MouseLeave);

                    Board.FigureStuffOut();

                    //UpdateGui.BeginInvoke(null, null, null, null);
                    UpdateGui.Invoke(null, null);
                }
            }
        }

        public void SetValue(object sender, string value)
        {
            RichTextBox richTextBox = (RichTextBox)sender;
            if (richTextBox.InvokeRequired)
            {
                SetValuesDelegate setValuesDelegate = new SetValuesDelegate(SetValue);
                richTextBox.Invoke(setValuesDelegate, new object[] { sender, value });
            }
            else
            {
                if (richTextBox.Text != value)
                {
                    richTextBox.Text = value;
                    richTextBox.BringToFront();
                    //UpdateGui.BeginInvoke(null, null, null, null);
                    UpdateGui.Invoke(null, null);
                }
            }
        }
        public void SetPossibleValues(object sender, string values)
        {
            RichTextBox richTextBox = (RichTextBox)sender;
            if (richTextBox.InvokeRequired)
            {
                SetValuesDelegate setValuesDelegate = new SetValuesDelegate(SetPossibleValues);
                richTextBox.Invoke(setValuesDelegate, new object[] { sender, values });
            }
            else
            {
                if (richTextBox.Text != values)
                {
                    richTextBox.Text = values;
                    //UpdateGui.BeginInvoke(null, null, null, null);
                    UpdateGui.Invoke(null, null);
                }
            }
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            richTextBox1.SendToBack();
            richTextBox2.Focus();
        }

        private void MouseLeave(object sender, EventArgs e)
        {
            richTextBox2.SendToBack();
            richTextBox1.Focus();
        }
    }
}
