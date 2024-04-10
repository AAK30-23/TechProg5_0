using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace TP_Lab5._1
{
    public partial class Form1 : Form
    {
        private List<List<string>> tabDataList;
        public Form1()
        {
            InitializeComponent();
            tabDataList = new List<List<string>>();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void InitComponents(int a)
        {
            TabPage tabPages = new TabPage(" " + (tabControl1.TabCount + 1));

            //Lables
            System.Windows.Forms.Label Label1 = new System.Windows.Forms.Label();
            Label1.Location = label5.Location;
            Label1.Size = label5.Size;
            Label1.Text = label5.Text;
            tabControl1.TabPages[a].Controls.Add(Label1);

            System.Windows.Forms.Label Label2 = new System.Windows.Forms.Label();
            Label2.Location = label6.Location;
            Label2.Size = label6.Size;
            Label2.Text = label6.Text;
            tabControl1.TabPages[a].Controls.Add(Label2);

            System.Windows.Forms.Label Label3 = new System.Windows.Forms.Label();
            Label3.Location = label7.Location;
            Label3.Size = label7.Size;
            Label3.Text = label7.Text;
            tabControl1.TabPages[a].Controls.Add(Label3);

            System.Windows.Forms.Label Label4 = new System.Windows.Forms.Label();
            Label4.Location = label8.Location;
            Label4.Size = label8.Size;
            Label4.Text = label8.Text;
            tabControl1.TabPages[a].Controls.Add(Label4);

            for (int i = 0; i < 4; i++)
            {
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
                textBox.Location = new Point(60, 10 + i * 50);
                textBox.Size = new Size(150, 20);
                tabControl1.TabPages[a].Controls.Add(textBox);
            }
        }

        //добавить набор
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TabPage newTabPage = new TabPage(" " + (tabControl1.TabCount + 1));
            tabControl1.TabPages.Add(newTabPage);
            InitComponents(Convert.ToInt32(tabControl1.TabPages.IndexOf(newTabPage).ToString()));
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Нет вкладок для удаления");
            }
        }
        //рассчитать все наборы

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }


        //заполняем А значениями из вызова функции После заполнения матрицы A
        //создается новый объект Mat_ с размерами p и q, и заполненный значениями матрицы A.
        public void Calculate()
        {
            Class2 class2 = new Class2();
            int _X = 0;
            foreach (double x in MatX.p)
            {
                _X++;
            }
            int _Y = 0;
            foreach (double y in MatY.p)
            {
                _Y++;
            }
            double[,] A = new double[_X, _Y];
            int p = 0;
            int q = 0;
            foreach (double x in MatX.p)
            {
                q = 0;
                foreach (double y in MatY.p)
                {
                    A[p, q] = class2.G(x, y);
                    q++;
                }
                p++;
            }
            Mat_ M1 = new Mat_(p, q, A);
        }

        public void Fill(int i, System.IO.StreamWriter f1)
        {
            string Name_0 = i.ToString().PadLeft(4, '0');
            string Name = Path.Combine(Directory.GetCurrentDirectory(), "G" + Name_0 + ".dat");

            int p = 1;
            int _X = MatX.p.Length;
            int _Y = MatY.p.Length;
            string[,] st = new string[_X + 1, _Y + 1];
            
            foreach (double x in MatX.p)
            {
                int Isk = 1;
                st[0, 0] = "Y\\X";
                st[p, 0] = Convert.ToString(x);
                foreach (double y in MatY.p)
                {
                    st[0, Isk] = Convert.ToString(y);
                    if (double.IsNaN(Mat_.p[p - 1, Isk - 1]))
                    {
                        st[p, Isk] = "NaN";
                        CreateMyError(f1, Name, p, Isk);
                    }
                    else
                    {
                        st[p, Isk] = Convert.ToString(Mat_.p[p - 1, Isk - 1]);
                    }
                    Isk++;
                }
                p++;
            }

            var filest = File.Create(Name);
            string FileName = filest.Name;
            filest.Close();

            StreamWriter f = new StreamWriter(FileName);
            f.Write(string.Format("y / Math.Sin(-Math.Pow(x, 2))\n"));
            f.Write(string.Format($"Количество X: {_X}\nКоличество Y: {_Y} \n\n"));

            for (int Isk = 0; Isk < _Y + 1; Isk++)
            {
                for (p = 0; p < _X + 1; p++)
                {
                    f.Write(string.Format("{0,30}", st[p, Isk]));
                }
                f.Write("\n");
            }
            f.Close();
        }

        public void CreateMyProgram()
        {
            string logDir = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";
            Directory.CreateDirectory(logDir); 
            string FilePath = logDir + "myProgram.log";
            var filest = File.Create(FilePath);
            string FileName = filest.Name;
            filest.Close();

            StreamWriter f = new StreamWriter(FileName);
            f.Write(string.Format("TechProg5_0 3\n"));
            f.Write(string.Format(Convert.ToString(DateTime.Now) + "\n"));
            f.Write(string.Format("y / Math.Sin(-Math.Pow(x, 2))\n"));

            DirectoryInfo d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileInfo[] Files = d.GetFiles("*.dat");
            foreach (FileInfo file in Files)
            {
                string str = file.Name;
                f.Write(string.Format(str + "\n"));
            }
            f.Close();
        }

        public void CreateMyError(System.IO.StreamWriter f1, string Name, double x, double y)
        {
            f1.Write(string.Format($"Файл {Name}: x = {x} и y = {y}. Ошибка \n\n"));
        }

        public void inFiles()
        {
            try
            {
                string defaultPath = AppDomain.CurrentDomain.BaseDirectory;
                string[] dirs = Directory.GetFiles(defaultPath, "*.dat");
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
                        string results = Regex.Replace(Values[i], "(.{1,30})", "\"$1\"\n");
                        string[] itemsInLine = results.Split('\n');
                        c = itemsInLine.Length;
                    }
                    string[,] arr = new string[lines.Length - 4, c - 1];
                    double[,] arr1 = new double[lines.Length - 4, c - 1];
                    for (int i = 0; i < lines.Length - 4; i++)
                    {
                        string results = Regex.Replace(Values[i], "(.{1,30})", "\"$1\"\n");
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
                            Console.Write(string.Format("{0,30}", arr[i, j]));
                        }
                        Console.WriteLine();
                    }
                    Mat M = new Mat(lines.Length - 4, c -1, arr);
                }
            }
            catch { }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            string defaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyErrors.log");
            var files = File.Create(defaultPath);
            string FileName1 = files.Name;
            files.Close();
            StreamWriter f1 = new StreamWriter(FileName1, true);
            f1.Write(string.Format("y / Math.Sin(-Math.Pow(x, 2))\n"));
            tabDataList.Clear();
            int i = 0;
            foreach (TabPage page in tabControl1.TabPages)
            {
                List<string> tabValues = new List<string>();
                foreach (Control control in page.Controls)
                {
                    if (control is System.Windows.Forms.TextBox textBox)
                    {
                        tabValues.Add(textBox.Text);
                    }
                }
                tabDataList.Add(tabValues);  
            }
            foreach (List<string> tabValues in tabDataList)
            {
                double X0 = double.Parse(tabValues[0]);
                //Console.WriteLine(X0);
                double Xk = double.Parse(tabValues[1]);
                //Console.WriteLine(Xk);
                int Nx = int.Parse(tabValues[2]);
                //Console.WriteLine(Nx);

                double[] aX = new double[Nx];
                for (int q = 0; q < Nx; q++)
                {
                    aX[q] = X0 + q * (Xk - X0) / (Nx - 1);
                }
                MatX X = new MatX(Nx, aX);

                int Ny = int.Parse(tabValues[3]);
                Random random = new Random();
                double[] aY = new double[Ny];
                for (int j = 0; j < Ny; j++)
                {
                    aY[j] = random.Next(-1000, 1000);
                }
                MatY Y = new MatY(Ny, aY);

                Calculate();
                Fill(i, f1);
                i++;
            }
            f1.Close();

            CreateMyProgram();
        }
        private void buttonFromFile_Click(object sender, EventArgs e)
        {
            inFiles();
        }

        public void Write(int i)
        {
            string FileName = i.ToString().PadLeft(4, '0');
            string path = Path.Combine(Directory.GetCurrentDirectory(), "G" + FileName + ".rez"); 

            //размеры массивов
            int _X = MatX.p.Length;
            int _Y = MatY.p.Length;

            BinaryWriter Matrix = new BinaryWriter(File.Open(path, FileMode.Create));
            int sh = 8;
            Matrix.BaseStream.Position = 0; //начальная позиция
            //циклом проходим по элементам двумерного массива `Mat_.p`,
            //записывая каждый элемент в файл, оставляя место под элемент 8 (ТИП double - 8 байт).
            for (int k = 0; k < _Y; k++)
            {
                for (int j = 0; j < _X; j++)
                {
                    Matrix.Write(Mat_.p[j, k]);
                    Matrix.BaseStream.Position = sh;
                    sh += 8;
                }
            }
            sh = Convert.ToInt32(Matrix.BaseStream.Length); // вся длина конечного файла
            Matrix.Close();

            BinaryWriter In = new BinaryWriter(File.Open(path, FileMode.Append)); //открывает файл для записи в режиме append
            //записываем элементы массивов
            foreach (double x in MatX.p)
            {
                In.BaseStream.Position = sh;
                In.Write(x);
                sh += 8;
            }
            foreach (double y in MatY.p)
            {
                In.BaseStream.Position = sh;
                In.Write(y);
                sh += 8;
            }
            In.BaseStream.Position = sh;
            In.Write(Convert.ToDouble(_X));
            sh += 8;
            In.BaseStream.Position = sh;
            In.Write(Convert.ToDouble(_Y));
            In.Close();
        }

        public double Read(string path, string IndexX, string IndexY)
        {
            string Name_0 = path.ToString().PadLeft(4, '0');
            string Name = Path.Combine(Directory.GetCurrentDirectory(), "G" + Name_0 + ".rez");
            Console.WriteLine(Name);

            double Isk = 0;
            int ColX;
            //считываем из файла информацию о размере матрицы
            using (BinaryReader reader = new BinaryReader(File.Open(Name, FileMode.Open)))
            {
                reader.BaseStream.Position = reader.BaseStream.Length - 16; //находим кол-во x (предпоследний элемент файла)
                ColX = Convert.ToInt32(reader.ReadDouble());
            }

            //открываем файл для чтения и перемещаем указатель позиции в файле к нужному элементу, используя формулу для вычисления смещения s.
            using (BinaryReader reader = new BinaryReader(File.Open(Name, FileMode.Open)))
            {
                int s = (Convert.ToInt32(IndexY) * ColX + Convert.ToInt32(IndexX)) * 8; // формула для вычисления смещения s
                reader.BaseStream.Position = s;
                if (reader.BaseStream.Position != reader.BaseStream.Length) //Если указатель не вышел за пределы файла
                {
                    Isk = reader.ReadDouble();
                    listBox1.Items.Add(Isk);
                    Console.WriteLine("Найденное значение", Isk);
                }
            }
            return Isk;
        }
        public void WriteToIni(string filePath, double X0, double Xk, int Nx, int Ny)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(X0);
                writer.Write(Xk);
                writer.Write(Nx);
                writer.Write(Ny);
            }
        }
        public void Culc ()
        {
            int i = 0;
            foreach (TabPage page in tabControl1.TabPages)
            {
                List<string> tabValues = new List<string>();

                foreach (Control control in page.Controls)
                {
                    if (control is System.Windows.Forms.TextBox textBox)
                    {
                        tabValues.Add(textBox.Text);
                    }
                }
                tabDataList.Add(tabValues);
                i++;
            }
            foreach (List<string> tabValues in tabDataList)
            {
                double X0 = double.Parse(tabValues[0]);
                //Console.WriteLine(X0);
                double Xk = double.Parse(tabValues[1]);
                //Console.WriteLine(Xk);
                int Nx = int.Parse(tabValues[2]);
                //Console.WriteLine(Nx);

                double[] aX = new double[Nx];
                for (int q = 0; q < Nx; q++)
                {
                    aX[q] = X0 + q * (Xk - X0) / (Nx - 1);
                }
                MatX X = new MatX(Nx, aX);

                int Ny = int.Parse(tabValues[3]);
                Random random = new Random();
                double[] aY = new double[Ny];
                for (int j = 0; j < Ny; j++)
                {
                    aY[j] = random.Next(-1000, 1000);
                }
                MatY Y = new MatY(Ny, aY);

                string filePath = $"Calc{i}.ini";
                WriteToIni(filePath, X0, Xk, Nx, Ny);

                Calculate();
                Write(i);
            }
        }
        private void buttonToRezFile_Click(object sender, EventArgs e)
        {
            Culc();
        }

        private void buttonFromRezFile_Click(object sender, EventArgs e)
        {
            if (!Int32.TryParse(textBox2.Text, out int value2) || !Int32.TryParse(textBox3.Text, out int value3) || !Int32.TryParse(textBox4.Text, out int value4))
            {
                MessageBox.Show("Введите только числа");
                return; // Прерываем выполнение кода
            }

            if (value2 < 0 || value3 < 0 || value4 < 0)
            {
                MessageBox.Show("Введите положительные значения");
                return; // Прерываем выполнение кода
            }

            if (value2 <= tabControl1.TabPages.Count)
            {
                double Isk = Read(textBox2.Text, textBox3.Text, textBox4.Text);
                Console.WriteLine(Isk);
            }
            else
            {
                MessageBox.Show("Введите номер существующего набора");
                textBox2.Text = "";
            }
        
        }
    }
}