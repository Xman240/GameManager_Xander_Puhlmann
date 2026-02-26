using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GhostData
{
    public List<GhostDataFrame> ghostDataFrames = new List<GhostDataFrame>();
}

[Serializable]
public class GhostDataFrame
{
    public Vector3 position;
    public Vector3 rotation;

    public GhostDataFrame(Vector3 position_, Vector3 rotation_)
    {
        this.position = position_;
        this.rotation = rotation_;
    }
}
