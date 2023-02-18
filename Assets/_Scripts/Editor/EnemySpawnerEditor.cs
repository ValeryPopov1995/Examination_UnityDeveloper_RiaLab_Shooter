using RiaShooter.Scripts.Level;
using UnityEditor;
using UnityEngine;

namespace RiaShooter.Scripts.EditorScripts
{
    [CustomEditor(typeof(EnemySpawner), true)]
    internal class EnemySpawnerEditor : Editor
    {
        public void OnSceneGUI()
        {
            var t = target as EnemySpawner;
            var tr = t.transform;
            
            var color = Color.red;
            Handles.color = color;
            GUI.color = color;

            Handles.DrawWireDisc(tr.position, tr.up, t.SpawnRadius);
            Handles.Label(tr.position + Vector3.back * t.SpawnRadius, t.SpawnRadius.ToString("F1"));
        }
    }
}