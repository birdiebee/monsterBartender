namespace Examples.LoadLevel1
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    using System.Collections.Generic;

    // I'm adding a comment to this script,  and submitting this change to the server.
    // This is a comment from an in class example.
    public class LoadLevel : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private string _activeScene = string.Empty;
        [SerializeField] private List<string> _scenePaths = new List<string>();

        private bool _isLoaded = false;

        private void Update()
        {
            Debug.Log("daniel wuz here");
            Debug.Log("Here's my in class example.");
            Debug.Log("This is likely going to cause a merge conflict.");

            if (Input.GetKeyDown(KeyCode.U))
            {
                Unload();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            // Monitor the active scene and ensure it's properly set.
            Scene desiredActiveScene = SceneManager.GetSceneByPath(_activeScene);
            if (desiredActiveScene.isLoaded && desiredActiveScene != SceneManager.GetActiveScene())
            {
                SceneManager.SetActiveScene(desiredActiveScene);
            }
        }

        public void Load()
        {
            if (_isLoaded)
            {
                return;
            }

            foreach (string scenePath in _scenePaths)
            {
                SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            }

            _isLoaded = true;
        }

        public void Unload()
        {
            if (!_isLoaded)
            {
                return;
            }

            foreach (string scenePath in _scenePaths)
            {
                SceneManager.UnloadSceneAsync(scenePath);
            }

            _isLoaded = false;
        }
    }
}
