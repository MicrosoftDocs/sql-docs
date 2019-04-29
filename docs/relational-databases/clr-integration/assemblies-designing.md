---
title: "Designing Assemblies | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "designing assemblies [SQL Server]"
  - "assemblies [CLR integration], designing"
ms.assetid: 9c07f706-6508-41aa-a4d7-56ce354f9061
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Assemblies - Designing
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes the following factors you should consider when you design assemblies:  
  
-   Packaging assemblies  
  
-   Managing assembly security  
  
-   Restrictions on assemblies  
  
## Packaging Assemblies  
 An assembly can contain functionality for more than one [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] routine or type in its classes and methods. Most of the time, it makes sense to package the functionality of routines that perform related functions within the same assembly, especially if these routines share classes whose methods call one another. For example, classes that perform data entry management tasks for common language runtime (CLR) triggers and CLR stored procedures can be packaged in the same assembly. This is because the methods for these classes are more likely to call each other than those of less related tasks.  
  
 When you are packaging code into assembly, you should consider the following:  
  
-   CLR user-defined types and indexes that depend on CLR user-defined functions can cause persisted data to be in the database that depends on the assembly. Modifying the code of an assembly is frequently more complex when there is persisted data that depends on the assembly in the database. Therefore, it is generally better to separate code on which persisted data dependencies rely (such as user-defined types and indexes using user-defined functions) from code that does not have such persisted data dependencies. For more information, see [Implementing Assemblies](../../relational-databases/clr-integration/assemblies-implementing.md) and [ALTER ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-assembly-transact-sql.md).  
  
-   If a piece of managed code requires higher permission, it is better to separate that code into a separate assembly from code that does not require higher permission.  
  
## Managing Assembly Security  
 You can control how much an assembly can access resources protected by .NET Code Access Security when it runs managed code. You do this by specifying one of three permission sets when you create or modify an assembly: SAFE, EXTERNAL_ACCESS, or UNSAFE.  
  
### SAFE  
 SAFE is the default permission set and it is the most restrictive. Code run by an assembly with SAFE permissions cannot access external system resources such as files, the network, environment variables, or the registry. SAFE code can access data from the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases or perform computations and business logic that do not involve accessing resources outside the local databases.  
  
 Most assemblies perform computation and data management tasks without having to access resources outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, we recommend SAFE as the assembly permission set.  
  
### EXTERNAL_ACCESS  
 EXTERNAL_ACCESS allows for assemblies to access certain external system resources such as files, networks, Web services, environmental variables, and the registry. Only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins with EXTERNAL ACCESS permissions can create EXTERNAL_ACCESS assemblies.  
  
 SAFE and EXTERNAL_ACCESS assemblies can contain only code that is verifiably type-safe. This means that these assemblies can only access classes through well-defined entry points that are valid for the type definition. Therefore, they cannot arbitrarily access memory buffers not owned by the code. Additionally, they cannot perform operations that might have an adverse effect on the robustness of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process.  
  
### UNSAFE  
 UNSAFE gives assemblies unrestricted access to resources, both within and outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Code that is running from within an UNSAFE assembly can call unmanaged code.  
  
 Also, specifying UNSAFE allows for the code in the assembly to perform operations that are considered type-unsafe by the CLR verifier. These operations can potentially access memory buffers in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process space in an uncontrolled manner. UNSAFE assemblies can also potentially subvert the security system of either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the common language runtime. UNSAFE permissions should be granted only to highly trusted assemblies by experienced developers or administrators. Only members of the **sysadmin** fixed server role can create UNSAFE assemblies.  
  
## Restrictions on Assemblies  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] puts certain restrictions on managed code in assemblies to make sure that they can run in a reliable and scalable manner. This means that certain operations that can compromise the robustness of the server are not permitted in SAFE and EXTERNAL_ACCESS assemblies.  
  
### Disallowed Custom Attributes  
 Assemblies cannot be annotated with the following custom attributes:  
  
```  
System.ContextStaticAttribute  
System.MTAThreadAttribute  
System.Runtime.CompilerServices.MethodImplAttribute  
System.Runtime.CompilerServices.CompilationRelaxationsAttribute  
System.Runtime.Remoting.Contexts.ContextAttribute  
System.Runtime.Remoting.Contexts.SynchronizationAttribute  
System.Runtime.InteropServices.DllImportAttribute   
System.Security.Permissions.CodeAccessSecurityAttribute  
System.STAThreadAttribute  
System.ThreadStaticAttribute  
```  
  
 Additionally, SAFE and EXTERNAL_ACCESS assemblies cannot be annotated with the following custom attributes:  
  
```  
System.Security.SuppressUnmanagedCodeSecurityAttribute  
System.Security.UnverifiableCodeAttribute  
```  
  
### Disallowed .NET Framework APIs  
 Any [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] API that is annotated with one of the disallowed **HostProtectionAttributes** cannot be called from SAFE and EXTERNAL_ACCESS assemblies.  
  
```  
eSelfAffectingProcessMgmt  
eSelfAffectingThreading  
eSynchronization  
eSharedState   
eExternalProcessMgmt  
eExternalThreading  
eSecurityInfrastructure  
eMayLeakOnAbort  
eUI  
```  
  
### Supported .NET Framework Assemblies  
 Any assembly that is referenced by your custom assembly must be loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using CREATE ASSEMBLY. The following [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assemblies are already loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and, therefore, can be referenced by custom assemblies without having to use CREATE ASSEMBLY.  
  
```  
custommarshallers.dll  
Microsoft.visualbasic.dll  
Microsoft.visualc.dll  
mscorlib.dll  
system.data.dll  
System.Data.SqlXml.dll  
system.dll  
system.security.dll  
system.web.services.dll  
system.xml.dll  
System.Transactions  
System.Data.OracleClient  
System.Configuration  
```  
  
## See Also  
 [Assemblies &#40;Database Engine&#41;](../../relational-databases/clr-integration/assemblies-database-engine.md)   
 [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md)  
  
  
