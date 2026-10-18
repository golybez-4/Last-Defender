using Assets.LastDefender.Scripts.Storage.Items;

namespace Assets.LastDefender.Scripts.Core.Models
{
    public class InventoryItem
    {
        public ItemData Data { get; private set; }
        public string Name { get; set; }
        public int Count { get; private set; }
        public int SlotIndex { get; set; } = -1;
        public int FastIndex { get; set; } = -1;

        public InventoryItem()
        {
        }

        public InventoryItem(ItemData data, int count)
        {
            Data = data;
            Count = count;
            Name = data?.Name;
        }
        public void Add(int amount)
        {
            Count += amount;
        }
        public void Remove(int amount)
        {
            Count -= amount;

            if (Count < 0)
                Count = 0;
        }

        public InventoryItem Clone()
        {
            InventoryItem inventoryItem = new InventoryItem
            {
                Name = Name,
                Count = Count,
                SlotIndex = SlotIndex,
                FastIndex = FastIndex
            };
            return inventoryItem;
        }
    }
}
