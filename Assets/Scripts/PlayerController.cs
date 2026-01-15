using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    Rigidbody2D rb;
    public float maxSpeed = 5f;
    public GameObject thruster;
    private float time = 0f;
    public float scoreMultiplier = 10f;
    private float score;
    public UIDocument uiDocument;
    /* ^^ Gives access to UILayout that we attached to the GameUI GameObject,
     that we attached to the Player GameObject.
     UILayout > GameUI > Player */
    private Label scoreText;
    /* ^^ Can only accept and store returns of type Label */
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        /* ^^ Search the UILayout for a node called Label with the name "ScoreLabel"
         and assign it to Label scoreText */

    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();



    }

    void OnCollisionEnter2D(Collision2D collision)
    // ^^ When player's RigidBody collides with ANY other collider
    {
        Destroy(gameObject);
        // ^^ Delete "this" GameObject that this script belongs to
    }

    private void UpdateScore()
    {
        if (Mouse.current.leftButton.IsPressed())
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
                if (rb.linearVelocity.magnitude > maxSpeed)
                    /* ^^ In order to ensure the object doesn't have runaway speed,
                     * we compare its speed each frame to maxSpeed. */
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
                    /* ^^ We set linearVelocity bc magnitude is Read-Only. Once we re-set the vector,
                     magnitude is then dependently calculated */

                }

            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
                // ^^ If LMB was pressed this frame (notice the no parameter syntax)
            {
                thruster.SetActive(true); // Turn on thruster sprite
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
                // ^^ If LMB was released this frame (notice the no parameter syntax)
            {
                thruster.SetActive(false); // Turn off thruster sprite
            }
    }

    private void MovePlayer()
    {
        time += Time.deltaTime; // Adds 1 to time every second, independent of framerate
        score = Mathf.FloorToInt(time * scoreMultiplier);
        // ^^ Converts score to int and updates score, every second

        scoreText.text = $"Score: {score}";
    }
}
