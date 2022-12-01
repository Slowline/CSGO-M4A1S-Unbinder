using System.Diagnostics;
using CSGOM4A1SUnbind;
using CSGSI;
using Newtonsoft.Json;
using PrimS.Telnet;

// Load Config File
var config = GetConfigFile();

// New GSI Listener Instance
var gsl = new GameStateListener(config.GsiPort);

// Telnet Client Reference & Telnet Connection State
Client? telnetClient = null;
var isTelnetConnected = false;

// Current M4A1S Equip State
var currentlyEquipped = false;

// Last Equipped Weapon
var lastWeapon = "";

// Start Listening, if an error occurs stop the program
if (!gsl.Start()) {
	Console.WriteLine("STOPPED");
	Environment.Exit(0);
}

// Check if CSGO is Running to Start the Telnet Client
while (Process.GetProcessesByName("csgo").Length <= 0) {
	Thread.Sleep(1000);
	Console.WriteLine("Waiting for CS:GO to Start");
}

// Initialize Telnet Connection to the CS:GO Client
while (!isTelnetConnected)
	try {
		telnetClient = new Client(
			"localhost",
			config.TelnetPort,
			new CancellationToken());
		isTelnetConnected = true;
	} catch (Exception _) {
		Console.WriteLine("Waiting for Telnet Connection");
		Thread.Sleep(1000);
	}

// Listen on New Game States and handle them accordingly
gsl.NewGameState += gs => {
	// Fetch current Weapon Name
	var weaponName = gs.Player.Weapons.ActiveWeapon.Name;

	// If Weapon is not M4A1S reset currentlyEquipped
	if (weaponName != config.WeaponIdentifier && weaponName != lastWeapon) {
		ExecuteCommands(config.BindCommands);
		currentlyEquipped = false;
		return;
	}

	// Return if M4A1-S is already equipped
	if (currentlyEquipped) return;

	// Set currentlyEquipped to true
	currentlyEquipped = true;

	// Send Telnet command to CS:GO
	Console.WriteLine("M4A1-S is active");
	ExecuteCommands(config.UnbindCommands);
};

// Just print a message to the console
Console.WriteLine($"Listening on GSI Events...\nTelnet Connection: {telnetClient?.IsConnected}");

// Execute all the Commands given as a List
async void ExecuteCommands (List<string> commands) {
	// Skip if Telnet Client is Null
	if (telnetClient == null) return;

	// Execute all Commands
	foreach (var command in commands) await telnetClient?.WriteLineAsync(command)!;
}

// Get the Config File if it exists or create a default one
Config GetConfigFile () {
	// Determine File Path for the Config File
	var filePath = Directory.GetCurrentDirectory() + "\\config.json";

	// Create new Config File if it does not exist
	if (!File.Exists("config.json")) {
		// Write Config File
		File.WriteAllText(filePath, JsonConvert.SerializeObject(new Config
		{
			WeaponIdentifier = "weapon_m4a1_silencer",
			BindCommands = new List<string>
			{
				"bind mouse2 +attack2",
				"echo M4A1-S is inactive"
			},
			UnbindCommands = new List<string>
			{
				"echo M4A1-S is active",
				"-attack2",
				"unbind mouse2"
			},
			GsiPort = 3000,
			TelnetPort = 2121
		}, Formatting.Indented));
		
		// Print
		Console.WriteLine($"Created new Config File at {filePath}");
	}

	// Read Config File and return it
	return JsonConvert.DeserializeObject<Config>(File.ReadAllText(filePath));
}
