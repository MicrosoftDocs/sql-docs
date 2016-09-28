---
title: "sys.external_tables (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 59dbf3cc-4fd1-4e55-9a79-45afc0b36067
caps.latest.revision: 7
author: BarbKess
---
# sys.external_tables (SQL Server PDW)
Contains a row for each external table in the current SQL Server PDW database.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|<inherited columns>||For a list of columns that this view inherits, see [sys.objects](../sqlpdw/sys-objects-sql-server-pdw.md).||  
|max_column_id_used|**int**|Maximum column ID for the lifetime of the table.|Always 0.|  
|uses_ansi_nulls|**bit**|Table was created with the SET ANSI_NULLS database option ON.|Always 1 --- true.|  
|data_source_id|**int**|Object ID for the external data source.||  
|file_format_id|**int**|Object ID for the external file format.|NULL if the data source is not a Hadoop data source.|  
|location|**nvarchar(4000)**|File and folder path of the external data in Hadoop.|NULL if the data source is not a Hadoop data source.|  
|reject_type|**tinyint**|The way rejected rows are counted when loading external data.|Value – the number of rejected rows.<br /><br />Percentage – the percentage of rejected rows.|  
|reject_value|**float**|For *reject_type =* value, this is the number of row rejections to allow before failing the load.<br /><br />For *reject_type* = percentage, this is the percentage of row rejections to allow before failing the load.|User-defined.|  
|reject_sample_value|**int**|For *reject_type* = percentage, this is the number of rows to load, either successfully or unsuccessfully, before calculating the percentage of rejected rows.|NULL if reject_type = value.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
