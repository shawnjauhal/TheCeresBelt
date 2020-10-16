using System;
using UnityEngine;
using System.Collections.Generic;

namespace Scoreboards
{
    [Serializable]
    public class EntryData : IComparable<EntryData>
    {
        internal EntryData(string entryName, double seconds, string formattedTime) {
            name = entryName;
            sec = seconds;
            time = formattedTime;
        }

        [SerializeField] private string name;
        [SerializeField] private double sec;
        [SerializeField] private string time;

        public string GetName() {
            return name;
        }

        public double GetSeconds() {
            return sec;
        }

        public string GetTime() {
            return time;
        }

        public int CompareTo(EntryData other)
        {
            return other.sec.CompareTo(this.sec);
        }
    }
} 