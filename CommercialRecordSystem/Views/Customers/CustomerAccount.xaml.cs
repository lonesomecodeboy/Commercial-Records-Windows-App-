﻿using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels;
using Windows.UI.Xaml;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace CommercialRecordSystem.Views.Customers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerAccount : ViewBase
    {
        public CustomerAccount() : base(typeof(CustomerAccountFrameVM))
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        public class FinancialStuff
        {
            public string Name { get; set; }
            public int Amount { get; set; }
        }
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }

        private void LoadChartContents()
        {
            System.Random rand = new System.Random();
            System.Collections.Generic.List<FinancialStuff> financialStuffList = new System.Collections.Generic.List<FinancialStuff>();
            financialStuffList.Add(new FinancialStuff() { Name = "Ağustos", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Eylül", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Ekim", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Kasım", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Aralık", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "Ocak", Amount = rand.Next(0, 200) });
            (ColumnChart.Series[0] as ColumnSeries).ItemsSource = financialStuffList;
            (ColumnChart.Series[1] as ColumnSeries).ItemsSource = financialStuffList;
            (ColumnChart.Series[2] as LineSeries).ItemsSource = financialStuffList;
        }
        
        private void actionList_Tabbed(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CommercialRecordSystem.Views.Transacts.Sales));
        }
        /*
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int customerId = (int)e.Parameter;
            this.DataContext = new CustomerAccountFrameVM(this.Frame, customerId);
        }*/
    }
}
