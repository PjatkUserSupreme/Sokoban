using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : TileOccupier
{
    [SerializeField] private Sprite LeftSprite;
    [SerializeField] private Sprite RightSprite;
    [SerializeField] private Sprite UpSprite;
    [SerializeField] private Sprite DownSprite;
    private SpriteRenderer _spriteRenderer;

    public void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void SetSprite(string direction)
    {
        switch (direction)
        {
            case "LEFT":
            {
                _spriteRenderer.sprite = LeftSprite;
                break;
            }
            case "RIGHT":
            {
                _spriteRenderer.sprite = RightSprite;
                break;
            }
            case "UP":
            {
                _spriteRenderer.sprite = UpSprite;
                break;
            }
            case "DOWN":
            {
                _spriteRenderer.sprite = DownSprite;
                break;
            }
        }
    }
}
