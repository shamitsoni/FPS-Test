using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    private string logFilePath;

    void Awake()
    {
        // Create a persistent log file path (unique per session)
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string folderPath = Path.Combine(desktopPath, "UnityGameLogs");
        Directory.CreateDirectory(folderPath); // Make sure folder exists

        logFilePath = Path.Combine(folderPath, $"GameSession_{timestamp}.csv");

        // Write header line
        File.WriteAllText(logFilePath, "Timestamp,Kills\n");
        Debug.Log($"[DataLogger] Logging to: {logFilePath}");
    }

    public void LogKill(int killCount)
    {
        string logEntry = $"{System.DateTime.Now:HH:mm:ss},{killCount}\n";
        File.AppendAllText(logFilePath, logEntry);
        Debug.Log($"[DataLogger] Kill logged: {killCount}");
    }

    public void LogSummary(string summary)
    {
        File.AppendAllText(logFilePath, $"SUMMARY: {summary}\n");
    }
}
