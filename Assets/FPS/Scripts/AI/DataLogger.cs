using System.IO;
using UnityEngine;

namespace Unity.FPS.AI
{
    public class DataLogger : MonoBehaviour
    {
        public static DataLogger Instance;

        private string logFilePath;
        int currentKills = 0;
        int currentDeaths = 0;

        void Awake()
        {
            // singleton guard: only one logger and persist across scenes
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Create a persistent log file path (unique per session)
            string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "UnityGameLogs");
            Directory.CreateDirectory(folderPath); // Make sure folder exists

            logFilePath = Path.Combine(folderPath, $"GameSession_{timestamp}.csv");

            // Write header line (overwrite any existing file for this session)
            File.WriteAllText(logFilePath, "Timestamp,Kills,Deaths\n");
            Debug.Log($"[DataLogger] Logging to: {logFilePath}");
        }

        // Keep existing API: when either counter updates, update internal totals and write a full row
        public void LogKill(int killCount)
        {
            // keep internal state in sync in case callers don't pass an absolute total
            currentKills = killCount;
            WriteFullRow();
            Debug.Log($"[DataLogger] Stats logged: Kills={currentKills}, Deaths={currentDeaths}");
        }

        public void LogDeath(int deathCount)
        {
            currentDeaths = deathCount;
            WriteFullRow();
            Debug.Log($"[DataLogger] Stats logged: Kills={currentKills}, Deaths={currentDeaths}");
        }

        void WriteFullRow()
        {
            string logEntry = $"{System.DateTime.Now:HH:mm:ss},{currentKills},{currentDeaths}\n";
            File.AppendAllText(logFilePath, logEntry);
        }

        public void LogSummary(string summary)
        {
            File.AppendAllText(logFilePath, $"SUMMARY: {summary}\n");
        }
    }
}
