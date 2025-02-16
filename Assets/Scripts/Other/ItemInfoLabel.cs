using TMPro;
using UnityEngine;

namespace Other
{
    public class ItemInfoLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemAmount;

        public void SetInfo(string itemName, int itemAmount)
        {
            _itemName.text = itemName;
            _itemAmount.text = $"({itemAmount.ToString()})";
        }
    }
}