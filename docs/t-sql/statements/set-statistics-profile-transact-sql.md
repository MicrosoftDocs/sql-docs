---
title: "SET STATISTICS PROFILE (Transact-SQL)"
description: SET STATISTICS PROFILE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PROFILE"
  - "SET_STATISTICS_PROFILE_TSQL"
  - "PROFILE_TSQL"
  - "SET STATISTICS PROFILE"
helpviewer_keywords:
  - "profiles [SQL Server], displaying"
  - "statements [SQL Server], profile information"
  - "SET STATISTICS PROFILE statement"
  - "STATISTICS PROFILE option"
  - "statistical information [SQL Server], profiles"
dev_langs:
  - "TSQL"
---
# SET STATISTICS PROFILE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Displays the profile information for a statement. STATISTICS PROFILE works for ad hoc queries, views, and stored procedures.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
SET STATISTICS PROFILE { ON | OFF }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks
 When STATISTICS PROFILE is ON, each executed query returns its regular result set, followed by an additional result set that shows a profile of the query execution.  
  
 The additional result set contains the SHOWPLAN_ALL columns for the query and these additional columns.  
  
|Column name|Description|  
|-----------------|-----------------|  
|**Rows**|Actual number of rows produced by each operator|  
|**Executes**|Number of times the operator has been executed|  
  
## Permissions  
 To use SET STATISTICS PROFILE and view the output, users must have the following permissions:  
  
-   Appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
-   SHOWPLAN permission on all databases containing objects that are referenced by the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
 For [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that do not produce STATISTICS PROFILE result sets, only the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are required. For [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that do produce STATISTICS PROFILE result sets, checks for both the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement execution permission and the SHOWPLAN permission must succeed, or the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement execution is aborted and no Showplan information is generated.  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET SHOWPLAN_ALL &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-all-transact-sql.md)   
 [SET STATISTICS TIME &#40;Transact-SQL&#41;](../../t-sql/statements/set-statistics-time-transact-sql.md)   
 [SET STATISTICS IO &#40;Transact-SQL&#41;](../../t-sql/statements/set-statistics-io-transact-sql.md)  
  
  
