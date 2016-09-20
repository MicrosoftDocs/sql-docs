---
title: "None of the Existing Columns Are a Good Choice for the Distribution Column (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7d75b6fc-f2fe-4bab-96c3-97b1becd9881
caps.latest.revision: 4
author: BarbKess
---
# None of the Existing Columns Are a Good Choice for the Distribution Column (SQL Server PDW)
This article describes best practices for choosing a distribution column when none of the distributed table columns seem to be a good choice. This applies to distribution tables for SQL Server PDW.  
  
## Best Practices  
Sometimes none of the columns in the table seem to be a good choice for the distribution columns. That’s okay. These are recommendations for choosing the distribution column when that happens.  
  
### 1. Look for less obvious columns, like those with a timestamp.  
If you cannot find an “obvious” uniform column to distribute on, then look for less obvious columns like those with a timestamp. While it is true that we want to be careful about distributing on a chronological column (such as DATE), timestamps that go down to the millisecond have proven to be excellent candidates for distribution because in the business/scientific world data arrive in a continuous flow. They already exist in the table and can easily be tested with no change to ETL/ELT. However, remember that this strategy will probably result in distribution-incompatible joins.  
  
### 2. Consider creating a new column that combines two columns  
If there are no obvious columns and you don’t have a good timestamp column, then consider introducing a new column that combines two columns together to provide a uniform distribution (a composite distribution). This will require loading a sample amount of data into the “original table” and then performing data analysis, but it won’t take you very long to determine if you can merge two columns into a distribution column. Be careful not to make your composite column too large since distributed tables are the largest in the system, you don’t want to take up too much space. Also be sure to choose columns that cannot be updated since you won’t be able to easily redistribute the rows that change (you cannot update a distribution column). Obviously, this strategy will affect your ETL/ELT processing, but the changes need not be very intrusive and are easily implemented.  
  
### 3. Consider using a unique key or increasing integer  
If no other effective distribution keys are present in a table that provide an even distribution of data, consider using a unique key (if one exists) or even an increasing integer value associated with each row, generated during data loading operations.  
  
You can introduce an integer column in the ETL/ELT process or by using a ROW_NUMBER function.  This guarantees a uniform distribution, but remember all queries will result in distribution-incompatible joins. You can test your hypothesis by using CTAS and the ROW_NUMBER function to create a sample table, run a representative sampling of the customer’s queries and see what the DSQL plans look like (i.e. look at the number of shufflemove steps and drill into them to see the number of rows re-distributed per distribution).  
  
### 4. As a final resort, consider redesigning the schema  
As a final resort, consider redesigning the schema entirely to make it more MPP friendly. I can honestly say that, in all my travels in the last 7 years, I’ve only had to do this once, but the client had an impossibly bad design to begin with.  
  
## See Also  
[Minimize Data Skew When Choosing a Distribution Column &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/minimize-data-skew-when-choosing-a-distribution-column-sql-server-pdw.md)  
[Minimize Data Movement When Choosing a Distribution Column &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/minimize-data-movement-when-choosing-a-distribution-column-sql-server-pdw.md)  
  
