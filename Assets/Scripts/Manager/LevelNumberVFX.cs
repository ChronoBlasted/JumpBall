using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNumberVFX : MonoBehaviour
{
    public bool _done;
    public ParticleSystem _VFX;
    public TMP_Text _text;
    public string _levelNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (!_done)
            {
                _VFX.Play();
                _text.text = _levelNumber;
                _done = true;
                ParticleSystem _ps = PlayerController.Instance._VFX;
                _ps.Play();
            }
        }
    }

}
