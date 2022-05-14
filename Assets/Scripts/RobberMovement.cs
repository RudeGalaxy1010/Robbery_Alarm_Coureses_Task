using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    private int _nextPointIndex;

    private void Start()
    {
        _nextPointIndex = 0;
    }

    private void Update()
    {
        if (transform.position != _points[_nextPointIndex].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_nextPointIndex].position, Time.deltaTime * _speed);
        }
        else
        {
            SwitchPoint();
        }
    }

    private void SwitchPoint()
    {
        _nextPointIndex++;

        if (_nextPointIndex >= _points.Length)
        {
            _nextPointIndex = 0;
        }
    }
}
