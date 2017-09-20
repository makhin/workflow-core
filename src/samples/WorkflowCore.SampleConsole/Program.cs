using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace WorkflowCore.SampleConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            //start the workflow host
            var host = serviceProvider.GetService<IWorkflowHost>();
            var reg = serviceProvider.GetService<IWorkflowRegistry>();
            WorkflowDefinition def = GetWorkflowDefinition();

            reg.RegisterWorkflow(def);

            host.Start();
            host.StartWorkflow("Test", 1, null);
            Console.ReadLine();
            host.Stop();
        }

        private static WorkflowDefinition GetWorkflowDefinition()
        {
            var result = new WorkflowDefinition
            {
                Id = "Test",
                Version = 1,
                Steps = new List<WorkflowStep>()
            };

            result.Steps.Add(new WorkflowStepInline()
            {
                Body = context =>
                {
                    Console.WriteLine("Hello");
                    return ExecutionResult.Next();
                }
            });

            return result;
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();
            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddDebug();
            return serviceProvider;
        }
    }
}
