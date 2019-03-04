---
title: "sys.database_service_objectives (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "08/30/2016"
ms.service: sql-database
ms.prod_service: "sql-database, sql-data-warehouse"
ms.reviewer: ""
ms.topic: conceptual
keywords: 
  - "elastic pool"
  - "elastic pool, management"
f1_keywords: 
  - "DATABASE_SERVICE_OBJECTIVES_TSQL"
ms.assetid: cecd8c31-06c0-4aa7-85d3-ac590e6874fa
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.database_service_objectives (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md)]

Returns the edition (service tier), service objective (pricing tier) and elastic pool name, if any, for an Azure SQL database or an Azure SQL Data Warehouse. If logged on to the master database in an Azure SQL Database server, returns information on all databases. For Azure SQL Data Warehouse, you must be connected to the master database.  
  
  
 For information on pricing , see [SQL Database options and performance: SQL Database Pricing](https://azure.microsoft.com/pricing/details/sql-database/) and [SQL Data Warehouse Pricing](https://azure.microsoft.com/pricing/details/sql-data-warehouse/).  
  
 To change the service settings, see [ALTER DATABASE (Azure SQL Database)](../../t-sql/statements/alter-database-azure-sql-database.md) and [ALTER DATABASE (Azure SQL Data Warehouse)](../../t-sql/statements/alter-database-azure-sql-data-warehouse.md).  
  
 The sys.database_service_objectives view contains the following columns.  
  
|Column Name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|int|The ID of the database, unique within an instance of Azure SQL Database server. Joinable with [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).|  
|edition|sysname|The service tier for the database or data warehouse: **Basic**, **Standard**, **Premium** or **Data Warehouse**.|  
|service_objective|sysname|The pricing tier of the database. If the database is in an elastic pool, returns **ElasticPool**.<br /><br /> On the **Basic** tier, returns **Basic**.<br /><br /> **Single database in a standard service tier** returns one of the following: S0, S1, S2, S3, S4, S6, S7, S9 or S12.<br /><br /> **Single database in a premium tier** returns of the following: P1, P2, P4, P6, P11 or P15.<br /><br /> **SQL Data Warehouse** returns DW100 through DW10000c.|  
|elastic_pool_name|sysname|The name of the [elastic pool](https://azure.microsoft.com/documentation/articles/sql-database-elastic-pool/) that the database belongs to. Returns **NULL** if the database is a single database or a data warehoue.|  
  
## Permissions  
 Requires **dbManager** permission on the master database.  At the database level, the user must be the creator or owner.  
  
## Examples  
 This example can  be run on the master database or on user databases. The query returns the name, service, and performance tier information of the database(s).  
  
```sql  
SELECT  d.name,   
     slo.*    
FROM sys.databases d   
JOIN sys.database_service_objectives slo    
ON d.database_id = slo.database_id;  
  
```  
  
  
