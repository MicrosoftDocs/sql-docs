---
title: "MSSQLSERVER_15517"
description: "MSSQLSERVER_15517"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "15517 (Database Engine error)"
---
# MSSQLSERVER_15517
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|15517|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SEC_CANNOTEXECUTEASUSER|  
|Message Text|Cannot execute as the database principal because the principal "*principal*" does not exist, this type of principal cannot be impersonated, or you do not have permission.|  
  
## User Action  
Use the name of an existing principal or get the IMPERSONATE permission on that principal.  
  
15517 can also occur after performing an attach and restore of a database by someone other than the original database owner. To resolve this error, change the db_owner to a login on your server, by running the following command:  
  
```  
ALTER AUTHORIZATION ON DATABASE:: DBName TO [NewLogin]  
```  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
