using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{

    public ParticleSystem flame;
    public Light flameLight;


    public float duration = 5f;
    public float cooldown = 10f;
    private float cooldownTimer = 0f;
    private bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        flame.Stop();
        flameLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // update cooldownTimer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        //if player left clicks, enable particle system for 10 seconds
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            flame.Play();
            flameLight.enabled = true;
            cooldownTimer = cooldown;
            isOn = true;
            // scare the enemyAI
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyAI>().isScared = true;
            }
            Invoke("StopFlame", duration);
        }
    }

    // stops the flame
    void StopFlame()
    {
        flame.Stop();
        flameLight.enabled = false;
        isOn = false;
        // scare the enemyAI
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyAI>().isScared = false;
        }
    }
}
