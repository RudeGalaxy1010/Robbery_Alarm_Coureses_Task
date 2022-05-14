using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private Speaker[] _speakers;
    [SerializeField] private AlarmTrigger[] _triggers;
    private bool isTriggered;

    private void Awake()
    {
        isTriggered = false;

        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i].Triggered.AddListener(OnTriggered);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i].Triggered.RemoveListener(OnTriggered);
        }
    }

    public void OnTriggered()
    {
        isTriggered = !isTriggered;

        if (isTriggered)
        {
            for (int i = 0; i < _speakers.Length; i++)
            {
                _speakers[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < _speakers.Length; i++)
            {
                _speakers[i].Stop();
            }
        }
    }
}
