public interface IDamageable
{
    void TakeDamage(int amount);
    void Die(bool dieInSilence = false);
}
