using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;


public class Bullet : MonoBehaviour
{

    public int damage = 1;
    public int explosiveDamage = 0;
    public float explosiveRadius = 0;

    public GameObject ignoreHitbox;
    private float projectilelifetime = 500f;
    

    [Header("VFX")]
    public GameObject hitVFX;

    public void setIgnoreHitbox(GameObject _ignoreHitbox){
        ignoreHitbox = _ignoreHitbox;
    }

    private PhotonView pv;

    void Start(){
        pv = GetComponent<PhotonView>();
    }
    void FixedUpdate(){
        projectilelifetime--;
        if (projectilelifetime < 0){
            if(pv){
            if (pv.IsMine){
                PhotonNetwork.Destroy(gameObject);
            }
        }
        }
        
    }
    
    void OnCollisionEnter(Collision other){
        if(hitVFX){
            PhotonNetwork.Instantiate(hitVFX.name, transform.position, Quaternion.identity);
        }
        ExplosionDamage(gameObject.transform.position, explosiveRadius);

        if (other.transform.gameObject.GetComponent<Health>() && other.transform.gameObject != ignoreHitbox){
            
            //PhotonNetwork.LocalPlayer.AddScore(damage); add score for damage
            if (damage >= other.transform.gameObject.GetComponent<Health>().health){
                //kill
                RoomManager.instance.kills++;
                RoomManager.instance.SetHashes();
                PhotonNetwork.LocalPlayer.AddScore(1);
            }
            other.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
        }
            
            
        if(pv){
            if (pv.IsMine){
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.gameObject.GetComponent<Health>()){
                if(!hitCollider.transform.gameObject.GetComponent<Health>().hasTakenExplosiveDamageThisTick){
                    hitCollider.transform.gameObject.GetComponent<Health>().hasTakenExplosiveDamageThisTick = true;
                    
                    if (explosiveDamage >= hitCollider.transform.gameObject.GetComponent<Health>().health){
                        //kill
                        RoomManager.instance.kills++;
                        RoomManager.instance.SetHashes();
                        PhotonNetwork.LocalPlayer.AddScore(1);
                    }
                    hitCollider.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, explosiveDamage);
                }
            }
        }

        foreach (var hitCollider in hitColliders){
            if(hitCollider.transform.gameObject.GetComponent<Health>()){
                hitCollider.transform.gameObject.GetComponent<Health>().hasTakenExplosiveDamageThisTick = false;
            }
        }
    }
}
