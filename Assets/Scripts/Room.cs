using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsLocked = false;
    public RoomDoor DoorRight;
    public RoomDoor DoorLeft;

    List<IRoomObject> m_listAllObjectInRoom = new List<IRoomObject>();

    private void Awake()
    {
        CheckRoomObjectIsDead();
        if(m_listAllObjectInRoom.Count > 0)
        {
            IsLocked = true;
        }
    }

    private void Update()
    {
        CheckRoomObjectIsDead();
        if (m_listAllObjectInRoom.Count == 0)
        {
            IsLocked = false;
        }
    }

    public void RoomStart()
    {
        foreach (var oObject in m_listAllObjectInRoom)
        {
            oObject.ActivateObject(true);
        }
    }

    public void CheckRoomObjectIsDead()
    {
        m_listAllObjectInRoom.Clear();
        Enemy[] arrAllEnemy = GetComponentsInChildren<Enemy>();
        EnemySpawner[] arrAllSpawner = GetComponentsInChildren<EnemySpawner>();

        for (int nIdx = 0; nIdx < arrAllEnemy.Length; nIdx++)
        {
            var oObject = arrAllEnemy[nIdx] as IRoomObject;
            m_listAllObjectInRoom.Add(oObject);
        }

        for (int nIdx = 0; nIdx < arrAllSpawner.Length; nIdx++)
        {
            var oObject = arrAllSpawner[nIdx] as IRoomObject;
            m_listAllObjectInRoom.Add(oObject);
        }
    }
}
