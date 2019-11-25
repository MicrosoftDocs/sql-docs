---
title: "Impersonation and CLR Integration Security | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "database-engine"
ms.topic: "reference"
helpviewer_keywords: 
  - "impersonation [CLR integration]"
  - "security [CLR integration]"
  - "execution context [CLR integration]"
  - "user impersonation [CLR integration]"
  - "context [CLR integration]"
ms.assetid: 1495a7af-2248-4cee-afdb-9269fb3a7774
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Impersonation and CLR Integration Security
  When managed code accesses external resources, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not automatically impersonate the current execution context under which the routine is executing. Code in `EXTERNAL_ACCESS` and `UNSAFE` assemblies can explicitly impersonate the current execution context.  
  
> [!NOTE]  
>  For information on behavior changes in impersonation, see [Breaking Changes to Database Engine Features in SQL Server 2014](../breaking-changes-to-database-engine-features-in-sql-server-2016.md).  
  
 The in-process data access provider provides an application programming interface, `SqlContext.WindowsIdentity`, that can be used to retrieve the token associated with the current security context. Managed code in `EXTERNAL_ACCESS` and `UNSAFE` assemblies can use this method to retrieve the context and use the .NET Framework `WindowsIdentity.Impersonate` method to impersonate that context. The following restrictions apply when user code explicitly impersonates:  
  
-   In-process data access is not allowed when managed code is in an impersonated state. Code can undo impersonation and then call in-process data access. Note that this requires storing the return value (a `WindowsImpersonationContext` object) of the original `Impersonate` method, and calling the `Undo` method on this `WindowsImpersonationContext`.  
  
     This restriction means that when in-process data access occurs, it is always in the context of the current security context in effect for the session. It cannot be modified by explicit impersonation within managed code.  
  
-   For managed code executing asynchronously (for example, through `UNSAFE` assemblies creating threads and running code asynchronously), in-process data access is never allowed. This is true whether or not there is impersonation.  
  
 When code is running in an impersonated context that is different from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it cannot perform in-process data access calls; it should undo the impersonation context before making in-process data access calls. When in-process data access is made from managed code, the original execution context of the [!INCLUDE[tsql](../../includes/tsql-md.md)] entry point into the managed code is always used for authorization.  
  
 Both `EXTERNAL_ACCESS` assemblies and `UNSAFE` assemblies access operating system resources with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account, unless they voluntarily impersonate the current security context as previously described. Because of this, the authors of `EXTERNAL_ACCESS` assemblies require a higher level of trust than those of `SAFE` assemblies, which is specified by the `EXTERNAL ACCESS` login-level permission. Only logins who are trusted to run code under the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account should be granted the `EXTERNAL ACCESS` permission.  
  
## See Also  
 [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md)   
 [Impersonation and Credentials for Connections](../../relational-databases/clr-integration/data-access/impersonation-and-credentials-for-connections.md)  
  
  
