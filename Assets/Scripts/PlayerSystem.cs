using System;
using System.Collections.Generic;
using UnityEngine;

public enum SystemId { MOVEMENT, INPUT, VISION }

public class PlayerSystem : MonoBehaviour
{
    public event Action<SystemId> OnDamaged;

    [SerializeField]
    private float ultimateTime = 0.5f;

    [SerializeField]
    private PlayerMovement movement = null;
    [SerializeField]
    private PlayerInput input = null;
    [SerializeField]
    private PlayerVision vision = null;
    [SerializeField]
    private RobotSprite sprite = null;

    [Header("Default Values")]
    [SerializeField]
    [Range(1, 100)]
    private int systemDamageRate = 30;
    [SerializeField]
    private float defaultSpeed = 5f;
    [SerializeField]
    [Tooltip("行動速度(%)")]
    private float[] speedPercentages = new float[0];
    [SerializeField]
    [Tooltip("輸入延遲(frame)")]
    private int[] inputDelayFrames = new int[0];

    private readonly System.Random random = new System.Random();
    private readonly List<int> systemLevels = new List<int>();

    //For Debug
    [SerializeField]
    private bool m_bNoDamage = false;

    private float remainUltimateTime = 0f;

    private void Start()
    {
        Application.targetFrameRate = 60;

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
                case SystemId.INPUT:
                    systemLevels.Add(inputDelayFrames.Length - 1);
                    break;
                case SystemId.VISION:
                    systemLevels.Add(vision.GetVisionCount() - 1);
                    break;
            }
        }

        RefreshSystemValues();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePause())
        {
            return;
        }

        if (remainUltimateTime > 0f)
        {
            remainUltimateTime -= Time.deltaTime;
        }
    }

    public int GetRemainSystemLevel()
    {
        int total = 0;
        for (int i = 0; i < systemLevels.Count; i++)
        {
            total += systemLevels[i];
        }

        return total;
    }

    public bool IsAllSystemBroken()
    {
        int total = 0;
        for (int i = 0; i < systemLevels.Count; i++)
        {
            total += systemLevels[i];
        }

        if (total > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Damaged()
    {
        if (!m_bNoDamage)
        {
            // 受傷後的無敵時間
            if (remainUltimateTime > 0f)
            {
                return;
            }

            sprite.Flash();

            if (random.Next(1, 100) < systemDamageRate)
            {
                int indexOfSystemId = MathUtility.RandomWithWeights(random, systemLevels);
                if (systemLevels[indexOfSystemId] > 0)
                {
                    systemLevels[indexOfSystemId]--;
                }
                RefreshSystemValues();
                OnDamaged?.Invoke((SystemId)indexOfSystemId);
            }

            remainUltimateTime = ultimateTime;
        }
    }
    public void Heal()
    {
        Debug.Log("補血");

        int systemIdLength = Enum.GetValues(typeof(SystemId)).Length;
        for (int i = 0; i < systemIdLength; i++)
        {
            SystemId systemId = (SystemId)i;
            switch (systemId)
            {
                default:
                    break;
                case SystemId.MOVEMENT:
                    if (systemLevels[i] < speedPercentages.Length - 1)
                    {
                        systemLevels[i]++;
                    }
                    break;
                case SystemId.INPUT:
                    if (systemLevels[i] < inputDelayFrames.Length - 1)
                    {
                        systemLevels[i]++;
                    }
                    break;
                case SystemId.VISION:
                    if (systemLevels[i] < vision.GetVisionCount() - 1)
                    {
                        systemLevels[i]++;
                    }
                    break;
            }
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
                    if (systemLevel > speedPercentages.Length - 1)
                    {
                        systemLevel = speedPercentages.Length - 1;
                        systemLevels[(int)systemId] = systemLevel;
                    }

                    movement.Speed = defaultSpeed * speedPercentages[systemLevel];
                    Debug.LogFormat("行動速度 => {0}", movement.Speed);
                    break;
                case SystemId.INPUT:
                    if (systemLevel > inputDelayFrames.Length - 1)
                    {
                        systemLevel = inputDelayFrames.Length - 1;
                        systemLevels[(int)systemId] = systemLevel;
                    }

                    input.InputDelayFrame = inputDelayFrames[systemLevel];
                    Debug.LogFormat("輸入延遲 => {0}", input.InputDelayFrame);
                    break;
                case SystemId.VISION:
                    if (systemLevel > vision.GetVisionCount() - 1)
                    {
                        systemLevel = vision.GetVisionCount() - 1;
                        systemLevels[(int)systemId] = systemLevel;
                    }

                    vision.SetVisionMode(systemLevel);
                    Debug.LogFormat("顯示範圍 => Level {0}", vision.GetVisionMode());
                    break;
            }
        }
    }
}
