---
title: "Design Pane (Mining Model Prediction View) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.prediction.design.f1"
ms.assetid: 17f24c8d-43cd-4f4d-83b3-a41ee8fbe8e8
author: minewiskan
ms.author: owend
manager: craigg
---
# Design Pane (Mining Model Prediction View)
  The **Design** pane contains the Prediction Query Builder, which you can use to build data mining predictions. You can design prediction queries that use tables of input data from a data source view, to generate bulk predictions, or you can create singleton prediction queries, which let you provide individual values.  
  
 To run the query and view the results, switch to query result view.  
  
 The **Query** view displays the Data Mining Extensions (DMX) query that Prediction Query Builder creates. If you are familiar with DMX, you can customize this query.  
  
> [!NOTE]  
>  If you make any manual changes to the query, your changes will be lost when you switch back to Design view. If you want to save the DMX query, you can copy the query to the Windows Clipboard and then paste it to a text file.  
  
 **For More Information:** [Data Mining Queries](data-mining/data-mining-queries.md)  
  
## Options  
 **Switch to query result view**  
 Click to switch between the **Design**, **Query**, and **Result** panes. Switching to the **Result** pane runs the query.  
  
 **Save the query result**  
 Opens the **Save Data Mining Query Result** dialog box.  
  
 **Singleton query**  
 Enables creating a singleton query, in which you can provide values directly for the query instead of providing a table as the source of the known data. The **Select Input Table(s)** table is replaced by a **Singleton Query Input** table.  
  
 **Refresh query results**  
 Reprocesses the prediction query. This is enabled only in the **Result** pane.  
  
 **Mining Model**  
 Displays the selected mining model on which you want to base predictions.  
  
 **Select Model**  
 Opens the **Select Mining Model** dialog box.  
  
 **Select Input Table(s)**  
 Displays the selected input tables that contain known data on which to base the predictions.  
  
 **Delete Table**  
 Deletes the selected table. This button is disabled if a table has not been selected or does not exist.  
  
 **Select Case Table**  
 Opens the **Select Table** dialog box. This button appears only if a case table has not been selected.  
  
 **Select Nested Table**  
 Opens the **Select Table** dialog box. This button appears only if a case table has been selected. If the associated mining structure does not contain a nested table, this button is disabled.  
  
 **Modify Join**  
 Opens the **Specify Nested Join** dialog box. This button is active only if the nested table is selected.  
  
 **Singleton Query input**  
 Enabled when you select the **Singleton query** button. Contains the following columns:  
  
|Value|Description|  
|-----------|-----------------|  
|**Mining Model Column**|Lists the mining model columns contained within the mining model that is selected in the **Mining Model** table.|  
|**Value**|Select a value from the list that contains each possible state of the selected mining model column.<br /><br /> If the column is a nested table column, clicking the value cell opens the **Nested Table Input** dialog box.|  
  
 **Source**  
 Select the source that contains the field that you will use for the column. You can either use the mining model that is selected in the **Mining Model** table, the input table or tables that are selected in the **Select Input Table(s)** table, a prediction function, or a custom expression.  
  
 Columns can be dragged from the tables containing the mining model and input tables onto the cell.  
  
 **Field**  
 Select a column from the list of columns derived from the source table. If you selected **Prediction Function** in **Source**, this contains the prediction function available for the selected mining model.  
  
 **Group**  
 Use with the **And/Or** column to group expressions together. For example, `(expr1 Or expr2) And expr3`.  
  
 **And/Or**  
 Use to create a logical query. For example, `(expr1 Or expr2) And expr3`.  
  
 **Criteria/Argument**  
 Specify a condition or user expression that applies to the column. Columns can be dragged from the tables containing the mining model and input tables onto the cell.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](/sql/dmx/data-mining-extensions-dmx-statements)   
 [Data Mining Query Interfaces](data-mining/data-mining-query-tools.md)   
 [Prediction Query Builder &#40;Data Mining&#41;](prediction-query-builder-data-mining.md)  
  
  
