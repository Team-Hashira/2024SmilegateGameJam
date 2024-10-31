using NavMeshPlus.Components;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField] private NavMeshSurface _navMeshSurface;
}
