using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.ScriptableObjects;
using SwapGame.EntityComponents;


namespace SwapGame.GameManagement
{
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private int _maxPossibleProjectiles;

        [SerializeField] public List<Projectile> _pooledProjectiles;

        private static ProjectileManager _instance;
        public static ProjectileManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            for (int i = 0; i < _maxPossibleProjectiles; i++)
            {
                GameObject temp;
                temp = Instantiate(_projectilePrefab);
                temp.SetActive(false);
                _pooledProjectiles.Add(temp.GetComponent<Projectile>());
            }
        }

        public void SpawnProjectile(Vector2 origin, Vector2 direction, ProjectileData projectileData, GameObject projectileOwner)
        {

            Projectile newProjectile = GetPooledProjectile();

            if (newProjectile == null)
            {
                Debug.Log("No projectiles available");
                return;
            }

            newProjectile.transform.position = origin;
            newProjectile.InitializeProjectile(projectileData, projectileOwner);
            newProjectile.RotateProjectile(direction);

            newProjectile.gameObject.SetActive(true);
        }

        private Projectile GetPooledProjectile()
        {
            for (int i = 0; i < _maxPossibleProjectiles; i++)
            {
                if (!_pooledProjectiles[i].gameObject.activeInHierarchy)
                {
                    return _pooledProjectiles[i];
                }
            }

            return null;
        }

        public void ClearAllProjectiles()
        {
            for (int i = 0; i < _maxPossibleProjectiles; i++)
            {
                _pooledProjectiles[i].gameObject.SetActive(false);
            }
        }
    }
}
