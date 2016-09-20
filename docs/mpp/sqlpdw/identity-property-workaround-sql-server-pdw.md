---
title: "IDENTITY Property Workaround (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 58ab4f5f-f191-4179-bbd3-623a89b56e5a
caps.latest.revision: 6
author: BarbKess
---
# IDENTITY Property Workaround (SQL Server PDW)
SQL Server PDW does not support the **IDENTITY** property for a column of a table or the sequence number object. The following example demonstrates a workaround that might be useful, depending upon your needs.  
  
## Remarks  
Data entering a large data warehouse is usually fully structured and validated. If a table has an **IDENTITY** column, the values have typically been assigned when the data was first created. The **IDENTITY** value in the table is inherently preserved during the transfer to the data warehouse, the same way any other value is preserved. If an **IDENTITY** value is not present but is needed, either create the source table with an **IDENTITY** column before transferring the table to the data warehouse, or use the following workaround to add a numeric counter column to the data as it is transferred. This method uses an **int** data type, but a **bigint** could be used. This example simulates a seed of 1 and increment of 1.  
  
> [!NOTE]  
> This workaround does not create a true **IDENTITY** value. The column value is not enforced as unique or as an identity column. The column does not have the **IDENTITY** property.  
  
## Examples  
The following example demonstrates a workaround to populate a table with a column that simulates an identity column. The example creates and populates a replicated staging table (`Basic_Stage`) to hold the data that will be transferred. Then the example creates the destination table (`Prod_Identity_Test`). Then the data is copied from the staging to the destination table, using the **ROW_NUMBER** function to add sequential number to the `IDCol` column.  
  
> [!NOTE]  
> The staging table must be replicated to work properly for this example. The destination table can be replicated or distributed.  
  
```  
-- Create and load a sample staging table  
CREATE TABLE Basic_Stage   
(IncomingData int NOT NULL)  
WITH (DISTRIBUTION = REPLICATE);  
  
INSERT INTO basic_stage (IncomingData) Values (1);  
INSERT INTO basic_stage (IncomingData) Values (2);  
INSERT INTO basic_stage (IncomingData) Values (3);  
INSERT INTO basic_stage (IncomingData) Values (4);  
INSERT INTO basic_stage (IncomingData) Values (5);  
SELECT * FROM basic_stage;  
  
--Create a sample production table  
Create Table Prod_Identity_Test   
(IDCol INT NOT NULL,  
OtherCols Varchar(10))  
WITH (DISTRIBUTION=HASH(IDCol));  
  
-- Populate the Prod_Identity_Test table with data   
-- from the Basic_Stage table.  
INSERT INTO Prod_Identity_Test  
SELECT (ROW_NUMBER() OVER ( ORDER BY p.IncomingData)) + maxID.maxID,   
p.IncomingData  
FROM   
    (SELECT IncomingData  FROM  basic_stage) AS p  
CROSS JOIN   
    (Select isnull((max(IDCol)),0) AS maxID   
     FROM Prod_Identity_Test)   
     AS maxID;   
-- The isnull ensures the first value is 1 when no records exist  
  
SELECT * FROM Prod_Identity_Test ORDER BY IDCol;  
```  
  
## See Also  
[ROW_NUMBER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/row-number-sql-server-pdw.md)  
  
