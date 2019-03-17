---
title: "DROP SYNONYM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP SYNONYM"
  - "DROP_SYNONYM_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "deleting synonyms"
  - "synonyms [SQL Server], removing"
  - "removing synonyms"
  - "DROP SYNONYM statement"
  - "dropping synonyms"
ms.assetid: 23578932-e4de-4c39-a5a0-ce45139c4269
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP SYNONYM (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Removes a synonym from a specified schema.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP SYNONYM [ IF EXISTS ] [ schema. ] synonym_name  
```  
  
## Arguments  
 *IF EXISTS*  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658))
  
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
  
```  
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
  
  
