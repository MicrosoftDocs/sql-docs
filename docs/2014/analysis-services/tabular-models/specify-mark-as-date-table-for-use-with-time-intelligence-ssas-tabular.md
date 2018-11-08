---
title: "Specify Mark as Date Table for use with Time Intelligence (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 30841d1f-0c3b-4575-8f4a-27a1492e248c
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify Mark as Date Table for use with Time Intelligence (SSAS Tabular)
  In order to use time intelligence functions in DAX formulas, you must specify a date table and a unique identifier (datetime) column of the Date data type. Once a column in the date table is specified as a unique identifier, you can create relationships between columns in the date table and any fact tables.  
  
 When using time intelligence functions, the following rules apply:  
  
-   When using DAX time intelligence functions, never specify a datetime column from a fact table. Always create a separate date table in your model with at least one datetime column of Date data type and with unique values.  
  
-   Make sure your date table has a continuous date range.  
  
-   The datetime column in the date table should be at day granularity (without fractions of a day).  
  
-   You must specify a date table and a unique identifier column by using the **Mark the Date Table** dialog box.  
  
-   Create relationships between fact tables and columns of Date data type in the date table.  
  
#### To specify a date table and unique identifier  
  
1.  In the model designer, click the date table.  
  
2.  Click the **Table** menu, then click **Date**, and then click **Mark as Date Table**  
  
3.  In the **Mark as Date Table** dialog box, in the **Date** listbox, select a column to be used as a unique identifier. This column must contain unique values and should be of Date data type. For example:  
  
    |Date|  
    |----------|  
    |7/1/2010 12:00:00 AM|  
    |7/2/2010 12:00:00 AM|  
    |7/3/2010 12:00:00 AM|  
    |7/4/2010 12:00:00 AM|  
    |7/5/2010 12:00:00 AM|  
  
4.  If necessary, create any relationships between fact tables and the date table.  
  
## See Also  
 [Calculations &#40;SSAS Tabular&#41;](calculations-ssas-tabular.md)   
 [Time Intelligence Functions &#40;DAX&#41;](https://msdn.microsoft.com/library/ee634763.aspx)  
  
  
