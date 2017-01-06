---
title: "sys.assembly_modules (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8729d9d3-c97d-43ab-b36f-c0f257ff2b7e
caps.latest.revision: 5
author: BarbKess
---
# sys.assembly_modules (SQL Server PDW)
Returns one row for each function, or procedure that is defined by a common language runtime (CLR) assembly. This catalog view maps CLR stored procedures, or CLR functions to their underlying implementation. Objects of type TA, AF, PC, FS, and FT have an associated assembly module. To find the association between the object and the assembly, you can join this catalog view to other catalog views. For example, when you create a CLR stored procedure, it is represented by one row in **sys.objects**, one row in **sys.procedures** (which inherits from **sys.objects**), and one row in **sys.assembly_modules**. The stored procedure itself is represented by the metadata in **sys.objects** and **sys.procedures**. References to the procedure’s underlying CLR implementation are found in **sys.assembly_modules**.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**object_id**|**int**|Object identification number of the SQL object. Is unique within a database.|  
|**assembly_id**|**int**|ID of the assembly from which this module was created.|  
|**assembly_class**|**sysname**|Name of the class within the assembly that defines this module.|  
|**assembly_method**|**sysname**|Name of the method within the **assembly_class** that defines this module.<br /><br />NULL for aggregate functions (AF).|  
|**null_on_null_input**|**bit**|Module was declared to produce a NULL output for any NULL input.|  
|**execute_as_principal_id**|**int**|ID of the database principal under which the context execution occurs, as specified by the EXECUTE AS clause of the CLR function, OR stored procedure.<br /><br />NULL = EXECUTE AS CALLER. This is the default.<br /><br />ID of the specified database principal = EXECUTE AS SELF, EXECUTE AS *user_name*, or EXECUTE AS *login_name*.<br /><br />-2 = EXECUTE AS OWNER.|  
  
## Permissions  
The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission. For more information, see [Metadata Visibility Configuration](http://msdn.microsoft.com/en-us/library/ms187113.aspx).  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[sys.all_sql_modules &#40;SQL Server PDW&#41;](../sqlpdw/sys-all-sql-modules-sql-server-pdw.md)  
  
