---
title: "sys.sql_logins (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/20/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sql_logins_TSQL"
  - "sql_logins_TSQL"
  - "sys.sql_logins"
  - "sql_logins"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sql_logins catalog view"
ms.assetid: 0d9c5b09-86fe-40ff-baab-00b7c051402f
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.sql_logins (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-pdw-md.md)]

  Returns one row for every [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**|--|Inherits from **sys.server_principals**.|  
|**is_policy_checked**|**bit**|Password policy is checked.|  
|**is_expiration_checked**|**bit**|Password expiration is checked.|  
|**password_hash**|**varbinary(256)**|Hash of SQL login password. Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], stored password information is calculated using SHA-512 of the salted password.|  
  
 For a list of columns that this view inherits, see [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md).  
  
## Remarks  
 To view both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins and Windows authentication logins, see [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md).  
  
 When contained database users are enabled, connections can be made without logins. To identify those accounts, see  [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md).  
  
## Permissions  
 Any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login can see their own login name, and the sa login. To see other logins, requires ALTER ANY LOGIN, or a permission on the login.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Password Policy](../../relational-databases/security/password-policy.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
