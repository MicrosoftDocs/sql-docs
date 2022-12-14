---
description: "The sp_change_feed_configure_parameters system stored procedure is used to reduce latency or reduce the cost by increasing the batch size with higher transactions"
title: "sp_change_feed_configure_parameters (Transact-SQL)"
ms.date: 11/09/2022
ms.service: synapse-analytics
ms.reviewer: wiassaf
ms.topic: "reference"
f1_keywords: 
  - "sp_change_feed_configure_parameters_TSQL"
  - "sp_change_feed_configure_parameters"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_change_feed_configure_parameters"
author: IdrisMotiwala
ms.author: imotiwala
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_change_feed_configure_parameters (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Used to reduce latency – by decreasing change batch size with maxtrans or to reduce the cost by increasing the batch size, as the batch size increases less IO operation will be performed between Azure SQL DB and the Landing Zone (LZ). For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

> [!NOTE]
> This stored procedure is used to fine tune the operational performance for [Azure Synapse Link for SQL](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)

## Syntax  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
```syntaxsql  
EXECUTE sys.sp_change_feed_configure_parameters
    @maxtrans = 1000,
    @pollinterval = 10
GO
```  

## Arguments  

#### @maxtrans

Data type is integer. Indicates the maximum number of transactions to process in each scan cycle. Default value if not specified is 500. If specified, the value must be a positive integer.

#### @pollinterval

Data type is integer. Describes the frequency that the log is scanned for any new changes, in seconds.  Default interval if not specified is 5 s, the value must be 5 or larger.

## Result set

Returns: 0 - Success, 1 - Failure

## Permissions  

  Currently, only a member of the sysadmin server role or db_owner role, or a user with CONTROL database permissions can execute this procedure. 

## See also  

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sp_change_feed_disable_db (Transact-SQL)](sp-change-feed-disable-db.md)
- [sp_change_feed_drop_table_group (Transact-SQL)](sp-change-feed-drop-table-group.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Get started with Azure Synapse Link for SQL Server 2022](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)