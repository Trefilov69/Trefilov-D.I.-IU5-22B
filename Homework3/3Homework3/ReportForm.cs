using Microsoft.EntityFrameworkCore;

namespace _3Homework3
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            Text = "Отчёт";
            Width = 1000;
            Height = 700;
            Load += ReportForm_Load;
        }

        private void ReportForm_Load(object? sender, EventArgs e)
        {
            using var context = new AppDbContext();

            var tabs = new TabControl();
            tabs.Dock = DockStyle.Fill;

            var report1 = context.Animals
                .Include(a => a.Zoo)
                .OrderBy(a => a.Name)
                .Select(a => new
                {
                    Название = a.Name,
                    Зоопарк = a.Zoo!.Name,
                    Масса_кг = a.WeightKg
                })
                .ToList();

            var report2 = context.Animals
                .GroupBy(a => a.Zoo!.Name)
                .Select(g => new
                {
                    Зоопарк = g.Key,
                    Количество = g.Count()
                })
                .OrderBy(r => r.Зоопарк)
                .ToList();

            var report3 = context.Animals
                .GroupBy(a => a.Zoo!.Name)
                .Select(g => new
                {
                    Зоопарк = g.Key,
                    Средняя_масса = g.Average(a => a.WeightKg)
                })
                .OrderByDescending(r => r.Средняя_масса)
                .ToList();

            AddTab(tabs, "Список животных", report1);
            AddTab(tabs, "Количество", report2);
            AddTab(tabs, "Средняя масса", report3);

            Controls.Add(tabs);
        }

        private void AddTab(TabControl tabs, string title, object data)
        {
            var page = new TabPage(title);

            var grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.DataSource = data;

            page.Controls.Add(grid);
            tabs.TabPages.Add(page);
        }
    }
}