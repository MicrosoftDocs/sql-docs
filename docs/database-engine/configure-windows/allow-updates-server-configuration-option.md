---
title: "allow updates Server Configuration Option"
description: "Learn about the obsolete SQL Server configuration option 'allow updates.' See how using this option will cause RECONFIGURE statements to fail."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "allow updates option"
---
# allow updates Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This option is still present in the **sp_configure** stored procedure, although its functionality is unavailable in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The setting has no effect. Direct updates to the system tables are not supported.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 Changing the **allow updates** option will cause the RECONFIGURE statement to fail. Changes to the **allow updates** option should be removed from all scripts.  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
  
