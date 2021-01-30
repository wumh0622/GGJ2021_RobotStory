using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsLocked = false;
    public RoomDoor DoorRight;
    public RoomDoor DoorLeft;

    Enemy[] m_arrAllEnemyInRoom;

    private void Awake()
    {
        m_arrAllEnemyInRoom = GetComponentsInChildren<Enemy>();
    }

    public void RoomStart()
    {
        foreach (var Enemy in m_arrAllEnemyInRoom)
        {
            Enemy.ActivateEnemy(true);
        }
    }
}
