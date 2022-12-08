---
title: "sys.database_credentials (Transact-SQL)"
description: sys.database_credentials (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "02/27/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.database_credentials"
  - "sys.database_credentials_TSQL"
  - "database_credentials"
  - "database_credentials_TSQL"
helpviewer_keywords:
  - "sys.database_credentials catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 796322df-e62a-45bf-b519-89e1d521abce
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_credentials (Transact-SQL)
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

  Returns one row for each database scoped credential in the database.  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) instead.    
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|credential_id|**int**|ID of the database scoped credential. Is unique in the database.|  
|name|**sysname**|Name of the database scoped credential. Is unique in the database.|  
|credential_identity|**nvarchar(4000)**|Name of the identity to use. This will generally be a Windows user. It does not have to be unique.|  
|create_date|**datetime**|Time at which the database scoped credential was created.|  
|modify_date|**datetime**|Time at which the database scoped credential was last modified.|  
|target_type|**nvarchar(100)**|Type of database scoped credential. Returns NULL for database scoped credentials.|  
|target_id|**int**|ID of the object that the database scoped credential is mapped to. Returns 0 for database scoped credentials|  
  
## Permissions  
 Requires `CONTROL` permission on the database.  
  
## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)   
 [ALTER DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-credential-transact-sql.md)   
 [DROP DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-scoped-credential-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)  
  
  
