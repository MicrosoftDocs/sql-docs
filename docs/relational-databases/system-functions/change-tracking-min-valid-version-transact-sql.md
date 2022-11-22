---
description: "CHANGE_TRACKING_MIN_VALID_VERSION (Transact-SQL)"
title: "CHANGE_TRACKING_MIN_VALID_VERSION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/08/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "CHANGE_TRACKING_CLEANUP_VERSION"
  - "CHANGE_TRACKING_CLEANUP_VERSION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CHANGE_TRACKING_MIN_VALID_VERSION"
  - "change tracking [SQL Server], CHANGE_TRACKING_MIN_VALID_VERSION"
ms.assetid: 5a43d23f-adcf-4c0b-95ad-07cee03c1f9d
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CHANGE_TRACKING_MIN_VALID_VERSION (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the minimum version on the client that is valid for use in obtaining change tracking information from the specified table, when you're using the [CHANGETABLE](../../relational-databases/system-functions/changetable-transact-sql.md) function.  
    
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CHANGE_TRACKING_MIN_VALID_VERSION ( table_object_id )  
```  
  
## Arguments  
 *table_object_id*  
 Is the object ID of the table. *table_object_id* is an **int**.  
  
## Return Type  
 **bigint**  
  
## Remarks  
 Use this function to validate the value of the *last_sync_version* parameter for CHANGETABLE. If *last_sync_version* is less than the value that is reported by this function, the results that are returned from a later call to CHANGETABLE might not be valid.  
  
 CHANGE_TRACKING_MIN_VALID_VERSION uses the following information to determine the return value:  
  
-   When the table was enabled for change tracking.  
  
-   When the background cleanup task ran to remove change tracking information older than the retention period specified for the database.  
  
-   If the table was truncated, this removes all change tracking information that is associated with the table.  
  
 The function returns NULL if any one of the following conditions is true:  
  
-   Change tracking isn't enabled for the database.  
  
-   The specified table object ID isn't valid for the current database.  
  
-   Insufficient permission to the table specified by the object ID.  
  
## Examples  
 The following example determines whether a specified version is a valid version. The example obtains the minimum valid version of the `dbo.Employees` table, and then compares this to the value of the `@last_sync_version` variable. If the value of `@last_sync_version` is lower than the value of `@min_valid_version`, the list of changed rows won't be valid.  
  
> [!NOTE]  
>  You would usually obtain the value from a table or other location where you stored the last version number that was used to synchronize data.  
  
```  
-- The tracked change is tagged with the specified context   
DECLARE @min_valid_version bigint, @last_sync_version bigint;  
  
SET @min_valid_version =   
CHANGE_TRACKING_MIN_VALID_VERSION(OBJECT_ID('dbo.Employees'));  
  
SET @last_sync_version = 11  
IF (@last_sync_version < @min_valid_version)  
-- Error � do not obtain changes  
ELSE  
-- Obtain changes using CHANGETABLE(CHANGES ...)  
```  
  
## See also  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)   
 [sys.change_tracking_tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/change-tracking-catalog-views-sys-change-tracking-tables.md)  
  
  
