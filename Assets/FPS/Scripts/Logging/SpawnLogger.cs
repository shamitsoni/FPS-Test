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

            logFilePath = Path.Combine(folderPath, $"SpawnLog_{timestamp}.csv");
            File.WriteAllText(logFilePath, "Timestamp,Type,Position\n");
        }

        public void LogSpawn(string type, Vector3 position)
        {
            string logEntry = $"{System.DateTime.Now:HH:mm:ss},{type},{position.x:F2},{position.y:F2},{position.z:F2}\n";
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}