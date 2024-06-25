# Immersive Piano

This is a mixed reality immersive piano project that allows users to play the piano in a virtual environment. The project uses the following technologies:

* Unity
* [Meta XR SDK V65](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657)
* [Maestro SDK](https://paxstellar.fr/)

## Thesis
Click [here](https://www.overleaf.com/read/cwfcmjjsgbgq#89465e) to view the thesis.

## To build and run the project:
*Make sure that you have met the requirement of ADB, Oculus XR Plugin and Android SDK in Unity
1. Clone the repo to your local machine with "git clone https://github.com/tranquanglam123/ImmersivePiano.git
2. Import the repo into your Unity
3. Press Ctrl + Shift + B in Unity to open the Build dialog.
4. Configurate your wanted settings and build.
5. After successfully built, go to the file that has the APK and open up terminal.
6. Type in "adb install -r + "yourbuildfile.apk"
7. Use the app in the device

## To play the piano:

1. Read the instruction before starting the game.<img width="1280" alt="image" src="https://github.com/tranquanglam123/ImmersivePiano/assets/101560324/283a1cd0-c1eb-4416-bf9e-bb44a3ae5201">

2. Place the left hand's index at the left of your table or the surface that you want to display the piano. <img width="1280" alt="image" src="https://github.com/tranquanglam123/ImmersivePiano/assets/101560324/8adc6e14-c732-4496-9368-4ea012712cda">

3. Place your right hand's index at the right of your surface and press confirm. <img width="1280" alt="image" src="https://github.com/tranquanglam123/ImmersivePiano/assets/101560324/65288915-3c6c-4020-9667-35dc7cd9afa1">

4. The piano show up and you are now able to practice.<img width="1280" alt="image" src="https://github.com/tranquanglam123/ImmersivePiano/assets/101560324/e3a583d4-c856-42f3-a30e-554be073b137">


## Controls:

* Raise your hand and point at the piano keys to play them.
* Move your hand to different positions on the keys to change the pitch.
* Press down on the keys with your hand to play them harder.
* Look around the environment to change your perspective.

## Solved issues:

* Multiple songs.
* MIDI Visualizations.
* The hand tracking is not perfect.
* The apk file is now unstable and can not work well. This issue is being debugged.

## Known issues:
* The apk file is now unstable and can not work well. This issue is being debugged.
* Crash Log Error : Null pointer dereference.

## Future work:
* Progress Tracking System

## Contributions:

This project is open source and contributions are welcome. If you have any suggestions or bug reports, please create an issue on the project's GitHub repository.

## License:

This project is licensed under the MIT License.
