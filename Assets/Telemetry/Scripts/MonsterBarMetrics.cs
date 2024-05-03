namespace TelemetryManagerExamples
{
    using USCG.Core.Telemetry;

    using UnityEngine;

    public class MonsterBarMetrics : MonoBehaviour
    {
        // Keep a reference to the metrics we create.
        private MetricId _deathCountMetric = default;
        private MetricId _wrongDrinkMetric = default;
        private MetricId _timeSurvivedMetric = default;

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

        public void StartSurvivalTimer()
        {

        }

        private void Update()
        {
            // There are a number of places where you can call these functions. This example only illustrates how you record metrics,
            // but it's up to you to decide when you want to record your metrics!

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // This will increment the provided metric by 1.
                TelemetryManager.instance.AccumulateMetric(_deathCountMetric, 1);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                // This will add a new sample where the value is a random vector.
                //TelemetryManager.instance.AddMetricSample(_randomVectorMetric, new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
            }

            // If you uncomment this line you'll see a sample get created for every frame. This may be desirable in some cases, but
            // you likely want a better way to collect samples (e.g. responding to an event).
            //TelemetryManager.instance.AddMetricSample(_deltaTimeMetric, Time.deltaTime);
        }
    }
}