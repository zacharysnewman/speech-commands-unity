using AnsibleEvents.Events;

namespace SpeechRecognition.Events
{
    public class CommandRecognizedEvent : AnsibleEventSync<string> { }
    public class PollEvent : AnsibleEventSync { }
}

