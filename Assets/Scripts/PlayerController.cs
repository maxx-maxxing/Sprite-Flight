using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (Mouse.current.leftButton.IsPressed())
                // ^^ When left mouse button pressed, the GameObject needs to rotate to point where you are clicking
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                // ^^ Use the Camera to convert mouse/pixel location to world coordinates
                Vector2 direction = mousePos - transform.position;
                // ^^ We need direction to be a positive vector, so we MUST subtract the GameObject's world pos. FROM the mouse's world pos.
                transform.up = direction;
                // ^^ The GameObjects Y will always = the positive vector calculated with each LMB press
            }    
             
            
        }
    }
}
