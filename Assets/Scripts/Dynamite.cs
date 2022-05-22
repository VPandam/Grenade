using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField]
    float timeToExplote = 3f;
    bool exploded;

    [SerializeField]
    float range = 6;

    [SerializeField]
    float explosionForce = 10;

    [SerializeField]
    ParticleSystem explosionEffect;

    Collider[] destructibleColliders;
    Collider[] destroyedColliders;

    Rigidbody _rb;
    bool throwed;
    [SerializeField]
    float throwForce;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Explote");
        _rb = GetComponent<Rigidbody>();
    }

    void Throw()
    {
        _rb.AddForce(new Vector3(0, 0.7f, 1) * throwForce, ForceMode.Impulse);
        _rb.useGravity = true;
    }
    IEnumerator Explote()
    {
        if (!exploded)
        {
            yield return new WaitForSeconds(timeToExplote);

            Instantiate(explosionEffect, this.transform.position, this.transform.rotation);

            destructibleColliders = Physics.OverlapSphere(transform.position, range);
            foreach (var collider in destructibleColliders)
            {
                Destructible destructible = collider.GetComponent<Destructible>();
                if (destructible)
                {
                    destructible.DestroyObject();
                }
            }
            destroyedColliders = Physics.OverlapSphere(transform.position, range);

            foreach (var destroyedCollider in destroyedColliders)
            {
                if (destroyedCollider && destroyedCollider.attachedRigidbody)
                {
                    destroyedCollider.attachedRigidbody.AddExplosionForce(
                        explosionForce, this.transform.position, range
                        );

                }

            }
            exploded = true;
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        if (throwed != true)
        {
            Throw();
            throwed = true;
        }

    }
}
