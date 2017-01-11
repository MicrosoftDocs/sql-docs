---
title: "sys.external_data_sources (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d1978455-2606-4168-9c95-87f80b244865
caps.latest.revision: 5
author: BarbKess
---
# sys.external_data_sources (SQL Server PDW)
Contains a row for each external data source in SQL Server PDW.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|data_source_id|**int**|Object ID for the external data source.||  
|name|**sysname**|Name of the external data source. This is unique for the server.||  
|location|**nvarchar(4000)**|The connection string, which includes the protocol, IP address, and port for the external data source..||  
|type_desc|**nvarchar(255)**|Data source type displayed as a string.|Hadoop or RDBMS|  
|type|**tinyint**|Data source type displayed as a number.|0 - Hadoop|  
|job_tracker_location|**nvarchar(4000)**|IP and port location of the Hadoop job tracker. This is used for submitting a job on the external data source.||  
  
## See Also  
[CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
