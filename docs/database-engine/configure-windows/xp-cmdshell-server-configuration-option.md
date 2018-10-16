---
title: "xp_cmdshell Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xp_cmdshell"
ms.assetid: c147c9e1-b81d-49c8-b800-3019f4d86a13
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# xp_cmdshell Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The **xp_cmdshell** option is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] server configuration option that enables system administrators to control whether the **xp_cmdshell** extended stored procedure can be executed on a system. By default, the **xp_cmdshell** option is disabled on new installations. Before enabling this option, it is important to consider the potential security implications associated with the use of this option. Newly-developed code should not use this option as it should generally be left disabled. Some legacy applications require it to be enabled, and if they cannot be modified to avoid the use of this option, it can be enabled by using the Policy-Based Management or by running the **sp_configure** system stored procedure as shown in the following code example:  
  
```  
-- To allow advanced options to be changed.  
EXEC sp_configure 'show advanced options', 1;  
GO  
-- To update the currently configured value for advanced options.  
RECONFIGURE;  
GO  
-- To enable the feature.  
EXEC sp_configure 'xp_cmdshell', 1;  
GO  
-- To update the currently configured value for this feature.  
RECONFIGURE;  
GO  
```  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
