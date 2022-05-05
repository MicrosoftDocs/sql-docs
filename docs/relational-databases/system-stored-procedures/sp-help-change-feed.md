---
description: "The sp_help_change_feed system stored procedure monitors the current Synapse Link configuration."
title: "sp_help_change_feed (Transact-SQL)"
ms.custom: ""
ms.date: "05/24/2022"
ms.service: synapse-analytics
ms.prod_service: "database-engine, sql-database, synapse-analytics"
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "sp_help_change_feed_TSQL"
  - "sp_help_change_feed"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_change_feed"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_help_change_feed (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Monitors the current configuration of the [Azure Synapse Link change feed](../../sql-server/synapse-link/synapse-link-sql-server-change-feed.md). For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

## Syntax  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
```syntaxsql  
EXECUTE sys.sp_help_change_feed;
```  

## Result set

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| **pollinterval** | int | An integer value that describes the frequency that the log is scanned for any new changes, in seconds. By default this value is 60. It can't currently be changed.| 
| **maxtrans** | int | Maximum number of transactions to process in each scan cycle. The default value is 500. |

## Remarks

## Permissions  

A user must have the VIEW PERFORMANCESTATE permission.

## See also  

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
