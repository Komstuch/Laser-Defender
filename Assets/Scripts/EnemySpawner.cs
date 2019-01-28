using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemySpawner : MonoBehaviour {

    [Header("Wave Configuration")]
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = true;

    private FormationController bossFormation;
    private AnalyticsManager analyticsManager;
    private int cyclesCompleted;
    private float speedMultiplier = 0f;
    private float healthMultiplier = 0f;
    private float damageMultiplier = 0f;
    private float scoreMultiplier = 0f;

    [Header("Boss UI")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] BossText bossText;

    IEnumerator Start () {
        analyticsManager = FindObjectOfType<AnalyticsManager>();
        bossFormation = FindObjectOfType<FormationController>();
        cyclesCompleted = 0;

        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
	}

    private IEnumerator SpawnAllWaves()
    {
        Analytics.CustomEvent("Cycle " + cyclesCompleted.ToString());

        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave)); //Nested Coroutines
        }
        cyclesCompleted++;
        ActivateHealthBar();
        yield return StartCoroutine(bossFormation.StartBossFight());
        DeactivateHealthBar();
        IncreaseDifficulty();
    }
	
	private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetwenSpawns());
        }
        analyticsManager.AddWavesCompleted();
    }

    public void DeactivateHealthBar()
    {
        healthBar.transform.parent.gameObject.SetActive(false);
        bossText.gameObject.SetActive(false);
    }

    private void ActivateHealthBar()
    {
        healthBar.transform.parent.gameObject.SetActive(true);
        bossText.gameObject.SetActive(true);
    }

    private void IncreaseDifficulty()
    {
        speedMultiplier += 0.1f;
        healthMultiplier += 0.2f;
        damageMultiplier += 0.1f;
        scoreMultiplier += 0.2f;
}

    public float GetDamageMultiplier() { return damageMultiplier; }
    public float GetHealthMultiplier() { return healthMultiplier; }
    public float GetSpeedMultiplier() { return speedMultiplier; }
    public float GetScoreMultiplier() { return scoreMultiplier; }
}
