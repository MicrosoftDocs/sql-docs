---
title: "sys.dm_operation_status (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "06/05/2017"
ms.prod: ""
ms.prod_service: "sql-database, sql-data-warehouse"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_operation_status_TSQL"
  - "dm_operation_status"
  - "sys.dm_operation_status"
  - "sys.dm_operation_status_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dm_operation_status dynamic management view"
  - "sys.dm_operation_status dynamic management view"
ms.assetid: cc847784-7f61-4c69-8b78-5f971bb24d61
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.dm_operation_status (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md)]

  Returns information about operations performed on databases in a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] server.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|session_activity_id|**uniqueidentifier**|ID of the operation. Not null.|  
|resource_type|**int**|Denotes the type of resource on which the operation is performed. Not null. In the current release, this view tracks operations performed on [!INCLUDE[ssSDS](../../includes/sssds-md.md)] only, and the corresponding integer value is 0.|  
|resource_type_desc|**nvarchar(2048)**|Description of the resource type on which the operation is performed. In the current release, this view tracks operations performed on [!INCLUDE[ssSDS](../../includes/sssds-md.md)] only.|  
|major_resource_id|**sql_variant**|Name of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] on which the operation is performed. Not Null.|  
|minor_resource_id|**sql_variant**|For internal use only. Not null.|  
|operation|**nvarchar(60)**|Operation performed on a [!INCLUDE[ssSDS](../../includes/sssds-md.md)], such as CREATE or ALTER.|  
|state|**tinyint**|The state of the operation.<br /><br /> 0 = Pending<br />1 = In progress<br />2 = Completed<br />3 = Failed<br />4 = Cancelled|  
|state_desc|**nvarchar(120)**|PENDING = operation is waiting for resource or quota availability.<br /><br /> IN_PROGRESS = operation has started and is in progress.<br /><br /> COMPLETED = operation completed successfully.<br /><br /> FAILED = operation failed. See the **error_desc** column for details.<br /><br /> CANCELLED = operation stopped at the request of the user.|  
|percent_complete|**int**|Percentage of operation that has completed. Values are not continuous and the valid values are listed below. Not NULL.<br/><br/>0 = Operation not started<br/>50 = Operation in progress<br/>100 = Operation complete|  
|error_code|**int**|Code indicating the error that occurred during a failed operation. If the value is 0, it indicates that the operation completed successfully.|  
|error_desc|**nvarchar(2048)**|Description of the error that occurred during a failed operation.|  
|error_severity|**int**|Severity level of the error that occurred during a failed operation. For more information about error severities, see [Database Engine Error Severities](https://go.microsoft.com/fwlink/?LinkId=251052).|  
|error_state|**int**|Reserved for future use. Future compatibility is not guaranteed.|  
|start_time|**datetime**|Timestamp when the operation started.|  
|last_modify_time|**datetime**|Timestamp when the record was last modified for a long running operation. In case of successfully completed operations, this field displays the timestamp when the operation completed.|  
  
## Permissions  
 This view is only available in the **master** database to the server-level principal login.  
  
## Remarks  
 To use this view, you must be connected to the **master** database. Use the `sys.dm_operation_status` view in the **master** database of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server to track the status of the following operations performed on a [!INCLUDE[ssSDS](../../includes/sssds-md.md)]:  
  
-   Create database  
  
-   Copy database. Database Copy creates a record in this view on both the source and target servers.  
  
-   Alter database  
  
-   Change the performance level of a service tier  
  
-   Change the service tier of a database, such as changing from Basic to Standard.  
  
-   Setting up a Geo-Replication relationship  
  
-   Terminating a Geo-Replication relationship  
  
-   Restore database  
  
-   Delete database  
  
## Example  
 Show most recent geo-replication operations associated with database 'mydb'.  
  
```  
SELECT * FROM sys.dm_ operation_status   
   WHERE major_resource_id = 'myddb'   
   ORDER BY start_time DESC;  
```  
  
## See Also  
 [Geo-Replication Dynamic Management Views and Functions &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)   
 [sys.dm_geo_replication_link_status &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md)   
 [sys.geo_replication_links &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-geo-replication-links-azure-sql-database.md)   
 [ALTER DATABASE &#40;Azure SQL Database&#41;](../../t-sql/statements/alter-database-azure-sql-database.md)  
  
  
