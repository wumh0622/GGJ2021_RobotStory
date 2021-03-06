﻿using System.Collections;
using UnityEngine;

public class RobotSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private Sprite front = null;
    [SerializeField]
    private Sprite back = null;
    [SerializeField]
    private Sprite left = null;
    [SerializeField]
    private Sprite right = null;
    [SerializeField]
    private Sprite leftFront = null;
    [SerializeField]
    private Sprite rightBack = null;
    [SerializeField]
    private float flashTime = 0.1f;

    [SerializeField]
    private Sprite special = null;

    [SerializeField]
    private bool flip = true;

    bool bSpecial = false;

    public void SetDirection(float degree)
    {
        if(bSpecial)
        {
            return;
        }
        if (degree < 0)
        {
            degree += 360f;
        }

        int sector = (Mathf.RoundToInt(degree / 22.5f) + 1) / 2;
        switch (sector)
        {
            case 0:
                if(right)
                {
                    spriteRenderer.sprite = right;
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.sprite = left;
                    spriteRenderer.flipX = true;
                }
                break;
            case 1:
                spriteRenderer.sprite = rightBack;
                spriteRenderer.flipX = false;
                break;
            case 2:
                spriteRenderer.sprite = back;
                spriteRenderer.flipX = false;
                break;
            case 3:
                spriteRenderer.sprite = rightBack;
                if(flip)
                {
                    spriteRenderer.flipX = true;
                }

                break;
            case 4:
                spriteRenderer.sprite = left;
                spriteRenderer.flipX = false;
                break;
            case 5:
                spriteRenderer.sprite = leftFront;
                spriteRenderer.flipX = false;
                break;
            case 6:
                spriteRenderer.sprite = front;
                spriteRenderer.flipX = false;
                break;
            case 7:
                spriteRenderer.sprite = leftFront;
                if (flip)
                {
                    spriteRenderer.flipX = true;
                }
                break;
        }
    }

    public void SpecialSprite(float fTime)
    {
        spriteRenderer.sprite = special;
        bSpecial = true;
        Invoke("SpecialEnd", fTime);
    }

    void SpecialEnd()
    {
        bSpecial = false;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashProcess());
    }

    private IEnumerator FlashProcess()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / flashTime;
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
            yield return null;
        }

        t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime / flashTime;
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
            yield return null;
        }

        spriteRenderer.color = Color.white;
    }
}
