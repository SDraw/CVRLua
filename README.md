# CVRLua
Experimental mod that provides Lua scripting for ChilloutVR worlds.
# How hard?
Not that hard. You just add `LuaScript` component on your game object, add script(s) as `TextAsset`(s) to it and it's done.
```lua
-- HelloPlayer.lua example
function Start()
    Log("Hello, "..localPlayer.name.."! This script is started on "..this.name.." gameObject.")
end
```
Scripting approach is kept very close to Unity's CSharp scripting. That means methods and properties of object(s)/component(s) are retained.
# How to use?
* For average users:
  * Download `CVRLua.dll` from latest release
  * Copy it into `ChilloutVR/Mods` folder
* For world creators:
   * Download `CVRLua_Editor.unitypackage` from latest release
   * Import it into your world project
   * Add `LuaScript` component to your GameObject(s)
# Scripting documentation
Visit [wiki pages](../../wiki) for in-depth information about scripting state and usage.  
Project uses Lua 5.4.6 from [Lua nuget package](https://www.nuget.org/packages/lua) (v141).

# Why?
Me when waiting for official game scripting: [link](https://www.youtube.com/watch?v=a6zKohQnFJk)  
Also me: [link](https://youtu.be/EzWNBmjyv7Y?t=6)
