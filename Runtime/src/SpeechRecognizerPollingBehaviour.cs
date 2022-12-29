using UnityEngine;
using AnsibleEvents;
using SpeechRecognition.Events;

// Only required for MacOS support since the MacOS speech plugin requires polling
public class SpeechRecognizerPollingBehaviour : MonoBehaviour
{
    void Update()
    {
        Ansible.Get<PollEvent>().Publish();
    }
}
