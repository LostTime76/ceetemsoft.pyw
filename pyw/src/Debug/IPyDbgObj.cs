namespace CeetemSoft.Pyw;

/// <summary>
///     All python object wrappers should implement this interface for proper debugging support
/// </summary>
public interface IPyDbgObj
{
    /// <summary>
    ///     Returns a value that indicates if the object is valid
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    ///     Returns the handle of the python object
    /// </summary>
    public nint Handle { get; }

    /// <summary>
    ///     Returns the python object typename that the wrapper wraps
    /// </summary>
    public string PyTypename { get; }

    /// <summary>
    ///     Returns a debug object for the wrapper
    /// </summary>
    public PyDbgObj DbgObj { get; }

    /// <summary>
    ///     Creates a new wrapper object using the given handle
    /// </summary>
    public static abstract IPyDbgObj FromHandle(nint hObj);
}