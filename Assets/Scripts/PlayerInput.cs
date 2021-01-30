using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int InputDelayFrame = 0;

    [SerializeField]
    private PlayerMovement movement = null;

    private readonly List<Vector2> inputQueue = new List<Vector2>();

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (InputDelayFrame == 0)
        {
            movement.Move(input.x, input.y);
        }
        else
        {
            inputQueue.Add(input);
            if (inputQueue.Count >= InputDelayFrame)
            {
                Vector2 oldInput = inputQueue[0];
                inputQueue.RemoveAt(0);
                movement.Move(oldInput.x, oldInput.y);
            }
        }
    }
}
