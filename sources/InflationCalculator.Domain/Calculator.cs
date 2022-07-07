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

namespace DustInTheWind.InflationCalculator.Domain
{
    public class Calculator
    {
        private List<Inflation> inflations;

        public float InputValue { get; set; } = 100;

        public string InputTime { get; set; }

        public string OutputTime { get; set; }

        public List<Inflation> Inflations
        {
            get => inflations;
            set
            {
                inflations = value;

                if (inflations == null || inflations.Count == 0)
                {
                    InputTime = null;
                    OutputTime = null;
                }
                //else
                //{
                //    InputTime = value.Count > 2
                //        ? value[^2]?.Time
                //        : value[^1]?.Time;
                //    OutputTime = value[^1]?.Time;
                //}
            }
        }

        public IEnumerable<string> AvailableTimes
        {
            get
            {
                if (inflations == null)
                    return Enumerable.Empty<string>();

                return inflations
                    .Select(x => x.Time)
                    .ToList();
            }
        }

        public float Calculate()
        {
            int inputIndex = -1;
            int outputIndex = -1;

            for (int i = 0; i < inflations.Count; i++)
            {
                if (inflations[i].Time == InputTime)
                    inputIndex = i;

                if (inflations[i].Time == OutputTime)
                    outputIndex = i;

                if (inputIndex >= 0 && outputIndex >= 0)
                    break;
            }

            if (inputIndex < 0)
                throw new Exception("Input time does not exist in the list.");

            if (outputIndex < 0)
                throw new Exception("Output time does not exist in the list.");

            if (inputIndex == outputIndex)
                return InputValue;

            if (inputIndex < outputIndex)
            {
                float value = InputValue;

                for (int i = inputIndex + 1; i <= outputIndex; i++)
                {
                    float inflationPercentage = inflations[i].Value;
                    float inflationValue = 1 + inflationPercentage / 100;
                    value *= inflationValue;
                }

                return value;
            }

            if (inputIndex > outputIndex)
            {
                float value = InputValue;

                for (int i = inputIndex; i > outputIndex; i--)
                {
                    float inflationPercentage = inflations[i].Value;
                    float inflationValue = 1 + inflationPercentage / 100;
                    value /= inflationValue;
                }

                return value;
            }

            return 0;
        }
    }
}