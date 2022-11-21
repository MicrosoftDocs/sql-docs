---
title: "SMO and DMO XPs Server Configuration Option"
description: "Learn how to enable SQL Server Management Object (SMO) extended stored procedures on a server. View information on the SMO and DMO XPs configuration option."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---
# SMO and DMO XPs Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the SMO and DMO XPs option to enable [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Object (SMO) extended stored procedures on this server.  
  
 Note than beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], DMO has been removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The possible values are described in the following table:  
  
|Value|Meaning|  
|-----------|-------------|  
|0|SMO XPs are not available.|  
|1|SMO XPs are available. This is the default.|  
  
 The setting takes effect immediately.  
  
## Examples  
 The following example enables SMO extended stored procedures.  
  
```  
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'SMO and DMO XPs', 1;  
GO  
RECONFIGURE  
GO  
```  
  
## See Also  
 [SQL Server Management Objects &#40;SMO&#41; Programming Guide](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md)  
  
  
