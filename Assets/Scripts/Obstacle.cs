using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    
    void Start()
    {
        float randomSize = Random.Range(0.5f, 2.0f);
        transform.localScale = new Vector3(randomSize, randomSize, 1);
    }

    
    void Update()
    {
        
    }
}
