using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangeCrosshair : MonoBehaviour
{
    public float duration;
    public Animator animator;

    [SerializeField] private Sprite[] sprites;

    private Image image;
    public bool isFinished = true;
    private bool isNormal = true;
    private int index = 0;
    private float timer = 0;

    void Start()
    {
        image = GetComponent<Image>();
        isFinished = false;
    }
    private void Update()
    {
        if ((timer += Time.deltaTime) >= (duration / sprites.Length) && !isFinished && isNormal)
        {
            timer = 0;
            image.sprite = sprites[index];
            index = (index + 1) % sprites.Length;

            if (index == 8)
            {
                isFinished = true;
                image.sprite = sprites[index];
                isNormal = false;
            }
        }

        if ((timer += Time.deltaTime) >= (duration / sprites.Length) && !isFinished && !isNormal)
        {
            timer = 0;
            image.sprite = sprites[index];
            index = (index - 1) % sprites.Length;

            if (index == 0)
            {
                isFinished = true;
                image.sprite = sprites[index];
                isNormal = true;
            }
        }
    }

}

