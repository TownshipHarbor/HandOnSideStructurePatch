# HandOnSideStructurePatch for Quest

CGNik's HandOnSideStructurePatch v2, built for the Quest client. The hand on side pose (the one Stinger and StingerReadded's Side Power Hit use) gets Raise Hand's charge and execute sounds.

Original: https://github.com/CG-Nik/HandOnSideStructurePatch

## Install

Put `HandOnSideStructurePatch.dll` in the headset's Mods folder, or take it from the Download board:

```
/sdcard/Android/data/com.Alta.ATownshipTale/files/MelonLoader/com.Alta.ATownshipTale/Mods/
```

Player side only, as on PC: the server needs nothing for it.

## Differences from the PC DLL

| PC v2 | Quest build |
|---|---|
| Copies the sounds at `OnInitializeMelon` | Tries there, then at `OnLateInitializeMelon`, then every second for a minute until the game has loaded its skills |
| Reflection on private fields | The IL2CPP properties for the same fields |
| | Logs one line when the sounds are copied |

## Building

`build.ps1` builds `quest\HandOnSideStructurePatch.Quest.csproj` into `build\`. Put LemonLoader's `net8` folder and its generated `Il2CppAssemblies` in `lemon\`.

## Credits

HandOnSideStructurePatch is by CGNik, under the MIT licence. Quest port by Shadow.

This is an unofficial, fan made mod for A Township Tale. It is not affiliated with or endorsed by the owners or developers of A Township Tale.
