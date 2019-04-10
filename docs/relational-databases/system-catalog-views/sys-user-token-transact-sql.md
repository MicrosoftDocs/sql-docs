---
title: "sys.user_token (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.user_token"
  - "user_token"
  - "sys.user_token_TSQL"
  - "user_token_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "logins [SQL Server], security tokens"
  - "sys.user_token catalog view"
  - "user tokens [SQL Server]"
  - "tokens [SQL Server]"
  - "user_token catalog view"
ms.assetid: be018103-5e57-43a4-9160-9bf420892aa7
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sys.user_token (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns one row for every database principal that is part of the user token in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**principal_id**|**int**|ID of the principal. The value is unique within database.|  
|**sid**|**varbinary(85)**|Security identifier of the principal if the principal is defined external to the database. For example, this can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, Windows login, Windows Group login, or a login mapped to a certificate, otherwise, this value is NULL.|  
|**name**|**nvarchar (128)**|Name of the principal. The value is unique within database.|  
|**type**|**nvarchar (128)**|Description of principal type. All types are mapped to **sid**. The value can be one of the following:<br /><br /> SQL USER<br /><br /> WINDOWS LOGIN<br /><br /> WINDOWS GROUP<br /><br /> ROLE<br /><br /> APPLICATION ROLE<br /><br /> DATABASE ROLE<br /><br /> USER MAPPED TO CERTIFICATE<br /><br /> USER MAPPED TO ASYMMETRIC KEY<br /><br /> CERTIFICATE<br /><br /> ASYMMETRIC KEY|  
|**usage**|**nvarchar (128)**|Indicates the principal participates in the evaluation of GRANT or DENY permissions, or serves as an authenticator.<br /><br /> This value can be one of the following:<br /><br /> GRANT OR DENY<br /><br /> DENY ONLY<br /><br /> AUTHENTICATOR|  
  
## See Also  
 [sys.login_token &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-login-token-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
