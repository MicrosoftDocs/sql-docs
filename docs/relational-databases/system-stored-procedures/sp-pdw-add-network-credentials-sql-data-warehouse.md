---
title: "sp_pdw_add_network_credentials"
titleSuffix: Azure Synapse Analytics
description: "sp_pdw_add_network_credentials (Azure Synapse Analytics)"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/14/2017"
ms.service: sql
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest"
---
# sp_pdw_add_network_credentials (Azure Synapse Analytics)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  This stores network credentials in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and associates them with a server. For example, use this stored procedure to give [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] appropriate read/write permissions to perform database backup and restore operations on a target server, or to create a backup of a certificate used for TDE.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
sp_pdw_add_network_credentials 'target_server_name',  'user_name', 'password'  
```  
> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments  
 '*target_server_name*'  
 Specifies the target server host name or IP address. [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] will access this server by using the username and password credentials passed to this stored procedure.  
  
 To connect through the InfiniBand network, use the InfiniBand IP address of the target server.  
  
 *target_server_name* is defined as nvarchar(337).  
  
 '*user_name*'  
 Specifies the user_name that has permissions to access the target server. If credentials already exist for the target server, they will be updated to the new credentials.  
  
 *user_name* is defined as nvarchar (513).  
  
 '*password*ꞌ  
 Specifies the password for *user_name*.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Requires **ALTER SERVER STATE** permission.  
  
## Error Handling  
 An error occurs if adding credentials does not succeed on the Control node and all Compute nodes.  
  
## General Remarks  
 This stored procedure adds network credentials to the NetworkService account for [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. The NetworkService account runs each instance of SMP [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the Control node and the Compute nodes. For example, when a backup operation runs, the Control node and each Compute node will use the NetworkService account credentials to gain read and write permission to the target server.  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Add credentials for performing a database backup  
 The following example associates the user name and password credentials for the domain user seattle\david with a target server that has an IP address of 10.172.63.255. The user seattle\david has read/write permissions to the target server. [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] will store these credentials and use them to read and write to and from the target server, as necessary for backup and restore operations.  
  
```sql  
EXEC sp_pdw_add_network_credentials '10.172.63.255', 'seattle\david', '********';  
```  
  
 The backup command requires that the server name be entered as an IP address.  
  
> [!NOTE]  
>  To perform the database backup over InfiniBand, be sure to use the InfiniBand IP address of the backup server.  
  
## See Also  
 [sp_pdw_remove_network_credentials &#40;Azure Synapse Analytics&#41;](../../relational-databases/system-stored-procedures/sp-pdw-remove-network-credentials-sql-data-warehouse.md)  
  
  

