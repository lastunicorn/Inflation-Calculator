// Inflation Calculator
// Copyright (C) 2022 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Globalization;
using System.Windows;
using Autofac;
using DustInTheWind.InflationCalculator.Wpf3.Presentation.ViewModels;
using DustInTheWind.InflationCalculator.Wpf3.Presentation.Views;

namespace DustInTheWind.InflationCalculator.Wpf3.Bootstrapper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ro-RO");
            CultureInfo.CurrentUICulture = new CultureInfo("ro-RO");

            IContainer container = Setup.ConfigureServices();

            MainWindow window = container.Resolve<MainWindow>();
            window.DataContext = container.Resolve<MainViewModel>();

            MainWindow = window;

            window.Show();

            base.OnStartup(e);
        }
    }
}