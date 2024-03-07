using System.Collections.Generic;
using UnityEditor.UIElements;

[System.Serializable]
public class RecordData
{
   int record;
   List<int> records= new List<int>();
    int limitRecord = 5;

    public RecordData(List<int> records)
    {
        this.records =records;
    }

    public List<int> GetRecords()
    {
        return records;
    }

    public void SetRecord(List<int> records)
    {
        this.records = records;
    }
}
