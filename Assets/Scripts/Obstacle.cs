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
        /* ^^ On collision, store this rb's speed */
        
        Rigidbody2D otherRb = collision.otherRigidbody;
        /* ^^ On collision, store the OTHER rb's RB */
        
        float otherSpeed = 0f;
        /* ^^ Instantiate otherSpeed for otherRb's speed */
        
        if (otherRb != null) // if the object rb collides with has a Rigidbody component...
        {
            otherSpeed = otherRb.linearVelocity.magnitude; // Set its speed
        }

        Obstacle otherObstacle = null;
        
        if (otherRb != null) // if the Other rb has an rb component..
        {
            otherObstacle = otherRb.GetComponent<Obstacle>(); // otherObstacle is assigned Obstacle's script
        }
        
        if (otherObstacle != null && otherSpeed > mySpeed)
            /* ^^ if otherObstacle was assigned the Obstacle script AND
                  the other rb's speed > THIS rb's speed.. */
        {
            return; 
            /* ^^ Do NOT do the rest of the code. Exit early.
                  No impact particle effect occurs */
        }
        float impactSpeed = Mathf.Max(mySpeed, otherSpeed);
        /* ^^ impactSpeed is assigned the highest value btwn mySpeed and otherSpeed */
        
        float t = Mathf.InverseLerp(0f, maxSpeed, impactSpeed);
        /* ^^ InverseLerp(a, b, v) returns how far v is between a and b (0–1)
              as a percentage */
        
        t *= t;
        /* ^^ square t to minimize small impact particle effect,
              and enhance big impact effects */
        
        float impactScale = Mathf.Lerp(minImpactScale, maxImpactScale, t);
        /* ^^ impactScale is the numerical value of t% between minImpactScale
           and maxImpactScale */
        
        Vector2 contactPoint = collision.GetContact(0).point;
        /* ^^ Return the first contact point between the two objects colliding */
        
        GameObject impactEffect = Instantiate(impactEffectPrefab, contactPoint, Quaternion.identity);
        /* ^^ Create a new instance of the prefab at the aforereturned contactPoint with no rotation */
        
        impactEffect.transform.localScale *= impactScale;
        /* ^^ Scale the size of the particle effect
              if impactScale < 1.0f, effect is smaller than original size
              if impactScale > 1.0f, effect is bigger than original size */
        
        Destroy(impactEffect, impactEffectTimeToDestroy);
        /* ^^ Destroy the impact particle effect after x time */
    }
}


