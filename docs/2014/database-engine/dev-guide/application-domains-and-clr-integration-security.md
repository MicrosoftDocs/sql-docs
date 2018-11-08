---
title: "Application Domains and CLR Integration Security | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "security [CLR integration]"
  - "application domains [CLR integration]"
ms.assetid: 54ee904e-e21a-4ee7-b4ad-a6f6f71bd473
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Application Domains and CLR Integration Security
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] loads assemblies belonging to the same owner in the same application domain. By virtue of a set of assemblies running in the same application domain, assemblies are able to discover each other at execution time using the.NET Framework reflection application programming interfaces or other means, and can call into them in late-bound fashion. Because such calls are occurring against assemblies belonging to the same owner, there are no [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] permissions checked for these calls. The placement scheme of assemblies in application domains is designed primarily to achieve scalability, security, and isolation goals, and can potentially change in future releases. Hence, you should not rely on finding assemblies in the same application domain through late-bound mechanisms.  
  
## See Also  
 [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md)  
  
  
