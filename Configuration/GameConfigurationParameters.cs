namespace Configuration
{
    public class GameConfigurationParameters
    {
        public int RewardsCount => 10; // scope: project, name: nba
        public int Sleep => 1; // scope: arena, name: 1
    }
    
    //instance name: "instance1" => GcParameters
    
    //instance1 =>
        //RewardsCount => scope: project, name: nba
        //Sleep => scope: arena, name: 1

    public class GameConfigurationNbaParameters
    {
        public int NbaRewardsCount => 10;
    }
}