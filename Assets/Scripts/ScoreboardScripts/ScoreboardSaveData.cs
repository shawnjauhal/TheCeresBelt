using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scoreboards
{
    [Serializable]
    public class ScoreboardSaveData
    {
        [SerializeField] private List<EntryData> scores = new List<EntryData>();


        public List<EntryData> GetScores() {
            return scores;
        }
        
        public void Add(EntryData data) {
            scores.Add(data);
            scores.Sort();
        }

        
    }
}