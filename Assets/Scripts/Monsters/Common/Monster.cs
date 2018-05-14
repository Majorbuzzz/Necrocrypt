using System.Collections;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private MonsterHealth Health;
    public SpriteRenderer Sprite;
    internal Animator Animator;
    internal Immunity Immunity;
    public float immunityTime = 0.5f;
    
    void Awake()
    {
        Immunity = new Immunity(Sprite);
    }
    void Start()
    {
        Animator = GetComponent<Animator>();
        Health = GetComponentInChildren<MonsterHealth>();
        StartOverride();
    }

    internal virtual void StartOverride() { }

    internal virtual void UpdateOverride()
    {
        if (Immunity.IsImmune)
            Immunity.DoYourThing();
    }

    internal abstract void Die();
    internal abstract void PlayerIsInRange(GameObject playerObject);

    internal virtual void YouGotHurt(GameObject playerObject)
    {
        if (Immunity.NotImmune)
        {
            Immunity.Start(immunityTime);
            Health.ChangeValue(-3);
            Animator.SetTrigger("Hurt");
        }
    }

    void Update()
    {
        UpdateOverride();
        if (Health.IsDead)
            StartCoroutine("Dying");
    }
    

    private IEnumerator Dying()
    {
        Animator.SetTrigger("Dying");
        Die();
        yield return new WaitForSeconds(1f);
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        Destroy(gameObject);
    }
}
