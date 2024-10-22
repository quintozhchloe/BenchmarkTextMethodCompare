using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using System.Text.RegularExpressions;


public class TextAnalysisBenchmark
{

    private string filePath = "F:\\2024 S2\\Stats705\\test.txt";

    // Method 1: use TextReader
    [Benchmark]

    public void AnalyzeWithTextReader()
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            int lineCount = 0, wordCount = 0, charCount = 0;
            bool inWord = false;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCount++;
                foreach (char c in line)
                {
                    charCount++;
                    if (char.IsLetterOrDigit(c))
                    {
                        if (!inWord)
                        {
                            inWord = true;
                            wordCount++;
                        }
                    }
                    else
                    {
                        inWord = false;
                    }
                }
            }
        }
    }

    // Method 2: use Regex
    [Benchmark]
    public void AnalyzeWithRegex()
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            int lineCount = 0, wordCount = 0, charCount = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCount++;
                charCount += line.Length;

                MatchCollection matches = Regex.Matches(line, @"\b\w+\b");
                wordCount += matches.Count;
            }
        }
    }

    // Method 3: use Split and LINQ- small file
    [Benchmark]
    public void AnalyzeWithSplitAndLinq()
    {
        string[] lines = File.ReadAllLines(filePath);
        int lineCount = lines.Length;
        int wordCount = lines.SelectMany(line => line.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Count();
        int charCount = lines.Sum(line => line.Length);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var config = ManualConfig.Create(DefaultConfig.Instance)
            .WithOption(ConfigOptions.DisableOptimizationsValidator, true)
            .AddJob(BenchmarkDotNet.Jobs.Job.Default.WithToolchain(InProcessNoEmitToolchain.Instance));

        var summary = BenchmarkRunner.Run<TextAnalysisBenchmark>(config);
    }
}
