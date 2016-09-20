---
title: "sp_pdw_database_encryption (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5011bb7b-1793-4b2b-bd9c-d4a8c8626b6e
caps.latest.revision: 9
author: BarbKess
---
# sp_pdw_database_encryption (SQL Server PDW)
Use **sp_pdw_database_encryption** to enable transparent data encryption on for a SQL Server PDW appliance. When **sp_pdw_database_encryption** set to 1, use the **ALTER DATABASE** statement to encrypt a database by using TDE.  
  
## Syntax  
  
```Transact-SQL  
sp_pdw_database_encryption [ [ @enabled = ] enabled ] ;  
```  
  
#### Parameters  
[ **@enabled=** ] *enabled*  
Determines whether transparent data encryption is enabled. *enabled* is **int**, and can be one of the following values:  
  
-   0 = Disabled  
  
-   1 = Enabled  
  
Executing **sp_pdw_database_encryption** without parameters returns the current state of TDE on the appliance as a scalar result set: 0 for disabled, or 1 for enabled.  
  
## Return Code Values  
**0** (success) or **1** (failure)  
  
## Remarks  
When the TDE is enabled using **sp_pdw_database_encryption**, the tempdb database is dropped, recreated and encrypted. For that reason, the TDE cannot be enabled on an appliance while there are other active sessions using tempdb. Enabling or disabling TDE on an appliance is an action that changes the state of the appliance, in most cases is expected to be performed once in the appliance lifetime, and should be executed when there is no traffic on the appliance.  
  
## Permissions  
Requires membership in the **sysadmin** fixed database role, or **CONTROL SERVER** permission.  
  
## Example  
The following example enables TDE on the appliance.  
  
```Transact-SQL  
EXEC sys.sp_pdw_database_encryption 1;  
```  
  
## See Also  
[sp_pdw_database_encryption_regenerate_system_keys &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-database-encryption-regenerate-system-keys-sql-server-pdw.md)  
[sp_pdw_log_user_data_masking &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp_pdw_log_user_data_masking-sql-server-pdw.md)  
  
