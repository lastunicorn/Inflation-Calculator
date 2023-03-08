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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.InflationCalculator.DataAccess.CsvModel;
using DustInTheWind.InflationCalculator.Domain;
using Newtonsoft.Json;

namespace DustInTheWind.InflationCalculator.DataAccess.Data
{
    public class YearlyDataContext : DataContext
    {
        protected override List<Inflation> ReadInflationsFromFile()
        {
            List<Inflation> readInflationsFromFile = ReadAllLines()
                .Skip(1)
                .Select(x => new Inflation
                {
                    Time = x[0],
                    InflationRate = float.Parse(x[2], new CultureInfo("ro-RO"))
                })
                .ToList();

            string json = JsonConvert.SerializeObject(readInflationsFromFile, Formatting.Indented);

            return readInflationsFromFile;
        }

        private static IEnumerable<CsvRow> ReadAllLines()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("DustInTheWind.InflationCalculator.DataAccess.Data.inflation-yearly.csv");
            CsvDocument csvDocument = new(stream);

            return csvDocument.EnumerateDataLines();
        }
    }
}