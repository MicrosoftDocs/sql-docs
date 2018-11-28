---
title: "Creating Predictions (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: a8410ed2-bb98-4d51-a9eb-b239be1201c2
author: minewiskan
ms.author: owend
manager: craigg
---
# Creating Predictions (Basic Data Mining Tutorial)
  After you have tested the accuracy of your mining models and decided that you are satisfied with the results, you can then generate predictions by using the Prediction Query Builder on the **Mining Model Prediction** tab in the Data Mining Designer.  
  
 The Prediction Query Builder has three views. With the **Design** and **Query** views, you can build and examine your query. You can then run the query and view the results in the **Result** view.  
  
 All prediction queries use DMX, which is short for the Data Mining Extensions (DMX) language. DMX has syntax like that of T-SQL but is used for queries against data mining objects. Although DMX syntax is not complicated, using a query builder like this one, or the one in the [SQL Server Data Mining Add-Ins for Office](../../2014/analysis-services/data-mining/sql-server-data-mining-add-ins-for-office.md), makes it much easier to select inputs and build expressions, so we highly recommend that you learn the basics.  
  
## Creating the Query  
 The first step in creating a prediction query is to select a mining model and input table.  
  
#### To select a model and input table  
  
1.  On the **Mining Model Prediction** tab of Data Mining Designer, in the **Mining Model** box, click **Select Model**.  
  
2.  In the **Select Mining Model** dialog box, navigate through the tree to the **Targeted Mailing** structure, expand the structure, select `TM_Decision_Tree`, and then click **OK**.  
  
3.  In the **Select Input Table(s)** box, click **Select Case Table**.  
  
4.  In the **Select Table** dialog box, in the **Data Source** list, select the data source view [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)].  
  
5.  In **Table/View Name**, select the **ProspectiveBuyer (dbo)** table, and then click **OK**.  
  
     The `ProspectiveBuyer` table most closely resembles the **vTargetMail** case table.  
  
## Mapping the Columns  
 After you select the input table, Prediction Query Builder creates a default mapping between the mining model and the input table, based on the names of the columns. At least one column from the structure must match a column in the external data.  
  
> [!IMPORTANT]  
>  The data that you use to determine the accuracy of the models must contain a column that can be mapped to the predictable column. If such a column does not exist, you can create one with empty values, but it must have the same data type as the predictable column.  
  
#### To map the inputs to the model  
  
1.  Right-click the lines connecting the **Mining Model** window to the **Select Input Table** window, and select **Modify Connections**.  
  
     Notice that not every column is mapped. We will add mappings for several **Table Columns**. We will also generate a new birth date column based on the current date column, so that the columns match better.  
  
2.  Under **Table Column**, click the `Bike Buyer` cell and select ProspectiveBuyer.Unknown from the dropdown.  
  
     This maps the predictable column, [Bike Buyer], to an input table column.  
  
3.  Click **OK**.  
  
4.  In **Solution Explorer**, right-click the **Targeted Mailing** data source view and select **View Designer**.  
  
5.  Right-click the table, ProspectiveBuyer, and select **New Named Calculation**.  
  
6.  In the **Create Named Calculation** dialog box, for **Column name**, type `calcAge`.  
  
7.  For **Description**, type **Calculate age based on birthdate**.  
  
8.  In the **Expression** box, type `DATEDIFF(YYYY,[BirthDate],getdate())` and then click **OK**.  
  
     Because the input table has no **Age** column corresponding to the one in the model, you can use this expression to calculate customer age from the BirthDate column in the input table. Since **Age** was identified as the most influential column for predicting bike buying, it must exist in both the model and in the input table.  
  
9. In Data Mining Designer, select the **Mining Model Prediction** tab and re-open the **Modify Connections** window.  
  
10. Under **Table Column**, click the **Age** cell and select ProspectiveBuyer.calcAge from the dropdown.  
  
    > [!WARNING]  
    >  If you do not see the column in the list, you might have to refresh the definition of the data source view that is loaded in the designer. To do this, from the **File** menu, select **Save all**, and then close and re-open the project in the designer.  
  
11. Click **OK**.  
  
## Designing the Prediction Query  
  
1.  The first button on the toolbar of the **Mining Model Prediction** tab is the **Switch to design view / Switch to result view / Switch to query view** button. Click the down arrow on this button, and select **Design**.  
  
2.  In the grid on the **Mining Model Prediction** tab, click the cell in the first empty row in the **Source** column, and then select **Prediction Function**.  
  
3.  In the **Prediction Function** row, in the **Field** column, select `PredictProbability`.  
  
     In the **Alias** column of the same row, type **Probability of result**.  
  
4.  From the **Mining Model** window above, select and drag [Bike Buyer] into the **Criteria/Argument** cell.  
  
     When you let go, [TM_Decision_Tree].[Bike Buyer] appears in the **Criteria/Argument** cell.  
  
     This specifies the target column for the `PredictProbability` function. For more information about functions, see [Data Mining Extensions &#40;DMX&#41; Function Reference](/sql/dmx/data-mining-extensions-dmx-function-reference).  
  
5.  Click the next empty row in the **Source** column, and then select TM_Decision_Tree mining model**.**  
  
6.  In the `TM_Decision_Tree` row, in the **Field** column, select `Bike Buyer`.  
  
7.  In the `TM_Decision_Tree` row, in the **Criteria/Argument** column, type `=1`.  
  
8.  Click the next empty row in the **Source** column, and then select **ProspectiveBuyer table**.  
  
9. In the `ProspectiveBuyer` row, in the **Field** column, select **ProspectiveBuyerKey**.  
  
     This adds the unique identifier to the prediction query so that you can identify who is and who is not likely to buy a bicycle.  
  
10. Add five more rows to the grid. For each row, select **ProspectiveBuyer table** as the **Source** and then add the following columns in the **Field** cells:  
  
    -   calcAge  
  
    -   LastName  
  
    -   FirstName  
  
    -   AddressLine1  
  
    -   AddressLine2  
  
 Finally, run the query and browse the results.  
  
 The **Prediction Query Builder** also includes these controls:  
  
-   **Show** check box  
  
     Lets you remove clauses from the query without having to delete them from the designer. This can be useful when you are working with complex queries and want to preserve syntax without having to copy and paste DMX into the window.  
  
-   **Group**  
  
     Inserts an opening (left) parentheses at the beginning of the selected line, or inserts a closing (right) parenthesis at the end of the current line.  
  
-   **AND/OR**  
  
     Inserts the  `AND` operator or the `OR` operator immediately after the current function or column.  
  
#### To run the query and view results  
  
1.  In the **Mining Model Prediction** tab, select the **Result** button.  
  
2.  After the query runs and the results are displayed, you can review the results.  
  
     The **Mining Model Prediction** tab displays contact information for potential customers who are likely to be bike buyers. The **Probability of result** column indicates the probability of the prediction being correct. You can use these results to determine which potential customers to target for the mailing.  
  
3.  At this point, you can save the results. You have three options:  
  
    -   Right-click a row of data in the results, and select **Copy** to save just that value (and the column heading) to the Clipboard.  
  
    -   Right-click any row in the results, and select **Copy All** to copy the entire result set, including column headings, to the Clipboard.  
  
    -   Click **Save query result** to save the results directly to a database as follows:  
  
        1.  In the **Save Data Mining Query Result** dialog box, select a data source, or define a new data source.  
  
        2.  Type a name for the table that will contain the query results.  
  
        3.  Use the option, **Add to DSV**, to create the table and add it to an existing data source view. This is useful if you want to keep all related tables for a model-such as training data, prediction source data, and query results-in the same data source view.  
  
        4.  Use the option, **Overwrite if exists**, to update an existing table with the latest results.  
  
             You must use the option to overwrite the table if you have added any columns to the prediction query, changed the names or data types of any columns in the prediction query, or if you have run any ALTER statements on the destination table.  
  
             Also, if multiple columns have the same name (for example, the default column name **Expression**) you must create an alias for the columns with duplicate names, or an error will be raised when the designer tries to save the results to SQL Server. The reason is that SQL Server does not allow multiple columns to have the same name.  
  
             For more information, see [Save Data Mining Query Result Dialog Box &#40;Mining Model Prediction View&#41;](../../2014/analysis-services/save-data-mining-query-result-dialog-box-mining-model-prediction-view.md).  
  
## Next Task in Lesson  
 [Using Drillthrough on Structure Data &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/using-drillthrough-on-structure-data-basic-data-mining-tutorial.md)  
  
## See Also  
 [Create a Prediction Query Using the Prediction Query Builder](../../2014/analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md)  
  
  
