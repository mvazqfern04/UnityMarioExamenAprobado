using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.dat";
    private static int record = 0;
    private static List<int> records= new List<int>();

    public static void SaveRecord(int pooints)
    {
        if(records.Count == 0)
        {
            records = new List<int>();
            records.Add(0);
            
        }

        if(pooints > records[records.Count-1]||records.Count<=5)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            records.Add(pooints );
            records.Sort();
            records.Reverse();


            Debug.Log("LIMPIEEEEZA");
            for (int i = records.Count-1; i>4; i--)
            {
                records.Remove(i);
            }

            RecordData data = new RecordData(records);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static List<int> LoadRecord()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RecordData data = formatter.Deserialize(stream) as RecordData;  // equivalent to (RecordData)formatter.Deserialize(stream);
            stream.Close();

            records = data.GetRecords();
            
        } else
        {
            records = new List<int>();
            records[0] = 0;
        }
        return records;
    }
}
