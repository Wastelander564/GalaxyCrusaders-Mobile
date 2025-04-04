using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
        public float health = 150f;
        public GameObject Laser;
        public float LaserSpeed = 10f;
        public float ShotsPerSecond = 0.5f;
        public int scoreValue = 100;
        private ScoreKeeper _scoreKeeper;

        private bool canShoot = false;  
        private bool canBeHit = false;  

        public float moveSpeed = 2f;
    public float moveDistance = 3f; // How far left and right the enemy moves

    private Vector3 startPosition;

    void Start()
    {
        _scoreKeeper = GameObject.Find("ScoreKeeper")?.GetComponent<ScoreKeeper>();
    
        if (_scoreKeeper == null)
        {
            Debug.LogError("ScoreKeeper not found!");
        }

        startPosition = transform.position; // Store the starting position
    }

    void Update()
    {
        if (canShoot)
        {
            Fire();
        }

        // Move left and right
        float movement = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = new Vector3(startPosition.x + movement, transform.position.y, transform.position.z);
    }
    
    public void SetCanShootOrBeHit(bool state)
    {
        canShoot = state;
        canBeHit = state;
    }

    public void Fire()
    {
        float possibility = Time.deltaTime * ShotsPerSecond;
        if (Random.value < possibility)
        {
            GameObject beam = Instantiate(Laser, transform.position, Quaternion.identity);
            beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LaserSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _scoreKeeper.AddScore(scoreValue);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (canBeHit)
        {
            Projectile missile = col.GetComponent<Projectile>();
            if (missile)
            {
                TakeDamage(missile.GetDamage());
                missile.Hit();
            }
        }
    }
}
