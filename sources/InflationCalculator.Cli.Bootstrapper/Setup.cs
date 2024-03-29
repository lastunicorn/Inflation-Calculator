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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using DustInTheWind.ConsoleTools.Commando.Autofac.DependencyInjection;
using DustInTheWind.InflationCalculator.Cli.Application.Calculate;
using DustInTheWind.InflationCalculator.Cli.Presentation;
using DustInTheWind.InflationCalculator.DataAccess;
using DustInTheWind.InflationCalculator.DataAccess.Data;
using DustInTheWind.InflationCalculator.Domain;
using DustInTheWind.InflationCalculator.Domain.DataAccess;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace DustInTheWind.InflationCalculator.Cli.Bootstrapper
{
    public class Setup
    {
        public static IContainer ConfigureServices()
        {
            ContainerBuilder containerBuilder = new();

            containerBuilder.RegisterType<YearlyDataContext>().As<DataContext>().SingleInstance();
            containerBuilder.RegisterType<InflationRepository>().As<IInflationRepository>();

            containerBuilder
                .Register(context =>
                {
                    IInflationRepository inflationRepository = context.Resolve<IInflationRepository>();

                    List<Inflation> inflations = inflationRepository.GetAll()
                        .ToList();

                    return new Calculator
                    {
                        Inflations = inflations
                    };
                })
                .AsSelf();

            Assembly applicationAssembly = typeof(CalculateRequest).Assembly;
            containerBuilder.RegisterMediatR(applicationAssembly);

            Assembly presentationAssembly = typeof(CalculateCommand).Assembly;
            containerBuilder.RegisterCommando(presentationAssembly);

            return containerBuilder.Build();
        }
    }
}