# *About*
**This repo contains code that allows you to fade and crossfade between different music tracks, or versions of a single track in Unity.** This is useful for adaptive and/or reactive music, where you can have a master track, and fade between different instruments or even vocals. If you want to demo it in action, you can download an executable demo, or just download the full editor. You are free to use the code in your own project as per the license, but not the music, they are made by and belong to *me*.

# *How to Use*
**Just make a manager GameObject and add an AdaptiveAudioManager script to it.** (*Prefab these for different track sets, they are not persistent between scenes.*) Add your tracks and set your variables for each track and the manager script. (*Check the tooltips for what they do.*) In any script, call the `Play()` method to fade or crossfade your tracks. It's that simple. `stopStart()` and `playPause()` methods are also available.

You can also use the `Scripts\Misc\Curves.cs\` script to create AnimationCurves visually in the editor, and store them in a public array for easy swapping of curves in the AAM script.

#  *Implementation*
The parent class `AdaptiveAudioManager` stores `Sound` objects in an array named `layers`. They contain definitions for `volume`, `isExclusive`, etc. When the scene is started, the *AAM* script creates the `Sound` objects that you made in the editor. The `Sound` objects create 'Audio Source' components in the GameObject and plays all of the Sounds at once. It is recommended to have a main track that starts at a volume of *1*, and the other tracks start at *0*.    