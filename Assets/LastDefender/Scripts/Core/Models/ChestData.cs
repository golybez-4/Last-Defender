using System.Collections.Generic;
using System.Linq;

namespace Assets.LastDefender.Scripts.Core.Models
{
    public class ChestData
    {
        private int _inventorySize = 12;
        public List<InventoryItem> ChestItems { get; private set; } = new List<InventoryItem>();

        public void AddItemToInventory(InventoryItem item)
        {
            if (ChestItems.Any(i => i.Data == item.Data))
            {
                var existingItem = ChestItems.First(i => i.Data == item.Data);
                existingItem.Add(item.Count);
            }
            else
            {
                for (int i = 0; i < _inventorySize; i++)
                {
                    if (!ChestItems.Any(s => s.SlotIndex == i))
                    {
                        item.SlotIndex = i;
                        ChestItems.Add(item);
                        return;
                    }
                }
            }
        }

        public void RemoveItemFromInventory(InventoryItem item)
        {
            if (ChestItems.Any(i => i.Data == item.Data))
            {
                var existingItem = ChestItems.First(i => i.Data == item.Data);
                ChestItems.Remove(existingItem);
            }
        }
    }
}
