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
using Autofac;
using DustInTheWind.InflationCalculator.Cli.Application.Calculate;
using MediatR;

namespace DustInTheWind.InflationCalculator.Cli.Bootstrapper
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IContainer container = Setup.ConfigureServices();

            IMediator mediator = container.Resolve<IMediator>();

            CalculateRequest request = new()
            {
                InputTime = "2010",
                InputValue = 100,
                OutputTime = "2021"
            };
            CalculateResponse response = await mediator.Send(request);

            Console.WriteLine(response.OutputValue);
        }
    }
}