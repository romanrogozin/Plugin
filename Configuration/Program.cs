using System;

namespace Configuration
{
    class Program
    {
        static void Main(string[] args)
        {
            ScopeManager scopeManager = new ScopeManager();
            ProjectScopeResolver projectScopeResolver = new ProjectScopeResolver(new HostingInformation(1, 1, "Magnus"));
            scopeManager.AddResolver(projectScopeResolver);
            IScopeResolver scopeResolver = scopeManager.GetResolver(ParameterScopeTypes.Project);
            // string scopeName = scopeResolver.Resolve();
            Console.WriteLine("Hello World!");
        }
    }
}