using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //when player collides with traps tag trigger die function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();

        //dont allow the player to move
        rb.bodyType = RigidbodyType2D.Static;

        //start die animation
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        //restarts the current level when we die
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
