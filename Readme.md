# README
This program uses the integrated Game State Management System of CS:GO to unbind the Silencer Removal of the M4A1-S.
The Program is written in C# and uses [CSGSI](https://github.com/rakijah/CSGSI) by *rakijah* to accept GSI-Events and [Telnet](https://github.com/9swampy/Telnet/) by *9swampy* to act as a Telnet-Client to connect to the built-in CS:GO console.

To execute CS:GO Console Commands, we have to activate the built-in Telnet Support of CS:GO.
To do this we have to add the following line to our launch options: `-netconport 2121` which will **open a Telnet Server that can be used to send commands to the CS:GO console**.
This Port can be changed to any other Port you want to use by editing the config file, which will be generated at the first startup.

The default config file will unbind `mouse2` for the weapons `weapon_m4a1_silencer, weapon_usp_silencer and weapon_deagle` because of the silencer and the annoying deagle bug where you're not able to shoot if you're holding right click before firing.
The content of the file will look like this and can be changed by you:
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

## Installation
1. Download the latest release from [here]()
2. Extract the zip file
3. Copy the `gamestate_integration_csgom4a1sunbind.cfg` to your `csgo/cfg` folder
4. Add `-netconport 2121` to your launch options
5. Run the .exe file
6. Enjoy having right click unbound if you have a M4A1-S equipped