---
title: "DROP EXTERNAL DATA SOURCE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b1887498-4c24-46b3-9e03-9de5a3a515ff
caps.latest.revision: 5
author: BarbKess
---
# DROP EXTERNAL DATA SOURCE (SQL Server PDW)
Removes a Hadoop external data source from SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Drop an external data source  
DROP EXTERNAL DATA SOURCE external_data_source_name  
[;]  
```  
  
## Arguments  
*external_data_source_name*  
The name of the external data source to drop.  
  
## Metadata  
To view a list of external data sources use the sys.external_data_sources system view.  
  
```  
SELECT * FROM sys.external_data_sources;  
```  
  
## Permissions  
Requires **CONTROL SERVER** permission or ALTER ANY EXTERNAL DATA SOURCE  
  
## Locking  
Takes a shared lock on the EXTERNALDATASOURCE object.  
  
## General Remarks  
Dropping an external data source does not remove the external data.  
  
## Examples  
  
### A. Using basic syntax  
  
```  
DROP EXTERNAL DATA SOURCE mydatasource;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
