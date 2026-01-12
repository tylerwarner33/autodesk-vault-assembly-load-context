using Autodesk.Connectivity.JobProcessor.Extensibility;
using IsolatedVaultAddin.Isolation;

namespace IsolatedVaultAddin;

/// <remarks>
///	Assembly attributes (ApiVersion, ExtensionId) are generated via MSBuild in the .csproj file.
/// </remarks>
public class VaultJobProcessorExtension : IsolatedIJobHandler
{
	public override bool OnCanProcess() => true;

	public override JobOutcome OnExecute() => JobOutcome.Success;

	public override void JobProcessorStartup() { }

	public override void JobProcessorShutdown() { }

	public override void JobProcessorWake() { }

	public override void JobProcessorSleep() { }
}