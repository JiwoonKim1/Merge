using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvReader : MonoBehaviour
{
    public static List<List<string>> Read(string path)
    {
        var lst = new List<List<string>>();

        var reader = new StreamReader(path);

        var line = "";
        while ((line = reader.ReadLine()) != null)
        {
            var split = line.Split(',');
            var splitList = new List<string>();

            foreach (var data in split)
            {
                splitList.Add(data);
            }

            lst.Add(splitList);
        }

        reader.Close();

        return lst;
    }
}