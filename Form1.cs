using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using TP_Lab5._1;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace TP_Lab5._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.White;

        }

        public void Error(System.IO.StreamWriter f1, string Name, double x, double y)
        {
            f1.Write(string.Format("В файле {0} При X: {1} и Y: {2} произошла ошибка \n\n", Name, x, y));
        }

        public void Calculate()
        {

            Class2 class2 = new Class2();
            int cX = 0;
            foreach (double x in MatrixX.a)
            {
                cX++;
            }

            int cY = 0;

            foreach (double y in MatrixY.a)
            {
                cY++;
            }

            double[,] A = new double[cX, cY];

            int a = 0;
            int b = 0;
            foreach (double x in MatrixX.a)
            {
                b = 0;
                foreach (double y in MatrixY.a)
                {
                    A[a, b] = class2.G(x, y);
                    b++;
                }
                a++;
            }
            Matrix1 M1 = new Matrix1(a, b, A);
        }

        public void Print(int i, System.IO.StreamWriter f1)
        {
            int countZero = 4;
            string zeroForm = i.ToString().PadLeft(countZero, '0');
            string Name = (@"C:\Lab5\G" + zeroForm + ".dat");

            int a = 1;
            int cX = MatrixX.a.Length;
            int cY = MatrixY.a.Length;
            string[,] B = new string[cX + 1, cY + 1];
            
            foreach (double x in MatrixX.a)
            {
                int b = 1;
                B[0, 0] = "Y\\X";
                B[a, 0] = Convert.ToString(x);
                foreach (double y in MatrixY.a)
                {
                    B[0, b] = Convert.ToString(y);
                    if (double.IsNaN(Matrix1.a[a - 1, b - 1]))
                    {
                        B[a, b] = "NaN";
                        Error(f1, Name, a, b);
                    }
                    else
                    {
                        B[a, b] = Convert.ToString(Matrix1.a[a - 1, b - 1]);
                    }
                    b++;
                }
                a++;
            }

            var filestream = File.Create(Name);
            string FileName = filestream.Name;
            filestream.Close();

            StreamWriter f = new StreamWriter(FileName);
            f.Write(string.Format("((x^3 - 8) / y)^0.5\n"));
            f.Write(string.Format("Количество X: {0}\nКоличество Y: {1} \n\n", cX, cY));

            for (int b = 0; b < cY + 1; b++)
            {
                for (a = 0; a < cX + 1; a++)
                {
                    f.Write(string.Format("{0,20}", B[a, b]));
                }
                f.Write("\n");
            }
            f.Close();
        }

        public void CleanDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Lab5\");
            foreach (FileInfo f in dir.GetFiles())
            {
                f.Delete();
            }
        }

        public void MyLogCreate()
        {
            string Name = (@"C:\Lab5\myProgram.log");
            var filestream = File.Create(Name);
            string FileName = filestream.Name;
            filestream.Close();

            StreamWriter f = new StreamWriter(FileName);
            f.Write(string.Format("TP_Lab5.1 Dfhbfyn 32\n"));
            f.Write(string.Format(Convert.ToString(DateTime.Now) + "\n"));
            f.Write(string.Format("((x^3 - 8) / y)^0.5\n"));

            DirectoryInfo d = new DirectoryInfo(@"C:\Lab5\");
            FileInfo[] Files = d.GetFiles("*.dat");
            foreach (FileInfo file in Files)
            {
                string str = file.Name;
                f.Write(string.Format(str + "\n"));
            }
            f.Close();
        }

        public void GetFiles()
        {
            try
            {
                string[] dirs = Directory.GetFiles(@"C:\Lab5\", "*.dat");
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);

                    string[] lines = File.ReadAllLines(dir);
                    string[] Values = new string[lines.Length - 4];

                    int k = 0;
                    for (int i = 0; i < lines.Length - 4; i++)
                    {
                        Values[i] = lines[i + 4];
                        k++;
                        Console.WriteLine(Values[i]);
                    }

                    int c = 0;
                    for (int i = 0; i < lines.Length - 4; i++)
                    {
                        string results = Regex.Replace(Values[i], "(.{1,20})", "\"$1\"\n");
                        string[] itemsInLine = results.Split('\n');
                        c = itemsInLine.Length;
                    }

                    string[,] arr = new string[lines.Length - 4, c - 1];
                    double[,] arr1 = new double[lines.Length - 4, c - 1];

                    for (int i = 0; i < lines.Length - 4; i++)
                    {
                        string results = Regex.Replace(Values[i], "(.{1,20})", "\"$1\"\n");
                        string[] itemsInLine = results.Split('\n');
                        for (int j = 0; j < c - 1; j++)
                        {
                            string item = Regex.Replace(itemsInLine[j], @"\s+", String.Empty);
                            item.Replace(",", ".");
                            arr[i, j] = item;
                        }
                    }

                    for (int i = 0; i < lines.Length - 4; i++)
                    {
                        for (int j = 0; j < c  -1; j++)
                        {
                            Console.Write(string.Format("{0,20}", arr[i, j]));
                        }
                        Console.WriteLine();
                    }

                    Matrix M = new Matrix(lines.Length - 4, c -1, arr);
                }
            }
            catch { }
        }

        public void Create_Box(int a)
        {
            NumericUpDown numericUpDown1 = new NumericUpDown();// Ввод количества x
            numericUpDown1.Location = new Point(17, 35);
            numericUpDown1.Visible = true;
            numericUpDown1.Name = "X";
            numericUpDown1.Minimum = 1;
            tabControl1.TabPages[a].Controls.Add(numericUpDown1);

            Label label1 = new Label();
            label1.Location = new Point(17, 55);
            label1.Visible = true;
            label1.Name = "Xin";
            label1.Text = "Количество X";
            tabControl1.TabPages[a].Controls.Add(label1);

            System.Windows.Forms.Button Save1 = new System.Windows.Forms.Button();
            Save1.Location = new Point(150, 30);
            Save1.Visible = true;
            Save1.Size = new Size { Width = 100, Height = 30 };
            Save1.Text = "Ввести";
            tabControl1.TabPages[a].Controls.Add(Save1);
            Save1.Click += new EventHandler(Save_ClickX);


            NumericUpDown numericUpDown2 = new NumericUpDown();// Ввод количества y
            numericUpDown2.Location = new Point(300, 35);
            numericUpDown2.Visible = true;
            numericUpDown2.Name = "Y";
            numericUpDown2.Minimum = 1;
            tabControl1.TabPages[a].Controls.Add(numericUpDown2);

            Label label2 = new Label();
            label2.Location = new Point(300, 17);
            label2.Visible = true;
            label2.Name = "Yin";
            label2.Text = "Количество Y";
            tabControl1.TabPages[a].Controls.Add(label2);

            System.Windows.Forms.Button Save2 = new System.Windows.Forms.Button();
            Save2.Location = new Point(433, 30);
            Save2.Visible = true;
            Save2.Size = new Size { Width = 100, Height = 30 };
            Save2.Text = "Ввести";
            tabControl1.TabPages[a].Controls.Add(Save2);
            Save2.Click += new EventHandler(Save_ClickY);
        }

        public void Save_ClickX(object sender, EventArgs e)
        {
            string i = tabControl1.SelectedIndex.ToString(); //Получение индекса активной вкладки
            int i1 = Convert.ToInt32(i);
            int x = Convert.ToInt32(tabControl1.TabPages[i1].Controls["X"].Text) + 1;
            DataGridView dataGridViewX = new DataGridView();
            dataGridViewX.Name = "X1";
            dataGridViewX.Visible = true;
            dataGridViewX.Location = new Point(17, 100);
            dataGridViewX.ColumnCount = 1;
            dataGridViewX.Columns[0].HeaderText = "X";
            dataGridViewX.Columns[0].Name = "X";
            dataGridViewX.RowCount = x;
            dataGridViewX.RowHeadersVisible = false;
            dataGridViewX.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX.AllowUserToAddRows = false;
            tabControl1.TabPages[i1].Controls.Add(dataGridViewX);
        }

        public void Save_ClickY(object sender, EventArgs e)
        {
            string i = tabControl1.SelectedIndex.ToString();  //Получение индекса активной вкладки
            int i1 = Convert.ToInt32(i);
            int y = Convert.ToInt32(tabControl1.TabPages[i1].Controls["Y"].Text) + 1;
            DataGridView dataGridViewY = new DataGridView();
            dataGridViewY.Name = "Y1";
            dataGridViewY.Visible = true;
            dataGridViewY.Location = new Point(300, 100);
            dataGridViewY.ColumnCount = 1;
            dataGridViewY.Columns[0].Name = "Y";
            dataGridViewY.Columns[0].HeaderText = "Y";
            dataGridViewY.RowCount = y;
            dataGridViewY.RowHeadersVisible = false;
            dataGridViewY.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewY.AllowUserToAddRows = false;
            tabControl1.TabPages[i1].Controls.Add(dataGridViewY);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabPage newTabPage = new TabPage();
            tabControl1.TabPages.Add(newTabPage);
            string i = tabControl1.TabPages.IndexOf(newTabPage).ToString(); //Получение индекса последней вкладки
            int i1 = Convert.ToInt32(i);
            newTabPage.Text = "Набор " + (i1 + 1);
            Create_Box(i1);
        }

        public void Delete_TabPage(int a)
        {
            try
            {
                tabControl1.TabPages.RemoveAt(a);
            }
            catch { };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string i = tabControl1.SelectedIndex.ToString();  //Получение индекса активной вкладки
            int i1 = Convert.ToInt32(i);
            Console.WriteLine(i1);
            Delete_TabPage(i1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //CleanDirectory();
            var filestream1 = File.Create(@"C:\Lab5\myErrors.log");
            string FileName1 = filestream1.Name;
            filestream1.Close();
            StreamWriter f1 = new StreamWriter(FileName1, true);
            f1.Write(string.Format("((x^3 - 8) / y)^0.5\n"));

            int i = 0;
            foreach (TabPage tbp in tabControl1.TabPages)
            {
                DataGridView DgX = tabControl1.TabPages[i].Controls.Find("X1", true).FirstOrDefault() as DataGridView;
                int x = DgX.RowCount;
                double[] aX = new double[x];
                int k1 = 0;

                foreach (DataGridViewRow row in DgX.Rows)
                {
                    aX[k1] = Convert.ToDouble(row.Cells["X"].Value);
                    k1++;
                }
                MatrixX X = new MatrixX(x, aX);


                DataGridView DgY = tabControl1.TabPages[i].Controls.Find("Y1", true).FirstOrDefault() as DataGridView;
                int y = DgY.RowCount;
                double[] aY = new double[y];
                int k2 = 0;

                foreach (DataGridViewRow row in DgY.Rows)
                {
                    aY[k2] = Convert.ToDouble(row.Cells["Y"].Value);
                    k2++;
                }
                MatrixY Y = new MatrixY(y, aY);
                i++;
                Calculate();
                Print(i, f1);
            }
            f1.Close();

            MyLogCreate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetFiles();
        }

        public void BinaryWriter(int i)
        {
            int countZero = 4;
            string zeroForm = i.ToString().PadLeft(countZero, '0');
            string path = (@"C:\Lab5\G" + zeroForm + ".rez");

            int cX = MatrixX.a.Length;
            int cY = MatrixY.a.Length;

            BinaryWriter STGMatrix = new BinaryWriter(File.Open(path, FileMode.Create));
            int c = 8;
            STGMatrix.BaseStream.Position = 0;

            for (int k = 0; k < cY; k++)
            {
                for (int j = 0; j < cX; j++)
                {
                    STGMatrix.Write(Matrix1.a[j, k]);
                    STGMatrix.BaseStream.Position = c;
                    c += 8;
                }
            }
            c = Convert.ToInt32(STGMatrix.BaseStream.Length);
            STGMatrix.Close();

            BinaryWriter STGInput = new BinaryWriter(File.Open(path, FileMode.Append));
            foreach (double x in MatrixX.a)
            {
                STGInput.BaseStream.Position = c;
                STGInput.Write(x);
                c += 8;
            }
            foreach (double y in MatrixY.a)
            {
                STGInput.BaseStream.Position = c;
                STGInput.Write(y);
                c += 8;
            }
            STGInput.BaseStream.Position = c;
            STGInput.Write(Convert.ToDouble(cX));
            c += 8;
            STGInput.BaseStream.Position = c;
            STGInput.Write(Convert.ToDouble(cY));
            STGInput.Close();
        }

        public double BinaryReader(string path, string IndexX, string IndexY)
        {
            int countZero = 4;
            string zeroForm = path.ToString().PadLeft(countZero, '0');
            string Name = (@"C:\Lab5\G" + zeroForm + ".rez");
            Console.WriteLine(Name);

            double b = 0;
            int CountY;
            int CountX;
            using (BinaryReader reader = new BinaryReader(File.Open(Name, FileMode.Open)))
            {
                reader.BaseStream.Position = reader.BaseStream.Length - 16;
                CountX = Convert.ToInt32(reader.ReadDouble());
                //Console.WriteLine(CountX);

                reader.BaseStream.Position = reader.BaseStream.Length - 8;
                CountY = Convert.ToInt32(reader.ReadDouble());
                //Console.WriteLine(CountY);
            }

            using (BinaryReader reader = new BinaryReader(File.Open(Name, FileMode.Open)))
            {
                int f = (Convert.ToInt32(IndexY) * CountX + Convert.ToInt32(IndexX)) * 8;
                reader.BaseStream.Position = f;
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    b = reader.ReadDouble();
                    textBox1.Text = Convert.ToString(b);
                    //Console.WriteLine(b);
                }
            }
            return b;
        }

        public void BinaryWork ()
        {
            int i = 0;
            foreach (TabPage tbp in tabControl1.TabPages)
            {
                DataGridView DgX = tabControl1.TabPages[i].Controls.Find("X1", true).FirstOrDefault() as DataGridView;
                int x = DgX.RowCount;
                double[] aX = new double[x];
                int k1 = 0;

                foreach (DataGridViewRow row in DgX.Rows)
                {
                    aX[k1] = Convert.ToDouble(row.Cells["X"].Value);
                    k1++;
                }
                MatrixX X = new MatrixX(x, aX);
                //numericUpDown1.Maximum = k1 - 1;

                DataGridView DgY = tabControl1.TabPages[i].Controls.Find("Y1", true).FirstOrDefault() as DataGridView;
                int y = DgY.RowCount;
                double[] aY = new double[y];
                int k2 = 0;

                foreach (DataGridViewRow row in DgY.Rows)
                {
                    aY[k2] = Convert.ToDouble(row.Cells["Y"].Value);
                    k2++;
                }
                MatrixY Y = new MatrixY(y, aY);
                //numericUpDown2.Maximum = k2 - 1;
                i++;
                Calculate();
                BinaryWriter(i);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            BinaryWork();
            numericUpDown1.Visible = true;
            numericUpDown2.Visible = true;
            numericUpDown3.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            textBox1.Visible = true;
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDown3.Text) <= tabControl1.TabPages.Count)
            {
                double b = BinaryReader(numericUpDown3.Text, numericUpDown1.Text, numericUpDown2.Text);
                Console.WriteLine(b);
            }
            else
            {
                MessageBox.Show("Введите номер существующего набора");
                numericUpDown3.Text = "";
            }
                //BinaryReader(numericUpDown3.Text, numericUpDown1.Text, numericUpDown2.Text);
        }
    }
}