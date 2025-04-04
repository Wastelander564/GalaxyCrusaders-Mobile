using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float Xmin;
    float Xmax;

    public float padding = 0.7f;
    public float speed = 15.0f;

    public GameObject laser;

    public float beamSpeed = 10f;
    public float fireRate = 0.2f;

    public float health = 250;
    private float maxHealth; // Store max health
    public Slider healthSlider;
    public AudioClip DeathSound;
    public float sceneSwitchDelay = 2f;

    void Start()
    {
        maxHealth = health; // Store the max health value
        SetScreenBorders();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
    }

    void SetScreenBorders()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Xmin = leftmost.x + padding;
        Xmax = rightmost.x - padding;
    }

    void Move()
    {
        float newX = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        newX = Mathf.Clamp(newX, Xmin, Xmax);
        transform.position = new Vector2(newX, transform.position.y);
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 1.2f, 0);
        GameObject beam = Instantiate(laser, startPosition, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, beamSpeed);
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.GetComponent<Projectile>();
        if (missile)
        {
            TakeDamage(missile.GetDamage());
            missile.Hit();
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth()
    {
        health = maxHealth; // Reset health
        if (healthSlider != null)
        {
            healthSlider.value = health; // Update UI
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        Destroy(gameObject);
        SceneManager.LoadScene("Lose");
    }
}
