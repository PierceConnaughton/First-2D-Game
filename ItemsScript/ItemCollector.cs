using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;

    [SerializeField] private AudioSource collectSoundEffect;

    //function for when we collide with an object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collides with a cherry the item we are colliding with is destroyed
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "cherries: " + cherries;
        }
    }
}
