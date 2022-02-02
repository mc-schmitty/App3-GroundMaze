using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveData
{
    static readonly string dir = "/Watermaze/";
    static readonly string file = "Leaderboard.dat";
    public static readonly string path = Application.persistentDataPath + dir + file;

    // Adds a Name:Time score pair, sorts result
    public static void AddSaveData(string name, string score)
    {
        LeaderboardData ld = LoadData();
        // If empty file, create a ld with a single entry and save
        if(ld == null)
        {
            ld = new LeaderboardData();
            ld.names[0] = name;
            ld.scores[0] = score;
            
            Save(ld);
            return;
        }

        // Here's our weird ass sorting thing (Bubblesort 4 lyfe)
        string tname = name;
        string tscore = score;
        for(int i=0; i<ld.names.Length; i++)
        {
            // If its just an empty slot, we just overwrite it
            if(ld.scores[i] == "")
            {
                ld.names[i] = tname;
                ld.scores[i] = tscore;
                break;      // No point sticking around
            }
            // Check if the temp score is lesser
            else if(ScoreToFloat(tscore) < ScoreToFloat(ld.scores[i]))
            {
                // Swap Scores! (So if its a new highscore, we have to swap everything :(  Maybe insertionsort would be better)
                string temp = ld.names[i];
                ld.names[i] = tname;
                tname = temp;
                temp = ld.scores[i];
                ld.scores[i] = tscore;
                tscore = temp;
            }
        }
        // Presumable after this whole kerfuffel theres only one pair remaining, which should be the lowest, or the list wasn't full
        // Then we save. Idk if this really works, but whatever whos gonna test/read this
        Save(ld);

    }

    // Returns formatted string of entire Name:Time formatted list
    public static string GetSaveData()
    {
        LeaderboardData ld = LoadData();
        string outp = "";
        if (ld != null)
        {
            for(int i=0; i<ld.names.Length; i++)
            {
                if(ld.scores[i] != "")
                    outp += ld.names[i] + ": " + ld.scores[i] + "\n";
            }
        }

        return outp;
    }

    // Loads data from file
    static LeaderboardData LoadData()
    {
        if (File.Exists(path))
        {
            return JsonUtility.FromJson<LeaderboardData>(File.ReadAllText(path));
        }

        // Create directory for file
        Directory.CreateDirectory(Application.persistentDataPath + dir);
        return null;
    }

    static void Save(LeaderboardData ld)
    {
        File.WriteAllText(path, JsonUtility.ToJson(ld));
    }

    // Takes a ':' seperated score and turns it into a float (note: result =/= the actual time, for comparison only)
    static float ScoreToFloat(string score)
    {
        // Score is x...x:yy so only 2 strings in array
        string[] s = score.Split(':');
        // This is fine
        float f = float.Parse(s[0]);
        // and only 2 seconds for precision from range 0-59, so this is ok as well
        f += 0.01f * float.Parse(s[1]);

        return f;
    }
}

// And here's the data thing, its small enough to fit in this file i guess
[System.Serializable] 
public class LeaderboardData
{
    public string[] names = new string[10];
    public string[] scores = new string[10];
}
