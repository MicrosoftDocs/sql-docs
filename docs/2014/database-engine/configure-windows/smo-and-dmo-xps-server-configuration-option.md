---
title: "SMO and DMO XPs Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: bcd945ba-5d81-4124-9a2b-d87491c2a369
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SMO and DMO XPs Server Configuration Option
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
  
  
