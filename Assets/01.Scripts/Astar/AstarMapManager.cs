using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gondr.Astar
{
    public class AstarMapManager : MonoBehaviour
    {
        public static AstarMapManager Instance;
        [SerializeField]
        private LayerMask _whatIsObstacle;
        [SerializeField] private Tilemap _floorTile, _collisionTile;
        private Collider2D[] _colliders = new Collider2D[100];
        private Dictionary<int, Vector2> _settleDictionary = new Dictionary<int, Vector2>();

        private void Awake()
        {
            Instance = this;
        }

        public void RegisterSettle(int instanceId, Vector2 pos)
        {
            _settleDictionary.Add(instanceId, pos);
        }

        public void UpdateSettle(int instanceId, Vector2 pos)
        {
            _settleDictionary[instanceId] = pos;
        }

        public Vector3Int GetTilePosition(Vector3 worldPosition)
        {
            return _floorTile.WorldToCell(worldPosition);
        }

        public Vector3 GetTileCenterWorld(Vector3Int tilePosition)
        {
            return _floorTile.GetCellCenterWorld(tilePosition);
        }

        public bool CanMove(Vector3Int pos, Unit owner)
        {
            TileBase tile = _floorTile.GetTile(pos);
            if (tile == null)
            {
                return false;
            }
            int count = Physics2D.OverlapBox(new Vector2(pos.x + 0.5f, pos.y + 0.5f), new Vector2(1f,1f), 0, new ContactFilter2D { layerMask = _whatIsObstacle, useLayerMask = true, useTriggers = true }, _colliders);
            if (count > 0)
            {
                foreach (Collider2D collider in _colliders)
                {
                    if (collider != null)
                    {
                        if (collider.transform != owner.settleTrm)
                        {
                            return false;
                        }
                    }
                }
            }

            return _collisionTile.GetTile(pos) == null;
        }

    }
}

