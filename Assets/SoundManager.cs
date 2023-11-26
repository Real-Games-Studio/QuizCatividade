using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _correctSound, _wrongSound, _bgSound;

    private void Start()
    {
        _bgSound.loop = true;
        _bgSound.Play();
    }
    public void PlayCorrect()
    {
        _correctSound.Play();
    }
    public void PlayWrong()
    {
        _wrongSound.Play();
    }
}
