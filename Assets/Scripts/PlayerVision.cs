using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> visionModes = new List<GameObject>();

    private int currentIndex = -1;

    private void Start()
    {
        SetVisionMode(visionModes.Count - 1);
    }

    public int GetVisionCount()
    {
        return visionModes.Count;
    }

    public int GetVisionMode()
    {
        return currentIndex;
    }

    public void SetVisionMode(int index)
    {
        if (index < 0 || index >= visionModes.Count)
        {
            return;
        }

        currentIndex = index;
        for (int i = 0; i < visionModes.Count; i++)
        {
            visionModes[i].SetActive(false);
        }

        visionModes[index].SetActive(true);
    }
}
