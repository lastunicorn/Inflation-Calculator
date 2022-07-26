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
using System.Threading.Tasks;
using DustInTheWind.InflationCalculator.Web.Presentation.Models;
using DustInTheWind.InflationCalculator.WebService.Application.Calculate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DustInTheWind.InflationCalculator.Web.Presentation.Controllers
{
    /// <summary>
    /// Perform calculations on amounts of money based on inflation.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IMediator mediator;

        public CalculatorController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Calculates the equivalent amount of money in different times taken into account the inflation.
        /// </summary>
        /// <param name="requestDto">The request containing the input value and time and the output time, the one for which to calculate the equivalent amount.</param>
        /// <returns>The equivalent amount of money for the specified value and time.</returns>
        [HttpPost]
        public async Task<float> Calculate(CalculateRequestDto requestDto)
        {
            CalculateRequest calculateRequest = new()
            {
                InputTime = requestDto.InputTime,
                InputValue = requestDto.InputValue,
                OutputTime = requestDto.OutputTime
            };

            CalculateResponse response = await mediator.Send(calculateRequest);

            return response.OutputValue;
        }
    }
}