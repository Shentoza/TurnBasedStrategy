using UnityEngine;
using System.Collections;
using System;

public class DijkstraSystem : MonoBehaviour {

    public BattlefieldCreater battleField;
    public Material begebarMat;
    public Material attackableMat;
    public Material defaultMat;

    CellComparer comp;

    ArrayList entdeckteZellen;

	// Use this for initialization
	void Start () {
        entdeckteZellen = new ArrayList();
        comp = new CellComparer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void executeDijsktra(Cell start, int moveRange,int attackRange)
    {
        resetDijkstra();

        start.dij_GesamtKosten = 0;
        entdeckteZellen.Add(start);

        while(entdeckteZellen.Count != 0)
        {
            entdeckteZellen.Sort(comp);
            Cell currentCell = (Cell)entdeckteZellen[entdeckteZellen.Count-1];
            entdeckteZellen.RemoveAt(entdeckteZellen.Count-1);
            currentCell.dij_ZellZustand = Cell.dij_Zustand.DIJ_ABGESCHLOSSEN;
            colorCell(currentCell,moveRange, attackRange);
            if (currentCell.dij_GesamtKosten > moveRange + attackRange)
                return;
            for(int i = 0; i < currentCell.neighbours.Count; ++i)
            {
                Cell currentNeighbour = (Cell)currentCell.neighbours[i];
                if (currentNeighbour.dij_ZellZustand != Cell.dij_Zustand.DIJ_ABGESCHLOSSEN)
                {
                    updateDistance(currentNeighbour, currentCell);
                }
            }
        }
    }

    public ArrayList getPath(Cell startKnoten, Cell zielKnoten)
    {
        ArrayList result = new ArrayList();
        Cell currentNode = zielKnoten;
        while(currentNode != startKnoten)
        {
            result.Add(currentNode);
            currentNode = currentNode.dij_Vorgaenger;
        }
        return result;
    }

    void updateDistance(Cell zielKnoten, Cell vorgaenger)
    {
        if(zielKnoten.isOccupied || zielKnoten.setOnFire)
        {
            zielKnoten.dij_ZellZustand = Cell.dij_Zustand.DIJ_ABGESCHLOSSEN;
            zielKnoten.dij_Vorgaenger = null;
            return;
        }
        if((vorgaenger.dij_GesamtKosten + zielKnoten.dij_KnotenKosten) < zielKnoten.dij_GesamtKosten)
        {
            zielKnoten.dij_Vorgaenger = vorgaenger;
            zielKnoten.dij_GesamtKosten = vorgaenger.dij_GesamtKosten + zielKnoten.dij_KnotenKosten;
            zielKnoten.dij_ZellZustand = Cell.dij_Zustand.DIJ_ENTDECKT;
            entdeckteZellen.Add(zielKnoten);
        }
    }

    void colorCell(Cell cell,int moveRange, int attackRange)
    {
        MeshRenderer meshRend = (MeshRenderer)cell.gameObject.GetComponent(typeof(MeshRenderer));
        if (cell.dij_GesamtKosten <= moveRange)
            meshRend.material = begebarMat;
        else if (cell.dij_GesamtKosten <= moveRange + attackRange)
            meshRend.material = attackableMat;

    }

    public void colorAllCells(bool reset, int moveRange, int attackRange)
    {
        for (int i = 0; i < (battleField.sizeX * 10); ++i)
            for (int j = 0; j < (battleField.sizeZ * 10); ++j)
            {
                Cell currentCell = battleField.getCell(i, j);
                if (reset)
                {
                    MeshRenderer meshRend = (MeshRenderer)currentCell.gameObject.GetComponent(typeof(MeshRenderer));
                    meshRend.material = defaultMat;
                }
                else
                {
                    colorCell(currentCell, moveRange, attackRange);
                }
            }
    }

    public void resetDijkstra()
    {
        entdeckteZellen.Clear();
        for(int i = 0; i < (battleField.sizeX*10);++i)
            for(int j = 0; j < (battleField.sizeZ*10);++j)
            {
                Cell currentCell = battleField.getCell(i, j);
                MeshRenderer meshRend = (MeshRenderer)currentCell.gameObject.GetComponent(typeof(MeshRenderer));
                meshRend.material = defaultMat;
                currentCell.dij_ZellZustand = Cell.dij_Zustand.DIJ_UNBESUCHT;
                currentCell.dij_Vorgaenger = null;
                currentCell.dij_GesamtKosten = int.MaxValue;
             }
    }
}


//SORTS FROM HIGH TO LOW
public class CellComparer: IComparer
{
    int IComparer.Compare(object x, object y)
    {
        if(x.GetType() == y.GetType() && x.GetType() == typeof(Cell))
        {
            Cell xCell = (Cell)x;
            Cell yCell = (Cell)y;

            if (xCell.dij_GesamtKosten == yCell.dij_GesamtKosten)
                return 0;
            if (xCell.dij_GesamtKosten < yCell.dij_GesamtKosten)
                return 1;
            return -1;
        }
        return 0;
    }
}