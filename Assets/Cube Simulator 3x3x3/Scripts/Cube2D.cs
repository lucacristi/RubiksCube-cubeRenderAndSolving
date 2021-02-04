
/* 2D CUBE LOGIC CLASS - Brute Force Code Version for performance optimization 
	 * (not really haha....okay maybe a little..).
		     ___ ___ ___
		   /___/___/___/|
		  /___/___/___/||
		 /___/___/__ /|/|
		|   |   |   | /||
		|___|___|___|/|/|
		|   |   |   | /||
		|___|___|___|/|/
		|   |   |   | /
		|___|___|___|/
	*/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Cube2D:MonoBehaviour
{

    // Cubies pieces are held in 3 separate arrays , 
    // this is just for representing the pieces as a whole regardless of their tiles , 
    // the tiles representation however can be found further below.

    // Main structure
    private string[,] cube1 = new string[3, 3];
    private string[,] cube2 = new string[3, 3];
    private string[,] cube3 = new string[3, 3];

    // Backup structure for AI Solving reasons
    private string[,] cubeA = new string[3, 3];
    private string[,] cubeB = new string[3, 3];
    private string[,] cubeC = new string[3, 3];

   private string[,] whitee = new string[3, 3];
   private string[,] redd = new string[3, 3];
   private string[,] bluee = new string[3, 3];
   private string[,] orangee = new string[3, 3];
   private string[,] greenn = new string[3, 3];
   private string[,] yelloww = new string[3, 3]; 


    public static char[,] cubeColors = new char[6, 9];
    // Use this for initialization

    public Cube2D()
    {

        Dll.main();



        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                cubeColors[i, j] = Dll.get_cube(i, j);
                Debug.Log(cubeColors[i, j]);
            }
        }

        redd[0, 0] = getColorFromChar(cubeColors[0, 2]);
        redd[0, 1] = getColorFromChar(cubeColors[0, 1]);
        redd[0, 2] = getColorFromChar(cubeColors[0, 0]);

        redd[1, 0] = getColorFromChar(cubeColors[0, 5]);
        redd[1, 1] = getColorFromChar(cubeColors[0, 4]);
        redd[1, 2] = getColorFromChar(cubeColors[0, 3]);

        redd[2, 0] = getColorFromChar(cubeColors[0, 8]);
        redd[2, 1] = getColorFromChar(cubeColors[0, 7]);
        redd[2, 2] = getColorFromChar(cubeColors[0, 6]);



        whitee[0, 0] = getColorFromChar(cubeColors[1, 2]);
        whitee[0, 1] = getColorFromChar(cubeColors[1, 1]);
        whitee[0, 2] = getColorFromChar(cubeColors[1, 0]);

        whitee[1, 0] = getColorFromChar(cubeColors[1, 5]);
        whitee[1, 1] = getColorFromChar(cubeColors[1, 4]);
        whitee[1, 2] = getColorFromChar(cubeColors[1, 3]);

        whitee[2, 0] = getColorFromChar(cubeColors[1, 8]);
        whitee[2, 1] = getColorFromChar(cubeColors[1, 7]);
        whitee[2, 2] = getColorFromChar(cubeColors[1, 6]);




        bluee[0, 0] = getColorFromChar(cubeColors[2, 2]);
        bluee[0, 1] = getColorFromChar(cubeColors[2, 1]);
        bluee[0, 2] = getColorFromChar(cubeColors[2, 0]);

        bluee[1, 0] = getColorFromChar(cubeColors[2, 5]);
        bluee[1, 1] = getColorFromChar(cubeColors[2, 4]);
        bluee[1, 2] = getColorFromChar(cubeColors[2, 3]);

        bluee[2, 0] = getColorFromChar(cubeColors[2, 8]);
        bluee[2, 1] = getColorFromChar(cubeColors[2, 7]);
        bluee[2, 2] = getColorFromChar(cubeColors[2, 6]);



        yelloww[0, 0] = getColorFromChar(cubeColors[3, 2]);    //56
        yelloww[0, 1] = getColorFromChar(cubeColors[3, 1]);    //53
        yelloww[0, 2] = getColorFromChar(cubeColors[3, 0]);    //50

        yelloww[1, 0] = getColorFromChar(cubeColors[3, 5]);
        yelloww[1, 1] = getColorFromChar(cubeColors[3, 4]);
        yelloww[1, 2] = getColorFromChar(cubeColors[3, 3]);

        yelloww[2, 0] = getColorFromChar(cubeColors[3, 8]);
        yelloww[2, 1] = getColorFromChar(cubeColors[3, 7]);
        yelloww[2, 2] = getColorFromChar(cubeColors[3, 6]);



        greenn[0, 0] = getColorFromChar(cubeColors[4, 2]);
        greenn[0, 1] = getColorFromChar(cubeColors[4, 1]);
        greenn[0, 2] = getColorFromChar(cubeColors[4, 0]);

        greenn[1, 0] = getColorFromChar(cubeColors[4, 5]);
        greenn[1, 1] = getColorFromChar(cubeColors[4, 4]);
        greenn[1, 2] = getColorFromChar(cubeColors[4, 3]);

        greenn[2, 0] = getColorFromChar(cubeColors[4, 8]);
        greenn[2, 1] = getColorFromChar(cubeColors[4, 7]);
        greenn[2, 2] = getColorFromChar(cubeColors[4, 6]);




        orangee[0, 0] = getColorFromChar(cubeColors[5, 2]);
        orangee[0, 1] = getColorFromChar(cubeColors[5, 1]);
        orangee[0, 2] = getColorFromChar(cubeColors[5, 0]);

        orangee[1, 0] = getColorFromChar(cubeColors[5, 5]);
        orangee[1, 1] = getColorFromChar(cubeColors[5, 4]);
        orangee[1, 2] = getColorFromChar(cubeColors[5, 3]);

        orangee[2, 0] = getColorFromChar(cubeColors[5, 8]);
        orangee[2, 1] = getColorFromChar(cubeColors[5, 7]);
        orangee[2, 2] = getColorFromChar(cubeColors[5, 6]);

        initArray();
        init2DTilesCube();
        
        
    }

    private void initArray()
    {

        //center pieces
        cube2[1, 2] = "Center Piece 1";
        cube3[1, 1] = "Center Piece 2";
        cube2[1, 0] = "Center Piece 3";
        cube1[1, 1] = "Center Piece 4";
        cube2[2, 1] = "Center Piece 5";
        cube2[0, 1] = "Center Piece 6";

        //corner pieces
          cube3[0, 2] = getCubeletNumber("cube3", 0, 2);
          cube1[0, 2] = getCubeletNumber("cube1", 0, 2);
          cube1[2, 2] = getCubeletNumber("cube1", 2, 2);
          cube3[2, 2] = getCubeletNumber("cube3", 2, 2);
          cube3[0, 0] = getCubeletNumber("cube3", 0, 0);
          cube3[2, 0] = getCubeletNumber("cube3", 2, 0);
          cube1[0, 0] = getCubeletNumber("cube1", 0, 0);
          cube1[2, 0] = getCubeletNumber("cube1", 2, 0);


          //edge pieces
          cube2[0, 2] = getCubeletNumber("cube2", 0, 2);   
          cube2[2, 2] = getCubeletNumber("cube2", 2, 2);        
          cube3[1, 2] = getCubeletNumber("cube3", 1, 2);        
          cube1[1, 2] = getCubeletNumber("cube1", 1, 2);        
          cube2[0, 0] = getCubeletNumber("cube2", 0, 0);        
          cube2[2, 0] = getCubeletNumber("cube2", 2, 0);        
          cube1[1, 0] = getCubeletNumber("cube1", 1, 0);        
          cube3[1, 0] = getCubeletNumber("cube3", 1, 0);        
          cube3[0, 1] = getCubeletNumber("cube3", 0, 1);        
          cube3[2, 1] = getCubeletNumber("cube3", 2, 1);        
          cube1[2, 1] = getCubeletNumber("cube1", 2, 1);       
          cube1[0, 1] = getCubeletNumber("cube1", 0, 1);

/*
        //corner pieces, BACK-UP EXAMPLE
        cube3[0, 2] = "Corner Piece 5";
        cube1[0, 2] = "Corner Piece 8";
        cube1[2, 2] = "Corner Piece 7";
        cube3[2, 2] = "Corner Piece 6";
        cube3[0, 0] = "Corner Piece 3";
        cube3[2, 0] = "Corner Piece 1";
        cube1[0, 0] = "Corner Piece 4";
        cube1[2, 0] = "Corner Piece 2";


        //edge pieces
        cube2[0, 2] = "Edge Piece 1";
        cube2[2, 2] = "Edge Piece 11";
        cube3[1, 2] = "Edge Piece 8";
        cube1[1, 2] = "Edge Piece 10";
        cube2[0, 0] = "Edge Piece 2";
        cube2[2, 0] = "Edge Piece 12";
        cube1[1, 0] = "Edge Piece 5";
        cube3[1, 0] = "Edge Piece 4";
        cube3[0, 1] = "Edge Piece 6";
        cube3[2, 1] = "Edge Piece 9";
        cube1[2, 1] = "Edge Piece 3";
        cube1[0, 1] = "Edge Piece 7";*/


        //core piece
        cube2[1, 1] = "Void";

    }

    public string[,] getFace(int face)
    {
        if (face == 0)
        {
            return getBlueFace();
        }
        else if (face == 1)
        {
            return getOrangeFace();
        }
        else if (face == 2)
        {
            return getGreenFace();
        }
        else if (face == 3)
        {
            return getRedFace();
        }
        else if (face == 4)
        {
            return getYellowFace();
        }
        else if (face == 5)
        {
            return getWhiteFace();
        }
        return null;
    }

    public string[,] getRedFace()
    {
        string[,] arr = new string[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                arr[i, j] = cube1[i, j];
        return arr;
    }

    public string[,] getOrangeFace()
    {
        string[,] arr = new string[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                arr[i, j] = cube3[i, j];
        return arr;
    }

    public string[,] getBlueFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[0, 2]; arr[0, 1] = cube2[0, 2]; arr[0, 2] = cube3[0, 2];
        arr[1, 0] = cube1[1, 2]; arr[1, 1] = cube2[1, 2]; arr[1, 2] = cube3[1, 2];
        arr[2, 0] = cube1[2, 2]; arr[2, 1] = cube2[2, 2]; arr[2, 2] = cube3[2, 2];
        return arr;
    }

    public string[,] getGreenFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[0, 0]; arr[0, 1] = cube2[0, 0]; arr[0, 2] = cube3[0, 0];
        arr[1, 0] = cube1[1, 0]; arr[1, 1] = cube2[1, 0]; arr[1, 2] = cube3[1, 0];
        arr[2, 0] = cube1[2, 0]; arr[2, 1] = cube2[2, 0]; arr[2, 2] = cube3[2, 0];
        return arr;
    }

    public string[,] getWhiteFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[0, 0]; arr[0, 1] = cube2[0, 0]; arr[0, 2] = cube3[0, 0];
        arr[1, 0] = cube1[0, 1]; arr[1, 1] = cube2[0, 1]; arr[1, 2] = cube3[0, 1];
        arr[2, 0] = cube1[0, 2]; arr[2, 1] = cube2[0, 2]; arr[2, 2] = cube3[0, 2];
        return arr;
    }

    public string[,] getYellowFace()
    {
        string[,] arr = new string[3, 3];
        arr[0, 0] = cube1[2, 0]; arr[0, 1] = cube2[2, 0]; arr[0, 2] = cube3[2, 0];
        arr[1, 0] = cube1[2, 1]; arr[1, 1] = cube2[2, 1]; arr[1, 2] = cube3[2, 1];
        arr[2, 0] = cube1[2, 2]; arr[2, 1] = cube2[2, 2]; arr[2, 2] = cube3[2, 2];
        return arr;
    }

    public void Rotate(int face)
    {
        if (face == 3)
        { //red
          //rotate corners
            string temp = cube1[0, 0];
            cube1[0, 0] = cube1[2, 0];
            cube1[2, 0] = cube1[2, 2];
            cube1[2, 2] = cube1[0, 2];
            cube1[0, 2] = temp;

            //rotate edges
            temp = cube1[0, 1];
            cube1[0, 1] = cube1[1, 0];
            cube1[1, 0] = cube1[2, 1];
            cube1[2, 1] = cube1[1, 2];
            cube1[1, 2] = temp;

            //rotate red face tiles
            rotate2DTRed();
        }
        else if (face == 0)
        {//blue

            //rotate corners
            string temp = cube1[0, 2];
            cube1[0, 2] = cube1[2, 2];
            cube1[2, 2] = cube3[2, 2];
            cube3[2, 2] = cube3[0, 2];
            cube3[0, 2] = temp;

            //rotate edges
            temp = cube2[0, 2];
            cube2[0, 2] = cube1[1, 2];
            cube1[1, 2] = cube2[2, 2];
            cube2[2, 2] = cube3[1, 2];
            cube3[1, 2] = temp;

            //rotate blue face tiles
            rotate2DTBlue();
        }
        else if (face == 1)
        {//orange
         //rotate corners
            string temp = cube3[0, 2];
            cube3[0, 2] = cube3[2, 2];
            cube3[2, 2] = cube3[2, 0];
            cube3[2, 0] = cube3[0, 0];
            cube3[0, 0] = temp;

            //rotate edges
            temp = cube3[0, 1];
            cube3[0, 1] = cube3[1, 2];
            cube3[1, 2] = cube3[2, 1];
            cube3[2, 1] = cube3[1, 0];
            cube3[1, 0] = temp;

            //rotate orange face tiles
            rotate2DTOrange();
        }
        else if (face == 2)
        {//green
         //rotate corners
            string temp = cube3[0, 0];
            cube3[0, 0] = cube3[2, 0];
            cube3[2, 0] = cube1[2, 0];
            cube1[2, 0] = cube1[0, 0];
            cube1[0, 0] = temp;

            //rotate edges
            temp = cube2[0, 0];
            cube2[0, 0] = cube3[1, 0];
            cube3[1, 0] = cube2[2, 0];
            cube2[2, 0] = cube1[1, 0];
            cube1[1, 0] = temp;

            //rotate green face tiles
            rotate2DTGreen();
        }
        else if (face == 5)
        {//white
         //rotate corners
            string temp = cube1[0, 0];
            cube1[0, 0] = cube1[0, 2];
            cube1[0, 2] = cube3[0, 2];
            cube3[0, 2] = cube3[0, 0];
            cube3[0, 0] = temp;

            //rotate edges
            temp = cube2[0, 0];
            cube2[0, 0] = cube1[0, 1];
            cube1[0, 1] = cube2[0, 2];
            cube2[0, 2] = cube3[0, 1];
            cube3[0, 1] = temp;

            //rotate white face tiles
            rotate2DTWhite();
        }
        else if (face == 4)
        {//yellow
         //rotate corners
            string temp = cube1[2, 0];
            cube1[2, 0] = cube3[2, 0];
            cube3[2, 0] = cube3[2, 2];
            cube3[2, 2] = cube1[2, 2];
            cube1[2, 2] = temp;

            //rotate edges
            temp = cube1[2, 1];
            cube1[2, 1] = cube2[2, 0];
            cube2[2, 0] = cube3[2, 1];
            cube3[2, 1] = cube2[2, 2];
            cube2[2, 2] = temp;

            //rotate yellow face tiles
            rotate2DTYellow();
        }
    }


    /*
	 * 2D Tiles Cube Logic blueprint(based on RK) , this is used for future development of an AI Cube Solver 
	 * as its necessary to identify tiles positions in solving algorithms as well the correct solved state. (Currently used to detect solved state)
	 * 
	 * A tile is identified by the color it carries , 
	 * in this case a face comprises of 9 tiles thus at the start 9 tiles at each face will have the same color.
	 * 
	 * FS stands for Final State or Finish State or Final Solve , it is used to indicate the solved state of the cube.
	 * 
	 * the one's ending with B (eg whiteB) are used as a backup to be used for the AI Solve procedure.
	 */

    private string[,] white, FSWhite, whiteB;
    private string[,] red, FSRed, redB;
    private string[,] blue, FSBlue, blueB;
    private string[,] orange, FSOrange, orangeB;
    private string[,] green, FSGreen, greenB;
    private string[,] yellow, FSYellow, yellowB;

    private void init2DTilesCube()
    {
        white = new string[3, 3]; FSWhite = new string[3, 3]; whiteB = new string[3, 3];
        red = new string[3, 3]; FSRed = new string[3, 3]; redB = new string[3, 3];
        blue = new string[3, 3]; FSBlue = new string[3, 3]; blueB = new string[3, 3];
        orange = new string[3, 3]; FSOrange = new string[3, 3]; orangeB = new string[3, 3];
        green = new string[3, 3]; FSGreen = new string[3, 3]; greenB = new string[3, 3];
        yellow = new string[3, 3]; FSYellow = new string[3, 3]; yellowB = new string[3, 3];


       
         red[0, 0] = getColorFromChar(cubeColors[0, 2]);
         red[0, 1] = getColorFromChar(cubeColors[0, 1]);
         red[0, 2] = getColorFromChar(cubeColors[0, 0]);

         red[1, 0] = getColorFromChar(cubeColors[0, 5]);
         red[1, 1] = getColorFromChar(cubeColors[0, 4]);
         red[1, 2] = getColorFromChar(cubeColors[0, 3]);

         red[2, 0] = getColorFromChar(cubeColors[0, 8]);
         red[2, 1] = getColorFromChar(cubeColors[0, 7]);
         red[2, 2] = getColorFromChar(cubeColors[0, 6]);



         white[0, 0] = getColorFromChar(cubeColors[1, 2]);
         white[0, 1] = getColorFromChar(cubeColors[1, 1]);
         white[0, 2] = getColorFromChar(cubeColors[1, 0]);

         white[1, 0] = getColorFromChar(cubeColors[1, 5]);
         white[1, 1] = getColorFromChar(cubeColors[1, 4]);
         white[1, 2] = getColorFromChar(cubeColors[1, 3]);

         white[2, 0] = getColorFromChar(cubeColors[1, 8]);
         white[2, 1] = getColorFromChar(cubeColors[1, 7]);
         white[2, 2] = getColorFromChar(cubeColors[1, 6]);




         blue[0, 0] = getColorFromChar(cubeColors[2, 2]);
         blue[0, 1] = getColorFromChar(cubeColors[2, 1]);
         blue[0, 2] = getColorFromChar(cubeColors[2, 0]);

         blue[1, 0] = getColorFromChar(cubeColors[2, 5]);
         blue[1, 1] = getColorFromChar(cubeColors[2, 4]);
         blue[1, 2] = getColorFromChar(cubeColors[2, 3]);

         blue[2, 0] = getColorFromChar(cubeColors[2, 8]);
         blue[2, 1] = getColorFromChar(cubeColors[2, 7]);
         blue[2, 2] = getColorFromChar(cubeColors[2, 6]);



         yellow[0, 0] = getColorFromChar(cubeColors[3, 2]);    //56
         yellow[0, 1] = getColorFromChar(cubeColors[3, 1]);    //53
         yellow[0, 2] = getColorFromChar(cubeColors[3, 0]);    //50

         yellow[1, 0] = getColorFromChar(cubeColors[3, 5]);
         yellow[1, 1] = getColorFromChar(cubeColors[3, 4]);
         yellow[1, 2] = getColorFromChar(cubeColors[3, 3]);

         yellow[2, 0] = getColorFromChar(cubeColors[3, 8]);
         yellow[2, 1] = getColorFromChar(cubeColors[3, 7]);
         yellow[2, 2] = getColorFromChar(cubeColors[3, 6]);



         green[0, 0] = getColorFromChar(cubeColors[4, 2]);
         green[0, 1] = getColorFromChar(cubeColors[4, 1]);
         green[0, 2] = getColorFromChar(cubeColors[4, 0]);

         green[1, 0] = getColorFromChar(cubeColors[4, 5]);
         green[1, 1] = getColorFromChar(cubeColors[4, 4]);
         green[1, 2] = getColorFromChar(cubeColors[4, 3]);

         green[2, 0] = getColorFromChar(cubeColors[4, 8]);
         green[2, 1] = getColorFromChar(cubeColors[4, 7]);
         green[2, 2] = getColorFromChar(cubeColors[4, 6]);




         orange[0, 0] = getColorFromChar(cubeColors[5, 2]);
         orange[0, 1] = getColorFromChar(cubeColors[5, 1]);
         orange[0, 2] = getColorFromChar(cubeColors[5, 0]);

         orange[1, 0] = getColorFromChar(cubeColors[5, 5]);
         orange[1, 1] = getColorFromChar(cubeColors[5, 4]);
         orange[1, 2] = getColorFromChar(cubeColors[5, 3]);

         orange[2, 0] = getColorFromChar(cubeColors[5, 8]);
         orange[2, 1] = getColorFromChar(cubeColors[5, 7]);
         orange[2, 2] = getColorFromChar(cubeColors[5, 6]);
        
    /*    //BACK-UP EXAMPLE
        red[0, 0] = "blue";
        red[0, 1] = "green";
        red[0, 2] = "yellow";

        red[1, 0] = "white";
        red[1, 1] = "red";
        red[1, 2] = "yellow";

        red[2, 0] = "blue";
        red[2, 1] = "blue";
        red[2, 2] = "red";



        white[0, 0] = "blue";
        white[0, 1] = "green";
        white[0, 2] = "orange";

        white[1, 0] = "yellow";
        white[1, 1] = "white";
        white[1, 2] = "white";

        white[2, 0] = "yellow";
        white[2, 1] = "red";
        white[2, 2] = "green";




        blue[0, 0] = "red";
        blue[0, 1] = "blue";
        blue[0, 2] = "green";

        blue[1, 0] = "orange";
        blue[1, 1] = "blue";
        blue[1, 2] = "orange";

        blue[2, 0] = "green";
        blue[2, 1] = "red";
        blue[2, 2] = "orange";



        yellow[0, 0] = "red";
        yellow[0, 1] = "orange";
        yellow[0, 2] = "white";

        yellow[1, 0] = "white";
        yellow[1, 1] = "yellow";
        yellow[1, 2] = "yellow";

        yellow[2, 0] = "blue";
        yellow[2, 1] = "orange";
        yellow[2, 2] = "yellow";



        green[0, 0] = "yellow";
        green[0, 1] = "blue";
        green[0, 2] = "orange";

        green[1, 0] = "red";
        green[1, 1] = "green";
        green[1, 2] = "green";

        green[2, 0] = "orange";
        green[2, 1] = "red";
        green[2, 2] = "white";




        orange[0, 0] = "white";
        orange[0, 1] = "yellow";
        orange[0, 2] = "red";

        orange[1, 0] = "green";
        orange[1, 1] = "orange";
        orange[1, 2] = "blue";

        orange[2, 0] = "green";
        orange[2, 1] = "white";
        orange[2, 2] = "white";
        */



        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                FSWhite[i, j] = "white";
                FSRed[i, j] = "red";
                FSBlue[i, j] = "blue";
                FSOrange[i, j] = "orange";
                FSGreen[i, j] = "green";
                FSYellow[i, j] = "yellow";
            }
        }
    }

    //clockwise (starting at top red layer)
    private void rotate2DTWhite()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = red[0, i]; //store red

        /*Rotation of neighbour colors tiles*/

        //red <- blue
        for (int i = 0; i < 3; i++)
            red[0, i] = blue[0, i];

        //blue <- orange
        for (int i = 0; i < 3; i++)
            blue[0, i] = orange[0, i];

        //orange <- green
        for (int i = 0; i < 3; i++)
            orange[0, i] = green[0, i];

        //green <- red
        for (int i = 0; i < 3; i++)
            green[0, i] = temp[i];


        /*Rotation of white face tiles*/

        //rotate corners
        temp[0] = white[0, 0];
        white[0, 0] = white[2, 0];
        white[2, 0] = white[2, 2];
        white[2, 2] = white[0, 2];
        white[0, 2] = temp[0];

        //rotate edges
        temp[0] = white[0, 1];
        white[0, 1] = white[1, 0];
        white[1, 0] = white[2, 1];
        white[2, 1] = white[1, 2];
        white[1, 2] = temp[0];
    }

    //clockwise (starting at bottom orange layer)
    private void rotate2DTYellow()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = orange[2, i]; //store orange

        /*Rotation of neighbour colors tiles*/

        //orange <- blue
        for (int i = 0; i < 3; i++)
            orange[2, i] = blue[2, i];

        //blue <- red
        for (int i = 0; i < 3; i++)
            blue[2, i] = red[2, i];

        //red <- green
        for (int i = 0; i < 3; i++)
            red[2, i] = green[2, i];

        //green <- orange
        for (int i = 0; i < 3; i++)
            green[2, i] = temp[i];


        /*Rotation of yellow face tiles*/

        //rotate corners
        temp[0] = yellow[0, 0];
        yellow[0, 0] = yellow[2, 0];
        yellow[2, 0] = yellow[2, 2];
        yellow[2, 2] = yellow[0, 2];
        yellow[0, 2] = temp[0];

        //rotate edges
        temp[0] = yellow[0, 1];
        yellow[0, 1] = yellow[1, 0];
        yellow[1, 0] = yellow[2, 1];
        yellow[2, 1] = yellow[1, 2];
        yellow[1, 2] = temp[0];
    }

    //clockwise (starting at top yellow layer)
    private void rotate2DTRed()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[0, i]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- blue
        for (int i = 0; i < 3; i++)
            yellow[0, i] = blue[2 - i, 0];

        //blue <- white
        for (int i = 0; i < 3; i++)
            blue[2 - i, 0] = white[2, 2 - i];

        //white <- green
        for (int i = 0; i < 3; i++)
            white[2, 2 - i] = green[i, 2];

        //green <- yellow
        for (int i = 0; i < 3; i++)
            green[i, 2] = temp[i];


        /*Rotation of red face tiles*/

        //rotate corners
        temp[0] = red[0, 0];
        red[0, 0] = red[2, 0];
        red[2, 0] = red[2, 2];
        red[2, 2] = red[0, 2];
        red[0, 2] = temp[0];

        //rotate edges
        temp[0] = red[0, 1];
        red[0, 1] = red[1, 0];
        red[1, 0] = red[2, 1];
        red[2, 1] = red[1, 2];
        red[1, 2] = temp[0];
    }

    //clockwise (starting at bottom yellow layer)
    private void rotate2DTOrange()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[2, i]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- green
        for (int i = 0; i < 3; i++)
            yellow[2, i] = green[i, 0];

        //green <- white
        for (int i = 0; i < 3; i++)
            green[i, 0] = white[0, 2 - i];

        //white <- blue
        for (int i = 0; i < 3; i++)
            white[0, 2 - i] = blue[2 - i, 2];

        //blue <- yellow
        for (int i = 0; i < 3; i++)
            blue[2 - i, 2] = temp[i];


        /*Rotation of orange face tiles*/

        //rotate corners
        temp[0] = orange[0, 0];
        orange[0, 0] = orange[2, 0];
        orange[2, 0] = orange[2, 2];
        orange[2, 2] = orange[0, 2];
        orange[0, 2] = temp[0];

        //rotate edges
        temp[0] = orange[0, 1];
        orange[0, 1] = orange[1, 0];
        orange[1, 0] = orange[2, 1];
        orange[2, 1] = orange[1, 2];
        orange[1, 2] = temp[0];
    }

    //clockwise (starting at right yellow layer)
    private void rotate2DTBlue()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[i, 2]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- orange
        for (int i = 0; i < 3; i++)
            yellow[i, 2] = orange[2 - i, 0];

        //orange <- white
        for (int i = 0; i < 3; i++)
            orange[2 - i, 0] = white[i, 2];

        //white <- red
        for (int i = 0; i < 3; i++)
            white[i, 2] = red[i, 2];

        //red <- yellow
        for (int i = 0; i < 3; i++)
            red[i, 2] = temp[i];


        /*Rotation of blue face tiles*/

        //rotate corners
        temp[0] = blue[0, 0];
        blue[0, 0] = blue[2, 0];
        blue[2, 0] = blue[2, 2];
        blue[2, 2] = blue[0, 2];
        blue[0, 2] = temp[0];

        //rotate edges
        temp[0] = blue[0, 1];
        blue[0, 1] = blue[1, 0];
        blue[1, 0] = blue[2, 1];
        blue[2, 1] = blue[1, 2];
        blue[1, 2] = temp[0];
    }

    //clockwise (starting at left yellow layer)
    private void rotate2DTGreen()
    {
        string[] temp = new string[3];
        for (int i = 0; i < 3; i++)
            temp[i] = yellow[i, 0]; //store yellow

        /*Rotation of neighbour colors tiles*/

        //yellow <- blue
        for (int i = 0; i < 3; i++)
            yellow[i, 0] = red[i, 0];

        //red <- white
        for (int i = 0; i < 3; i++)
            red[i, 0] = white[i, 0];

        //white <- orange
        for (int i = 0; i < 3; i++)
            white[i, 0] = orange[2 - i, 2];

        //orange <- yellow
        for (int i = 0; i < 3; i++)
            orange[2 - i, 2] = temp[i];


        /*Rotation of blue face tiles*/

        //rotate corners
        temp[0] = green[0, 0];
        green[0, 0] = green[2, 0];
        green[2, 0] = green[2, 2];
        green[2, 2] = green[0, 2];
        green[0, 2] = temp[0];

        //rotate edges
        temp[0] = green[0, 1];
        green[0, 1] = green[1, 0];
        green[1, 0] = green[2, 1];
        green[2, 1] = green[1, 2];
        green[1, 2] = temp[0];
    }

    public string[,] GetWhiteFaceTiles()
    {
        return white;
    }

    public string[,] GetRedFaceTiles()
    {
        return red;
    }

    public string[,] GetYellowFaceTiles()
    {
        return yellow;
    }

    public string[,] GetOrangeFaceTiles()
    {
        return orange;
    }

    public string[,] GetBlueFaceTiles()
    {
        return blue;
    }

    public string[,] GetGreenFaceTiles()
    {
        return green;
    }

    // check if the cube is solved
    public bool isSolved()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (orange[i, j] != FSOrange[i, j])
                {
                    return false;
                }
                else if (green[i, j] != FSGreen[i, j])
                {
                    return false;
                }
                else if (white[i, j] != FSWhite[i, j])
                {
                    return false;
                }
                else if (blue[i, j] != FSBlue[i, j])
                {
                    return false;
                }
                else if (red[i, j] != FSRed[i, j])
                {
                    return false;
                }
                else if (yellow[i, j] != FSYellow[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    /*Blueprint source code for future development of an AI solver using certain algorithms (Currently not used) */
    public void solveCube()
    {
         string[,] cube1ref = new string[3, 3];
         string[,] cube2ref = new string[3, 3];
         string[,] cube3ref = new string[3, 3];
        //center pieces
        cube2ref[1, 2] = "Center Piece 1";
        cube3ref[1, 1] = "Center Piece 2";
        cube2ref[1, 0] = "Center Piece 3";
        cube1ref[1, 1] = "Center Piece 4";
        cube2ref[2, 1] = "Center Piece 5";
        cube2ref[0, 1] = "Center Piece 6";

        //corner pieces
        cube3ref[0, 2] = "Corner Piece 1";
        cube1ref[0, 2] = "Corner Piece 2";
        cube1ref[2, 2] = "Corner Piece 3";
        cube3ref[2, 2] = "Corner Piece 4";
        cube3ref[0, 0] = "Corner Piece 5";
        cube3ref[2, 0] = "Corner Piece 6";
        cube1ref[0, 0] = "Corner Piece 7";
        cube1ref[2, 0] = "Corner Piece 8";

        //edge pieces
        cube2ref[0, 2] = "Edge Piece 1";
        cube2ref[2, 2] = "Edge Piece 2";
        cube3ref[1, 2] = "Edge Piece 3";
        cube1ref[1, 2] = "Edge Piece 4";
        cube2ref[0, 0] = "Edge Piece 5";
        cube2ref[2, 0] = "Edge Piece 6";
        cube1ref[1, 0] = "Edge Piece 7";
        cube3ref[1, 0] = "Edge Piece 8";
        cube3ref[0, 1] = "Edge Piece 9";
        cube3ref[2, 1] = "Edge Piece 10";
        cube1ref[2, 1] = "Edge Piece 11";
        cube1ref[0, 1] = "Edge Piece 12";

        //core piece
        cube2ref[1, 1] = "Void";

        string[,] whiteref = new string[3, 3];
        string[,] redref = new string[3, 3];
        string[,] blueref = new string[3, 3];
        string[,] orangeref = new string[3, 3];
        string[,] greenref = new string[3, 3];
        string[,] yellowref = new string[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                whiteref[i, j] = "white";
                redref[i, j] = "red";
                blueref[i, j] = "blue";
                orangeref[i, j] = "orange";
                greenref[i, j] = "green";
                yellowref[i, j] = "yellow";
            }
        }
        // store original before solve

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cubeA[i, j] = cube1ref[i, j];
                cubeB[i, j] = cube2ref[i, j];
                cubeC[i, j] = cube3ref[i, j];

                whiteB[i, j] = whiteref[i, j];
                redB[i, j] = redref[i, j];
                blueB[i, j] = blueref[i, j];
                orangeB[i, j] = orangeref[i, j];
                greenB[i, j] = greenref[i, j];
                yellowB[i, j] = yellowref[i, j];
            }
        }
        //Debug.Log ("Solving The Cross!");
        solveCross();
        //Debug.Log ("Solving F2L!");
        solveF2L();
        //Debug.Log ("Solving OLL!");
        solveOLL();
        //Debug.Log ("Solving PLL");
        solvePLL();

        //reset to original to not corrupt the 3D cube parenting process
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cube1[i, j] = cubeA[i, j];
                cube2[i, j] = cubeB[i, j];
                cube3[i, j] = cubeC[i, j];

                white[i, j] = whiteB[i, j];
                red[i, j] = redB[i, j];
                blue[i, j] = blueB[i, j];
                orange[i, j] = orangeB[i, j];
                green[i, j] = greenB[i, j];
                yellow[i, j] = yellowB[i, j];
            }
        }
    }

    /*
	 * Solving the cross means to solve to a certain's face "+" sign
	 * with the adjacent middle layer parrlel edges being the correct color
	 * to the middle layer's center piece , in my algorithm for this secnario will being from the white center piece.
	 */

    public List<string> edgeSolveCubies = new List<string>();
    public List<string> moveList = new List<string>();

    private void solveCross()
    {
        /*
		 * 4 main pieces needed to be solved in order to pass this stage
		 * 
		 * Pieces are:
		 * - Edge Piece 1
		 * - Edge Piece 5
		 * - Edge Piece 9
		 * - Edge Piece 12
		 * 
		 * Rules:
		 * - Edge Pieces can be only found in 12 places since there are 12 of them:
		 * - Tiles representation is used in order to find out the correct position of a tile which will influence the algorithm.
		 * 
		 * Yellow Side Edge Pieces Locations In The Array (Circular starting from red -> green -> orange -> blue):
		 * - cube1[2,1]
		 * - cube2[2,0]
		 * - cube3[2,1]
		 * - cube2[2,2]
		 * 
		 * Center Side Edge Pieces Locations In The Array (Circular starting from blue/red -> red/green -> green/orange -> orange/blue):
		 * - cube1[1,2]
		 * - cube1[1,0]
		 * - cube3[1,0]
		 * - cube3[1,2]
		 * 
		 * White Side Edge Pieces Locations In The Array (Circular starting from red -> orange -> green -> blue):
		 * - cube1[0,1]
		 * - cube2[0,0]
		 * - cube3[0,1]
		 * - cube2[0,2]
		 * 
		 * Objective:
		 * Get the 4 pieces in their respective locations at the white side with the correct tile positioning , meaning:
		 * 
		 * - Edge Piece 1  -> cube2[0,2]
		 * - Edge Piece 5  -> cube2[0,0]
		 * - Edge Piece 9  -> cube3[0,1]
		 * - Edge Piece 12 -> cube1[0,1]
		 * 
		 */

        // if the edge piece isn't in its proper location , add it to the to be solved list
        if (cube2[0, 2] != "Edge Piece 1")
        {
            edgeSolveCubies.Add("Edge Piece 1");
        }
        else
        {
            if (white[1, 2] != "white" && blue[0, 1] != "blue")
            {
                edgeSolveCubies.Add("Edge Piece 1");
            }
        }
        if (cube2[0, 0] != "Edge Piece 5")
        {
            edgeSolveCubies.Add("Edge Piece 5");
        }
        else
        {
            if (white[1, 0] != "white" && green[0, 1] != "green")
                edgeSolveCubies.Add("Edge Piece 5");
        }
        if (cube3[0, 1] != "Edge Piece 9")
        {
            edgeSolveCubies.Add("Edge Piece 9");
        }
        else
        {
            if (white[0, 1] != "white" && orange[0, 1] != "orange")
                edgeSolveCubies.Add("Edge Piece 9");
        }

        if (cube1[0, 1] != "Edge Piece 12")
        {
            edgeSolveCubies.Add("Edge Piece 12");
        }
        else
        {
            if (white[2, 1] != "white" && red[0, 1] != "red")
                edgeSolveCubies.Add("Edge Piece 12");
        }



        while (edgeSolveCubies.Count > 0)
        {
            string piece = edgeSolveCubies[0];
            edgeSolveCubies.RemoveAt(0);

            //find piece location array
            string arrName = "";
            int x = -1, y = -1;
            bool flag = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cube1[i, j] == piece)
                    {
                        x = i; y = j;
                        arrName = "cube1";
                        flag = true;
                        break;
                    }
                    else if (cube2[i, j] == piece)
                    {
                        x = i; y = j;
                        arrName = "cube2";
                        flag = true;
                        break;
                    }
                    else if (cube3[i, j] == piece)
                    {
                        x = i; y = j;
                        arrName = "cube3";
                        flag = true;
                        break;
                    }
                }
                if (flag) break;
            }

            /* Use location to check edge piece colors position (only 4 locations per array) then perform solving steps accordingly
				 (take them down , preserve , find correct match and take it up!)
				 the idea is to solve internally here while keeping track of all moves in the array to re-simulate it later on the 3D cube.
				 The current idea in solving the cross is to take the white edged pieces down and place them under the red center piece and use an algorithm to solve
				 (kinda expensive or illogical....but it should work and save me some coding time) */

            /* Rotate (x);
				x = 0 = blue face
				x = 1 = orange face
				x = 2 = green face
				x = 3 = red face
				x = 4 = yellow face
				x = 5 = white face


				Movelist:

				F = Front = Red Face
				R = Right = Blue Face
				B = Back = Orange Face
				L = Left = Green Face
				U = Up = White Face
				D = Down = Yellow face

				* Adding a "-" before the move indicator without quotes indicates a "reverse". (eg. -F -> indicates a revese of the front or red face rotation)
				* Just not to be confused you should note when I say white/red faces edge piece or relevant examples I mean the "edge piece" with UNKOWN COLORS that sits between the white/red faces or x/y faces.

			*/

            if (arrName == "cube1")
            { // First Array , for each edge piece position a custom algorithm
                if (x == 0 && y == 1)
                { //check white/red faces edge piece and take it down and solve
                    if (red[0, 1] == "white") 
                    {
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0); /*3 times rotation is to simulate a "reverse" rotation (yeah yeah...I know its cheating but it saves me from extra coding :| )*/
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                        moveList.Add("R"); Rotate(0);

                    }
                    else
                    {
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("F"); Rotate(3);
                    }
                    /* This concludes the algorithm to solve the white/red layers edge piece */

                }
                else if (x == 1 && y == 0)
                {
                    //check green/red faces edge piece and take it down

                    if (red[1, 0] == "white")
                    {
                        moveList.Add("L"); Rotate(2);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                    }
                    else
                    {
                        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("D"); Rotate(4);


                    }
                }
                else if (x == 2 && y == 1)
                {
                    // check yellow/red faces (already down but check white tile position)

                    if (red[2, 1] == "white")
                    {
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("L"); Rotate(2);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    }
                    else
                    {

                    }

                }
                else if (x == 1 && y == 2)
                {
                    // check blue/red faces and take it down
                    if (red[1, 2] == "white")
                    {
                        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                        moveList.Add("R"); Rotate(0);
                    }
                    else
                    {
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    }
                }
            }
            else if (arrName == "cube2")
            {

                if (x == 0 && y == 0)
                {
                    if (green[0, 1] == "white")
                    {
                        moveList.Add("L"); Rotate(2);
                        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("D"); Rotate(4);



                    }
                    else
                    {
                        moveList.Add("L"); Rotate(2);
                        moveList.Add("L"); Rotate(2);
                        moveList.Add("D"); Rotate(4);



                    }
                }
                else if (x == 0 && y == 2)
                {
                    if (blue[0, 1] == "white")
                    {
                        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);



                    }
                    else
                    {
                        moveList.Add("R"); Rotate(0);
                        moveList.Add("R"); Rotate(0);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);



                    }
                }
                else if (x == 2 && y == 0)
                {
                    if (green[2, 1] == "white")
                    {
                        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("L"); Rotate(2);



                    }
                    else
                    {
                        moveList.Add("D"); Rotate(4);



                    }
                }
                else if (x == 2 && y == 2)
                {
                    if (blue[2, 1] == "white")
                    {
                        moveList.Add("R"); Rotate(0);
                        moveList.Add("F"); Rotate(3);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);



                    }
                    else
                    {
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);



                    }
                }
            }
            else if (arrName == "cube3")
            {
                if (x == 0 && y == 1)
                {
                    if (orange[0, 1] == "white")
                    {
                        moveList.Add("B"); Rotate(1);
                        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("L"); Rotate(2);
                    }
                    else
                    {
                        moveList.Add("B"); Rotate(1);
                        moveList.Add("B"); Rotate(1);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("D"); Rotate(4);
                    }
                }
                else if (x == 1 && y == 0)
                {
                    if (orange[1, 2] == "white")
                    {
                        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("L"); Rotate(2);
                    }
                    else
                    {
                        moveList.Add("B"); Rotate(1);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                    }

                }
                else if (x == 2 && y == 1)
                {
                    if (orange[2, 1] == "white")
                    {
                        moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("L"); Rotate(2);
                        moveList.Add("B"); Rotate(1);
                    }
                    else
                    {
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("D"); Rotate(4);
                    }

                }
                else if (x == 1 && y == 2)
                {
                    if (orange[1, 0] == "white")
                    {
                        moveList.Add("R"); Rotate(0);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
                    }
                    else
                    {
                        moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("B"); Rotate(1);
                    }
                }
            }
            // at this stage it is down , next stage is to solve it and rotate it to where it belongs
            solveRYEdge();

            //	//Debug.Log("Piece : " + piece + " on array " + arrName);
            //	for(int i = cpp;i < moveList.Count;i++) //Debug.Log(moveList[i]);
            //	cpp = moveList.Count;
        }
    }
    //int cpp = 0;
    private void solveRYEdge()
    {
        if (red[2, 1] != "red")
        {
            if (red[2, 1] == "blue")
            {
                moveList.Add("D"); Rotate(4);
                moveList.Add("R"); Rotate(0);
                moveList.Add("R"); Rotate(0);
            }
            else if (red[2, 1] == "orange")
            {
                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                moveList.Add("B"); Rotate(1);
                moveList.Add("B"); Rotate(1);
            }
            else if (red[2, 1] == "green")
            {
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("L"); Rotate(2);
                moveList.Add("L"); Rotate(2);
            }
        }
        else
        {
            moveList.Add("F"); Rotate(3);
            moveList.Add("F"); Rotate(3);
        }
    }


    public List<string> cornerSolveCubies = new List<string>();

    //not really "the F2L or Fredrich"

    /*
	 * In order to pass this phase , four corner pieces need to be placed correctly
	 * in the first layer , these pieces names are the following:
	 * - Corner Piece 1
	 * - Corner Piece 2
	 * - Corner Piece 5
	 * - Corner Piece 7
	 * 
	 */
    private void solveF2L()
    {

        /* find and solve corner pieces that are not in position or not aligned properly (Up to 4 possible cases) */

        if (cube3[0, 2] != "Corner Piece 1")
        {
            cornerSolveCubies.Add("Corner Piece 1");
        }
        else
        {
            if (white[0, 2] != "white" && blue[0, 2] != "blue" && orange[0, 0] != "orange")
            {
                cornerSolveCubies.Add("Corner Piece 1");
            }
        }

        if (cube1[0, 2] != "Corner Piece 2")
        {
            cornerSolveCubies.Add("Corner Piece 2");
        }
        else
        {
            if (white[2, 2] != "white" && red[0, 2] != "red" && blue[0, 0] != "blue")
            {
                cornerSolveCubies.Add("Corner Piece 2");
            }
        }

        if (cube3[0, 0] != "Corner Piece 5")
        {
            cornerSolveCubies.Add("Corner Piece 5");
        }
        else
        {
            if (white[0, 0] != "white" && orange[0, 2] != "orange" && green[0, 0] != "green")
            {
                cornerSolveCubies.Add("Corner Piece 5");
            }
        }

        if (cube1[0, 0] != "Corner Piece 7")
        {
            cornerSolveCubies.Add("Corner Piece 7");
        }
        else
        {

            if ((white[2, 0] != "white") && (green[0, 2] != "green") && (red[0, 0] != "red"))
            {
                cornerSolveCubies.Add("Corner Piece 7");
            }
        }

        //algorithm to align the corner piece at the correct position (8 possible cases)

        while (cornerSolveCubies.Count > 0)
        {
            string piece = cornerSolveCubies[0];
            cornerSolveCubies.RemoveAt(0);
            //find piece location array
            string arrName = "";
            int x = -1, y = -1;
            bool flag = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cube1[i, j] == piece)
                    {
                        x = i;
                        y = j;
                        arrName = "cube1";
                        flag = true;
                        break;
                    }
                    else if (cube2[i, j] == piece)
                    {
                        x = i;
                        y = j;
                        arrName = "cube2";
                        flag = true;
                        break;
                    }
                    else if (cube3[i, j] == piece)
                    {
                        x = i;
                        y = j;
                        arrName = "cube3";
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    break;
            }

            // move the pieces to Red/Blue/Yellow Corner position
            if (arrName == "cube1")
            {
                if (x == 0 && y == 0)
                {
                    moveList.Add("L"); Rotate(2);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);

                }
                else if (x == 0 && y == 2)
                {
                    moveList.Add("F"); Rotate(3);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);

                }
                else if (x == 2 && y == 0)
                {
                    moveList.Add("D"); Rotate(4);
                }
                else if (x == 2 && y == 2)
                {
                    //already at the correct position
                }
            }
            else if (arrName == "cube3")
            {
                if (x == 0 && y == 0)
                {
                    moveList.Add("B"); Rotate(1);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);

                }
                else if (x == 0 && y == 2)
                {
                    moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("B"); Rotate(1);
                }
                else if (x == 2 && y == 0)
                {
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);

                }
                else if (x == 2 && y == 2)
                {
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                }
            }

            /* After placing the corner at the bottom Red/Blue/Yellow corner position , 
			this algorithm will move and insert the piece at the right place */

            if (piece == "Corner Piece 1")
            {
                moveList.Add("D"); Rotate(4);
                if (orange[2, 0] == "white")
                {
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("R"); Rotate(0);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);

                }
                else if (blue[2, 2] == "white")
                {
                    moveList.Add("R"); Rotate(0);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);

                }
                else if (yellow[2, 2] == "white")
                {
                    moveList.Add("R"); Rotate(0);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("R"); Rotate(0);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
                }
            }
            else if (piece == "Corner Piece 2")
            { //already at the correct position

                if (red[2, 2] == "white")
                {
                    moveList.Add("F"); Rotate(3);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                }
                else if (blue[2, 0] == "white")
                {
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("F"); Rotate(3);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                }
                else if (yellow[0, 2] == "white")
                {
                    moveList.Add("F"); Rotate(3);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("F"); Rotate(3);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                }
            }
            else if (piece == "Corner Piece 5")
            {
                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);

                if (green[2, 0] == "white")
                {
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("B"); Rotate(1);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);

                }
                else if (orange[2, 2] == "white")
                {
                    moveList.Add("B"); Rotate(1);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);


                }
                else if (yellow[2, 0] == "white")
                {
                    moveList.Add("B"); Rotate(1);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("B"); Rotate(1);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                }
            }
            else if (piece == "Corner Piece 7")
            {
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);

                if (red[2, 0] == "white")
                {
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("L"); Rotate(2);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);

                }
                else if (green[2, 2] == "white")
                {
                    moveList.Add("L"); Rotate(2);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);

                }
                else if (yellow[0, 0] == "white")
                {
                    moveList.Add("L"); Rotate(2);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    moveList.Add("L"); Rotate(2);
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                }
            }
        }

        /* solve edges of the second layer */

        /* there are 4 edges to be solved , can be found in 8 places
		  the next algorithm will find these pieces , get them in position and perform an algorithm
		  these pieces are : 
		  - Edge Piece 3
		  - Edge Piece 4
		  - Edge Piece 7
		  - Edge Piece 8
		*/

        // first to be solved are the edges in the correct position but missmatch colors

        if (cube3[1, 2] != "Edge Piece 3")
        {
            edgeSolveCubies.Add("Edge Piece 3");
            //Debug.Log("Edge 3 not in location");

        }
        else
        {
            if (blue[1, 2] != "blue" && orange[1, 0] != "orange")
            {
                moveList.Add("D"); Rotate(4);
                moveList.Add("R"); Rotate(0);
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                moveList.Add("D"); Rotate(4);
                moveList.Add("B"); Rotate(1);

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);

                moveList.Add("D"); Rotate(4);
                moveList.Add("R"); Rotate(0);
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
                moveList.Add("D"); Rotate(4);
                moveList.Add("B"); Rotate(1);
            }
        }

        if (cube1[1, 2] != "Edge Piece 4")
        {
            edgeSolveCubies.Add("Edge Piece 4");
            //Debug.Log("Red/Blue Edge not in location Edge Piece 4");
        }
        else
        {
            if (red[1, 2] != "red" && blue[1, 0] != "blue")
            {

                edgeRBInsertion(false);

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);

                edgeRBInsertion(false);
            }
        }

        if (cube1[1, 0] != "Edge Piece 7")
        {
            edgeSolveCubies.Add("Edge Piece 7");
            //Debug.Log("Edge 7 not in location");

        }
        else
        {
            if (green[1, 2] != "green" && red[1, 0] != "red")
            {
                moveList.Add("D"); Rotate(4);
                moveList.Add("L"); Rotate(2);
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                moveList.Add("D"); Rotate(4);
                moveList.Add("F"); Rotate(3);

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);

                moveList.Add("D"); Rotate(4);
                moveList.Add("L"); Rotate(2);
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
                moveList.Add("D"); Rotate(4);
                moveList.Add("F"); Rotate(3);
            }
        }

        if (cube3[1, 0] != "Edge Piece 8")
        {
            edgeSolveCubies.Add("Edge Piece 8");
            //Debug.Log("Edge 8 not in location");

        }
        else
        {
            if (orange[1, 2] != "orange" && green[1, 0] != "green")
            {
                moveList.Add("D"); Rotate(4);
                moveList.Add("B"); Rotate(1);
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                moveList.Add("D"); Rotate(4);
                moveList.Add("L"); Rotate(2);

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);

                moveList.Add("D"); Rotate(4);
                moveList.Add("B"); Rotate(1);
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
                moveList.Add("D"); Rotate(4);
                moveList.Add("L"); Rotate(2);
            }
        }

        //solve the edge pieces on top
        //Debug.Log ("Edges Count : " + edgeSolveCubies.Count);
        while (edgeSolveCubies.Count > 0)
        {
            //Debug.Log("Started Edge Solving while loop!");
            string piece = edgeSolveCubies[0];
            edgeSolveCubies.RemoveAt(0);

            //find piece
            string arrName = "";
            int x = -1, y = -1;
            if (cube1[2, 1] == piece)
            {
                x = 2; y = 1;
                arrName = "cube1";
            }
            else if (cube1[1, 0] == piece)
            {
                x = 1; y = 0;
                arrName = "cube1";
            }
            else if (cube1[1, 2] == piece)
            {
                x = 1; y = 2;
                arrName = "cube1";
            }
            else if (cube2[2, 2] == piece)
            {
                x = 2; y = 2;
                arrName = "cube2";
            }
            else if (cube2[2, 0] == piece)
            {
                x = 2; y = 0;
                arrName = "cube2";
            }
            else if (cube3[2, 1] == piece)
            {
                x = 2; y = 1;
                arrName = "cube3";
            }
            else if (cube3[1, 0] == piece)
            {
                x = 1; y = 0;
                arrName = "cube3";
            }
            else if (cube3[1, 2] == piece)
            {
                x = 1; y = 2;
                arrName = "cube3";
            }

            //Debug.Log("Array name obtained: " + arrName);
            //Debug.Log(x + " x <- |Values| y -> " + y);

            //move the piece to the correct position
            if (arrName == "cube1")
            {
                //Debug.Log(x + " x <- |cube1| y ->" + y);

                //if the piece is at the edges , move it to the yellow layer then perform check to move it to the correct position
                if (x == 1)
                {
                    if (y == 0)
                    {
                        //Debug.Log("Reversing GR");
                        edgeGRInsertion(true);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    }
                    else if (y == 2)
                    {
                        //Debug.Log("RB Insertion");
                        edgeRBInsertion(false);
                        moveList.Add("D"); Rotate(4);
                    }
                }

                if (red[2, 1] == "red")
                {
                    //at the correct position
                }
                else if (red[2, 1] == "blue")
                {
                    moveList.Add("D"); Rotate(4);
                }
                else if (red[2, 1] == "green")
                {
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                }
                else if (red[2, 1] == "orange")
                {
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                }
            }
            else if (arrName == "cube2")
            {
                //Debug.Log(x + " x <- |cube2| y ->" + y);

                if (x == 2 && y == 0)
                {
                    if (green[2, 1] == "red")
                    {
                        moveList.Add("D"); Rotate(4);
                    }
                    else if (green[2, 1] == "blue")
                    {
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("D"); Rotate(4);
                    }
                    else if (green[2, 1] == "green")
                    {
                        // at the correct position
                    }
                    else if (green[2, 1] == "orange")
                    {
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    }
                }
                else if (x == 2 && y == 2)
                {
                    if (blue[2, 1] == "red")
                    {
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    }
                    else if (blue[2, 1] == "blue")
                    {
                        //at the correct position
                    }
                    else if (blue[2, 1] == "green")
                    {
                        moveList.Add("D"); Rotate(4);
                        moveList.Add("D"); Rotate(4);
                    }
                    else if (blue[2, 1] == "orange")
                    {
                        moveList.Add("D"); Rotate(4);
                    }
                }
            }
            else if (arrName == "cube3")
            {
                //Debug.Log(x + " x <- |cube3| y ->" + y);
                //if the piece is at the edges , move it to the yellow layer then perform check to move it to the correct position
                if (x == 1)
                {
                    if (y == 0)
                    {
                        //Debug.Log("Solving Edge OG");
                        edgeOGInsertion(false);
                        moveList.Add("D"); Rotate(4);

                    }
                    else if (y == 2)
                    {
                        //Debug.Log("Reversing BO");

                        edgeBOInsertion(true);
                        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    }
                }

                if (orange[2, 1] == "red")
                {
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                }
                else if (orange[2, 1] == "blue")
                {
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                }
                else if (orange[2, 1] == "green")
                {
                    moveList.Add("D"); Rotate(4);
                }
                else if (orange[2, 1] == "orange")
                {
                    //at the correct position
                }
            }

            // piece at the correct place , use edge insertion algorithm to insert it
            if (piece == "Edge Piece 3")
            {
                if (cube3[2, 1] == piece)
                {
                    edgeBOInsertion(false);
                }
                else if (cube2[2, 2] == piece)
                {
                    edgeBOInsertion(true);
                }
            }
            else if (piece == "Edge Piece 4")
            {
                if (cube1[2, 1] == piece)
                {
                    edgeRBInsertion(true);
                }
                else if (cube2[2, 2] == piece)
                {
                    edgeRBInsertion(false);
                }
            }
            else if (piece == "Edge Piece 7")
            {
                if (cube1[2, 1] == piece)
                {
                    edgeGRInsertion(false);
                }
                else if (cube2[2, 0] == piece)
                {
                    edgeGRInsertion(true);
                }
            }
            else if (piece == "Edge Piece 8")
            {
                if (cube3[2, 1] == piece)
                {
                    edgeOGInsertion(true);
                }
                else if (cube2[2, 0] == piece)
                {
                    edgeOGInsertion(false);
                }
            }
        }
    }

    /* Four insertion algorithms for each edge in the second layer (reusable)*/

    private void edgeRBInsertion(bool reverse)
    {
        if (!reverse)
        {
            moveList.Add("D"); Rotate(4);
            moveList.Add("F"); Rotate(3);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);

            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
            moveList.Add("D"); Rotate(4);
            moveList.Add("R"); Rotate(0);

        }
        else
        {
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
            moveList.Add("D"); Rotate(4);
            moveList.Add("R"); Rotate(0);

            moveList.Add("D"); Rotate(4);
            moveList.Add("F"); Rotate(3);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
        }
    }

    private void edgeBOInsertion(bool reverse)
    {
        if (!reverse)
        {
            moveList.Add("D"); Rotate(4);
            moveList.Add("R"); Rotate(0);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);

            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
            moveList.Add("D"); Rotate(4);
            moveList.Add("B"); Rotate(1);
        }
        else
        {
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
            moveList.Add("D"); Rotate(4);
            moveList.Add("B"); Rotate(1);

            moveList.Add("D"); Rotate(4);
            moveList.Add("R"); Rotate(0);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
        }
    }

    private void edgeOGInsertion(bool reverse)
    {
        if (!reverse)
        {
            moveList.Add("D"); Rotate(4);
            moveList.Add("B"); Rotate(1);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);

            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
            moveList.Add("D"); Rotate(4);
            moveList.Add("L"); Rotate(2);
        }
        else
        {
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
            moveList.Add("D"); Rotate(4);
            moveList.Add("L"); Rotate(2);

            moveList.Add("D"); Rotate(4);
            moveList.Add("B"); Rotate(1);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-B"); Rotate(1); Rotate(1); Rotate(1);
        }
    }

    private void edgeGRInsertion(bool reverse)
    {
        if (!reverse)
        {
            moveList.Add("D"); Rotate(4);
            moveList.Add("L"); Rotate(2);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);

            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
            moveList.Add("D"); Rotate(4);
            moveList.Add("F"); Rotate(3);
        }
        else
        {
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
            moveList.Add("D"); Rotate(4);
            moveList.Add("F"); Rotate(3);

            moveList.Add("D"); Rotate(4);
            moveList.Add("L"); Rotate(2);
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
        }
    }


    /* Orientation of the last layer */
    private void solveOLL()
    {
        /* This stage's objective is to cement the yellow face with yellow tiles
		 * by using a variety of algorithms , for this purpose I'm using the well known
		 * 2 Look OLL in order to pass this stage.
		 * 
		 * The algorithm starts by solving the yellow cross by correctly orienting the yellow face yellow edge tiles ,
		 * next comes the corner orientation which is the second step of this algorithm in order for it to complete this stage.
		 */

        /* Edge Orientation */

        // Checking 4 cases , 1 which is correct that would be the yellow cross , the rest should be moved and solved.
        int yellowCount = 0;

        if (yellow[0, 1] == "yellow" && yellow[1, 0] == "yellow" && yellow[2, 1] == "yellow" && yellow[1, 2] == "yellow")
        {
            //already yellow cross
            //Debug.Log("Yellow Cross Case");
        }
        else
        {
            //check 3 different states , move them to the correct position , perform algorithm to solve.

            if (yellow[0, 1] == "yellow") ++yellowCount;
            if (yellow[1, 0] == "yellow") ++yellowCount;
            if (yellow[2, 1] == "yellow") ++yellowCount;
            if (yellow[1, 2] == "yellow") ++yellowCount;

            //Debug.Log("Yellow Count : " + yellowCount);
            if (yellowCount > 1)
            {
                if (yellow[0, 1] == "yellow" && yellow[2, 1] == "yellow")
                {
                    //Debug.Log("Case Vertical Line");
                    yellowCrossEdgeAlgo();
                }
                else if (yellow[1, 0] == "yellow" && yellow[1, 2] == "yellow")
                {
                    //Debug.Log("Case Horizontal Line");
                    moveList.Add("D"); Rotate(4);
                    yellowCrossEdgeAlgo();
                }
                else if (yellow[0, 1] == "yellow" && yellow[1, 0] == "yellow")
                {
                    //Debug.Log("Case Upper Left Twist");
                    moveList.Add("D"); Rotate(4);
                    yellowCrossEdgeAlgo();
                    moveList.Add("D"); Rotate(4);
                    yellowCrossEdgeAlgo();
                }
                else if (yellow[2, 1] == "yellow" && yellow[1, 0] == "yellow")
                {
                    //Debug.Log("Case Lower Left Twist");
                    moveList.Add("D"); Rotate(4);
                    moveList.Add("D"); Rotate(4);
                    yellowCrossEdgeAlgo();
                    moveList.Add("D"); Rotate(4);
                    yellowCrossEdgeAlgo();
                }
                else if (yellow[2, 1] == "yellow" && yellow[1, 2] == "yellow")
                {
                    //Debug.Log("Case Lower Right Twist");
                    moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                    yellowCrossEdgeAlgo();
                    moveList.Add("D"); Rotate(4);
                    yellowCrossEdgeAlgo();
                }
                else if (yellow[0, 1] == "yellow" && yellow[1, 2] == "yellow")
                {
                    //Debug.Log("Case Upper Right Twist");
                    yellowCrossEdgeAlgo();
                    moveList.Add("D"); Rotate(4);
                    yellowCrossEdgeAlgo();
                }
            }
            else
            {
                //Debug.Log("Case 1 Yellow Center piece");
                yellowCrossEdgeAlgo();
                yellowCrossEdgeAlgo();
                moveList.Add("D"); Rotate(4);
                yellowCrossEdgeAlgo();
            }
        }

        //Debug.Log("Done Edge Orientation step....starting corner orientation step..");

        // at this stage there are 7 cases , the next algorithm will position them correctly and then do an algorithm to solve it

        yellowCount = 0;
        string y = "yellow";

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (yellow[i, j] == y)
                    yellowCount++;


        //Debug.Log("Corner Yellow Count : " + yellowCount);

        if (yellowCount == 5)
        {
            //Debug.Log("Entered where Yellow Count is 5");
            // Car case
            if (red[2, 0] == y && red[2, 2] == y && orange[2, 0] == y && orange[2, 2] == y)
            {
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                yellowCrossCornerAlgo1();
                //Debug.Log("Car Case Red/Orange"); //checked , working!
            }
            else if (green[2, 0] == y && green[2, 2] == y && blue[2, 0] == y && blue[2, 2] == y)
            {
                //correct
                yellowCrossCornerAlgo1();
                yellowCrossCornerAlgo1();
                //Debug.Log("Car Case Green/Blue");
            }

            // Blinker case
            else if (red[2, 0] == y && red[2, 2] == y)
            {
                //Debug.Log("Blinker Case 1");

                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
            }
            else if (blue[2, 0] == y && blue[2, 2] == y)
            {
                //Debug.Log("Blinker Case 2"); //checked , working!

                //correct
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
            }
            else if (orange[2, 0] == y && orange[2, 2] == y)
            {
                //Debug.Log("Blinker Case 3"); //checked , working!

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
            }
            else if (green[2, 0] == y && green[2, 2] == y)
            {
                //Debug.Log("Blinker Case 4");

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
            }
        }
        else if (yellowCount == 6)
        {
            //Debug.Log("Entered where Yellow Count is 6");

            // Sune case
            if (blue[2, 0] == y && orange[2, 0] == y && green[2, 0] == y)
            {
                //Debug.Log("Sune Case 1"); //checked , working!

                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
            }
            else if (red[2, 0] == y && orange[2, 0] == y && green[2, 0] == y)
            {
                //Debug.Log("Sune Case 2");

                //correct
                yellowCrossCornerAlgo1();
            }
            else if (blue[2, 0] == y && red[2, 0] == y && green[2, 0] == y)
            {
                //Debug.Log("Sune Case 3");

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();

            }
            else if (blue[2, 0] == y && orange[2, 0] == y && red[2, 0] == y)
            {
                //Debug.Log("Sune Case 4"); 

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
            }

            // Anti-Sune case
            else if (blue[2, 2] == y && orange[2, 2] == y && green[2, 2] == y)
            {
                //Debug.Log("Anti-Sune Case 1"); //checked , working!

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo2();
            }
            else if (red[2, 2] == y && orange[2, 2] == y && green[2, 2] == y)
            {
                //Debug.Log("Anti-Sune Case 2");

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo2();
            }
            else if (blue[2, 2] == y && orange[2, 2] == y && red[2, 2] == y)
            {
                //Debug.Log("Anti-Sune Case 3"); //checked , working!

                //correct
                yellowCrossCornerAlgo2();
            }
            else if (blue[2, 2] == y && red[2, 2] == y && green[2, 2] == y)
            {
                //Debug.Log("Anti-Sune Case 4");

                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo2();
            }

        }
        else if (yellowCount == 7)
        {
            //Debug.Log("Entered where Yellow Count is 7");

            // Headlight case
            if (red[2, 0] == y && red[2, 2] == y)
            {
                //Debug.Log("Headlight Case 1"); //checked , working properly

                //correct
                yellowCrossCornerAlgo1();
                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo2();
            }
            else if (blue[2, 0] == y && blue[2, 2] == y)
            {
                //Debug.Log("Headlight Case 2"); //checked , working properly

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo2();
            }
            else if (orange[2, 0] == y && orange[2, 2] == y)
            {
                //Debug.Log("Headlight Case 3");

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo2();
            }
            else if (green[2, 0] == y && green[2, 2] == y)
            {
                //Debug.Log("Headlight Case 4");

                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo2();
            }

            // Chameleon case
            else if (red[2, 2] == y && orange[2, 0] == y)
            {
                //Debug.Log("Chameleon Case 1");

                //correct
                yellowCrossCornerAlgo1();
                yellowCrossCornerAlgo2();
            }
            else if (blue[2, 2] == y && green[2, 0] == y)
            {
                //Debug.Log("Chameleon Case 2"); //checked , working properly

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
                yellowCrossCornerAlgo2();
            }
            else if (red[2, 0] == y && orange[2, 2] == y)
            {
                //Debug.Log("Chameleon Case 3");

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                yellowCrossCornerAlgo2();
            }
            else if (blue[2, 0] == y && green[2, 2] == y)
            {
                //Debug.Log("Chameleon Case 4"); //checked , working properly

                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                yellowCrossCornerAlgo2();
            }
            // Bowtie case
            else if (red[2, 2] == y && green[2, 0] == y)
            {
                //Debug.Log("Bowtie Case 1");

                //correct
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo2();
            }
            else if (red[2, 0] == y && blue[2, 2] == y)
            {
                //Debug.Log("Bowtie Case 2");

                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo2();

            }
            else if (blue[2, 0] == y && orange[2, 2] == y)
            {
                //Debug.Log("Bowtie Case 3");

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo2();

            }
            else if (orange[2, 0] == y && green[2, 2] == y)
            {
                //Debug.Log("Bowtie Case 4"); //checked , working!

                moveList.Add("D"); Rotate(4);
                yellowCrossCornerAlgo1();
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                yellowCrossCornerAlgo2();
            }
        }
    }

    private void yellowCrossEdgeAlgo()
    {
        moveList.Add("R"); Rotate(0);
        moveList.Add("F"); Rotate(3);
        moveList.Add("D"); Rotate(4);
        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
    }

    private void yellowCrossCornerAlgo1()
    {
        moveList.Add("L"); Rotate(2);
        moveList.Add("D"); Rotate(4);
        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
        moveList.Add("D"); Rotate(4);
        moveList.Add("L"); Rotate(2);
        moveList.Add("D"); Rotate(4);
        moveList.Add("D"); Rotate(4);
        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
    }

    private void yellowCrossCornerAlgo2()
    {
        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        moveList.Add("R"); Rotate(0);
        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        moveList.Add("R"); Rotate(0);
    }

    /* Permutation of the last layer */
    private void solvePLL()
    {
        /* This is the last stage to be done in order to solve the cube, permutation of the last layer.
         * After the OLL phase the last layer will be done in several steps , crucial checks needs to be done in order to figure out which algorithms to use*/

        // Level 1 Check (Corners)
        if (red[2, 0] != red[2, 2] && blue[2, 0] != blue[2, 2] && orange[2, 0] != orange[2, 2] && green[2, 0] != green[2, 2])
        {
            //find corner piece 4 and move it to the correct position
            if (cube1[2, 0] == "Corner Piece 4")
            {
                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
            }
            else if (cube1[2, 2] == "Corner Piece 4")
            {
                moveList.Add("D"); Rotate(4);
            }
            else if (cube3[0, 2] == "Corner Piece 4")
            {
                //correct
            }
            else if (cube3[2, 2] == "Corner Piece 4")
            {
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            }

            cornerPermutationAlgorithm();
        }

        // Level 2 Check (Corners)
        if (red[2, 0] == red[2, 2] && blue[2, 0] != blue[2, 2])
        {
            moveList.Add("D"); Rotate(4);
            moveList.Add("D"); Rotate(4);
            cornerPermutationAlgorithm();
        }
        else if (blue[2, 0] == blue[2, 2] && orange[2, 0] != orange[2, 2])
        {
            moveList.Add("D"); Rotate(4);
            cornerPermutationAlgorithm();
        }
        else if (orange[2, 0] == orange[2, 2] && green[2, 0] != green[2, 2])
        {
            //correct
            cornerPermutationAlgorithm();
        }
        else if (green[2, 0] == green[2, 2] && red[2, 0] != red[2, 2])
        {
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
            cornerPermutationAlgorithm();
        }

        // Level 3 Check (Edges)
        if (red[2, 0] == red[2, 2] && red[2, 0] != red[2, 1] &&
            blue[2, 0] == blue[2, 2] && blue[2, 0] != blue[2, 1] &&
            orange[2, 0] == orange[2, 2] && orange[2, 0] != orange[2, 1] &&
            green[2, 0] == green[2, 2] && green[2, 0] != green[2, 1])
        { //fix center tile check on each ledge side
            //Debug.Log("Solving Level 3");
            edgePermutationAlgorithm1();
        }

        // Level 4 Check (Edges)
        if (red[2, 0] == red[2, 2] && red[2, 0] == red[2, 1] &&
            blue[2, 0] == blue[2, 2] && blue[2, 0] == blue[2, 1] &&
            orange[2, 0] == orange[2, 2] && orange[2, 0] == orange[2, 1] &&
            green[2, 0] == green[2, 2] && green[2, 0] == green[2, 1])
        {
            //Debug.Log("Level 4 Final Turn");
            finalTurn();
        }
        else
        {
            if (red[2, 0] == red[2, 1] && red[2, 0] == red[2, 2])
            {
                //Debug.Log("red side edge permutation");

                moveList.Add("D"); Rotate(4);
                moveList.Add("D"); Rotate(4);
                if (red[2, 1] == blue[2, 0])
                {
                    edgePermutationAlgorithm1();
                }
                else
                {
                    edgePermutationAlgorithm2();
                }
            }
            else if (blue[2, 0] == blue[2, 1] && blue[2, 0] == blue[2, 2])
            {
                moveList.Add("D"); Rotate(4);
                //Debug.Log("Blue side edge permutation");

                if (red[2, 1] == blue[2, 0])
                {
                    edgePermutationAlgorithm1();
                }
                else
                {
                    edgePermutationAlgorithm2();
                }
            }
            else if (orange[2, 0] == orange[2, 1] && orange[2, 0] == orange[2, 2])
            {
                //correct
                //Debug.Log("Orange side edge permutation");

                if (red[2, 1] == blue[2, 0])
                {
                    edgePermutationAlgorithm1();
                }
                else
                {
                    edgePermutationAlgorithm2();
                }
            }
            else if (green[2, 0] == green[2, 1] && green[2, 0] == green[2, 2])
            {
                //Debug.Log("Green side edge permutation");
                moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
                if (red[2, 1] == blue[2, 0])
                {
                    edgePermutationAlgorithm1();
                }
                else
                {
                    edgePermutationAlgorithm2();
                }
            }
        }


        // Level 5 Check (Final)
        finalTurn();
    }

    private void finalTurn()
    {
        //Debug.Log("final Turn");
        if (red[2, 1] == "red")
        {
            //correct position (solved)
        }
        else if (red[2, 1] == "blue")
        {
            moveList.Add("D"); Rotate(4);
        }
        else if (red[2, 1] == "orange")
        {
            moveList.Add("D"); Rotate(4);
            moveList.Add("D"); Rotate(4);
        }
        else if (red[2, 1] == "green")
        {
            moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        }
    }

    private void cornerPermutationAlgorithm()
    {
        //Debug.Log("Corner Permutation");
        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
        moveList.Add("F"); Rotate(3);
        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
        moveList.Add("B"); Rotate(1);
        moveList.Add("B"); Rotate(1);
        moveList.Add("L"); Rotate(2);
        moveList.Add("-F"); Rotate(3); Rotate(3); Rotate(3);
        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
        moveList.Add("B"); Rotate(1);
        moveList.Add("B"); Rotate(1);
        moveList.Add("L"); Rotate(2);
        moveList.Add("L"); Rotate(2);
    }


    private void edgePermutationAlgorithm1()
    {
        //Debug.Log("Edge Permutation 1");
        moveList.Add("F"); Rotate(3);
        moveList.Add("F"); Rotate(3);
        moveList.Add("D"); Rotate(4);
        moveList.Add("R"); Rotate(0);
        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
        moveList.Add("F"); Rotate(3);
        moveList.Add("F"); Rotate(3);
        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
        moveList.Add("L"); Rotate(2);
        moveList.Add("D"); Rotate(4);
        moveList.Add("F"); Rotate(3);
        moveList.Add("F"); Rotate(3);
    }


    private void edgePermutationAlgorithm2()
    {
        //Debug.Log("Edge Permutation 2");
        moveList.Add("F"); Rotate(3);
        moveList.Add("F"); Rotate(3);
        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        moveList.Add("R"); Rotate(0);
        moveList.Add("-L"); Rotate(2); Rotate(2); Rotate(2);
        moveList.Add("F"); Rotate(3);
        moveList.Add("F"); Rotate(3);
        moveList.Add("-R"); Rotate(0); Rotate(0); Rotate(0);
        moveList.Add("L"); Rotate(2);
        moveList.Add("-D"); Rotate(4); Rotate(4); Rotate(4);
        moveList.Add("F"); Rotate(3);
        moveList.Add("F"); Rotate(3);
    }


    string getColorFromChar(char color)
    {
        if (color == 'w')
            return "white";
        if (color == 'b')
            return "blue";
        if (color == 'r')
            return "red";
        if (color == 'g')
            return "green";
        if (color == 'o')
            return "orange";
        if (color == 'y')
            return "yellow";
        return "black";
    }

    public string getCubeletNumber(string cubeName,int cubeX, int cubeY)
    {
        if (cubeName == "cube1")
        {
            //corners of cube1
            if ((cubeX == 0) && (cubeY == 0))
            {
                //corners of redd face(F) and adjacent ones
                if (((redd[0, 0] == "red") || (redd[0, 0] == "white") || (redd[0, 0] == "green")) && ((whitee[2, 0] == "red") || (whitee[2, 0] == "white") || (whitee[2, 0] == "green")) && ((greenn[0, 2] == "red") || (greenn[0, 2] == "white") || (greenn[0, 2] == "green")))
                    return "Corner Piece 7";

                if (((redd[0, 0] == "red") || (redd[0, 0] == "white") || (redd[0, 0] == "blue")) && ((whitee[2, 0] == "red") || (whitee[2, 0] == "white") || (whitee[2, 0] == "blue")) && ((greenn[0, 2] == "red") || (greenn[0, 2] == "white") || (greenn[0, 2] == "blue")))
                    return "Corner Piece 2";

                if (((redd[0, 0] == "red") || (redd[0, 0] == "yellow") || (redd[0, 0] == "blue")) && ((whitee[2, 0] == "red") || (whitee[2, 0] == "yellow") || (whitee[2, 0] == "blue")) && ((greenn[0, 2] == "red") || (greenn[0, 2] == "yellow") || (greenn[0, 2] == "blue")))
                    return "Corner Piece 3";

                if (((redd[0, 0] == "red") || (redd[0, 0] == "yellow") || (redd[0, 0] == "green")) && ((whitee[2, 0] == "red") || (whitee[2, 0] == "yellow") || (whitee[2, 0] == "green")) && ((greenn[0, 2] == "red") || (greenn[0, 2] == "yellow") || (greenn[0, 2] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((redd[0, 0] == "orange") || (redd[0, 0] == "white") || (redd[0, 0] == "green")) && ((whitee[2, 0] == "orange") || (whitee[2, 0] == "white") || (whitee[2, 0] == "green")) && ((greenn[0, 2] == "orange") || (greenn[0, 2] == "white") || (greenn[0, 2] == "green")))
                    return "Corner Piece 5";

                if (((redd[0, 0] == "orange") || (redd[0, 0] == "white") || (redd[0, 0] == "blue")) && ((whitee[2, 0] == "orange") || (whitee[2, 0] == "white") || (whitee[2, 0] == "blue")) && ((greenn[0, 2] == "orange") || (greenn[0, 2] == "white") || (greenn[0, 2] == "blue")))
                    return "Corner Piece 1";

                if (((redd[0, 0] == "orange") || (redd[0, 0] == "yellow") || (redd[0, 0] == "blue")) && ((whitee[2, 0] == "orange") || (whitee[2, 0] == "yellow") || (whitee[2, 0] == "blue")) && ((greenn[0, 2] == "orange") || (greenn[0, 2] == "yellow") || (greenn[0, 2] == "blue")))
                    return "Corner Piece 4";

                if (((redd[0, 0] == "orange") || (redd[0, 0] == "yellow") || (redd[0, 0] == "green")) && ((whitee[2, 0] == "orange") || (whitee[2, 0] == "yellow") || (whitee[2, 0] == "green")) && ((greenn[0, 2] == "orange") || (greenn[0, 2] == "yellow") || (greenn[0, 2] == "green")))
                    return "Corner Piece 6";
            }


            //////////////////////////////////////////////////////////////////
            if ((cubeX == 0) && (cubeY == 2))
            {
                //corners of redd face(F) and adjacent ones
                if (((redd[0, 2] == "red") || (redd[0, 2] == "white") || (redd[0, 2] == "green")) && ((whitee[2, 2] == "red") || (whitee[2, 2] == "white") || (whitee[2, 2] == "green")) && ((bluee[0, 0] == "red") || (bluee[0, 0] == "white") || (bluee[0, 0] == "green")))
                    return "Corner Piece 7";

                if (((redd[0, 2] == "red") || (redd[0, 2] == "white") || (redd[0, 2] == "blue")) && ((whitee[2, 2] == "red") || (whitee[2, 2] == "white") || (whitee[2, 2] == "blue")) && ((bluee[0, 0] == "red") || (bluee[0, 0] == "white") || (bluee[0, 0] == "blue")))
                    return "Corner Piece 2";

                if (((redd[0, 2] == "red") || (redd[0, 2] == "yellow") || (redd[0, 2] == "blue")) && ((whitee[2, 2] == "red") || (whitee[2, 2] == "yellow") || (whitee[2, 2] == "blue")) && ((bluee[0, 0] == "red") || (bluee[0, 0] == "yellow") || (bluee[0, 0] == "blue")))
                    return "Corner Piece 3";

                if (((redd[0, 2] == "red") || (redd[0, 2] == "yellow") || (redd[0, 2] == "green")) && ((whitee[2, 2] == "red") || (whitee[2, 2] == "yellow") || (whitee[2, 2] == "green")) && ((bluee[0, 0] == "red") || (bluee[0, 0] == "yellow") || (bluee[0, 0] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((redd[0, 2] == "orange") || (redd[0, 2] == "white") || (redd[0, 2] == "green")) && ((whitee[2, 2] == "orange") || (whitee[2, 2] == "white") || (whitee[2, 2] == "green")) && ((bluee[0, 0] == "orange") || (bluee[0, 0] == "white") || (bluee[0, 0] == "green")))
                    return "Corner Piece 5";

                if (((redd[0, 2] == "orange") || (redd[0, 2] == "white") || (redd[0, 2] == "blue")) && ((whitee[2, 2] == "orange") || (whitee[2, 2] == "white") || (whitee[2, 2] == "blue")) && ((bluee[0, 0] == "orange") || (bluee[0, 0] == "white") || (bluee[0, 0] == "blue")))
                    return "Corner Piece 1";

                if (((redd[0, 2] == "orange") || (redd[0, 2] == "yellow") || (redd[0, 2] == "blue")) && ((whitee[2, 2] == "orange") || (whitee[2, 2] == "yellow") || (whitee[2, 2] == "blue")) && ((bluee[0, 0] == "orange") || (bluee[0, 0] == "yellow") || (bluee[0, 0] == "blue")))
                    return "Corner Piece 4";

                if (((redd[0, 2] == "orange") || (redd[0, 2] == "yellow") || (redd[0, 2] == "green")) && ((whitee[2, 2] == "orange") || (whitee[2, 2] == "yellow") || (whitee[2, 2] == "green")) && ((bluee[0, 0] == "orange") || (bluee[0, 0] == "yellow") || (bluee[0, 0] == "green")))
                    return "Corner Piece 6";
            }

            ////////////////////////////////////////////////////////
            if ((cubeX == 2) && (cubeY == 0))
            {
                if (((redd[2, 0] == "red") || (redd[2, 0] == "white") || (redd[2, 0] == "green")) && ((yelloww[0, 0] == "red") || (yelloww[0, 0] == "white") || (yelloww[0, 0] == "green")) && ((greenn[2, 2] == "red") || (greenn[2, 2] == "white") || (greenn[2, 2] == "green")))
                    return "Corner Piece 7";

                if (((redd[2, 0] == "red") || (redd[2, 0] == "white") || (redd[2, 0] == "blue")) && ((yelloww[0, 0] == "red") || (yelloww[0, 0] == "white") || (yelloww[0, 0] == "blue")) && ((greenn[2, 2] == "red") || (greenn[2, 2] == "white") || (greenn[2, 2] == "blue")))
                    return "Corner Piece 2";

                if (((redd[2, 0] == "red") || (redd[2, 0] == "yellow") || (redd[2, 0] == "blue")) && ((yelloww[0, 0] == "red") || (yelloww[0, 0] == "yellow") || (yelloww[0, 0] == "blue")) && ((greenn[2, 2] == "red") || (greenn[2, 2] == "yellow") || (greenn[2, 2] == "blue")))
                    return "Corner Piece 3";

                if (((redd[2, 0] == "red") || (redd[2, 0] == "yellow") || (redd[2, 0] == "green")) && ((yelloww[0, 0] == "red") || (yelloww[0, 0] == "yellow") || (yelloww[0, 0] == "green")) && ((greenn[2, 2] == "red") || (greenn[2, 2] == "yellow") || (greenn[2, 2] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((redd[2, 0] == "orange") || (redd[2, 0] == "white") || (redd[2, 0] == "green")) && ((yelloww[0, 0] == "orange") || (yelloww[0, 0] == "white") || (yelloww[0, 0] == "green")) && ((greenn[2, 2] == "orange") || (greenn[2, 2] == "white") || (greenn[2, 2] == "green")))
                    return "Corner Piece 5";

                if (((redd[2, 0] == "orange") || (redd[2, 0] == "white") || (redd[2, 0] == "blue")) && ((yelloww[0, 0] == "orange") || (yelloww[0, 0] == "white") || (yelloww[0, 0] == "blue")) && ((greenn[2, 2] == "orange") || (greenn[2, 2] == "white") || (greenn[2, 2] == "blue")))
                    return "Corner Piece 1";

                if (((redd[2, 0] == "orange") || (redd[2, 0] == "yellow") || (redd[2, 0] == "blue")) && ((yelloww[0, 0] == "orange") || (yelloww[0, 0] == "yellow") || (yelloww[0, 0] == "blue")) && ((greenn[2, 2] == "orange") || (greenn[2, 2] == "yellow") || (greenn[2, 2] == "blue")))
                    return "Corner Piece 4";

                if (((redd[2, 0] == "orange") || (redd[2, 0] == "yellow") || (redd[2, 0] == "green")) && ((yelloww[0, 0] == "orange") || (yelloww[0, 0] == "yellow") || (yelloww[0, 0] == "green")) && ((greenn[2, 2] == "orange") || (greenn[2, 2] == "yellow") || (greenn[2, 2] == "green")))
                    return "Corner Piece 6";
            }

            /////////////////////////////////////////
            if ((cubeX == 2) && (cubeY == 2))
            {
                if (((redd[2, 2] == "red") || (redd[2, 2] == "white") || (redd[2, 2] == "green")) && ((yelloww[0, 2] == "red") || (yelloww[0, 2] == "white") || (yelloww[0, 2] == "green")) && ((bluee[2, 0] == "red") || (bluee[2, 0] == "white") || (bluee[2, 0] == "green")))
                    return "Corner Piece 7";

                if (((redd[2, 2] == "red") || (redd[2, 2] == "white") || (redd[2, 2] == "blue")) && ((yelloww[0, 2] == "red") || (yelloww[0, 2] == "white") || (yelloww[0, 2] == "blue")) && ((bluee[2, 0] == "red") || (bluee[2, 0] == "white") || (bluee[2, 0] == "blue")))
                    return "Corner Piece 2";

                if (((redd[2, 2] == "red") || (redd[2, 2] == "yellow") || (redd[2, 2] == "blue")) && ((yelloww[0, 2] == "red") || (yelloww[0, 2] == "yellow") || (yelloww[0, 2] == "blue")) && ((bluee[2, 0] == "red") || (bluee[2, 0] == "yellow") || (bluee[2, 0] == "blue")))
                    return "Corner Piece 3";

                if (((redd[2, 2] == "red") || (redd[2, 2] == "yellow") || (redd[2, 2] == "green")) && ((yelloww[0, 2] == "red") || (yelloww[0, 2] == "yellow") || (yelloww[0, 2] == "green")) && ((bluee[2, 0] == "red") || (bluee[2, 0] == "yellow") || (bluee[2, 0] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((redd[2, 2] == "orange") || (redd[2, 2] == "white") || (redd[2, 2] == "green")) && ((yelloww[0, 2] == "orange") || (yelloww[0, 2] == "white") || (yelloww[0, 2] == "green")) && ((bluee[2, 0] == "orange") || (bluee[2, 0] == "white") || (bluee[2, 0] == "green")))
                    return "Corner Piece 5";

                if (((redd[2, 2] == "orange") || (redd[2, 2] == "white") || (redd[2, 2] == "blue")) && ((yelloww[0, 2] == "orange") || (yelloww[0, 2] == "white") || (yelloww[0, 2] == "blue")) && ((bluee[2, 0] == "orange") || (bluee[2, 0] == "white") || (bluee[2, 0] == "blue")))
                    return "Corner Piece 1";

                if (((redd[2, 2] == "orange") || (redd[2, 2] == "yellow") || (redd[2, 2] == "blue")) && ((yelloww[0, 2] == "orange") || (yelloww[0, 2] == "yellow") || (yelloww[0, 2] == "blue")) && ((bluee[2, 0] == "orange") || (bluee[2, 0] == "yellow") || (bluee[2, 0] == "blue")))
                    return "Corner Piece 4";

                if (((redd[2, 2] == "orange") || (redd[2, 2] == "yellow") || (redd[2, 2] == "green")) && ((yelloww[0, 2] == "orange") || (yelloww[0, 2] == "yellow") || (yelloww[0, 2] == "green")) && ((bluee[2, 0] == "orange") || (bluee[2, 0] == "yellow") || (bluee[2, 0] == "green")))
                    return "Corner Piece 6";
            }

            //edges of cube1
            if ((cubeX == 0) && (cubeY == 1))
            {
                if (((redd[0, 1] == "red") || (redd[0, 1] == "white")) && ((whitee[2, 1] == "red") || (whitee[2, 1] == "white")))
                    return "Edge Piece 12";

                if (((redd[0, 1] == "red") || (redd[0, 1] == "blue")) && ((whitee[2, 1] == "red") || (whitee[2, 1] == "blue")))
                    return "Edge Piece 4";

                if (((redd[0, 1] == "red") || (redd[0, 1] == "yellow")) && ((whitee[2, 1] == "red") || (whitee[2, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((redd[0, 1] == "red") || (redd[0, 1] == "green")) && ((whitee[2, 1] == "red") || (whitee[2, 1] == "green")))
                    return "Edge Piece 7";


                if (((redd[0, 1] == "orange") || (redd[0, 1] == "white")) && ((whitee[2, 1] == "orange") || (whitee[2, 1] == "white")))
                    return "Edge Piece 9";

                if (((redd[0, 1] == "orange") || (redd[0, 1] == "blue")) && ((whitee[2, 1] == "orange") || (whitee[2, 1] == "blue")))
                    return "Edge Piece 3";

                if (((redd[0, 1] == "orange") || (redd[0, 1] == "yellow")) && ((whitee[2, 1] == "orange") || (whitee[2, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((redd[0, 1] == "orange") || (redd[0, 1] == "green")) && ((whitee[2, 1] == "orange") || (whitee[2, 1] == "green")))
                    return "Edge Piece 8";


                if (((redd[0, 1] == "blue") || (redd[0, 1] == "white")) && ((whitee[2, 1] == "blue") || (whitee[2, 1] == "white")))
                    return "Edge Piece 1";

                if (((redd[0, 1] == "yellow") || (redd[0, 1] == "blue")) && ((whitee[2, 1] == "yellow") || (whitee[2, 1] == "blue")))
                    return "Edge Piece 2";

                if (((redd[0, 1] == "white") || (redd[0, 1] == "green")) && ((whitee[2, 1] == "white") || (whitee[2, 1] == "green")))
                    return "Edge Piece 5";

                if (((redd[0, 1] == "yellow") || (redd[0, 1] == "green")) && ((whitee[2, 1] == "yellow") || (whitee[2, 1] == "green")))
                    return "Edge Piece 6";
            }
            ///////////////////////////////////////////////////////////////
            if ((cubeX == 1) && (cubeY == 2))
            {
                if (((redd[1, 2] == "red") || (redd[1, 2] == "white")) && ((bluee[1, 0] == "red") || (bluee[1, 0] == "white")))
                    return "Edge Piece 12";

                if (((redd[1, 2] == "red") || (redd[1, 2] == "blue")) && ((bluee[1, 0] == "red") || (bluee[1, 0] == "blue")))
                    return "Edge Piece 4";

                if (((redd[1, 2] == "red") || (redd[1, 2] == "yellow")) && ((bluee[1, 0] == "red") || (bluee[1, 0] == "yellow")))
                    return "Edge Piece 11";

                if (((redd[1, 2] == "red") || (redd[1, 2] == "green")) && ((bluee[1, 0] == "red") || (bluee[1, 0] == "green")))
                    return "Edge Piece 7";


                if (((redd[1, 2] == "orange") || (redd[1, 2] == "white")) && ((bluee[1, 0] == "orange") || (bluee[1, 0] == "white")))
                    return "Edge Piece 9";

                if (((redd[1, 2] == "orange") || (redd[1, 2] == "blue")) && ((bluee[1, 0] == "orange") || (bluee[1, 0] == "blue")))
                    return "Edge Piece 3";

                if (((redd[1, 2] == "orange") || (redd[1, 2] == "yellow")) && ((bluee[1, 0] == "orange") || (bluee[1, 0] == "yellow")))
                    return "Edge Piece 10";

                if (((redd[1, 2] == "orange") || (redd[1, 2] == "green")) && ((bluee[1, 0] == "orange") || (bluee[1, 0] == "green")))
                    return "Edge Piece 8";


                if (((redd[1, 2] == "blue") || (redd[1, 2] == "white")) && ((bluee[1, 0] == "blue") || (bluee[1, 0] == "white")))
                    return "Edge Piece 1";

                if (((redd[1, 2] == "yellow") || (redd[1, 2] == "blue")) && ((bluee[1, 0] == "yellow") || (bluee[1, 0] == "blue")))
                    return "Edge Piece 2";

                if (((redd[1, 2] == "white") || (redd[1, 2] == "green")) && ((bluee[1, 0] == "white") || (bluee[1, 0] == "green")))
                    return "Edge Piece 5";

                if (((redd[1, 2] == "yellow") || (redd[1, 2] == "green")) && ((bluee[1, 0] == "yellow") || (bluee[1, 0] == "green")))
                    return "Edge Piece 6";
            }

            ///////////////////////////////////////////////
            if ((cubeX == 2) && (cubeY == 1))
            {
                if (((redd[2, 1] == "red") || (redd[2, 1] == "white")) && ((yelloww[0, 1] == "red") || (yelloww[0, 1] == "white")))
                    return "Edge Piece 12";

                if (((redd[2, 1] == "red") || (redd[2, 1] == "blue")) && ((yelloww[0, 1] == "red") || (yelloww[0, 1] == "blue")))
                    return "Edge Piece 4";

                if (((redd[2, 1] == "red") || (redd[2, 1] == "yellow")) && ((yelloww[0, 1] == "red") || (yelloww[0, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((redd[2, 1] == "red") || (redd[2, 1] == "green")) && ((yelloww[0, 1] == "red") || (yelloww[0, 1] == "green")))
                    return "Edge Piece 7";


                if (((redd[2, 1] == "orange") || (redd[2, 1] == "white")) && ((yelloww[0, 1] == "orange") || (yelloww[0, 1] == "white")))
                    return "Edge Piece 9";

                if (((redd[2, 1] == "orange") || (redd[2, 1] == "blue")) && ((yelloww[0, 1] == "orange") || (yelloww[0, 1] == "blue")))
                    return "Edge Piece 3";

                if (((redd[2, 1] == "orange") || (redd[2, 1] == "yellow")) && ((yelloww[0, 1] == "orange") || (yelloww[0, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((redd[2, 1] == "orange") || (redd[2, 1] == "green")) && ((yelloww[0, 1] == "orange") || (yelloww[0, 1] == "green")))
                    return "Edge Piece 8";


                if (((redd[2, 1] == "blue") || (redd[2, 1] == "white")) && ((yelloww[0, 1] == "blue") || (yelloww[0, 1] == "white")))
                    return "Edge Piece 1";

                if (((redd[2, 1] == "yellow") || (redd[2, 1] == "blue")) && ((yelloww[0, 1] == "yellow") || (yelloww[0, 1] == "blue")))
                    return "Edge Piece 2";

                if (((redd[2, 1] == "white") || (redd[2, 1] == "green")) && ((yelloww[0, 1] == "white") || (yelloww[0, 1] == "green")))
                    return "Edge Piece 5";

                if (((redd[2, 1] == "yellow") || (redd[2, 1] == "green")) && ((yelloww[0, 1] == "yellow") || (yelloww[0, 1] == "green")))
                    return "Edge Piece 6";
            }

            ////////////////////////////////////////
            if ((cubeX == 1) && (cubeY == 0))
            {
                if (((redd[1, 0] == "red") || (redd[1, 0] == "white")) && ((greenn[1, 2] == "red") || (greenn[1, 2] == "white")))
                    return "Edge Piece 12";

                if (((redd[1, 0] == "red") || (redd[1, 0] == "blue")) && ((greenn[1, 2] == "red") || (greenn[1, 2] == "blue")))
                    return "Edge Piece 4";

                if (((redd[1, 0] == "red") || (redd[1, 0] == "yellow")) && ((greenn[1, 2] == "red") || (greenn[1, 2] == "yellow")))
                    return "Edge Piece 11";

                if (((redd[1, 0] == "red") || (redd[1, 0] == "green")) && ((greenn[1, 2] == "red") || (greenn[1, 2] == "green")))
                    return "Edge Piece 7";


                if (((redd[1, 0] == "orange") || (redd[1, 0] == "white")) && ((greenn[1, 2] == "orange") || (greenn[1, 2] == "white")))
                    return "Edge Piece 9";

                if (((redd[1, 0] == "orange") || (redd[1, 0] == "blue")) && ((greenn[1, 2] == "orange") || (greenn[1, 2] == "blue")))
                    return "Edge Piece 3";

                if (((redd[1, 0] == "orange") || (redd[1, 0] == "yellow")) && ((greenn[1, 2] == "orange") || (greenn[1, 2] == "yellow")))
                    return "Edge Piece 10";

                if (((redd[1, 0] == "orange") || (redd[1, 0] == "green")) && ((greenn[1, 2] == "orange") || (greenn[1, 2] == "green")))
                    return "Edge Piece 8";


                if (((redd[1, 0] == "blue") || (redd[1, 0] == "white")) && ((greenn[1, 2] == "blue") || (greenn[1, 2] == "white")))
                    return "Edge Piece 1";

                if (((redd[1, 0] == "yellow") || (redd[1, 0] == "blue")) && ((greenn[1, 2] == "yellow") || (greenn[1, 2] == "blue")))
                    return "Edge Piece 2";

                if (((redd[1, 0] == "white") || (redd[1, 0] == "green")) && ((greenn[1, 2] == "white") || (greenn[1, 2] == "green")))
                    return "Edge Piece 5";

                if (((redd[1, 0] == "yellow") || (redd[1, 0] == "green")) && ((greenn[1, 2] == "yellow") || (greenn[1, 2] == "green")))
                    return "Edge Piece 6";
            }
        }
        if (cubeName == "cube2")
        {
            //edges of cube2
            if ((cubeX == 0) && (cubeY == 0))
            {
                if (((whitee[1, 0] == "red") || (whitee[1, 0] == "white")) && ((greenn[0, 1] == "red") || (greenn[0, 1] == "white")))
                    return "Edge Piece 12";

                if (((whitee[1, 0] == "red") || (whitee[1, 0] == "blue")) && ((greenn[0, 1] == "red") || (greenn[0, 1] == "blue")))
                    return "Edge Piece 4";

                if (((whitee[1, 0] == "red") || (whitee[1, 0] == "yellow")) && ((greenn[0, 1] == "red") || (greenn[0, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((whitee[1, 0] == "red") || (whitee[1, 0] == "green")) && ((greenn[0, 1] == "red") || (greenn[0, 1] == "green")))
                    return "Edge Piece 7";


                if (((whitee[1, 0] == "orange") || (whitee[1, 0] == "white")) && ((greenn[0, 1] == "orange") || (greenn[0, 1] == "white")))
                    return "Edge Piece 9";

                if (((whitee[1, 0] == "orange") || (whitee[1, 0] == "blue")) && ((greenn[0, 1] == "orange") || (greenn[0, 1] == "blue")))
                    return "Edge Piece 3";

                if (((whitee[1, 0] == "orange") || (whitee[1, 0] == "yellow")) && ((greenn[0, 1] == "orange") || (greenn[0, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((whitee[1, 0] == "orange") || (whitee[1, 0] == "green")) && ((greenn[0, 1] == "orange") || (greenn[0, 1] == "green")))
                    return "Edge Piece 8";


                if (((whitee[1, 0] == "blue") || (whitee[1, 0] == "white")) && ((greenn[0, 1] == "blue") || (greenn[0, 1] == "white")))
                    return "Edge Piece 1";

                if (((whitee[1, 0] == "yellow") || (whitee[1, 0] == "blue")) && ((greenn[0, 1] == "yellow") || (greenn[0, 1] == "blue")))
                    return "Edge Piece 2";

                if (((whitee[1, 0] == "white") || (whitee[1, 0] == "green")) && ((greenn[0, 1] == "white") || (greenn[0, 1] == "green")))
                    return "Edge Piece 5";

                if (((whitee[1, 0] == "yellow") || (whitee[1, 0] == "green")) && ((greenn[0, 1] == "yellow") || (greenn[0, 1] == "green")))
                    return "Edge Piece 6";
            }

            ////////////////////////////////
            if ((cubeX == 0) && (cubeY == 2))
            {
                if (((whitee[1, 2] == "red") || (whitee[1, 2] == "white")) && ((bluee[0, 1] == "red") || (bluee[0, 1] == "white")))
                    return "Edge Piece 12";

                if (((whitee[1, 2] == "red") || (whitee[1, 2] == "blue")) && ((bluee[0, 1] == "red") || (bluee[0, 1] == "blue")))
                    return "Edge Piece 4";

                if (((whitee[1, 2] == "red") || (whitee[1, 2] == "yellow")) && ((bluee[0, 1] == "red") || (bluee[0, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((whitee[1, 2] == "red") || (whitee[1, 2] == "green")) && ((bluee[0, 1] == "red") || (bluee[0, 1] == "green")))
                    return "Edge Piece 7";


                if (((whitee[1, 2] == "orange") || (whitee[1, 2] == "white")) && ((bluee[0, 1] == "orange") || (bluee[0, 1] == "white")))
                    return "Edge Piece 9";

                if (((whitee[1, 2] == "orange") || (whitee[1, 2] == "blue")) && ((bluee[0, 1] == "orange") || (bluee[0, 1] == "blue")))
                    return "Edge Piece 3";

                if (((whitee[1, 2] == "orange") || (whitee[1, 2] == "yellow")) && ((bluee[0, 1] == "orange") || (bluee[0, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((whitee[1, 2] == "orange") || (whitee[1, 2] == "green")) && ((bluee[0, 1] == "orange") || (bluee[0, 1] == "green")))
                    return "Edge Piece 8";


                if (((whitee[1, 2] == "blue") || (whitee[1, 2] == "white")) && ((bluee[0, 1] == "blue") || (bluee[0, 1] == "white")))
                    return "Edge Piece 1";

                if (((whitee[1, 2] == "yellow") || (whitee[1, 2] == "blue")) && ((bluee[0, 1] == "yellow") || (bluee[0, 1] == "blue")))
                    return "Edge Piece 2";

                if (((whitee[1, 2] == "white") || (whitee[1, 2] == "green")) && ((bluee[0, 1] == "white") || (bluee[0, 1] == "green")))
                    return "Edge Piece 5";

                if (((whitee[1, 2] == "yellow") || (whitee[1, 2] == "green")) && ((bluee[0, 1] == "yellow") || (bluee[0, 1] == "green")))
                    return "Edge Piece 6";
            }

            ////////////////////////////////
            if ((cubeX == 2) && (cubeY == 0))
            {
                if (((yelloww[1, 0] == "red") || (yelloww[1, 0] == "white")) && ((greenn[2, 1] == "red") || (greenn[2, 1] == "white")))
                    return "Edge Piece 12";

                if (((yelloww[1, 0] == "red") || (yelloww[1, 0] == "blue")) && ((greenn[2, 1] == "red") || (greenn[2, 1] == "blue")))
                    return "Edge Piece 4";

                if (((yelloww[1, 0] == "red") || (yelloww[1, 0] == "yellow")) && ((greenn[2, 1] == "red") || (greenn[2, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((yelloww[1, 0] == "red") || (yelloww[1, 0] == "green")) && ((greenn[2, 1] == "red") || (greenn[2, 1] == "green")))
                    return "Edge Piece 7";


                if (((yelloww[1, 0] == "orange") || (yelloww[1, 0] == "white")) && ((greenn[2, 1] == "orange") || (greenn[2, 1] == "white")))
                    return "Edge Piece 9";

                if (((yelloww[1, 0] == "orange") || (yelloww[1, 0] == "blue")) && ((greenn[2, 1] == "orange") || (greenn[2, 1] == "blue")))
                    return "Edge Piece 3";

                if (((yelloww[1, 0] == "orange") || (yelloww[1, 0] == "yellow")) && ((greenn[2, 1] == "orange") || (greenn[2, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((yelloww[1, 0] == "orange") || (yelloww[1, 0] == "green")) && ((greenn[2, 1] == "orange") || (greenn[2, 1] == "green")))
                    return "Edge Piece 8";


                if (((yelloww[1, 0] == "blue") || (yelloww[1, 0] == "white")) && ((greenn[2, 1] == "blue") || (greenn[2, 1] == "white")))
                    return "Edge Piece 1";

                if (((yelloww[1, 0] == "yellow") || (yelloww[1, 0] == "blue")) && ((greenn[2, 1] == "yellow") || (greenn[2, 1] == "blue")))
                    return "Edge Piece 2";

                if (((yelloww[1, 0] == "white") || (yelloww[1, 0] == "green")) && ((greenn[2, 1] == "white") || (greenn[2, 1] == "green")))
                    return "Edge Piece 5";

                if (((yelloww[1, 0] == "yellow") || (yelloww[1, 0] == "green")) && ((greenn[2, 1] == "yellow") || (greenn[2, 1] == "green")))
                    return "Edge Piece 6";
            }

            ////////////////////////////////
            if ((cubeX == 2) && (cubeY == 2))
            {
                if (((yelloww[1, 2] == "red") || (yelloww[1, 2] == "white")) && ((bluee[2, 1] == "red") || (bluee[2, 1] == "white")))
                    return "Edge Piece 12";

                if (((yelloww[1, 2] == "red") || (yelloww[1, 2] == "blue")) && ((bluee[2, 1] == "red") || (bluee[2, 1] == "blue")))
                    return "Edge Piece 4";

                if (((yelloww[1, 2] == "red") || (yelloww[1, 2] == "yellow")) && ((bluee[2, 1] == "red") || (bluee[2, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((yelloww[1, 2] == "red") || (yelloww[1, 2] == "green")) && ((bluee[2, 1] == "red") || (bluee[2, 1] == "green")))
                    return "Edge Piece 7";


                if (((yelloww[1, 2] == "orange") || (yelloww[1, 2] == "white")) && ((bluee[2, 1] == "orange") || (bluee[2, 1] == "white")))
                    return "Edge Piece 9";

                if (((yelloww[1, 2] == "orange") || (yelloww[1, 2] == "blue")) && ((bluee[2, 1] == "orange") || (bluee[2, 1] == "blue")))
                    return "Edge Piece 3";

                if (((yelloww[1, 2] == "orange") || (yelloww[1, 2] == "yellow")) && ((bluee[2, 1] == "orange") || (bluee[2, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((yelloww[1, 2] == "orange") || (yelloww[1, 2] == "green")) && ((bluee[2, 1] == "orange") || (bluee[2, 1] == "green")))
                    return "Edge Piece 8";


                if (((yelloww[1, 2] == "blue") || (yelloww[1, 2] == "white")) && ((bluee[2, 1] == "blue") || (bluee[2, 1] == "white")))
                    return "Edge Piece 1";

                if (((yelloww[1, 2] == "yellow") || (yelloww[1, 2] == "blue")) && ((bluee[2, 1] == "yellow") || (bluee[2, 1] == "blue")))
                    return "Edge Piece 2";

                if (((yelloww[1, 2] == "white") || (yelloww[1, 2] == "green")) && ((bluee[2, 1] == "white") || (bluee[2, 1] == "green")))
                    return "Edge Piece 5";

                if (((yelloww[1, 2] == "yellow") || (yelloww[1, 2] == "green")) && ((bluee[2, 1] == "yellow") || (bluee[2, 1] == "green")))
                    return "Edge Piece 6";
            }
        }

        if (cubeName == "cube3")
        {
            //corners of cube3
            if ((cubeX == 0) && (cubeY == 0))
            {
                //corners of red face(F) and adjacent ones
                if (((orangee[0, 2] == "red") || (orangee[0, 2] == "white") || (orangee[0, 2] == "green")) && ((whitee[0, 0] == "red") || (whitee[0, 0] == "white") || (whitee[0, 0] == "green")) && ((greenn[0, 0] == "red") || (greenn[0, 0] == "white") || (greenn[0, 0] == "green")))
                    return "Corner Piece 7";

                if (((orangee[0, 2] == "red") || (orangee[0, 2] == "white") || (orangee[0, 2] == "blue")) && ((whitee[0, 0] == "red") || (whitee[0, 0] == "white") || (whitee[0, 0] == "blue")) && ((greenn[0, 0] == "red") || (greenn[0, 0] == "white") || (greenn[0, 0] == "blue")))
                    return "Corner Piece 2";

                if (((orangee[0, 2] == "red") || (orangee[0, 2] == "yellow") || (orangee[0, 2] == "blue")) && ((whitee[0, 0] == "red") || (whitee[0, 0] == "yellow") || (whitee[0, 0] == "blue")) && ((greenn[0, 0] == "red") || (greenn[0, 0] == "yellow") || (greenn[0, 0] == "blue")))
                    return "Corner Piece 3";

                if (((orangee[0, 2] == "red") || (orangee[0, 2] == "yellow") || (orangee[0, 2] == "green")) && ((whitee[0, 0] == "red") || (whitee[0, 0] == "yellow") || (whitee[0, 0] == "green")) && ((greenn[0, 0] == "red") || (greenn[0, 0] == "yellow") || (greenn[0, 0] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((orangee[0, 2] == "orange") || (orangee[0, 2] == "white") || (orangee[0, 2] == "green")) && ((whitee[0, 0] == "orange") || (whitee[0, 0] == "white") || (whitee[0, 0] == "green")) && ((greenn[0, 0] == "orange") || (greenn[0, 0] == "white") || (greenn[0, 0] == "green")))
                    return "Corner Piece 5";

                if (((orangee[0, 2] == "orange") || (orangee[0, 2] == "white") || (orangee[0, 2] == "blue")) && ((whitee[0, 0] == "orange") || (whitee[0, 0] == "white") || (whitee[0, 0] == "blue")) && ((greenn[0, 0] == "orange") || (greenn[0, 0] == "white") || (greenn[0, 0] == "blue")))
                    return "Corner Piece 1";

                if (((orangee[0, 2] == "orange") || (orangee[0, 2] == "yellow") || (orangee[0, 2] == "blue")) && ((whitee[0, 0] == "orange") || (whitee[0, 0] == "yellow") || (whitee[0, 0] == "blue")) && ((greenn[0, 0] == "orange") || (greenn[0, 0] == "yellow") || (greenn[0, 0] == "blue")))
                    return "Corner Piece 4";

                if (((orangee[0, 2] == "orange") || (orangee[0, 2] == "yellow") || (orangee[0, 2] == "green")) && ((whitee[0, 0] == "orange") || (whitee[0, 0] == "yellow") || (whitee[0, 0] == "green")) && ((greenn[0, 0] == "orange") || (greenn[0, 0] == "yellow") || (greenn[0, 0] == "green")))
                    return "Corner Piece 6";
            }

            //////////////////////////////////////
            if ((cubeX == 0) && (cubeY == 2))
            {
                //corners of red face(F) and adjacent ones
                if (((orangee[0, 0] == "red") || (orangee[0, 0] == "white") || (orangee[0, 0] == "green")) && ((whitee[0, 2] == "red") || (whitee[0, 2] == "white") || (whitee[0, 2] == "green")) && ((bluee[0, 2] == "red") || (bluee[0, 2] == "white") || (bluee[0, 2] == "green")))
                    return "Corner Piece 7";

                if (((orangee[0, 0] == "red") || (orangee[0, 0] == "white") || (orangee[0, 0] == "blue")) && ((whitee[0, 2] == "red") || (whitee[0, 2] == "white") || (whitee[0, 2] == "blue")) && ((bluee[0, 2] == "red") || (bluee[0, 2] == "white") || (bluee[0, 2] == "blue")))
                    return "Corner Piece 2";

                if (((orangee[0, 0] == "red") || (orangee[0, 0] == "yellow") || (orangee[0, 0] == "blue")) && ((whitee[0, 2] == "red") || (whitee[0, 2] == "yellow") || (whitee[0, 2] == "blue")) && ((bluee[0, 2] == "red") || (bluee[0, 2] == "yellow") || (bluee[0, 2] == "blue")))
                    return "Corner Piece 3";

                if (((orangee[0, 0] == "red") || (orangee[0, 0] == "yellow") || (orangee[0, 0] == "green")) && ((whitee[0, 2] == "red") || (whitee[0, 2] == "yellow") || (whitee[0, 2] == "green")) && ((bluee[0, 2] == "red") || (bluee[0, 2] == "yellow") || (bluee[0, 2] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((orangee[0, 0] == "orange") || (orangee[0, 0] == "white") || (orangee[0, 0] == "green")) && ((whitee[0, 2] == "orange") || (whitee[0, 2] == "white") || (whitee[0, 2] == "green")) && ((bluee[0, 2] == "orange") || (bluee[0, 2] == "white") || (bluee[0, 2] == "green")))
                    return "Corner Piece 5";

                if (((orangee[0, 0] == "orange") || (orangee[0, 0] == "white") || (orangee[0, 0] == "blue")) && ((whitee[0, 2] == "orange") || (whitee[0, 2] == "white") || (whitee[0, 2] == "blue")) && ((bluee[0, 2] == "orange") || (bluee[0, 2] == "white") || (bluee[0, 2] == "blue")))
                    return "Corner Piece 1";

                if (((orangee[0, 0] == "orange") || (orangee[0, 0] == "yellow") || (orangee[0, 0] == "blue")) && ((whitee[0, 2] == "orange") || (whitee[0, 2] == "yellow") || (whitee[0, 2] == "blue")) && ((bluee[0, 2] == "orange") || (bluee[0, 2] == "yellow") || (bluee[0, 2] == "blue")))
                    return "Corner Piece 4";

                if (((orangee[0, 0] == "orange") || (orangee[0, 0] == "yellow") || (orangee[0, 0] == "green")) && ((whitee[0, 2] == "orange") || (whitee[0, 2] == "yellow") || (whitee[0, 2] == "green")) && ((bluee[0, 2] == "orange") || (bluee[0, 2] == "yellow") || (bluee[0, 2] == "green")))
                    return "Corner Piece 6";
            }

            //////////////////////////////////////
            if ((cubeX == 2) && (cubeY == 0))
            {
                //corners of red face(F) and adjacent ones
                if (((orangee[2, 2] == "red") || (orangee[2, 2] == "white") || (orangee[2, 2] == "green")) && ((yelloww[2, 0] == "red") || (yelloww[2, 0] == "white") || (yelloww[2, 0] == "green")) && ((greenn[2, 0] == "red") || (greenn[2, 0] == "white") || (greenn[2, 0] == "green")))
                    return "Corner Piece 7";

                if (((orangee[2, 2] == "red") || (orangee[2, 2] == "white") || (orangee[2, 2] == "blue")) && ((yelloww[2, 0] == "red") || (yelloww[2, 0] == "white") || (yelloww[2, 0] == "blue")) && ((greenn[2, 0] == "red") || (greenn[2, 0] == "white") || (greenn[2, 0] == "blue")))
                    return "Corner Piece 2";

                if (((orangee[2, 2] == "red") || (orangee[2, 2] == "yellow") || (orangee[2, 2] == "blue")) && ((yelloww[2, 0] == "red") || (yelloww[2, 0] == "yellow") || (yelloww[2, 0] == "blue")) && ((greenn[2, 0] == "red") || (greenn[2, 0] == "yellow") || (greenn[2, 0] == "blue")))
                    return "Corner Piece 3";

                if (((orangee[2, 2] == "red") || (orangee[2, 2] == "yellow") || (orangee[2, 2] == "green")) && ((yelloww[2, 0] == "red") || (yelloww[2, 0] == "yellow") || (yelloww[2, 0] == "green")) && ((greenn[2, 0] == "red") || (greenn[2, 0] == "yellow") || (greenn[2, 0] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((orangee[2, 2] == "orange") || (orangee[2, 2] == "white") || (orangee[2, 2] == "green")) && ((yelloww[2, 0] == "orange") || (yelloww[2, 0] == "white") || (yelloww[2, 0] == "green")) && ((greenn[2, 0] == "orange") || (greenn[2, 0] == "white") || (greenn[2, 0] == "green")))
                    return "Corner Piece 5";

                if (((orangee[2, 2] == "orange") || (orangee[2, 2] == "white") || (orangee[2, 2] == "blue")) && ((yelloww[2, 0] == "orange") || (yelloww[2, 0] == "white") || (yelloww[2, 0] == "blue")) && ((greenn[2, 0] == "orange") || (greenn[2, 0] == "white") || (greenn[2, 0] == "blue")))
                    return "Corner Piece 1";

                if (((orangee[2, 2] == "orange") || (orangee[2, 2] == "yellow") || (orangee[2, 2] == "blue")) && ((yelloww[2, 0] == "orange") || (yelloww[2, 0] == "yellow") || (yelloww[2, 0] == "blue")) && ((greenn[2, 0] == "orange") || (greenn[2, 0] == "yellow") || (greenn[2, 0] == "blue")))
                    return "Corner Piece 4";

                if (((orangee[2, 2] == "orange") || (orangee[2, 2] == "yellow") || (orangee[2, 2] == "green")) && ((yelloww[2, 0] == "orange") || (yelloww[2, 0] == "yellow") || (yelloww[2, 0] == "green")) && ((greenn[2, 0] == "orange") || (greenn[2, 0] == "yellow") || (greenn[2, 0] == "green")))
                    return "Corner Piece 6";
            }


            //////////////////////////////////////
            if ((cubeX == 2) && (cubeY == 2))
            {
                //corners of red face(F) and adjacent ones
                if (((orangee[2, 0] == "red") || (orangee[2, 0] == "white") || (orangee[2, 0] == "green")) && ((yelloww[2, 2] == "red") || (yelloww[2, 2] == "white") || (yelloww[2, 2] == "green")) && ((bluee[2, 2] == "red") || (bluee[2, 2] == "white") || (bluee[2, 2] == "green")))
                    return "Corner Piece 7";

                if (((orangee[2, 0] == "red") || (orangee[2, 0] == "white") || (orangee[2, 0] == "blue")) && ((yelloww[2, 2] == "red") || (yelloww[2, 2] == "white") || (yelloww[2, 2] == "blue")) && ((bluee[2, 2] == "red") || (bluee[2, 2] == "white") || (bluee[2, 2] == "blue")))
                    return "Corner Piece 2";

                if (((orangee[2, 0] == "red") || (orangee[2, 0] == "yellow") || (orangee[2, 0] == "blue")) && ((yelloww[2, 2] == "red") || (yelloww[2, 2] == "yellow") || (yelloww[2, 2] == "blue")) && ((bluee[2, 2] == "red") || (bluee[2, 2] == "yellow") || (bluee[2, 2] == "blue")))
                    return "Corner Piece 3";

                if (((orangee[2, 0] == "red") || (orangee[2, 0] == "yellow") || (orangee[2, 0] == "green")) && ((yelloww[2, 2] == "red") || (yelloww[2, 2] == "yellow") || (yelloww[2, 2] == "green")) && ((bluee[2, 2] == "red") || (bluee[2, 2] == "yellow") || (bluee[2, 2] == "green")))
                    return "Corner Piece 8";

                //corners of orange face(B) and adjiacent ones
                if (((orangee[2, 0] == "orange") || (orangee[2, 0] == "white") || (orangee[2, 0] == "green")) && ((yelloww[2, 2] == "orange") || (yelloww[2, 2] == "white") || (yelloww[2, 2] == "green")) && ((bluee[2, 2] == "orange") || (bluee[2, 2] == "white") || (bluee[2, 2] == "green")))
                    return "Corner Piece 5";

                if (((orangee[2, 0] == "orange") || (orangee[2, 0] == "white") || (orangee[2, 0] == "blue")) && ((yelloww[2, 2] == "orange") || (yelloww[2, 2] == "white") || (yelloww[2, 2] == "blue")) && ((bluee[2, 2] == "orange") || (bluee[2, 2] == "white") || (bluee[2, 2] == "blue")))
                    return "Corner Piece 1";

                if (((orangee[2, 0] == "orange") || (orangee[2, 0] == "yellow") || (orangee[2, 0] == "blue")) && ((yelloww[2, 2] == "orange") || (yelloww[2, 2] == "yellow") || (yelloww[2, 2] == "blue")) && ((bluee[2, 2] == "orange") || (bluee[2, 2] == "yellow") || (bluee[2, 2] == "blue")))
                    return "Corner Piece 4";

                if (((orangee[2, 0] == "orange") || (orangee[2, 0] == "yellow") || (orangee[2, 0] == "green")) && ((yelloww[2, 2] == "orange") || (yelloww[2, 2] == "yellow") || (yelloww[2, 2] == "green")) && ((bluee[2, 2] == "orange") || (bluee[2, 2] == "yellow") || (bluee[2, 2] == "green")))
                    return "Corner Piece 6";
            }


            //edges of cube3
            if ((cubeX == 0) && (cubeY == 1))
            {
                if (((orangee[0, 1] == "red") || (orangee[0, 1] == "white")) && ((whitee[0, 1] == "red") || (whitee[0, 1] == "white")))
                    return "Edge Piece 12";

                if (((orangee[0, 1] == "red") || (orangee[0, 1] == "blue")) && ((whitee[0, 1] == "red") || (whitee[0, 1] == "blue")))
                    return "Edge Piece 4";

                if (((orangee[0, 1] == "red") || (orangee[0, 1] == "yellow")) && ((whitee[0, 1] == "red") || (whitee[0, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((orangee[0, 1] == "red") || (orangee[0, 1] == "green")) && ((whitee[0, 1] == "red") || (whitee[0, 1] == "green")))
                    return "Edge Piece 7";


                if (((orangee[0, 1] == "orange") || (orangee[0, 1] == "white")) && ((whitee[0, 1] == "orange") || (whitee[0, 1] == "white")))
                    return "Edge Piece 9";

                if (((orangee[0, 1] == "orange") || (orangee[0, 1] == "blue")) && ((whitee[0, 1] == "orange") || (whitee[0, 1] == "blue")))
                    return "Edge Piece 3";

                if (((orangee[0, 1] == "orange") || (orangee[0, 1] == "yellow")) && ((whitee[0, 1] == "orange") || (whitee[0, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((orangee[0, 1] == "orange") || (orangee[0, 1] == "green")) && ((whitee[0, 1] == "orange") || (whitee[0, 1] == "green")))
                    return "Edge Piece 8";


                if (((orangee[0, 1] == "blue") || (orangee[0, 1] == "white")) && ((whitee[0, 1] == "blue") || (whitee[0, 1] == "white")))
                    return "Edge Piece 1";

                if (((orangee[0, 1] == "yellow") || (orangee[0, 1] == "blue")) && ((whitee[0, 1] == "yellow") || (whitee[0, 1] == "blue")))
                    return "Edge Piece 2";

                if (((orangee[0, 1] == "white") || (orangee[0, 1] == "green")) && ((whitee[0, 1] == "white") || (whitee[0, 1] == "green")))
                    return "Edge Piece 5";

                if (((orangee[0, 1] == "yellow") || (orangee[0, 1] == "green")) && ((whitee[0, 1] == "yellow") || (whitee[0, 1] == "green")))
                    return "Edge Piece 6";
            }

            ///////////////////////////////////////
            if ((cubeX == 1) && (cubeY == 2))
            {
                if (((orangee[1, 0] == "red") || (orangee[1, 0] == "white")) && ((bluee[1, 2] == "red") || (bluee[1, 2] == "white")))
                    return "Edge Piece 12";

                if (((orangee[1, 0] == "red") || (orangee[1, 0] == "blue")) && ((bluee[1, 2] == "red") || (bluee[1, 2] == "blue")))
                    return "Edge Piece 4";

                if (((orangee[1, 0] == "red") || (orangee[1, 0] == "yellow")) && ((bluee[1, 2] == "red") || (bluee[1, 2] == "yellow")))
                    return "Edge Piece 11";

                if (((orangee[1, 0] == "red") || (orangee[1, 0] == "green")) && ((bluee[1, 2] == "red") || (bluee[1, 2] == "green")))
                    return "Edge Piece 7";


                if (((orangee[1, 0] == "orange") || (orangee[1, 0] == "white")) && ((bluee[1, 2] == "orange") || (bluee[1, 2] == "white")))
                    return "Edge Piece 9";

                if (((orangee[1, 0] == "orange") || (orangee[1, 0] == "blue")) && ((bluee[1, 2] == "orange") || (bluee[1, 2] == "blue")))
                    return "Edge Piece 3";

                if (((orangee[1, 0] == "orange") || (orangee[1, 0] == "yellow")) && ((bluee[1, 2] == "orange") || (bluee[1, 2] == "yellow")))
                    return "Edge Piece 10";

                if (((orangee[1, 0] == "orange") || (orangee[1, 0] == "green")) && ((bluee[1, 2] == "orange") || (bluee[1, 2] == "green")))
                    return "Edge Piece 8";


                if (((orangee[1, 0] == "blue") || (orangee[1, 0] == "white")) && ((bluee[1, 2] == "blue") || (bluee[1, 2] == "white")))
                    return "Edge Piece 1";

                if (((orangee[1, 0] == "yellow") || (orangee[1, 0] == "blue")) && ((bluee[1, 2] == "yellow") || (bluee[1, 2] == "blue")))
                    return "Edge Piece 2";

                if (((orangee[1, 0] == "white") || (orangee[1, 0] == "green")) && ((bluee[1, 2] == "white") || (bluee[1, 2] == "green")))
                    return "Edge Piece 5";

                if (((orangee[1, 0] == "yellow") || (orangee[1, 0] == "green")) && ((bluee[1, 2] == "yellow") || (bluee[1, 2] == "green")))
                    return "Edge Piece 6";
            }

            ////////////////////////////////////
            if ((cubeX == 2) && (cubeY == 1))
            {
                if (((orangee[2, 1] == "red") || (orangee[2, 1] == "white")) && ((yelloww[2, 1] == "red") || (yelloww[2, 1] == "white")))
                    return "Edge Piece 12";

                if (((orangee[2, 1] == "red") || (orangee[2, 1] == "blue")) && ((yelloww[2, 1] == "red") || (yelloww[2, 1] == "blue")))
                    return "Edge Piece 4";

                if (((orangee[2, 1] == "red") || (orangee[2, 1] == "yellow")) && ((yelloww[2, 1] == "red") || (yelloww[2, 1] == "yellow")))
                    return "Edge Piece 11";

                if (((orangee[2, 1] == "red") || (orangee[2, 1] == "green")) && ((yelloww[2, 1] == "red") || (yelloww[2, 1] == "green")))
                    return "Edge Piece 7";


                if (((orangee[2, 1] == "orange") || (orangee[2, 1] == "white")) && ((yelloww[2, 1] == "orange") || (yelloww[2, 1] == "white")))
                    return "Edge Piece 9";

                if (((orangee[2, 1] == "orange") || (orangee[2, 1] == "blue")) && ((yelloww[2, 1] == "orange") || (yelloww[2, 1] == "blue")))
                    return "Edge Piece 3";

                if (((orangee[2, 1] == "orange") || (orangee[2, 1] == "yellow")) && ((yelloww[2, 1] == "orange") || (yelloww[2, 1] == "yellow")))
                    return "Edge Piece 10";

                if (((orangee[2, 1] == "orange") || (orangee[2, 1] == "green")) && ((yelloww[2, 1] == "orange") || (yelloww[2, 1] == "green")))
                    return "Edge Piece 8";


                if (((orangee[2, 1] == "blue") || (orangee[2, 1] == "white")) && ((yelloww[2, 1] == "blue") || (yelloww[2, 1] == "white")))
                    return "Edge Piece 1";

                if (((orangee[2, 1] == "yellow") || (orangee[2, 1] == "blue")) && ((yelloww[2, 1] == "yellow") || (yelloww[2, 1] == "blue")))
                    return "Edge Piece 2";

                if (((orangee[2, 1] == "white") || (orangee[2, 1] == "green")) && ((yelloww[2, 1] == "white") || (yelloww[2, 1] == "green")))
                    return "Edge Piece 5";

                if (((orangee[2, 1] == "yellow") || (orangee[2, 1] == "green")) && ((yelloww[2, 1] == "yellow") || (yelloww[2, 1] == "green")))
                    return "Edge Piece 6";
            }

            ////////////////////////////////////////
            if ((cubeX == 1) && (cubeY == 0))
            {
                if (((orangee[1, 2] == "red") || (orangee[1, 2] == "white")) && ((greenn[1, 0] == "red") || (greenn[1, 0] == "white")))
                    return "Edge Piece 12";

                if (((orangee[1, 2] == "red") || (orangee[1, 2] == "blue")) && ((greenn[1, 0] == "red") || (greenn[1, 0] == "blue")))
                    return "Edge Piece 4";

                if (((orangee[1, 2] == "red") || (orangee[1, 2] == "yellow")) && ((greenn[1, 0] == "red") || (greenn[1, 0] == "yellow")))
                    return "Edge Piece 11";

                if (((orangee[1, 2] == "red") || (orangee[1, 2] == "green")) && ((greenn[1, 0] == "red") || (greenn[1, 0] == "green")))
                    return "Edge Piece 7";


                if (((orangee[1, 2] == "orange") || (orangee[1, 2] == "white")) && ((greenn[1, 0] == "orange") || (greenn[1, 0] == "white")))
                    return "Edge Piece 9";

                if (((orangee[1, 2] == "orange") || (orangee[1, 2] == "blue")) && ((greenn[1, 0] == "orange") || (greenn[1, 0] == "blue")))
                    return "Edge Piece 3";

                if (((orangee[1, 2] == "orange") || (orangee[1, 2] == "yellow")) && ((greenn[1, 0] == "orange") || (greenn[1, 0] == "yellow")))
                    return "Edge Piece 10";

                if (((orangee[1, 2] == "orange") || (orangee[1, 2] == "green")) && ((greenn[1, 0] == "orange") || (greenn[1, 0] == "green")))
                    return "Edge Piece 8";


                if (((orangee[1, 2] == "blue") || (orangee[1, 2] == "white")) && ((greenn[1, 0] == "blue") || (greenn[1, 0] == "white")))
                    return "Edge Piece 1";

                if (((orangee[1, 2] == "yellow") || (orangee[1, 2] == "blue")) && ((greenn[1, 0] == "yellow") || (greenn[1, 0] == "blue")))
                    return "Edge Piece 2";

                if (((orangee[1, 2] == "white") || (orangee[1, 2] == "green")) && ((greenn[1, 0] == "white") || (greenn[1, 0] == "green")))
                    return "Edge Piece 5";

                if (((orangee[1, 2] == "yellow") || (orangee[1, 2] == "green")) && ((greenn[1, 0] == "yellow") || (greenn[1, 0] == "green")))
                    return "Edge Piece 6";
            }
        }

            return "";

            
    }
   
}



static class Dll
{
    [DllImport("OPENCV_FIRST")] 
    internal static extern int main();

    [DllImport("OPENCV_FIRST")]
    internal static extern char get_cube(int i, int j);

}

