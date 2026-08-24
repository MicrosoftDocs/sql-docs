---
title: "DROP MASTER KEY (Transact-SQL)"
description: DROP MASTER KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DROP MASTER KEY"
  - "DROP_MASTER_KEY_TSQL"
helpviewer_keywords:
  - "removing Database Master Keys"
  - "database master key [SQL Server], removing"
  - "encryption [SQL Server], Database Master Key"
  - "DROP MASTER KEY statement"
  - "cryptography [SQL Server], Database Master Key"
  - "dropping Database Master Keys"
  - "deleting Database Master Keys"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# DROP MASTER KEY (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw-fabric](../../includes/applies-to-version/sql-asdbmi-asa-pdw-fabricsqldb.md)]

  Removes the master key from the current database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
DROP MASTER KEY  
```  
  
## Arguments  
 This statement takes no arguments.  
  
## Remarks  
 The drop will fail if any private key in the database is protected by the master key.  
  
## Permissions  
 Requires CONTROL permission on the database.  
  
## Examples  
 The following example removes the master key for the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
USE AdventureWorks2022;  
DROP MASTER KEY;  
GO  
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example removes the master key.  
  
```sql  
USE master;  
DROP MASTER KEY;  
GO  
```  
  
## Related content

- [CREATE MASTER KEY (Transact-SQL)](create-master-key-transact-sql.md)
- [OPEN MASTER KEY (Transact-SQL)](open-master-key-transact-sql.md)
- [CLOSE MASTER KEY (Transact-SQL)](close-master-key-transact-sql.md)
- [BACKUP MASTER KEY (Transact-SQL)](backup-master-key-transact-sql.md)
- [RESTORE MASTER KEY (Transact-SQL)](restore-master-key-transact-sql.md)
- [ALTER MASTER KEY (Transact-SQL)](alter-master-key-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
