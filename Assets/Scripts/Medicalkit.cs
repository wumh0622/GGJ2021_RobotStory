using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicalkit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameManager.Instance.GetPlayer().gameObject)
        {
            GameManager.Instance.GetPlayer().Heal();
            Destroy(gameObject);
        }
    }
}
