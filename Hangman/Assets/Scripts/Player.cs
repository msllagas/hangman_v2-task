using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int totalWins;
    public int totalLosses;

    public int gamesPlayed;
    public float winRatio;
    //public int fastestTime;

/*    public Player(int totalWins, int totalLosses, int gamesPlayed, float winRatio, int fastestTime)
    {
        this.totalWins = totalWins;
        this.totalLosses = totalLosses;
        this.gamesPlayed = gamesPlayed;
        this.winRatio = winRatio;
        this.fastestTime = fastestTime;
    }
    */
    public Player(int totalWins, int totalLosses, int gamesPlayed, float winRatio)
    {
        this.totalWins = totalWins;
        this.totalLosses = totalLosses;
        this.gamesPlayed = gamesPlayed;
        this.winRatio = winRatio;
    }


}
