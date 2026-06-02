namespace _3Homework3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            new ZooForm().ShowDialog();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            new AnimalForm().ShowDialog();
        }

        private void button3_Click(object? sender, EventArgs e)
        {
            new ReportForm().ShowDialog();
        }

        private void button4_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}