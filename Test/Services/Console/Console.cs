using System.Windows;
using Test.ViewModels;

namespace Test.Services.Console
{
    internal class Console
    {
        private static Console _model;

        public static Console Model => _model ??= new Console();
        public string Param1 { get; private set; }
        public string Param2 { get; private set; }


        public void Controller(StartupEventArgs args)
        {
            if (args.Args.Length > 0)
            {
                Param1 = args.Args[0];
                Param2 = args.Args[1];
            }
        }
    }
}