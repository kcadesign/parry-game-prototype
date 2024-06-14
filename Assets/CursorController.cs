using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float idleTime = 1.0f; // Time in seconds to wait before hiding the cursor

    private Vector3 lastMousePosition;
    private float timer;

    public static CursorController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        lastMousePosition = Input.mousePosition;
        timer = 0f;
        Cursor.visible = true; // Ensure the cursor is visible at start
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        // Check if the mouse has moved
        if (currentMousePosition != lastMousePosition)
        {
            // Reset the timer if the mouse moved
            timer = 0f;
            // Make the cursor visible
            Cursor.visible = true;
        }
        else
        {
            // Increment the timer if the mouse hasn't moved
            timer += Time.deltaTime;
            // Hide the cursor if the mouse hasn't moved for the specified idle time
            if (timer >= idleTime)
            {
                Cursor.visible = false;
            }
        }

        // Update the last mouse position
        lastMousePosition = currentMousePosition;
    }
}
