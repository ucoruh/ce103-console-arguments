using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace commandline_sample
{
    [Verb("add", HelpText = "Add file contents to the index.")]
    public class Add_Verb
    {
        [Option('p', "patch", SetName = "mode-p",
            HelpText = "Interactively choose hunks of patch between the index and the work tree and add them to the index.")]
        public bool Patch { get; set; }

        [Option('f', "force", SetName = "mode-f",
            HelpText = "Allow adding otherwise ignored files.")]
        public bool Force { get; set; }

        [Value(0)]
        public IEnumerable<string> FileNames { get; set; }
    }

    [Verb("add", HelpText = "Add file contents to the index.")]
    public class Add_Verb_With_Usage_Attribute
    {
        [Option('p', "patch", SetName = "mode-p",
            HelpText = "Interactively choose hunks of patch between the index and the work tree and add them to the index.")]
        public bool Patch { get; set; }

        [Option('f', "force", SetName = "mode-f",
            HelpText = "Allow adding otherwise ignored files.")]
        public bool Force { get; set; }

        [Value(0)]
        public IEnumerable<string> FileNames { get; set; }

        [Option("secert-option", Hidden = true, HelpText = "This is a description for a secert hidden option that should never be visibile to the user via help text.")]
        public string SecertOption { get; set; }

        [Usage(ApplicationAlias = "git")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Forcing file", new Add_Verb { FileNames = new string[]{ "README.md" }, Force = true });
            }
        }
    }

    [Verb("commit", HelpText = "Record changes to the repository.")]
    public class Commit_Verb_With_Usage_Attribute
    {
        [Option('p', "patch",
            HelpText = "Use the interactive patch selection interface to chose which changes to commit.")]
        public bool Patch { get; set; }

        [Option("amend", HelpText = "Used to amend the tip of the current branch.")]
        public bool Amend { get; set; }

        [Option('m', "message", HelpText = "Use the given message as the commit message.")]
        public string Message { get; set; }

        [Option("secert-option", Hidden = true, HelpText = "This is a description for a secert hidden option that should never be visibile to the user via help text.")]
        public string SecertOption { get; set; }

        [Usage(ApplicationAlias = "git")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Committing work", new Commit_Verb_With_Usage_Attribute { Patch = true });
            }
        }
    }

    [Verb("clone", HelpText = "Clone a repository into a new directory.")]
    public class Clone_Verb_With_Usage_Attribute
    {
        [Option("no-hardlinks",
            HelpText = "Optimize the cloning process from a repository on a local filesystem by copying files.")]
        public bool NoHardLinks { get; set; }

        [Option('q', "quiet",
            HelpText = "Suppress summary message.")]
        public bool Quiet { get; set; }

        [Option("secert-option", Hidden = true, HelpText = "This is a description for a secert hidden option that should never be visibile to the user via help text.")]
        public string SecertOption { get; set; }

        [Value(0, MetaName = "URLS",
            HelpText = "A list of url(s) to clone.")]
        public IEnumerable<string> Urls { get; set; }

        [Usage(ApplicationAlias = "git")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Basic cloning", new Clone_Verb_With_Usage_Attribute { Urls = new[] { "https://github.com/gsscoder/csharpx" } });
                yield return new Example("Cloning quietly", new Clone_Verb_With_Usage_Attribute { Quiet = true, Urls = new[] { "https://github.com/gsscoder/railwaysharp" } });
                yield return new Example("Cloning without hard links", new Clone_Verb_With_Usage_Attribute { NoHardLinks = true, Urls = new[] { "https://github.com/gsscoder/csharpx" } });
            }
        }
    }

    internal class Program
    {
        static int Main(string[] args)
        {
            int val = CommandLine.Parser.Default.ParseArguments<Add_Verb_With_Usage_Attribute,
                Commit_Verb_With_Usage_Attribute,
                Clone_Verb_With_Usage_Attribute>(args)
      .MapResult(
        (Add_Verb_With_Usage_Attribute opts) => RunAddAndReturnExitCode(opts),
        (Commit_Verb_With_Usage_Attribute opts) => RunCommitAndReturnExitCode(opts),
        (Clone_Verb_With_Usage_Attribute opts) => RunCloneAndReturnExitCode(opts),
        errs => 1);

            Console.ReadLine();

            return val;
        }

        private static int RunAddAndReturnExitCode(Add_Verb_With_Usage_Attribute opts)
        {
            Console.WriteLine("RUNNING ADD COMMAND");

            foreach(var file in opts.FileNames)
            {
                Console.WriteLine("File: {0}", file);
            }

            Console.WriteLine("Force: {0}", opts.Force);
            Console.WriteLine("Patch: {0}", opts.Patch);

            return 0;
        }

        private static int RunCloneAndReturnExitCode(Clone_Verb_With_Usage_Attribute opts)
        {
            Console.WriteLine("RUNNING CLONE COMMAND");
            Console.WriteLine("NoHardLinks: {0}", opts.NoHardLinks);
            Console.WriteLine("Urls: {0}", opts.Urls);
            Console.WriteLine("Quiet: {0}", opts.Quiet);
            

            return 0;
        }

        private static int RunCommitAndReturnExitCode(Commit_Verb_With_Usage_Attribute opts)
        {
            Console.WriteLine("RUNNING COMMIT COMMAND");
            Console.WriteLine("Patch: {0}", opts.Patch);
            Console.WriteLine("Amend: {0}", opts.Amend);
            Console.WriteLine("Message: {0}", opts.Message);
            Console.WriteLine("SecertOption: {0}", opts.SecertOption);

            return 0;
        }

    }
}
