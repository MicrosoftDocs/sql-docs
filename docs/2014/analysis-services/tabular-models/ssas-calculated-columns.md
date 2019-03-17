---
title: "Calculated Columns (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: e1011278-556d-4984-b01d-a37f8a33b304
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculated Columns (SSAS Tabular)
  Calculated columns, in tabular models, allow you to add new data to your model. Instead of pasting or importing values into the column, you create a DAX formula that defines the column's row level values. The calculated column can then be used in a report, PivotTable, or PivotChart as would any other column.  
  
> [!NOTE]  
>  Calculated columns are not supported for tabular models in DirectQuery mode. For more information, see [DirectQuery Mode &#40;SSAS Tabular&#41;](directquery-mode-ssas-tabular.md).  
  
 Sections in this topic:  
  
-   [Benefits](#bkmk_understanding)  
  
-   [Naming a Calculated Column](#bkmk_naming)  
  
-   [Performance of Calculated Columns](#bkmk_perf)  
  
-   [Related Tasks](#bkmk_rel_tasks)  
  
##  <a name="bkmk_understanding"></a> Benefits  
 Formulas in calculated columns are much like formulas in Excel. Unlike Excel, however, you cannot create different formulas for different rows in a table; instead, the DAX formula is automatically applied to the entire column.  
  
 When a column contains a formula, the value is computed for each row. The results are calculated for the column when you enter a valid formula. Column values are then recalculated as necessary, such as when the underlying data is refreshed.  
  
 You can create calculated columns that are based on measures and other calculated columns. For example, you might create one calculated column to extract a number from a string of text, and then use that number in another calculated column.  
  
 A calculated column is based on data that you already have in an existing table, or created by using a DAX formula. For example, you might choose to concatenate values, perform addition, extract substrings, or compare the values in other fields. To add a calculated column, you must have at least one table in your model.  
  
 This example demonstrates a simple formula in a calculated column:  
  
```  
=EOMONTH([StartDate],0])  
  
```  
  
 This formula extracts the month from the StartDate column. It then calculates the end of the month value for each row in the table. The second parameter specifies the number of months before or after the month in StartDate; in this case, 0 means the same month. For example, if the value in the StartDate column is 6/1/2001, the value in the calculated column will be 6/30/2001.  
  
##  <a name="bkmk_naming"></a> Naming a Calculated Column  
 By default, new calculated columns are added to the right of other columns in a table, and the column is automatically assigned the default name of **CalculatedColumn1**, **CalculatedColumn2**, and so forth. You can also right click a column, and then click Insert Column to create a new column between two existing columns. You can rearrange columns within the same table by clicking and dragging, and you can rename columns after they are created; however, you should be aware of the following restrictions on changes to calculated columns:  
  
-   Each column name must be unique within a table.  
  
-   Avoid names that have already been used for measures within the same model. Although it is possible for a measure and a calculated column to have the same name, if names are not unique you can get calculation errors. To avoid accidentally invoking a measure, when referring to a column always use a fully qualified column reference.  
  
-   When you rename a calculated column, any formulas that rely on the column must be updated manually. Unless you are in manual update mode, updating the results of formulas takes place automatically. However, this operation might take some time.  
  
-   There are some characters that cannot be used within the names of columns. For more information, see "Naming Requirements" in [DAX Syntax Specification for PowerPivot](https://msdn.microsoft.com/library/ee634217(v=sql.120).aspx).  
  
##  <a name="bkmk_perf"></a> Performance of Calculated Columns  
 The formula for a calculated column can be more resource-intensive than the formula used for a measure. One reason is that the result for a calculated column is always calculated for each row in a table, whereas a measure is only calculated for the cells defined by the filter used in a report, PivotTable, or PivotChart. For example, a table with a million rows will always have a calculated column with a million results, and a corresponding effect on performance. However, a PivotTable generally filters data by applying row and column headings; therefore, a measure is calculated only for the subset of data in each cell of the PivotTable.  
  
 A formula has dependencies on the objects that are referenced in the formula, such as other columns or expressions that evaluate values. For example, a calculated column that is based on another column, or a calculation that contains an expression with a column reference, cannot be evaluated until the other column is evaluated. By default, automatic refresh is enabled in workbooks; therefore, all such dependencies can affect performance while values are updated and formulas refreshed.  
  
 To avoid performance issues when you create calculated columns, follow these guidelines:  
  
-   Rather than create a single formula that contains many complex dependencies, create the formulas in steps, with results saved to columns, so that you can validate the results and assess performance.  
  
-   Modification of data will often require that calculated columns be recalculated. You can prevent this by setting the recalculation mode to manual; however, if any values in the calculated column are incorrect the column will be grayed out until you refresh and recalculate the data.  
  
-   If you change or delete relationships between tables, formulas that use columns in those tables will become invalid.  
  
-   If you create a formula that contains a circular or self-referencing dependency, an error will occur.  
  
##  <a name="bkmk_rel_tasks"></a> Related Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create a Calculated Column &#40;SSAS Tabular&#41;](ssas-calculated-columns-create-a-calculated-column.md)|Tasks in this topic describe how to add a new calculated column to a table.|  
  
## See Also  
 [Tables and Columns &#40;SSAS Tabular&#41;](tables-and-columns-ssas-tabular.md)   
 [Measures &#40;SSAS Tabular&#41;](measures-ssas-tabular.md)   
 [Calculations &#40;SSAS Tabular&#41;](calculations-ssas-tabular.md)  
  
  
