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

using System;
using System.Reflection;
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Wpf3.Application.Initialize;
using DustInTheWind.InflationCalculator.Wpf3.Infrastructure;
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf3.Presentation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        private string title;

        public string Title
        {
            get => title;
            private set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public InputValueViewModel InputValueViewModel { get; }

        public InputTimeViewModel InputTimeViewModel { get; }

        public OutputTimeViewModel OutputTimeViewModel { get; }

        public OutputValueViewModel OutputValueViewModel { get; }

        public MainViewModel(IMediator mediator, EventBus eventBus)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            InputValueViewModel = new InputValueViewModel(mediator, eventBus);
            InputTimeViewModel = new InputTimeViewModel(mediator, eventBus);
            OutputTimeViewModel = new OutputTimeViewModel(mediator, eventBus);
            OutputValueViewModel = new OutputValueViewModel(eventBus);

            _ = Initialize();
        }

        private async Task Initialize()
        {
            await RunInitializeMode(async () =>
            {
                Title = CalculateApplicationTitle();

                InitializeRequest request = new();
                await mediator.Send(request);
            });
        }

        private static string CalculateApplicationTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;

            return $"Inflation Calculator (With Event Bus) {version}";
        }
    }
}