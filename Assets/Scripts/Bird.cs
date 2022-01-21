using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
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
