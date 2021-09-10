using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Player : MonoBehaviour
{
    public static event System.EventHandler PlayerShot;
    public static List<Player> Instances { get; private set; }

    #region fields
    private Rigidbody2D _body;
    [SerializeField]
    private readonly float _speed;
    [SerializeField]
    private Collider2D _hitBox;
    #endregion
    #region properties
    public Rigidbody2D Body => _body;
    public Collider2D HitBox => _hitBox;
    public float Speed => _speed;
    public Collider2D CollidedWall
    {
        get
        {
            var casts = new List<RaycastHit2D>();
            int castUp = Body.Cast(Vector2.up, new ContactFilter2D(), casts, 0.1f);
            if (castUp < 1) Body.Cast(Vector2.down, new ContactFilter2D(), casts, 0.1f);

            return casts?.FirstOrDefault(cast => cast.collider.tag == "Wall").collider;
        }
    }

    #endregion
    #region unity
#pragma warning disable IDE0051 
    private void Start()
    {
        Instances.Add(this);
        _body = GetComponent<Rigidbody2D>();
    }
    private void OnDestroy()
    {
        Instances.Remove(this);
    }
#pragma warning restore IDE0051 
    #endregion
    #region methods
    public void Move(Vector2 direction)
    {
        switch (CollidedWall?.name)
        {
            case "Up":
                if (direction.y > 0) return;
                break;
            case "Down":
                if (direction.y < 0) return;
                break;
        }

        float offset = _speed * Time.deltaTime;
        _body.transform.Translate(direction * offset);
    }
    public void Shoot()
    {
        PlayerShot.Invoke(this, System.EventArgs.Empty);
    }
    #endregion
}
