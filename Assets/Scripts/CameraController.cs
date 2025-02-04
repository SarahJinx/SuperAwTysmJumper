using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 5f;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y,
                                                            transform.position.z), followSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y,
                                                            transform.position.z), Mathf.Abs(player.GetComponent<PlayerController>().currHSpeed) * Time.deltaTime);
        }
    }
}
