using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{

    public int totalWins;
    public int totalLosses;

    public int gamesPlayed;
    public float winRatio;
    public int fastestTime;

    //public int points;
    public StatsData ( Stats statsdata )
    {
        totalWins = statsdata.totalWins;
        totalLosses = statsdata.totalLosses;
        gamesPlayed = statsdata.gamesPlayed;
        winRatio = statsdata.winRatio;
        fastestTime = statsdata.fastestTime;
    }

}
