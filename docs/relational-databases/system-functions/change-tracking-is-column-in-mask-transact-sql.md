---
title: "CHANGE_TRACKING_IS_COLUMN_IN_MASK (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/08/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "CHANGE_TRACKING_IS_COLUMN_IN_MASK_TSQL"
  - "CHANGE_TRACKING_IS_COLUMN_IN_MASK"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "change tracking [SQL Server], CHANGE_TRACKING_IS_COLUMN_IN_MASK"
  - "CHANGE_TRACKING_IS_COLUMN_IN_MASK"
ms.assetid: 649b370b-da54-4915-919d-1b597a39d505
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CHANGE_TRACKING_IS_COLUMN_IN_MASK (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Interprets the SYS_CHANGE_COLUMNS value that is returned by the CHANGETABLE(CHANGES ...) function. This enables an application to determine whether the specified column is included in the values that are returned for SYS_CHANGE_COLUMNS.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CHANGE_TRACKING_IS_COLUMN_IN_MASK ( column_id , change_columns )  
```  
  
## Arguments  
 *column_id*  
 Is the ID of the column that is being checked. The column ID can be obtained by using the [COLUMNPROPERTY](../../t-sql/functions/columnproperty-transact-sql.md) function.  
  
 *change_columns*  
 Is the binary data from the SYS_CHANGE_COLUMNS column of the [CHANGETABLE](../../relational-databases/system-functions/changetable-transact-sql.md) data.  
  
## Return Type  
 **bit**  
  
## Return Values  
 CHANGE_TRACKING_IS_COLUMN_IN_MASK returns the following values.  
  
|Return value|Description|  
|------------------|-----------------|  
|0|The specified column is not in the *change_columns* list.|  
|1|The specified column is in the *change_columns* list.|  
  
## Remarks  
 CHANGE_TRACKING_IS_COLUMN_IN_MASK does not perform any checks to validate the *column_id* value or that the *change_columns* parameter was obtained from the table from which the *column_id* was obtained.  
  
## Examples  
 The following example determines whether the `Salary` column of the `Employees` table was updated. The `COLUMNPROPERTY` function returns the column ID of the `Salary` column. The `@change_columns` local variable must be set to the results of a query by using CHANGETABLE as a data source.  
  
```sql  
SET @SalaryChanged = CHANGE_TRACKING_IS_COLUMN_IN_MASK  
    (COLUMNPROPERTY(OBJECT_ID('Employees'), 'Salary', 'ColumnId')  
    ,@change_columns);  
```  
  
## See Also  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)   
 [CHANGETABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/changetable-transact-sql.md)   
 [Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)  
  
  
