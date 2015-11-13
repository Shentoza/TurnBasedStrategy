using UnityEngine;
using System.Collections;

public class DijkstraSystem : MonoBehaviour {

    public BattlefieldCreater battleField;
    public Material begebarMat;
    public Material attackableMat;
    int range;
    int attackRange;
    ArrayList entdeckteZellen;

	// Use this for initialization
	void Start () {
        range = 3;
        attackRange = 1;
        entdeckteZellen = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void executeDijsktra(Cell start)
    {
        resetDijkstra();
        entdeckteZellen.Clear();
        start.dij_GesamtKosten = 0;
        entdeckteZellen.Add(start);

        while(entdeckteZellen.Count != 0)
        {
            int lastIndex = entdeckteZellen.Count - 1;
            Cell currentCell = (Cell)entdeckteZellen[lastIndex];
            entdeckteZellen.RemoveAt(lastIndex);
            currentCell.dij_ZellZustand = Cell.dij_Zustand.DIJ_ABGESCHLOSSEN;
            colorCell(currentCell);
            for(int i = 0; i < currentCell.neighbours.Count; ++i)
            {
                Cell currentNeighbour = (Cell)currentCell.neighbours[i];
                if (currentNeighbour.dij_ZellZustand != Cell.dij_Zustand.DIJ_ABGESCHLOSSEN);
                {
                    updateDistance(currentNeighbour, currentCell);
                }
            }
        }
    }

    void updateDistance(Cell zielKnoten, Cell vorgaenger)
    {
        if((vorgaenger.dij_GesamtKosten + zielKnoten.dij_KnotenKosten) < zielKnoten.dij_GesamtKosten)
        {
            zielKnoten.dij_Vorgaenger = vorgaenger;
            zielKnoten.dij_GesamtKosten = vorgaenger.dij_GesamtKosten + zielKnoten.dij_KnotenKosten;
            zielKnoten.dij_ZellZustand = Cell.dij_Zustand.DIJ_ENTDECKT;
            entdeckteZellen.Add(zielKnoten);
        }
    }

    void colorCell(Cell cell)
    {
        MeshRenderer meshRend = (MeshRenderer)cell.gameObject.GetComponent(typeof(MeshRenderer));
        if (cell.dij_GesamtKosten <= range)
            meshRend.material = begebarMat;
        else if (cell.dij_GesamtKosten <= range + attackRange)
            meshRend.material = attackableMat;

    }

    void resetDijkstra()
    {
        for(int i = 0; i < (battleField.sizeX*10);++i)
            for(int j = 0; j < (battleField.sizeZ*10);++j)
            {
                Cell currentCell = battleField.getCell(i, j);
                currentCell.dij_ZellZustand = Cell.dij_Zustand.DIJ_UNBESUCHT;
                currentCell.dij_Vorgaenger = null;
                currentCell.dij_GesamtKosten = int.MaxValue;
             }
    }
}