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

using System.Collections.Generic;

namespace DustInTheWind.InflationCalculator.Wpf2.Application.Initialize
{
    public class InitializeResponse
    {
        public List<string> AvailableInputTimes { get; set; }
        
        public List<string> AvailableOutputTimes { get; set; }
        
        public float InputValue { get; set; }
        
        public float OutputValue { get; set; }
        
        public string InputTime { get; set; }
        
        public string OutputTime { get; set; }
    }
}