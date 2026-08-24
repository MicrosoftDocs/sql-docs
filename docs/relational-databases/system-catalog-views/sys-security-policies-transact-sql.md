---
title: "sys.security_policies (Transact-SQL)"
description: sys.security_policies (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYS.SECURITY_POLICIES_TSQL"
  - "SECURITY_POLICIES_TSQL"
  - "SYS.SECURITY_POLICIES"
  - "SECURITY_POLICIES"
helpviewer_keywords:
  - "sys.security_policies catalog view"
  - "security_policies catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || =azure-sqldw-latest || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.security_policies (Transact-SQL)

[!INCLUDE [SQL Server 2016 Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics FabricSQLDB](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricsqldb.md)]

  Returns a row for each security policy in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**sysname**|Name of the security policy, unique within the database.|  
|object_id|**int**|ID of the security policy.|  
|principal_id|**int**|ID of the owner of the security policy, as registered to the database. NULL if the owner is determined via the schema.|  
|schema_id|**int**|ID of the schema where the object resides.|  
|parent_object_id|**int**|ID of the object to which the policy belongs. Must be 0.|  
|type|**vachar(2)**|Must be **SP**.|  
|type_desc|**nvarchar(60)**|**SECURITY_POLICY**.|  
|create_date|**datetime**|UTC date the security policy was created.|  
|modify_date|**datetime**|UTC date the security policy was last modified.|  
|is_ms_shipped|**bit**|Always false.|  
|is_enabled|**bit**|Security policy specification state:<br /><br /> 0 = disabled<br /><br /> 1 = enabled|  
|is_not_for_replication|**bit**|Policy was created with the NOT FOR REPLICATION option.|  
|uses_database_collation|**bit**|Uses the same collation as the database.|  
|is_schemabinding_enabled|**bit**|Schemabinding state for the security policy:<br /><br /> 0 or NULL = enabled<br /><br /> 1 = disabled|  
  
## Permissions  
 Principals with the **ALTER ANY SECURITY POLICY** permission have access to all objects in this catalog view as well as anyone with **VIEW DEFINITION** on the object.  
  
## Related content

- [Row-level security](../security/row-level-security.md)
- [sys.security_predicates (Transact-SQL)](sys-security-predicates-transact-sql.md)
- [CREATE SECURITY POLICY (Transact-SQL)](../../t-sql/statements/create-security-policy-transact-sql.md)
- [Security Catalog Views (Transact-SQL)](security-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Principals (Database Engine)](../security/authentication-access/principals-database-engine.md)
