using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameCamera gameCamera = null;
    [SerializeField]
    private PlayerSystem player = null;
    [SerializeField]
    private ItemPanel itemPanel = null;
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

        //啟動第一個房間
        if (rooms.Count > 0)
        {
            rooms[0].RoomStart();
        }
    }

    public PlayerSystem GetPlayer()
    {
        return player;
    }

    private void Update()
    {
        if (player.IsAllSystemBroken())
        {
            SceneManager.LoadScene(2);
        }

        // 測試用，直接跳回標題
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void DamagePlayer()
    {
        player.Damaged();
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
        //啟動房間
        door.NextDoor.BelongRoom.RoomStart();
    }
}
