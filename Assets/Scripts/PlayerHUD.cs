using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private Text systemDamageText = null;

    public void SetSystemDamgeText(string text)
    {
        systemDamageText.text = text;
    }
}
