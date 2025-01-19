using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class escapeMenu : MonoBehaviour
{

    public GameObject escapemenu;
    public MouseLook mouseLook;
    public WeaponSwitcher weaponSwitcher;

    private bool menuOpen = false;
    private bool buttonWasPressedLastFrame = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Esc")){
            if (!buttonWasPressedLastFrame){
                if(menuOpen){
                    closeMenu();
                }else{
                    openMenu();
                }
            }

            buttonWasPressedLastFrame = true;
        }else{
            buttonWasPressedLastFrame = false;
        }
        
    }


    private void openMenu(){
        escapemenu.SetActive(true);
        weaponSwitcher.preventFire();
        menuOpen = true;
        mouseLook.UnlockCursor();
    }
    private void closeMenu(){
        escapemenu.SetActive(false);
        weaponSwitcher.allowFire();
        mouseLook.LockCursor();
        menuOpen = false;
    }

    
    public void backToMenuButtonPressed(){
        ResetScene();
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
