using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageSystem : MonoBehaviour
{
    public Bullet m_oBulletObject;

    public Transform[] m_arrBulletSpawnPoints;

    public string m_strAttackInputKey = "Fire1";

    public float m_fBulletDelayTime = 0.5f;

    public int m_nBulletAmountInOneShot = 1;

    public float m_fOneShotInterval = 0.1f;

    public float m_fDamage = 1.0f;

    public bool m_bIsPlayerInput = false;

    public float m_fBulletSpeed = 1.0f;

    bool m_bActivate = false;

    float m_fFireTimer = 0.0f;

    bool m_bAIShoot = false;

    bool m_bStartShoot = false;

    int m_nBulletAmountTemp = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_fFireTimer = m_fBulletDelayTime;
        ActivateSystem(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGamePause())
        {
            return;
        }

        if (m_bActivate)
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
                if (m_fFireTimer >= m_fBulletDelayTime && !m_bStartShoot)
                {
                    m_bStartShoot = true;
                    InvokeRepeating("SpawnBullet", 0.0f, m_fOneShotInterval);
                    m_fFireTimer = 0.0f;
                }
                if (!m_bStartShoot)
                {
                    m_fFireTimer += Time.deltaTime;
                }
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

    void SpawnBullet()
    {
        for (int nIdx = 0; nIdx < m_arrBulletSpawnPoints.Length; nIdx++)
        {
            Bullet oBullet = Instantiate(m_oBulletObject);
            oBullet.transform.position = m_arrBulletSpawnPoints[nIdx].position;
            oBullet.transform.rotation = m_arrBulletSpawnPoints[nIdx].rotation;
            if (m_bIsPlayerInput)
            {
                oBullet.SetOwner(gameObject, EBulletOwnerType.m_ePlayer);
            }
            else
            {
                oBullet.SetOwner(gameObject, EBulletOwnerType.m_eEnemy);
            }

            oBullet.SetDamage(m_fDamage);
            oBullet.SetSpeed(m_fBulletSpeed);
        }
        m_nBulletAmountTemp++;
        if(m_nBulletAmountTemp == m_nBulletAmountInOneShot)
        {
            m_bStartShoot = false;
            m_nBulletAmountTemp = 0;
            CancelInvoke("SpawnBullet");
        }
    }
}
