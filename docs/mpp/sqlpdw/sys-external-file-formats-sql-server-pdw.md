---
title: "sys.external_file_formats (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7ad38969-3e44-4417-8156-31968f3768eb
caps.latest.revision: 4
author: BarbKess
---
# sys.external_file_formats (SQL Server PDW)
Contains a row for each external file format in SQL Server PDW.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|file_format_id|**int**|Object ID for the external file format.||  
|name|**sysname**|Name of the file format. This is unique for the server.||  
|format_type|**tinyint**|The file format type.|DELIMITEDTEXT or RCFILE|  
|field_terminator|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the field terminator.||  
|string_delimiter|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the string delimiter.||  
|date_format|**nvarchar(50)**|For format_type = DELIMITEDTEXT, this is the user-defined date and time format.||  
|use_type_default|**bit**|Specifies how to handle missing values when PolyBase is importing data from HDFS text files into SQL Server PDW.|0 – store missing values as the string 'NULL'.<br /><br />1 – store missing values as the column default value.|  
|serde_method|**nvarchar(255)**|For format_type – RCFILE, this is the serialization/deserialization method.||  
|row_terminator|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the character string that terminates each row in the external Hadoop file.|Always '\n'.|  
|encoding|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the encoding method for the external Hadoop file.|Always 'UTF8'.|  
|data_compression|**nvarchar(255)**|For format_type = DELIMITEDTEXT, this is the data compression method.|Always NULL.<br /><br />Not supported in this release.|  
  
## See Also  
[CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-external-file-format-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
