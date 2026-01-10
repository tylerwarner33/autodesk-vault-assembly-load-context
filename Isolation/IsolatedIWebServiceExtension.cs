using Autodesk.Connectivity.WebServices;
using System.ComponentModel;

namespace IsolatedVaultAddin.Isolation;

/// <summary>
///	<see cref="IWebServiceExtension" /> is the entry point of the Vault addin.
///	This class is extended to have fully isolated addin dependency container.
///	Inherit this class and add custom logic to overrides of <see cref="Load" />.
/// </summary>
/// <remarks>
///	Must set 'Private' to 'False' on 'Autodesk.Connectivity.Explorer.Extensibility', 'Autodesk.Connectivity.Explorer.ExtensibilityTools', 'Autodesk.Connectivity.Extensibility.Framework', 'Autodesk.Connectivity.JobProcessor.Extensibility', 'Autodesk.Connectivity.WebServices', 'Autodesk.DataManagement.Client.Framework', 'Autodesk.DataManagement.Client.Framework.Vault'  project file references.
/// </remarks>
public abstract class IsolatedIWebServiceExtension : IWebServiceExtension
{
#if NETCOREAPP
	private object? _isolatedInstance;
#endif


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnLoad()
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnLoad));
			return;
		}
#endif


#if NETCOREAPP
		Load();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			Load();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin is loaded and <see cref="IWebServiceExtension.OnLoad" /> method is executed.
	/// </summary>
	public abstract void Load();
}
