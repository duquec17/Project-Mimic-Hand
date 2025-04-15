using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionTextBox; // The text box for displaying instructions
    [SerializeField] private TextMeshProUGUI actionLogTextBox; // The text box for displaying action logs
    private Queue<string> actionLogQueue = new Queue<string>(); // Queue to store recent actions
    private const int MaxLogEntries = 5; // Limit the number of displayed actions

    private void Start()
    {
        displayInstruction(instructionTextBox.text);
        //actionLogTextBox.text = "";
    }

    public void displayInstruction(string instructions)
    {
        instructionTextBox.text = instructions;
        instructionTextBox.text = "Use the AWSD keys to move in all directions and avoid attacks";
        StartCoroutine(HideInstructionsAfterDelay(5)); // Hides instructions after 5 seconds
    }

    private IEnumerator HideInstructionsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        instructionTextBox.text = ""; // Clear the instructions
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

        // Update the log text box
        UpdateActionLogText();
    }

    private void UpdateActionLogText()
    {
        actionLogTextBox.text = string.Join("\n", actionLogQueue.ToArray());
    }
}
