using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HANOI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int column1, column2, column3;
        int circles, target = 0,point = 0;
        ListBox listbox;
        TextBox textbox;
        PictureBox picturebox;
        Timer timer;
        Graphics graphics;
        Bitmap bitmab;
        SolidBrush solid;
        Pen pen;
        int[,] box;
        char[,] move;
        private void Hanoi(int circles, char A,char B,char C)
        {
            if (circles==1)
            {
                listbox.Items.Add(A + ">>>" + C);
                move[0, target] = A;
                move[1, target] = C;
                target++;
            }
            else
            {
                Hanoi(circles-1,A,C,B);
                Hanoi(1,A,B,C);
                Hanoi(circles-1,B,A,C);
            }
        }
        private void Create()
        {
            pen = new Pen(Color.DarkCyan, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < circles; j++)
                {
                    if (box[i, j] != 0)
                    {
                        switch (box[i, j])
                        {
                            case 1: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 2: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 3: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 4: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 5: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 6: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 7: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 8: solid = new SolidBrush(Color.DarkSeaGreen); break;
                            case 9: solid = new SolidBrush(Color.DarkSeaGreen); break;
                        }
                        graphics.FillRectangle(solid, (i + 1) * 190 - 60 - (box[i, j]) * 11, 320 - j * 25, box[i, j] * 22, 25);
                    }
                }
            }
        }
        private void Button1_click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            listbox.Items.Clear();
            circles = int.Parse(textbox.Text);
            box = new int[3, circles];
            move = new char[2, 1000];
            for (int i = 0; i < circles; i++) { box[0, i] = circles - i; }
            Hanoi(circles, 'A', 'B', 'C');
            Create();
            timer.Enabled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listbox = new ListBox();
            listbox.Parent = this;
            listbox.Location = new Point(760, 100);
            listbox.Size = new Size(70, 408);

            Button button1 = new Button();
            button1.Parent = this;
            button1.Location = new Point(235, 40);
            button1.Click += new EventHandler(Button1_click);
            button1.Text = "Create";

            Label label1= new Label();
            label1.Parent = this;
            label1.Location = new Point(130, 15);
            label1.Text = "enter game";

            textbox = new TextBox();
            textbox.Parent = this;
            textbox.Location = new Point(130, 40);

            picturebox = new PictureBox();
            picturebox.Parent = this;
            picturebox.Location = new Point(50, 100);
            picturebox.Size = new Size(700, 403);
            picturebox.BackColor = Color.White;

            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += new EventHandler(Visual);

            bitmab = new Bitmap(picturebox.Width, picturebox.Height);
            graphics = Graphics.FromImage(bitmab);
        }
        private void Visual(object sender,EventArgs e)
        {
            graphics.Clear(Color.White);

            column1 = -1;
            column2 = -1;
            column3 = -1;

            graphics.DrawLine(pen,42,345,218,345);
            graphics.DrawLine(pen,232,345,408,345);
            graphics.DrawLine(pen,422,345,598,345);
            graphics.DrawLine(pen,130,345,130,150);
            graphics.DrawLine(pen,320,345,320,150);
            graphics.DrawLine(pen,510,345,510,150);
            graphics.DrawString("A                             B                               C",new Font("Arial",15),Brushes.Green,200,150);

            for (int i = 0; i < circles; i++) { if (box[0, i] == 0) { column1 = i; break; } }
            for (int i = 0; i < circles; i++) { if (box[1, i] == 0) { column2 = i; break; } }
            for (int i = 0; i < circles; i++) { if (box[2, i] == 0) { column3 = i; break; } }
            if (column1 == -1) { column1 = circles; }
            if (column2 == -1) { column2 = circles; }
            if (column3 == -1) { column3 = circles; }
            switch (move[0,point])
            {
                case 'A':
                    switch (move[1, point])
                    {
                        case 'B':
                            box[1, column2] = box[0, column1 - 1];
                            box[0, column1 - 1] = 0;
                            break;
                        case 'C':
                            box[2, column3] = box[0, column1 - 1];
                            box[0, column1 - 1] = 0;
                            break;
                    }
                    break;
                case 'B':
                    switch (move[1, point])
                    {
                        case 'A':
                            box[0, column1] = box[1, column2 - 1];
                            box[1, column2 - 1] = 0;
                            break;
                        case 'C':
                            box[2, column3] = box[1, column2 - 1];
                            box[1, column2 - 1] = 0;
                            break;
                    }
                    break;
                case 'C':
                    switch (move[1, point])
                    {
                        case 'A':
                            box[0, column1] = box[2, column3 - 1];
                            box[2, column3 - 1] = 0;
                            break;
                        case 'B':
                            box[1, column2] = box[2, column3 - 1];
                            box[2, column3 - 1] = 0;
                            break;
                    }
                    break;
            }
            Create();
            picturebox.Image = bitmab;
            point++;
        }
    }
}