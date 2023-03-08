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
using System.Threading.Tasks;
using Autofac;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Commando;

namespace DustInTheWind.InflationCalculator.Cli.Bootstrapper
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IContainer container = Setup.ConfigureServices();

            await using ILifetimeScope lifetimeScope = container.BeginLifetimeScope();
            await ProcessRequest(args, lifetimeScope);
        }

        private static async Task ProcessRequest(string[] args, IComponentContext context)
        {
            CommandRouter commandRouter = context.Resolve<CommandRouter>();
            commandRouter.CommandCreated += HandleCommandCreated;

            Arguments arguments = new(args);
            await commandRouter.Execute(arguments);
        }

        private static void HandleCommandCreated(object sender, CommandCreatedEventArgs e)
        {
            if (e.UnusedArguments.Count > 0)
            {

                IEnumerable<string> unusedArguments = e.UnusedArguments
                    .Select(x => x.Name ?? x.Value);

                foreach (string unusedArgument in unusedArguments)
                    CustomConsole.WriteLine(ConsoleColor.DarkYellow, $"Unknown argument: {unusedArgument}");
            }
        }
    }
}