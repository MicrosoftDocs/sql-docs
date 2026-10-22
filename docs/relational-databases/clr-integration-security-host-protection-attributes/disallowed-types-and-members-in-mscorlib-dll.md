---
title: "Disallowed Types and Members in Mscorlib.dll"
description: SQL Server CLR programming disallows a type or member with some values for the HostProtectionResource enum. This article lists mscorlib.dll disallowed values.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "host protection attributes [CLR integration]"
  - "common language runtime [SQL Server], host protection attributes"
---
# Disallowed types and members in mscorlib.dll

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] common language integration (CLR) programming disallows the use of a type or member that has a `HostProtectionAttribute` that specifies a `System.Security.Permissions.HostProtectionResource` enumeration with a value of `ExternalProcessMgmt`, `ExternalThreading`, `MayLeakOnAbort`, `SecurityInfrastructure`, `SelfAffectingProcessMgmt`, `SelfAffectingThreading`, `SharedState`, `Synchronization`, or `UI`. The following table lists the members and types of the mscorlib.dll assembly whose Host Protection Attribute (HPA) values are disallowed.

> [!NOTE]  
> This list was generated from the supported assemblies. For more information, see [Supported .NET Framework libraries](../clr-integration/database-objects/supported-net-framework-libraries.md).

| Type or member | HPA values |
| --- | --- |
| `SyncStream.BeginRead()` | `ExternalThreading` |
| `SyncStream.BeginWrite()` | `ExternalThreading` |
| `System.Collections.ArrayList.Synchronized()` | `Synchronization` |
| `System.Collections.Hashtable.Synchronized()` | `Synchronization` |
| `System.Collections.Queue.Synchronized()` | `Synchronization` |
| `System.Collections.SortedList.Synchronized()` | `Synchronization` |
| `System.Collections.Stack.Synchronized()` | `Synchronization` |
| `System.Console.Beep()` | `UI` |
| `System.Console.get_Error()` | `UI` |
| `System.Console.get_In()` | `UI` |
| `System.Console.get_KeyAvailable()` | `UI` |
| `System.Console.get_Out()` | `UI` |
| `System.Console.OpenStandardError()` | `UI` |
| `System.Console.OpenStandardInput()` | `UI` |
| `System.Console.OpenStandardOutput()` | `UI` |
| `System.Console.Read()` | `UI` |
| `System.Console.ReadKey()` | `UI` |
| `System.Console.ReadLine()` | `UI` |
| `System.Console.SetError()` | `UI` |
| `System.Console.SetIn()` | `UI` |
| `System.Console.SetOut()` | `UI` |
| `System.Console.Write()` | `UI` |
| `System.Console.WriteLine()` | `UI` |
| `System.Diagnostics.LogMessageEventHandler` | `ExternalThreading`, `Synchronization` |
| `System.IO.FileStream.BeginRead()` | `ExternalThreading` |
| `System.IO.FileStream.BeginWrite()` | `ExternalThreading` |
| `System.IO.Stream.Synchronized()` | `Synchronization` |
| `System.IO.TextReader.Synchronized()` | `Synchronization` |
| `System.IO.TextWriter.Synchronized()` | `Synchronization` |
| `System.Reflection.Emit.AssemblyBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.ConstructorBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.CustomAttributeBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.EnumBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.EventBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.FieldBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.MethodBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.MethodRental` | `MayLeakOnAbort` |
| `System.Reflection.Emit.ModuleBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.PropertyBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.TypeBuilder` | `MayLeakOnAbort` |
| `System.Reflection.Emit.UnmanagedMarshal` | `MayLeakOnAbort` |
| `System.Security.Principal.WindowsPrincipal` | `SecurityInfrastructure` |
| `System.Threading.AutoResetEvent` | `ExternalThreading`, `Synchronization` |
| `System.Threading.EventWaitHandle` | `ExternalThreading`, `Synchronization` |
| `System.Threading.ManualResetEvent` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Monitor` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Mutex` | `ExternalThreading`, `Synchronization` |
| `System.Threading.ReaderWriterLock` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Thread.AllocateDataSlot()` | `ExternalThreading`, `SharedState` |
| `System.Threading.Thread.AllocateNamedDataSlot()` | `ExternalThreading`, `SharedState` |
| `System.Threading.Thread.BeginCriticalRegion()` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Thread.EndCriticalRegion()` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Thread.FreeNamedDataSlot()` | `ExternalThreading`, `SharedState` |
| `System.Threading.Thread.GetData()` | `ExternalThreading`, `SharedState` |
| `System.Threading.Thread.GetNamedDataSlot()` | `ExternalThreading`, `SharedState` |
| `System.Threading.Thread.Join()` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Thread.set_ApartmentState()` | `Synchronization`, `SelfAffectingThreading` |
| `System.Threading.Thread.set_CurrentUICulture()` | `ExternalThreading` |
| `System.Threading.Thread.set_IsBackground()` | `SelfAffectingThreading` |
| `System.Threading.Thread.set_Name()` | `ExternalThreading` |
| `System.Threading.Thread.set_Priority()` | `SelfAffectingThreading` |
| `System.Threading.Thread.SetApartmentState()` | `Synchronization`, `SelfAffectingThreading` |
| `System.Threading.Thread.SetData()` | `ExternalThreading`, `SharedState` |
| `System.Threading.Thread.SpinWait()` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Thread.Start()` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Thread.TrySetApartmentState()` | `Synchronization`, `SelfAffectingThreading` |
| `System.Threading.ThreadPool` | `ExternalThreading`, `Synchronization` |
| `System.Threading.Timer` | `ExternalThreading`, `Synchronization` |
| `System.Threading.TimerBase` | `ExternalThreading`, `Synchronization` |

## Related content

- [Host protection attributes and CLR integration programming](host-protection-attributes-and-clr-integration-programming.md)
- [Disallowed types and members in Microsoft.VisualBasic.dll](disallowed-types-and-members-in-microsoft-visualbasic-dll.md)
- [Disallowed types and members in System.dll](disallowed-types-and-members-in-system-dll.md)
- [Disallowed types and members in System.Data.dll](disallowed-types-and-members-in-system-data-dll.md)
- [Disallowed types and members in System.Core.dll](disallowed-types-and-members-in-system-core-dll.md)
