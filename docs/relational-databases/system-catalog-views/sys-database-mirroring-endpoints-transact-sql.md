---
title: "sys.database_mirroring_endpoints (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.database_mirroring_endpoints_TSQL"
  - "database_mirroring_endpoints"
  - "sys.database_mirroring_endpoints"
  - "database_mirroring_endpoints_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database mirroring [SQL Server], endpoint"
  - "HADR [SQL Server], endpoint"
  - "database mirroring [SQL Server], catalog views"
  - "sys.database_mirroring_endpoints catalog view"
ms.assetid: f2285199-97ad-473c-a52d-270044dd862b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sys.database_mirroring_endpoints (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for the database mirroring endpoint of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  The database mirroring endpoint supports both sessions between database mirroring partners and with witnesses and sessions between the primary replica of a Always On availability group and its secondary replicas.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**|-|Inherits columns from **sys.endpoints** (for more information, see [sys.endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md)).|  
|**role**|**tinyint**|Mirroring role, one of:<br /><br /> **0** = None<br /><br /> **1** = Partner<br /><br /> **2** = Witness<br /><br /> **3** = All<br /><br /> Note: This value is relevant only for database mirroring.|  
|**role_desc**|**nvarchar(60)**|Description of mirroring role, one of:<br /><br /> **NONE**<br /><br /> **PARTNER**<br /><br /> **WITNESS**<br /><br /> **ALL**<br /><br /> Note: This value is relevant only for database mirroring.|  
|**is_encryption_enabled**|**bit**|**1** means that encryption is enabled.<br /><br /> **0** means that encryption is disabled.|  
|**connection_auth**|**tinyint**|The type of connection authentication required for connections to this endpoint, one of:<br /><br /> **1** - NTLM<br /><br /> **2** - KERBEROS<br /><br /> **3** - NEGOTIATE<br /><br /> **4** - CERTIFICATE<br /><br /> **5** - NTLM, CERTIFICATE<br /><br /> **6** - KERBEROS, CERTIFICATE<br /><br /> **7** - NEGOTIATE, CERTIFICATE<br /><br /> **8** - CERTIFICATE, NTLM<br /><br /> **9** - CERTIFICATE, KERBEROS<br /><br /> **10** - CERTIFICATE, NEGOTIATE|  
|**connection_auth_desc**|**Nvarchar (60)**|Description of the type of authentication required for connections to this endpoint, one of:<br /><br /> NTLM<br /><br /> KERBEROS<br /><br /> NEGOTIATE<br /><br /> CERTIFICATE<br /><br /> NTLM, CERTIFICATE<br /><br /> KERBEROS, CERTIFICATE<br /><br /> NEGOTIATE, CERTIFICATE<br /><br /> CERTIFICATE, NTLM<br /><br /> CERTIFICATE, KERBEROS<br /><br /> CERTIFICATE, NEGOTIATE|  
|**certificate_id**|**int**|ID of certificate used for authentication, if any.<br /><br /> 0 = Windows Authentication is being used.|  
|**encryption_algorithm**|**tinyint**|Encryption algorithm, one of:<br /><br /> **0** - NONE<br /><br /> **1** - RC4<br /><br /> **2** - AES<br /><br /> **3** - NONE, RC4<br /><br /> **4** - NONE, AES<br /><br /> **5** - RC4, AES<br /><br /> **6** - AES, RC4<br /><br /> **7** - NONE, RC4, AES<br /><br /> **8** - NONE, AES, RC4|  
|**encryption_algorithm_desc**|**nvarchar(60)**|Description of the encryption algorithm, one of:<br /><br /> NONE<br /><br /> RC4<br /><br /> AES<br /><br /> NONE, RC4<br /><br /> NONE, AES<br /><br /> RC4, AES<br /><br /> AES, RC4<br /><br /> NONE, RC4, AES<br /><br /> NONE, AES, RC4|  
  
## Remarks  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and higher, material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)   
 [sys.availability_replicas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)   
 [sys.database_mirroring &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md)   
 [sys.database_mirroring_witnesses &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)  
  
  
