---
title: "sp_OAStop (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_OAStop"
  - "sp_OAStop_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_OAStop"
ms.assetid: aa9eab66-c4f7-4ec7-9f0d-5d24d16da654
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_OAStop (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Stops the server-wide OLE Automation stored procedure execution environment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_OAStop      
```  
  
## Return Code Values  
 0 (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.  
  
 For more information about HRESULT Return Codes, see [OLE Automation Return Codes and Error Information](../../relational-databases/stored-procedures/ole-automation-return-codes-and-error-information.md).  
  
## Remarks  
 A single execution environment is shared by all clients that are using the OLE Automation stored procedures. If one client calls **sp_OAStop** the shared execution environment will be stopped for all clients. After the execution environment has been stopped, any call to **sp_OACreate** restarts the execution environment.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example stops the shared OLE Automation execution environment.  
  
```  
EXEC sp_OAStop;  
GO  
```  
  
## See Also  
 [OLE Automation Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/ole-automation-stored-procedures-transact-sql.md)   
 [OLE Automation Sample Script](../../relational-databases/stored-procedures/ole-automation-sample-script.md)  
  
  
