namespace Configuration
{
    public interface IScopeResolver
    {
        ParameterScopeTypes Scope { get; }
        ParameterScope Resolve();
    }
    
    public class ProjectScopeResolver : IScopeResolver
    {
        private HostingInformation _hostingInformation;
        
        public ProjectScopeResolver(HostingInformation hostingInformation)
        {
            _hostingInformation = hostingInformation;
        }

        public ParameterScopeTypes Scope => ParameterScopeTypes.Project;

        public ParameterScope Resolve()
        {
            return new ParameterScope(Scope, _hostingInformation.Project);
        }
    }
}