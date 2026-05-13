using System.Collections.Generic;

namespace Mozo.Model;

public class FileMergeModel
{
    private static FileMergeModel instance;
    private readonly List<string> FileList;

    private FileMergeModel()
    {
        try
        {
            FileList = new List<string>();
        }
        catch
        {
        }
    }

    public static FileMergeModel Instance
    {
        get
        {
            if (instance == null)
                instance = new FileMergeModel();
            return instance;
        }
    }

    public void AddFile(string BaseFileName)
    {
        FileList.Add(BaseFileName);
    }

    public bool InUse(string BaseFileName)
    {
        return FileList.Contains(BaseFileName);
    }

    public bool RemoveFile(string BaseFileName)
    {
        return FileList.Remove(BaseFileName);
    }

    public int Count()
    {
        return FileList.Count;
    }


    public void RemoveAll()
    {
        FileList.Clear();
    }
}