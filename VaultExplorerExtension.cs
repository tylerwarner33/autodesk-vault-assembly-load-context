using Autodesk.Connectivity.Explorer.Extensibility;
using Autodesk.Connectivity.Extensibility.Framework;
using IsolatedVaultAddin.Isolation;

// ApiVersion assembly-level attribute must match the targeted Vault version.
//TODO: Add dynamic parameter for ApiVersion.
[assembly: ApiVersion("19.0")]
[assembly: ExtensionId("5b01004f-b05b-4ef9-af7e-b9efd8bc16d1")]

namespace IsolatedVaultAddin;

// TODO: Add configurations to solution for Vault versions.

public class VaultExplorerExtension : IsolatedIExplorerExtension
{
	public override void Startup() { }

	public override void LogOn()
	{
		try
		{
			//TODO: Add button to show the Serilog versions window.
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Unable to customize the ribbon.\n\n{ex}");
		}
	}

	public override void LogOff() { }

	public override void Shutdown() { }

	public override IEnumerable<CommandSite>? OnCommandSites() => null;

	public override IEnumerable<CustomEntityHandler>? OnCustomEntityHandlers() => null;

	public override IEnumerable<DetailPaneTab>? OnDetailTabs() => null;

	public override IEnumerable<DockPanel>? OnDockPanels() => null;

	public override IEnumerable<string>? OnHiddenCommands() => null;
}