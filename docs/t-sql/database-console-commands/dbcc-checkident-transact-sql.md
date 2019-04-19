---
title: "DBCC CHECKIDENT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CHECKIDENT"
  - "DBCC CHECKIDENT"
  - "CHECKIDENT_TSQL"
  - "DBCC_CHECKIDENT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "checking identity values"
  - "reseeding identity values"
  - "resetting identity values"
  - "identity values [SQL Server]"
  - "identity values [SQL Server], checking"
  - "modifying identity values"
  - "current identity values"
  - "DBCC CHECKIDENT statement"
  - "identity values [SQL Server], reseeding"
  - "reporting current identity values"
ms.assetid: 2c00ee51-2062-4e47-8b19-d90f524c6427
author: pmasl
ms.author: umajay
manager: craigg
monikerRange: "= azuresqldb-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azure-sqldw-latest||= sqlallproducts-allversions"
---
# DBCC CHECKIDENT (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]

  Checks the current identity value for the specified table in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and, if it's needed, changes the identity value. You can also use DBCC CHECKIDENT to manually set a new current identity value for the identity column.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```console
  
DBCC CHECKIDENT
 (
    table_name  
        [, { NORESEED | { RESEED [, new_reseed_value ] } } ]  
)  
[ WITH NO_INFOMSGS ]  
```  
  
## Arguments

 *table_name*  
 Is the name of the table for which to check the current identity value. The table specified must contain an identity column. Table names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Two or three part names must be delimited, such as 'Person.AddressType' or [Person.AddressType].
  
 NORESEED  
 Specifies that the current identity value shouldn't be changed.  
  
 RESEED  
 Specifies that the current identity value should be changed.  
  
 *new_reseed_value*  
 Is the new value to use as the current value of the identity column.  
  
 WITH NO_INFOMSGS  
 Suppresses all informational messages.  
  
## Remarks

 The specific corrections made to the current identity value depend on the parameter specifications.  
  
|DBCC CHECKIDENT command|Identity correction or corrections made|  
|-----------------------------|---------------------------------------------|  
|DBCC CHECKIDENT ( *table_name*, NORESEED )|Current identity value is not reset. DBCC CHECKIDENT returns the current identity value and the current maximum value of the identity column. If the two values are not the same, you should reset the identity value to avoid potential errors or gaps in the sequence of values.|  
|DBCC CHECKIDENT ( *table_name* )<br /><br /> or<br /><br /> DBCC CHECKIDENT ( *table_name*, RESEED )|If the current identity value for a table is less than the maximum identity value stored in the identity column, it is reset using the maximum value in the identity column. See the 'Exceptions' section that follows.|  
|DBCC CHECKIDENT ( *table_name*, RESEED, *new_reseed_value* )|Current identity value is set to the *new_reseed_value*. If no rows have been inserted into the table since the table was created, or if all rows have been removed by using the TRUNCATE TABLE statement, the first row inserted after you run DBCC CHECKIDENT uses *new_reseed_value* as the identity.<br /><br /> If rows are present in the table, or if all rows have been removed by using the DELETE statement, the next row inserted uses *new_reseed_value* + the [current increment](../../t-sql/functions/ident-incr-transact-sql.md) value.<br /><br /> If the table is not empty, setting the identity value to a number less than the maximum value in the identity column can result in one of the following conditions:<br /><br /> -If a PRIMARY KEY or UNIQUE constraint exists on the identity column, error message 2627 will be generated on later insert operations into the table because the generated identity value will conflict with existing values.<br /><br /> -If a PRIMARY KEY or UNIQUE constraint does not exist, later insert operations will result in duplicate identity values.|  
  
## Exceptions

 The following table lists conditions when DBCC CHECKIDENT doesn't automatically reset the current identity value, and provides methods for resetting the value.  
  
|Condition|Reset methods|  
|---------------|-------------------|  
|The current identity value is larger than the maximum value in the table.|Execute DBCC CHECKIDENT (*table_name*, NORESEED) to determine the current maximum value in the column. Next, specify that value as the *new_reseed_value* in a DBCC CHECKIDENT (*table_name*, RESEED,*new_reseed_value*) command.<br /><br /> -OR-<br /><br /> Execute DBCC CHECKIDENT (*table_name*, RESEED,*new_reseed_value*) with *new_reseed_value* set to a very low value, and then run DBCC CHECKIDENT (*table_name*, RESEED) to correct the value.|  
|All rows are deleted from the table.|Execute DBCC CHECKIDENT (*table_name*, RESEED,*new_reseed_value*) with *new_reseed_value* set to the new starting value.|  
  
## Changing the Seed Value

 The seed value is the value inserted into an identity column for the first row loaded into the table. All subsequent rows contain the current identity value plus the increment value where current identity value is the last identity value generated for the table or view.  
  
 You can't use DBCC CHECKIDENT for the following tasks:  
  
- Change the original seed value specified for an identity column when the table or view was created.  
  
- Reseed existing rows in a table or view.  
  
 To change the original seed value and reseed any existing rows, drop the identity column and recreate it specifying the new seed value. When the table contains data, the identity numbers are added to the existing rows with the specified seed and increment values. The order in which the rows are updated isn't guaranteed.  
  
## Result Sets

 Whether or not you specify any options for a table that contains an identity column, DBCC CHECKIDENT returns the following message for all operations except one. That operation is specifying a new seed value.  
  
`Checking identity information: current identity value '\<current identity value>', current column value '\<current column value>'. DBCC execution completed. If DBCC printed error messages, contact your system administrator.`
  
 When DBCC CHECKIDENT is used to specify a new seed value by using RESEED *new_reseed_value*, the following message is returned.  
  
`Checking identity information: current identity value '\<current identity value>'. DBCC execution completed. If DBCC printed error messages, contact your system administrator.`
  
## Permissions

 Caller must own the schema that contains the table, or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.

Azure SQL Data Warehouse requires **db_owner** permissions.
  
## Examples  
  
### A. Resetting the current identity value, if it's needed  
 The following example resets the current identity value, if it's needed, of the specified table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql
USE AdventureWorks2012;  
GO  
DBCC CHECKIDENT ('Person.AddressType');  
GO  
```  
  
### B. Reporting the current identity value

 The following example reports the current identity value in the specified table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, and doesn't correct the identity value if it's incorrect.  
  
```sql
USE AdventureWorks2012;
GO  
DBCC CHECKIDENT ('Person.AddressType', NORESEED);
GO  
```  
  
### C. Forcing the current identity value to a new value  
 The following example forces the current identity value in the `AddressTypeID` column in the `AddressType` table to a value of 10. Because the table has existing rows, the next row inserted will use 11 as the value – the new current identity value defined for the column plus 1 (which is the column's increment value).  

```sql
USE AdventureWorks2012;  
GO  
DBCC CHECKIDENT ('Person.AddressType', RESEED, 10);  
GO  
```

### D. Resetting the identity value on an empty table

 The following example forces the current identity value in the `ErrorLogID` column in the `ErrorLog` table to a value of 1, after deleting all records from table. Because the table has no existing rows, the next row inserted will use 1 as the value, that is, the new current identity value, without adding the increment value defined for the column.  
  
```sql
USE AdventureWorks2012;  
GO  
TRUNCATE TABLE dbo.ErrorLog
GO
DBCC CHECKIDENT ('dbo.ErrorLog', RESEED, 1);  
GO  
```  
  
## See Also

[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql-identity-property.md)  
[Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md)  
[USE &#40;Transact-SQL&#41;](../../t-sql/language-elements/use-transact-sql.md)  
[IDENT_SEED &#40;Transact-SQL&#41;](../../t-sql/functions/ident-seed-transact-sql.md)  
[IDENT_INCR &#40;Transact-SQL&#41;](../../t-sql/functions/ident-incr-transact-sql.md)  