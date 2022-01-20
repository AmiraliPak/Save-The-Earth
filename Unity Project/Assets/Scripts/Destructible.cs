using UnityEngine;

// object with a life
public abstract class Destructible : MonoBehaviour
{
    protected bool friendlyFire = true;
    public float life;
    public float Score;

    public virtual void  OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();
            if(projectile == null) projectile = collision.gameObject.GetComponentInParent<IProjectile>();
            if(friendlyFire || (collision.gameObject.name != "SimpleBullet(Clone)" && collision.gameObject.name != "Missile (Launched)(Clone)"))
            {
                Debug.Log(this.GetType().ToString() + " got hit: -" + projectile.Damage.ToString());
                AddLife(-projectile.Damage);
            }
            projectile.Deactivate();
        }
    }

    protected void AddLife(float life)
    {
        this.life += life;
        TakeDamage();
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
    public virtual void OnDestruction()
    {
        EventSystemCustom.Instance.OnIncreaseScore.Invoke(Score);
    }

    public abstract void TakeDamage();
    void AnimateDestruction()
    {

    }
}
