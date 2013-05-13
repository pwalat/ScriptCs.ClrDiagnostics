## ScriptCs.ClrDiagnostics

ScriptCS script pack that allows for interactive managed process diagnostics under REPL. Uses CLR Memory Diagnostics (ClrMD) library.

### Quick start

* Install ScriptCS.ClrDiagnostics: `scriptcs -install ScriptCs.ClrDiagnostics -pre`
* Launch ScriptCS in REPL mode (you may need to get latest version for that): `scriptcs.exe`
* You should be able to load the script pack and attach to a process like this:
	
```csharp

// Load ClrDiag object
> var c = Require<ClrDiag>();

// Attach to process 
> c.Attach(6152)
Attaching to process PID=6152
Using CLR Version=v4.0.30319.18033 DACFileName=mscordacwks_amd64_Amd64_4.0.30319.18033.dll
Succesfully attached to process PID=6152 Name=WpfApplication2

// You can also use process name c.Attach("MyApplication")

// Check current state
> c.IsAttached 
True           

// Now you can access ClrMD API
> c.Clr.Threads.Count
2

> c.Process.WorkingSet64
51265536

// You can also analyze the heap using built-in helpers: 
> c.PrintTypes("System.");
Total size               Count   Name
167.82KB                 398     System.Object[]
143.29KB                 2404    System.String
126.98KB                 2385    System.Delegate[]
126.98KB                 2385    System.Delegate[]

> c.PrintStackTrace()                                                                                                                                     
Stacktrace for ThreadId=3200                                                                                                                              
           0       A9E9B8 InlinedCallFrame                                                                                                                
           0       A9E9B8 InlinedCallFrame                                                                                                                
 7F92AF92093       A9E990 DomainBoundILStubClass.IL_STUB_PInvoke(System.Windows.Interop.MSG ByRef, System.Runtime.InteropServices.HandleRef, Int32, Int32)
 7F92AF87640       A9EA60 System.Windows.Threading.Dispatcher.GetMessage(System.Windows.Interop.MSG ByRef, IntPtr, Int32, Int32)                          
 7F92AF85E9E       A9EB20 System.Windows.Threading.Dispatcher.PushFrameImpl(System.Windows.Threading.DispatcherFrame)                                     
 7F9272172DA       A9EBC0 System.Windows.Application.RunInternal(System.Windows.Window)                                                                   
 7F927216BD7       A9EC60 System.Windows.Application.Run()                                                                                                
 7F8E3700107       A9ECA0 WpfApplication2.App.Main()                                                                                                      

// Detach when you are finished
> c.Detach()                                                    
Successfully detached from process PID=6152 Name=WpfApplication2
```

### Reminder

Remember that attaching is an invasive process so do not try that in production environments. ClrMD is currently still in beta.

### Troubleshooting

ScriptCs.ClrDiagnostics lets you specify DAC file (*mscordacwks.dll*) location while attaching:

```csharp
> c.Attach(6152, @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\mscordacwks.dll");
```

This may be useful when troubleshooting debugging problems arising from mismatched CLR versions.

### Todo

*more coming soon ...*




