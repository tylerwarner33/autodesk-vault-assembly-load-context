#if NETCOREAPP
using Autodesk.Connectivity.Explorer.Extensibility;
using Autodesk.Connectivity.JobProcessor.Extensibility;
using Autodesk.Connectivity.WebServices;
using System.Reflection;
using System.Runtime.Loader;

namespace IsolatedVaultAddin.Isolation;

/// <summary>
///	Isolated addin dependency container.
/// </summary>
/// <remarks>
///	References:
///	<a href="https://learn.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support">
///	Microsoft/Tutorials/Plugins
///	</a>
///	<a href="https://github.com/dotnet/coreclr/blob/v2.1.0/Documentation/design-docs/assemblyloadcontext.md">
///	GitHub/DotNet/CoreCLR
///	</a>
/// </remarks>
internal sealed class AddinLoadContext : AssemblyLoadContext
{
	/// <summary>
	///   Add-ins contexts storage.
	/// </summary>
	private static readonly Dictionary<string, AddinLoadContext> _dependenciesProviders = new(1);

	private readonly AssemblyDependencyResolver _resolver;

	private const BindingFlags MethodSearchFlags = BindingFlags.Public | BindingFlags.Instance;


	private AddinLoadContext(Type type, string addinName) : base(addinName)
	{
		string addinLocation = type.Assembly.Location;

		_resolver = new AssemblyDependencyResolver(addinLocation);
	}


	/// <summary>
	///   Resolve and load dependency any time one is loaded if it exists in the isolated addin dependency container.
	/// </summary>
	protected override Assembly? Load(AssemblyName assemblyName)
	{
		string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);

		return assemblyPath is not null
			 ? LoadFromAssemblyPath(assemblyPath)
			 : null;
	}


	/// <summary>
	///   Resolve and load unmanaged native dependency any time one is loaded if it exists in the isolated addin dependency container.
	/// </summary>
	protected override nint LoadUnmanagedDll(string assemblyName)
	{
		string? assemblyPath = _resolver.ResolveUnmanagedDllToPath(assemblyName);

		return assemblyPath is not null
			 ? LoadUnmanagedDllFromPath(assemblyPath)
			 : nint.Zero;
	}


	/// <summary>
	///   Determine if the <see cref="AssemblyLoadContext" /> is custom or still the default context.
	/// </summary>
	public static bool CheckIfCustomContext(Type type)
	{
		AssemblyLoadContext? currentContext = GetLoadContext(type.Assembly);

		return currentContext != Default;
	}


	/// <summary>
	///     Get or create a new isolated context for the type.
	/// </summary>
	public static AddinLoadContext GetDependenciesProvider(Type type)
	{
		// Assembly location used as context name and the unique provider key.
		string addinRoot = System.IO.Path.GetDirectoryName(type.Assembly.Location)!;
		if (_dependenciesProviders.TryGetValue(addinRoot, out var provider))
		{
			return provider;
		}

		string addinName = System.IO.Path.GetFileName(addinRoot);
		provider = new AddinLoadContext(type, addinName);
		_dependenciesProviders.Add(addinRoot, provider);

		return provider;
	}


	/// <summary>
	///   Create new instance in the separated context.
	/// </summary>
	public object CreateAssemblyInstance(Type type)
	{
		string assemblyLocation = type.Assembly.Location;
		Assembly assembly = LoadFromAssemblyPath(assemblyLocation);

		return assembly.CreateInstance(type.FullName!)!;
	}


	/// <summary>
	///   Execute <see cref="IExplorerExtension.OnStartup" />, <see cref="IExplorerExtension.OnLogOn" />, <see cref="IExplorerExtension.OnLogOff" />, <see cref="IExplorerExtension.OnShutdown" /> methods in the isolated context.
	/// </summary>
	/// <remarks>
	///   Matches parameter format of <see cref="IExplorerExtension.OnStartup" />, <see cref="IExplorerExtension.OnLogOn" />, <see cref="IExplorerExtension.OnLogOff" />, <see cref="IExplorerExtension.OnShutdown" /> methods.
	/// </remarks>
	public static void Invoke(object instance, string methodName, IApplication application)
	{
		Type instanceType = instance.GetType();

		Type[] methodParameterTypes =
		[
			 typeof(IApplication)
		];

		object[] methodParameters =
		[
			 application
		];

		MethodInfo method = instanceType.GetMethod(methodName, MethodSearchFlags, null, methodParameterTypes, null)!;

		_ = method.Invoke(instance, methodParameters)!;
	}


	/// <summary>
	///   Execute <see cref="IJobHandler.OnJobProcessorStartup" />, <see cref="IJobHandler.OnJobProcessorShutdown" />, <see cref="IJobHandler.OnJobProcessorWake" />, <see cref="IJobHandler.OnJobProcessorSleep" /> methods in the isolated context.
	/// </summary>
	/// <remarks>
	///   Matches parameter format of <see cref="IJobHandler.OnJobProcessorStartup" />, <see cref="IJobHandler.OnJobProcessorShutdown" />, <see cref="IJobHandler.OnJobProcessorWake" />, <see cref="IJobHandler.OnJobProcessorSleep" /> methods.
	/// </remarks>
	public static void Invoke(object instance, string methodName, IJobProcessorServices context)
	{
		Type instanceType = instance.GetType();

		Type[] methodParameterTypes =
		[
			 typeof(IJobProcessorServices)
		];

		object[] methodParameters =
		[
			 context!
		];

		MethodInfo method = instanceType.GetMethod(methodName, MethodSearchFlags, null, methodParameterTypes, null)!;

		_ = method.Invoke(instance, methodParameters)!;
	}


	/// <summary>
	///   Execute <see cref="IJobHandler.Execute" /> method in the isolated context.
	/// </summary>
	/// <remarks>
	///   Matches parameter format of <see cref="IJobHandler.Execute" /> method.
	/// </remarks>
	public static JobOutcome Invoke(object instance, string methodName, IJobProcessorServices context, IJob job)
	{
		Type instanceType = instance.GetType();

		Type[] methodParameterTypes =
		[
			 typeof(IJobProcessorServices),
			 typeof(IJob)
		];

		object[] methodParameters =
		[
			 context!,
			 job!
		];

		MethodInfo method = instanceType.GetMethod(methodName, MethodSearchFlags, null, methodParameterTypes, null)!;

		return (JobOutcome)method.Invoke(instance, methodParameters)!;
	}


	/// <summary>
	///   Execute <see cref="IJobHandler.CanProcess" /> method in the isolated context.
	/// </summary>
	/// <remarks>
	///   Matches parameter format of <see cref="IJobHandler.CanProcess" /> method.
	/// </remarks>
	public static bool Invoke(object instance, string methodName, string jobType)
	{
		Type instanceType = instance.GetType();

		Type[] methodParameterTypes =
		[
			 typeof(string)
		];

		object[] methodParameters =
		[
			 jobType
		];

		MethodInfo method = instanceType.GetMethod(methodName, MethodSearchFlags, null, methodParameterTypes, null)!;

		return (bool)method.Invoke(instance, methodParameters)!;
	}


	/// <summary>
	///   Execute <see cref="IWebServiceExtension.OnLoad" /> method in the isolated context.
	/// </summary>
	/// <remarks>
	///   Matches parameter format of <see cref="IWebServiceExtension.OnLoad" /> method.
	/// </remarks>
	public static void Invoke(object instance, string methodName)
	{
		Type instanceType = instance.GetType();

		MethodInfo method = instanceType.GetMethod(methodName, MethodSearchFlags, null, [], null)!;

		_ = method.Invoke(instance, [])!;
	}


	/// <summary>
	///   Execute <see cref="IExplorerExtension.CommandSites" /> method in the isolated context.
	/// </summary>
	/// <remarks>
	///   Matches parameter format of <see cref="IExplorerExtension.CommandSites" /> method.
	/// </remarks>
	public static IEnumerable<T>? Invoke<T>(object instance, string methodName)
	{
		Type instanceType = instance.GetType();

		MethodInfo method = instanceType.GetMethod(methodName, MethodSearchFlags, null, [], null)!;

		return (IEnumerable<T>?)method.Invoke(instance, [])!;
	}
}
#endif