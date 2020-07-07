---
title: "access check cache Server Configuration Options | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "access check cache option"
  - "access check cache bucket count"
  - "access check cache quota"
ms.assetid: 0a992ea8-3ec6-4a4d-97b5-460ae7326247
author: MikeRayMSFT
ms.author: mikeray
---
# access check cache Server Configuration Options
When database objects are accessed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the access check is cached in an internal structure called the **access check result cache**. 
  
The **access check cache bucket count** option controls the number of hash buckets that are used for the access check result cache. 

The **access check cache quota** option controls the number of entries that are stored in the access check result cache. When the maximum number of entries is reached, the oldest entries are removed from the access check result cache.
  
The default values of 0 indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is managing these options. From [!INCLUDE[ssKatmai](../../includes/ssKatmai-md.md)] through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], the default values translate to the following internal configurations:
-   For access check cache bucket count, the value 0 sets a default value of 256 buckets for x86 architecture, and 2,048 buckets for x64 and IA-64 architectures.
-   For access check cache quota, the value 0 sets a default value of 1,024 entries for x86 architecture, and 28,192,048 buckets for x64 and IA-64 architectures.

In rare circumstances, performance can be improved by changing these options. For example, you may want to reduce the size of the access check result cache if too much memory is used. Or, you may want to increase the size of the access check result cache if you experience high CPU usage when permissions are recalculated.
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
