using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Diagnostics;
using System.IO;

namespace Odin.MSBuild
{
    public class Odin : ToolTask
    {
        public Odin()
        {
            var odinRoot = Environment.GetEnvironmentVariable("ODIN_ROOT");
            if (!string.IsNullOrEmpty(odinRoot))
            {
                ToolPath = odinRoot;
            }
        }

        [Required]
        public string Command { get; set; }

        [Required]
        public string InputFile { get; set; }

        public string ExeOutputPath { get; set; }

        protected override string ToolName => "odin.exe";

        protected override MessageImportance StandardOutputLoggingImportance => MessageImportance.High;

        protected override string GenerateFullPathToTool()
        {
            if (string.IsNullOrEmpty(ToolPath)) return ToolName;
            return Path.Combine(ToolPath, ToolName);
        }

        public override bool Execute()
        {
            Log.LogMessage("Executing odin ({0})", ToolPath);
            return base.Execute();
        }

        protected override string GenerateCommandLineCommands()
        {
            //return base.GenerateCommandLineCommands();
            var builder = new CommandLineBuilder();

            builder.AppendSwitch(Command);
            //builder.AppendSwitch(InputFile);
            builder.AppendFileNameIfNotNull(InputFile);
            builder.AppendSwitchIfNotNull("-out:", ExeOutputPath);

            var commandLineCommands = builder.ToString();
            //LogMessage("Arguments", commandLineCommands);
            return commandLineCommands;
        }

        protected override void LogToolCommand(string message)
        {
            Debug.WriteLine(">>" + message + "<< ");
            base.LogToolCommand(message);
        }
    }
}