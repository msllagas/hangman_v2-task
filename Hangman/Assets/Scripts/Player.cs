using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    /*public int totalWins;
    public int totalLosses;

    public int gamesPlayed;
    public float winRatio;
    public int fastestTime;*/

    public float motivationLevel;
    public float averageMotivationLevel;
    public float motLevPerc;
    public float aveMLPerc;
    public string fullName;


    /*    public Player(int totalWins, int totalLosses, int gamesPlayed, float winRatio)
        {
            this.totalWins = totalWins;
            this.totalLosses = totalLosses;
            this.gamesPlayed = gamesPlayed;
            this.winRatio = winRatio;
        }*/

    public Player(string fullName, float motivationLevel, float averageMotivationLevel, float motLevPerc, float aveMLPerc)
    {
        this.motivationLevel = motivationLevel;
        this.averageMotivationLevel = averageMotivationLevel;
        this.motLevPerc = motLevPerc;
        this.aveMLPerc = aveMLPerc;
        this.fullName = fullName;
    }


}
