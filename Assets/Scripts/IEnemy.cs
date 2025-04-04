public interface IEnemy
{
    void SetCanShootOrBeHit(bool state);
    void Fire();
    void TakeDamage(float damage);
}
