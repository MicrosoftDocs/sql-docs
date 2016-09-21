---
title: "sys.filegroups (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 89d86300-52a4-4c3f-8850-b8db089ae414
caps.latest.revision: 10
author: BarbKess
---
# sys.filegroups (SQL Server PDW)
Contains a row for each data space that is a filegroup.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|<inherited columns from data spaces>|**--**|Information not available.||  
|filegroup_guid|**uniqueidentifier**|Information not available.||  
|log_filegroup_id|**int**|Identified for informational purposes only. Not supported.|Returns NULL.|  
|is_read_only|**bit**|1 = Filegroup is read-only.<br /><br />0 = Filegroup is read/write|Returns 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
