using System.Windows;
using Test.ViewModels;

namespace Test.Services.Console
{
    internal class GLConsole
    {
        private static GLConsole _model;

        public static GLConsole Model => _model ??= new GLConsole();
        public static string Param1 { get; private set; }
        public static string Param2 { get; private set; }


        public void Controller(StartupEventArgs args)
        {
            if (args.Args.Length > 0)
            {
                Param1 = args.Args[0];
                Param2 = args.Args[1];
            }
        }

        public static bool Checker()
        {
            return Param1 switch
            {
                null => false,
                _ => true
            };
        }
    }
}