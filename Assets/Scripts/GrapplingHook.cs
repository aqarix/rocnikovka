using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] float horizontalPullSpeed;
    [SerializeField] float verticalPullSpeed;
    [SerializeField] float grappleReach;
    [SerializeField] LayerMask grappleLayer;
    [SerializeField] LineRenderer ropeRenderer;
    AudioSource asc;
    [SerializeField] AudioClip grappleSound;
    bool soundPlayed = false;

    Vector2 targetPosition;
    bool isPulling = false;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        asc = GetComponent<AudioSource>();

        if (ropeRenderer != null)
        {
            ropeRenderer.positionCount = 2;
            ropeRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; 

            RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseWorldPos - transform.position, Mathf.Infinity, grappleLayer);

            if (hit.collider != null)
            {
                targetPosition = hit.point;
                isPulling = true;

                if (ropeRenderer != null)
                {
                    ropeRenderer.enabled = true;
                    ropeRenderer.SetPosition(0, transform.position);
                    ropeRenderer.SetPosition(1, targetPosition);
                }

                if(!soundPlayed)
                {
                    asc.PlayOneShot(grappleSound);
                    soundPlayed = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPulling = false;
            soundPlayed = false;
            if (ropeRenderer != null)
            {
                ropeRenderer.enabled = false;
            }
        }

        if (isPulling && Vector2.Distance(transform.position, targetPosition) >= grappleReach)
        {
            isPulling = false;
            if (ropeRenderer != null)
            {
                ropeRenderer.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isPulling && Input.GetMouseButton(0))
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            float pullX = direction.x * horizontalPullSpeed * Time.fixedDeltaTime;
            float pullY = direction.y * verticalPullSpeed * Time.fixedDeltaTime;

            rb.velocity += new Vector2(pullX, pullY);

            if (ropeRenderer != null)
            {
                ropeRenderer.SetPosition(0, transform.position);
            }
        }
    }
}
