---
title: "sp_xp_cmdshell_proxy_account (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_xp_cmdshell_proxy_account"
  - "sp_xp_cmdshell_proxy_account_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_xp_cmdshell_proxy_account"
  - "xp_cmdshell"
ms.assetid: f807c373-7fbc-4108-a2bd-73b48a236003
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_xp_cmdshell_proxy_account (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates a proxy credential for **xp_cmdshell**.  
  
> [!NOTE]  
>  **xp_cmdshell** is disabled by default. To enable **xp_cmdshell**, see [xp_cmdshell Server Configuration Option](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_xp_cmdshell_proxy_account [ NULL | { 'account_name' , 'password' } ]  
```  
  
## Arguments  
 NULL  
 Specifies that the proxy credential should be deleted.  
  
 *account_name*  
 Specifies a Windows login that will be the proxy.  
  
 *password*  
 Specifies the password of the Windows account.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 The proxy credential will be called **##xp_cmdshell_proxy_account##**.  
  
 When it is executed using the NULL option, **sp_xp_cmdshell_proxy_account** deletes the proxy credential.  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Examples  
  
### A. Creating the proxy credential  
 The following example shows how to create a proxy credential for a Windows account called `ADVWKS\Max04` with password `ds35efg##65`.  
  
```  
EXEC sp_xp_cmdshell_proxy_account 'ADVWKS\Max04', 'ds35efg##65';  
GO  
```  
  
### B. Dropping the proxy credential  
 The following example removes the proxy credential from the credential store.  
  
```  
EXEC sp_xp_cmdshell_proxy_account NULL;  
GO  
```  
  
## See Also  
 [xp_cmdshell &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-cmdshell-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)  
  
  
