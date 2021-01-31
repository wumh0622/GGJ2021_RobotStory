using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image image = null;
    [SerializeField]
    private Text itemName = null;
    [SerializeField]
    private Text description = null;

    public void SetItem(Item item)
    {
        image.sprite = item.Sprite;
        itemName.text = item.ItemName;
        description.text = item.Description;
    }
}
