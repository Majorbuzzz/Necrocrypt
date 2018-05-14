using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float CurrentValue;
    public float Max;
    public SpriteRenderer HeartSprite;
    public SpriteRenderer ThreeQuarterHeart;
    public SpriteRenderer HalfHeart;
    public SpriteRenderer QuarterHeart;
    public SpriteRenderer EmptyHeart;
    public Transform Canvas;
    private List<SpriteRenderer> HeartsSprites = new List<SpriteRenderer>();

    private void Start()
    {
        ShowHealthSprites();
    }

    public void ChangeHealth(float value)
    {
        CurrentValue += value;
        UpdateHealthSprites();
    }

    private void ShowHealthSprites()
    {
        for (int i = 0; i < Max; i++)
        {
            var indexOfHeartToRender = i;
            var sprite = GetSprite(indexOfHeartToRender);
            var location = new Vector3(-9f + i * 0.70f, 4f, 0);
            HeartsSprites.Add(Instantiate(sprite, location, Quaternion.identity, Canvas));
            HeartsSprites[i].sortingLayerName = "UI";
        }
    }

    private void UpdateHealthSprites()
    {
        for (int i = 0; i < Max; i++)
        {
            var indexOfHeartToRender = i;
            var sprite = GetSprite(indexOfHeartToRender);
            HeartsSprites[i].sprite = sprite.sprite;
        }
    }

    private SpriteRenderer GetSprite(int indexOfHeartToRender)
    {
        if (indexOfHeartToRender > CurrentValue)
            return EmptyHeart;

        var integralPartOfDecimal = Math.Truncate(CurrentValue);

        if (indexOfHeartToRender == integralPartOfDecimal)
        {
            if (CurrentValue - Math.Floor(CurrentValue) == 0)
                return EmptyHeart;
            if (CurrentValue - Math.Floor(CurrentValue) <= 0.25)
                return QuarterHeart;
            if (CurrentValue - Math.Floor(CurrentValue) <= 0.50)
                return HalfHeart;
            if (CurrentValue - Math.Floor(CurrentValue) <= 0.75)
                return ThreeQuarterHeart;
            return HeartSprite;
        }
        else
            return HeartSprite;
    }
}
