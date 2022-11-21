---
description: "The sp_change_feed_enable_table system stored procedure enables the creation of a new Azure Synapse Link table to an existing Azure Synapse Link table group."
title: "sp_change_feed_enable_table (Transact-SQL)"
ms.date: 11/09/2022
ms.service: synapse-analytics
ms.reviewer: wiassaf
ms.topic: "reference"
f1_keywords: 
  - "sp_change_feed_enable_table_TSQL"
  - "sp_change_feed_enable_table_db"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_change_feed_enable_table"
author: IdrisMotiwala
ms.author: imotiwala
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_change_feed_enable_table (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Stored procedure to enable the creation of a new [Azure Synapse Link for SQL](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview) table to an existing Azure Synapse Link table group. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

> [!NOTE]
> This stored procedure is used internally and is not recommended for direct administrative use. Use Synapse Studio instead. Using this procedure will introduce inconsistency with Synapse Workspace configuration.

## Syntax  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
```syntaxsql  
EXECUTE sys.sp_change_feed_enable_table
    @table_group_id,
    @table_id,
    @source_schema,
    @source_name
GO
```  

## Arguments

#### @table_group_id

The unique identifier of the table group.

#### @table_id

The unique identifier for the change feed table generated during setup workflow.

#### @source_schema

The source table schema name.

#### @source_name

The source table name.

## Permissions  

 Currently, only a member of the sysadmin server role or db_owner role, or a user with CONTROL database permissions can execute this procedure.

## See also  

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sp_change_feed_enable_db (Transact-SQL)](sp-change-feed-enable-db.md)
- [sp_change_feed_disable_db (Transact-SQL)](sp-change-feed-disable-db.md)
- [sp_change_feed_drop_table_group (Transact-SQL)](sp-change-feed-drop-table-group.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Get started with Azure Synapse Link for SQL Server 2022](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)