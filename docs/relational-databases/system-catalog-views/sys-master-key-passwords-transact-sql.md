---
title: "sys.master_key_passwords (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.master_key_passwords_TSQL"
  - "master_key_passwords_TSQL"
  - "sys.master_key_passwords"
  - "master_key_passwords"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.master_key_passwords catalog view"
ms.assetid: b8e18cff-a9e6-4386-98ce-1cd855506e03
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.master_key_passwords (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row for each database master key password added by using the **sp_control_dbmasterkey_password** stored procedure. The passwords that are used to protect the master keys are stored in the credential store. The credential name follows this format: ##DBMKEY_<database_family_guid>_<random_password_guid>##. The password is stored as the credential secret. For each password added by using **sp_control_dbmasterkey_password**, there is a row in **sys.credentials**.  
  
 Each row in this view shows a **credential_id** and the **family_guid** of a database the master key of which is protected by the password associated with that credential. A join with **sys.credentials** on the **credential_id** will return useful fields, such as the **create_date** and credential name.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**credential_id**|**int**|ID of the credential to which the password belongs. This ID is unique within the server instance.|  
|**family_guid**|**uniqueidentifier**|Unique ID of the original database at creation. This GUID remains the same after the database is restored or attached, even if the database name is changed.<br /><br /> If automatic decryption by the service master key fails, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the **family_guid** to identify credentials that may contain the password used to protect the database master key.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sp_control_dbmasterkey_password &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-control-dbmasterkey-password-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
