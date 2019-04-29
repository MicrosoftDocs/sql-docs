---
title: "SET STATISTICS PROFILE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "PROFILE"
  - "SET_STATISTICS_PROFILE_TSQL"
  - "PROFILE_TSQL"
  - "SET STATISTICS PROFILE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "profiles [SQL Server], displaying"
  - "statements [SQL Server], profile information"
  - "SET STATISTICS PROFILE statement"
  - "STATISTICS PROFILE option"
  - "statistical information [SQL Server], profiles"
ms.assetid: c635e262-35fa-421a-aa6f-a1c30f351647
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SET STATISTICS PROFILE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Displays the profile information for a statement. STATISTICS PROFILE works for ad hoc queries, views, and stored procedures.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SET STATISTICS PROFILE { ON | OFF }  
```  
  
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
  
  
