using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class rocket : MonoBehaviour
{
    public int damage = 1;
    public GameObject ignoreHitbox;
    public GameObject blastRadius;
    private PhotonView pv;

    public void setIgnoreHitbox(GameObject _ignoreHitbox){
        ignoreHitbox = _ignoreHitbox;
    }

    

    void Start(){

    }
    

}
