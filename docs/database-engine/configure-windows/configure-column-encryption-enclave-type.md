---
title: "Configure the enclave type for Always Encrypted Server Configuration Option"
description: Find out how to enable or disable a secure enclave for Always Encrypted. Learn how to confirm whether an enclave has been correctly initialized.
author: jaszymas
ms.author: jaszymas
ms.date: "01/15/2021"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
monikerRange: ">= sql-server-ver15"
---
# Configure the enclave type for Always Encrypted Server Configuration Option

[!INCLUDE [sqlserver2019-windows-only](../../includes/applies-to-version/sqlserver2019-windows-only.md)]

This article describes how to enable or disable a secure enclave for Always Encrypted with secure enclaves. For more information, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md) and [Configure the secure enclave in SQL Server](../../relational-databases/security/encryption/always-encrypted-enclaves-configure-enclave-type.md).

The **column encryption enclave type** Server Configuration Option controls the type of a secure enclave used for Always Encrypted. The option can be set to one of the following values:  
  
|Value|Description|  
|-------------------|-----------------| 
|0|**No secure enclave**. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will not initialize the secure enclave for Always Encrypted. As a result, the functionality of Always Encrypted with secure enclaves will not be available.|  
|1|**Virtualization based security (VBS)**. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will attempt to initialize a virtualization-based security (VBS) enclave.

> [!IMPORTANT]
> Changes to the **column encryption enclave type** do not take effect until you restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
   
You can check the configured enclave type value and the enclave type value currently in effect by using the [sys.configurations (Transact-SQL)](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) view. 

To confirm an enclave of the type (greater than 0) that is currently in effect has been correctly initialized after the last restart of 
[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], check the [sys.dm_column_encryption_enclave (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-column-encryption-enclave.md) view:
 - If the view contains exactly one row, the enclave is correctly initialized. 
 - If the view contains no rows, check the SQL Server error log for enclave initialization errors - see [View the SQL Server error log (SQL Server Management Studio)](../../relational-databases/performance/view-the-sql-server-error-log-sql-server-management-studio.md).

For step-by-step instructions on how to configure a VBS enclave, see [Enable Always Encrypted with secure enclaves in SQL Server](../../relational-databases/security/tutorial-getting-started-with-always-encrypted-enclaves.md#step-3-enable-always-encrypted-with-secure-enclaves-in-sql-server).

## Examples  
 The following example enables the secure enclave and sets the enclave type to VBS:

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

The following query retrieves the configured enclave type and the enclave type that is currently in effect:

```sql  
USE [master];
GO
SELECT
[value]
, CASE [value] WHEN 0 THEN 'No enclave' WHEN 1 THEN 'VBS' ELSE 'Other' END AS [value_description]
, [value_in_use]
, CASE [value_in_use] WHEN 0 THEN 'No enclave' WHEN 1 THEN 'VBS' ELSE 'Other' END AS [value_in_use_description]
FROM sys.configurations
WHERE [name] = 'column encryption enclave type'; 
```  
## Next Steps
 [Manage keys for Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md)

## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [sys.configurations (Transact-SQL)](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)   
 [sys.dm_column_encryption_enclave (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-column-encryption-enclave.md)   
