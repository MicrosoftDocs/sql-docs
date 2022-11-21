---
title: "disallow results from triggers Server Configuration Option"
description: "Learn about the 'disallow results from triggers' option. See how it can prevent problems in applications that aren't designed to work with result sets."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "triggers [SQL Server], result sets"
  - "result sets [SQL Server], triggers"
  - "disallow results from triggers option"
---
# disallow results from triggers Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the **disallow results from triggers** option to control whether triggers return result sets. Triggers that return result sets may cause unexpected behavior in applications that are not designed to work with them.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you set this value to 1.  
  
 When set to 1, the **disallow results from triggers** option is set to ON. The default setting for this option is 0 (OFF). If this option is set to 1 (ON), any attempt by a trigger to return a result set fails, and the user receives the following error message:  
  
 "Msg 524, Level 16, State 1, Procedure \<Procedure Name>, Line \<Line#>  
  
 "A trigger returned a resultset and the server option 'disallow_results_from_triggers' is true."  
  
 The **disallow results from triggers** option is applied at the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance level, and it will determine behavior for all existing triggers within the instance.  
  
 The **disallow results from triggers** option is an advanced option. If you are using the **sp_configure** system stored procedure to change the setting, you can change disallow results from triggers only when **show advanced options** is set to 1. The setting takes effect immediately without a server restart.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
