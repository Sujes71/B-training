using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileIndex
{
    public List<string> profileFileNames;
  
    public ProfileIndex()
    {
        this.profileFileNames = new List<string>();
    }
    public int GetNumOfElements()
    {
        return this.profileFileNames.Count;
    }
}
