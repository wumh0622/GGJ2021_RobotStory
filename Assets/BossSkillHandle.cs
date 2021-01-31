using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillHandle : MonoBehaviour
{
    public float m_fStartTime = 10.0f;

    public float m_fCDTime = 10.0f;

    BarrageSystem m_obarrageSystem;


    // Start is called before the first frame update
    void Start()
    {
        m_obarrageSystem = GetComponent<BarrageSystem>();
        InvokeRepeating("UseSkill", m_fStartTime, m_fCDTime);
    }

    // Update is called once per frame
    void UseSkill()
    {
        m_obarrageSystem.ChangeBarrageState(1, 3.0f);
    }
}
