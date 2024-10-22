```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19044.5011/21H2/November2021Update)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.403
  [Host] : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2 DEBUG [AttachedDebugger]

Toolchain=InProcessNoEmitToolchain  

```
| Method                  | Mean       | Error    | StdDev   |
|------------------------ |-----------:|---------:|---------:|
| AnalyzeWithTextReader   |   689.7 μs |  4.40 μs |  3.90 μs |
| AnalyzeWithRegex        | 2,165.1 μs | 42.02 μs | 39.31 μs |
| AnalyzeWithSplitAndLinq |   680.8 μs |  5.04 μs |  4.47 μs |
