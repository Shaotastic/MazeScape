using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerShooting : MonoBehaviour
{
    // PUBLIC VARIABLES FOR TESTING
    public Transform FlashPoint;
    public GameObject bullet;
    public GameObject bulletMarker;
    public GameObject MuzzleFlash;
    public GameObject Explosion;
    public GameObject BulletImpact;
    public GameObject ArmCanon;

    public GameObject cursor;
    public float cursorSize;
    public float cursorMaxSize;
    public AudioSource RifleShotSound;
    public Transform PlayerCam;
    [SerializeField] private float fireRate;
    [SerializeField] private Animator m_Animator;
    public float markerFireRate = 0.2f;
    public float bulletSpeed = 20;
    public int vibrate;
    public int elastic;
    public bool snap;

    bool activate;
    bool shootButton;
    bool firedBullet = false;
    bool firedMarker = false;
    bool bulletSound;
    Vector3 bulletTarget;
    // PRIVATE VARIABLES

    void Start()
    {
        RifleShotSound.SetScheduledEndTime(0.1);

    }

    // Update is called once per frame (for Physics)
    void Update()
    {
        if (firedBullet)
        {
            cursor.transform.localScale = Vector3.Lerp(cursor.transform.localScale, new Vector3(cursorSize, cursorSize), (2 / fireRate) * Time.fixedDeltaTime);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            //    RifleShotSound.Stop();
            //    RifleShotSound.Play();
            //    activate = true;
            shootButton = false;
        }

        if (Input.GetButton("Fire1") && !firedBullet)
        {
            shootButton = true;

            ArmCanon.transform.DOPunchPosition(Vector3.back / 3, fireRate, vibrate, elastic, snap);
            StartCoroutine(Fire());
        }

        if (Input.GetButton("Fire2") && !firedMarker)
        {
            Debug.Log("RIght click");
            StartCoroutine(FireMarker());
        }

        //m_Animator.SetBool("Shooting", shootButton);
        //m_Animator.SetFloat("RecoilSpeed", fireRate);
    }

    void LateUpdate()
    {
        //if(shootButton)
        //{
        //    if (activate)
        //    {
        //        if (RifleShotSound.isPlaying)
        //            RifleShotSound.Stop();
        //        activate = false;
        //    }
        //
        //    if (!RifleShotSound.isPlaying)
        //    {
        //        RifleShotSound.Stop();
        //        RifleShotSound.Play();
        //        RifleShotSound.SetScheduledEndTime(AudioSettings.dspTime + 0.1);
        //    }
        //}
    }

    IEnumerator Fire()
    {
        FireBullet();
        firedBullet = true;


        Instantiate(this.MuzzleFlash, this.FlashPoint.position, Quaternion.identity);

        if (cursor)
            cursor.transform.localScale = new Vector3(cursorMaxSize, cursorMaxSize);

        if (!bulletSound)
        {
            bulletSound = true;
            if (RifleShotSound.isPlaying)
                RifleShotSound.Stop();
            RifleShotSound.Play();
        }

        yield return new WaitForSeconds(fireRate);

        bulletSound = false;
        firedBullet = false;
    }

    IEnumerator FireMarker()
    {
        FireMarkerBullet();
        firedMarker = true;
        
        yield return new WaitForSeconds(markerFireRate);
        firedMarker = false;
    }

    public void FireBullet()
    {
        Ray ray = new Ray(Camera.main.transform.position - (-1 * Camera.main.transform.forward), Camera.main.transform.forward);
        RaycastHit raycast;

        if (Physics.Raycast(ray, out raycast, Mathf.Infinity))
        {
            if (raycast.collider.tag == "Enemy")
            {
                bulletTarget = raycast.collider.transform.position;
            }
            else
                bulletTarget = raycast.point;

        }
        else
            bulletTarget = Vector3.zero;

        GameObject temp = Instantiate(bullet, this.FlashPoint.position, transform.rotation);
        temp.GetComponent<PlayerBullet>().IntializeBullet(FlashPoint.forward, bulletSpeed, bulletTarget);
    }

    void FireMarkerBullet()
    {
        Ray ray = new Ray(Camera.main.transform.position - (-1 * Camera.main.transform.forward), Camera.main.transform.forward);
        RaycastHit raycast;
        
        if (Physics.Raycast(ray, out raycast, Mathf.Infinity))
        {
            if (raycast.collider.tag == "Enemy")
            {
                bulletTarget = raycast.collider.transform.position;
            }
            else
                bulletTarget = raycast.point;

        }
        else
            bulletTarget = Vector3.zero;

        GameObject temp = Instantiate(bulletMarker, this.FlashPoint.position, transform.rotation);
        temp.GetComponent<PlayerMarker>().IntializeBullet(FlashPoint.forward, bulletSpeed, bulletTarget);
    }

    public void FireGunRayCast()
    {

    }
}
