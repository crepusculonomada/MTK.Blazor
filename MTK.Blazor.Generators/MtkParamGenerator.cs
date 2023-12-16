﻿using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

//TODO: Need to fetch info from ViewModel and generate code for each property

namespace MTK.Blazor.Generators;

[Generator]
public class MtkParamGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classes = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (node, _) => QueryNodes(node),
            transform: static (syntaxContext, _) => GetSyntaxNodes(syntaxContext)
        );

        context.RegisterSourceOutput(classes, static (ctx, source) => Execute(ctx, source));

        context.RegisterPostInitializationOutput(static (ctx) => PostInitializationOutput(ctx));
    }

    private static void PostInitializationOutput(IncrementalGeneratorPostInitializationContext ctx)
    {
        var text = new StringBuilder();
        var fileName = "MtkParams.g.cs";

        text.Append("namespace MTK.Blazor;\n");
        text.Append("[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]\n");
        text.Append("internal class MtkParamAttribute(string name) : Attribute{\n");
        text.Append("public string Name { get; } = name;\n");
        text.Append("}\n");

        ctx.AddSource(fileName, text.ToString());
    }

    private static bool QueryNodes(SyntaxNode node)
        => node is ClassDeclarationSyntax classDeclarationSyntax && classDeclarationSyntax.AttributeLists.Any();


    private static ClassToGenerate? GetSyntaxNodes(GeneratorSyntaxContext syntaxContext)
    {
        var classSyntax = (ClassDeclarationSyntax)syntaxContext.Node;
        var classSymbol = syntaxContext.SemanticModel.GetDeclaredSymbol(classSyntax);
        if (classSymbol is null) return null;

        var classInfo = syntaxContext.SemanticModel.Compilation.GetTypeByMetadataName(classSymbol.ToDisplayString());
        if (classInfo is null) return null;

        var attributeSymbol =
            syntaxContext.SemanticModel.Compilation.GetTypeByMetadataName("MTK.Blazor.MtkParamAttribute");

        var attributes = classInfo.GetAttributes()
            .Where(a => a.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default)).ToList();

        if (!attributes.Any()) return null;

        var paramsToGenerate = attributes.Select(a =>
        {
            var name = a.ConstructorArguments.FirstOrDefault().Value?.ToString();
            return string.IsNullOrEmpty(name) ? null : new ParamsToGenerate(name!, "int");
        }).Where(p => p != null);

        return new ClassToGenerate(classInfo.ContainingNamespace.ToDisplayString(), classInfo.Name, paramsToGenerate!);
    }

    private static void Execute(SourceProductionContext context, ClassToGenerate? classToGenerate)
    {
        if (classToGenerate is null) return;

        var text = new StringBuilder();
        var fileName = $"{classToGenerate.NameSpaceName}.{classToGenerate.Name}.g.cs";

        text.Append("// AutoGenerated \n");

        if (classToGenerate.Params.Any())
        {
            text.Append($"namespace {classToGenerate.NameSpaceName};\n\n");
            text.Append($"using Microsoft.AspNetCore.Components;\n");
            text.Append($"using System;\n\n");

            text.Append($"public partial class {classToGenerate.Name} {{\n");

            foreach (var par in classToGenerate.Params)
            {
                text.Append(
                    $"\t [Parameter] public {par.ParamType} {par.ParamName} {{ get => ViewModel.{par.ParamName}; set => ViewModel.{par.ParamName} = value; }}\n");
            }

            text.Append("}");
        }

        context.AddSource(fileName, text.ToString());
    }

    private class ParamsToGenerate(string paramName, string paramType)
    {
        public string ParamName { get; } = paramName;
        public string ParamType { get; } = paramType;
    }

    private class ClassToGenerate(string nameSpaceName, string name, IEnumerable<ParamsToGenerate> @params)
    {
        public string NameSpaceName { get; } = nameSpaceName;
        public string Name { get; } = name;
        public IEnumerable<ParamsToGenerate> Params { get; } = @params;
    }
}