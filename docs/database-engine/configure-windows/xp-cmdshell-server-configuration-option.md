---
title: xp_cmdshell Server configuration option
description: 'Learn about the "xp_cmdshell" option. See how it controls whether SQL Server can run the "xp_cmdshell" extended stored procedure. Find out how to turn it on.'
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "xp_cmdshell"
ms.assetid: c147c9e1-b81d-49c8-b800-3019f4d86a13
author: markingmyname
ms.author: maghan
ms.custom: contperfq4
ms.date: 06/12/2020
---

# xp_cmdshell Server configuration option

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to enable the **xp_cmdshell** SQL Server configuration option. This option allows system administrators to control whether the [xp_cmdshell extended stored procedure](../../relational-databases/system-stored-procedures/xp-cmdshell-transact-sql.md) can be executed on a system. By default, the **xp_cmdshell** option is disabled on new installations.

Before enabling this option, it's important to consider the potential security implications.

- Newly developed code shouldn't use the **xp_cmdshell** stored procedure and generally it should be left disabled.
- Some legacy applications require **xp_cmdshell** to be enabled. If they can't be modified to avoid the use of this stored procedure, you can enable it as described below.

If you need to enable **xp_cmdshell**, you can use [Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md) or run the **sp_configure** system stored procedure as shown in the following code example:  
  
``` sql
-- To allow advanced options to be changed.  
EXECUTE sp_configure 'show advanced options', 1;  
GO  
-- To update the currently configured value for advanced options.  
RECONFIGURE;  
GO  
-- To enable the feature.  
EXECUTE sp_configure 'xp_cmdshell', 1;  
GO  
-- To update the currently configured value for this feature.  
RECONFIGURE;  
GO  
```  
  
## Next steps

- [xp_cmdshell extended stored procedure](../../relational-databases/system-stored-procedures/xp-cmdshell-transact-sql.md)
- [Server Configuration Options (SQL Server)](server-configuration-options-sql-server.md)
- [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
