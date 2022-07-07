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
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Cli.Application.Calculate;
using MediatR;

namespace DustInTheWind.InflationCalculator.Cli.Presentation
{
    public class CalculateCommand
    {
        private readonly IMediator mediator;
        private readonly CalculateView view;

        public CalculateCommand(IMediator mediator, CalculateView view)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public async Task Execute()
        {

            CalculateRequest request = new()
            {
                InputTime = "2020",
                InputValue = 100,
                OutputTime = "2021"
            };

            CalculateResponse response = await mediator.Send(request);

            view.OutputValue = response.OutputValue;
            view.Display();
        }
    }
}