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

namespace DustInTheWind.InflationCalculator.Wpf3.Application.SetInputValue
{
    public class SetInputValueUseCase : AsyncRequestHandler<SetInputValueRequest>
    {
        private readonly Calculator calculator;
        private readonly EventBus eventBus;

        public SetInputValueUseCase(Calculator calculator, EventBus eventBus)
        {
            this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        protected override async Task<Unit> Handle(SetInputValueRequest request, CancellationToken cancellationToken)
        {
            calculator.InputValue = request.InputValue;

            InputTimeChangedEvent inputTimeChangedEvent = new();
            await eventBus.Publish(inputTimeChangedEvent, cancellationToken);

            return Unit.Value;
        }
    }
}