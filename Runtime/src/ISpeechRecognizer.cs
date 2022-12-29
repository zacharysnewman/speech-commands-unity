namespace SpeechRecognition
{
    public interface ISpeechRecognizer
    {
        public void Start(string[] commands) { }
        public void Poll() { }
        public void Stop() { }
    }
}

