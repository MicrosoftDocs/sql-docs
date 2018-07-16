---
title: "Column encryption enclave type Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "07/13/2018"
ms.prod: sql
ms.prod_service: security
ms.reviewer: ""
ms.suite: "sql"
ms.technology: configuration
ms.tgt_pltfrm: ""
ms.topic: conceptual
caps.latest.revision: 11
author: jaszymas
ms.author: jaszymas
manager: craigg
---
# column encryption enclave type Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Use the **column encryption enclave type** option to enable and disable a secure enclave for Always Encrypted in the instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  Please see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md) for more information.

 The following table lists the possible values of the **column encryption enclave type** option.  
  
|Outcome value|Description|  
|-------------------|-----------------|  
|0|No secure enclave. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will not attempt to initialize the secure enclave for Always Encrypted on the next restart. As a result, the functionality of Always Encrypted with secure enclaves will not be available in the [!INCLUDE[ssDE](../../includes/ssde-md.md)].|  
|1|Virtualization based security (VBS). The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will attempt to initialize the secure enclave for Always Encrypted, which is a VBS secure memory enclave, on the next restart.|    

> [!NOTE]
> Reconfiguring the **column encryption enclave type** option requires a restart of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
  
   
## Examples  
 The following example enables the secure enclave of the VBS type in the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
```sql  
sp_configure 'column encryption enclave type', 1;  
GO  
RECONFIGURE;  
GO  
```  
 The following example disables (if it is currently enabled) the secure enclave in the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
```sql  
sp_configure 'column encryption enclave type', 0;  
GO  
RECONFIGURE;  
GO  
```  
  
## See Also  
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
  
