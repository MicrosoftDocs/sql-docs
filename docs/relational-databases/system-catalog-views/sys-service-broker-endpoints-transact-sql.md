---
title: "sys.service_broker_endpoints (Transact-SQL)"
description: sys.service_broker_endpoints (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.service_broker_endpoints_TSQL"
  - "service_broker_endpoints"
  - "service_broker_endpoints_TSQL"
  - "sys.service_broker_endpoints"
helpviewer_keywords:
  - "sys.service_broker_endpoints catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 6979ec9b-0043-411e-aafb-0226fa26c5ba
---
# sys.service_broker_endpoints (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view contains one row for the Service Broker endpoint. For every row in this view, there is a corresponding row with the same **endpoint_id** in the **sys.tcp_endpoints** view that contains the TCP configuration metadata. TCP is the only allowed protocol for Service Broker.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**|**--**|Inherits columns from [sys.endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md).|  
|**is_message_forwarding_enabled**|**bit**|Does endpoint support message forwarding. This is initially set to **0** (disabled). Not NULLABLE.|  
|**message_forwarding_size**|**int**|The maximum number of megabytes of **tempdb** space allowed to be used  for messages being forwarded. This is initially set to **10**. Not NULLABLE.|  
|**connection_auth**|**tinyint**|The type of connection authentication required for connections to this endpoint, one of:<br /><br /> **1** - NTLM<br /><br /> **2** - KERBEROS<br /><br /> **3** - NEGOTIATE<br /><br /> **4** - CERTIFICATE<br /><br /> **5** - NTLM, CERTIFICATE<br /><br /> **6** - KERBEROS, CERTIFICATE<br /><br /> **7** - NEGOTIATE, CERTIFICATE<br /><br /> **8** - CERTIFICATE, NTLM<br /><br /> **9** - CERTIFICATE, KERBEROS<br /><br /> **10** - CERTIFICATE, NEGOTIATE<br /><br /> Not NULLABLE.|  
|**connection_auth_desc**|**nvarchar(60)**|Description of the type of connection authentication required for connections to this endpoint, one of:<br /><br /> NTLM<br /><br /> KERBEROS<br /><br /> NEGOTIATE<br /><br /> CERTIFICATE<br /><br /> NTLM, CERTIFICATE<br /><br /> KERBEROS, CERTIFICATE<br /><br /> NEGOTIATE, CERTIFICATE<br /><br /> CERTIFICATE, NTLM<br /><br /> CERTIFICATE, KERBEROS<br /><br /> CERTIFICATE, NEGOTIATE<br /><br /> NULLABLE.|  
|**certificate_id**|**int**|ID of certificate used for authentication, if any.<br /><br /> 0 = Windows Authentication is being used.|  
|**encryption_algorithm**|**tinyint**|Encryption algorithm. The following are the possible values with their descriptions and corresponding DDL options.<br /><br /> **0** : NONE. Corresponding DDL option: Disabled.<br /><br /> **1** :  RC4. Corresponding DDL option: {Required   &#124; Required   algorithm RC4}.<br /><br /> **2** : AES. Corresponding DDL option: Required Algorithm AES.<br /><br /> **3** : NONE, RC4. Corresponding DDL option: {Supported &#124; Supported Algorithm RC4}.<br /><br /> **4** : NONE, AES. Corresponding DDL option: Supported Algorithm AES.<br /><br /> **5** : RC4, AES. Corresponding DDL option: Required Algorithm RC4 AES.<br /><br /> **6** : AES, RC4. Corresponding DDL option: Required Algorithm AES RC4.<br /><br /> **7** : NONE, RC4, AES. Corresponding DDL option: Supported Algorithm RC4 AES.<br /><br /> **8** : NONE, AES, RC4. Corresponding DDL option: Supported Algorithm AES RC4.<br /><br /> Not NULLABLE.|  
|**encryption_algorithm_desc**|**nvarchar(60)**|Encryption algorithm description. The possible values and their corresponding DDL options are listed below:<br /><br /> NONE : Disabled<br /><br /> RC4  : {Required &#124; Required Algorithm RC4}<br /><br /> AES  : Required Algorithm AES<br /><br /> NONE, RC4 : {Supported &#124; Supported Algorithm RC4}<br /><br /> NONE, AES : Supported Algorithm AES<br /><br /> RC4, AES : Required Algorithm RC4 AES<br /><br /> AES, RC4 : Required Algorithm AES RC4<br /><br /> NONE, RC4, AES : Supported Algorithm RC4 AES<br /><br /> NONE, AES, RC4 : Supported Algorithm AES RC4<br /><br /> NULLABLE.|  
  
## Remarks  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions, material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md)   
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)  
  
  
