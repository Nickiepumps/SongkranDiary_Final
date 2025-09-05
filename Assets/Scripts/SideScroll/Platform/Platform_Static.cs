using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Static : Platform
{
    [SerializeField] private bool isStable = true; // False if the platform is not stable
    [SerializeField] private float holdingTime = 3f;
    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Vector2 normal = collision.GetContact(0).normal;
            if(normal.y == -1)
            {
                if (isStable == true)
                {
                    platformAnim.Play();
                }
                else
                {
                    // Play platform shaking anim
                    StartCoroutine(PlatformFalling());
                }
            }
        }
    }
    private IEnumerator PlatformFalling()
    {
        platformAnim.Play("Platform_Shaking");
        yield return new WaitForSeconds(holdingTime);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().AddForce(Vector2.down);
        platformAnim.Stop();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
