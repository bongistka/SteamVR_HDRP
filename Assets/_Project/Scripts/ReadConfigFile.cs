using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadConfigFile : MonoBehaviour
{
    public MriSequence mriSequence;

    // Start is called before the first frame update
    void Start()
    {
        ReadTextFile(Application.dataPath + "/../Configs/" + "default.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path, System.Text.Encoding.UTF8);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            if (inp_ln.Contains("#BedHeight: "))
            {
                float bedHeight = float.Parse(inp_ln.Replace("#BedHeight: ", ""));
                if (bedHeight < 1.041462)
                    mriSequence.bedStart = bedHeight;
            }
        }
        inp_stm.Close();
    }
}
