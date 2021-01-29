using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameCamera gameCamera = null;
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private List<Room> rooms = new List<Room>();

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].DoorRight.BelongRoom = rooms[i];
            rooms[i].DoorLeft.BelongRoom = rooms[i];
        }

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            rooms[i].DoorRight.NextDoor = rooms[i + 1].DoorLeft;
            rooms[i + 1].DoorLeft.NextDoor = rooms[i].DoorRight;
        }
    }

    public void ChangeRoomFrom(RoomDoor door)
    {
        // 房間被鎖住了，不切換
        if (door.BelongRoom.IsLocked)
        {
            return;
        }

        // 沒有下一個門，不切換
        if (door.NextDoor == null)
        {
            return;
        }

        // 切換中，不切換
        if (gameCamera.IsMoving())
        {
            return;
        }

        // 先把玩家移到下一個門的位置
        player.transform.position = door.NextDoor.transform.position;
        // 然後切換攝影機到下一個房間的位置
        gameCamera.MoveTo(door.NextDoor.BelongRoom.transform.position);
    }
}
