namespace Examples.Coroutines
{
    using System.Collections;
    using UnityEngine.SceneManagement;
    using System.Collections.Generic;
    using UnityEngine;

    public class CoroutineExample : MonoBehaviour
    {
        [SerializeField] private string _activeScenePath = string.Empty;
        [SerializeField] private List<string> _scenePath = new List<string>();

        private bool _isLoaded = false;
        private bool _isProcessing = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(IELoadLevel());
            }
        }

        private class CustomYeildInstruction : CustomYieldInstruction
        {
            public override bool keepWaiting => throw new System.NotImplementedException();
        }

        public IEnumerator IELoadLevel()
        {
            if (_isLoaded)
            {
                yield break;
            }

            if (_isProcessing)
            {
                yield break;
            }

            _isProcessing = true;

            foreach (string scenePath in _scenePath)
            {
                yield return SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(_activeScenePath));

            _isLoaded = true;
            _isProcessing = false;
        }

        public void Start()
        {
            StartCoroutine(IECountdown());
        }
        public IEnumerator IECountdown()
        {
            float timer = 3.0f;
            while (timer > 0.0f)
            {
                Debug.Log($"Timer:  {timer}");
                yield return new WaitForSeconds(1.0f);
                timer -= 1.0f;
            }
            Debug.Log("Done");
        }
    }
}
