---
title: "SET QUERY_GOVERNOR_COST_LIMIT (Transact-SQL)"
description: SET QUERY_GOVERNOR_COST_LIMIT (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET QUERY_GOVERNOR_COST_LIMIT"
  - "SET_QUERY_GOVERNOR_COST_LIMIT_TSQL"
  - "QUERY_GOVERNOR_COST_LIMIT"
  - "QUERY_GOVERNOR_COST_LIMIT_TSQL"
helpviewer_keywords:
  - "SET QUERY_GOVERNOR_COST_LIMIT statement"
  - "connections [SQL Server], overriding"
  - "QUERY_GOVERNOR_COST_LIMIT option"
  - "overriding connection values"
dev_langs:
  - "TSQL"
---
# SET QUERY_GOVERNOR_COST_LIMIT (Transact-SQL)
[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Overrides the currently configured **query governor cost limit** value for the current connection.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
SET QUERY_GOVERNOR_COST_LIMIT value  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### value
  
 Is a numeric or integer value specifying the highest estimated cost allowed for a given query to run. Values are rounded down to the nearest integer. Negative values are rounded up to 0. The query governor disallows execution of any query that has an estimated cost exceeding that value. Specifying 0 (the default) for this option turns off the query governor, and all queries of any cost are allowed to execute.  
  
 Query cost is an abstract figure determined by the query optimizer based on estimated execution requirements such as cpu time, memory, and disk IO and refers to the estimated elapsed time, in seconds, that would be required to complete a query on a specific hardware configuration. This abstract figure does not equate to the time required to complete a query on the running instance, and should instead be treated as a relative measure.
  
## Remarks  

 Using SET QUERY_GOVERNOR_COST_LIMIT applies to the current connection only and lasts the duration of the current connection. Use the [Configure the query governor cost limit Server Configuration Option](../../database-engine/configure-windows/configure-the-query-governor-cost-limit-server-configuration-option.md)option of **sp_configure** to change the server-wide query governor cost limit value. For more information about configuring this option, see [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) and [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
 The setting of SET QUERY_GOVERNOR_COST_LIMIT is set at execute or run time and not at parse time.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
