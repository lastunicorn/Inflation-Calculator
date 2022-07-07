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
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Wpf3.Application;
using DustInTheWind.InflationCalculator.Wpf3.Application.Calculate;
using DustInTheWind.InflationCalculator.Wpf3.Application.Initialize;
using DustInTheWind.InflationCalculator.Wpf3.Application.SetInputTime;
using DustInTheWind.InflationCalculator.Wpf3.Application.SetInputValue;
using DustInTheWind.InflationCalculator.Wpf3.Application.SetOutputTime;
using DustInTheWind.InflationCalculator.Wpf3.Infrastructure;
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf3.Presentation
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private readonly EventBus eventBus;
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

        public MainViewModel(IMediator mediator, EventBus eventBus)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            eventBus.Subscribe<InputTimeChangedEvent>(HandleInputTimeChanged);
            eventBus.Subscribe<InputValueChangedEvent>(HandleInputValueChanged);
            eventBus.Subscribe<OutputTimeChangedEvent>(HandleOutputTimeChanged);

            _ = Initialize();
        }

        private async Task Initialize()
        {
            await RunInitializeMode(async () =>
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
            });
        }

        private void RunInitializeMode(Action action)
        {
            isInitializationMode = true;

            try
            {
                action();
            }
            finally
            {
                isInitializationMode = false;
            }
        }

        private async Task RunInitializeMode(Func<Task> action)
        {
            isInitializationMode = true;

            try
            {
                await action();
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

            return $"Inflation Calculator {version}";
        }

        private async Task SetInputValue()
        {
            SetInputValueRequest request = new()
            {
                InputValue = InputValue
            };

            await mediator.Send(request);
        }

        private async Task SetInputTime()
        {
            SetInputTimeRequest request = new()
            {
                InputTime = InputTime
            };

            await mediator.Send(request);
        }

        private async Task SetOutputTime()
        {
            SetOutputTimeRequest request = new()
            {
                OutputTime = OutputTime
            };

            await mediator.Send(request);
        }

        private async Task HandleInputTimeChanged(InputTimeChangedEvent ev, CancellationToken cancellationToken)
        {
            await RecalculateOutputValue(cancellationToken);
        }

        private async Task HandleInputValueChanged(InputValueChangedEvent ev, CancellationToken cancellationToken)
        {
            await RecalculateOutputValue(cancellationToken);
        }

        private async Task HandleOutputTimeChanged(OutputTimeChangedEvent ev, CancellationToken cancellationToken)
        {
            await RecalculateOutputValue(cancellationToken);
        }

        private async Task RecalculateOutputValue(CancellationToken cancellationToken)
        {
            CalculateRequest request = new();

            CalculateResponse response = await mediator.Send(request, cancellationToken);
            OutputValue = response.OutputValue;
        }
    }
}