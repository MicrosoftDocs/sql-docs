---
title: "DROP STATISTICS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: db9df17a-f912-4597-853d-a624c97bd8b1
caps.latest.revision: 36
author: BarbKess
---
# DROP STATISTICS (SQL Server PDW)
Removes and deletes query optimization statistics from a SQL Server PDW table, including an external table.  
  
For more information, see [Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md).  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP STATISTICS [ schema_name . ] table_name.statistics_name   
[;]  
```  
  
## Arguments  
[ *schema_name* . ] *table_name*  
The one- or two-part name of the table that contains the statistics to drop.  
  
*statistics_name*  
The name of the statistics to drop.  
  
## Permissions  
Requires ALTER permission on the table.  
  
## Performance  
Dropping statistics can affect the query execution plan chosen by the query optimizer. This could have a negative impact on query performance.  
  
## Examples  
  
### A. Dropping statistics from a table  
The following examples drop the `CustomerStats1` statistics from table `Customer`.  
  
```  
DROP STATISTICS Customer.CustomerStats1;  
DROP STATISTICS dbo.Customer.CustomerStats1;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE STATISTICS &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-statistics-sql-server-pdw.md)  
[UPDATE STATISTICS &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/update-statistics-sql-server-pdw.md)  
  
