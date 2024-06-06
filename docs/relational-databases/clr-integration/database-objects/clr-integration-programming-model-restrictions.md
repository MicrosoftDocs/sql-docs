---
title: CLR integration programming model restrictions
description: SQL Server performs code checks on managed database objects when first registered using CREATE ASSEMBLY and at runtime.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/17/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "common language runtime [SQL Server], programming model restrictions"
  - "assemblies [CLR integration], CREATE ASSEMBLY checks"
  - "programming model restrictions [CLR integration]"
  - "assemblies [CLR integration], runtime checks"
---
# CLR integration programming model restrictions

[!INCLUDE [sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

When you build a managed stored procedure or other managed database object, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] performs certain code checks that need to be considered. These checks are performed on the managed code assembly when first registered in the database, using the `CREATE ASSEMBLY` statement, and also at runtime. The managed code is also checked at runtime because in an assembly there might be code paths that might never actually be reached at runtime.

These code checks provide flexibility for registering third-party assemblies especially, so that an assembly isn't blocked where there's *unsafe* code designed to run in a client environment, but would never be executed in the hosted common language runtime (CLR). The requirements that the managed code must meet depend on whether the assembly is registered as `SAFE`, `EXTERNAL_ACCESS`, or `UNSAFE`. `SAFE` is the strictest security level.

In addition to restrictions being placed on the managed code assemblies, there are also code security permissions that are granted. The CLR supports a security model called code access security (CAS) for managed code. In this model, permissions are granted to assemblies based on the identity of the code. `SAFE`, `EXTERNAL_ACCESS`, and `UNSAFE` assemblies have different CAS permissions. For more information, see [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md).

If the [publisher policy](/dotnet/framework/configure-apps/how-to-create-a-publisher-policy) is set, `CREATE ASSEMBLY` fails.

## CREATE ASSEMBLY checks

When the `CREATE ASSEMBLY` statement runs, the following checks are performed for each security level. If any check fails, `CREATE ASSEMBLY` fails with an error message.

### Global (any security level)

All referenced assemblies must meet one or more of the following criteria:

- The assembly is already registered in the database.

- The assembly is one of the supported assemblies. For more information, see [Supported .NET Framework Libraries](supported-net-framework-libraries.md).

- You're using `CREATE ASSEMBLY FROM <location>`, and all the referenced assemblies and their dependencies are available in `<location>`.

- You're using `CREATE ASSEMBLY FROM <bytes ...>`, and all the references are specified via space separated bytes.

### EXTERNAL_ACCESS

All `EXTERNAL_ACCESS` assemblies must meet the following criteria:

- Static fields aren't used to store information. Read-only static fields are allowed.

- The PEVerify test is passed. The PEVerify tool (`peverify.exe`), which checks that the common intermediate language (CIL) code and associated metadata meet type safety requirements, is provided with the .NET Framework SDK.

- Synchronization, for example with the `SynchronizationAttribute` class, isn't used.

- Finalizer methods aren't used.

The following custom attributes are disallowed in `EXTERNAL_ACCESS` assemblies:

- `System.ContextStaticAttribute`
- `System.MTAThreadAttribute`
- `System.Runtime.CompilerServices.MethodImplAttribute`
- `System.Runtime.CompilerServices.CompilationRelaxationsAttribute`
- `System.Runtime.Remoting.Contexts.ContextAttribute`
- `System.Runtime.Remoting.Contexts.SynchronizationAttribute`
- `System.Runtime.InteropServices.DllImportAttribute`
- `System.Security.Permissions.CodeAccessSecurityAttribute`
- `System.Security.SuppressUnmanagedCodeSecurityAttribute`
- `System.Security.UnverifiableCodeAttribute`
- `System.STAThreadAttribute`
- `System.ThreadStaticAttribute`

### SAFE

- All `EXTERNAL_ACCESS` assembly conditions are checked.

## Runtime checks

At runtime, the code assembly is checked for the following conditions. If any of these conditions are found, the managed code isn't allowed to run, and an exception is thrown.

### UNSAFE

You can't load an assembly, either explicitly by calling the `System.Reflection.Assembly.Load()` method from a byte array, or implicitly using `Reflection.Emit` namespace.

### EXTERNAL_ACCESS

All `UNSAFE` conditions are checked.

All types and methods annotated with the following host protection attribute (HPA) values in the supported list of assemblies are disallowed.

- `SelfAffectingProcessMgmt`
- `SelfAffectingThreading`
- `Synchronization`
- `SharedState`
- `ExternalProcessMgmt`
- `ExternalThreading`
- `SecurityInfrastructure`
- `MayLeakOnAbort`
- `UI`

For more information about HPAs and a list of disallowed types and members in the supported assemblies, see [Host Protection Attributes and CLR Integration Programming](../../clr-integration-security-host-protection-attributes/host-protection-attributes-and-clr-integration-programming.md).

### SAFE

All `EXTERNAL_ACCESS` conditions are checked.

## Related content

- [Supported .NET Framework Libraries](supported-net-framework-libraries.md)
- [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md)
- [Host Protection Attributes and CLR Integration Programming](../../clr-integration-security-host-protection-attributes/host-protection-attributes-and-clr-integration-programming.md)
- [Creating an Assembly](../assemblies/creating-an-assembly.md)
