---
title: "SWITCHOFFSET (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 78b26d0e-1c7f-42e9-9680-3acfe9706ea5
caps.latest.revision: 6
author: BarbKess
---
# SWITCHOFFSET (SQL Server PDW)
Returns a **datetimeoffset** value that is changed from the stored time zone offset to a specified new time zone offset.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SWITCHOFFSET (DATETIMEOFFSET,time_zone)  
```  
  
## Arguments  
*DATETIMEOFFSET*  
Is an expression that can be resolved to a **datetimeoffset(n)** value.  
  
*time_zone*  
Is a character string in the format [+|-]TZH:TZM or a signed integer (of minutes) that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted.  
  
## Return Type  
**datetimeoffset** with the fractional precision of the *DATETIMEOFFSET* argument.  
  
## Remarks  
Use SWITCHOFFSET to select a **datetimeoffset** value into a time zone offset that is different from the time zone offset that was originally stored. SWITCHOFFSET does not update the stored *time_zone* value.  
  
SWITCHOFFSET can be used to update a **datetimeoffset** column.  
  
Using SWITCHOFFSET with the function GETDATE() can cause the query to run slowly. This is because the query optimizer is unable to obtain accurate cardinality estimates for the datetime value.  
  
## Examples  
The following example uses `SWITCHOFFSET` to display a different time zone offset than the value stored in the database.  
  
```  
CREATE TABLE dbo.test   
    (  
    ColDatetimeoffset datetimeoffset  
    );  
GO  
INSERT INTO dbo.test   
VALUES ('1998-09-20 7:45:50.71345 -5:00');  
GO  
SELECT SWITCHOFFSET (ColDatetimeoffset, '-08:00')   
FROM dbo.test;  
GO  
--Returns: 1998-09-20 04:45:50.7134500 -08:00  
SELECT ColDatetimeoffset  
FROM dbo.test;  
--Returns: 1998-09-20 07:45:50.7134500 -05:00  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
