---
title: "sp_pdw_add_network_credentials (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6ff78159-4668-4f6e-908a-e46eb650ce87
caps.latest.revision: 9
author: BarbKess
---
# sp_pdw_add_network_credentials (SQL Server PDW)
This stores network credentials in SQL Server PDW and associates them with a server. For example, use this stored procedure to give SQL Server PDW appropriate read/write permissions to perform database backup and restore operations on a target server, or to create a backup of a certificate used for TDE.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_pdw_add_network_credentials 'target_server_name',  'user_name', ꞌpasswordꞌ  
```  
  
## Arguments  
'*target_server_name*'  
Specifies the target server host name or IP address. SQL Server PDW will access this server by using the username and password credentials passed to this stored procedure.  
  
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
This stored procedure adds network credentials to the NetworkService account for SQL Server PDW. The NetworkService account runs each instance of SMP SQL Server on the Control node and the Compute nodes. For example, when a backup operation runs, the Control node and each Compute node will use the NetworkService account credentials to gain read and write permission to the target server.  
  
## Examples  
  
### A. Add credentials for performing a database backup  
The following example associates the user name and password credentials for the domain user seattle\david with a target server that has an IP address of 10.172.63.255. The user seattle\david has read/write permissions to the target server. SQL Server PDW will store these credentials and use them to read and write to and from the target server, as necessary for backup and restore operations.  
  
```  
EXEC sp_pdw_add_network_credentials '10.172.63.255', 'seattle\david', '********';  
```  
  
The backup command requires that the server name be entered as an IP address.  
  
> [!NOTE]  
> To perform the database backup over InfiniBand, be sure to use the InfiniBand IP address of the backup server.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[BACKUP DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/backup-database-sql-server-pdw.md)  
[sp_pdw_remove_network_credentials &#40;SQL Server PDW&#41;](../sqlpdw/sp-pdw-remove-network-credentials-sql-server-pdw.md)  
  
