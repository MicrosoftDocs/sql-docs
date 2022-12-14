---
title: "DROP SEQUENCE (Transact-SQL)"
description: DROP SEQUENCE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/11/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SEQUENCE"
  - "DROP_SEQUENCE_TSQL"
helpviewer_keywords:
  - "DROP SEQUENCE statement"
  - "sequence number object, dropping"
dev_langs:
  - "TSQL"
---
# DROP SEQUENCE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Removes a sequence object from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP SEQUENCE [ IF EXISTS ] { database_name.schema_name.sequence_name | schema_name.sequence_name | sequence_name } [ ,...n ]  
 [ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the sequence only if it already exists.  
  
 *database_name*  
 Is the name of the database in which the sequence object was created.  
  
 *schema_name*  
 Is the name of the schema to which the sequence object belongs.  
  
 *sequence_name*  
 Is the name of the sequence to be dropped. Type is **sysname**.  
  
## Remarks  
 After generating a number, a sequence object has no continuing relationship to the number it generated, so the sequence object can be dropped, even though the number generated is still in use.  
  
 A sequence object can be dropped while it is referenced by a stored procedure, or trigger, because it is not schema bound. A sequence object cannot be dropped if it is referenced as a default value in a table. The error message will list the object referencing the sequence.  
  
 To list all sequence objects in the database, execute the following statement.  
  
```sql  
SELECT sch.name + '.' + seq.name AS [Sequence schema and name]   
    FROM sys.sequences AS seq  
    JOIN sys.schemas AS sch  
        ON seq.schema_id = sch.schema_id ;  
GO  
```  
  
## Security  
  
### Permissions  
 Requires ALTER or CONTROL permission on the schema.  
  
### Audit  
 To audit **DROP SEQUENCE**, monitor the **SCHEMA_OBJECT_CHANGE_GROUP**.  
  
## Examples  
 The following example removes a sequence object named `CountBy1` from the current database.  
  
```sql  
DROP SEQUENCE CountBy1 ;  
GO  
```  
  
## See Also  
 [ALTER SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-sequence-transact-sql.md)   
 [CREATE SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-sequence-transact-sql.md)   
 [NEXT VALUE FOR &#40;Transact-SQL&#41;](../../t-sql/functions/next-value-for-transact-sql.md)   
 [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md)  
