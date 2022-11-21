using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerState : MonoBehaviour
{
    private bool winState = false;
    public bool hasWon => winState;
    public AudioClip win_se;
    private AudioSource audiosource;

    private PlayerInput playerInput;
    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        audiosource = GameObject.Find("/AudioSource").GetComponent<AudioSource>();
    }
    public void Win() {
        if (winState) return;
        winState = true;
        playerInput.DeactivateInput();
        // TODO: Restart scene (maybe fade?)

        audiosource.Stop();
        audiosource.PlayOneShot(win_se);
    }
}
