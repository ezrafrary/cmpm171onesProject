using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    /*
        A lot going on here, watching the video https://www.youtube.com/watch?v=IQRx7ojL7r8&list=PL0iUgXtqnG2gPaXE1hHYoBjqTSWTNwR-6&index=3&ab_channel=bananadev2
        will help with understanding it, as well as the following 3 videos, but that only helps with understaindg weapon switching, the projectile weapons work
        differenlty from how the video does it. 
    */

    public Image ammoCircle;
    public Image reloadCircle;

    public int damage;

    public Camera camera;

    public float fireRate;

    private float nextFire;

    [Header("VFX")]
    public GameObject hitVFX;

    [Header("Ammo")]
    public int mag = 5;
    public int ammo = 30;
    public int magAmmo = 30;

    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;

    [Header("Animation")]
    public Animation animation;
    public AnimationClip reload;


    [Header("Recoil Settings")]
    [Range(0,1)]
    // public float recoilPercent = 0.3f;
    // [Range(0,2)]
    public float recoverPercent = 0.7f;
    [Space]
    public float recoilUp = 1f;
    public float recoilBack = 0;

    [Header("projectile settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 4f;
    public GameObject playerObjForIgnoreHitbox;

    [HideInInspector]
    public bool preventFire = false;

    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;

    private bool recoiling;
    private bool recovering;
    private float recoilLength;
    private float recoverLength;
    private float reloadMaxTime;

    public void setAmmoCircle(){
        ammoCircle.fillAmount = (float) ammo / magAmmo;
    }
    public void SetReloadCircle(){
        reloadCircle.fillAmount = animation["reload"].time/reloadMaxTime;
    }


    void Start(){
        reloadMaxTime = reload.length;
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
        setAmmoCircle();
        originalPosition = transform.localPosition;
        recoilLength = 0;
        recoverLength = 1 / fireRate * recoverPercent;
    }

    void Update()
    {
        SetReloadCircle();
        if (nextFire > 0){
            nextFire -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && animation.isPlaying == false && !bulletPrefab && !preventFire){
            nextFire = 1 / fireRate;
            ammo--;
            
            SetGunText();

            Fire();
        }

        if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && animation.isPlaying == false && bulletPrefab && !preventFire){
            nextFire = 1 / fireRate;
            ammo--;
            
            SetGunText();

            FireProjectile();
        }
        


        if (Input.GetKeyDown(KeyCode.R) && animation.isPlaying == false && mag > 0 && magAmmo > ammo){
            Reload();
        }

        if (recoiling){
            Recoil();
        }
        if (recovering){
            Recovering();
        }
    }

    public void SetGunText(){
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
        setAmmoCircle();
    }

    void Reload(){
        animation.Play(reload.name);

        if (mag > 0){
            mag--;
            ammo = magAmmo;
        }
        SetGunText();
    }

    public bool IsReloading(){
        //Debug.Log("isreloading called");
        if (animation.isPlaying){
            return true;
        }
        return false;
    }


    void Fire(){
        recoiling = true;
        recovering = false;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f)){
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);

            if (hit.transform.gameObject.GetComponent<Health>()){
                //PhotonNetwork.LocalPlayer.AddScore(damage); add score for damage
                if (damage >= hit.transform.gameObject.GetComponent<Health>().health){
                    //kill

                    RoomManager.instance.kills++;
                    RoomManager.instance.SetHashes();
                    PhotonNetwork.LocalPlayer.AddScore(1);
                }
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }

    void FireProjectile(){
        recoiling = true;
        recovering = false;
        if(bulletPrefab){
            spawnBullet();

        }else{
            Debug.Log("no bullet prefab");
        }
    }


    void spawnBullet(){
        var bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawnPoint.position, Camera.main.transform.rotation * bulletPrefab.transform.rotation);
        
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

        Bullet bulletSctipt = bullet.GetComponent<Bullet>();
        bulletSctipt.setIgnoreHitbox(playerObjForIgnoreHitbox);
        
    }

    void Recoil(){
        Vector3 finalPosition = new Vector3(originalPosition.x, originalPosition.y + recoilUp, originalPosition.z - recoilBack);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoilLength);

        if(transform.localPosition == finalPosition){
            recoiling = false;
            recovering = true;
        }
    }
    
    void Recovering(){
        Vector3 finalPosition = originalPosition;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLength);

        if(transform.localPosition == finalPosition){
            recoiling = false;
            recovering = false;
        }
    }
}
