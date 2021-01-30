using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5.0f;

    [SerializeField]
    private Transform body = null;
    [SerializeField]
    private RobotSprite sprite = null;
    [SerializeField]
    private Transform rightSensor = null;
    [SerializeField]
    private Transform leftSensor = null;
    [SerializeField]
    private Transform topSensor = null;
    [SerializeField]
    private Transform downSensor = null;
    [SerializeField]
    private float sensorRadius = 0.02f;

    public void Move(float x, float y)
    {
        if (Mathf.Approximately(x, 0f) && Mathf.Approximately(y, 0f))
        {
            return;
        }

        float degree = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        Vector2 direction = new Vector2(1f, 0f).Rotate(degree);
        Vector3 translation = new Vector3(direction.x, direction.y, 0f) * Speed * Time.deltaTime;

        if (translation.x > 0f)
        {
            var collider = Physics2D.OverlapCircle(rightSensor.position, sensorRadius, 1);
            if (collider != null)
            {
                translation.x = 0f;
            }
        }

        if (translation.x < 0f)
        {
            var collider = Physics2D.OverlapCircle(leftSensor.position, sensorRadius, 1);
            if (collider != null)
            {
                translation.x = 0f;
            }
        }

        if (translation.y > 0f)
        {
            var collider = Physics2D.OverlapCircle(topSensor.position, sensorRadius, 1);
            if (collider != null)
            {
                translation.y = 0f;
            }
        }

        if (translation.y < 0f)
        {
            var collider = Physics2D.OverlapCircle(downSensor.position, sensorRadius, 1);
            if (collider != null)
            {
                translation.y = 0f;
            }
        }

        transform.localPosition = transform.localPosition + translation;
        body.localEulerAngles = new Vector3(0f, 0f, degree);
        sprite.SetDirection(degree);
    }
}
