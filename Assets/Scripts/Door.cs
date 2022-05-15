using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [HideInInspector] public UnityAction Triggered;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<RobberMovement>(out RobberMovement movement))
        {
            Triggered?.Invoke();
        }
    }
}
