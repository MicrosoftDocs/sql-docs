---
title: "DROP EXTERNAL FILE FORMAT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f44138d5-87a4-47bd-8ec2-c153ef69428e
caps.latest.revision: 6
author: BarbKess
---
# DROP EXTERNAL FILE FORMAT (SQL Server PDW)
Removes a Hadoop external file format from SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Drop an external file format  
DROP EXTERNAL FILE FORMAT external_file_format_name  
[;]  
```  
  
## Arguments  
*external_file_format_name*  
The name of the external file format to drop.  
  
## Metadata  
To view a list of external file formats use the [sys.external_file_formats &#40;SQL Server PDW&#41;](../sqlpdw/sys-external-file-formats-sql-server-pdw.md) system view.  
  
```  
SELECT * FROM sys.external_file_formats;  
```  
  
## Permissions  
Requires **CONTROL SERVER** permission or ALTER ANY EXTERNAL FILE FORMAT  
  
## General Remarks  
Dropping an external file format does not remove the external data.  
  
## Locking  
Takes a shared lock on the EXTERNALFILEFORMAT object.  
  
## Examples  
  
### A. Using basic syntax  
  
```  
DROP EXTERNAL FILE FORMAT myfileformat;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
