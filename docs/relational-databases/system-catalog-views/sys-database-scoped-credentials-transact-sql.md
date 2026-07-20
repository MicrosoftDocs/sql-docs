---
title: "sys.database_scoped_credentials (Transact-SQL)"
description: sys.database_scoped_credentials (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 05/19/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.database_scoped_credentials"
  - "sys.database_scoped_credentials_TSQL"
  - "database_scoped_credentials"
  - "database_scoped_credentials_TSQL"
helpviewer_keywords:
  - "sys.database_scoped_credentials catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.database_scoped_credentials (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabric](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricsqldb.md)]

  Returns one row for each database scoped credential in the database.  

::: moniker range=">=sql-server-2017||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-linux-2017||=azuresqldb-mi-current|| =fabric-sqldb"
|Column name|Data type|Description|
|-----------------|---------------|-----------------|  
| `name` |**sysname**|Name of the database scoped credential. Is unique in the database.|  
| `credential_id` |**int**|ID of the database scoped credential. Is unique in the database.|  
| `principal_id` |**int**|ID of the database principal who owns the key.|  
| `credential_identity` |**nvarchar(4000)**|Name of the identity to use. It does not have to be unique.|  
| `create_date` |**datetime**|Time at which the database scoped credential was created.|  
| `modify_date` |**datetime**|Time at which the database scoped credential was last modified.|  
| `target_type` |**nvarchar(100)**|Type of database scoped credential. Returns `NULL` for database scoped credentials.|  
| `target_id` |**int**|ID of the object that the database scoped credential is mapped to. Returns `0` for database scoped credentials|  
::: moniker-end

## Permissions

 Requires `CONTROL` permission on the database.  

## Related content

- [Credentials (Database Engine)](../security/authentication-access/credentials-database-engine.md)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [ALTER DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/alter-database-scoped-credential-transact-sql.md)
- [DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/drop-database-scoped-credential-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)
- [sys.credentials (Transact-SQL)](sys-credentials-transact-sql.md)
