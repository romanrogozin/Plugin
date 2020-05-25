namespace Configuration
{
    public class HostingInformation
    {
        public int Arena { get; }
        public int Env { get; }
        public string Project { get; }
        
        public HostingInformation(int arena, int env, string project)
        {
            Arena = arena;
            Env = env;
            Project = project;
        }
    }
    
}