﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candy.Framework.Configuration;
using Candy.Framework.Infrastructure.DependencyManagement;

namespace Candy.Framework.Infrastructure
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get;}
        void Initialize(CandyConfig config);
        void RunBeginRequestTasks();
        T Resolve<T>() where T : class;
        object Resolve(Type type);
        T[] ResolveAll<T>();
    }
}
