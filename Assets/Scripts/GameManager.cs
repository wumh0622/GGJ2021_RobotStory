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

    private bool isGamePause = false;
    private Room currentRoom;

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
            currentRoom = rooms[0];
        }
    }

    public bool IsPlayerPressDownFire()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1");
    }

    public PlayerSystem GetPlayer()
    {
        return player;
    }

    public bool IsGamePause()
    {
        return isGamePause;
    }

    public void SetGamePause(bool value)
    {
        isGamePause = value;
    }

    private void Update()
    {
        if (player.IsAllSystemBroken())
        {
            SceneManager.LoadScene(2);
        }

        // 房間解鎖後，才可以與道具互動
        if (!currentRoom.IsLocked && IsPlayerPressDownFire())
        {
            var collider = Physics2D.OverlapCircle(player.transform.position, 0.4f, 1 << LayerMask.NameToLayer("Item"));
            if (collider != null)
            {
                Item item = collider.GetComponent<Item>();
                if (item != null)
                {
                    itemPanel.SetItem(item);
                    itemPanel.gameObject.SetActive(true);
                    if(!item.bGameover)
                    {
                        SetGamePause(true);
                    }
                    else
                    {
                        SceneManager.LoadScene(3);
                    }
                }
            }
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
        currentRoom = door.NextDoor.BelongRoom;
        gameCamera.MoveTo(currentRoom.transform.position);
        //啟動房間
        currentRoom.RoomStart();
    }
}
