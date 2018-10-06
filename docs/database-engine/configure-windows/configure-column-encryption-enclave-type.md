---
title: "Column encryption enclave type Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: sql
ms.prod_service: security
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
manager: craigg
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# column encryption enclave type Server Configuration Option
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

  Use the **column encryption enclave type** option to enable or disable a secure enclave for Always Encrypted.  For more information, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

 The following table lists the possible **column encryption enclave type** values:  
  
|Value|Description|  
|-------------------|-----------------|  
|0|**No secure enclave**. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will not initialize the secure enclave for Always Encrypted. As a result, the functionality of Always Encrypted with secure enclaves will not be available.|  
|1|**Virtualization based security (VBS)**. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will initialize the secure enclave (a VBS secure memory enclave) for Always Encrypted.|    

> [!IMPORTANT]
> Changes to the **column encryption enclave type** do not take affect until you restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
  
   
## Examples  
 The following example enables the secure enclave:  

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
  
  
