---
title: "Create a Prediction Query Using the Prediction Query Builder | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create a Prediction Query Using the Prediction Query Builder
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can create prediction queries either while you are building a data mining solution in BI Development Studio, or by right-clicking an existing mining model in SQL Server Management Studio, and then choosing the option, **Build Prediction Query**.  
  
 The **Prediction Query Builder** has the following three design modes, which you can switch among by clicking the icons in the upper-left corner.  
  
-   **Design**  
  
-   **Query**  
  
-   **Result**  
  
 **Design** mode lets you build a prediction query by choosing input data, mapping the data to the model, and then adding prediction functions into statements you build by using the grid. The design grid contains these building blocks:  
  
 **Source**  
 Choose the source of the new column. You can use columns from the mining model, input tables included in the data source view, a prediction function, or a customized expression.  
  
 **Field**  
 Determines the specific column or function that is associated with the selection in the **Source** column.  
  
 **Alias**  
 Determines how the column is to be named in the result set.  
  
 **Show**  
 Determines whether the selection in the **Source** column is displayed in the results.  
  
 **Group**  
 Works with the **And/Or** column to group expressions together by using parentheses. For example, (expr1 or expr2) and expr3.  
  
 **And/Or**  
 Creates logic in the query. For example, (expr1 or expr2) and expr3.  
  
 **Criteria/Argument**  
 Specifies a condition or user expression that applies to the column. You can drag columns from the tables to the cell.  
  
 **Query** mode provides a text editor that gives you direct access to the Data Mining Extensions (DMX) language, along with a view of the input data and model columns. When you select **Query** mode, the grid that you used to define the query is replaced by a basic text editor. You can use this editor to copy and save queries you have composed, or to paste in existing DMX queries and from the Clipboard and run them.  
  
 **Result** view runs the current query and displays the results in a grid. If the underlying data has changed and you want to rerun the query, click the Play button in the status bar  
  
 You can design a data mining query by using a combination of the visual tools and the text editor. If you type changes to the query in the text editor and switch back to the **Design** view, all the changes are lost, and the query reverts to the original query created by Prediction Query Builder This topic walks you through use of the graphical query builder.  
  
### To create a prediction query  
  
1.  Click the **Mining Model Prediction** tab in Data Mining Designer.  
  
2.  Click **Select Model** on the **Mining Model** table.  
  
     The **Select Mining Model** dialog box opens to show all the mining structures that exist in the current project.  
  
3.  Select the model on which you want to create a prediction, and then click **OK**.  
  
4.  On the **Select Input Table(s)** table, click **Select Case Table**.  
  
     The **Select Table** dialog box opens.  
  
5.  In the **Data Source** list, select the data source that contains the data on which to create a prediction.  
  
6.  In the **Table/View Name** box, select the table that contains the data on which to create a prediction, and then click **OK**.  
  
     After you select the input table, Prediction Query Builder creates a default mapping between the mining model and the input table, based on the names of the columns. To delete a mapping, click to select the line that links the column in the **Mining Model** table to the mapped column in the **Select Input Table(s)** table, and then press DELETE. You can also manually create mappings by clicking a column in the **Select Input Table(s)** table and dragging it onto the corresponding column in the **Mining Model** table.  
  
7.  Add any combination of the following three types of information to the Prediction Query Builder grid:  
  
    -   Predictable columns from the **Mining Model** box.  
  
    -   Any combination of input columns from the **Select Input Table(s)** box.  
  
    -   Prediction functions  
  
8.  Run the query by clicking the first button on the toolbar of the **Mining Model Prediction** tab, and then selecting **Result**.  
  
## See Also  
 [Create a Singleton Query in the Data Mining Designer](../../analysis-services/data-mining/create-a-singleton-query-in-the-data-mining-designer.md)   
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)  
  
  
