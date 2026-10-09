using UnityEngine;

public class GridManagerScript : MonoBehaviour
{
    const int grid_height = 4;
    const int grid_width = 6;
    const float grid_spacing_const = 1.1f;
    const float initial_grid_x_coord = -3f;
    const float initial_grid_y_coord = -2f;

    public GameObject[,] Grid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Grid = new GameObject[grid_height, grid_width];

        for (int h = 0; h < grid_height; h++)
        {
            for (int w = 0; w < grid_width; w++)
            {
                Grid[h,w] = Instantiate(Resources.Load<GameObject>("GridCell"), new Vector2(w * grid_spacing_const + initial_grid_x_coord, h * grid_spacing_const + initial_grid_y_coord), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
