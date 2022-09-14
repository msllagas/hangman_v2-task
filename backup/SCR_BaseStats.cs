using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // 43

// 41
//[CreateAssetMenu(fileName = "New Stat", menuName = "Hangman/Save")]
public class SCR_BaseStats : ScriptableObject
{
    [SerializeField] int totalWins;
    [SerializeField] int totalLosses;
    [SerializeField] float winRatio;
    [SerializeField] int gamesPlayed;
    [SerializeField] int fastestTime = 9999; // in seconds

    public void SaveStats(bool hasWonGame, int playtime)
    {

        //AssetDatabase.Refresh();

        totalWins += (hasWonGame)? 1 : 0;
        totalLosses += (!hasWonGame)? 1 : 0;
        gamesPlayed = totalLosses + totalWins;

        winRatio = ((float)totalWins / gamesPlayed) * 100;

        if (hasWonGame)
        {
            fastestTime = (playtime >= fastestTime) ? fastestTime : playtime;
        }

        PlayerPrefs.SetInt("totalWins", totalWins);
        PlayerPrefs.SetInt("totalLosses", totalLosses);
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
        PlayerPrefs.SetFloat("winRatio", winRatio);
        PlayerPrefs.SetInt("fastestTime", fastestTime);
        PlayerPrefs.Save();


        /*EditorUtility.SetDirty(this); // 43
        AssetDatabase.SaveAssets(); // 43*/
    }
    public List<int> GetStats() // 44
    {
        //AssetDatabase.Refresh();

        List<int> statsList = new List<int>();
        statsList.Add(PlayerPrefs.GetInt("totalWins"));
        statsList.Add(PlayerPrefs.GetInt("totalLosses"));
        statsList.Add(Mathf.RoundToInt(PlayerPrefs.GetFloat("winRatio")));
        statsList.Add(PlayerPrefs.GetInt("gamesPlayed"));
        statsList.Add(PlayerPrefs.GetInt("fastestTime"));

        return statsList;
    } 

}
