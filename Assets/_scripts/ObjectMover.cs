using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private enum Direction { Right, Left, Up, Down }
    [SerializeField] private Direction _direction;

    void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        // move object based on direction
        switch (_direction)
        {
            case Direction.Right:
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                break;
            case Direction.Left:
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                break;
            case Direction.Up:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
            case Direction.Down:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                break;
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
