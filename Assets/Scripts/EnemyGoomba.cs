using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGoomba : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    public LayerMask PlayerLayer;
    public Transform PlayerCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.Log("No player found");
        }

        if (player != null)
        {
            Vector2 targetPos = new Vector2 (player.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);            
        }

        if (CheckOnSide())
        {
            Destroy(player.gameObject);
        }
        
        if (CheckOnTop())
        {
            player.GetComponent<PlayerController>().Jump();
            Destroy(gameObject);
        }
    }

    bool CheckOnSide()
    {
        bool output = Physics2D.Raycast(transform.position, Vector2.left, 0.505f, PlayerLayer) || Physics2D.Raycast(transform.position, Vector2.right, 0.505f, PlayerLayer);
        return output;
    }

    bool CheckOnTop()
    {
        bool output = Physics2D.BoxCast(transform.position + 0.5f * Vector3.up, new Vector2(0.975f, 0.05f), 0, Vector2.up, 0, PlayerLayer);
        return output;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(transform.position + 0.5f * Vector3.up, new Vector2(0.975f, 0.025f));
    //}
}
