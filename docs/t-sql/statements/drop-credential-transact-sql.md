---
title: "DROP CREDENTIAL (Transact-SQL)"
description: DROP CREDENTIAL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/19/2015"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP CREDENTIAL"
  - "DROP_CREDENTIAL_TSQL"
helpviewer_keywords:
  - "removing credentials"
  - "DROP CREDENTIAL statement"
  - "credentials [SQL Server], DROP CREDENTIAL statement"
  - "authentication [SQL Server], credentials"
  - "deleting credentials"
  - "dropping credentials"
dev_langs:
  - "TSQL"
---
# DROP CREDENTIAL (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes a credential from the server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP CREDENTIAL credential_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *credential_name*  
 Is the name of the credential to remove from the server.  
  
## Remarks  
 To drop the secret associated with a credential without dropping the credential itself, use [ALTER CREDENTIAL](../../t-sql/statements/alter-credential-transact-sql.md).  
  
 Information about credentials is visible in the **sys.credentials** catalog view.  
  
> [!WARNING]  
>  Proxies are associated with a credential. Deleting a credential that is used by a proxy leaves the associated proxy in an unusable state. When dropping a credential used by a proxy, delete the proxy (by using [sp_delete_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-proxy-transact-sql.md) and recreate the associated proxy by using [sp_add_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-proxy-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY CREDENTIAL permission. If dropping a system credential, requires CONTROL SERVER permission.  
  
## Examples  
 The following example removes the credential called `Saddles`.  
  
```sql  
DROP CREDENTIAL Saddles;  
GO  
```  
  
## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [ALTER CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-credential-transact-sql.md)   
 [DROP DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-scoped-credential-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)  
  
  
