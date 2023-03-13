---
title: "sp_OAStop (Transact-SQL)"
description: "sp_OAStop (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_OAStop"
  - "sp_OAStop_TSQL"
helpviewer_keywords:
  - "sp_OAStop"
dev_langs:
  - "TSQL"
---
# sp_OAStop (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stops the server-wide OLE Automation stored procedure execution environment.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
 Requires membership in the **sysadmin** fixed server role or execute permission directly on this Stored Procedure. `Ole Automation Procedures` configuration must be **enabled** to use any system procedure related to OLE Automation.  
  
## Examples  
 The following example stops the shared OLE Automation execution environment.  
  
```  
EXEC sp_OAStop;  
GO  
```  
  
## See Also  
 [OLE Automation Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/ole-automation-stored-procedures-transact-sql.md)   
 [OLE Automation Sample Script](../../relational-databases/stored-procedures/ole-automation-sample-script.md)  
  
  
