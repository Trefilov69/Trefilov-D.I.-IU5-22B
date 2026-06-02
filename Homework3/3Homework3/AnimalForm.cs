using Microsoft.EntityFrameworkCore;

namespace _3Homework3
{
    public partial class AnimalForm : Form
    {
        public AnimalForm()
        {
            InitializeComponent();
            Text = "Животные";
            Width = 900;
            Height = 500;
            Load += AnimalForm_Load;
        }

        private void AnimalForm_Load(object? sender, EventArgs e)
        {
            using var context = new AppDbContext();

            var animals = context.Animals
                .Include(a => a.Zoo)
                .OrderBy(a => a.Name)
                .Select(a => new
                {
                    a.Id,
                    Название = a.Name,
                    Зоопарк = a.Zoo!.Name,
                    Масса_кг = a.WeightKg
                })
                .ToList();

            DataGridView grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.DataSource = animals;

            Controls.Add(grid);
        }
    }
}