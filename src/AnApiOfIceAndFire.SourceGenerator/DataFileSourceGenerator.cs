using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;

namespace SourceGenerator
{
    [Generator]
    public class DataFileSourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var csvFiles = context.AdditionalTextsProvider.Where(file => file.Path.EndsWith(".csv"));
            var files = csvFiles.Select((text, cancellationToken) => (path: text.Path, lines: text.GetText(cancellationToken).Lines));

            context.RegisterSourceOutput(files, (spc, file) =>
            {
                var folderName = Path.GetFileName(Path.GetDirectoryName(file.path));
                var relativeFilePath = Path.Combine(folderName, Path.GetFileName(file.path));

                var fileNameWithoutExtenson = Path.GetFileNameWithoutExtension(file.path);
                var nameInPascalCase = SnakeCaseToPascalCase(fileNameWithoutExtenson);

                var maxId = -1;
                foreach (var line in file.lines.Skip(1))
                {
                    var str = line.ToString();
                    var first = str.IndexOf(',');
                    var possibleId = str.Substring(0, first);

                    if (int.TryParse(possibleId, out var possibleMaxId) && possibleMaxId > maxId)
                    {
                        maxId = possibleMaxId;
                    }
                }

                spc.AddSource($"DataConstants.{nameInPascalCase}.g.cs", $@"
namespace AnApiOfIceAndFire.Database.Seeder
{{
    public static partial class DataConstants
    {{
        public const string {nameInPascalCase}FilePath = @""{relativeFilePath}"";
        public const int {nameInPascalCase}MaxId = {maxId};
    }}
}}");
            });
        }

        private static string SnakeCaseToPascalCase(string text)
        {
            var individualWords = text.Split('_');

            return string.Join("", individualWords.Select(s => $"{char.ToUpper(s[0])}{s.Substring(1)}"));
        }
    }
}