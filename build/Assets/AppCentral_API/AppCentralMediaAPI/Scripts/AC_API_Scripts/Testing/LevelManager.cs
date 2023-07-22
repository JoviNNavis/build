using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppCentralTesting
{
    public class LevelManager : MonoBehaviour
    {
        string LevelPref = "AC_LevelPref";
        string TotalLevelPlayed_Pref = "AC_TotalLevelPlayedLevelPref";

        public static int currentlevel_ID = 0;
        public static int TotalLevels_PlayedByUserSoFar = 0;

        bool oneTime = true;

        public void initialize()
        {
            currentlevel_ID = get_LevelPref();

            int nextLevelID = currentlevel_ID + 1;

            if (nextLevelID > 100)
            {
                nextLevelID = Random.Range(10, 95);
                ACLogger.UserDebug("Creating a random Level");
            }

            TotalLevels_PlayedByUserSoFar = get_TotalLevelPlayed_Pref();
        }

        private void Start()
        {
            oneTime = true;
        }

        public void InceraseLevel()
        {
            currentlevel_ID++;

            if (currentlevel_ID >= 100)
            {
                currentlevel_ID = 0;
            }

            set_levelPref(currentlevel_ID);
        }

        public void Incerase_TotalLevelsPlayedSoFar()
        {
            TotalLevels_PlayedByUserSoFar++;
            set_TotalLevelPlayed_Pref(TotalLevels_PlayedByUserSoFar);
        }

        public void DecreaseLevel()
        {
            currentlevel_ID--;

            if (currentlevel_ID < 0)
            {
                currentlevel_ID = 99;
            }

            set_levelPref(currentlevel_ID);
        }

        public int get_LevelPref()
        {
            return PlayerPrefs.GetInt(LevelPref, 1);
        }

        public int get_TotalLevelPlayed_Pref()
        {
            return PlayerPrefs.GetInt(LevelPref, 0);
        }

        private void set_levelPref(int currentLevel)
        {
            PlayerPrefs.SetInt(LevelPref, currentLevel);
        }

        private void set_TotalLevelPlayed_Pref(int currentLevel)
        {
            PlayerPrefs.SetInt(LevelPref, currentLevel);
        }
    }
}
