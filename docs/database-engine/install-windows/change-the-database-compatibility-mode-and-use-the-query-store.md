---
title: "Change the Database Compatibility Level and Use the Query Store | Microsoft Docs"
ms.custom: ""
ms.date: "07/21/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "query plans [SQL Server], migrating"
  - "upgrading SQL Server, migrating query plans"
  - "plan guides [SQL Server], migrating query plans"
ms.assetid: 7e02a137-6867-4f6a-a45a-2b02674f7e65
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Change the Database Compatibility Level and use the Query Store

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], some changes are only enabled once the [database compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) has been changed. This was done for several reasons:  
  
- Since upgrade is a one-way operation (it is not possible to downgrade the file format), there is value in separating the enablement of new features to a separate operation within the database. It is possible to revert a setting to a prior database compatibility level.  The new model reduces the number of things that must happen during an outage window.  
  
- Changes to the query processor can have complex effects. Even though a "good" change to the system may be great for most workloads - it may cause an unacceptable regression on an important query for others. Separating this logic from the upgrade process, allows for features such as the Query Store, to mitigate plan choice regressions quickly or even avoid them completely in production servers.  
  
> [!IMPORTANT]  
> The below behaviors are expected for [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] when a database is attached or restored, and after an in-place upgrade:
> - If the compatibility level of a user database was 100 or higher before the upgrade, it remains the same after upgrade.    
> - If the compatibility level of a user database was 90 before upgrade, in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].    
> - The compatibility levels of the tempdb, model, msdb and Resource databases are set to the current compatibility level after upgrade.   
> - The master system database retains the compatibility level it had before upgrade.    
  
The upgrade process to enable new query processor functionality is related to the post-release servicing model of the product.  Some of those fixes are released under [trace flag 4199](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#4199).  Customers needing fixes can opt-in to those fixes without causing unexpected regressions for other customers. The post-release servicing model for query processor hotfixes is documented [here](https://support.microsoft.com/kb/974006). Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], moving to a new compatibility level implies that trace flag 4199 is no longer needed, because those fixes are now enabled by default in the latest compatibility level. Therefore, as part of the upgrade process, it is important to validate that 4199 is not enabled once the upgrade process completes.  

> [!NOTE]
> However, trace flag 4199 is still needed to enable any new query processor fixes released after RTM, if applicable.
  
The recommended workflow for upgrading the query processor to the latest version of the code is documented in the [Keep performance stability during the upgrade to newer SQL Server section of Query Store Usage Scenarios](../../relational-databases/performance/query-store-usage-scenarios.md#CEUpgrade), as seen below.  
  
![query-store-usage-5](../../relational-databases/performance/media/query-store-usage-5.png "query-store-usage-5") 
 
## See Also  
[View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)     
[Query Store Usage Scenarios](../../relational-databases/performance/query-store-usage-scenarios.md)     
[ALTER DATABASE &#40;Transact-SQL&#41; Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
    
  
