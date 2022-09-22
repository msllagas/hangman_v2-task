using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats
{
    public int totalWins;
    public int totalLosses;
    public int gamesPlayed;
    public float winRatio;
    public int fastestTime = 9999; // in seconds


 

    public void SaveStats(bool hasWonGame, int playtime)
    {

        StatsData statsList = SaveSystem.LoadStats();

        //AssetDatabase.Refresh();
       // statsList.points += (hasWonGame) ? 5 : 0;
        statsList.totalWins += (hasWonGame) ? 1 : 0;
        statsList.totalLosses += (!hasWonGame) ? 1 : 0;
        statsList.gamesPlayed = statsList.totalLosses + statsList.totalWins;

        statsList.winRatio = (float)Math.Round(((float)statsList.totalWins / statsList.gamesPlayed) * 100, 2);

        if (hasWonGame)
        {
            statsList.fastestTime = (playtime >= fastestTime) ? fastestTime : playtime;
        }

        SaveSystem.SaveStats( statsList );

        /*EditorUtility.SetDirty(this); // 43
        AssetDatabase.SaveAssets(); // 43*/
    }

    public void InitStats()
    {
        SaveSystem.InitSave(this);
    }
    public void LoadStats()
    {
        StatsData data = SaveSystem.LoadStats();
    }
}
