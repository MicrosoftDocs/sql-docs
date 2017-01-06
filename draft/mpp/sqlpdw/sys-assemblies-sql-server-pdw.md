---
title: "sys.assemblies (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c02e5c2b-52b4-4a3e-a7ae-d4df3daf614d
caps.latest.revision: 6
author: BarbKess
---
# sys.assemblies (SQL Server PDW)
Returns a row for each assembly.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**name**|**sysname**|Name of the assembly. Is unique within the database.|  
|**principal_id**|**int**|ID of the principal that owns this assembly.|  
|**assembly_id**|**int**|Assembly identification number. Is unique within a database.|  
|**clr_name**|**nvarchar(4000)**|Canonical string that encodes the simple name, version number, culture, public key, and architecture of the assembly. This value uniquely identifies the assembly on the common language runtime (CLR) side.|  
|**permission_set**|**tinyint**|Permission-set/security-level for assembly.<br /><br />1 = Safe Access<br /><br />2 = External Access<br /><br />3 = Unsafe Access|  
|**permission_set_desc**|**nvarchar(60)**|Description for permission-set/security-level for assembly.<br /><br />SAFE_ACCESS<br /><br />EXTERNAL_ACCESS<br /><br />UNSAFE_ACCESS|  
|**is_visible**|**bit**|1 = Assembly is visible to register Transact\-SQL entry points.<br /><br />0 = Assembly is intended only for managed callers. That is, the assembly provides internal implementation for other assemblies in the database.|  
|**create_date**|**datetime**|Date the assembly was created or registered.|  
|**modify_date**|**datetime**|Date the assembly was modified.|  
|**is_user_defined**|**bit**|Indicates the source of the assembly.<br /><br />0 = System-defined assemblies (such as Microsoft.SqlServer.Types for the **hierarchyid** data type)<br /><br />1 = User-defined assemblies|  
  
## Permissions  
The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
[sys.all_views &#40;SQL Server PDW&#41;](../sqlpdw/sys-all-views-sql-server-pdw.md)  
[sys.assembly_types &#40;SQL Server PDW&#41;](../sqlpdw/sys-assembly-types-sql-server-pdw.md)  
  
