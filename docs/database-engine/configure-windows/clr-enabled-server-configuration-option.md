---
title: "clr enabled Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/20/2017"
ms.prod: sql
ms.prod_service: high-availability
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
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Use the clr enabled option to specify whether user assemblies can be run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The clr enabled option provides the following values: 
  
|Value|Description|  
|-----------|-----------------|  
|0|Assembly execution not allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|1|Assembly execution allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
WOW64 only. Restart WOW64 servers to effect the settings changes. No restart required for other server types.  

When you run RECONFIGURE, and the run value of the clr enabled option is changed from 1 to 0, all application domains containing user assemblies are immediately unloaded.  
  
>  **Common language runtime (CLR) execution is not supported under lightweight pooling** Disable one of two options: "clr enabled" or "lightweight pooling". Features that rely upon CLR and that do not work properly in fiber mode include the **hierarchy** data type, replication, and Policy-Based Management.  
> 
> [!WARNING]
>  CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this is not recommended. Microsoft recommends that all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators can also add assemblies to a list of assemblies, which the Database Engine should trust. For more information, see [sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md).
  
## Example  
 The following example first displays the current setting of the clr enabled option and then enables the option by setting the option value to 1. To disable the option, set the value to 0.  
  
```sql  
EXEC sp_configure 'clr enabled';  
EXEC sp_configure 'clr enabled' , '1';  
RECONFIGURE;    
```  
  
## See Also  
 [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md)  
  
  
