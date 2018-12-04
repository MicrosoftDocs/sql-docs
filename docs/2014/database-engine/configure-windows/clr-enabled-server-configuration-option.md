---
title: "clr enabled Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "assemblies [CLR integration], verifying can run"
  - "clr enabled option"
ms.assetid: 0722d382-8fd3-4fac-b4a8-cd2b7a7e0293
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# clr enabled Server Configuration Option
  Use the clr enabled option to specify whether user assemblies can be run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The clr enabled option provides the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|0|Assembly execution not allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|1|Assembly execution allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
 WOW64 servers must be restarted before the changes to this setting will take effect. Restart is not required for other server types.  
  
> [!NOTE]  
>  When RECONFIGURE is run and the run value of the clr enabled option is changed from 1 to 0, all application domains containing user assemblies are immediately unloaded.  
  
> [!NOTE]  
>  Common language runtime (CLR) execution is not supported under lightweight pooling. Disable one of two options: "clr enabled" or "lightweight pooling. Features that rely upon CLR and that do not work properly in fiber mode include the `hierarchy` data type, replication, and Policy-Based Management.  
  
## Example  
 The following example first displays the current setting of the clr enabled option and then enables the option by setting the option value to 1. To disable the option, set the value to 0.  
  
```  
EXEC sp_configure 'clr enabled';  
EXEC sp_configure 'clr enabled' , '1';  
RECONFIGURE;  
  
```  
  
## See Also  
 [lightweight pooling Server Configuration Option](lightweight-pooling-server-configuration-option.md)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)   
 [lightweight pooling Server Configuration Option](lightweight-pooling-server-configuration-option.md)  
  
  
