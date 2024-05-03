namespace TelemetryManagerExamples
{
    using USCG.Core.Telemetry;

    using UnityEngine;

    public class RecordMetricsExample : MonoBehaviour
    {
        // Keep a reference to the metrics we create.
        private MetricId _deathCountMetric = default;
        private MetricId _wrongDrinkMetric = default;
        private MetricId _timeSurvivedMetric = default;
        private float timeSurvived = 0.0f;
        bool timerStarted = false;

        private void Awake()
        {
                DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            // Create all metrics in Start().
            _deathCountMetric = TelemetryManager.instance.CreateAccumulatedMetric("timesKilled");
            _wrongDrinkMetric = TelemetryManager.instance.CreateAccumulatedMetric("wrongDrinkServed");
            _timeSurvivedMetric = TelemetryManager.instance.CreateSampledMetric<float>("timeSurvived");
        }

        public void Killed()
        {
            TelemetryManager.instance.AccumulateMetric(_deathCountMetric, 1);
        }

        public void Wrong()
        {
            TelemetryManager.instance.AccumulateMetric(_wrongDrinkMetric, 1);
        }

        public void StartTimer()
        {
            timerStarted = true;
        }

        public void EndTimer()
        {
            timerStarted = false;
            Debug.Log(timeSurvived);
            TelemetryManager.instance.AddMetricSample(_timeSurvivedMetric, timeSurvived);
            timeSurvived = 0.0f;
        }
        private void Update()
        {
            if(timerStarted)
            {
                timeSurvived += Time.deltaTime;
            }
        }
    }
}