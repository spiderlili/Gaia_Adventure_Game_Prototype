using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadSystem 
{
    public static void Save(string slotKey, SavingData data) {
        //save into PlayerPrefs for each data item within the SavingData structure
        PlayerPrefs.SetInt(slotKey + "_level", data.level);
        PlayerPrefs.SetFloat(slotKey + "_positionX", data.positionX);
        PlayerPrefs.SetFloat(slotKey + "_positionY", data.positionY);
        PlayerPrefs.SetInt(slotKey + "_score", data.score);
        PlayerPrefs.SetInt(slotKey + "_score", data.lives);
        PlayerPrefs.SetFloat(slotKey + "_score", data.health);
        PlayerPrefs.SetFloat(slotKey + "_positionY", data.timeElapsed);
        PlayerPrefs.SetString(slotKey + "_positionY", data.playerName);

        //save into permanent memory
        PlayerPrefs.Save();
    }

    public static SavingData Load(string slotkey) {
        //create a new SavingData structure
        SavingData data = new SavingData();

        //load from memory each item to fill up the data structure
        data.level = PlayerPrefs.GetInt(slotkey + "_level");
        data.positionX = PlayerPrefs.GetFloat(slotkey + "_positionX");
        data.positionY = PlayerPrefs.GetFloat(slotkey + "_positionY");
        data.score = PlayerPrefs.GetInt(slotkey + "_score");
        data.health = PlayerPrefs.GetFloat(slotkey + "_health");
        data.timeElapsed = PlayerPrefs.GetFloat(slotkey + "_time");
        data.playerName = PlayerPrefs.GetString(slotkey + "_name");
        data.lives = PlayerPrefs.GetInt(slotkey + "_lives");

        //return the data structure
        return data;
    }

    //checks whether a safe slot is available in meomry - show the player which slots are empty / taken
    public static bool HasSlot(string slotKey) {
        //check whether the slotkey exist
        return PlayerPrefs.HasKey(slotKey + "_level");
    }

    public static void DeleteSlot(string slotKey) {
        //delete the whole slot item by item
        PlayerPrefs.DeleteKey(slotKey + "_level");
        PlayerPrefs.DeleteKey(slotKey + "_health");
        PlayerPrefs.DeleteKey(slotKey + "_positionX");
        PlayerPrefs.DeleteKey(slotKey + "_positionY");
        PlayerPrefs.DeleteKey(slotKey + "_score");
        PlayerPrefs.DeleteKey(slotKey + "_time");
        PlayerPrefs.DeleteKey(slotKey + "_lives");
        PlayerPrefs.DeleteKey(slotKey + "_playerName");
    }

    public static void DeleteAllSlots() {
        PlayerPrefs.DeleteAll(); //delete all PlayerPrefs
    }

    public struct SavingData {
        public int level;
        public float positionX, positionY;
        public int score;
        public float timeElapsed;
        public float health;
        public int lives;
        public string playerName;
    }
}
