using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Speaker : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _volumeRisingSpeed;
    private AudioSource _audioSource;
    private Coroutine _activeCoroutine;
    private float _timer;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        _audioSource.clip = _sound;
        _timer = 0;
    }

    public void Play()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }

        _audioSource.volume = 0;
        _activeCoroutine = StartCoroutine(ChangeVolumeTo(1));
    }

    public void Stop()
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
