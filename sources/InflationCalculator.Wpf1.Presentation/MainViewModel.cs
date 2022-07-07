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
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Wpf1.Application.Calculate;
using DustInTheWind.InflationCalculator.Wpf1.Application.Initialize;
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf1.Presentation
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private volatile bool isInitializationMode;

        private float inputValue;
        private List<string> availableInputTimes;
        private float outputValue;
        private List<string> availableOutputTimes;
        private string inputTime;
        private string outputTime;
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

        public float InputValue
        {
            get => inputValue;
            set
            {
                inputValue = value;
                OnPropertyChanged();

                if (!isInitializationMode)
                    _ = SetInputValue();
            }
        }

        public List<string> AvailableInputTimes
        {
            get => availableInputTimes;
            private set
            {
                availableInputTimes = value;
                OnPropertyChanged();
            }
        }

        public string InputTime
        {
            get => inputTime;
            set
            {
                inputTime = value;
                OnPropertyChanged();

                if (!isInitializationMode)
                    _ = SetInputTime();
            }
        }

        public float OutputValue
        {
            get => outputValue;
            set
            {
                outputValue = value;
                OnPropertyChanged();
            }
        }

        public List<string> AvailableOutputTimes
        {
            get => availableOutputTimes;
            private set
            {
                availableOutputTimes = value;
                OnPropertyChanged();
            }
        }

        public string OutputTime
        {
            get => outputTime;
            set
            {
                outputTime = value;
                OnPropertyChanged();

                if (!isInitializationMode)
                    _ = SetOutputTime();
            }
        }

        public MainViewModel(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _ = Initialize();
        }

        private async Task Initialize()
        {
            isInitializationMode = true;

            try
            {
                Title = CalculateApplicationTitle();

                InitializeRequest request = new();
                InitializeResponse response = await mediator.Send(request);

                AvailableInputTimes = response.AvailableInputTimes;
                InputTime = response.InputTime;
                InputValue = response.InputValue;

                AvailableOutputTimes = response.AvailableOutputTimes;
                OutputTime = response.OutputTime;
                OutputValue = response.OutputValue;
            }
            finally
            {
                isInitializationMode = false;
            }
        }

        private static string CalculateApplicationTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;

            return $"Inflation Calculator (State in GUI) {version}";
        }

        private async Task SetInputValue()
        {
            await Recalculate();
        }

        private async Task SetInputTime()
        {
            await Recalculate();
        }

        private async Task SetOutputTime()
        {
            await Recalculate();
        }

        private async Task Recalculate()
        {
            CalculateRequest request = new()
            {
                InputValue = InputValue,
                InputTime = InputTime,
                OutputTime = OutputTime
            };
            CalculateResponse response = await mediator.Send(request);

            OutputValue = response.OutputValue;
        }
    }
}