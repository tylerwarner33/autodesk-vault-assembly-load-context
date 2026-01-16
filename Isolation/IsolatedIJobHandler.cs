using Autodesk.Connectivity.Explorer.Extensibility;
using Autodesk.Connectivity.JobProcessor.Extensibility;
using System.ComponentModel;

namespace IsolatedVaultAddin.Isolation;

/// <summary>
///	<see cref="IJobHandler" /> is the entry point of the Vault job processor addin.
///	This class is extended to have fully isolated addin dependency container.
///	Inherit this class and add custom logic to overrides of <see cref="OnExecute" />, <see cref="OnCanProcess" />, <see cref="JobProcessorStartup" />, <see cref="JobProcessorShutdown" />, <see cref="JobProcessorWake" />, or <see cref="JobProcessorSleep" />.
/// </summary>
/// <remarks>
///	Must set 'Private' to 'False' on 'Autodesk.Connectivity.Explorer.Extensibility', 'Autodesk.Connectivity.Explorer.ExtensibilityTools', 'Autodesk.Connectivity.Extensibility.Framework', 'Autodesk.Connectivity.JobProcessor.Extensibility', 'Autodesk.Connectivity.WebServices', 'Autodesk.DataManagement.Client.Framework', 'Autodesk.DataManagement.Client.Framework.Vault'  project file references.
/// </remarks>
public abstract class IsolatedIJobHandler : IJobHandler
{
#if NETCOREAPP
	private object? _isolatedInstance;
#endif

	/// <summary>
	///	Reference to the parameter in <see cref="IExplorerExtension.Execute" />, <see cref="IExplorerExtension.OnJobProcessorStartup" />, <see cref="IExplorerExtension.OnJobProcessorShutdown" />, <see cref="IExplorerExtension.OnJobProcessorWake" />, <see cref="IExplorerExtension.OnJobProcessorSleep" />.
	/// </summary>
	public IJobProcessorServices Context { get; private set; } = default!;

	/// <summary>
	///	Reference to the parameter in <see cref="IExplorerExtension.Execute" />.
	/// </summary>
	public IJob Job { get; private set; } = default!;

	/// <summary>
	///	Reference to the parameter in <see cref="IExplorerExtension.CanProcess" />.
	/// </summary>
	public string JobType { get; private set; } = default!;


	[EditorBrowsable(EditorBrowsableState.Never)]

	public JobOutcome Execute(IJobProcessorServices context, IJob job)
	{
		Type currentType = GetType();


#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			return AddinLoadContext.Invoke(_isolatedInstance, nameof(Execute), context, job);
		}
#endif

		Context = context;
		Job = job;

#if NETCOREAPP
		return OnExecute();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			return OnExecute();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}



	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool CanProcess(string jobType)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			return AddinLoadContext.Invoke(_isolatedInstance, nameof(CanProcess), jobType);
		}
#endif

		JobType = jobType;

#if NETCOREAPP
		return OnCanProcess();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			return OnCanProcess();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnJobProcessorStartup(IJobProcessorServices context)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnJobProcessorStartup), context);
			return;
		}
#endif

		Context = context;

#if NETCOREAPP
		JobProcessorStartup();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			JobProcessorStartup();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnJobProcessorShutdown(IJobProcessorServices context)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnJobProcessorShutdown), context);
			return;
		}
#endif

		Context = context;

#if NETCOREAPP
		JobProcessorShutdown();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			JobProcessorShutdown();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnJobProcessorWake(IJobProcessorServices context)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnJobProcessorWake), context);
			return;
		}
#endif

		Context = context;

#if NETCOREAPP
		JobProcessorWake();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			JobProcessorWake();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	[EditorBrowsable(EditorBrowsableState.Never)]
	public void OnJobProcessorSleep(IJobProcessorServices context)
	{
		Type currentType = GetType();

#if NETCOREAPP
		if (AddinLoadContext.CheckIfCustomContext(currentType) is false)
		{
			AddinLoadContext dependenciesProvider = AddinLoadContext.GetDependenciesProvider(currentType);
			_isolatedInstance = dependenciesProvider.CreateAssemblyInstance(currentType);

			AddinLoadContext.Invoke(_isolatedInstance, nameof(OnJobProcessorSleep), context);
			return;
		}
#endif

		Context = context;

#if NETCOREAPP
		JobProcessorSleep();
#else
		try
		{
			ResolveHelper.BeginAssemblyResolve(currentType);
			JobProcessorSleep();
		}
		finally
		{
			ResolveHelper.EndAssemblyResolve();
		}
#endif
	}


	/// <summary>
	///	Overload this method to execute custom logic when the Vault job processor is ... and <see cref="IJobHandler.ExecuteAsync" /> method is executed.
	/// </summary>
	public abstract JobOutcome OnExecute();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault job processor is ... and <see cref="IJobHandler.CanProcess" /> method is executed.
	/// </summary>
	public abstract bool OnCanProcess();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault job processor is ... and <see cref="IJobHandler.OnJobProcessorStartup" /> method is executed.
	/// </summary>
	public abstract void JobProcessorStartup();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault job processor is ... and <see cref="IJobHandler.OnJobProcessorShutdown" /> method is executed.
	/// </summary>
	public abstract void JobProcessorShutdown();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault job processor is ... and <see cref="IJobHandler.OnJobProcessorWake" /> method is executed.
	/// </summary>
	public abstract void JobProcessorWake();

	/// <summary>
	///	Overload this method to execute custom logic when the Vault job processor is ... and <see cref="IJobHandler.OnJobProcessorSleep" /> method is executed.
	/// </summary>
	public abstract void JobProcessorSleep();
}
