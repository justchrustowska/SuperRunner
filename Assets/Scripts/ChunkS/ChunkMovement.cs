using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    

    void Update()
    {
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
