using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageSystem : MonoBehaviour
{
    public Bullet m_oBulletObject;

    public Transform[] m_arrBulletSpawnPoints;

    public string m_strAttackInputKey = "Fire1";

    public float m_fBulletDelayTime = 0.5f;

    public bool m_bIsPlayerInput = false;

    bool m_bActivate = false;

    float m_fFireTimer = 0.0f;

    bool m_bAIShoot = false;

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
            bool bShoot = false;
            if(m_bIsPlayerInput)
            {
                if (Input.GetButton(m_strAttackInputKey))
                {
                    bShoot = true;
                }
            }
            else
            {
                if(m_bAIShoot)
                {
                    bShoot = true;
                }
            }

            if(bShoot)
            {
                if (m_fFireTimer >= m_fBulletDelayTime)
                {
                    for (int nIdx = 0; nIdx < m_arrBulletSpawnPoints.Length; nIdx++)
                    {
                        Bullet oBullet = Instantiate(m_oBulletObject);
                        oBullet.transform.position = m_arrBulletSpawnPoints[nIdx].position;
                        oBullet.transform.rotation = m_arrBulletSpawnPoints[nIdx].rotation;
                        if(m_bIsPlayerInput)
                        {
                            oBullet.SetOwner(EBulletOwnerType.m_ePlayer);
                        }
                        else
                        {
                            oBullet.SetOwner(EBulletOwnerType.m_eEnemy);
                        }
                    }
                    m_fFireTimer = 0.0f;
                }
                m_fFireTimer += Time.deltaTime;
            }
            else
            {
                m_fFireTimer = m_fBulletDelayTime;
            }
        }
    }

    public void AIShoot(bool bShoot)
    {
        m_bAIShoot = bShoot;
    }

    void ActivateSystem(bool bActivate)
    {
        m_bActivate = bActivate;
    }
}
