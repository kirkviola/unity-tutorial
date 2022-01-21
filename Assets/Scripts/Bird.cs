using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // SerializeField allows changing in Unity editor
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;
    private Vector2 _startPosition;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidBody.position;
        _rigidBody.isKinematic = true;

    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
    }

    private void OnMouseUp()
    {
        var currentPosition = _rigidBody.position;
        var direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(direction * _launchForce);
        _spriteRenderer.color = Color.white;
    }

    private void OnMouseDrag()
    {
        // Gets position of mouse on main camera screen
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);

        if(distance > this._maxDragDistance)
        {
            Vector2 direction = desiredPosition - this._startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + direction * _maxDragDistance;
        }


        desiredPosition.x = desiredPosition.x > _startPosition.x
            ? _startPosition.x : desiredPosition.x;

        _rigidBody.position = desiredPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidBody.position = _startPosition;
        _rigidBody.isKinematic = true;
        _rigidBody.velocity = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
