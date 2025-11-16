using System.IO;
using UnityEngine;

namespace Unity.FPS.Logging
{
    public class SpawnLogger : MonoBehaviour
    {
        public static SpawnLogger Instance;

        private string logFilePath;

        void Awake()
        {
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

            string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "UnityGameLogs");
            Directory.CreateDirectory(folderPath);

            logFilePath = Path.Combine(folderPath, $"PositionLog_{timestamp}.csv");
            File.WriteAllText(logFilePath, "Timestamp,Event,PlayerX,PlayerY,PlayerZ,EnemyX,EnemyY,EnemyZ\n");
        }

        public void LogEventWithCoords(string eventType, Vector3 playerPos, Vector3 enemyPos)
        {
            string logEntry = $"{System.DateTime.Now:HH:mm:ss.fff},{eventType},{playerPos.x:F2},{playerPos.y:F2},{playerPos.z:F2},{enemyPos.x:F2},{enemyPos.y:F2},{enemyPos.z:F2}\n";
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}