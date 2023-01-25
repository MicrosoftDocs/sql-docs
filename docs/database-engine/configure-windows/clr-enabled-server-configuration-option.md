---
title: "clr enabled Server Configuration Option"
description: Learn how to use the clr enabled option to specify whether SQL Server can run user assemblies. See when common language runtime execution is not supported.
author: rwestMSFT
ms.author: randolphwest
ms.date: "01/14/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "assemblies [CLR integration], verifying can run"
  - "clr enabled option"
---
# clr enabled Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the `clr enabled` option to specify whether user assemblies can be run by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The `clr enabled` option provides the following values: 
  
|Value|Description|  
|-----------|-----------------|  
|0|Assembly execution not allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|1|Assembly execution allowed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
For WOW64 only: restart WOW64 servers to apply these changes. No restart required for other server types.  

When you run RECONFIGURE, and the run value of the `clr enabled` option is changed from 1 to 0, all application domains containing user assemblies are immediately unloaded.  

> [!IMPORTANT]
>  **Common language runtime (CLR) execution is not supported under lightweight pooling** Disable one of two options: "clr enabled" or "lightweight pooling". Features that rely upon CLR and that do not work properly in fiber mode include the **hierarchyid** data type, the `FORMAT` function, replication, and Policy-Based Management. For more information, see [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md)  

 
> [!WARNING]
>  CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssql17](../../includes/sssql17-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this is not recommended. Microsoft recommends that all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators can also add assemblies to a list of assemblies, which the Database Engine should trust. For more information, see [sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md).

> [!NOTE]
> Though the `clr enabled` configuration option is enabled in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], developing CLR user functions are not supported in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

## Example  
 The following example first displays the current setting of the `clr enabled` option and then enables the option by setting the option value to 1. To disable the option, set the value to 0.  
  
```sql  
EXEC sp_configure 'clr enabled';  
EXEC sp_configure 'clr enabled' , '1';  
RECONFIGURE;    
```  
  
## Next steps

 - [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 - [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   

  
  
