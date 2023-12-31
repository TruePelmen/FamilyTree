﻿namespace FamilyTree.WPF
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.BLL.Services;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Legends;
    using OxyPlot.Series;
    using OxyPlot.Wpf;

    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private readonly ITreePersonService treePersonService;
        private readonly IPersonService personService;

        public Statistics(ITreePersonService treePersonService, IPersonService personService)
        {
            this.InitializeComponent();

            // Ініціалізація сервісів
            this.treePersonService = treePersonService;
            this.personService = personService;

            // Ініціалізуємо модель графіка
            this.plotView.Model = new PlotModel();
        }

        public int TreeID { get; set; } = 12;

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void BtnMinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GenerateStatistics_Click(object sender, RoutedEventArgs e)
        {
            // Очищення попередніх даних на графіку
            this.plotView.Model.Series.Clear();

            bool includeBirth = this.birthCheckBox.IsChecked ?? false;
            bool includeDeath = this.deathCheckBox.IsChecked ?? false;

            var statistics = this.GetYearStatistics(this.TreeID, includeBirth, includeDeath);

            // Оновлення графіку за допомогою даних статистики
            var series = new StemSeries
            {
                Title = "Birth",
                MarkerType = MarkerType.Circle,
            };

            foreach (var entry in statistics)
            {
                series.Points.Add(new DataPoint(entry.Year, entry.BirthCount));
            }

            if (includeDeath)
            {
                var deathSeries = new StemSeries
                {
                    Title = "Death",
                    MarkerType = MarkerType.Circle,
                    Color = OxyColor.FromRgb(250, 250, 0),
                };
                foreach (var entry in statistics)
                {
                    deathSeries.Points.Add(new DataPoint(entry.Year, entry.DeathCount));
                }

                this.plotView.Model.Series.Add(deathSeries);
            }

            this.plotView.Model.Series.Add(series);
            this.plotView.InvalidatePlot(true);
        }

        private List<YearStatistics> GetYearStatistics(int treeId, bool includeBirth, bool includeDeath)
        {
            var people = this.treePersonService.GetTreePeopleByTreeId(treeId);

            var statistics = new List<YearStatistics>();

            foreach (var person in people)
            {
                if (includeBirth && person.BirthDate.HasValue)
                {
                    this.AddOrUpdateStatistics(statistics, person.BirthDate.Value.Year, "Birth");
                }

                if (includeDeath && person.DeathDate.HasValue)
                {
                    this.AddOrUpdateStatistics(statistics, person.DeathDate.Value.Year, "Death");
                }
            }

            return statistics;
        }

        private void AddOrUpdateStatistics(List<YearStatistics> statistics, int year, string eventType)
        {
            var existing = statistics.FirstOrDefault(s => s.Year == year);

            if (existing != null)
            {
                if (eventType == "Birth")
                {
                    existing.BirthCount++;
                }
                else if (eventType == "Death")
                {
                    existing.DeathCount++;
                }
            }
            else
            {
                var newStatistics = new YearStatistics
                {
                    Year = year,
                    BirthCount = eventType == "Birth" ? 1 : 0,
                    DeathCount = eventType == "Death" ? 1 : 0,
                };

                statistics.Add(newStatistics);
            }
        }
    }
}