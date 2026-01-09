using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float maxTorque = 10f;


    void Awake()
    {
        rb =  GetComponent<Rigidbody2D>(); // ref to Obstacle's physics component
    }
    
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize); // variable editable in inspector
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize; // variable editable in inspector
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDirection * randomSpeed);
        rb.AddTorque(Random.Range(-maxTorque, maxTorque));
    }

    
    void Update()
    {
        
    }
}
