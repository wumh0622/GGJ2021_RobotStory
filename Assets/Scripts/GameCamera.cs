using System.Collections;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 1f)]
    private float moveTime = 0.2f;

    private bool isMoving = false;

    public bool IsMoving()
    {
        return isMoving;
    }

    public void MoveTo(Vector2 position)
    {
        StopAllCoroutines();
        StartCoroutine(MoveProcess(position));
    }

    private IEnumerator MoveProcess(Vector2 position)
    {
        isMoving = true;
        Vector3 targetPosition = new Vector3(position.x, position.y, transform.position.z);

        float t = 0f;
        Vector3 originPosition = transform.position;

        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;
            transform.position = Vector3.Slerp(originPosition, targetPosition, t);
            yield return null;
        }
        isMoving = false;
    }
}
