---
description: "The sp_change_feed_disable_db system stored procedure disables the Azure Synapse Link for SQL change feed at the database level."
title: "sp_change_feed_disable_db (Transact-SQL)"
ms.custom:
- event-tier1-build-2022
ms.date: 11/09/2022
ms.service: synapse-analytics
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "sp_change_feed_disable_db_TSQL"
  - "sp_change_feed_disable_db"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_change_feed_disable_db"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_change_feed_disable_db (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Disable the change feed at the database level, and subsequently the metadata for all the associated tables for [Azure Synapse Link for SQL](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview). For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

> [!NOTE]
> This stored procedure is used internally and is not recommended for direct administrative use. Use Synapse Studio instead. Using this procedure will introduce inconsistency with Synapse Workspace configuration and keep Azure Synapse resources allocated.

## Syntax  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
```syntaxsql  
EXECUTE sys.sp_change_feed_disable_db;
```  
  
## Permissions  

 Currently, only a member of the sysadmin server role or db_owner role, or a user with CONTROL database permissions can execute this procedure. 

## Remarks

When the change feed is disabled with active table groups, all connections and schedulers will be stopped immediately/forcefully without waiting for the current operations are completed. No new change feed table groups can be created for the database, and all the existing metadata describing the table groups will be deleted without waiting for the current operations to complete. Re-enabling change feed will result in clean initializations of all table groups and reseeding of all the data. 

## See also  

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sp_change_feed_drop_table_group (Transact-SQL)](sp-change-feed-drop-table-group.md)
- [sp_change_feed_disable_table (Transact-SQL)](sp-change-feed-disable-table.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Get started with Synapse Link for SQL Server 2022](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
