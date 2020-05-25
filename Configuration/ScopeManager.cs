using System.Collections.Generic;

namespace Configuration
{
    public class ScopeManager
    {
        private readonly Dictionary<ParameterScopeTypes, IScopeResolver> _resolverByScope;

        public ScopeManager()
        {
            _resolverByScope = new Dictionary<ParameterScopeTypes, IScopeResolver>();
        }
        
        public void AddResolver(IScopeResolver scopeResolver)    
        {
            _resolverByScope.Add(scopeResolver.Scope, scopeResolver);
        }

        public IScopeResolver GetResolver(ParameterScopeTypes scopeType)
        {
            return _resolverByScope[scopeType];
        }
    }
}