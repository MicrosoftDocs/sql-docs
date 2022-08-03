---
title: "sys.resource_usage"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.resource_usage (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/18/2022"
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "sys.resource_usage_TSQL"
  - "resource_usage"
  - "sys.resource_usage"
  - "resource_usage_TSQL"
helpviewer_keywords:
  - "resource_usage"
  - "sys.resource_usage"
dev_langs:
  - "TSQL"
ms.assetid: b90147a3-fd8e-408e-961d-5c7000e068ad
monikerRange: "=azuresqldb-current"
---
# sys.resource_usage (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

    
> [!IMPORTANT]
>  This feature is in a preview state. Do not take a dependency on the specific implementation of this feature because the feature might be changed or removed in a future release.  
> 
>  While in a preview state, the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] operations team might turn data collection off and on for this DMV:  
> 
>  -   When turned on, the DMV returns current data as it is aggregated.  
> -   When turned off, the DMV returns historical data, which might be stale.  
  
 Provides hourly summary of resource usage data for user databases in the current server. Historical data is retained for 90 days.  
  
 For each user database, there is one row for every hour in continuous fashion. Even if the database was idle during that hour, there is one row, and the usage_in_seconds value for that database will be 0. Storage usage and SKU information are rolled up for the hour appropriately.  
  
|Columns|Data Type|Description|  
|-------------|---------------|-----------------|  
|end_time|**datetime**|Time (UTC) in hour increments.|  
|database_name|**nvarchar**|Name of user database.|  
|sku|**nvarchar**|Name of the service tier. Possible values include: Basic, Standard, Premium, GeneralPurpose, BusinessCritical, Hyperscale. |  
|storage_in_megabytes|**decimal**|Maximum used storage size for the hour, including database data, indexes, stored procedures and metadata.|  
  
## Permissions  

Requires permission to access the **master** database on the [logical server](/azure/azure-sql/database/logical-servers) in Azure SQL Database.  

## Examples

The following query returns data from the past two days:

```sql
SELECT end_time, database_name, sku, storage_in_megabytes 
FROM sys.resource_usage
WHERE end_time > DATEADD(dd,-2,SYSDATETIME());
GO
```
  
## Next steps

Learn more about Azure SQL Database in the following articles:

- [sys.resource_stats (Azure SQL Database)](sys-resource-stats-azure-sql-database.md)
- [Azure SQL Database Catalog Views](azure-sql-database-catalog-views.md)
- [sys.event_log (Azure SQL Database)](sys-event-log-azure-sql-database.md)
- [Diagnose and troubleshoot high CPU on Azure SQL Database](/azure/azure-sql/database/high-cpu-diagnose-troubleshoot)
- [Understand and resolve Azure SQL Database blocking problems](/azure/azure-sql/database/understand-resolve-blocking)
