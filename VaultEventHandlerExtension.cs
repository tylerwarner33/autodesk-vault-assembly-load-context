using IsolatedVaultAddin.Isolation;

namespace IsolatedVaultAddin;

/// <remarks>
///	Assembly attributes (ApiVersion, ExtensionId) are generated via MSBuild in the .csproj file.
/// </remarks>
public class VaultEventHandlerExtension : IsolatedIWebServiceExtension
{
	public override void Load() { }
}