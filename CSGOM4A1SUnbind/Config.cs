namespace CSGOM4A1SUnbind; 

public struct Config {
	public int GsiPort { get; set; }
	public int TelnetPort { get; set; }
	public List<string> UnbindCommands { get; set; }
	public List<string> BindCommands { get; set; }
	public List<string> WeaponIdentifiers { get; set; }
}
