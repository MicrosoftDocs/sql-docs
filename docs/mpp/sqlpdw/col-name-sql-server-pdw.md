---
title: "COL_NAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 77ff4bb4-4453-433c-9fac-145258acf5be
caps.latest.revision: 11
author: BarbKess
---
# COL_NAME (SQL Server PDW)
Returns the name of a column from a specified corresponding table identification number and column identification number. For more information, see the [COL_NAME (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms174974.aspx) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
COL_NAME (table_id,column_id )  
```  
  
## Arguments  
*table_id*  
The identification number of the table that contains the column. *table_id* is of type **int**.  
  
*column_id*  
The identification number of the column. *column_id* parameter is of type **int**.  
  
## Return Types  
**sysname**  
  
## Error Handling  
Returns NULL on error or if a caller does not have permission to view the object.  
  
A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as COL_NAME may return NULL if the user does not have any permission on the object.  
  
## Remarks  
The *table_id* and *column_id* parameters together produce a column name string.  
  
For more information about obtaining table and column identification numbers, see OBJECT_ID.  
  
## Examples  
The following example returns the name of the first column in a sample `Employee` table.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT COL_NAME(OBJECT_ID('dbo.FactResellerSales'), 1) AS FirstColumnName,  
COL_NAME(OBJECT_ID('dbo.FactResellerSales'), 2) AS SecondColumnName;  
```  
  
Here is the result set.  
  
```  
ColumnName          
------------   
BusinessEntityID  
```  
  
