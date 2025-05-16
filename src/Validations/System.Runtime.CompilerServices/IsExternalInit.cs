namespace System.Runtime.CompilerServices;

#if (NETSTANDARD || NETCOREAPP)
/// <summary>
/// Dummy class to solve compilation bug. 
/// See 
/// <see href="https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined">
/// Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported [duplicate]
/// </see> 
/// and
/// <see href="https://developercommunity.visualstudio.com/t/error-cs0518-predefined-type-systemruntimecompiler/1244809">
/// Error CS0518 Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported
/// </see> 
/// </summary>
/// <remarks>
/// Error details: after running <code>dotnet build</code> without this dummy type, got the following compiler error for netstandard2.1 and netcoreapp3.1
/// <code>
/// Restore complete (0.4s)
///  Validations netstandard2.1 failed with 2 error(s) (0.1s)
///    ~/Validations/Utilities/SimpleOption.cs(29,33): error CS0518: Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported
///    ~/Validations/Utilities/SimpleOption.cs(29,45): error CS0518: Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported
///  Validations netcoreapp3.1 failed with 2 error(s) (0.1s)
///    ~/Validations/Utilities/SimpleOption.cs(29,33): error CS0518: Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported
///    ~/Validations/Utilities/SimpleOption.cs(29,45): error CS0518: Predefined type 'System.Runtime.CompilerServices.IsExternalInit' is not defined or imported
/// </code>
/// </remarks>

public class IsExternalInit { }

/// <summary>
/// Another type not found within netstandard2.1 and netcoreapp3.1. 
/// Credit for hack to Matthew Watson 
/// <see href="https://stackoverflow.com/a/70034587/963299">
/// How can I use CallerArgumentExpression with Visual Studio 2022 and .net Standard 2.0 or .net 4.8?
/// </see>
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
internal sealed class CallerArgumentExpressionAttribute(string parameterName) : Attribute
{
    public string ParameterName { get; } = parameterName;
}
#endif