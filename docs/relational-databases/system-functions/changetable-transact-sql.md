---
description: "CHANGETABLE (Transact-SQL)"
title: "CHANGETABLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "CHANGETABLE_TSQL"
  - "CHANGETABLE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CHANGETABLE"
  - "change tracking [SQL Server], CHANGETABLE"
ms.assetid: d405fb8d-3b02-4327-8d45-f643df7f501a
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CHANGETABLE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns change tracking information for a table. You can use this statement to return all changes for a table or change tracking information for a specific row.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CHANGETABLE (  
    { CHANGES <table_name> , <last_sync_version> 
    | VERSION <table_name> , <primary_key_values> } 
    , [ FORCESEEK ] 
    )  
[AS] <table_alias> [ ( <column_alias> [ ,...n ] )  
  
<primary_key_values> ::=  
( <column_name> [ , ...n ] ) , ( <value> [ , ...n ] )  
```  
  
## Arguments  
 CHANGES *table_name* , *last_sync_version*  
 Returns tracking information for all changes to a table that have occurred since the version that is specified by *last_sync_version*.  
  
 *table_name*  
 Is the user-defined table on which to obtain tracked changes. Change tracking must be enabled on the table. A one-, two-, three-, or four-part table name can be used. The table name can be a synonym to the table.  
  
 *last_sync_version*  
 A nullable **bigint** scalar value. An [expression](../../t-sql/language-elements/expressions-transact-sql.md) will cause a syntax error. If the value is NULL, all tracked changes are returned.
 When it obtains changes, the calling application must specify the point from which changes are required. The *last_sync_version* specifies that point. The function returns information for all rows that have been changed since that version. The application is querying to receive changes with a version greater than *last_sync_version*. 
 Typically, before it obtains changes, the application will call `CHANGE_TRACKING_CURRENT_VERSION()` to obtain the version that will be used the next time changes are required. Therefore, the application does not have to interpret or understand the actual value. Because *last_sync_version* is obtained by the calling application, the application has to persist the value. If the application loses this value then it will need to re-initialize data. 
 *last_sync_version* should be validated to ensure that it is not too old, because some or all the change information might have been cleaned up according to the retention period configured for the database. For more information, see [CHANGE_TRACKING_MIN_VALID_VERSION &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-min-valid-version-transact-sql.md) and [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
 VERSION *table_name*, { *primary_key_values* }  
 Returns the latest change tracking information for a specified row. Primary key values must identify the row. *primary_key_values* identifies the primary key columns and specifies the values. The primary key column names can be specified in any order.  
  
 *table_name*  
 Is the user-defined table on which to obtain change tracking information. Change tracking must be enabled on the table. A one-, two-, three-, or four-part table name can be used. The table name can be a synonym to the table.  
  
 *column_name*  
 Specifies the name of primary key column or columns. Multiple column names can be specified in any order.  
  
 *value*  
 Is the value of the primary key. If there are multiple primary key columns, the values must be specified in the same order as the columns appear in the *column_name* list.  

 [ FORCESEEK ]   
 **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2 CU16, [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] CU24, and [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] CU11), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]    
 
 Optional parameter that forces a seek operation to be used to access the *table_name*. In some cases where very few rows have changed, a scan operation may still be used to access the *table_name*. If a scan operation this introduces a performance issue, use the `FORCESEEK` parameter.

 [AS] *table_alias* [ (*column_alias* [ ,...*n* ] ) ]  
 Provides names for the results that are returned by CHANGETABLE.  
  
 *table_alias*  
 Is the alias name of the table that is returned by CHANGETABLE. *table_alias* is required and must be a valid [identifier](../../relational-databases/databases/database-identifiers.md).  
  
 *column_alias*  
 Is an optional column alias or list of column aliases for the columns that are returned by CHANGETABLE. This enables column names to be customized in case there are duplicate names in the results.  

## Return Types  
 **table**  
  
## Return Values  
  
### CHANGETABLE CHANGES  
 When CHANGES is specified, zero or more rows that have the following columns are returned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|SYS_CHANGE_VERSION|**bigint**|Version value that is associated with the last change to the row|  
|SYS_CHANGE_CREATION_VERSION|**bigint**|Version values that are associated with the last insert operation.|  
|SYS_CHANGE_OPERATION|**nchar(1)**|Specifies the type of change:<br /><br /> **U** = Update<br /><br /> **I** = Insert<br /><br /> **D** = Delete|  
|SYS_CHANGE_COLUMNS|**varbinary(4100)**|Lists the columns that have changed since the last_sync_version (the baseline). Note that computed columns are never listed as changed.<br /><br /> The value is NULL when any one of the following conditions is true:<br /><br /> Column change tracking is not enabled.<br /><br /> The operation is an insert or delete operation.<br /><br /> All nonprimary key columns were updated in one operation. This binary value should not be interpreted directly. Instead, to interpret it, use [CHANGE_TRACKING_IS_COLUMN_IN_MASK()](../../relational-databases/system-functions/change-tracking-is-column-in-mask-transact-sql.md).|  
|SYS_CHANGE_CONTEXT|**varbinary(128)**|Change context information that you can optionally specify by using the [WITH](../../relational-databases/system-functions/with-change-tracking-context-transact-sql.md) clause as part of an INSERT, UPDATE, or DELETE statement.|  
|\<primary key column value>|Same as the user table columns|The primary key values for the tracked table. These values uniquely identify each row in the user table.|  
  
### CHANGETABLE VERSION  
 When VERSION is specified, one row that has the following columns is returned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|SYS_CHANGE_VERSION|**bigint**|Current change version value that is associated with the row.<br /><br /> The value is NULL if a change has not been made for a period longer than the change tracking retention period, or the row has not been changed since change tracking was enabled.|  
|SYS_CHANGE_CONTEXT|**varbinary(128)**|Change context information that you can optionally specify by using the WITH clause as part of an INSERT, UPDATE, or DELETE statement.|  
|\<primary key column value>|Same as the user table columns|The primary key values for the tracked table. These values uniquely identify each row in the user table.|  
  
## Remarks  
 The CHANGETABLE function is typically used in the FROM clause of a query as if it were a table.  
  
## CHANGETABLE(CHANGES...)  
 To obtain row data for new or modified rows, join the result set to the user table by using the primary key columns. Only one row is returned for each row in the user table that has been changed, even if there have been multiple changes to the same row since the *last_sync_version* value.  
  
 Primary key column changes are never marked as updates. If a primary key value changes, it is considered to be a delete of the old value and an insert of the new value.  
  
 If you delete a row and then insert a row that has the old primary key, the change is seen as an update to all columns in the row.  
  
 The values that are returned for the `SYS_CHANGE_OPERATION` and `SYS_CHANGE_COLUMNS` columns are relative to the baseline (last_sync_version) that is specified. For example, if an insert operation was made at version `10` and an update operation at version `15`, and if the baseline *last_sync_version* is `12`, an update will be reported. If the *last_sync_version* value is `8`, an insert will be reported. `SYS_CHANGE_COLUMNS` will never report computed columns as having been updated.  
  
 Generally, all operations that insert, update, or delete of data in user tables are tracked, including the MERGE statement.  
  
 The following operations that affect user table data are not tracked:  
  
-   Executing the `UPDATETEXT` statement. This statement is deprecated and will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, changes that are made by using the `.WRITE` clause of the UPDATE statement are tracked.  
  
-   Deleting rows by using `TRUNCATE TABLE`. When a table is truncated, the change tracking version information that is associated with the table is reset as if change tracking has just been enabled on the table. A client application should always validate its last synchronized version. The validation fails if the table has been truncated.  
  
## CHANGETABLE(VERSION...)  
 An empty result set is returned if a nonexistent primary key is specified.  
  
 The value of `SYS_CHANGE_VERSION` might be NULL if a change has not been made for longer than the retention period (for example, the cleanup has removed the change information) or the row has never been changed since change tracking was enabled for the table.  
  
## Permissions  
 Requires the `SELECT` permission on the primary key columns and `VIEW CHANGE TRACKING` permission on the table that is specified by the *<table_name>* value to obtain change tracking information.
  
## Examples  
  
### A. Returning rows for an initial synchronization of data  
 The following example shows how to obtain data for an initial synchronization of the table data. The query returns all row data and their associated versions. You can then insert or add this data to the system that will contain the synchronized data.  
  
```sql  
-- Get all current rows with associated version  
SELECT e.[Emp ID], e.SSN, e.FirstName, e.LastName,  
    c.SYS_CHANGE_VERSION, c.SYS_CHANGE_CONTEXT  
FROM Employees AS e  
CROSS APPLY CHANGETABLE   
    (VERSION Employees, ([Emp ID], SSN), (e.[Emp ID], e.SSN)) AS c;  
```  
  
### B. Listing all changes that were made since a specific version  
 The following example lists all changes that were made in a table since the specified version (`@last_sync_version)`. [Emp ID] and SSN are columns in a composite primary key.  
  
```sql  
DECLARE @last_sync_version bigint;  
SET @last_sync_version = <value obtained from query>;  
SELECT [Emp ID], SSN,  
    SYS_CHANGE_VERSION, SYS_CHANGE_OPERATION,  
    SYS_CHANGE_COLUMNS, SYS_CHANGE_CONTEXT   
FROM CHANGETABLE (CHANGES Employees, @last_sync_version) AS C;  
```  
  
### C. Obtaining all changed data for a synchronization  
 The following example shows how you can obtain all data that has changed. This query joins the change tracking information with the user table so that user table information is returned. A `LEFT OUTER JOIN` is used so that a row is returned for deleted rows.  
  
```sql  
-- Get all changes (inserts, updates, deletes)  
DECLARE @last_sync_version bigint;  
SET @last_sync_version = <value obtained from query>;  
SELECT e.FirstName, e.LastName, c.[Emp ID], c.SSN,  
    c.SYS_CHANGE_VERSION, c.SYS_CHANGE_OPERATION,  
    c.SYS_CHANGE_COLUMNS, c.SYS_CHANGE_CONTEXT   
FROM CHANGETABLE (CHANGES Employees, @last_sync_version) AS c  
    LEFT OUTER JOIN Employees AS e  
        ON e.[Emp ID] = c.[Emp ID] AND e.SSN = c.SSN;  
```  
  
### D. Detecting conflicts by using CHANGETABLE(VERSION...)  
 The following example shows how to update a row only if the row has not changed since the last synchronization. The version number of the specific row is obtained by using `CHANGETABLE`. If the row has been updated, changes are not made and the query returns information about the most recent change to the row.  
  
```sql  
-- @last_sync_version must be set to a valid value  
UPDATE  
    SalesLT.Product  
SET  
    ListPrice = @new_listprice  
FROM  
    SalesLT.Product AS P  
WHERE  
    ProductID = @product_id AND  
    @last_sync_version >= ISNULL (  
        (SELECT CT.SYS_CHANGE_VERSION FROM   
            CHANGETABLE(VERSION SalesLT.Product,  
            (ProductID), (P.ProductID)) AS CT),  
        0);  
```  
  
## See Also  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)   
 [Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)   
 [CHANGE_TRACKING_IS_COLUMN_IN_MASK &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-is-column-in-mask-transact-sql.md)   
 [CHANGE_TRACKING_CURRENT_VERSION &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-current-version-transact-sql.md)   
 [CHANGE_TRACKING_MIN_VALID_VERSION &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-min-valid-version-transact-sql.md)  
  
