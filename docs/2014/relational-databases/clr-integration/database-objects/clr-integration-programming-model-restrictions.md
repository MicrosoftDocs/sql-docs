---
title: "CLR Integration Programming Model Restrictions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "common language runtime [SQL Server], programming model restrictions"
  - "assemblies [CLR integration], CREATE ASSEMBLY checks"
  - "programming model restrictions [CLR integration]"
  - "assemblies [CLR integration], runtime checks"
ms.assetid: 2446afc2-9d21-42d3-9847-7733d3074de9
author: rothja
ms.author: jroth
manager: craigg
---
# CLR Integration Programming Model Restrictions
  When you are building a managed stored procedure or other managed database object, there are certain code checks performed by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] performs checks on the managed code assembly when it is first registered in the database, using the `CREATE ASSEMBLY` statement, and also at runtime. The managed code is also checked at runtime because in an assembly there may be code paths that may never actually be reached at runtime.  This provides flexibility for registering third party assemblies, especially, so that an assembly wouldn't be blocked where there is 'unsafe' code designed to run in a client environment but would never be executed in the hosted CLR. The requirements that the managed code must meet depend on whether the assembly is registered as `SAFE`, `EXTERNAL_ACCESS`, or `UNSAFE`, `SAFE` being the strictest, and are listed below.  
  
 In addition to restrictions being placed on the managed code assemblies, there are also code security permissions that are granted. The common language runtime (CLR) supports a security model called code access security (CAS) for managed code. In this model, permissions are granted to assemblies based on the identity of the code. `SAFE`, `EXTERNAL_ACCESS`, and `UNSAFE` assemblies have different CAS permissions. For more information, see [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md).  
  
## CREATE ASSEMBLY Checks  
 When the `CREATE ASSEMBLY` statement is run, the following checks are performed for each security level.  If any check fails, `CREATE ASSEMBLY` will fail with an error message.  
  
### Global (any security level)  
 All referenced assemblies must meet one or more of the following criteria:  
  
-   The assembly is already registered in the database.  
  
-   The assembly is one of the supported assemblies. For more information, see [Supported .NET Framework Libraries](supported-net-framework-libraries.md).  
  
-   You are using `CREATE ASSEMBLY FROM`*\<location>,* and all the referenced assemblies and their dependencies are available in *\<location>*.  
  
-   You are using `CREATE ASSEMBLY FROM`*\<bytes ...>,* and all the references are specified via space separated bytes.  
  
### EXTERNAL_ACCESS  
 All `EXTERNAL_ACCESS` assemblies must meet the following criteria:  
  
-   Static fields are not used to store information. Read-only static fields are allowed.  
  
-   PEVerify test is passed. The PEVerify tool (peverify.exe), which checks that the MSIL code and associated metadata meet type safety requirements, is provided with the .NET Framework SDK.  
  
-   Synchronization, for example with the `SynchronizationAttribute` class, is not used.  
  
-   Finalizer methods are not used.  
  
 The following custom attributes are disallowed in `EXTERNAL_ACCESS` assemblies:  
  
-   System.ContextStaticAttribute  
  
-   System.MTAThreadAttribute  
  
-   System.Runtime.CompilerServices.MethodImplAttribute  
  
-   System.Runtime.CompilerServices.CompilationRelaxationsAttribute  
  
-   System.Runtime.Remoting.Contexts.ContextAttribute  
  
-   System.Runtime.Remoting.Contexts.SynchronizationAttribute  
  
-   System.Runtime.InteropServices.DllImportAttribute  
  
-   System.Security.Permissions.CodeAccessSecurityAttribute  
  
-   System.Security.SuppressUnmanagedCodeSecurityAttribute  
  
-   System.Security.UnverifiableCodeAttribute  
  
-   System.STAThreadAttribute  
  
-   System.ThreadStaticAttribute  
  
### SAFE  
  
-   All `EXTERNAL_ACCESS` assembly conditions are checked.  
  
## Runtime Checks  
 At runtime, the code assembly is checked for the following conditions. If any of these conditions are found, the managed code will not be allowed to run and an exception will be thrown.  
  
### UNSAFE  
 Loading an assembly-either explicitly by calling the `System.Reflection.Assembly.Load()` method from a byte array, or implicitly through the use of `Reflection.Emit` namespace-is not permitted.  
  
### EXTERNAL_ACCESS  
 All `UNSAFE` conditions are checked.  
  
 All types and methods annotated with the following host protection attribute (HPA) values in the supported list of assemblies are disallowed.  
  
-   SelfAffectingProcessMgmt  
  
-   SelfAffectingThreading  
  
-   Synchronization  
  
-   SharedState  
  
-   ExternalProcessMgmt  
  
-   ExternalThreading  
  
-   SecurityInfrastructure  
  
-   MayLeakOnAbort  
  
-   UI  
  
 For more information about HPAs and a list of disallowed types and members in the supported assemblies, see [Host Protection Attributes and CLR Integration Programming](../../clr-integration-security-host-protection-attributes/host-protection-attributes-and-clr-integration-programming.md).  
  
### SAFE  
 All `EXTERNAL_ACCESS` conditions are checked.  
  
## See Also  
 [Supported .NET Framework Libraries](supported-net-framework-libraries.md)   
 [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md)   
 [Host Protection Attributes and CLR Integration Programming](../../clr-integration-security-host-protection-attributes/host-protection-attributes-and-clr-integration-programming.md)   
 [Creating an Assembly](../assemblies/creating-an-assembly.md)  
  
  
