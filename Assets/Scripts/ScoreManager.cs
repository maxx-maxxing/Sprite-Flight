using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;  

public class ScoreManager : MonoBehaviour
{
    public float scoreMultiplier = 10f;
    private float time = 0f;
    private int score = 0;
    private int highScore = 0;
    private UIDocument uiDocument; 
    private Label scoreText;
    private Button restartButton;
    private bool isGameOver; // defaults to false
    private Label highScoreText;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        /* Grab the UI Document on this (GameUI) GameObject.
           (GameUI holds UIDocument + ScoreManager) */

        if (uiDocument != null)
            // ^^ if uiDocument exists
        {
            var root = uiDocument.rootVisualElement;
            /* Root of the UI tree — from here we query labels/buttons. */

            // --- SCORE LABEL ---
            scoreText = root.Q<Label>("ScoreLabel");
            /* Finds the UI label whose Name is "ScoreLabel"
               (set inside UI Builder). */

            if (scoreText == null) 
                // ^^ If unsuccessfully returned a Label named "ScoreLabel"
            {
                Debug.LogError("ScoreManager: Could not find a Label named 'ScoreLabel' in the UIDocument.");
            }
            
            highScoreText = root.Q<Label>("HighScoreLabel");

            if (highScoreText == null)
            {
                Debug.LogError("ScoreManager: Could not find a Label named 'HighScoreLabel' in the UIDocument.");
            }

            // --- RESTART BUTTON ---
            restartButton = root.Q<Button>("RestartButton");
            /* Finds the restart button by Name. Controls visibility + click. */

            if (restartButton != null)
                // ^^ If successfully returned a Button named "RestartButton"
            {
                restartButton.style.display = DisplayStyle.None;
                /* ^^ Hide restart button at game start. */

                restartButton.clicked += OnRestartClicked;
                /* Register a function to run when button is clicked. */
            }
            else
            {
                Debug.LogError("ScoreManager: Could not find a Button named 'RestartButton' in the UIDocument.");
            }
        }
        else
        {
            Debug.LogError("ScoreManager: UIDocument not found on GameUI.");
        }
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }



    void Update()
    {
        if (!isGameOver)
        {
            UpdateScore();
            /* ^^ Score only increases while player is alive. */
        }
    }



    private void UpdateScore()
    {
        time += Time.deltaTime;
        /* ^^ Time.deltaTime = seconds since last frame.
           Accumulating it gives “how long player survived.” */

        score = Mathf.FloorToInt(time * scoreMultiplier);
        /* ^^ Converts survival time → integer score.
           scoreMultiplier controls points-per-second. */

        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
            /* ^^ Push new score into the UI. */
        }
        
    }


    
// vv Called by PlayerController when the player collides and dies
    public void OnPlayerDied()
    {
        isGameOver = true;
        /* ^^ Stops score from updating. Locks final score. */

        if (restartButton != null)
        {
            restartButton.style.display = DisplayStyle.Flex;
            /* ^^ Reveal Restart button after game over. */
        }
        if (highScoreText != null)
        {
            if (score > highScore)
            {
                highScoreText.text = $"High Score: {score}";
            }
        }
        /* We only update high score on death so the high score is constantly updating like the regular score tracker */
        
        
    }
    
// Restart Button → Reload the current scene
    private void OnRestartClicked()
    {
        var scene = SceneManager.GetActiveScene();
        /* Get a reference to the currently loaded scene. */

        SceneManager.LoadScene(scene.buildIndex);
        /* Reload same scene = full reset. */
    }
}

