---
description: "The sp_change_feed_enable_db system stored procedure creates a database change feed for Synapse Link for SQL Server."
title: "sp_change_feed_enable_db (Transact-SQL)"
ms.custom: ""
ms.date: "05/24/2022"
ms.service: synapse-analytics
ms.prod_service: "database-engine, sql-database, synapse-analytics"
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "sp_change_feed_enable_db_TSQL"
  - "sp_change_feed_enable_db"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_change_feed_enable_db"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_change_feed_enable_db (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Enable the change feed for a database and create internal metadata objects for [Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link). For more information, see [Manage Azure Synapse Link for SQL Server](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

## Syntax  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
```syntaxsql  
EXECUTE sys.sp_change_feed_enable_db  
      @maxtrans  
```  

## Arguments  

#### @maxtrans

The @maxtrans parameter specifies the maximum transactions to process in each cycle. The default value is 500. Must be a positive integer.
  
 
## Permissions  

 Only a member of the sysadmin server role or db_owner database role can execute this procedure. 

## See also  

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [sp_change_feed_disable_db (Transact-SQL)](sp-change-feed-disable-db.md)
- [sp_change_feed_create_table_group (Transact-SQL)](sp-change-feed-create-table-group.md)
- [sp_change_feed_drop_table_group (Transact-SQL)](sp-change-feed-drop-table-group.md)
- [sp_change_feed_disable_table (Transact-SQL)](sp-change-feed-disable-table.md)
- [sp_change_feed_enable_table (Transact-SQL)](sp-change-feed-enable-table.md)

## Next steps

- [Manage Azure Synapse Link for SQL Server](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Get started with Synapse Link for SQL Server 2022 (Preview)](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
