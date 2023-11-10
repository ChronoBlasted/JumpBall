using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    public Vector3 InitialForce;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ParticleSystem _dieFx;

    LevelManager _lastLevelManager;


    private void OnEnable()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 30)
        {
            if (collision.gameObject.tag == "Spike")
            {
                _dieFx.transform.parent = transform;
                _dieFx.transform.localPosition = Vector3.zero;

                _dieFx.transform.parent = null;
                _dieFx.Play();

                PlayerController.Instance.ResetBlade();

                return;
            }

            if (collision.gameObject.tag == "Jumper")
            {
                var impulse = Vector3.zero;

                impulse = new Vector3(InitialForce.x * 1.5f, 1.5f, 0);

                _rb.AddForce(impulse * 5, ForceMode2D.Impulse);

                var jumperController = collision.gameObject.GetComponent<JumperController>();

                jumperController.DoJump();


                return;
            }
        }


        if (collision.gameObject.layer == 25)
        {
            if (collision.gameObject.tag == "Gum")
            {
                InitialForce = new Vector3(InitialForce.x * -1, InitialForce.y, InitialForce.z);
            }
            else if (collision.gameObject.tag == "Ice")
            {
                var slideForce = new Vector2(_rb.velocity.x, 0);

                PlayerController.Instance.TeleportToBlade(slideForce, _rb.velocity);

                return;
            }
            else
            {
                PlayerController.Instance.TeleportToBlade(Vector3.zero, _rb.velocity);

                return;
            }
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 30)
        {
            if (collision.gameObject.tag == "Fan")
            {
                var fanController = collision.GetComponent<FanController>();

                switch (fanController.Direction)
                {
                    case Direction.NONE:
                        break;
                    case Direction.UP:
                        _rb.AddForce(Vector3.up / 2, ForceMode2D.Impulse);
                        break;
                    case Direction.RIGHT:
                        _rb.AddForce(Vector3.right / 2, ForceMode2D.Impulse);
                        break;
                    case Direction.DOWN:
                        _rb.AddForce(Vector3.down / 2, ForceMode2D.Impulse);
                        break;
                    case Direction.LEFT:
                        _rb.AddForce(Vector3.left / 2, ForceMode2D.Impulse);
                        break;
                }
            }
        }
    }
}
