using System;

namespace Configuration
{
    public class MatchmakingService
    {
        private readonly GameConfigurationParameters _someGcParameters;
        private readonly GameConfigurationParameters _defaultGcParameters;
        public MatchmakingService(GameConfigurationParameters nbaGcParameters, GameConfigurationParameters defaultGcParameters)
        {
            _someGcParameters = nbaGcParameters;
            _defaultGcParameters = defaultGcParameters;
        }
        public void MatchmakeStart()
        {
             Console.WriteLine(_defaultGcParameters.RewardsCount);
        }

        public void NbaMatchmakeStart()
        {
            Console.WriteLine(_someGcParameters.RewardsCount); 
        }
    }
}