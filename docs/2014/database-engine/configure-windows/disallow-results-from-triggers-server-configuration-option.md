---
title: "disallow results from triggers Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "triggers [SQL Server], result sets"
  - "result sets [SQL Server], triggers"
  - "disallow results from triggers option"
ms.assetid: 47149073-307d-47a5-b7d2-66a737d3231d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# disallow results from triggers Server Configuration Option
  Use the **disallow results from triggers** option to control whether triggers return result sets. Triggers that return result sets may cause unexpected behavior in applications that are not designed to work with them.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepNextDontUse](../../includes/ssnotedepnextdontuse-md.md)] We recommend that you set this value to 1.  
  
 When set to 1, the **disallow results from triggers** option is set to ON. The default setting for this option is 0 (OFF). If this option is set to 1 (ON), any attempt by a trigger to return a result set fails, and the user receives the following error message:  
  
 "Msg 524, Level 16, State 1, Procedure \<Procedure Name>, Line \<Line#>  
  
 "A trigger returned a resultset and the server option 'disallow_results_from_triggers' is true."  
  
 The **disallow results from triggers** option is applied at the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance level, and it will determine behavior for all existing triggers within the instance.  
  
 The **disallow results from triggers** option is an advanced option. If you are using the **sp_configure** system stored procedure to change the setting, you can change disallow results from triggers only when **show advanced options** is set to 1. The setting takes effect immediately without a server restart.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
