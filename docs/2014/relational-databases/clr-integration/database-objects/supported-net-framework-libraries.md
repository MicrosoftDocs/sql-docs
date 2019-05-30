---
title: "Supported .NET Framework Libraries | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "common language runtime [SQL Server], .NET Framework libraries"
  - ".NET Framework [CLR Integration]"
ms.assetid: 417544ff-c25c-496e-add4-2f278f8a4911
author: rothja
ms.author: jroth
manager: craigg
---
# Supported .NET Framework Libraries
  With the common language runtime (CLR) hosted in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you can author stored procedures, triggers, user-defined functions, user-defined types, and user-defined aggregates in managed code. With the functionality found in the .NET Framework class libraries, you have access to pre-built classes that provide functionality for string manipulation, advanced math operations, file access, cryptography, and more. These classes can be accessed from any managed stored procedure, user-defined type, trigger, user-defined function, or user-defined aggregate.  
  
> [!NOTE]  
>  If you service or upgrade unsupported assemblies in the global assembly cache (GAC), your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If an assembly exists both in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] CLR integration. If you service or upgrade any assemblies in the GAC that are also registered in the database, including unsupported .NET Framework assemblies, make sure to also service or upgrade the copy of the assembly inside your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases with the `ALTER ASSEMBLY` statement. For more information, see [Knowledge Base article 949080](https://support.microsoft.com/kb/949080).  
  
## Supported Libraries  
 Beginning with [!INCLUDE[ssVersion2005](../../../includes/ssnoversion-md.md)] has a list of supported .NET Framework libraries, which have been tested to ensure that they meet reliability and security standards for interaction with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] loads them directly from the Global Assembly Cache (GAC).  
  
 The libraries/namespaces supported by CLR integration in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] are:  
  
-   CustomMarshalers  
  
-   Microsoft.VisualBasic  
  
-   Microsoft.VisualC  
  
-   mscorlib  
  
-   System  
  
-   System.Configuration  
  
-   System.Data  
  
-   System.Data.OracleClient  
  
-   System.Data.SqlXml  
  
-   System.Deployment  
  
-   System.Security  
  
-   System.Transactions  
  
-   System.Web.Services  
  
-   System.Xml  
  
-   System.Core.dll  
  
-   System.Xml.Linq.dll  
  
## Unsupported Libraries  
 Unsupported libraries can still be called from your managed stored procedures, triggers, user-defined functions, user-defined types, and user-defined aggregates. The unsupported library must first be registered in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, using the `CREATE ASSEMBLY` statement, before it can be used in your code. Any unsupported library that is registered and run on the server should be reviewed and tested for security and reliability.  
  
 For example, the `System.DirectoryServices` namespace is not supported. You must register the System.DirectoryServices.dll assembly with `UNSAFE` permissions before you can call it from your code. The `UNSAFE` permission is necessary because classes in the `System.DirectoryServices` namespace do not meet the requirements for `SAFE` or `EXTERNAL_ACCESS`. For more information, see [CLR Integration Programming Model Restrictions](clr-integration-programming-model-restrictions.md) and [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md).  
  
## See Also  
 [Creating an Assembly](../assemblies/creating-an-assembly.md)   
 [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md)   
 [CLR Integration Programming Model Restrictions](clr-integration-programming-model-restrictions.md)  
  
  
