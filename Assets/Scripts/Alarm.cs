using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private Door[] _triggers;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _volumeRisingSpeed;
    
    private AudioSource _audioSource;
    private Coroutine _activeCoroutine;
    private float _timer;
    private bool isTriggered;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        _audioSource.clip = _sound;
        _timer = 0;

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
            Play();
        }
        else
        {
            Stop();
        }
    }

    private void Play()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _audioSource.volume = 0;
        _activeCoroutine = StartCoroutine(ChangeVolumeTo(1));
    }

    private void Stop()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _activeCoroutine = StartCoroutine(ChangeVolumeTo(0));
    }

    private IEnumerator ChangeVolumeTo(float targetValue)
    {
        if (targetValue != 0 && _audioSource.isPlaying == false)
        {
            _audioSource.loop = true;
            _audioSource.Play();
        }

        while (_audioSource.volume != targetValue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetValue, _timer);
            _timer += Time.deltaTime * _volumeRisingSpeed;
            yield return null;
        }

        if (targetValue == 0 && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
