using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float defaultSpeed = 5.0f;

    private float speed;

    private void Start()
    {
        speed = defaultSpeed;
    }

    private void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Move(float x, float y)
    {
        if (Mathf.Approximately(x, 0f) && Mathf.Approximately(y, 0f))
        {
            return;
        }

        float degree = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        Vector2 direction = new Vector2(1f, 0f).Rotate(degree);
        transform.localPosition = transform.localPosition + new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0f, 0f, degree);
    }
}
