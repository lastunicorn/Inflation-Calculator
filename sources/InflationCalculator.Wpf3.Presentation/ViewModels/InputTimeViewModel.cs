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
using DustInTheWind.InflationCalculator.Wpf3.Application.SetInputTime;
using DustInTheWind.InflationCalculator.Wpf3.Infrastructure;
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf3.Presentation.ViewModels
{
    public class InputTimeViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        private List<string> availableInputTimes;
        private string inputTime;

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

                if (!IsInitializationMode)
                    _ = SetInputTime();
            }
        }

        public InputTimeViewModel(IMediator mediator, EventBus eventBus)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            eventBus.Subscribe<ApplicationInitializedEvent>(HandleApplicationInitialized);
        }

        private Task HandleApplicationInitialized(ApplicationInitializedEvent ev, CancellationToken cancellationToken)
        {
            RunInitializeMode(() =>
            {
                AvailableInputTimes = ev.AvailableInputTimes;
                InputTime = ev.InputTime;
            });

            return Task.CompletedTask;
        }

        private async Task SetInputTime()
        {
            SetInputTimeRequest request = new()
            {
                InputTime = InputTime
            };

            await mediator.Send(request);
        }
    }
}