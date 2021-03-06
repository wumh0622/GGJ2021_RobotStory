﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BarrageSystem))]
public class Enemy : MonoBehaviour , IRoomObject
{
    public float m_fMaxHp = 100.0f;

    public float m_fSpeed = 10.0f;

    public float m_fRotSpeed = 5.0f;

    [Range(0,1)]
    public float m_fAttackAngleRange = 0.2f;

    public float m_fStopDistance = 5.0f;

    float m_fHP = 100.0f;

    GameObject m_oPlayer;

    Rigidbody2D m_oMoveRB2D;

    BarrageSystem m_oBarrageSystem;

    RobotSprite m_oRobotSprite;

    public bool m_bActivate { get ; set; }

    // Start is called before the first frame update
    void Start()
    {
        m_fHP = m_fMaxHp;
        m_oPlayer = GameManager.Instance.GetPlayer().gameObject;
        m_oMoveRB2D = GetComponent<Rigidbody2D>();
        m_oBarrageSystem = GetComponent<BarrageSystem>();
        m_oRobotSprite = GetComponentInChildren<RobotSprite>();
        //ActivateEnemy(true);
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
            Vector2 forward = transform.up * -1;
            Vector2 toOther = (m_oPlayer.transform.position - transform.position).normalized;

            float m_fVelocityForward = Vector2.Dot(forward, toOther);

            if(Mathf.Abs(m_fVelocityForward) > 0.1f)
            {

                  m_oMoveRB2D.AddTorque(m_fVelocityForward * -1 * m_fRotSpeed);

            }
            else
            {
               // m_oRB2D.angularVelocity = 0.0f;
            }

            float fDistance = Vector3.Distance(m_oPlayer.transform.position, transform.position);
            if(fDistance > m_fStopDistance)
            {
                m_oMoveRB2D.AddForce(transform.right * m_fSpeed);
            }
            else
            {
                //m_oRB2D.velocity = Vector3.zero;
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

    void Attack(bool bAttack)
    {
        m_oBarrageSystem.AIShoot(bAttack);
    }

    public void TakeDamage(float fDamage)
    {
        m_fHP -= fDamage;
        m_oRobotSprite.Flash();
        if (m_fHP <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void AddForceToEnemy(Vector2 oForce)
    {
        m_oMoveRB2D.AddForce(oForce);
    }

    public void ActivateObject(bool bActivate)
    {
        m_bActivate = bActivate;
    }
}
