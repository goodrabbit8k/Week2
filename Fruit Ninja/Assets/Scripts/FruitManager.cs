using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] GameObject wholeFruit;
    [SerializeField] GameObject slicedFruit;

    public int fruitPoint;

    KnifeManager knife;
    GameManager scoreSystem;
    Rigidbody fruitRigidbody;
    Collider fruitCollider;
    ParticleSystem fruitJuiceEffect;

    void Start()
    {
        knife = GameObject.Find("Knife").GetComponent<KnifeManager>();
        scoreSystem = GameObject.Find("Game Manager").GetComponent<GameManager>();
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        fruitJuiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FruitSliced(knife.knifeDirection, knife.transform.position, knife.knifeForce);
        }    
    }

    void FruitSliced(Vector3 direction, Vector3 position, float force)
    {
        wholeFruit.SetActive(false);
        slicedFruit.SetActive(true);

        fruitJuiceEffect.Play();

        scoreSystem.AddingScore(fruitPoint);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slicedFruit.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] fruitSlices = slicedFruit.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody fruitSlice in fruitSlices)
        {
            fruitSlice.velocity = fruitRigidbody.velocity;
            fruitSlice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }

        fruitCollider.enabled = false;
    }
}
