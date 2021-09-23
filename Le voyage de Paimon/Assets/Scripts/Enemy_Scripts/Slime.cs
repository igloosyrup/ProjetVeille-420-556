using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Slime : MonoBehaviour
{

    [SerializeField] private AIPath aiPath;
    [SerializeField] private List<GameObject> listProjectiles; 
    private const float DefaultHp = 100f;
    private const float DestroyDelay = 0.3f;
    private const string WolfWeaponTag = "WolfWeapon";
    private float _slimeHp;
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _slimeHp = DefaultHp;
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(WolfWeaponTag))
        {
            StartCoroutine(DelayDestroy());
        }
    }
    
    
    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(DestroyDelay);
        Destroy(_collider2D);
        Destroy(_rigidbody2D);
        Destroy(_spriteRenderer);
        Destroy(gameObject);
    }
}
