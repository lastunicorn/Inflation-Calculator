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
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Wpf3.Application;
using DustInTheWind.InflationCalculator.Wpf3.Application.SetOutputTime;
using DustInTheWind.InflationCalculator.Wpf3.Infrastructure;
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf3.Presentation.ViewModels
{
    public class OutputTimeViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        private List<string> availableOutputTimes;
        private string outputTime;

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

                if (!IsInitializationMode)
                    _ = SetOutputTime();
            }
        }

        public OutputTimeViewModel(IMediator mediator, EventBus eventBus)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            eventBus.Subscribe<ApplicationInitializedEvent>(HandleApplicationInitialized);
        }

        private Task HandleApplicationInitialized(ApplicationInitializedEvent ev, CancellationToken cancellationToken)
        {
            RunInitializeMode(() =>
            {
                AvailableOutputTimes = ev.AvailableOutputTimes;
                OutputTime = ev.OutputTime;
            });

            return Task.CompletedTask;
        }

        private async Task SetOutputTime()
        {
            SetOutputTimeRequest request = new()
            {
                OutputTime = OutputTime
            };

            await mediator.Send(request);
        }
    }
}