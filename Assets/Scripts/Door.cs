using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [HideInInspector] public UnityEvent Triggered = new UnityEvent();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Robber"))
        {
            Triggered.Invoke();
        }
    }
}
