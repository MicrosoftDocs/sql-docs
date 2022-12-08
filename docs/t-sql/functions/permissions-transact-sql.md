---
title: "PERMISSIONS (Transact-SQL)"
description: "PERMISSIONS (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "PERMISSIONS_TSQL"
  - "PERMISSIONS"
helpviewer_keywords:
  - "permissions [SQL Server], verifying"
  - "current permission status"
  - "users [SQL Server], permissions status"
  - "checking permission status"
  - "security [SQL Server], permissions"
  - "verifying permission status"
  - "testing permissions"
  - "PERMISSIONS function"
dev_langs:
  - "TSQL"
---
# PERMISSIONS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a value containing a bitmap that indicates the statement, object, or column permissions of the current user.  
  
 > [!IMPORTANT]  
 > [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [fn_my_permissions](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md) and [Has_Perms_By_Name](../../t-sql/functions/has-perms-by-name-transact-sql.md) instead. Continued use of the PERMISSIONS function may result in slower performance.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
PERMISSIONS ( [ objectid [ , 'column' ] ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *objectid*  
 Is the ID of a securable. If *objectid* is not specified, the bitmap value contains statement permissions for the current user; otherwise, the bitmap contains permissions on the securable for the current user. The securable specified must be in the current database. Use the [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) function to determine the *objectid* value.  
  
 **'** *column* **'**  
 Is the optional name of a column for which permission information is being returned. The column must be a valid column name in the table specified by *objectid*.  
  
## Return Types  
 **int**  
  
## Remarks  
 PERMISSIONS can be used to determine whether the current user has the permissions required to execute a statement or to GRANT a permission to another user.  
  
 The permissions information returned is a 32-bit bitmap.  
  
 The lower 16 bits reflect permissions granted to the user, and also permissions that are applied to Windows groups or and fixed server roles of which the current user is a member. For example, a returned value of 66 (hex value 0x42), when no *objectid* is specified, indicates that the user has permission to execute the CREATE TABLE (decimal value 2) and BACKUP DATABASE (decimal value 64) statements.  
  
 The upper 16 bits reflect the permissions that the user can GRANT to other users. The upper 16 bits are interpreted exactly as those for the lower 16 bits described in the following tables, except they are shifted to the left by 16 bits (multiplied by 65536). For example, 0x8 (decimal value 8) is the bit that indicates INSERT permission when an *objectid* is specified. Whereas, 0x80000 (decimal value 524288) indicates the ability to GRANT INSERT permission, because 524288 = 8 x 65536.  
  
 Because of membership in roles, a user that does not have permission to execute a statement may still be able to grant that permission to another user.  
  
 The following table shows the bits that are used for statement permissions (*objectid* is not specified).  
  
|Bit (dec)|Bit (hex)|Statement permission|  
|-----------------|-----------------|--------------------------|  
|1|0x1|CREATE DATABASE (master database only)|  
|2|0x2|CREATE TABLE|  
|4|0x4|CREATE PROCEDURE|  
|8|0x8|CREATE VIEW|  
|16|0x10|CREATE RULE|  
|32|0x20|CREATE DEFAULT|  
|64|0x40|BACKUP DATABASE|  
|128|0x80|BACKUP LOG|  
|256|0x100|Reserved|  
  
 The following table shows the bits used for object permissions that are returned when only *objectid* is specified.  
  
|Bit (dec)|Bit (hex)|Statement permission|  
|-----------------|-----------------|--------------------------|  
|1|0x1|SELECT ALL|  
|2|0x2|UPDATE ALL|  
|4|0x4|REFERENCES ALL|  
|8|0x8|INSERT|  
|16|0x10|DELETE|  
|32|0x20|EXECUTE (procedures only)|  
|4096|0x1000|SELECT ANY (at least one column)|  
|8192|0x2000|UPDATE ANY|  
|16384|0x4000|REFERENCES ANY|  
  
 The following table shows the bits used for column-level object permissions that are returned when both *objectid* and column are specified.  
  
|Bit (dec)|Bit (hex)|Statement permission|  
|-----------------|-----------------|--------------------------|  
|1|0x1|SELECT|  
|2|0x2|UPDATE|  
|4|0x4|REFERENCES|  
  
 A NULL is returned when a specified parameter is NULL or not valid (for example, an *objectid* or column that does not exist). The bit values for permissions that do not apply (for example EXECUTE permission, bit 0x20, for a table) are undefined.  
  
 Use the bitwise AND (&) operator to determine each bit set in the bitmap that is returned by the PERMISSIONS function.  
  
 The sp_helprotect system stored procedure can also be used to return a list of permissions for a user in the current database.  
  
## Examples  
  
### A. Using the PERMISSIONS function with statement permissions  
 The following example determines whether the current user can execute the `CREATE TABLE` statement.  
  
```sql  
IF PERMISSIONS()&2=2  
   CREATE TABLE test_table (col1 INT)  
ELSE  
   PRINT 'ERROR: The current user cannot create a table.';  
```  
  
### B. Using the PERMISSIONS function with object permissions  
 The following example determines whether the current user can insert a row of data into the `Address` table in the `AdventureWorks2012` database.  
  
```sql  
IF PERMISSIONS(OBJECT_ID('AdventureWorks2012.Person.Address','U'))&8=8   
   PRINT 'The current user can insert data into Person.Address.'  
ELSE  
   PRINT 'ERROR: The current user cannot insert data into Person.Address.';  
```  
  
### C. Using the PERMISSIONS function with grantable permissions  
 The following example determines whether the current user can grant the INSERT permission on the `Address` table in the `AdventureWorks2012` database to another user.  
  
```sql  
IF PERMISSIONS(OBJECT_ID('AdventureWorks2012.Person.Address','U'))&0x80000=0x80000  
   PRINT 'INSERT on Person.Address is grantable.'  
ELSE  
   PRINT 'You may not GRANT INSERT permissions on Person.Address.';  
```  
  
## See Also  
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [OBJECT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/object-id-transact-sql.md)   
 [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)   
 [sp_helprotect &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helprotect-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)  
  
  
