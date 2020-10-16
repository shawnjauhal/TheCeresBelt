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

        public int Size() {
            return scores.Count;
        }

        public EntryData[] ToSubarray(int index, int count) {
            EntryData[] entries = new EntryData[scores.Count];
            scores.CopyTo(index, entries, 0, count);
            return entries;
        }
    }
}