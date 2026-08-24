---
title: "sys.security_predicates (Transact-SQL)"
description: sys.security_predicates (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYS.SECURITY_PREDICATES"
  - "SECURITY_PREDICATES"
  - "SECURITY_PREDICATES_TSQL"
  - "SYS.SECURITY_PREDICATES_TSQL"
helpviewer_keywords:
  - "sys.security_predicates catalog view"
  - "security_predicates catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || =azure-sqldw-latest || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.security_predicates (Transact-SQL)

[!INCLUDE [SQL Server 2016 Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics FabricSQLDB](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricsqldb.md)]

  Returns a row for each security predicate in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the security policy that contains this predicate.|  
|security_predicate_id|**int**|Predicate ID within this security policy.|  
|target_object_id|**int**|ID of the object on which the security predicate is bound.|  
|predicate_definition|**nvarchar(max)**|Fully qualified name of the function that will be used as a security predicate, including the arguments. Note that the `schema.function` name may be normalized (i.e. escaped) as well as any other element in the text for consistency. For example:<br /><br /> `[dbo].[fn_securitypredicate]([wing], [startTime], [endTime])`|  
|predicate_type|**int**|The type of predicate used by the security policy:<br /><br /> 0 = FILTER PREDICATE<br /><br /> 1 = BLOCK PREDICATE|  
|predicate_type_desc|**nvarchar(60)**|The type of predicate used by the security policy:<br /><br /> FILTER<br /><br /> BLOCK|  
|operation|**int**|The type of operation specified for the predicate:<br /><br /> NULL = all applicable operations<br /><br /> 1 = AFTER INSERT<br /><br /> 2 = AFTER UPDATE<br /><br /> 3 = BEFORE UPDATE<br /><br /> 4 = BEFORE DELETE|  
|operation_desc|**nvarchar(60)**|The type of operation specified for the predicate:<br /><br /> NULL<br /><br /> AFTER INSERT<br /><br /> AFTER UPDATE<br /><br /> BEFORE UPDATE<br /><br /> BEFORE DELETE|  
  
## Permissions  
 Principals with the **ALTER ANY SECURITY POLICY** permission have access to all objects in this catalog view as well as anyone with **VIEW DEFINITION** on the object.  
  
## Related content

- [Row-level security](../security/row-level-security.md)
- [sys.security_policies (Transact-SQL)](sys-security-policies-transact-sql.md)
- [CREATE SECURITY POLICY (Transact-SQL)](../../t-sql/statements/create-security-policy-transact-sql.md)
- [Security Catalog Views (Transact-SQL)](security-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Principals (Database Engine)](../security/authentication-access/principals-database-engine.md)
