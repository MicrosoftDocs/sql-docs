---
title: "access check cache Server Configuration Options | Microsoft Docs"
description: "Learn about the access check result cache and the options that control the cache's behavior. See when to change these options in SQL Server."
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "access check cache option"
  - "access check cache bucket count"
  - "access check cache quota"
ms.assetid: 0a992ea8-3ec6-4a4d-97b5-460ae7326247
author: markingmyname
ms.author: maghan
---
# access check cache Server Configuration Options
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

When database objects are accessed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the access check is cached in an internal structure called the **access check result cache**. 
  
The **access check cache bucket count** option controls the number of hash buckets that are used for the access check result cache. 

The **access check cache quota** option controls the number of entries that are stored in the access check result cache. When the maximum number of entries is reached, the oldest entries are removed from the access check result cache.
  
The default values of 0 indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is managing these options. Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], the default values translate to the following internal configurations:
-   For access check cache bucket count, the value 0 sets a default value of 256 buckets.
-   For access check cache quota, the value 0 sets a default value of 1,024 entries.

In rare circumstances, performance can be improved by changing these options. For example, you may want to reduce the size of the access check result cache if too much memory is used. Or, you may want to increase the size of the access check result cache if you experience high CPU usage when permissions are recalculated.
 
> [!IMPORTANT]
> Microsoft recommends only changing these options when directed by Microsoft Customer Support Services. If you have to change the "access check cache bucket count" and "access check cache quota" values, use a ratio of 1:4. For example, if you change the "access check cache bucket count" value to 512, you should change the "access check cache quota" value to 2,048. 
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
