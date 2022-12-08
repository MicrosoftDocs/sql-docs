---
title: "sys.database_scoped_configurations (Transact-SQL)"
description: sys.database_scoped_configurations (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/14/2018"
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "database_scoped_configurations"
  - "database_scoped_configurations_TSQL"
  - "sys.database_scoped_configurations"
  - "sys.database_scoped_configurations_TSQL"
helpviewer_keywords:
  - "sys.database_scoped_configurations catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 8899310a-3464-4d38-9f2f-88396c4e7dc2
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azure-sqldw-latest"
---
# sys.database_scoped_configurations (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2016-asdb-addw-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]

Contains one row per configuration. 

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**configuration_id**|**int**|ID of the configuration option.|
|**name**|**nvarchar(60)**|The name of the configuration option. For information about the possible configurations, see [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).|
|**value**|**sqlvariant**|The value set for this configuration option for the primary replica.|
|**value_for_secondary**|**sqlvariant**|The value set for this configuration option for the secondary replicas.|
|**is_value_default**|**bit** |Specifies whether the value set is the default value. Added in SQL Server 2017.|

## <a name="Permissions"></a> Permissions

Requires membership in the **public** role.

## Remarks

When NULL is returned as the value for **value_for_secondary**, this means that the secondary is set to PRIMARY.
 
Database scoped configuration settings will be carried over with the database. This means that when a given database is restored or attached, the existing configuration settings remain.

## See Also

[ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
