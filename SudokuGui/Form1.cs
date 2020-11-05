using Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuGui
{
    public partial class Form1 : Form
    {
        Board board;

        GuiCell[,] guiCells;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board();
            guiCells = new GuiCell[9, 9];
            Panel[,] panels = new Panel[3, 3];
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Panel panel = new Panel();
                    this.Controls.Add(panel);
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.Size = new Size(150, 150);
                    panel.Location = new Point(x * 150, y * 150);
                    panels[x, y] = panel;
                }
            }


            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    Panel panel = panels[x / 3, y / 3];
                    GuiCell guiCell = new GuiCell(x, y, board);
                    guiCells[x, y] = guiCell;
                    panel.Controls.Add(guiCell);
                    guiCell.Location = new Point(x % 3 * 50, y % 3 * 50);
                    guiCell.UpdateGui += GuiCell_UpdateGui;
                }
            }
        }

        private void GuiCell_UpdateGui(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void UpdateGui()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    GuiCell guiCell = guiCells[x, y];
                    Cell cell = board.BoardLayout[x, y];
                    if (cell.Value != null)
                    {
                        guiCell.SetValue(guiCell.richTextBox2, cell.Value.ToString());
                    }
                    else
                    {
                        StringBuilder possibleValues = new StringBuilder();

                        AddRow(cell, possibleValues, 1);
                        AddRow(cell, possibleValues, 4);
                        AddRow(cell, possibleValues, 7);

                        guiCell.SetPossibleValues(guiCell.richTextBox1, possibleValues.ToString());
                    }
                }
            }
        }

        private static void AddRow(Cell cell, StringBuilder possibleValues, int offSet)
        {
            for (int i = offSet; i < offSet + 2; i++)
            {
                if (cell.PossibleValues.Contains(i))
                {
                    possibleValues.Append(i.ToString() + " ");
                }
                else
                {
                    possibleValues.Append("  ");
                }

            }
            if (cell.PossibleValues.Contains(offSet + 2))
            {
                possibleValues.Append((offSet + 2).ToString());
            }
            else
            {
                possibleValues.Append("");
            }
            possibleValues.Append("\n");
        }
    }
}
