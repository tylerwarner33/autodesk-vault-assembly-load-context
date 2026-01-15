using Autodesk.Connectivity.Explorer.Extensibility;
using System.ComponentModel;

//TODO: Handle `DockPanels()` method only being available in Vault 2026+.

namespace IsolatedVaultAddin.Isolation;

/// <summary>
///	<see cref="IExplorerExtension" /> is the entry point of the Vault addin.
///	This class is extended to have fully isolated addin dependency container.
///	Inherit this class and add custom logic to overrides of <see cref="Startup" />, <see cref="LogOn" />, <see cref="LogOff" />, <see cref="Shutdown" />, <see cref="OnCommandSites" />, <see cref="OnCustomEntityHandlers" />, <see cref="OnDetailTabs" />, <see cref="OnDockPanels" />, or <see cref="OnHiddenCommands" />.
/// </summary>
/// <remarks>
///	Must set 'Private' to 'False' on 'Autodesk.Connectivity.Explorer.Extensibility', 'Autodesk.Connectivity.Explorer.ExtensibilityTools', 'Autodesk.Connectivity.Extensibility.Framework', 'Autodesk.Connectivity.JobProcessor.Extensibility', 'Autodesk.Connectivity.WebServices', 'Autodesk.DataManagement.Client.Framework', 'Autodesk.DataManagement.Client.Framework.Vault'  project file references.
/// </remarks>
public abstract class IsolatedIExplorerExtension : IExplorerExtension
{
#if NETCOREAPP
	private object? _isolatedInstance;
#endif

	/// <summary>
	///	Reference to the parameter in <see cref="IExplorerExtension.OnStartup" />, <see cref="IExplorerExtension.OnLogOn" />, <see cref="IExplorerExtension.OnLogOff" />, <see cref="IExplorerExtension.OnShutdown" />.
	/// </summary>
	public IApplication Application { get; private set; } = default!;


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnStartup(IApplication application)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnStartup), application);
			return;
		}
#endif

		Application = application;

#if NETCOREAPP
		Startup();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			Startup();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnLogOn(IApplication application)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnLogOn), application);
			return;
		}
#endif

		Application = application;

#if NETCOREAPP
		LogOn();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			LogOn();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnLogOff(IApplication application)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnLogOff), application);
			return;
		}
#endif

		Application = application;

#if NETCOREAPP
		LogOff();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			LogOff();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnShutdown(IApplication application)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnShutdown), application);
			return;
		}
#endif

		Application = application;

#if NETCOREAPP
		Shutdown();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			Shutdown();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<CommandSite>? CommandSites()
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			return AddinLoadContext.Invoke<CommandSite>(_isolatedInstance, nameof(CommandSites));
		}
#endif

#if NETCOREAPP
		return OnCommandSites();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			return OnCommandSites();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<CustomEntityHandler>? CustomEntityHandlers()
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			return AddinLoadContext.Invoke<CustomEntityHandler>(_isolatedInstance, nameof(CustomEntityHandlers));
		}
#endif

#if NETCOREAPP
		return OnCustomEntityHandlers();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			return OnCustomEntityHandlers();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<DetailPaneTab>? DetailTabs()
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			return AddinLoadContext.Invoke<DetailPaneTab>(_isolatedInstance, nameof(DetailTabs));
		}
#endif

#if NETCOREAPP
		return OnDetailTabs();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			return OnDetailTabs();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


#if VAULT_HAS_DOCK_PANELS
	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<DockPanel>? DockPanels()
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			return AddinLoadContext.Invoke<DockPanel>(_isolatedInstance, nameof(DockPanels));
		}
#endif

#if NETCOREAPP
		return OnDockPanels();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			return OnDockPanels();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}
#endif


	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<string>? HiddenCommands()
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			return AddinLoadContext.Invoke<string>(_isolatedInstance, nameof(HiddenCommands));
		}
#endif

#if NETCOREAPP
		return OnHiddenCommands();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			return OnHiddenCommands();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin is loaded and <see cref="IExplorerExtension.OnStartup" /> method is executed.
	/// </summary>
	public abstract void Startup();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault is logged into and <see cref="IExplorerExtension.OnLogOn" /> method is executed.
	/// </summary>
	public abstract void LogOn();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault is logged out of and <see cref="IExplorerExtension.OnLogOff" /> method is executed.
	/// </summary>
	public abstract void LogOff();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin is unloaded and <see cref="IExplorerExtension.OnShutdown" /> method is executed.
	/// </summary>
	public abstract void Shutdown();


	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin ... and <see cref="IExplorerExtension.CommandSites" /> method is executed.
	/// </summary>
	public abstract IEnumerable<CommandSite>? OnCommandSites();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin ... and <see cref="IExplorerExtension.CustomEntityHandlers" /> method is executed.
	/// </summary>
	public abstract IEnumerable<CustomEntityHandler>? OnCustomEntityHandlers();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin ... and <see cref="IExplorerExtension.DetailTabs" /> method is executed.
	/// </summary>
	public abstract IEnumerable<DetailPaneTab>? OnDetailTabs();

#if VAULT_HAS_DOCK_PANELS
	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin ... and <see cref="IExplorerExtension.DockPanels" /> method is executed.
	/// </summary>
	public abstract IEnumerable<DockPanel>? OnDockPanels();
#endif	

	/// <summary>
	///	Overload this method to execute custom logic when the Vault addin ... and <see cref="IExplorerExtension.HiddenCommands" /> method is executed.
	/// </summary>
	public abstract IEnumerable<string>? OnHiddenCommands();
}
