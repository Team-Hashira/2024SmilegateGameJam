using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gondr.Astar
{
    public class AstarMapManager : MonoSingleton<AstarMapManager>
    {
        [SerializeField]
        private LayerMask _whatIsObstacle;
        [SerializeField] private Tilemap _floorTile, _collisionTile;

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
            return _collisionTile.GetTile(pos) == null;
        }

    }
}

