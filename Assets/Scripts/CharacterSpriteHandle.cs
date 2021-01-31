using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteHandle : MonoBehaviour
{
    // Start is called before the first frame update

    RobotSprite m_oSpriteComponent;

    void Start()
    {
        m_oSpriteComponent = GetComponent<RobotSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (!m_oSpriteComponent)
        {
            return;
        }

        var angle = Mathf.Atan2(transform.parent.transform.up.x, transform.parent.transform.up.y) * Mathf.Rad2Deg;
        m_oSpriteComponent.SetDirection(angle);

    }
}
