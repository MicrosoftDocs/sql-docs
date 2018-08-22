---
title: "Column encryption enclave type Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "08/22/2018"
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

  Use the **column encryption enclave type** option to enable or disable a secure enclave for Always Encrypted.  For more information, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

 The following table lists the possible **column encryption enclave type** values:  
  
|Outcome value|Description|  
|-------------------|-----------------|  
|0|**No secure enclave**. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will not attempt to initialize the secure enclave for Always Encrypted on the next restart. As a result, the functionality of Always Encrypted with secure enclaves will not be available.|  
|1|**Virtualization based security (VBS)**. After the next restart, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will attempt to initialize the secure enclave (a VBS secure memory enclave) for Always Encrypted.|    

> [!NOTE]
> Changing the **column encryption enclave type** requires a restart of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
  
   
## Examples  
 The following example enables the secure enclave of the VBS type:  

```sql  
sp_configure 'column encryption enclave type', 1;  
GO  
RECONFIGURE;  
GO  
```  

The following example disables the secure enclave:  

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
  
  
