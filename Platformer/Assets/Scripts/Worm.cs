using UnityEngine;

public class Worm : Entity
{
    [SerializeField] private int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hero.Instance.GetDamage();
        lives--;
        Debug.Log("У червяка " + lives);     
    
        if (lives < 1)
            Die();
    }
}
