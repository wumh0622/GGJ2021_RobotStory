using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageSystem : MonoBehaviour
{
    public GameObject m_oBulletObject;

    public Transform[] m_arrBulletSpawnPoints;

    public string m_strAttackInputKey;

    public float m_fBulletDelayTime = 0.5f;

    bool m_bActivate = false;

    float m_fFireTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_fFireTimer = m_fBulletDelayTime;
        ActivateSystem(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bActivate)
        {
            if(Input.GetButton(m_strAttackInputKey))
            {
                if (m_fFireTimer >= m_fBulletDelayTime)
                {
                    for (int nIdx = 0; nIdx < m_arrBulletSpawnPoints.Length; nIdx++)
                    {
                        GameObject oBullet = Instantiate(m_oBulletObject);
                        oBullet.transform.position = m_arrBulletSpawnPoints[nIdx].position;
                        oBullet.transform.rotation = m_arrBulletSpawnPoints[nIdx].rotation;
                    }
                    m_fFireTimer = 0.0f;
                }
                m_fFireTimer += Time.deltaTime;
            }
            else if(Input.GetButtonUp(m_strAttackInputKey))
            {
                m_fFireTimer = m_fBulletDelayTime;
            }
        }
    }

    void ActivateSystem(bool bActivate)
    {
        m_bActivate = bActivate;
    }
}
