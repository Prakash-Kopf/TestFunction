﻿using System;
using System.Threading.Tasks;
using Fclp;
using Azure.Functions.Cli.Interfaces;
using Microsoft.Azure.WebJobs;
using DurableTask.Core;
using Colors.Net;
using static Azure.Functions.Cli.Common.OutputTheme;
using static Colors.Net.StringStaticMethods;
using Azure.Functions.Cli.Common;


namespace Azure.Functions.Cli.Actions.DurableActions
{
    // The name is the [action] part on the command line
    // Context and SubContext are also defined here if you need them.
    // You can also alias commands by having multiple [Action] attributes
    // For example if you want to also have `func durable show` to be an alias for this command
    // you can just add
    // [Action(Name = "show", Context = Context.Durable, HelpText = "")]
    [Action(Name = "runtime-status", Context = Context.Durable, HelpText = "Checks the status of a specified instance of an orchestrator function")]
    class DurableRuntimeStatus : BaseAction
    {
        // I usually have actions define their properties public like this
        // That way actions can instantiate and run each others if needed
        // Some actions already do that, like extensions install, calling extensions sync after words
        public string Version { get; set; }

        public string Instance { get; set; }

        public bool AllExecutions { get; set; }



        private readonly ISecretsManager _secretsManager;
        private readonly DurableManager durableManager;
        //private readonly DurableOrchestrationClientBase _client;
        //public readonly IOrchestrationServiceClient serviceClient;

        // The constructor supports DI for objects defined here https://github.com/Azure/azure-functions-core-tools/blob/master/src/Azure.Functions.Cli/Program.cs#L44
        //public DurableStartNew(DurableOrchestrationClientBase client)
        public DurableRuntimeStatus(ISecretsManager secretsManager)
        {
            _secretsManager = secretsManager;
            durableManager = new DurableManager(secretsManager);
            //_client = client;
        }


        public override ICommandLineParserResult ParseArgs(string[] args)
        {
            var parser = new FluentCommandLineParser();
            parser
                .Setup<string>("version")
                .WithDescription("This shows up in the help next to the version option.")
                .SetDefault(string.Empty)
                // This is a call back with the value you can use it however you like
                .Callback(v => Version = v);

            parser
                .Setup<string>("instance")
                .WithDescription("This specifies the instanceId of a new orchestration")
                .SetDefault(Guid.NewGuid().ToString("N"))
                .Callback(i => Instance = i);

            parser
                 .Setup<bool>("all-executions")
                 .WithDescription("This specifies the name of an event to raise")
                 .SetDefault(false)
                 .Callback(e => AllExecutions = e);


            return parser.Parse(args);
        }

        public override async Task RunAsync()
        {
            await durableManager.GetOrchestrationState(Instance, AllExecutions);
        }
    }
}