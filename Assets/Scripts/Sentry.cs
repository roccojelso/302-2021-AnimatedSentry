using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour
{

    private GameObject target;
    private bool targetLocked;

    public GameObject bullet;
    //public GameObject TurretHead;
    public Transform player;
    public Transform SentryHead;
    public GameObject BulletSpawnPoint;
    public float fireTimer;
    private bool ReadyToShoot;

    private void Start()
    {
        ReadyToShoot = true;
    }


    private void Update()
    {

        //detecing and shooting enemies 
        if (targetLocked)
        {
            //TurretHead.transform.LookAt(target.transform);
            //TurretHead.transform.Rotate(0, 90, 0);
            TurnHead();

            if (ReadyToShoot)
            {   
                Shoot();
            }


        }
    }

    void Shoot()
    {
        Transform _bullet = Instantiate(bullet.transform, BulletSpawnPoint.transform.position, Quaternion.identity);
        _bullet.transform.rotation = BulletSpawnPoint.transform.rotation;
        ReadyToShoot = false;
        StartCoroutine(FireRate());
    }

    private void TurnHead()
    {
        SentryHead.localRotation = AnimMath.Slide(SentryHead.localRotation, Quaternion.Euler(0, 90, 0));

    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        ReadyToShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && targetLocked == false)
        {
            
            target = other.gameObject;
            targetLocked = true;
        }
    }

}
