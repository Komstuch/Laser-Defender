using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineThruster : MonoBehaviour
{
    GameObject player;
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Vector3 previousPosition;
    ParticleSystem particleSystem;
    Vector3 offset = new Vector3(0f, -0.4f, 0f);

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        player = FindObjectOfType<PlayerController>().gameObject;
        previousPosition = player.transform.position;
    }

    private void FixedUpdate()
    {
        currentPosition = player.transform.position;

        if(currentPosition != previousPosition)
        {
            particleSystem.Play();
            previousPosition = currentPosition;
        }
        else
        {
            particleSystem.Stop();
        }
    }
    public void KillEngine()
    {
        Destroy(gameObject);
    }
}
