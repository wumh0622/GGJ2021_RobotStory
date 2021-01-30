using UnityEngine;

public class PlayerHUDSwitcher : MonoBehaviour
{
    [SerializeField]
    private PlayerSystem player = null;
    [SerializeField]
    private GameObject normal = null;
    [SerializeField]
    private GameObject damaged = null;
    [SerializeField]
    private int switchThreshold = 4;

    private void Update()
    {
        if (player.GetRemainSystemLevel() <= switchThreshold)
        {
            normal.SetActive(false);
            damaged.SetActive(true);
        }
        else
        {
            normal.SetActive(true);
            damaged.SetActive(false);
        }
    }
}
