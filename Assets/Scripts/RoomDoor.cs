using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public Room BelongRoom;
    public RoomDoor NextDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ChangeRoomFrom(this);
        }
    }
}
