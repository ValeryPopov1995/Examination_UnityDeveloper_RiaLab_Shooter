using RiaShooter.Scripts.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RiaShooter.Scripts.Level
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private View _blackoutView;
        [SerializeField] private float _loadDelay = 1.1f;

        public async void LoadSceneAsync(int buildIndex)
        {
            _blackoutView?.Show();
            await Task.Delay(TimeSpan.FromSeconds(_loadDelay));
            SceneManager.LoadSceneAsync(buildIndex);
        }
    }
}