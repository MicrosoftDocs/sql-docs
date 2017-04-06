---
title: "sys.dm_db_tuning_recommendations (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_tuning_recommendations"
  - "dm_db_tuning_recommendations"
  - "sys.dm_db_tuning_recommendations_TSQL"
  - "dm_db_tuning_recommendations_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database tuning recommendations feature [SQL Server], sys.dm_db_tuning_recommendations dynamic management view"
  - "sys.dm_db_tuning_recommendations dynamic management view"
ms.assetid: ced484ae-7c17-4613-a3f9-6d8aba65a110
caps.latest.revision: 37
author: "jovanpop-msft"
ms.author: "jovanpop-msft"
manager: "jhubbard"
---
# sys.dm\_db\_tuning\_recommendations (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns detailed information about tuning recomendations.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn’t belong to the connected tenant is filtered out.
  
## Remarks  
 Information returned by **sys.dm\_db\_tuning\_recommendations** is updated when database engine identifies potential query performance regression, and is not persisted. Recommendations are kept only until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. Database administrators should periodically make backup copies of the tuning recommendation if they want to keep it after server recycling.  
  
## Using tuning recommendations information  
 To convert the recommendation returned by **sys.dm\_db\_tuning\_recommendations** into an information that can be used to see the impact of recommendation and to get the T-SQL script that will fix the issue, you can use the following query:  
 
```
SELECT name, reason, score,
		JSON_VALUE(details, '$.implementationDetails.script') as script,
		details.* 
FROM sys.dm_db_tuning_recommendations
	CROSS APPLY OPENJSON(details, '$.planForceDetails')
				WITH (	query_id int '$.queryId',
						regressed_plan_id int '$.regressedPlanId',
						last_good_plan_id int '$.forcedPlanId') as details
WHERE JSON_VALUE(state, '$.currentValue') = 'Active'
```
  
 For more information about memory-optimized indexes, see [Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md).
  
## Permissions  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Premium Tiers, requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Standard and Basic Tiers, requires the  **Server admin** or an **Azure Active Directory admin** account.  
  
## See Also  
 [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md)
 [sys.database_automatic_tuning_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-automatic-tuning-options-transact-sql.md)
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)
 
