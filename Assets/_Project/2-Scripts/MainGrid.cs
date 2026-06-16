using UnityEngine;

public class MainGrid : MonoBehaviour
{
    [SerializeField] private GameObject gridStart;
    [SerializeField] public GameObject gridEnd;

    [SerializeField] public int gridSize;
    [SerializeField] private float gizmoSize;


    public bool drawGrid = true;
    

    public Vector3 GetColumnLocation(int columnNumber)
    {
        //get grid starting point, then offset to know ho much is a column adn then mark the end position of the column
        Vector3 startPosition = gridStart.transform.position;
        Vector3 endPosition = gridEnd.transform.position;

        float columnWidth = (endPosition.x - startPosition.x) / (gridSize - 1);
        Vector3 columnLocation = startPosition;
        columnLocation.x += (columnNumber * columnWidth);

        return columnLocation;

    }


    private void OnDrawGizmos()
    {
        if (!drawGrid || !gridStart || !gridEnd)  return;
        
        Gizmos.color = Color.blue;
        
        for (int i = 0; i < gridSize; i++) // loop through each column
        {
            Vector3 columnLocation = GetColumnLocation(i);
            Gizmos.DrawWireSphere(columnLocation, gizmoSize);
        }
        
        
    }
    
}
