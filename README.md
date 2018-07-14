# GUI framework for MonoGame

Easy framework for writing GUI applications on the MonoGame platform.

#### Features:
- Available controls: button, label, switch (checkbox), changer
- Available containers (horizontally, vertically)
- Wrapping the user input mechanism (mouse, touch screen)
- Wrapping mechanism for loading and using resources (texture)
- mechanism of navigation (Scroll, Swype)
- mechanism of alignment content (horizontally, vertically)
- Scaling mechanism

### Json resource file structure

Information about the required resources is transferred to System.Resources as a json script, the script format is as follows:

```json
[
    {"Name": "textureName1", "Type": "Texture2D"},
    {"Name": "textureName2", "Type": "Font"},
]
```

field Name: the name of the texture file (without extension)
field Type: textures can have 2 types, Fonr or Texture2D

The protocol for loading resources must be transmitted before the call "Game.Run".

    Resources.AddJsonLoadResources (JSON);

### Quick start

"MonoGui/Example" contains an example application for android and debugging it on Windows

    /MonoGuiGameView - sample application
    /MonoGuiWin - port for Windows
    /MonoGuiAndroid - port for Android
