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

    EBulletOwnerType m_eOwner;

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

    public void SetOwner(EBulletOwnerType eType)
    {
        m_eOwner = eType;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DestoryBullet();
    }
}
