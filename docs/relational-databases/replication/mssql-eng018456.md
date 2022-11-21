---
description: "MSSQL_ENG018456"
title: "MSSQL_ENG018456 | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG018456 error"
ms.assetid: 3daa8144-d81f-445a-b6c3-4bb3e9fd1526
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG018456
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|18456|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Login failed for user '%.*ls'.%.\*ls|  
  
## Explanation  
 Error MSSQL_ENG018456 is raised whenever a login attempt fails. If the error message includes the account **distributor_admin** (Login failed for user 'distributor_admin'.), the issue is with an account used by replication. Replication creates a remote server, **repl_distributor**, which allows communication between the Distributor and Publisher. The login **distributor_admin** is associated with this remote server and must have a valid password.  
  
## User Action  
 Ensure that you have specified a password for this account. For more information, see [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
