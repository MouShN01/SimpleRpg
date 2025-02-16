using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Other.UI
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField]private Image itemIcon;
        [SerializeField]private TMP_Text itemName;
        [SerializeField]private TMP_Text itemAmount;

        public void SetItem(IItem item)
        {
            itemIcon.sprite = item.Icon;
            itemName.text = item.Name;
            itemAmount.text = $"({item.Quantity.ToString()})";
        }
    }
}