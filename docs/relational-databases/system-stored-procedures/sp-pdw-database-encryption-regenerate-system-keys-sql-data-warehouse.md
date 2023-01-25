---
title: sp_pdw_database_encryption_regenerate_system_keys (Azure Synapse Analytics)
description: Use **sp_pdw_database_encryption_regenerate_system_keys** to rotate the certificate and database encryption key for internal databases that are encrypted when TDE is enabled on the appliance. 
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: bb13e323-a984-4462-8b6d-6019c38ddd9d
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest"
---
# sp_pdw_database_encryption_regenerate_system_keys (Azure Synapse Analytics)

[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Use **sp_pdw_database_encryption_regenerate_system_keys** to rotate the certificate and database encryption key for internal databases that are encrypted when TDE is enabled on the appliance. This includes `tempdb`. This will succeed only if TDE is enabled.  
  
## Syntax  
  
```syntaxsql  
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
sp_pdw_database_encryption_regenerate_system_keys  ;  
```  

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 The procedure has no parameters.  
  
 This procedure should be used when the traffic in the appliance is low.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed database role, or **CONTROL SERVER** permission.  
  
## Example  
 The following example regenerates the database encryption keys.  
  
```sql  
EXEC sys.sp_pdw_database_encryption_regenerate_system_keys;  
```  
  
## See also  
 [sp_pdw_database_encryption &#40;Azure Synapse Analytics&#41;](../../relational-databases/system-stored-procedures/sp-pdw-database-encryption-sql-data-warehouse.md)   
 [sp_pdw_log_user_data_masking &#40;Azure Synapse Analytics&#41;](../../relational-databases/system-stored-procedures/sp-pdw-log-user-data-masking-sql-data-warehouse.md)  
  
