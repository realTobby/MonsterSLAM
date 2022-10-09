using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]

public class Hammer : MonoBehaviour
{
    internal Animator _hammerAnimator;
    Rigidbody _hammerRigidbody;
    
    Vector3 _smoothMoveVeleocity;

    internal HammerHitBox _hammerHitBox;

    public GameObject KnockbackHitbox;

    private void Awake()
    {
        _hammerHitBox = transform.GetComponentInChildren<HammerHitBox>();
        _hammerHitBox.gameObject.SetActive(false);

        _hammerAnimator = transform.GetChild(0).GetComponent<Animator>();
        _hammerRigidbody = GetComponent<Rigidbody>();
        _hammerRigidbody.useGravity = false;
    }

    private void Start()
    {
        PlayerStats.Instance.Stats.RestartStats();
    }

    Vector3 _moveAmount;


    private void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * PlayerStats.Instance.Stats.Speed;
        _moveAmount = Vector3.SmoothDamp(_moveAmount, targetMoveAmount, ref _smoothMoveVeleocity, .15f);
    }

    public void Update()
    {
        // Cursor.visible = false;
        RotateHammerTowardsMouse();

        Move();
        
    }

    private void RotateHammerTowardsMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 worldHitPos = hit.point;
            Vector3 rotateDir = (worldHitPos - transform.position).normalized;
            var rot = Quaternion.LookRotation(rotateDir);
            rot.x = 0f;
            rot.z = 0f;
            transform.rotation = rot;
        }

        //var WorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void FixedUpdate()
    {
        // _hammerRigidbody.MovePosition(_hammerRigidbody.position + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime);

        _hammerRigidbody.velocity = _moveAmount + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _hammerRigidbody.velocity = Vector3.zero;
        }
    }


}
