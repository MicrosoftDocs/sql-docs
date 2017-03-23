---
title: "server trigger recursion Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "recursive triggers [SQL Server]"
  - "triggers [SQL Server], recursive"
  - "server trigger recursion option"
ms.assetid: da4c25f5-d04c-4951-a3db-409e71a1b468
caps.latest.revision: 24
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# server trigger recursion Server Configuration Option
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Use the **server trigger recursion** option to specify whether to allow server-level triggers to fire recursively. When this option is set to 1 (ON), server-level triggers will be allowed to fire recursively. When set to 0 (OFF), server-level triggers cannot be fired recursively. Only direct recursion is prevented when the server trigger recursion option is set to 0 (OFF). (To disable indirect recursion, set the **nested triggers** option to 0.) The default value for this option is 1 (ON). The setting takes effect immediately (without a server restart).  
  
 For more information, see [Create Nested Triggers](../../relational-databases/triggers/create-nested-triggers.md).  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
