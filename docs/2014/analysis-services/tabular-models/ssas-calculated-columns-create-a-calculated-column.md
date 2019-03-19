---
title: "Create a Calculated Column (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.as.daxref.CreataCalculatedColumn.f1"
ms.assetid: 59440510-2d76-41dc-9b55-edc15259f9da
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a Calculated Column (SSAS Tabular)
  Calculated columns allow you to add new data to your model. Instead of pasting or importing values into the column, you create a DAX formula that defines the column's row level values. The values in each row of a calculated column are calculated and populated when you create a valid formula and then click ENTER. The calculated column can then be added to a reporting or analysis application just as would any other column of data. This topic describes how to create a new calculated column by using the DAX formula bar in the model designer.  
  
#### To create a new calculated column  
  
1.  In the model designer, in Data View, select the table to which you want to add a calculated column, then click the **Column** menu, and then click **Add Column**.  
  
     **Add Column** is highlighted over the empty rightmost column, and the cursor moves to the formula bar.  
  
     To create a new column between two existing columns, right-click an existing column, and then click **Insert Column**.  
  
2.  In the formula bar, do one of the following:  
  
    -   Type an equal sign followed by a formula.  
  
    -   Type an equal sign, followed by a DAX function, followed by arguments and parameters as required by the function.  
  
    -   Click the function button (**fx**), then in the **Insert Function** dialog box, select a category and function, and then click **OK**. In the formula bar, type the remaining arguments and parameters as required by the function.  
  
3.  Press ENTER to accept the formula.  
  
     The entire column is populated with the formula, and a value is calculated for each row.  
  
> [!TIP]  
>  You can use DAX Formula AutoComplete in the middle of an existing formula with nested functions. The text immediately before the insertion point is used to display values in the drop-down list, and all of the text after the insertion point remains unchanged.  
  
## See Also  
 [Calculated Columns &#40;SSAS Tabular&#41;](ssas-calculated-columns.md)   
 [Measures &#40;SSAS Tabular&#41;](measures-ssas-tabular.md)  
  
  
