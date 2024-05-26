using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyToContinue : MonoBehaviour
{
    public Animator animator;
    public AudioClip[] uIAudioClips;
    public AudioSource audioSource;
    public float delayOnClick = 0.7f;

    private static int hashClose = Animator.StringToHash("Close");
    private Button spaceButton;
    private bool flag = true;

    void Start()
    {
        spaceButton = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && flag)
        {
            flag = false;
            animator.SetBool(hashClose, true);
            MouseManager.ShowMouse();
            Invoke(nameof(PressSpace), delayOnClick);
        }
    }

    public void PressSpace()
    {
        spaceButton.onClick.Invoke();
    }
}
