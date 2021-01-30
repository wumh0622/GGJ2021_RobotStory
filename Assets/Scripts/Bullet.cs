using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBulletOwnerType
{
    m_ePlayer,
    m_eEnemy
}

public class Bullet : MonoBehaviour
{
    public float m_fBulletAliveTime = 3.0f;

    public float m_fSpeed = 10.0f;

    EBulletOwnerType m_eOwnerType;

    GameObject m_oOwner;

    float m_fDamage;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestoryBullet", m_fBulletAliveTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * m_fSpeed);
    }

    void DestoryBullet()
    {
        Destroy(gameObject);
    }

    public void SetOwner(GameObject oOwner, EBulletOwnerType eType)
    {
        m_oOwner = oOwner;
        m_eOwnerType = eType;
    }

    public void SetDamage(float fDamage)
    {
        m_fDamage = fDamage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_eOwnerType == EBulletOwnerType.m_ePlayer)
        {
            if (collision.gameObject == GameManager.Instance.GetPlayer().gameObject)
            {
                return;
            }

            Enemy oHitTarget = null;
            oHitTarget = collision.gameObject.GetComponent<Enemy>();
            if (oHitTarget != null)
            {
                oHitTarget.TakeDamage(m_fDamage);
            }
        }
        else if(m_eOwnerType == EBulletOwnerType.m_eEnemy)
        {
            if(collision.gameObject == m_oOwner)
            {
                return;
            }
            else if (collision.gameObject == GameManager.Instance.GetPlayer().gameObject)
            {
                //Damage Player
                return;
            }
        }

        DestoryBullet();
    }
}
