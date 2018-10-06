---
title: "sys.crypt_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "crypt_properties"
  - "crypt_properties_TSQL"
  - "sys.crypt_properties_TSQL"
  - "sys.crypt_properties"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.crypt_properties catalog view"
ms.assetid: d5684f5a-30b1-418e-ae4d-ab040db9257e
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.crypt_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns one row for each cryptographic property associated with a securable.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**class**|**tinyint**|Identifies class of thing on which property exists.<br /><br /> 1 = Object or column<br /> 5 = Assembly|  
|**class_desc**|**nvarchar(60)**|Description of the class of thing on which property exists.<br /><br /> OBJECT_OR_COLUMN<br /> ASSEMBLY|  
|**major_id**|**int**|ID of thing on which property exists, interpreted according to class|  
|**thumbprint**|**varbinary(32)**|SHA-1 hash of the certificate or asymmetric key used.|  
|**crypt_type**|**char(4)**|Encryption type.<br /><br /> SPVC = Encrypted by certificate private key<br /><br /> SPVA = Encrypted by asymmetric private key<br /><br /> CPVC = Counter signature by certificate private key<br /><br /> CPVA = Counter signature by asymmetric  key|  
|**crypt_type_desc**|**nvarchar(60)**|Description of encryption type.<br /><br /> SIGNATURE BY CERTIFICATE<br /><br /> SIGNATURE BY ASYMMETRIC KEY<br /><br /> COUNTER SIGNATURE BY CERTIFICATE<br /><br /> COUNTER SIGNATURE BY ASYMMETRIC KEY|  
|**crypt_property**|**varbinary(max)**|Signed or encrypted bits.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [Securables](../../relational-databases/security/securables.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
