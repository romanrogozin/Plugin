using System;

namespace Configuration
{
    public class ParameterScope
    {
        public ParameterScopeTypes ScopeType { get; set; }
        public string ScopeName { get; set; }

        public ParameterScope(ParameterScopeTypes scopeType, string scopeName)
        {
            ScopeType = scopeType;
            ScopeName = scopeName;
        }
        
        protected bool Equals(ParameterScope other)
        {
            return ScopeType == other.ScopeType && ScopeName == other.ScopeName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ParameterScope) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) ScopeType, ScopeName);
        }

        public static bool operator ==(ParameterScope left, ParameterScope right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ParameterScope left, ParameterScope right)
        {
            return !Equals(left, right);
        }
    }
}