using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class XyzScript : MonoBehaviour
{
    private class SensorData
    {
        public float targetX;
        public float targetY;
        public float targetZ;
        public float rotationSpeedX; 
        public float rotationSpeedY;
        public float rotationSpeedZ;
    }

    private List<SensorData> sensorDataPool = new List<SensorData>();
    private Queue<SensorData> dataQueue = new Queue<SensorData>();

    private int currentDataIndex = 0;

    void Start()
    {
        string filePath = "Assets/exceldata.csv";
        string[] lines = File.ReadAllLines(filePath);

        Debug.Log($"Total data lines in the file: {lines.Length}");

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            SensorData data = new SensorData
            {
                targetX = float.Parse(values[10]),
                targetY = float.Parse(values[12]),
                targetZ = float.Parse(values[11]),
                rotationSpeedX = float.Parse(values[7]),
                rotationSpeedY = float.Parse(values[9]),
                rotationSpeedZ = float.Parse(values[8]),
            };

            sensorDataPool.Add(data);
        }

        foreach (var data in sensorDataPool)
        {
            dataQueue.Enqueue(data);
        }

        StartCoroutine(UpdateRotation());
    }

    IEnumerator UpdateRotation()
    {
        while (dataQueue.Count > 0)
        {
            SensorData data = dataQueue.Dequeue();

            Quaternion currentRotation = transform.rotation;

            Quaternion targetRotation = Quaternion.Euler(data.targetX, data.targetY, data.targetZ);

            float elapsedTime = 0f;
            float rotationTime = 0.1f; 

            while (elapsedTime < rotationTime)
            {
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, elapsedTime / rotationTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Debug.Log($"TargetX: {data.targetX}, TargetY: {data.targetY}, TargetZ: {data.targetZ}");

            ResetAndRepoolData(data);
        }
    }

    void ResetAndRepoolData(SensorData data)
    {
        data.targetX = 0f;
        data.targetY = 0f;
        data.targetZ = 0f;
        data.rotationSpeedX = 0f;
        data.rotationSpeedY = 0f;
        data.rotationSpeedZ = 0f;

        sensorDataPool[currentDataIndex] = data;
        currentDataIndex = (currentDataIndex + 1) % sensorDataPool.Count;
    }
}
