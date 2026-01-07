using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float maxTorque = 10f;
    
    
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize); // variable editable in inspector
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        rb =  GetComponent<Rigidbody2D>(); // ref to Obstacle's physics component
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize; // variable editable in inspector
        Vector2 randomDirection = Random.insideUnitCircle;
        rb.AddForce(randomDirection * randomSpeed);
        /* ^^ Could expand to create an array or enum to
         randomize direction and amount. NO MAGIC NUMBERS.
         ".right" pushes everything to the +x direction */
        rb.AddTorque(Random.Range(-maxTorque, maxTorque));
    }

    
    void Update()
    {
        
    }
}
