using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float maxTorque = 10f;


    void Awake() // Grab the rigidbody component before frame 1
    {
        rb =  GetComponent<Rigidbody2D>(); // ref to Obstacle's physics component
    }
    
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize); // variable editable in inspector
        transform.localScale = new Vector3(randomSize, randomSize, 1); // change size on frame 1
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize; // small things affected by magn. more, big objects less so 
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // random direction within a circle (360 degrees)
        rb.AddForce(randomDirection * randomSpeed); // Apply force in a direction at a speed
        rb.AddTorque(Random.Range(-maxTorque, maxTorque)); // Apply a rotational force
    }

    
    void Update()
    {
        
    }
}
