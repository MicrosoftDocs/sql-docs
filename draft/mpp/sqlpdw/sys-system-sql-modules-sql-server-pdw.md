---
title: "sys.system_sql_modules (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3024153d-380a-43f7-99be-1b7a09c3c037
caps.latest.revision: 11
author: BarbKess
---
# sys.system_sql_modules (SQL Server PDW)
Returns SQL Server system modules.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|object_id|**int**|ID of the object of the containing object. Is unique within a database.||  
|definition|**nvarchar(max)**|SQL text that defines this module.||  
|uses_ansi_nulls|**bit**|Module was created with SET ANSI_NULLS ON.<br /><br />Will always be = 0 for rules and defaults.||  
|uses_quoted_identifier|**bit**|Module was created with SET QUOTED_IDENTIFIER ON.||  
|is_schema_bound|**bit**|Module was created with SCHEMABINDING option.||  
|uses_database_collation|**bit**||Always 0.|  
|is_recompiled|**bit**|Procedure was created WITH RECOMPILE option.|Always 0.|  
|null_on_null_input|**bit**|Module was declared to produce a NULL output on any NULL input.|Always 0.|  
|execute_as_principal_id|**int**||Always NULL.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
