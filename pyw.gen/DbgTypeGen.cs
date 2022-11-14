using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Text;

[Generator]
internal class DbgTypeGen : IIncrementalGenerator
{
    private const string PyObjIface = "IPyObj";
    private const string Filename   = "pytypes_dbg.g.cs";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Create the syntax provider transformation
        IncrementalValuesProvider<StructDeclarationSyntax> syntaxProvider =
            context.SyntaxProvider.CreateSyntaxProvider<StructDeclarationSyntax>(FilterStructType, FilterIface)
            .Where(FilterNullType);

        // Combine the syntax provider with the compilation provider
        IncrementalValueProvider<(Compilation, ImmutableArray<StructDeclarationSyntax>)> compilationProvider =
            context.CompilationProvider.Combine(syntaxProvider.Collect());

        // Generate source code
        context.RegisterSourceOutput(compilationProvider, GenerateSource);
    }

    private void GenerateSource(SourceProductionContext context,
        (Compilation compilation, ImmutableArray<StructDeclarationSyntax> types) tuple)
    {
        Compilation   compilation = tuple.compilation;
        StringBuilder text        = new StringBuilder();

        foreach (StructDeclarationSyntax structDecl in tuple.types)
        {

        }

        context.AddSource(Filename, text.ToString());
    }

    private void AddDbgDispAttr(string typename, StringBuilder text)
    {

    }

    private void AddDbgTypeProxyAttr(string typename, StringBuilder text)
    {

    }

    private bool FilterStructType(SyntaxNode node, CancellationToken cancelToken)
    {
        // We are looking for structure decalarations
        return ((node is StructDeclarationSyntax structDecl) && (structDecl.BaseList != null));
    }

    private bool FilterNullType(object type)
    {
        return (type != null);
    }

    private StructDeclarationSyntax FilterIface(GeneratorSyntaxContext context, CancellationToken cancelToken)
    {
        StructDeclarationSyntax structDecl = (StructDeclarationSyntax)context.Node;

        // Iterate through all of the base types of the structure
        foreach (SyntaxNode child in structDecl.BaseList.ChildNodes())
        {
            if ((context.SemanticModel.GetSymbolInfo(child, cancelToken).Symbol is not INamedTypeSymbol typeSymbol) ||
                (typeSymbol.Name != PyObjIface))
            {
                continue;
            }

            return structDecl;
        }

        // No declaration found
        return null;
    }
}