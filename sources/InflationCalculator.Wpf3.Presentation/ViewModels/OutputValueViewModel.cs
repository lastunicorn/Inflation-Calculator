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
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Wpf3.Application;
using DustInTheWind.InflationCalculator.Wpf3.Infrastructure;

namespace DustInTheWind.InflationCalculator.Wpf3.Presentation.ViewModels
{
    public class OutputValueViewModel : ViewModelBase
    {
        private float outputValue;

        public float OutputValue
        {
            get => outputValue;
            set
            {
                outputValue = value;
                OnPropertyChanged();
            }
        }

        public OutputValueViewModel(EventBus eventBus)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            eventBus.Subscribe<ApplicationInitializedEvent>(HandleApplicationInitialized);
            eventBus.Subscribe<OutputValueChangedEvent>(HandleOutputValueChanged);
        }

        private Task HandleApplicationInitialized(ApplicationInitializedEvent ev, CancellationToken cancellationToken)
        {
            RunInitializeMode(() =>
            {
                OutputValue = ev.OutputValue;
            });

            return Task.CompletedTask;
        }

        private Task HandleOutputValueChanged(OutputValueChangedEvent ev, CancellationToken cancellationToken)
        {
            RunInitializeMode(() =>
            {
                OutputValue = ev.NewValue;
            });

            return Task.CompletedTask;
        }
    }
}