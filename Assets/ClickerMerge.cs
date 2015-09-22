using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickerMerge
{
    public string Type;
    public List<ClickGenerator> Clickers = new List<ClickGenerator>();

    public ClickerMerge(string type, List<ClickGenerator> clickers)
    {
        Type = type;
        AddClickers(clickers);
    }

    public Vector3 CenterPoint
    {
        get
        {
            var summedPositions = new Vector3();
            foreach (var clicker in Clickers)
            {
                summedPositions += clicker.transform.position;
            }
            return summedPositions/Clickers.Count;
        }
    }

    public void AddClickers(List<ClickGenerator> clickers)
    {
        Clickers.AddRange(clickers.Except(Clickers));
        clickers.ForEach(x => x.Merge(this));
    }

    public void Remove(ClickGenerator clicker)
    {
        Clickers.Remove(clicker);
    }
}