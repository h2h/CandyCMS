using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

using Candy.Framework.Plugins;
using Candy.Framework.Localization;
using Candy.Framework.Configuration;
using Candy.Framework.Infrastructure.DependencyManagement;

namespace Candy.Framework.Infrastructure
{
    public class CandyEngine : IEngine
    {
        private ContainerManager _containerManager;
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        /// <summary>
        /// 运行启动任务
        /// </summary>
        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            
            //排序
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            
            // 执行所有任务
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }
        /// <summary>
        /// 运行请求开始任务
        /// </summary>
        public void RunBeginRequestTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var beginRequestTaskTypes = typeFinder.FindClassesOfType<IBeginRequestTask>();

            var beginReuqestTasks = new List<IBeginRequestTask>();
            foreach (var beginReuqestTaskType in beginRequestTaskTypes)
                beginReuqestTasks.Add((IBeginRequestTask)Activator.CreateInstance(beginReuqestTaskType));

            beginReuqestTasks = beginReuqestTasks.AsQueryable().OrderBy(s => s.Order).ToList();

            foreach (var task in beginReuqestTasks)
                task.Execute();
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="config">配置实体</param>
        protected virtual void RegisterDependencies(CandyConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //we create new instance of ContainerBuilder
            //because Build() or Update() method can only be called once on a ContainerBuilder.

            //dependencies
            var typeFinder = new WebAppTypeFinder(config);
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<CandyConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            //register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));

            //sort
            //drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);


            this._containerManager = new ContainerManager(container);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config">配置实体</param>
        public void Initialize(CandyConfig config)
        {
            //注册依赖
            RegisterDependencies(config);

            //运行任务
            if (!config.IgnoreStartupTasks)
                RunStartupTasks();
        }

        /// <summary>
        /// 解析依赖
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  解析依赖
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// 解析依赖
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}
