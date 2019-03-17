---
title: "server trigger recursion Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "recursive triggers [SQL Server]"
  - "triggers [SQL Server], recursive"
  - "server trigger recursion option"
ms.assetid: da4c25f5-d04c-4951-a3db-409e71a1b468
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# server trigger recursion Server Configuration Option
  Use the **server trigger recursion** option to specify whether to allow server-level triggers to fire recursively. When this option is set to 1 (ON), server-level triggers will be allowed to fire recursively. When set to 0 (OFF), server-level triggers cannot be fired recursively. Only direct recursion is prevented when the server trigger recursion option is set to 0 (OFF). (To disable indirect recursion, set the **nested triggers** option to 0.) The default value for this option is 1 (ON). The setting takes effect immediately (without a server restart).  
  
 For more information, see [Create Nested Triggers](../../relational-databases/triggers/create-nested-triggers.md).  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
