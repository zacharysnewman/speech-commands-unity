using AnsibleEvents;
using SpeechRecognition.Events;
#if UNITY_STANDALONE_WIN
using UnityEngine.Windows.Speech;
#elif UNITY_STANDALONE_OSX
using System.Runtime.InteropServices;
#endif

namespace SpeechRecognition
{
    public class SpeechRecognizer : ISpeechRecognizer
    {
        public SpeechRecognizer()
        {
            Ansible.Get<PollEvent>().Subscribe(Poll);
        }
        ~SpeechRecognizer()
        {
            Ansible.Get<PollEvent>().Unsubscribe(Poll);
        }

        #if UNITY_STANDALONE_OSX
        [DllImport("Speech-MacOS")]
        public static extern void StartListening(string commandsStr);
        [DllImport("Speech-MacOS")]
        public static extern void StopListening();
        [DllImport("Speech-MacOS")]
        public static extern bool DidRecognizeCommand();
        [DllImport("Speech-MacOS")]
        public static extern string GetRecognizedCommand();

        public void Start(string[] commands)
        {
            StartListening(string.Join(",", commands));
        }

        public void Poll()
        {
            if (DidRecognizeCommand())
            {
                var command = GetRecognizedCommand();
                if (command != null)
                {
                    Ansible.Get<CommandRecognizedEvent>().Publish(command);
                }
            }
        }

        public void Stop()
        {
            StopListening();
        }

        #elif UNITY_STANDALONE_WIN
        private KeywordRecognizer m_Recognizer;
        private void OnPhraseRecognized(PhraseRecognizedEventArgs args) => Ansible.Get<CommandRecognizedEvent>().Publish(args.text);

        public void Start(string[] commands)
        {
            m_Recognizer = new KeywordRecognizer(commands);
            m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
            m_Recognizer.Start();
        }

        public void Poll() { }

        public void Stop()
        {
            if (m_Recognizer == null)
            {
                return;
            }
            m_Recognizer.Stop();
            m_Recognizer.Dispose();
        }
        #endif
    }
}