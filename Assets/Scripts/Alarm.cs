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
    private bool _isPlaying;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        _audioSource.clip = _sound;
        _timer = 0;

        _isPlaying = false;

        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i].Triggered += Switch;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i].Triggered -= Switch;
        }
    }

    private void Switch()
    {
        if (_isPlaying)
        {
            if (_activeCoroutine != null)
            {
                StopCoroutine(_activeCoroutine);
            }

            _isPlaying = false;
            _activeCoroutine = StartCoroutine(ChangeVolumeTo(0));
        }
        else
        {
            if (_activeCoroutine != null)
            {
                StopCoroutine(_activeCoroutine);
            }

            _isPlaying = true;
            _audioSource.volume = 0;
            _activeCoroutine = StartCoroutine(ChangeVolumeTo(1));
        }
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
