---
title: "sp_pdw_remove_network_credentials (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5d5c35f5-7054-45cd-ac1c-be74e48b005e
caps.latest.revision: 4
author: BarbKess
---
# sp_pdw_remove_network_credentials (SQL Server PDW)
This removes network credentials stored in SQL Server PDW to access a network file share. For example, use this stored procedure to remove permission for SQL Server PDW to perform backup and restore operations on a server that resides within your own network.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_pdw_remove_network_credentials 'target_server_name'  
```  
  
## Arguments  
'*target_server_name*'  
Specifies the target server host name or IP address. Credentials to access this server will be removed from SQL Server PDW. This does not change or remove any permissions on the actual target server which is managed by your own team.  
  
*target_server_name* is defined as nvarchar(337).  
  
## Return Code Values  
0 (success) or 1 (failure)  
  
## Permissions  
Requires **ALTER SERVER STATE** permission.  
  
## Error Handling  
An error occurs if removing credentials does not succeed on the Control node and all Compute nodes.  
  
## General Remarks  
This stored procedure removes network credentials from the NetworkService account for SQL Server PDW. The NetworkService account runs each instance of SMP SQL Server on the Control node and the Compute nodes. For example, when a backup operation runs, the Control node and each Compute node will use the NetworkService account credentials to access the target server.  
  
## Metadata  
To list all credentials and to verify the credentials have been removed, use [sys.dm_pdw_network_credentials &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-network-credentials-sql-server-pdw.md).  
  
To add credentials, use [sp_pdw_add_network_credentials &#40;SQL Server PDW&#41;](../sqlpdw/sp-pdw-add-network-credentials-sql-server-pdw.md).  
  
## Examples  
  
### A. Remove credentials for performing a database backup  
The following example removes user name and password credentials for accessing the target server which has an IP address of 10.192.147.63.  
  
```  
EXEC sp_pdw_remove_network_credentials '10.192.147.63';  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[BACKUP DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/backup-database-sql-server-pdw.md)  
  
