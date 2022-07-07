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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Domain;
using DustInTheWind.InflationCalculator.Domain.DataAccess;
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf2.Application.Initialize
{
    internal class InitializeUseCase : IRequestHandler<InitializeRequest, InitializeResponse>
    {
        private readonly IInflationRepository inflationRepository;
        private readonly Calculator calculator;

        public InitializeUseCase(IInflationRepository inflationRepository, Calculator calculator)
        {
            this.inflationRepository = inflationRepository ?? throw new ArgumentNullException(nameof(inflationRepository));
            this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        public Task<InitializeResponse> Handle(InitializeRequest request, CancellationToken cancellationToken)
        {
            List<Inflation> inflations = inflationRepository.GetAll().ToList();

            calculator.Inflations = inflations;
            calculator.InputTime = inflations.Count > 2
                ? inflations[^2]?.Time
                : inflations[^1]?.Time;
            calculator.OutputTime = inflations[^1]?.Time;

            List<string> availableTimes = calculator.AvailableTimes.ToList();

            InitializeResponse response = new()
            {
                AvailableInputTimes = availableTimes,
                InputTime = calculator.InputTime,
                InputValue = calculator.InputValue,
                AvailableOutputTimes = availableTimes,
                OutputTime = calculator.OutputTime,
                OutputValue = calculator.Calculate()
            };

            return Task.FromResult(response);
        }
    }
}