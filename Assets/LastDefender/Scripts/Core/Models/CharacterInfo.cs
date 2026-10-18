using System.Collections.Generic;
using System.Linq;

namespace Assets.LastDefender.Scripts.Core.Models
{
    public class CharacterInfo
    {
        private const int INVENTORY_SIZE = 12;   
        private const int MAX_CARRY_WEIGHT = 20;
        private float _stamina;
        public string Name { get; private set; }

        public int Level { get; private set; } = 1;
        public float Experience { get; private set; }

        public float MaxHealth => 100 + Level * 10;
        public float Health { get; set; }
        public float MaxStamina => 50 + Level * 5;
        public float Stamina
        {
            get => _stamina;
            set => _stamina = value < 0 ? 0 : value > MaxStamina ? MaxStamina : value;
        }

        public List<InventoryItem> Inventory { get; private set; } = new List<InventoryItem>();

        public int Money { get; set; }

        public CharacterInfo()
        {

        }

        public float GetInventoryWeight()
        {
            return Inventory.Sum(i => i.Data.Weight * i.Count);
        }

        public void Set(string name, int level, float experience, int money, List<InventoryItem> inventory = null, float health = default, float stamina = default)
        {
            Name = name;
            Level = level;
            Experience = experience;
            Money = money;

            if (health == default)
                Health = MaxHealth;
            if (stamina == default)
                Stamina = MaxStamina;

            if (inventory != null)
                Inventory = inventory;
        }

        public void Set(CharacterInfo characterInfo)
        {
            Name = characterInfo.Name;
            Level = characterInfo.Level;
            Experience = characterInfo.Experience;
            Money = characterInfo.Money;

            if(characterInfo.Health == default)
                Health = MaxHealth;
            if (characterInfo.Stamina == default)
                Stamina = MaxStamina;

            if (characterInfo.Inventory != null)
                Inventory = characterInfo.Inventory;
        }

        public bool CanCarryItem(InventoryItem item)
        {
            if (Inventory.Count + item.Count >= INVENTORY_SIZE)
                return false;
            if (GetInventoryWeight() + item.Data.Weight * item.Count > MAX_CARRY_WEIGHT)
                return false;
            return true;
        }

        public void AddItemToInventory(InventoryItem item)
        {
            if (Inventory.Any(i => i.Data == item.Data))
            {
                var existingItem = Inventory.First(i => i.Data == item.Data);
                existingItem.Add(item.Count);
            }
            else
            {
                for (int i = 0; i < INVENTORY_SIZE; i++)
                {
                    if (!Inventory.Any(s => s.SlotIndex == i))
                    {
                        item.SlotIndex = i; 
                        Inventory.Add(item);
                        return;
                    }
                }
            }
        }

        public void RemoveItemFromInventory(InventoryItem item)
        {
            if (Inventory.Contains(item))
                Inventory.Remove(item);
        }
    }
}