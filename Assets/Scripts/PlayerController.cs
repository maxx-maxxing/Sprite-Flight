using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            if(Mouse.current.leftButton.IsPressed())
                // ^^ When left mouse button pressed, the GameObject needs to rotate to point where you are clicking
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                // ^^ Use the Camera to convert mouse/pixel location to world coordinates
                Vector2 direction = (mousePos - transform.position).normalized;
                /* ^^ We need direction to be a positive vector, so we MUST subtract the
                 GameObject's world pos. FROM the mouse's world pos. Then we normalize the value
                 to keep the strength of force independent of mouse-to-object distance
                 */
                transform.up = direction;
                // ^^ The GameObjects Y will always = the positive vector calculated with each LMB press
                rb.AddForce(direction * thrustForce);
                // ^^ Push object the direction it's facing by thrustForce
                
            }    
             
            
        }
    }
}
