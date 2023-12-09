using System;
using UnityEngine;

namespace EpicMergeClone.Utils
{
    public static class PlayerPrefsStorage
    {
        public static string GetString(string key, string defaultValue = "") 
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public static float GetFloat(string key, float defaultValue = 0.0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public static DateTime GetDateTime(string key, DateTime defaultValue)
        {
            if(DateTime.TryParse(PlayerPrefs.GetString(key), out var date))
            {
                return date;
            }

            return DateTime.UtcNow;
        }

        public static void SetDateTime(string key, DateTime value)
        {
            PlayerPrefs.SetString(key, value.ToString());
            PlayerPrefs.Save();
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}