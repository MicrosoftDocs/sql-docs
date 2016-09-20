---
title: "sp_pdw_database_encryption_regenerate_system_keys (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 41db74b7-c986-4cb0-8253-7cf31e4d08e9
caps.latest.revision: 6
author: BarbKess
---
# sp_pdw_database_encryption_regenerate_system_keys (SQL Server PDW)
Use **sp_pdw_database_encryption_regenerate_system_keys** to rotate the certificate and database encryption key for internal databases that are encrypted when TDE is enabled on the appliance. This includes `tempdb`. This will succeed only if TDE is enabled.  
  
## Syntax  
  
```Transact-SQL  
sp_pdw_database_encryption_regenerate_system_keys  ;  
```  
  
## Return Code Values  
**0** (success) or **1** (failure)  
  
## Remarks  
The procedure has no parameters.  
  
This procedure should be used when the traffic in the appliance is low.  
  
## Permissions  
Requires membership in the **sysadmin** fixed database role, or **CONTROL SERVER** permission.  
  
## Example  
The following example regenerates the database encryption keys.  
  
```Transact-SQL  
EXEC sys.sp_pdw_database_encryption_regenerate_system_keys;  
```  
  
## See Also  
[sp_pdw_database_encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-database-encryption-sql-server-pdw.md)  
[sp_pdw_log_user_data_masking &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp_pdw_log_user_data_masking-sql-server-pdw.md)  
  
