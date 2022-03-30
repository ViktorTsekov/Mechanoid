using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject damagePopup;
    public GameObject bangParticleEffect;

    private float health;
    private Text score;
    private int singleton;

    void Start()
    {
        health = 500f;
        score = GameObject.Find("Score").GetComponent<Text>();
        singleton = 0;
    }

    public void takeDamage(float damage)
    {
        GameObject popup = Instantiate(damagePopup, transform.position, Quaternion.identity, transform);
        GameObject popupText = popup.transform.GetChild(0).gameObject;

        popupText.GetComponent<TextMesh>().text = "" + damage;
        health -= damage;

        if(health <= 0)
        {
            GameObject particleEffet = (GameObject) Instantiate(bangParticleEffect, transform.position, transform.rotation);
            int currentScore = int.Parse(score.text.Split(char.Parse(" "))[1]);

            if(singleton == 0)
            {
                currentScore += 100;
                score.text = "Score: " + currentScore;
                singleton = 1;
            }
  
            Destroy(gameObject);
        }
    }
}
