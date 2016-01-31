using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UnitScreenMP : MonoBehaviour {


    public int unitIconWidth;
    public int unitIconHeight;
    public int unitListXAnkerP1 = 50;
    public int unitListXAnkerP2 = -1;
    public int unitListYAnker = 50;  
    public int borderWidth = 2;


    public Texture2D backgroundTexture;
    public Texture2D unitListBackground;

    public Texture2D newUnitButton;


    public List<Texture2D> weapons, util;
    public Texture2D unit1;
    public Texture2D unit2;
    public Texture2D unit3;
    public Texture2D unit4;

    Vector4 equip = new Vector4(0,0,0,0);
    List<Vector4> unitsListP1 = new List<Vector4>();
    List<int> unitsListP2 = new List<int>();


    int xAnkerP1;
    int yAnkerP1;

    //dropdown
    int c = 0;
    bool dp1, dp2, dp3, dp4;

    //picking abfolge
    bool player1Picking; 


	// Use this for initialization
	void Start () {

        if (unitListXAnkerP2 == -1)
        {
            unitListXAnkerP2 = Screen.width - unitListXAnkerP1 - unitIconWidth - 2 * borderWidth;
        }
       
        
	}
	
	// Update is called once per frame
	void Update () {
        

        if (unitsListP1.Count > unitsListP2.Count)
        {
            player1Picking = false;
        }
        else
        {
            player1Picking = true;
        }



        dropdownUpdate();
	}


    void  p1Pick(){
        if (player1Picking)
        {
            unitsListP1.Add(equip);
            equip = new Vector4(0, 0, 0, 0);
        }
    }

    void p2Pick(int i)
    {
        if (!player1Picking)
        {
            unitsListP2.Add(i);
        } 
    }

    void OnGUI()
    {
        //erstelle hintergrund
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        //rebellen
        drawP1();

        //staat
       drawP2();

    }


    //rebellen
    void drawP1()
    {
        int unitListXAnker = unitListXAnkerP1;

        //einheitenlisten hintergrund
        GUI.DrawTexture(new Rect(unitListXAnker, unitListYAnker, unitIconWidth + 2 * borderWidth, (int)(Screen.height * 0.8f)), unitListBackground);


        //einheitenliste
        xAnkerP1 = unitListXAnker + borderWidth;
        yAnkerP1 = unitListYAnker;
        drawUnitList();


        //add unit button
        if (GUI.Button(new Rect(unitListXAnker, unitListYAnker + (int)(Screen.height * 0.8f) + 5, unitIconWidth, unitIconHeight), newUnitButton))
        {
                p1Pick();        
        }


        //ausrüstungsbuttons auswahlbuttons
        drawEquipButtons();
        drawDropdown();

    
    }


    void drawDropdown()
    {
        int xBase = 0;
        int yBase = 0;
        int draw = 0;

        int optionSize = 50;

        if (dp1 == true)
        {
            xBase = 502;
            yBase = 125;
            draw = 1;
        }
        else if (dp2 == true)
        {
            xBase = 502;
            yBase = 175;
            draw = 1;
        }
        else if (dp3 == true)
        {
            xBase = 502;
            yBase = 300;
            draw = 2;
        }
        else if (dp4 == true)
        {
            xBase = 502;
            yBase = 425;
            draw = 2;
        }

        if (draw == 0)
        {
        }
        else if (draw == 1)
        {
            yBase -= (weapons.Count * optionSize / 2);
            for (int i = 0; i < weapons.Count; i++)
            {
                if (GUI.Button(new Rect(xBase, yBase + i * optionSize, optionSize, optionSize), weapons[i]))
                {
                    if (dp1)
                    {
                        equip.x = i;
                        dp1 = false;
                    }
                    else if (dp2)
                    {
                        equip.y =  i;
                        dp2 = false;
                    }

                }
            }
        }
        else if (draw == 2)
        {
            yBase -= (weapons.Count * optionSize / 2);
            for (int i = 0; i < util.Count; i++)
            {
                if (GUI.Button(new Rect(xBase, yBase + i * optionSize, optionSize, optionSize), util[i]))
                {
                    if (dp3)
                    {
                        equip.z = i;
                        dp3 = false;
                    }
                    else if (dp4)
                    {
                        equip.w = i;
                        dp4 = false;
                    }

                }
            }
        }

    }

    void dropdownUpdate(){
        if ((dp1 | dp2 | dp3 | dp4) & Input.GetMouseButtonDown(0))
        {
            c = 8;
        }
        if (c == 0)
        {
            dp1 = false;
            dp2 = false;
            dp3 = false;
            dp4 = false;
        }
        c = Math.Max(-1, --c);
    }

    void drawEquipButtons()
    {
        
        if (GUI.Button(new Rect(400, 50, 75, 75), weapons[(int)equip.x]))
        {
            dp1 = true;
        }
        if (GUI.Button(new Rect(400, 175, 75, 75), weapons[(int)equip.y]))
        {
            dp2 = true;
        }
        if (GUI.Button(new Rect(400, 300, 75, 75), util[(int)equip.z]))
        {
            dp3 = true;
        }
        if (GUI.Button(new Rect(400, 425, 75, 75), util[(int)equip.w]))
        {
            dp4 = true;
        }
        
       
    }

    void drawUnitList()
    {
        for (int i = 0; i < unitsListP1.Count; i++)
        {

            yAnkerP1 += borderWidth;

            //draw einheit
            drawUnitIcon(i, xAnkerP1, yAnkerP1);

            yAnkerP1 += unitIconHeight + borderWidth;

        }
    }

    void drawUnitIcon(int unitID, int xPos, int yPos)
    {
        if (GUI.Button(new Rect(xPos, yPos, unitIconWidth, unitIconHeight), unitListBackground))
        {
            /*   if (Event.current.button == 0)
               {
                   activeUnit = unitID;
               }
               else if (Event.current.button == 1)
               {
                   unitsList.RemoveAt(unitID);
               }
             * */
        }

        //ausrüstung
        GUI.DrawTexture(new Rect(2 + xPos, yPos + 10, unitIconWidth / 5, unitIconWidth / 5), weapons[(int)(unitsListP1[unitID].x)]);
        GUI.DrawTexture(new Rect((2 + xPos + unitIconWidth / 5 + unitIconWidth / 5 / 6), yPos + 10, unitIconWidth / 5, unitIconWidth / 5), weapons[(int)(unitsListP1[unitID].y)]);
        GUI.DrawTexture(new Rect((2 + xPos + 2 * (unitIconWidth / 5 + unitIconWidth / 5 / 6)), yPos + 10, unitIconWidth / 5, unitIconWidth / 5), util[(int)(unitsListP1[unitID].z)]);
        GUI.DrawTexture(new Rect((2 + xPos + 3 * (unitIconWidth / 5 + unitIconWidth / 5 / 6)), yPos + 10, unitIconWidth / 5, unitIconWidth / 5), util[(int)(unitsListP1[unitID].w)]);
    }




    //staats einheiten 
    void drawP2()
    {
        int unitListXAnker = unitListXAnkerP2;

        //einheitenlisten hintergrund
        GUI.DrawTexture(new Rect(unitListXAnker,unitListYAnker, unitIconWidth+2*borderWidth, (int)(Screen.height*0.8f)), unitListBackground);


        //einheitenliste
        int xAnker = unitListXAnker + borderWidth;
        int yAnker = unitListYAnker;

      

        for (int i = 0; i < unitsListP2.Count; i++ )
        {
           
            yAnker += borderWidth;

            //draw einheit
            drawP2UnitIcon(i, xAnker, yAnker);

            yAnker += unitIconHeight + borderWidth;

        }

     

        //einheiten auswahlbuttons
        if (GUI.Button(new Rect((int)(Screen.width * 0.6), (int)(Screen.height * 0.4), 75, 75), unit1))
        {
            p2Pick(1);
        }
     
        if (GUI.Button(new Rect((int)(Screen.width * 0.8), (int)(Screen.height * 0.4), 75, 75), unit2))
        {
            if (!player1Picking)
            {
                p2Pick(2);
            }
  
        }
        if (GUI.Button(new Rect((int)(Screen.width * 0.6), (int)(Screen.height * 0.6), 75, 75), unit3))
        {
            if (!player1Picking)
            {
                p2Pick(3);
            }
   
        }
        if (GUI.Button(new Rect((int)(Screen.width * 0.8), (int)(Screen.height * 0.6), 75, 75), unit4))
        {
            if (!player1Picking)
            {
                p2Pick(4);
            }
        }

    }

    void drawP2UnitIcon(int unitID, int xPos, int yPos )
    {

        if (unitsListP2[unitID] == 1)
        {
            if (GUI.Button(new Rect(xPos, yPos, unitIconWidth, unitIconHeight), unit1))
            {
              /*  if (Event.current.button == 0){
                    activeUnit = unitID;
                }
                else if (Event.current.button == 1)
                {
                   unitsList.RemoveAt(unitID);
                }
               */
            }
        }
        else if (unitsListP2[unitID] == 2)
        {
            if (GUI.Button(new Rect(xPos, yPos, unitIconWidth, unitIconHeight), unit2))
            {
             
            }
        }
        else if (unitsListP2[unitID] == 3)
        {
            if (GUI.Button(new Rect(xPos, yPos, unitIconWidth, unitIconHeight), unit3))
            {

            }

        }
        else if (unitsListP2[unitID] == 4)
        {
            if (GUI.Button(new Rect(xPos, yPos, unitIconWidth, unitIconHeight), unit4))
            {
               
            }
        }
    }



}
