using Assets.LastDefender.Scripts.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using CharacterInfo = Assets.LastDefender.Scripts.Core.Models.CharacterInfo;

namespace Assets.LastDefender.Scripts.Core.Common.Helpers
{
    public static class FileHelper
    {
        public static void SaveData(CharacterInfo character)
        {
            SaveCharacterInfo(character);
            SaveInventory(character);
            PlayerPrefs.Save();
        }

        private static void SaveCharacterInfo(CharacterInfo character)
        {
            PlayerPrefs.SetString(nameof(character.Name), character.Name);
            PlayerPrefs.SetInt(nameof(character.Level), character.Level);
            PlayerPrefs.SetFloat(nameof(character.Experience), character.Experience);
            PlayerPrefs.SetInt(nameof(character.Money), character.Money);
        }

        private static void SaveInventory(CharacterInfo character)
        {
            List<InventoryItem> items = new List<InventoryItem>();
            
            foreach (var item in character.Inventory)
                items.Add(item.Clone());

            PlayerPrefs.SetString(nameof(character.Inventory), JsonConvert.SerializeObject(items));
        }

        public static CharacterInfo LoadData()
        {
            string name = PlayerPrefs.GetString(nameof(CharacterInfo.Name), "Hero");
            int level = PlayerPrefs.GetInt(nameof(CharacterInfo.Level), 1);
            float experience = PlayerPrefs.GetFloat(nameof(CharacterInfo.Experience), 0f);
            int money = PlayerPrefs.GetInt(nameof(CharacterInfo.Money), 0);

            var inventoryJson = PlayerPrefs.GetString(nameof(CharacterInfo.Inventory), "[]");
            var inventory = JsonConvert.DeserializeObject<List<InventoryItem>>(inventoryJson);
            
            var character = new CharacterInfo();
            character.Set(name, level, experience, money, inventory);

            return character;
        }
    }
}
