---
description: "REFERENTIAL_CONSTRAINTS (Transact-SQL)"
title: "REFERENTIAL_CONSTRAINTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "REFERENTIAL_CONSTRAINTS"
  - "REFERENTIAL_CONSTRAINTS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS view"
  - "REFERENTIAL_CONSTRAINTS view"
ms.assetid: 5d358f18-0a85-4b55-af4b-98d5f4cd1020
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# REFERENTIAL_CONSTRAINTS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns one row for each FOREIGN KEY constraint in the current database. This information schema view returns information about the objects to which the current user has permissions.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**CONSTRAINT_CATALOG**|**nvarchar(**128**)**|Constraint qualifier.|  
|**CONSTRAINT_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the constraint.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**CONSTRAINT_NAME**|**sysname**|Constraint name.|  
|**UNIQUE_CONSTRAINT_CATALOG**|**nvarchar(**128**)**|UNIQUE constraint qualifier.|  
|**UNIQUE_CONSTRAINT_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the UNIQUE constraint.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**UNIQUE_CONSTRAINT_NAME**|**sysname**|UNIQUE constraint.|  
|**MATCH_OPTION**|**varchar(**7**)**|Referential constraint-matching conditions. Always returns SIMPLE. This means that no match is defined. The condition is considered a match when one of the following is true:<br /><br /> At least one value in the foreign key column is NULL.<br /><br /> All values in the foreign key column are not NULL, and there is a row in the primary key table that has the same key.|  
|**UPDATE_RULE**|**varchar(**11**)**|Action taken when a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement violates the referential integrity that is defined by this constraint. Returns one of the following: <br />NO ACTION<br />CASCADE<br />SET NULL<br />SET DEFAULT<br /><br /> If NO ACTION is specified on ON UPDATE for this constraint, the update of the primary key that is referenced in the constraint won't be propagated to the foreign key. If such an update of a primary key will cause a referential integrity violation because at least one foreign key contains the same value, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won't make any change to the parent and referring tables. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also will raise an error.<br /><br /> If CASCADE is specified on ON UPDATE for this constraint, any change to the primary key value is automatically propagated to the foreign key value.|  
|**DELETE_RULE**|**varchar(**11**)**|Action taken when a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement violates referential integrity defined by this constraint. Returns one of the following: <br />NO ACTION<br />CASCADE<br />SET NULL<br />SET DEFAULT<br /><br /> If NO ACTION is specified on ON DELETE for this constraint, the delete on the primary key that is referenced in the constraint won't be propagated to the foreign key. If such a delete of a primary key will cause a referential integrity violation because at least one foreign key contains the same value, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won't make any change to the parent and referring tables. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also will raise an error.<br /><br /> If CASCADE is specified on ON DELETE on this constraint, any change to the primary key value is automatically propagated to the foreign key value.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.foreign_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-foreign-keys-transact-sql.md)  
  
