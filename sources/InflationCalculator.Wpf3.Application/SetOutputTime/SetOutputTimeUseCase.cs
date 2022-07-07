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
using DustInTheWind.InflationCalculator.Domain;
using DustInTheWind.InflationCalculator.Wpf3.Infrastructure;
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf3.Application.SetOutputTime
{
    public class SetOutputTimeUseCase : AsyncRequestHandler<SetOutputTimeRequest>
    {
        private readonly Calculator calculator;
        private readonly EventBus eventBus;

        public SetOutputTimeUseCase(Calculator calculator, EventBus eventBus)
        {
            this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        protected override async Task<Unit> Handle(SetOutputTimeRequest request, CancellationToken cancellationToken)
        {
            calculator.OutputTime = request.OutputTime;
            
            float newOutputValue = calculator.Calculate();
            await RaiseOutputValueChangedEvent(newOutputValue, cancellationToken);

            return Unit.Value;
        }

        private async Task RaiseOutputValueChangedEvent(float newOutputValue, CancellationToken cancellationToken)
        {
            OutputValueChangedEvent outputValueChangedEvent = new()
            {
                NewValue = newOutputValue
            };
            await eventBus.Publish(outputValueChangedEvent, cancellationToken);
        }
    }
}