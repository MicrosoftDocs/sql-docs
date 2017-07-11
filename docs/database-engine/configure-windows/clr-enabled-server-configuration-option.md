---
title: "clr enabled Server Configuration Option | Microsoft Docs"
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
  - "assemblies [CLR integration], verifying can run"
  - "clr enabled option"
ms.assetid: 0722d382-8fd3-4fac-b4a8-cd2b7a7e0293
caps.latest.revision: 36
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# clr enabled Server Configuration Option
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Use the clr enabled option to specify whether user assemblies can be run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The clr enabled option provides the following values: 
  
|Value|Description|  
|-----------|-----------------|  
|0|Assembly execution not allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|1|Assembly execution allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
WOW64 only. Restart WOW64 servers to effect the settings changes. No restart required for other server types.  

When you run RECONFIGURE, and the run value of the clr enabled option is changed from 1 to 0, all application domains containing user assemblies are immediately unloaded.  
  
>  **Common language runtime (CLR) execution is not supported under lightweight pooling** Disable one of two options: "clr enabled" or "lightweight pooling. Features that rely upon CLR and that do not work properly in fiber mode include the **hierarchy** data type, replication, and Policy-Based Management.  
  
## Example  
 The following example first displays the current setting of the clr enabled option and then enables the option by setting the option value to 1. To disable the option, set the value to 0.  
  
```tsql  
EXEC sp_configure 'clr enabled';  
EXEC sp_configure 'clr enabled' , '1';  
RECONFIGURE;    
```  
  
## See Also  
 [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md)  
  
  
