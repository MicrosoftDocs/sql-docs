---
title: "sys.certificates (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: acdd94d2-822a-483d-b91d-6d6514d85049
caps.latest.revision: 6
author: BarbKess
---
# sys.certificates (SQL Server PDW)
Returns a row for each certificate in the database.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**name**|**sysname**|Name of the certificate. Is unique within the database.|  
|**certificate_id**|**int**|ID of the certificate. Is unique within the database.|  
|**principal_id**|**int**|ID of the database principal that owns this certificate.|  
|**pvt_key_encryption_type**|**char(2)**|How the private key is encrypted.<br /><br />NA = There is no private key for the certificate<br /><br />MK = Private key is encrypted by the master key<br /><br />PW = Private key is encrypted by a user-defined password<br /><br />SK = Private key is encrypted by the service master key.|  
|**pvt_key_encryption_type_desc**|**nvarchar(60)**|Description of how the private key is encrypted.<br /><br />NO_PRIVATE_KEY<br /><br />ENCRYPTED_BY_MASTER_KEY<br /><br />ENCRYPTED_BY_PASSWORD<br /><br />ENCRYPTED_BY_SERVICE_MASTER_KEY|  
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
|**pvt_key_last_backup_date**|**datetime**|The date and time the certificate’s private key was last exported.|  
  
## Permissions  
The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
[CREATE CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/create-certificate-sql-server-pdw.md)  
  
