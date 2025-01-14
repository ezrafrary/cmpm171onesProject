using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Animation animation;
    public AnimationClip draw;

    private int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            selectedWeapon = 2;
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0){
            if (selectedWeapon >= transform.childCount - 1){
                selectedWeapon = 0;
            }else {
                selectedWeapon += 1;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            if (selectedWeapon <= 0){
                selectedWeapon = transform.childCount - 1;
            }else {
                selectedWeapon -= 1;
            }
        }

        if (previousSelectedWeapon != selectedWeapon){
            SelectWeapon();
        }
    }
    void SelectWeapon(){
        if (selectedWeapon >= transform.childCount){
            selectedWeapon = transform.childCount - 1;
        }

        animation.Stop();
        animation.Play(draw.name);

        int i = 0;
        foreach(Transform _weapon in transform){
            if(i == selectedWeapon){
                _weapon.gameObject.SetActive(true);
                _weapon.gameObject.GetComponent<Weapon>().SetGunText();
            }else{
                _weapon.gameObject.SetActive(false);
            }

            i++;
        }
        
    }
}
