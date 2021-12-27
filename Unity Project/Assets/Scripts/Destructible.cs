using UnityEngine;

// object with a life
public abstract class Destructible : MonoBehaviour
{
    [SerializeField] float life;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            Debug.Log(this.GetType().ToString() + " got hit: -" + projectile.Damage.ToString());
            AddLife(-projectile.Damage);
            projectile.Deactivate();
        }
    }

    void AddLife(float life)
    {
        this.life += life;
        // emit event
        CheckLife();
    }

    void CheckLife()
    {
        if (life <= 0f)
        {
            Debug.Log(this.GetType().ToString() + " life zero: Object deactivated");
            AnimateDestruction();
            OnDestruction();
            // emit event
            gameObject.SetActive(false);
        }
    }
    public abstract void OnDestruction();

    void AnimateDestruction()
    {

    }
}
