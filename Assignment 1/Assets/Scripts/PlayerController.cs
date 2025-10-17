using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private const float MIN_ROLL_VELOCITY_SQRT = 0.01f; // Magnitude of 0.1 squared

    [SerializeField]
    private int _collectScore = 1; // Give it a default value, e.g., 1

    public UnityEvent<int> OnPickupCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            OnPickupCollected.Invoke(_collectScore);

            SoundManager.PlaySound(SoundType.PICKUP);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody >();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            moveX = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) 
            moveX = 1f;

        if (Input.GetKey(KeyCode.UpArrow))
            moveZ = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            moveZ = -1f;

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        
        rb.AddForce(movement * speed , ForceMode.Force);

        // Check if the player is currently moving/rolling by checking the velocity magnitude.
        // We use sqrMagnitude for performance (it avoids a square root calculation).
        if (rb.linearVelocity.sqrMagnitude > MIN_ROLL_VELOCITY_SQRT)
        {
            SoundManager.StartRollingSound(0.5f); // Start rolling sound (set volume to your preference)
        }
        else
        {
            // If the player is stopped, turn the sound off.
            SoundManager.StopRollingSound();
        }
    }
}
