---
title: "sys.server_principals (Transact-SQL)"
description: sys.server_principals (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "04/11/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_principals"
  - "sys.server_principals_TSQL"
  - "sys.server_principals"
  - "server_principals_TSQL"
helpviewer_keywords:
  - "sys.server_principals catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-current||=azuresqldb-mi-current"
---
# sys.server_principals (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

  Contains a row for every server-level principal.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the principal. Is unique within a server.|  
|**principal_id**|**int**|ID number of the Principal. Is unique within a server.|  
|**sid**|**varbinary(85)**|SID (Security-IDentifier) of the principal.|  
|**type**|**char(1)**|Principal type:<br /><br /> S = SQL login<br /> R = Server role<br /><br /> E = External Login from Azure Active Directory<br /><br /> X = External group from Azure Active Directory group or applications<br />|  
|**type_desc**|**nvarchar(60)**|Description of the principal type:<br /><br /> SQL_LOGIN<br /><br /> SERVER_ROLE<br /><br /> EXTERNAL_LOGIN<br /><br /> EXTERNAL_GROUP<br />|  
|**is_disabled**|**int**|1 = Login is disabled.|  
|**create_date**|**datetime**|Time at which the principal was created.|  
|**modify_date**|**datetime**|Time at which the principal definition was last modified.|  
|**default_database_name**|**sysname**|Default database for this principal.|  
|**default_language_name**|**sysname**|Default language for this principal.|  
|**credential_id**|**int**|ID of a credential associated with this principal. If no credential is associated with this principal, credential_id will be NULL.|  
|**owning_principal_id**|**int**|The **principal_id** of the owner of a server role. NULL if the principal is not a server role.|  
|**is_fixed_role**|**bit**|Returns 1 if the principal is one of the built-in server roles with fixed permissions. For more information, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md).|  
  
## Permissions  
 Any login can see their own login name, the system logins, and the fixed server roles. Only members of the server role **##MS_LoginManager##** or the special database role loginmanager in `master` or the Azure AD admin and server Admin can see all logins.
 
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
    
> [!NOTE]  
>  The permissions of fixed server roles do not appear in sys.server_permissions.
  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)  
  
  
