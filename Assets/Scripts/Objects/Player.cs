using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Scripts.Objects
{
    public class Player : MonoBehaviour
    {
        public static event System.EventHandler PlayerShot;
        public static List<Player> Instances { get; private set; } = new List<Player>();

        #region fields
        [SerializeField]
        private float _speed;
        private float _velocity;
        [SerializeField]
        private Collider2D _hitBox;
        #endregion
        #region properties
        public Collider2D HitBox => _hitBox;
        public float Speed => _speed;
        public float Velocity { get; private set; }
        public Collider2D CollidedWall
        {
            get
            {
                var casts = new List<RaycastHit2D>();
                var flags = new ContactFilter2D();
                flags.NoFilter();

                int castUp = HitBox.Cast(Vector2.up, flags, casts, 0.1f);
                if (castUp < 1) HitBox.Cast(Vector2.down, flags, casts, 0.1f);

                return casts?.FirstOrDefault(cast => cast.collider.tag == "Wall").collider;
            }
        }

        #endregion
        #region unity
#pragma warning disable IDE0051
        private void Start() => Instances.Add(this);
        private void OnDestroy() => Instances.Remove(this);
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

            Velocity = _speed * Time.deltaTime;
            transform.Translate(direction * Velocity);
        }
        public void Shoot()
        {
            PlayerShot.Invoke(this, System.EventArgs.Empty);
        }
        #endregion
    }
}