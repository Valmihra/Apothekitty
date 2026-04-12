using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //private Camera followPlayer;
    public Transform moveDestination;
    
    public LayerMask barriers;
    
    public static float moveSpeed = 5.0f;

    public bool canMove = true;

    void Awake()
    {
        moveDestination = GetComponentInChildren<Transform>();
    }
    
    //public Transform transform;
    
    //Vector3 position = transform.position;
    //Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //transform = GameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 currentPos = transform.position;
        //position = Transform.position;

        if (Input.GetKeyDown(/*KeyCode.A || /KeyCode.LeftArrow))
        {
            //currentPos.x -= 1f * Time.deltaTime;
            currentPos = new Vector3(-.1f,0,0);// * Time.deltaTime;
        }
        if (Input.GetKeyDown(/*KeyCode.D || /KeyCode.RightArrow))
        {
            //currentPos.x += 1f * Time.deltaTime;
            currentPos = new Vector3(.1f,0,0);//   * Time.deltaTime;
        }

        transform.position += currentPos * Time.deltaTime;*/


        transform.position = Vector3.MoveTowards(transform.position, moveDestination.position, moveSpeed);

        if (Vector3.Distance(transform.position, moveDestination.position) <= 0f)
        {
            if (Input.GetAxisRaw("Horizontal") == 1f)
            {
                //if (Physics2D.OverlapCircle(moveDestination.position + Vector3.right, 0.2f, barriers))
                {
                    MovePlayer(Vector3.right * moveSpeed * Time.deltaTime);
                }
            }
            else if (-Input.GetAxisRaw("Horizontal") == 1f)
            {
                //if (Physics2D.OverlapCircle(moveDestination.position + Vector3.right, 0.2f, barriers))
                {
                    MovePlayer(-Vector3.right * moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    void MovePlayer(Vector3 direction)
    {
        //Debug.Log("Moving");
        moveDestination.position += direction;
    }
}


