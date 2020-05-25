using System.Collections.Generic;
using System.Linq;

namespace Configuration
{
    public interface IParameter { }

    public class ParameterInstance<TParameter> : IParameter
    {
        public TParameter Parameter { get; set; }
    }
    
    public class InstanceManager
    {
        private List<IParameter> _params;
        
        public string InstanceName { get; set; }

        public InstanceManager(string instanceName)
        {
            InstanceName = instanceName;
            _params = new List<IParameter>();
        }
        
        public void AddParameter<TParameter>( TParameter parameter)
        {
            var par = _params.FirstOrDefault(x => x is ParameterInstance<TParameter>);
            if (par != null)
            {
                var instance = par as ParameterInstance<TParameter>;
                instance.Parameter = parameter;
            }
            else
            {
                _params.Add(new ParameterInstance<TParameter>()
                {
                    Parameter = parameter
                });
            }
        }
        public TParameter GetParameter<TParameter>(ParameterScope scope)
        {
            if (!_params.Any(x => x is ParameterInstance<TParameter>))
            {
                return default;
            }

            return (_params.First(x => x is ParameterInstance<TParameter>) as ParameterInstance<TParameter>).Parameter;
        }
    }
}