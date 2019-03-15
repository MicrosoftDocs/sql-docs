---
title: "sys.security_predicates (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "SYS.SECURITY_PREDICATES"
  - "SECURITY_PREDICATES"
  - "SECURITY_PREDICATES_TSQL"
  - "SYS.SECURITY_PREDICATES_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.security_predicates catalog view"
  - "security_predicates catalog view"
ms.assetid: c7a2f28c-98da-463d-8b8a-8e5619e2c6a6
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=azure-sqldw-latest||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.security_predicates (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

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
  
## See Also  
 [Row-Level Security](../../relational-databases/security/row-level-security.md)   
 [sys.security_policies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md)   
 [CREATE SECURITY POLICY &#40;Transact-SQL&#41;](../../t-sql/statements/create-security-policy-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
