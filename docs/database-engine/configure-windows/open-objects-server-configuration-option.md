---
title: "open objects Server Configuration Option"
description: "Learn about the disabled configuration option open objects. See how SQL Server now manages the number of open database objects."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "open objects option"
---
# open objects Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This option is still present in **sp_configure**, although its functionality has been disabled in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. (The setting has no effect.) In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the number of open database objects is managed dynamically and is limited only by the available memory. The **open objects** option available in **sp_configure** for backward compatibility with existing scripts.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
  
