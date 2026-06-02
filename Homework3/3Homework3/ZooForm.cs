using Microsoft.EntityFrameworkCore;

namespace _3Homework3
{
    public partial class ZooForm : Form
    {
        public ZooForm()
        {
            InitializeComponent();

            Text = "Зоопарки";

            Load += ZooForm_Load;
        }

        private void ZooForm_Load(object? sender, EventArgs e)
        {
            using var context = new AppDbContext();

            var zoos = context.Zoos
                .OrderBy(z => z.Name)
                .ToList();

            DataGridView grid = new DataGridView();

            grid.Dock = DockStyle.Fill;
            grid.DataSource = zoos;

            Controls.Add(grid);
        }
    }
}