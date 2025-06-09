using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LR7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.StartPosition = FormStartPosition.Manual;
            newForm.Location = new System.Drawing.Point(this.Location.X + 30, this.Location.Y + 30);
            newForm.Show();
            newForm.Activate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (Application.OpenForms.Count == 0)
            {
                Application.Exit();
            }
        }


        //Друге
        private Bitmap originalImage;
        private Bitmap rotatedImage;
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openDlg.FileName);
                pictureBox1.Image = originalImage;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (rotatedImage == null)
            {
                MessageBox.Show("Немає зображення для збереження.");
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                rotatedImage.Save(saveDlg.FileName);
                MessageBox.Show("Зображення збережено.");
            }
        }
        private Bitmap RotateImage45(Bitmap image)
        {
            float angle = 45f;
            float radians = angle * (float)Math.PI / 180f;

            int w = image.Width;
            int h = image.Height;

            double cos = Math.Abs(Math.Cos(radians));
            double sin = Math.Abs(Math.Sin(radians));
            int newWidth = (int)(w * cos + h * sin);
            int newHeight = (int)(w * sin + h * cos);

            Bitmap rotatedBmp = new Bitmap(newWidth, newHeight);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                g.Clear(Color.Transparent);
                g.TranslateTransform(newWidth / 2f, newHeight / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-w / 2f, -h / 2f);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawImage(image, new Point(0, 0));
            }

            return rotatedBmp;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Спочатку відкрийте зображення.");
                return;
            }

            rotatedImage = RotateImage45(originalImage);
            pictureBox1.Image = rotatedImage;
        }

        //Третє
        private Figure[] figures;
        private Random rand = new Random();
        private Bitmap bmp;
        private void button6_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox2.Text);
            figures = new Figure[n];

            bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g = Graphics.FromImage(bmp);

            for (int i = 0; i < n; i++)
            {
                int x = rand.Next(20, pictureBox2.Width - 40);
                int y = rand.Next(20, pictureBox2.Height - 40);
                Color color = Color.FromName(comboBox3.SelectedItem.ToString());
                string text = textBox3.Text;

                switch (comboBox2.SelectedItem.ToString())
                {
                    case "Square":
                        figures[i] = new Square(x, y, color, text, int.Parse(textBox4.Text));
                        break;
                    case "Triangle":
                        figures[i] = new Triangle(x, y, color, text, int.Parse(textBox5.Text));
                        break;
                    case "Circle":
                        figures[i] = new Circle(x, y, color, text, int.Parse(textBox6.Text));
                        break;
                }
                figures[i].Draw(g);
            }
            pictureBox2.Image = bmp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox2.Text);
            figures = new Figure[n];

            if (bmp == null)
                bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            Graphics g = Graphics.FromImage(bmp);

            for (int i = 0; i < n; i++)
            {
                int x = rand.Next(20, pictureBox2.Width - 40);
                int y = rand.Next(20, pictureBox2.Height - 40);
                Color color = Color.FromName(comboBox3.SelectedItem.ToString());
                string text = textBox3.Text;

                switch (comboBox2.SelectedItem.ToString())
                {
                    case "Square":
                        figures[i] = new Square(x, y, color, text, int.Parse(textBox4.Text));
                        break;
                    case "Triangle":
                        figures[i] = new Triangle(x, y, color, text, int.Parse(textBox5.Text));
                        break;
                    case "Circle":
                        figures[i] = new Circle(x, y, color, text, int.Parse(textBox6.Text));
                        break;
                }
                figures[i].Draw(g);
            }
            pictureBox2.Image = bmp;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            bmp = null;

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
    }
}
