using BaseTemplate.Behaviours;
using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] PlayerAnimatorController _playerAnimator;
    [SerializeField] LayerMask _isGroundedLayerMask;
    [SerializeField] Rigidbody2D _RB;
    [SerializeField] BoxCollider2D _collider;
    [SerializeField] CinemachineTargetGroup _targetGroup;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] GameObject _lastStandGO;
    public ParticleSystem _VFX;


    [SerializeField] float _bladeSpeed, _maxFallSpeed;

    Vector3 _lastStandPos;
    GameObject _lastBlade;
    bool _canCastBlade;

    public Rigidbody2D RB { get => _RB; }
    public float BladeSpeed { get => _bladeSpeed; }
    public bool CanCastBlade { get => _canCastBlade; set => _canCastBlade = value; }

    public void Init()
    {
        _spriteRenderer.enabled = false;
        _canCastBlade = false;
    }

    public void OnStartLevel()
    {
        _spriteRenderer.enabled = true;
        _canCastBlade = true;
    }

    public void OnEndLevel()
    {
        _spriteRenderer.enabled = false;
        _canCastBlade = false;
    }

    private void Update()
    {
        IsGrounded();
        IsWalled(Vector2.left);
        IsWalled(Vector2.right);

        if (IsGrounded() && _RB.velocity.y == 0)
        {
            _lastStandPos = transform.position;
            _lastStandGO.transform.position = _lastStandPos;
        }

        int indexLevel = (int)(transform.position.y / 5.5f);
    }

    public bool IsGrounded()
    {
        float extraHeightText = .05f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, extraHeightText, _isGroundedLayerMask);

        return raycastHit.collider != null;
    }

    public bool IsWalled(Vector2 direction)
    {
        float extraHeightText = .05f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, direction, extraHeightText, _isGroundedLayerMask);

        if (raycastHit.collider != null)
        {
            if (_RB.velocity.y < 0f) _RB.velocity = new Vector2(_RB.velocity.x, -1);

            return true;
        }
        else return false;
    }

    public void CastBlade(Vector2 direction)
    {
        if (_canCastBlade == false) return;

        _canCastBlade = false;

        _lastBlade = PoolManager.Instance.SpawnFromPool("Blade", transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = _lastBlade.GetComponent<Rigidbody2D>();

        _lastBlade.GetComponent<BladeController>().InitialForce = direction;

        projectileRigidbody.velocity = direction * _bladeSpeed;

        projectileRigidbody.velocity += _RB.velocity;

        _virtualCamera.Follow = _lastBlade.transform;
        _virtualCamera.LookAt = _lastBlade.transform;

        // _targetGroup.AddMember(_lastBlade.transform, 1, 0);
    }

    public void TeleportToBlade(Vector3 impulseOnHit, Vector3 bladeVelocity)
    {
        if (bladeVelocity.x < 0f) _playerAnimator.SetFlipX(true);
        else if (bladeVelocity.x > 0f) _playerAnimator.SetFlipX(false);

        _RB.velocity = Vector3.zero;
        _collider.enabled = false;

        _lastBlade.SetActive(false);

        transform.DOMove(_lastBlade.transform.position, .1f).OnComplete(() =>
        {
            // _targetGroup.RemoveMember(_lastBlade.transform);
            _virtualCamera.Follow = transform;
            _virtualCamera.LookAt = transform;

            _canCastBlade = true;
            _collider.enabled = true;

            if (impulseOnHit != Vector3.zero)
            {
                _RB.AddForce(impulseOnHit, ForceMode2D.Impulse);
            }
        });
    }

    public void ResetBlade()
    {
        _lastBlade.SetActive(false);

        //_targetGroup.RemoveMember(_lastBlade.transform);

        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;

        _canCastBlade = true;
    }

    public void GetKnockBack(Vector3 impulse)
    {
        _RB.velocity = Vector3.zero;

        int side = Random.Range(0, 2);

        if (impulse == Vector3.zero)
        {
            if (side == 0) impulse = new Vector3(Random.Range(-.5f, -.25f), 1, 0);
            else { impulse = new Vector3(Random.Range(.25f, .5f), 1, 0); }
        }

        _RB.AddForce(impulse * 3, ForceMode2D.Impulse);
    }

    public void GetHit()
    {
        GetKnockBack(Vector3.zero);

        _playerAnimator.PlayerHit();

        TimeManager.Instance.DoLagTime();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 30)
        {
            if (collision.gameObject.tag == "Spike")
            {
                _collider.enabled = false;

                transform.DOMove(_lastStandPos, .1f).OnComplete(() =>
                {
                    _collider.enabled = true;
                });
            }

            if (collision.gameObject.tag == "Jumper")
            {

                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    GetKnockBack(new Vector3(Random.Range(-.5f, -.25f), 1, 0));
                }
                else
                {
                    GetKnockBack(new Vector3(Random.Range(.25f, .5f), 1, 0));
                }
            }
        }
    }
}
