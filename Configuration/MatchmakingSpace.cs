using System.Collections.Generic;

namespace Configuration
{
    public class MatchmakingSpace
    {
        public MatchmakingService Configure(HostingInformation hostingInformation, ParameterManager parameterManager)
        {
            ScopeManager scopeManager = new ScopeManager();
            scopeManager.AddResolver(new ProjectScopeResolver(hostingInformation));
            var instanceIniter = new ParameterInstanceIniter(parameterManager, scopeManager);

            GameConfigurationParameters gcDefaultParams = instanceIniter.Get<GameConfigurationParameters>();
            GameConfigurationParameters gcNbaParams = instanceIniter.Get<GameConfigurationParameters>("nba");
            return new MatchmakingService(gcDefaultParams, gcNbaParams);
        }
    }

    public class ParameterInstanceIniter
    {
        private readonly Dictionary<string, ParameterScope> _scopeByName;
        private readonly ParameterManager _parametersManager;
        private readonly ScopeManager _scopeManager;

        public ParameterInstanceIniter(ParameterManager parametersManager, ScopeManager scopeManager)
        {
            _scopeByName = new Dictionary<string, ParameterScope>();
            _parametersManager = parametersManager;
            _scopeManager = scopeManager;
        }

        public void Add(string instanceName, ParameterScopeTypes scopeType)
        {
            var parameterScope = _scopeManager.GetResolver(scopeType).Resolve();
            _scopeByName[instanceName] = parameterScope;
        }

        public TParameter Get<TParameter>(string instanceName = "default")
        {
            if (!_parametersManager.InstanceByName.ContainsKey(instanceName))
            {
                return default;
            }

            var scope = _scopeByName[instanceName];
            var instanceManager = _parametersManager.InstanceByName[instanceName];
            return instanceManager.GetParameter<TParameter>(scope);
        }
    }
    
    public class ParameterManager {
        public Dictionary<string, InstanceManager> InstanceByName { get; set; }

        public ParameterManager()
        {
            InstanceByName = new Dictionary<string, InstanceManager>();
        }
    }
}