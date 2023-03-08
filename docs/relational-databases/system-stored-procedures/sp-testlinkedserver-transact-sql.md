---
title: "sp_testlinkedserver (Transact-SQL)"
description: "sp_testlinkedserver (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_testlinkedserver"
  - "sp_testlinkedserver_TSQL"
helpviewer_keywords:
  - "sp_testlinkedserver"
dev_langs:
  - "TSQL"
---
# sp_testlinkedserver (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Tests the connection to a linked server. If the test is unsuccessful the procedure raises an exception with the reason of the failure.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_testlinkedserver [ @servername ] = servername  
```  
  
## Arguments  
`[ @servername = ]servername`
 Is the name of the linked server. *servername* is **sysname**, with no default value.  
  
## Result Sets  
 None  
  
## Permissions  
 No permissions are checked; however, the caller must have the appropriate login mapping.  
  
## Examples  
 The following example creates a linked server named `SEATTLESales`, and then tests the connection.  
  
```  
USE master;  
GO  
EXEC sp_addlinkedserver   
    'SEATTLESales',  
    N'SQL Server';  
GO  
sp_testlinkedserver SEATTLESales;  
GO  
```  
  
## See Also  
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sp_addlinkedsrvlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md)  
  
  
