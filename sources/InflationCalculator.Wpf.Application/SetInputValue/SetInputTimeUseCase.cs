﻿// Inflation Calculator
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
using MediatR;

namespace DustInTheWind.InflationCalculator.Wpf.Application.SetInputValue
{
    public class SetInputValueUseCase : IRequestHandler<SetInputValueRequest, SetInputValueResponse>
    {
        private readonly Calculator calculator;

        public SetInputValueUseCase(Calculator calculator)
        {
            this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        public Task<SetInputValueResponse> Handle(SetInputValueRequest request, CancellationToken cancellationToken)
        {
            calculator.InputValue = request.InputValue;

            SetInputValueResponse response = new()
            {
                OutputValue = calculator.Calculate()
            };

            return Task.FromResult(response);
        }
    }
}