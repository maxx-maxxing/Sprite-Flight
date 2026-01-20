using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed = 5f;
    public float maxSpeed = 15f;
    public float maxTorque = 10f;
    public GameObject impactEffectPrefab;
    public float impactEffectTimeToDestroy = 1f;
    public float minImpactScale = 0.5f;
    public float maxImpactScale = 1.5f;

    void Awake() // Grab the rigidbody component before frame 1
    {
        rb = GetComponent<Rigidbody2D>(); // ref to Obstacle's physics component
    }
    
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        // ^^ Even in 2D game, all transform properties utilize Vector3 types.

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize; 
        // ^^ Small things affected by magn. more, big objects less so.

        Vector2 randomDirection = Random.insideUnitCircle.normalized; 
        /* ^^ Random vector inside a unit circle (all directions possible).
           .normalized projects this vector outward to the circle's perimeter,
           ensuring a magnitude of exactly 1 for consistent directional force. */

        rb.AddForce(randomDirection * randomSpeed);
        /* ^^ .AddForce(Vector2) => That's why direction * speed works. */

        rb.AddTorque(Random.Range(-maxTorque, maxTorque));
        /* ^^ Apply a rotational force to get some spin. */
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float mySpeed = rb.linearVelocity.magnitude;
        Rigidbody2D otherRb = collision.otherRigidbody;
        float otherSpeed = 0f;
        if (otherRb != null)
        {
            otherSpeed = otherRb.linearVelocity.magnitude;
        }

        Obstacle otherObstacle = null;
        if (otherRb != null)
        {
            otherObstacle = otherRb.GetComponent<Obstacle>();
        }
        
        if (otherObstacle != null && otherSpeed > mySpeed)
        {
            return;
        }
        float impactSpeed = Mathf.Max(mySpeed, otherSpeed);
        float t = Mathf.InverseLerp(0f, maxSpeed, impactSpeed);
        t *= t;
        float impactScale = Mathf.Lerp(minImpactScale, maxImpactScale, t);
        
        
        Vector2 contactPoint = collision.GetContact(0).point;
        GameObject impactEffect = Instantiate(impactEffectPrefab, contactPoint, Quaternion.identity);
        impactEffect.transform.localScale *= impactScale;
        Destroy(impactEffect, impactEffectTimeToDestroy);
        
    }

    
}


