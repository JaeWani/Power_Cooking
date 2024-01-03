using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private Queue<string> commands = new Queue<string>();

    [SerializeField] private TMP_Text commandsText;

    public void AddCommand(string command)
    {
        commands.Enqueue(command);
        UpdateQueuedCommandsText();
    }

    public void RunNextCommand()
    {
        if (commands.Count == 0) 
        {
            Debug.Log("No commands to run!");
            return;
        }

        string cmd = commands.Dequeue();
        Debug.Log(cmd);

        UpdateQueuedCommandsText();
    }

    void UpdateQueuedCommandsText()
    {
        commandsText.text = string.Empty;

        foreach (string command in commands) 
        {
            commandsText.text += command + ",";
        }
    }
}
