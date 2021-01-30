using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BarrageSystem))]
public class Enemy : MonoBehaviour
{
    public float m_fMaxHp = 100.0f;

    public float m_fSpeed = 10.0f;

    public float m_fRotSpeed = 5.0f;

    [Range(0,1)]
    public float m_fAttackAngleRange = 0.2f;

    float m_fHP = 100.0f;

    bool m_bActivateAI = false;

    GameObject m_oPlayer;

    Rigidbody2D m_oRB2D;

    BarrageSystem m_oBarrageSystem;

    // Start is called before the first frame update
    void Start()
    {
        m_oPlayer = GameManager.Instance.GetPlayer().gameObject;
        m_oRB2D = GetComponent<Rigidbody2D>();
        m_oBarrageSystem = GetComponent<BarrageSystem>();
        ActivateEnemy(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bActivateAI)
        {
            Vector2 forward = transform.up * -1;
            Vector2 toOther = m_oPlayer.transform.position - transform.position;

            float m_fVelocityForward = Vector2.Dot(forward, toOther);

            if(Mathf.Abs(m_fVelocityForward) > 0.1f)
            {
                m_oRB2D.AddTorque(m_fVelocityForward * -1 * m_fRotSpeed);
            }
            else
            {
                m_oRB2D.angularVelocity = 0.0f;
            }

            float fDistance = Vector3.Distance(m_oPlayer.transform.position, transform.position);
            if(fDistance > 4.0f)
            {
                m_oRB2D.AddForce(transform.right * m_fSpeed);
            }
            else
            {
                m_oRB2D.velocity = Vector3.zero;
            }


            if (Mathf.Abs(m_fVelocityForward) > Mathf.Abs(m_fAttackAngleRange))
            {
                Attack(false);
            }
            else
            {
                Attack(true);
            }
        }
    }

    void ActivateEnemy(bool bActivate)
    {
        m_bActivateAI = bActivate;
    }

    void Attack(bool bAttack)
    {
        m_oBarrageSystem.AIShoot(bAttack);
    }
}
