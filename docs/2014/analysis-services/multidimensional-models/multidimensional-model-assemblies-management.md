---
title: "Multidimensional Model Assemblies Management | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "permissions [Analysis Services], assemblies"
  - "calling user-defined functions"
  - "user impersonation [Analysis Services]"
  - "impersonation [Analysis Services]"
  - "Data Mining Extensions [Analysis Services], assemblies"
  - "MDX [Analysis Services], assemblies"
  - "user-defined functions [Analysis Services]"
  - "Analysis Services objects, assemblies"
  - "assemblies [Analysis Services]"
  - "application domains [Analysis Services]"
ms.assetid: b2645d10-6d17-444e-9289-f111ec48bbfb
author: minewiskan
ms.author: owend
manager: craigg
---
# Multidimensional Model Assemblies Management
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supplies lots of intrinsic functions for use with the Multidimensional Expressions (MDX) and Data Mining Extensions (DMX) languages, designed to accomplish everything from standard statistical calculations to traversing members in a hierarchy. But, as with any other complex and robust product, there is always the need to extend the functionality of such a product further.  
  
 Therefore, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] lets you add assemblies to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance or database. Assemblies let you create external, user-defined functions using any common language runtime (CLR) language, such as Microsoft Visual Basic .NET or Microsoft Visual C#. You can also use Component Object Model (COM) Automation languages such as Microsoft Visual Basic or Microsoft Visual C++.  
  
> [!IMPORTANT]  
>  COM assemblies might pose a security risk. Due to this risk and other considerations, COM assemblies were deprecated in [!INCLUDE[ssASversion10](../../includes/ssasversion10-md.md)]. COM assemblies might not be supported in future releases.  
  
 Assemblies let you extend the business functionality of MDX and DMX. You build the functionality that you want into a library, such as a dynamic link library (DLL) and add the library as an assembly to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. The public methods in the library are then exposed as user-defined functions to MDX and DMX expressions, procedures, calculations, actions, and client applications.  
  
 An assembly with new procedures and functions can be added to the server. You can use assemblies to enhance or add custom functionality that is not provided by the server. By using assemblies, you can add new functions to Multidimensional Expressions (MDX), Data Mining Extensions (DMX), or stored procedures. Assemblies are loaded from the location where the custom application is run, and a copy of the assembly binary file is saved along with the database data in the server. When an assembly is removed, the copied assembly is also removed from the server.  
  
 Assemblies can be of two different types: COM and CLR. CLR assemblies are assemblies developed in .NET Framework programming languages such as C#, Visual Basic .NET, managed C++. COM assemblies are COM libraries that must be registered in the server  
  
 Assemblies can be added to <xref:Microsoft.AnalysisServices.Server> or <xref:Microsoft.AnalysisServices.Database> objects. Server assemblies can be called by any user connected to the server or any object in the server. Database assemblies can be called only by <xref:Microsoft.AnalysisServices.Database> objects or users connected to the database.  
  
 A simple <xref:Microsoft.AnalysisServices.Assembly> object is composed of basic information (Name and Id), file collection, and security specifications.  
  
 The file collection refers to the loaded assembly files and their corresponding debugging (.pdb) files, if the debugging files were loaded with the assembly files. Assembly files are loaded from the location where the application defined the files to, and a copy is saved in the server along with the data. The copy of the assembly file is used to load the assembly every time the service is started.  
  
 Security specifications include the permission set and the impersonation used to run the assembly.  
  
## Calling User-Defined Functions  
 Calling a user-defined function in an assembly is performed just like calling an intrinsic function, except that you must use a fully qualified name. For example, a user-defined function that returns a type expected by MDX is included in an MDX query, as shown in the following example:  
  
```  
Select MyAssembly.MyClass.MyStoredProcedure(a, b, c) on 0 from Sales  
```  
  
 User-defined functions can also be called using the CALL keyword. You must use the CALL keyword for user-defined functions which return recordsets or void values, and you cannot use the CALL keyword if the user-defined function depends on an object in the context of the MDX or DMX statement or script, such as the current cube or data mining model. A common use for a function called outside an MDX or DMX query is to use the AMO object model to perform administrative functions. If, for example, you wanted to use the function `MyVoidProcedure(a, b, c)` in an MDX statement, the following syntax would be employed:  
  
```  
Call MyAssembly.MyClass.MyVoidProcedure(a, b, c)  
```  
  
 Assemblies simplify database development by enabling common code to be developed once and stored in a single location. Client software developers can create libraries of functions for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and distribute them with their applications.  
  
 Assemblies and user-defined functions can duplicate the function names of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] function library or of other assemblies. As long as you call the user-defined function by using its fully qualified name, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will use the correct procedure. For security purposes, and to eliminate the chance of calling a duplicate name in a different class library, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] requires that you use only fully qualified names for stored procedures.  
  
 To call a user-defined function from a specific CLR assembly, the user-defined function is preceded by the assembly name, full class name, and procedure name, as demonstrated here:  
  
 *AssemblyName*.*FullClassName*.*ProcedureName*(*Argument1*, *Argument2*, ...)  
  
 For backward compatibility with earlier versions of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the following syntax is also acceptable:  
  
 *AssemblyName*!*FullClassName*!*ProcedureName*(*Argument1*, *Argument2*, ...)  
  
 If a COM library supports multiple interfaces, the interface ID can also be used to resolve the procedure name, as demonstrated here:  
  
 *AssemblyName*!*InterfaceID*!*ProcedureName*(*Argument1*, *Argument2*, ...)  
  
## Security  
 Security for assemblies is based on the .NET Framework security model, which is a code-access security model. .NET Framework supports a code-access security mechanism that assumes that the runtime can host both fully trusted and partially trusted code. The resources that are protected by .NET Framework code access security are typically wrapped by managed code which demands the corresponding permission before enabling access to the resource. The demand for the permission is satisfied only if all the callers (at the assembly level) in the call stack have the corresponding resource permission.  
  
 For assemblies, permission for execution is passed with the `PermissionSet` property on the `Assembly` object. The permissions that managed code receives are determined by the security policy in effect. There are already three levels of policy in effect in a non-[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] hosted environment: enterprise, computer and user. The effective list of permissions that code receives is determined by the intersection of the permissions obtained by these three levels.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supplies a host-level security policy level to the CLR while hosting it; this policy is an additional policy level below the three policy levels that are always in effect. This policy is set for every application domain that is created by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] host-level policy is a combination of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] fixed policy for system assemblies and user-specified policy for user assemblies. The user-specified piece of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] host policy is based on the assembly owner specifying one of three permission buckets for each assembly:  
  
|Permission Setting|Description|  
|------------------------|-----------------|  
|`Safe`|Provides internal computation permission. This permission bucket does not assign permissions to access any of the protected resources in the .NET Framework. This is the default permission bucket for an assembly if none is specified with the `PermissionSet` property.|  
|`ExternalAccess`|Provides the same access as the `Safe` setting, with the additional ability to access external system resources. This permission bucket does not offer security guarantees (although it is possible to secure this scenario), but it does give reliability guarantees.|  
|`Unsafe`|Provides no restrictions. No security or reliability guarantees can be made for managed code running under this permission set. Any permission, even a custom permission included by the administrator, is granted to code running at this level of trust.|  
  
 When CLR is hosted by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the stack-walk based permission check stops at the boundary with native [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] code. Any managed code in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] assemblies always falls into one of the three permission categories listed earlier.  
  
 COM (or unmanaged) assembly routines do not support the CLR security model.  
  
### Impersonation  
 Whenever managed code accesses any resource outside [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] follows the rules associated with the `ImpersonationMode` property setting of the assembly to make sure that the access occurs in an appropriate Windows security context. Because assemblies using the `Safe` permission setting cannot access resources outside [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], these rules are applicable only for assemblies using the `ExternalAccess` and `Unsafe` permission settings.  
  
-   If the current execution context corresponds to Windows Authenticated login and is the same as the context of the original caller (that is, there is no EXECUTE AS in the middle), [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will impersonate the Windows Authenticated login before accessing the resource.  
  
-   If there is an intermediate EXECUTE AS that changed the context from that of the original caller), the attempt to access external resource will fail.  
  
 The `ImpersonationMode` property can be set to `ImpersonateCurrentUser` or `ImpersonateAnonymous`. The default setting, `ImpersonateCurrentUser`, runs an assembly under the current user's network login account. If the `ImpersonateAnonymous` setting is used, the execution context is corresponds to the Windows login user account IUSER_*servername* on the server. This is the Internet guest account, which has limited privileges on the server. An assembly running in this context can only access limited resources on the local server.  
  
### Application Domains  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] does not expose application domains directly. Because of a set of assemblies running in the same application domain, application domains can discover each other at execution time by using the `System.Reflection` namespace in the .NET Framework or in some other way, and can call into them in late-bound manner. Such calls will be subject to the permission checks used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] authorization-based security.  
  
 You should not rely on finding assemblies in the same application domain, because the application domain boundary and the assemblies that go into each domain are defined by the implementation.  
  
## See Also  
 [Setting Security for Stored Procedures](../multidimensional-models-extending-olap-stored-procedures/setting-security-for-stored-procedures.md)   
 [Defining Stored Procedures](../multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md)  
  
  
