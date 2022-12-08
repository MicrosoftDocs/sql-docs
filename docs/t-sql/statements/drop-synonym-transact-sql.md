---
title: "DROP SYNONYM (Transact-SQL)"
description: DROP SYNONYM (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SYNONYM"
  - "DROP_SYNONYM_TSQL"
helpviewer_keywords:
  - "deleting synonyms"
  - "synonyms [SQL Server], removing"
  - "removing synonyms"
  - "DROP SYNONYM statement"
  - "dropping synonyms"
dev_langs:
  - "TSQL"
---
# DROP SYNONYM (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Removes a synonym from a specified schema.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP SYNONYM [ IF EXISTS ] [ schema. ] synonym_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).
  
 Conditionally drops the synonym only if it already exists.  
  
 *schema*  
 Specifies the schema in which the synonym exists. If schema is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the default schema of the current user.  
  
 *synonym_name*  
 Is the name of the synonym to be dropped.  
  
## Remarks  
 References to synonyms are not schema-bound; therefore, you can drop a synonym at any time. References to dropped synonyms will be found only at run time.  
  
 Synonyms can be created, dropped and referenced in dynamic SQL.  
  
## Permissions  
 To drop a synonym, a user must satisfy at least one of the following conditions. The user must be:  
  
-   The current owner of a synonym.  
  
-   A grantee holding CONTROL on a synonym.  
  
-   A grantee holding ALTER SCHEMA permission on the containing schema.  
  
## Examples  
 The following example first creates a synonym, `MyProduct`, and then drops the synonym.  
  
```sql  
USE tempdb;  
GO  
-- Create a synonym for the Product table in AdventureWorks2012.  
CREATE SYNONYM MyProduct  
FOR AdventureWorks2012.Production.Product;  
GO  
-- Drop synonym MyProduct.  
USE tempdb;  
GO  
DROP SYNONYM MyProduct;  
GO  
```  
  
## See Also  
 [CREATE SYNONYM &#40;Transact-SQL&#41;](../../t-sql/statements/create-synonym-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
