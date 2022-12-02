# README
This program uses the integrated Game State Management System of CS:GO to unbind the Silencer Removal of the M4A1-S.
The Program is written in C# and uses [CSGSI](https://github.com/rakijah/CSGSI) by *rakijah* to accept GSI-Events and [Telnet](https://github.com/9swampy/Telnet/) by *9swampy* to act as a Telnet-Client to connect to the built-in CS:GO console.

To execute CS:GO Console Commands, we have to activate the built-in Telnet Support of CS:GO.
To do this we have to add the following line to our launch options: `-netconport 2121` which will **open a Telnet Server that can be used to send commands to the CS:GO console**.
This Port can be changed to any other Port you want to use by editing the config file, which will be generated at the first startup.

## Installation
1. Download the latest release from [here](https://github.com/Slowline/CSGO-M4A1S-Unbinder/releases)
2. Extract the zip file
3. Copy the `gamestate_integration_csgom4a1sunbind.cfg` to your `csgo/cfg` folder
4. Add `-netconport 2121` to your launch options
5. Run the .exe file
6. Enjoy having right click unbound if you have a M4A1-S equipped

## Configuration

The config file will be created at the first startup of the program and will be located inside the same folder as the .exe file with the name `config.json`.
As default, the program will unbind `mouse2` for the weapons `weapon_m4a1_silencer, weapon_usp_silencer and weapon_deagle` because of the silencer and the annoying deagle bug where you're not able to shoot if you're holding right click before firing.

The content of the `config.json`-file will look like this and can be changed by you:
```json
{
  "GsiPort": 3000,
  "TelnetPort": 2121,
  "UnbindCommands": [
    "echo Weapons are active, unbinding...",
    "-attack2",
    "unbind mouse2"
  ],
  "BindCommands": [
    "bind mouse2 +attack2",
    "echo Weapons are inactive, binding..."
  ],
  "WeaponIdentifiers": [
    "weapon_m4a1_silencer",
    "weapon_usp_silencer",
    "weapon_deagle"
  ]
}
```

As soon as the Game is sending a Game State Event, the program will check against the given Weapon Identifiers if the player is holding one of the weapons.
If true, the program will execute the commands in the `UnbindCommands` array. If false, the program will execute the commands in the `BindCommands` array.

If you change the `GsiPort` or `TelnetPort` you have to adjust the launch options or the game state config file accordingly.

## References
1. [https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration)
2. [https://developer.valvesoftware.com/wiki/Command_Line_Options](https://developer.valvesoftware.com/wiki/Command_Line_Options#:~:text=%2Dnetconport%20%3Cnumber%3E%20%2D%20Creates%20a%20remotely%20accessible%20server%20console%20on%20the%20specified%20port.%20This%20can%20be%20connected%20to%20with%20telnet%20or%20similar%20applications%2C%20and%20allows%20controlling%20of%20the%20server%20as%20if%20the%20commands%20were%20being%20typed%20in%20at%20the%20console)