using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class writeCSV : MonoBehaviour
{
    private CsvFileWriter writer;
    private List<string> columns;
    private List<string> timeLine;

    // Start is called before the first frame update
    void Start()
    {
        columns = new List<string>() { "Time" }; //컬럼
        timeLine = new List<string>();

        setFilePath();
    }

    private void setFilePath()
    {
        var dataPath = Application.persistentDataPath + "/Data/";

        Debug.Log("Path_csv : " + dataPath);

        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);
        if (!Directory.Exists(dataPath + "BT"))
            Directory.CreateDirectory(dataPath + "BT");

        writer = new CsvFileWriter(dataPath + "BT/Result" + System.DateTime.Now.ToString("yyyyMMddHHmm") + ".csv");
        writer.WriteRow(columns);
        columns.Clear();
    }

    //버튼 입력 발생할때마다 시간기록
    //time은 여기말고 테스트씬이 있는 곳에서 지나간 시간
    public void writeTime(float min, float sec)
    {
        string s = min + ":" + sec;
        timeLine.Add(s);
    }

    //기록들을 csv파일에 적기
    public void writeTimeLine()
    {
        writer.WriteRow(timeLine);
    }
}
