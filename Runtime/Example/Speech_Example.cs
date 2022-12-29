using UnityEngine;
using AnsibleEvents;
using SpeechRecognition;
using SpeechRecognition.Events;

public class Speech_Example : MonoBehaviour
{
    private SpeechRecognizer speechRecognizer;
    public string[] commands = new[] { "Hello", "World" };

    private void OnEnable()
    {
        Ansible.Get<CommandRecognizedEvent>().Subscribe(OnCommandRecognized);
    }

    private void OnDisable()
    {
        Ansible.Get<CommandRecognizedEvent>().Unsubscribe(OnCommandRecognized);
    }

    private void Start()
    {
        this.speechRecognizer = new SpeechRecognizer();
        this.speechRecognizer.Start(this.commands);
    }

    private void OnDestroy()
    {
        this.speechRecognizer.Stop();
    }

    private void OnCommandRecognized(string command)
    {
        Debug.Log($"Command: {command}");
    }
}
