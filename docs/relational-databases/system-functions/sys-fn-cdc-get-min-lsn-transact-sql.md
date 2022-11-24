---
description: "sys.fn_cdc_get_min_lsn (Transact-SQL)"
title: "sys.fn_cdc_get_min_lsn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.fn_cdc_get_min_lsn"
  - "fn_cdc_get_min_lsn"
  - "fn_cdc_get_min_lsn_TSQL"
  - "sys.fn_cdc_get_min_lsn_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_cdc_get_min_lsn"
  - "sys.fn_cdc_get_min_lsn"
ms.assetid: bd49e28a-128b-4f6b-8545-6a2ec3f4afb3
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_cdc_get_min_lsn (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the start_lsn column value for the specified capture instance from the [cdc.change_tables](../../relational-databases/system-tables/cdc-change-tables-transact-sql.md) system table. This value represents the low endpoint of the validity interval for the capture instance.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_cdc_get_min_lsn ( 'capture_instance_name' )  
```  
  
## Arguments  
 **'** *capture_instance_name* **'**  
 Is the name of the capture instance. *capture_instance_name* is **sysname**.  
  
## Return Types  
 **binary(10)**  
  
## Remarks  
 Returns 0x00000000000000000000 when the capture instance does not exist or when the caller is not authorized to access the change data associated with the capture instance.  
  
 This function is typically used to identify the low endpoint of the change data capture timeline associated with a capture instance. You can also use this function to validate that the endpoints of a query range fall within the capture instance timeline before requesting change data. It is important to perform such checks because the low endpoint of a capture instance changes when cleanup is performed on the change tables. If the time between requests for change data is significant, even a low endpoint that is set to the high endpoint of the previous change data request might lie outside the current timeline.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role or db_owner fixed database role. For all other users, requires SELECT permission on all captured columns in the source table and, if a gating role for the capture instance was defined, membership in that database role.  
  
## Examples  
  
### A. Returning the minimum LSN value for a specified capture instance  
 The following example returns the minimum LSN value for the capture instance `HumanResources_Employee` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
USE AdventureWorks2-12;  
GO  
SELECT sys.fn_cdc_get_min_lsn ('HumanResources_Employee')AS min_lsn;  
  
```  
  
### B. Verifying the low endpoint of a query range  
 The following example uses the minimum LSN value returned by `sys.fn_cdc_get_min_lsn` to verify that the proposed low endpoint for a change data query is valid for the current timeline for the capture instance `HumanResources_Employee`. This example assumes that the previous high endpoint LSN for the capture instance was saved and is available to set the `@save_to_lsn` variable. For the purposes of this example, `@save_to_lsn` is set to 0x000000000000000000 to force the error-handling section to run.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @min_lsn binary(10), @from_lsn binary(10),@save_to_lsn binary(10), @to_lsn binary(10);  
-- Sets @save_to_lsn to the previous high endpoint saved from the last change data request.  
SET @save_to_lsn = 0x000000000000000000;  
-- Sets the upper endpoint for the query range to the current maximum LSN.  
SET @to_lsn = sys.fn_cdc_get_max_lsn();  
-- Sets the @min_lsn parameter to the current minimum LSN for the capture instance.  
SET @min_lsn = sys.fn_cdc_get_min_lsn ('HumanResources_Employee');  
-- Sets the low endpoint for the query range to the LSN that follows the previous high endpoint.  
SET @from_lsn = sys.fn_cdc_increment_lsn(@save_to_lsn);  
-- Tests to verify the low endpoint is valid for the current capture instance.  
IF (@from_lsn < @min_lsn)  
    BEGIN  
        RAISERROR('Low endpoint of the request interval is invalid.', 16, -1);  
    END  
ELSE  
-- Return the changes occurring within the query range.  
    SELECT * FROM cdc.fn_cdc_get_all_changes_HumanResources_Employee(@from_lsn, @to_lsn, 'all');  
GO  
```  
  
## See Also  
 [sys.fn_cdc_get_max_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-get-max-lsn-transact-sql.md)   
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)  
  
  
