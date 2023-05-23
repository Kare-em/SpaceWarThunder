using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, vertical, 0f);
        transform.Translate(direction * (_speed * Time.fixedDeltaTime));
    }
}