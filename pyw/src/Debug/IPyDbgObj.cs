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
    ///     Returns all of the members within the wrapper object
    /// </summary>
    public PyDbgMember[] DbgMembers { get; }

    /// <summary>
    ///     Creates a new wrapper object using the given handle
    /// </summary>
    public static abstract IPyDbgObj FromHandle(nint hObj);
}