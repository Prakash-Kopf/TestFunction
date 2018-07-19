﻿////using System;
////using System.Threading.Tasks;
////using Fclp;
////using Azure.Functions.Cli.Interfaces;
////using Microsoft.Azure.WebJobs;
////using DurableTask.Core;
////using Colors.Net;
////using static Azure.Functions.Cli.Common.OutputTheme;
////using static Colors.Net.StringStaticMethods;


////namespace Azure.Functions.Cli.Actions.DurableActions
////{
////    // The name is the [action] part on the command line
////    // Context and SubContext are also defined here if you need them.
////    // You can also alias commands by having multiple [Action] attributes
////    // For example if you want to also have `func durable show` to be an alias for this command
////    // you can just add
////    // [Action(Name = "show", Context = Context.Durable, HelpText = "")]
////    [Action(Name = "Rewind", Context = Context.Durable, HelpText = "Rewinds indicated instance")]
////    class DurableRewind : BaseAction
////    {
////        // I usually have actions define their properties public like this
////        // That way actions can instantiate and run each others if needed
////        // Some actions already do that, like extensions install, calling extensions sync after words
////        public string Version { get; set; }

////        public string Instance { get; set; }

////        public string Reason { get; set; }


////        private readonly ISecretsManager _secretsManager;
////        private readonly DurableOrchestrationClient _client;
////        //public readonly IOrchestrationServiceClient serviceClient;

////        // The constructor supports DI for objects defined here https://github.com/Azure/azure-functions-core-tools/blob/master/src/Azure.Functions.Cli/Program.cs#L44
////        public DurableRewind(ISecretsManager secretsManager, DurableOrchestrationClient client)
////        {
////            _secretsManager = secretsManager;
////            _client = client;
////        }


////        public override ICommandLineParserResult ParseArgs(string[] args)
////        {
////            var parser = new FluentCommandLineParser();
////            parser
////                .Setup<string>("version")
////                .WithDescription("This shows up in the help next to the version option.")
////                .SetDefault(string.Empty)
////                // This is a call back with the value you can use it however you like
////                .Callback(v => Version = v);

////            parser
////                .Setup<string>("instance")
////                .WithDescription("This specifies an orchestration by instanceId")
////                .SetDefault(string.Empty)
////                .Callback(i => Instance = i);

////            parser
////                .Setup<string>("reason")
////                .WithDescription("This is the reason for rewinding")
////                .SetDefault(string.Empty)
////                .Callback(r => Reason = r);

////            return parser.Parse(args);
////        }

////        public override async Task RunAsync()
////        {
////            // HACK: checking that instance exists, status checked later in RewindAsync
////            //await _client.RewindAsync(Instance, Reason);
////            //var status = await _client.GetStatusAsync(Instance, false, false);
////            //if (status.RuntimeStatus.ToString() == "Completed" || status.RuntimeStatus.ToString() == "Running")
////            //{
////            //    ColoredConsole.WriteLine(Yellow($"Instance {Instance} rewound."));
////            //}
////            //else
////            //{
////            //    ColoredConsole.Error.WriteLine(ErrorColor($"Could not rewind function with instanceId {Instance}"));
////            //}
////            ColoredConsole.Error.WriteLine(ErrorColor("Durable Functions does not have rewind implemented."));
////        }
////    }
////}