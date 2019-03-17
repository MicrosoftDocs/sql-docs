---
title: "sys.login_token (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "login_token_TSQL"
  - "sys.login_token_TSQL"
  - "sys.login_token"
  - "login_token"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.login_token catalog view"
  - "logins [SQL Server], security tokens"
  - "tokens [SQL Server]"
ms.assetid: 86e06938-9d0a-44e5-99e2-55c8ef5f2f84
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sys.login_token (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row for every server principal that is part of the login token.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**principal_id**|**int**|ID of the principal. This value is unique within server.|  
|**sid**|**varbinary(85)**|Security identifier of the principal. If this is a Windows principal, **sid** = Windows SID. If the login is mapped to a certificate, **sid** = GUID from the certificate.|  
|**name**|**nvarchar(128)**|Name of the principal. This value is unique within server.|  
|**type**|**nvarchar(128)**|Description of principal type. All types are mapped to **sid**. The value can be one of the following:<br /><br /> SQL LOGIN<br /><br /> WINDOWS LOGIN<br /><br /> WINDOWS GROUP<br /><br /> SERVER ROLE<br /><br /> LOGIN MAPPED TO CERTIFICATE<br /><br /> LOGIN MAPPED TO ASYMMETRIC KEY<br /><br /> CERTIFICATE<br /><br /> ASYMMETRIC KEY|  
|**usage**|**nvarchar(128)**|Indicates the principal participates in the evaluation of GRANT or DENY permissions, or serves as an authenticator.<br /><br /> This value can be one of the following:<br /><br /> GRANT OR DENY<br /><br /> DENY ONLY<br /><br /> AUTHENTICATOR|  
  
## See Also  
 [sys.user_token &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-user-token-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
