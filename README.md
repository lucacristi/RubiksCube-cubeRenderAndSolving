# Unity Cube Simulator 3x3x3

**! The solving algorithm, cube drawing and other effects were made by [NoCakeNoCode](https://github.com/NoCakeNoCode) on its repository [Unity-Cube-Simulator-3x3x3](https://github.com/NoCakeNoCode/Unity-Cube-Simulator-3x3x3) !**  

My contribution consists on working with .dll file and its data, from [RubiksCube-colorDetection](https://github.com/lucacristi/RubiksCube-colorDetection) .  


## Objective
- The aim of this application is to render the rubik's cube, using the colors obtained from user's webcam.  
- The implementation is done with the help of Unity.  
- The .dll file (contining the application of colors detection) is triggered at the start of the application.
 
Implementation of the Application's objective is illustrated in the following image.
<p align="center">
  <img width="500" height="300" src="https://user-images.githubusercontent.com/54026035/106956547-a6d3ff80-672e-11eb-923d-80bd4ed8264d.png" alt="Final App Diagram">
  <br clear="center"/>
      Final Application Objectives
</p>

## Cube Rendering 
In order to render the cube, the colors of each face should be scanned.
The .dll file is added to Unity, and through a script, the functions from the file are called.

A class called DLL is created, that imports the *main()* function and the *get_cube()* function of the color scan application from the file named Get_colorsApp_OpenCV.dll


## Short Demo
https://youtu.be/VK_TNnc5jcE


