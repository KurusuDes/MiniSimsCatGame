using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public List<Vector3> positions;//-List of positions to move
    public float moveSpeed = 1.0f;
    private int currentIndex;
    public Animator animator;
    public bool ableMovent = true;

    [HideInInspector]
    public int lastDir;
    void Update()
    {
        if (ableMovent)
            Movement();
    }
    public void Movement()
    {
        // If there are no positions, do nothing
        if (positions.Count == 0)
            return;

        // Get the current position and the target position
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = positions[currentIndex] + new Vector3(-22.5f, -5.5f, 0);

        // Calculate a new position that is a blend of the current and target positions based on the move speed
        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        // Update the position of the object
        transform.position = newPosition;

        // If the object has reached the target position, move to the next position
        if (Vector3.Distance(newPosition, targetPosition) < 0.05f)
        {
            transform.position = targetPosition;
            if (currentIndex == positions.Count - 1)
            {
                positions.Clear();
                currentIndex = 0;
            }
            currentIndex++;
        }
        DirectionMechanism();// check the direction of the moment
    }
    public void ClearList()
    {
        positions.RemoveRange(1, positions.Count - 1);
    }
    public void FixCurrentPos()//-try to create a smooth movent to change
    {      
        if (positions.Count != 0)
        {
            Vector3 keep = positions[currentIndex];
            positions.Clear();
            currentIndex = 0;
            positions.Add(keep);
        }
        else
        {
            positions.Clear();
            currentIndex = 0;
        }
    }
    public void AbortMovement()
    {
        if (positions.Count > 0)
        {
           // Vector3 targetPosition = positions[currentIndex-1] + new Vector3(-22.5f, -5.5f, 0);
            //transform.position = targetPosition;
            currentIndex = 0;
            positions.Clear();
            SetBool(5);
        }
           
    }
    public void DirectionMechanism()
    {
        
        if (positions.Count != 0)
        {
            Vector3 targetPosition = positions[currentIndex] + new Vector3(-22.5f, -5.5f, 0);
            switch (getDirection(transform.position, targetPosition))
            {
                case 1:
                    {
                        SetBool(1);
                        GameManager.Instance.setClotherPosition(1);
                        animator.Play("WalkRight");
                        lastDir = 1;
                    }
                    break;
                case 2:
                    {
                        SetBool(2);
                        GameManager.Instance.setClotherPosition(2);
                        animator.Play("WalkLeft");
                        lastDir = 2;
                    }
                    break;
                case 3:
                    {
                        SetBool(3);
                        GameManager.Instance.setClotherPosition(3);
                        animator.Play("WalkFront");
                        lastDir = 3;
                    }
                    break;
                case 4:
                    {
                        SetBool(4);
                        GameManager.Instance.setClotherPosition(4);
                        animator.Play("WalkBack");
                        lastDir = 4;
                    }
                    break;

                default:
                    break;
            }

        }
        else
        {
            SetBool(5);
        }

           
    }
   
    public void SetBool(int x)
    {
        switch (x)
        {
            case 1:
                {
                    animator.SetBool("Front", false);
                    animator.SetBool("Back", false);
                    animator.SetBool("Left", false);
                    animator.SetBool("Right", true);

                }
                break;
            case 2:
                {
                    animator.SetBool("Front", false);
                    animator.SetBool("Back", false);
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                }
                break;
            case 3:
                {
                    animator.SetBool("Front", true);
                    animator.SetBool("Back", false);
                    animator.SetBool("Left", false);
                    animator.SetBool("Right", false);
                }
                break;
            case 4:
                {
                    animator.SetBool("Front", false);
                    animator.SetBool("Back", true);
                    animator.SetBool("Left", false);
                    animator.SetBool("Right", false);
                }
                break;

            default:
                {
                    animator.SetBool("Front", false);
                    animator.SetBool("Back", false);
                    animator.SetBool("Left", false);
                    animator.SetBool("Right", false);
                }
                break;
        }
    }
    public int getDirection(Vector3 start , Vector3 end)
    {
        if (end.x > start.x)
        {
            // end is to the right of start
            return 1;
        }
        else if (end.x < start.x)
        {
            // end is to the left of start
            return 2;
        }
        else if (end.y < start.y)
        {
            // end is below start
            return 3;
        }
        else
        {
            // end is above start
            return 4;
        }
    }
    public void setMovement(bool ablemove)
    {
        ableMovent = ablemove;
    }
}
