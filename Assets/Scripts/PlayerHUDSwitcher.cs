using System.Collections;
using UnityEngine;

public class PlayerHUDSwitcher : MonoBehaviour
{
    [SerializeField]
    private PlayerSystem player = null;
    [SerializeField]
    private PlayerHUD normal = null;
    [SerializeField]
    private PlayerHUD damaged = null;
    [SerializeField]
    private int switchThreshold = 4;

    private PlayerHUD currentHUD;

    private void Start()
    {
        currentHUD = normal;
        player = GameManager.Instance.GetPlayer();
        player.OnDamaged += ShowDamagedSystem;
    }

    private void Update()
    {
        if (player.GetRemainSystemLevel() <= switchThreshold)
        {
            normal.gameObject.SetActive(false);
            damaged.gameObject.SetActive(true);
            currentHUD = damaged;
        }
        else
        {
            normal.gameObject.SetActive(true);
            damaged.gameObject.SetActive(false);
            currentHUD = normal;
        }
    }

    private void ShowDamagedSystem(SystemId systemId)
    {
        switch (systemId)
        {
            default:
                break;
            case SystemId.MOVEMENT:
                StopAllCoroutines();
                StartCoroutine(ShowSystemDamageText("噴射系統損毀"));
                break;
            case SystemId.INPUT:
                StopAllCoroutines();
                StartCoroutine(ShowSystemDamageText("傳動系統損毀"));
                break;
            case SystemId.VISION:
                StopAllCoroutines();
                StartCoroutine(ShowSystemDamageText("探測系統損毀"));
                break;
        }
    }

    private IEnumerator ShowSystemDamageText(string text)
    {
        PlayerHUD usingHUD = currentHUD;
        usingHUD.SetSystemDamgeText(text);
        yield return new WaitForSeconds(3f);
        usingHUD.SetSystemDamgeText("");
    }
}
