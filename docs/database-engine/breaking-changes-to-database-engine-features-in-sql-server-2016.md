---
title: "Breaking Changes to Database Engine Features in SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "11/27/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine [SQL Server], what's new"
  - "breaking changes [SQL Server]"
ms.assetid: 47edefbd-a09b-4087-937a-453cd5c6e061
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Breaking Changes to Database Engine Features in SQL Server 2016
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  This topic describes breaking changes in the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] and earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade.  
  
##  <a name="SQL15"></a> Breaking Changes in [!INCLUDE[ssSQL15](../includes/sssql15-md.md)]  
  
-   The sample_ms column of sys.dm_io_virtual_file_stats has expanded from an **int** to a **bigint** data type.  
  
-   The TimeStamp column of sys.fn_virtualfilestats has expanded from an **int** to a **bigint** data type.  

-   Using the MD2, MD4, MD5, SHA, or SHA1 hash algorithms (not recommended) requires setting the database compatibility level to earlier than 130.  

-   Under database compatibility level 130, implicit conversions from **datetime** to **datetime2** data types show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values. Use explicit casting to datetime2 datatype whenever a mixed comparison scenario between datetime and datetime2 datatypes exists. For more information, refer to this [Microsoft Support Article](https://support.microsoft.com/help/4010261).

-   Under database compatibility level 130, operations that perform implicit conversions between certain numeric and datetime data types show improved accuracy and can result in different converted values. This includes usage of functions that require calculations such as for example, DATEDIFF and ROUND. For more information, refer to this [Microsoft Support Article](https://support.microsoft.com/help/4010261).

## <a name="previous-versions"></a> Previous Versions  

For information about breaking changes in SQL Server version 2014, and in some earlier versions, see [Breaking Changes to Database Engine Features in SQL Server 2014](https://docs.microsoft.com/sql/database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016?view=sql-server-2014#SQL14).

#### Archived Documentation for Very Old Versions of SQL Server

[!INCLUDE[Archived documentation for very old versions of SQL Server](../includes/paragraph-content/previous-versions-archive-documentation-sql-server.md)]

## See Also  
 [Deprecated Database Engine Features in SQL Server 2016](../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)   
 [Discontinued Database Engine Functionality in SQL Server 2016](../database-engine/discontinued-database-engine-functionality-in-sql-server-2016.md)   
 [SQL Server Database Engine Backward Compatibility](../database-engine/sql-server-database-engine-backward-compatibility.md)   
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md)   
 [SQL Server 2016 or SQL Server 2017 on Windows improvements in handling some data types and uncommon operations](https://support.microsoft.com/help/4010261)
  
  
