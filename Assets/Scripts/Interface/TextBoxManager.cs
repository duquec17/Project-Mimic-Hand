using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox; // Single text box for both instructions and action logs
    private Queue<string> actionLogQueue = new Queue<string>(); // Queue to store recent actions
    private const int MaxLogEntries = 3; // Limit the number of displayed actions

    private void Start()
    {
        DisplayInstructions("Use the AWSD keys to move in all directions and avoid attacks");
    }

    public void DisplayInstructions(string instructions)
    {
        // Display instructions in the text box
        textBox.text = instructions;

        // Check if any of the W, A, S, or D keys are pressed
        StartCoroutine(WaitForPlayerInput());
    }

    private IEnumerator WaitForPlayerInput()
    {
        // Wait until one of the AWSD keys is pressed
        yield return new WaitUntil(() => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));

        // Hide instructions after 5 seconds
        StartCoroutine(HideInstructionsAfterDelay(5));
    }

    private IEnumerator HideInstructionsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Clear instructions and update the log
        textBox.text = "";
        LogPlayerAction("Player read the instructions.");
    }

    // Method to log a player's action
    public void LogPlayerAction(string action)
    {
        // Add action to the queue
        actionLogQueue.Enqueue(action);

        // If the queue exceeds the maximum entries, remove the oldest action
        if (actionLogQueue.Count > MaxLogEntries)
        {
            actionLogQueue.Dequeue();
        }

        // Update the text box with the action log
        UpdateActionLogText();
    }

    private void UpdateActionLogText()
    {
        // Combine all action logs into a single string and display in the text box
        textBox.text = string.Join("\n", actionLogQueue.ToArray());
    }
}
