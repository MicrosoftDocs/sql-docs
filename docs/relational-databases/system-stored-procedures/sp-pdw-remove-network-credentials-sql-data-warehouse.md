---
title: "sp_pdw_remove_network_credentials (SQL Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.service: sql-data-warehouse
ms.subservice: design
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: c12696a2-5939-402b-9866-8a837ca4c0a3
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sp_pdw_remove_network_credentials (SQL Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  This removes network credentials stored in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] to access a network file share. For example, use this stored procedure to remove permission for [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] to perform backup and restore operations on a server that resides within your own network.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
sp_pdw_remove_network_credentials 'target_server_name'  
```  
  
## Arguments  
 '*target_server_name*'  
 Specifies the target server host name or IP address. Credentials to access this server will be removed from [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. This does not change or remove any permissions on the actual target server which is managed by your own team.  
  
 *target_server_name* is defined as nvarchar(337).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Requires **ALTER SERVER STATE** permission.  
  
## Error Handling  
 An error occurs if removing credentials does not succeed on the Control node and all Compute nodes.  
  
## General Remarks  
 This stored procedure removes network credentials from the NetworkService account for [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. The NetworkService account runs each instance of SMP [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the Control node and the Compute nodes. For example, when a backup operation runs, the Control node and each Compute node will use the NetworkService account credentials to access the target server.  
  
## Metadata  
 To list all credentials and to verify the credentials have been removed, use [sys.dm_pdw_network_credentials &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-network-credentials-transact-sql.md).  
  
 To add credentials, use [sp_pdw_add_network_credentials &#40;SQL Data Warehouse&#41;](../../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md).  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Remove credentials for performing a database backup  
 The following example removes user name and password credentials for accessing the target server which has an IP address of 10.192.147.63.  
  
```  
EXEC sp_pdw_remove_network_credentials '10.192.147.63';  
```  
  
  

