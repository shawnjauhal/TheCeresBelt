using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Scoreboards 
{
    public class ScoreboardHandler : MonoBehaviour
    {
        public GameObject scoreDisplay;
        public GameObject scoreSelector;
        public ScoreboardSaveData scores;
        int numScores;
        EntryData newEntry;
        private string SavePath => $"{Application.persistentDataPath}/scoreboard.json";

        // Start is called before the first frame update
        void Start()
        {
            scores = LoadScoreboard();
            GenerateScores();
            SaveScores();
            numScores = scores.GetScores().Count;
            UpdateUI();
        }

        ScoreboardSaveData LoadScoreboard() 
        {
            if (!File.Exists(SavePath)) {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            } else {
                using(StreamReader stream = new StreamReader(SavePath)) {
                    string json = stream.ReadToEnd();
                    return JsonUtility.FromJson<ScoreboardSaveData>(json);
                }
            }
        }

        private void SaveScores() {
            using(StreamWriter stream = new StreamWriter(SavePath)) {
                string json = JsonUtility.ToJson(scores, true);
                stream.Write(json);
            }
        }

        private void UpdateUI()
        {
            int first;
            int last;
            int count = scores.GetScores().Count;
            if (count <= 100) {
                scoreSelector.SetActive(false);
                first = count - count;
                last = count - 1;
            }
            string output = "";
            int x = 1;
            foreach (EntryData entry in scores.GetScores()) {
                string line = x++ + ". " + entry.GetName() + ": " + entry.GetTime() + "\n";
                output += line;
            }
            scoreDisplay.GetComponent<Text>().text = output;
        }

        private void GenerateScores() {
            string name = "shawn";
            for (int x = 0; x < 80; x++) {
                float rand = UnityEngine.Random.Range(100f, 1000f);
                TimeSpan tspan = TimeSpan.FromSeconds(rand);
                String formattedTime = tspan.ToString(@"hh\:mm\:ss\.ff");
                EntryData entry = new EntryData(name, rand, formattedTime);
                scores.Add(entry);
                newEntry = entry;
            }
        }
    }

}
