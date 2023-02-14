using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] GameObject cellPref;

    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        GeneratedCell[,] maze = generator.Generate();
        for(int x = 0; x<generator.width;x++)
        {
            for(int y = 0; y<generator.height;y++)
            {
                Cell  cell = Instantiate(
                    cellPref,
                    new Vector2(x,y),
                    Quaternion.identity).GetComponent<Cell>();
                cell.wallLeft.SetActive(maze[x,y].wallLeft);
                cell.wallBottom.SetActive(maze[x,y].wallBottom);
                cell.transform.SetParent(transform);
            }
        }

        MeshComposer meshComposer = GetComponent<MeshComposer>();
        meshComposer.Compose();
    }
}
