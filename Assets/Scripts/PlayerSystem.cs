using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement movement = null;

    [Header("Default Values")]
    [SerializeField]
    private float defaultSpeed = 5f;
    [SerializeField]
    [Tooltip("行動速度(%)")]
    private float[] speedPercentages = new float[0];

    public enum SystemId { MOVEMENT }

    private readonly System.Random random = new System.Random();
    private readonly List<int> systemLevels = new List<int>();

    private void Start()
    {
        int systemIdLength = Enum.GetValues(typeof(SystemId)).Length;
        for (int i = 0; i < systemIdLength; i++)
        {
            SystemId systemId = (SystemId)i;
            switch (systemId)
            {
                default:
                    systemLevels.Add(0);
                    break;
                case SystemId.MOVEMENT:
                    systemLevels.Add(speedPercentages.Length - 1);
                    break;
            }
        }

        RefreshSystemValues();
    }

    public void Damaged()
    {
        Debug.Log("受傷了");
        int indexOfSystemId = MathUtility.RandomWithWeights(random, systemLevels);
        if (systemLevels[indexOfSystemId] > 0)
        {
            systemLevels[indexOfSystemId]--;
        }
        RefreshSystemValues();
    }

    private void RefreshSystemValues()
    {
        for (int i = 0; i < systemLevels.Count; i++)
        {
            SystemId systemId = (SystemId)i;
            int systemLevel = systemLevels[(int)systemId];
            switch (systemId)
            {
                default:
                    break;
                case SystemId.MOVEMENT:
                    movement.Speed = defaultSpeed * speedPercentages[systemLevel];
                    break;
            }
        }
    }
}
