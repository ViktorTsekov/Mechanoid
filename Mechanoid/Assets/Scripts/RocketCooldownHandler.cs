using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketCooldownHandler : MonoBehaviour
{
    public Image cooldownVisualIndicator;
    public GameObject cooldownText;

    private bool readyToFire;
    private float tick;
    private float timeBetweenTicks;
    private float cooldownPeriod;
    private float spriteMaxHeight;
    private float spriteCurHeight;

    void Start()
    {
        readyToFire = true;
        tick = Time.time;
        timeBetweenTicks = 1f;
        cooldownPeriod = 10f;
        spriteMaxHeight = cooldownVisualIndicator.GetComponent<RectTransform>().rect.height;
    }

    void Update()
    {
        if (Time.time > tick)
        {
            tick = Time.time + timeBetweenTicks;
            cooldown();
        }

        if(readyToFire)
        {
            cooldownText.SetActive(true);
        } 
        else
        {
            cooldownText.SetActive(false);
        }
    }

    private void cooldown()
    {
        if(!readyToFire)
        {
            if (spriteCurHeight < spriteMaxHeight)
            {
                spriteCurHeight += spriteMaxHeight / cooldownPeriod;
                cooldownVisualIndicator.rectTransform.sizeDelta = new Vector2(spriteMaxHeight, spriteCurHeight);
            } 
            else
            {
                readyToFire = true;
            }
        } 
    }

    public void setReadyToFire(bool readyToFire)
    {
        if (!readyToFire)
        {
            spriteCurHeight = 0f;
            cooldownVisualIndicator.rectTransform.sizeDelta = new Vector2(spriteMaxHeight, spriteCurHeight);
        }

        this.readyToFire = readyToFire;
    }

    public bool getReadyToFire() 
    {
        return readyToFire;
    }

}
