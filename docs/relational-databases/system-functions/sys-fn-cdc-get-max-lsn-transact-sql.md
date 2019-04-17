---
title: "sys.fn_cdc_get_max_lsn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.fn_cdc_get_max_lsn"
  - "fn_cdc_get_max_lsn"
  - "fn_cdc_get_max_lsn_TSQL"
  - "sys.fn_cdc_get_max_lsn_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_cdc_get_max_lsn"
  - "sys.fn_cdc_get_max_lsn"
ms.assetid: 93f3a4c8-b91f-4ebb-8e96-9397bb3a1c43
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.fn_cdc_get_max_lsn (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the maximum log sequence number (LSN) from the start_lsn column in the [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md) system table. You can use this function to return the high endpoint of the change data capture timeline for any capture instance.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_cdc_get_max_lsn ()  
```  
  
## Return Types  
 **binary(10)**  
  
## Remarks  
 This function returns the maximum LSN in the start_lsn column of the [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md) table. As such, it is the last LSN processed by the capture process when changes are propagated to the database change tables. It serves as the high endpoint for the all timelines that are associated with capture instances defined for the database.  
  
 The function is typically used to obtain an appropriate high endpoint for a query interval.  
  
## Permissions  
 Requires membership in the public database role.  
  
## Examples  
  
### A. Returning the maximum LSN value  
 The following example returns the maximum LSN for all capture instances in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT sys.fn_cdc_get_max_lsn()AS max_lsn;  
```  
  
### B. Setting the high endpoint of a query range  
 The following example uses the maximum LSN returned by `sys.fn_cdc_get_max_lsn` to set the high endpoint for a query range for the capture instance `HumanResources_Employee`.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @from_lsn binary(10), @to_lsn binary(10);  
SET @from_lsn = sys.fn_cdc_get_min_lsn(N'HumanResources_Employee');  
SET @to_lsn = sys.fn_cdc_get_max_lsn();  
SELECT * FROM cdc.fn_cdc_get_all_changes_HumanResources_Employee(@from_lsn, @to_lsn, 'all');  
GO  
```  
  
## See Also  
 [sys.fn_cdc_get_min_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-get-min-lsn-transact-sql.md)   
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)  
  
  
