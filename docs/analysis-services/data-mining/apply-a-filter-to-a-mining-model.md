---
title: "Apply a Filter to a Mining Model | Microsoft Docs"
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
# Apply a Filter to a Mining Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  If your mining structure contains a nested table, you can apply a filter to the case table, the nested table, or both.  
  
 The following procedure demonstrates how to create both kinds of filters: case filters, and filters on the nested table rows.  
  
 The condition on the case table restricts customers to those with income between 30000 and 40000. The condition on the nested table restricts the customers to those who did not purchase a particular item.  
  
 The complete filter condition created in this example is as follows:  
  
```  
[Income] > '30000'   
AND  [Income] < '40000'   
AND EXISTS (SELECT * FROM [<nested table name>]   
WHERE [Model] <> 'Water Bottle' )   
```  
  
### To create a case filter on a mining model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in Solution Explorer, click the mining structure that contains the mining model you want to filter.  
  
2.  Click the **Mining Models** tab.  
  
3.  Select the model, and right-click to open the shortcut menu.  
  
     -or-  
  
     Select the model. Then, on the **Mining Model** menu, select **Set Model Filter**.  
  
4.  In the **Model Filter** dialog box, click the top row in the grid, in the **Mining Structure Column** text box.  
  
5.  If the data source contains a single flat table, the drop-down list displays only the names of the columns in that table.  
  
     If the mining structure contains multiple tables, the list shows the names of the source tables. The column names do not display until a table has been selected.  
  
     If the mining structure contains a case table and a nested table, the drop-down list shows columns from the case table, and the name of the nested table.  
  
6.  Select a column from the drop-down list.  
  
     The icon at the left side of the text box changes to indicate that the selected item is a table or a column.  
  
7.  Click the **Operator** text box and select an operator from the list. The valid operators change depending on the data type of the column you selected.  
  
8.  Click the **Value** text box, and type a value in the box.  
  
     For example, select **Income** as the column, select the greater than operator (>), and then type **30000**.  
  
9. Click the next row in the grid.  
  
     The filter condition that you created is automatically added to the Expression text box. For example, `[Income] > '30000'`  
  
10. Click the **AND/OR** text box in the next row of the grid to add a condition.  
  
     For example, to create a BETWEEN condition, select **AND** from the drop-down list of logical operands.  
  
11. Select an operator and type a value as described in Steps 7 and 8.  
  
     For example, select **Income** as the column again, select the less than operator (<), and then type **40000**.  
  
12. Click the next row in the grid.  
  
13. The filter condition in the Expression text box is automatically updated to include the new condition. The completed expression is as follows: `[Income] > '30000'AND [Income] < '40000'`  
  
### To add a filter on the nested table in a mining model  
  
1.  In the **\<name>Model Filter** Dialog box, click an empty row in the grid under **Mining Structure Column**.  
  
2.  Select the name of the nested table from the drop-down list.  
  
     The icon at the left side of the text box changes to indicate that the selected item is the name of a table.  
  
3.  Click the **Operator** text box and select **Contains** or **Not Contain**.  
  
     These are the only conditions available for the nested table in the **Model Filter** dialog box, because you are restricting the case table to only those cases that contain a certain value in the nested table. You will set the value for the condition on the nested table in the next step.  
  
4.  Click the **Value** box, and then click the **(...)** button to build an expression.  
  
     The **\<name>Filter** dialog box opens. This dialog box can set conditions only on the current table, which in this case is the nested table.  
  
5.  Click the **Mining Structure Column** box and select a column name from the dropdown lists of nested table columns.  
  
6.  Click **Operator** and select an operator from the list of valid operators for the column.  
  
7.  Click **Value** and type a value.  
  
     For example, for **Mining Structure Column,** select **Model**. For **Operator**, select **<>**, and type the value **Water Bottle**. This condition creates the following filter expression:  
  
```  
EXISTS (SELECT * FROM [<nested table name>] WHERE [Model] <> 'Water Bottle' )   
```  
  
> [!NOTE]  
>  Because the number of nested table attributes is potentially unlimited, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] does not supply a list of possible values from which to select. You must type the exact value. Also, you cannot use a LIKE operator in a nested table.  
  
1.  Add more conditions as necessary, combining the conditions by selecting **AND** or **OR** in the **AND/OR** box at the left side of the **Conditions** grid. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
2.  In the **Model Filter** dialog box, review the conditions that you created by using the **Filter** dialog box. The conditions for the nested table are appended to the conditions for the case table, and the complete set of filter conditions is displayed in the **Expression** text box.  
  
3.  Optionally, click **Edit Query** to manually change the filter expression.  
  
    > [!NOTE]  
    >  If you change any part of a filter expression manually, the grid will be disabled and thereafter you must work with the filter expression in text edit mode only. To restore grid editing mode, you must clear the filter expression and start over.  
  
## See Also  
 [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md)   
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)   
 [Delete a Filter from a Mining Model](../../analysis-services/data-mining/delete-a-filter-from-a-mining-model.md)  
  
  
