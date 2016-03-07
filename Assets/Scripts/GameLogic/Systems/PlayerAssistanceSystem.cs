using UnityEngine;
using System.Collections;

public class PlayerAssistanceSystem : MonoBehaviour {

    bool drawingWalkPath;
    ArrayList walkPath = new ArrayList();
    bool drawingThrowPath;
    ArrayList throwPath = new ArrayList();

    public Material oneArrow;
    public Material startArrow;
    public Material endArrow;
    public Material middleArrow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*Todo: neuen pfad nur bei wechselndem Pfad zeichnen (aktuell jeder Frame neuer Pfad)
    *irgendwelche fckn nullpointer fliegen
    */
    public void PaintWalkPath(ArrayList path)
    {
        ClearDraws(false, true);
        MeshRenderer meshr;
        for(int i = 0; i < path.Count;++i)
        {
            Cell currentCell = (Cell)path[i];
            GameObject currentArrow = Instantiate(currentCell.gameObject);
            meshr = (MeshRenderer)currentArrow.GetComponent(typeof(MeshRenderer));
            Destroy(currentArrow.GetComponent(typeof(Cell)));
            Destroy(currentArrow.GetComponent(typeof(BoxCollider)));

            meshr.material = startArrow;

            
        }
    }

    void ClearDraws(bool resetThrowPath, bool resetWalkPath)
    {
        if (resetThrowPath)
            for (int j = 0; j < throwPath.Count; ++j)
            {
                GameObject current = (GameObject)throwPath[j];
                Destroy(current);
            }


        if(resetWalkPath)
            for (int i = 0; i < walkPath.Count; ++i)
            {
                GameObject current = (GameObject)walkPath[i];
                Destroy(current);
            }
    }
}
