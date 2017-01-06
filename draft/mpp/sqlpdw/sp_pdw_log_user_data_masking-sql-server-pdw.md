---
title: "sp_pdw_log_user_data_masking (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f21bb123-397b-4334-987a-1ab58d9dabed
caps.latest.revision: 10
author: BarbKess
---
# sp_pdw_log_user_data_masking (SQL Server PDW)
Use **sp_pdw_log_user_data_masking** to enable user data masking in PDW activity logs. User data masking affects the statements on all databases on the appliance.  
  
> [!IMPORTANT]  
> The PDW activity logs affected by **sp_pdw_log_user_data_masking** are certain PDW activity logs. **sp_pdw_log_user_data_masking** does not affect database transaction logs, or SQL Server error logs.  
  
**Background:** In the default configuration PDW activity logs contain full Transact\-SQL statements, and can in some cases include user data contained in operations such as **INSERT**, **UPDATE**, and **SELECT** statements. In case of a problem on the appliance, this permits the analysis of the conditions that caused the problem without a need to reproduce the issue. In order to prevent the user data from being written to PDW activity logs, customers can choose to turn on the user data masking by using this stored procedure. The statements will still be written to PDW activity logs, but all the literals in statements that may contain user data will be masked; replaced with some predefined constant values.  
  
When transparent data encryption is enabled on the appliance, masking of the user data in PDW activity logs is automatically turned on. For more information about TDE, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
## Syntax  
  
```  
sp_pdw_log_user_data_masking [ [ @masking_mode = ] value ] ;  
```  
  
#### Parameters  
[ **@masking_mode=** ] *masking_mode*  
Determines whether transparent data encryption log user data masking is enabled. *masking_mode* is **int**, and can be one of the following values:  
  
-   0 = Disabled, user data appears in the PDW activity logs.  
  
-   1 = Enabled, user data statements appear in the PDW activity logs but the user data is masked.  
  
-   2 = Statements containing user data are not written to the PDW activity logs.  
  
Executing **sp_pdw_ log_user_data_masking** without parameters returns the current state of TDE log user data masking on the appliance as a scalar result set.  
  
## Remarks  
User data masking in PDW activity logs enables replacement of literals with predefined constant values in **SELECT** and DML statements, as they can contain user data. Setting *masking_mode* to 1 does not mask metadata, such as column names or table names. Setting *masking_mode* to 2 removes statements with metadata, such as column names or table names.  
  
User data masking in PDW activity logs is implemented in the following way:  
  
-   TDE and user data masking in PDW activity logs are turned off by default. The statements will not be automatically masked if database encryption is not enabled on the appliance.  
  
-   Enabling TDE on the appliance automatically turns on the user data masking in PDW activity logs.  
  
-   Disabling TDE does not affect user data masking in PDW activity logs.  
  
-   You can explicitly enable user data masking in PDW activity logs by using the **sp_pdw_log_user_data_masking** procedure.  
  
## Permissions  
Requires membership in the **sysadmin** fixed database role, or **CONTROL SERVER** permission.  
  
## Example  
The following example enables TDE log user data masking on the appliance.  
  
```  
EXEC sp_pdw_log_user_data_masking 1;  
```  
  
## See Also  
[sp_pdw_database_encryption &#40;SQL Server PDW&#41;](../sqlpdw/sp-pdw-database-encryption-sql-server-pdw.md)  
[sp_pdw_database_encryption_regenerate_system_keys &#40;SQL Server PDW&#41;](../sqlpdw/sp-pdw-database-encryption-regenerate-system-keys-sql-server-pdw.md)  
  
