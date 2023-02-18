using RiaShooter.Scripts.Enemies;
using UnityEditor;
using UnityEngine;

namespace RiaShooter.Scripts.EditorScripts
{
    [CustomEditor(typeof(Enemy), true)]
    public class EnemyEditor : Editor
    {
        public void OnSceneGUI()
        {
            var t = target as Enemy;
            var tr = t.transform;

            Handles.color = Color.red;
            Handles.DrawWireDisc(tr.position, tr.up, t.EnemyConfig.FireRadius);
            GUI.color = Color.red;
            Handles.Label(tr.position + Vector3.back * t.EnemyConfig.FireRadius, t.EnemyConfig.FireRadius.ToString("F1"));

            Handles.color = new Color(1, 0.8f, 0.4f, 1);
            Handles.DrawWireDisc(tr.position, tr.up, t.EnemyConfig.DetectRadius);
            GUI.color = new Color(1, 0.8f, 0.4f, 1);
            Handles.Label(tr.position + Vector3.back * t.EnemyConfig.DetectRadius, t.EnemyConfig.DetectRadius.ToString("F1"));
        }
    }
}