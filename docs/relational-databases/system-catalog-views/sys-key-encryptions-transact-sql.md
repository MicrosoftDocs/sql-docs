---
title: "sys.key_encryptions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.key_encryptions"
  - "key_encryptions_TSQL"
  - "sys.key_encryptions_TSQL"
  - "key_encryptions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.key_encryptions catalog view"
ms.assetid: c39cecf8-af63-40b9-98e5-f84a5bf3ae54
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.key_encryptions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a row for each symmetric key encryption specified by using the ENCRYPTION BY clause of the CREATE SYMMETRIC KEY statement.  

  
|Column names|Data types|Description|  
|------------------|----------------|-----------------|  
|**key_id**|**int**|ID of the encrypted key.|  
|**thumbprint**|**varbinary(32)**|SHA-1 hash of the certificate with which the key is encrypted, or the GUID of the symmetric key with which the key is encrypted.|  
|**crypt_type**|**char(4)**|Type of encryption:<br /><br /> ESKS = Encrypted by symmetric key<br /><br /> ESKP, ESP2, or ESP3 = Encrypted by password<br /><br /> EPUC = Encrypted by certificate<br /><br /> EPUA = Encrypted by asymmetric key<br /><br /> ESKM = Encrypted by master key|  
|**crypt_type_desc**|**nvarchar(60)**|Description of encryption type:<br /><br /> ENCRYPTION BY SYMMETRIC KEY<br /><br /> ENCRYPTION BY PASSWORD <br />(Beginning with [!INCLUDE[sssqlv14_md](../../includes/sssqlv14-md.md)], includes a version number for use by CSS.)<br /><br /> ENCRYPTION BY CERTIFICATE<br /><br /> ENCRYPTION BY ASYMMETRIC KEY<br /><br /> ENCRYPTION BY MASTER KEY<br /><br /> Note: Windows DPAPI is used to protect the service master key.|  
|**crypt_property**|**varbinary(max)**|Signed or encrypted bits.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)  
  
  
