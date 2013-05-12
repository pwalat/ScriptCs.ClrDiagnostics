## ScriptCs.ClrDiagnostics

ScriptCS script pack that allows for interactive managed process diagnostics under REPL. Uses CLR Memory Diagnostics (ClrMD) library.

### Quick start
	1. Install ScriptCS.ClrDiagnostics: ```scriptcs -install ScriptCs.ClrDiagnostics -pre```
	2. Launch ScriptCS in REPL mode (you may need to get latest version for that): ```scriptcs.exe```
	3. You should be able to load the script pack and attach to a process like this:
	
```csharp

// Load ClrDiag object
> var c = Require<ClrDiag>();

// Attach to process 
> c.Attach(6152)
Attaching to process PID=6152
Using CLR Version=v4.0.30319.18033 DACFileName=mscordacwks_amd64_Amd64_4.0.30319.18033.dll
Succesfully attached to process PID=6152 Name=WpfApplication2

// Now you can access ClrMd API
> c.Clr.Threads.Count

// And other useful properties
> c.Process.WorkingSet64
51265536

// You can print various diagnostics: 
> c.PrintTypes("System.");
Total size               Count   Name
167.82KB                 398     System.Object[]
143.29KB                 2404    System.String
126.98KB                 2385    System.Delegate[]
126.98KB                 2385    System.Delegate[]




