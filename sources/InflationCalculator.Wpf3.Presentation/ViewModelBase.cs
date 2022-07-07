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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DustInTheWind.InflationCalculator.Wpf3.Presentation
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected bool IsInitializationMode { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RunInitializeMode(Action action)
        {
            IsInitializationMode = true;

            try
            {
                action();
            }
            finally
            {
                IsInitializationMode = false;
            }
        }

        protected async Task RunInitializeMode(Func<Task> action)
        {
            IsInitializationMode = true;

            try
            {
                await action();
            }
            finally
            {
                IsInitializationMode = false;
            }
        }
    }
}