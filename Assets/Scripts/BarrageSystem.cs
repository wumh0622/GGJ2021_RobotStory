using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BarrageSystem : MonoBehaviour
{
    [System.Serializable]
    public class SBarrageInfo
    {
        public Bullet m_oBulletObject;

        public Transform[] m_arrBulletSpawnPoints;

        public float m_fBulletDelayTime;

        public int m_nBulletAmountInOneShot;

        public float m_fOneShotInterval;

        public float m_fDamage;

        public float m_fBulletSpeed;
    }


    public string m_strAttackInputKey = "Fire1";

    public bool m_bIsPlayerInput = false;

    public SBarrageInfo[] m_arrBarrageInfos;

    SBarrageInfo m_oCurrentInfos;

    bool m_bActivate = false;

    float m_fFireTimer = 0.0f;

    bool m_bAIShoot = false;

    bool m_bStartShoot = false;

    int m_nBulletAmountTemp = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(m_arrBarrageInfos.Length > 0)
        {
            m_oCurrentInfos = m_arrBarrageInfos[0];
        }
        m_fFireTimer = m_oCurrentInfos.m_fBulletDelayTime;
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
                if (m_fFireTimer >= m_oCurrentInfos.m_fBulletDelayTime && !m_bStartShoot)
                {
                    m_bStartShoot = true;
                    InvokeRepeating("SpawnBullet", 0.0f, m_oCurrentInfos.m_fOneShotInterval);
                    m_fFireTimer = 0.0f;
                }
                if (!m_bStartShoot)
                {
                    m_fFireTimer += Time.deltaTime;
                }
            }
            else
            {
                m_fFireTimer = m_oCurrentInfos.m_fBulletDelayTime;
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
        for (int nIdx = 0; nIdx < m_oCurrentInfos.m_arrBulletSpawnPoints.Length; nIdx++)
        {
            Bullet oBullet = Instantiate(m_oCurrentInfos.m_oBulletObject);
            oBullet.transform.position = m_oCurrentInfos.m_arrBulletSpawnPoints[nIdx].position;
            oBullet.transform.rotation = m_oCurrentInfos.m_arrBulletSpawnPoints[nIdx].rotation;
            if (m_bIsPlayerInput)
            {
                oBullet.SetOwner(gameObject, EBulletOwnerType.m_ePlayer);
            }
            else
            {
                oBullet.SetOwner(gameObject, EBulletOwnerType.m_eEnemy);
            }

            oBullet.SetDamage(m_oCurrentInfos.m_fDamage);
            oBullet.SetSpeed(m_oCurrentInfos.m_fBulletSpeed);
        }
        m_nBulletAmountTemp++;
        if(m_nBulletAmountTemp == m_oCurrentInfos.m_nBulletAmountInOneShot)
        {
            m_bStartShoot = false;
            m_nBulletAmountTemp = 0;
            CancelInvoke("SpawnBullet");
        }
    }

    public void ChangeBarrageState(int nState, float fTime)
    {
        if(m_arrBarrageInfos.Length > nState)
        {
            m_bStartShoot = false;
            m_nBulletAmountTemp = 0;
            CancelInvoke("SpawnBullet");

            m_oCurrentInfos = m_arrBarrageInfos[nState];
            Invoke("ResetBarrageState", fTime);
        }
    }

    void ResetBarrageState()
    {
        if (m_arrBarrageInfos.Length > 0)
        {
            m_bStartShoot = false;
            m_nBulletAmountTemp = 0;
            CancelInvoke("SpawnBullet");
            m_oCurrentInfos = m_arrBarrageInfos[0];
        }
    }
}
