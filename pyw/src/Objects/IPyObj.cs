namespace CeetemSoft.Pyw;

/// <summary>
///     All py object wrappers should implement this interface
/// </summary>
public interface IPyObj
{
    /// <summary>
    ///     Returns the handle of the object
    /// </summary>
    public nint GetHandle();

    /// <summary>
    ///     Returns the typename for the python object wrapped by the wrapper
    /// </summary>
    public string GetPyTypename();
}