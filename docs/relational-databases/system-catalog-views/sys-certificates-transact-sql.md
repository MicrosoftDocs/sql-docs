---
title: "sys.certificates (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "certificates"
  - "certificates_TSQL"
  - "sys.certificates_TSQL"
  - "sys.certificates"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.certificates catalog view"
ms.assetid: e5046102-a65c-401e-b80d-05636884dec9
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.certificates (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a row for each certificate in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the certificate. Is unique within the database.|  
|**certificate_id**|**int**|ID of the certificate. Is unique within the database.|  
|**principal_id**|**int**|ID of the database principal that owns this certificate.|  
|**pvt_key_encryption_type**|**char(2)**|How the private key is encrypted.<br /><br /> NA = There is no private key for the certificate<br /><br /> MK = Private key is encrypted by the master key<br /><br /> PW = Private key is encrypted by a user-defined password<br /><br /> SK = Private key is encrypted by the service master key.|  
|**pvt_key_encryption_type_desc**|**nvarchar(60)**|Description of how the private key is encrypted.<br /><br /> NO_PRIVATE_KEY<br /><br /> ENCRYPTED_BY_MASTER_KEY<br /><br /> ENCRYPTED_BY_PASSWORD<br /><br /> ENCRYPTED_BY_SERVICE_MASTER_KEY|  
|**is_active_for_begin_dialog**|**bit**|If 1, this certificate is used to initiate encrypted service dialogs.|  
|**issuer_name**|**nvarchar(442)**|Name of certificate issuer.|  
|**cert_serial_number**|**nvarchar(64)**|Serial number of certificate.|  
|**sid**|**varbinary(85)**|Login SID for this certificate.|  
|**string_sid**|**nvarchar(128)**|String representation of the login SID for this certificate|  
|**subject**|**nvarchar(4000)**|Subject of this certificate.|  
|**expiry_date**|**datetime**|When certificate expires.|  
|**start_date**|**datetime**|When certificate becomes valid.|  
|**thumbprint**|**varbinary(32)**|SHA-1 hash of the certificate. The SHA-1 hash is globally unique.|  
|**attested_by**|**nvarchar(260)**|System use only.|  
|pvt_key_last_backup_date|**datetime**|The date and time the certificate's private key was last exported.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)  
  
  
