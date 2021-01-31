using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IRoomObject
{

    public Enemy m_oSpawnObject;

    public float m_fSpawnCDTime = 5.0f;

    public float m_fSpawnRange = 3.0f;

    public int m_nSpawnCount = 1;

    //if 0 than loop spawn
    public int m_nSpawnTime = 0;

    int m_nCurrentSpawnTime = 0;

    float m_fTimer = 0.0f;

    public bool m_bActivate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
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
            if (m_fTimer >= m_fSpawnCDTime)
            {
                for (int nTime = 0; nTime < m_nSpawnCount; nTime++)
                {
                    Enemy oEnemy = Instantiate<Enemy>(m_oSpawnObject);
                    if(m_fSpawnRange != 0)
                    {
                        oEnemy.transform.position = Random.insideUnitSphere * m_fSpawnRange + transform.position;
                    }
                    else
                    {
                        oEnemy.transform.position = transform.position;
                    }
                    var oEnemyInterface = oEnemy as IRoomObject;
                    oEnemyInterface.ActivateObject(true);
                }

                if (m_nSpawnTime > 0)
                {
                    m_nCurrentSpawnTime++;
                    if (m_nCurrentSpawnTime >= m_nSpawnTime)
                    {
                        m_bActivate = false;
                        Destroy(gameObject);
                    }
                }
                m_fTimer = 0;
            }
            m_fTimer += Time.deltaTime;
        }
    }

    void IRoomObject.ActivateObject(bool bActivate)
    {
        m_bActivate = bActivate;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_fSpawnRange);
    }
}
