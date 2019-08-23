---
title: "Filtering a Nested Table in a Mining Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 0a3ae0e5-897b-4898-a60d-5455eec3d305
author: minewiskan
ms.author: owend
manager: kfile
---
# Filtering a Nested Table in a Mining Model (Intermediate Data Mining Tutorial)
  After you have created and explored the model, you decide that you want to focus on a subset of the customer data. For example, you might want to analyze only the baskets that contain a specific item, or to analyze the demographics of customers who have not purchased anything in a certain period.  
  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides the ability to filter the data that is used in a mining model. This feature is useful because you do not need to set up a new data source view to use different data. In the Basic Data Mining Tutorial, you learned how to filter data from a flat table by applying conditions to the case table. In this task, you create a filter that applies to a nested table.  
  
## Filters on Nested vs. Case Tables  
 If your data source view contains a case table and a nested table, like the data source view used in the Association model, you can filter on values from the case table, the presence or absence of a value in the nested table, or some combination of both.  
  
 In this task, you will first make a copy of the Association model and then add the IncomeGroup and Region attributes to the new related model, so that you can filter on those attributes in the case table.  
  
#### To create and modify a copy of the Association model  
  
1.  In the **Mining Models** tab of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], right-click the `Association` model, and select **New Mining Model**.  
  
2.  For **Model Name**, type `Association Filtered`. For **Algorithm Name**, select **Microsoft Association Rules**. Click **OK**.  
  
3.  In the column for the Association Filtered model, click the IncomeGroup row and change the value from **Ignore** to **Input**.  
  
 Next, you will create a filter on the case table in the new association model. The filter will pass to the model only the customers in the target region or with the target income level. Then, you will add a second set of filter conditions to specify that the model uses only customers whose shopping baskets contained at least one item.  
  
#### To add a filter to a mining model  
  
1.  In the **Mining Models** tab, right-click the model Association Filtered, and select **Set Model Filter**.  
  
2.  In the **Model Filter** dialog box, click the top row in the grid, in the **Mining Structure Column** text box.  
  
3.  In the **Mining Structure Column** text box, select IncomeGroup.  
  
     The icon at the left side of the text box changes to indicate that the selected item is a column.  
  
4.  Click the **Operator** text box and select the **=** operator from the list.  
  
5.  Click the **Value** text box, and type `High` in the box.  
  
6.  Click the next row in the grid.  
  
7.  Click the **AND/OR** text box in the next row of the grid and select **OR**.  
  
8.  In the **Mining Structure Column** text box, select IncomeGroup. In the **Value** text box, type `Moderate`.  
  
     The filter condition that you created is automatically added to the **Expression** text box, and should appears as follows:  
  
     `[IncomeGroup] = 'High' OR [IncomeGroup] = 'Moderate'`  
  
9. Click the next row in the grid, leaving the operator as the default, **AND**.  
  
10. For **Operator**, leave the default value, **Contains**. Click the **Value** text box.  
  
11. In the **Filter** dialog box, in the first row under **Mining Structure Column**, select `Model`.  
  
12. For **Operator**, select **IS NOT NULL**. Leave the **Value** text box blank. Click **OK**.  
  
     The filter condition in the **Expression** text box of the **Model Filter** dialog box is automatically updated to include the new condition on the nested table. The completed expression is as follows:  
  
     `[IncomeGroup] = 'High' OR [IncomeGroup] = 'Moderate' AND EXISTS SELECT * FROM [vAssocSeqLineItems] WHERE [Model] <> NULL).`  
  
13. [!INCLUDE[clickOK](../includes/clickok-md.md)] ``  
  
#### To enable drillthrough and to process the filtered model  
  
1.  In the **Mining Models** tab, right-click the `Association Filtered` model, and select **Properties**.  
  
2.  Change the **AllowDrillThrough** property to **True**.  
  
3.  Right-click the `Association Filtered` mining model, and select **Process Model**.  
  
4.  Click **Yes** in the error message to deploy the new model to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
5.  In the **Process Mining Structure** dialog box, click **Run**.  
  
6.  When processing is complete click **Close** to exit the **Process Progress** dialog box, and click **Close** again to exit the **Process Mining Structure** dialog box.  
  
 You can verify by using the Microsoft Generic Content Tree viewer and looking at the value for NODE_SUPPORT that the filtered model contains fewer cases than the original model.  
  
## Remarks  
 The nested table filter that you just created checks only for the presence of at least one row in the nested table; however, you can also create filter conditions that check for the presence of specific products.  For example, you could create the following filter:  
  
```  
[IncomeGroup] = 'High' AND  
 EXISTS (SELECT * FROM [<nested table name>] WHERE [Model] = 'Water Bottle' )   
```  
  
 This statement means that you are restricting the customers from the case table to only those who have purchased a water bottle. However, because the number of nested table attributes is potentially unlimited, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] does not supply a list of possible values from which to select. Instead, you must type the exact value.  
  
 You can click **Edit Query** to manually change the filter expression. However, if you change any part of a filter expression manually, the grid will be disabled and thereafter you must work with the filter expression in text edit mode only. To restore grid editing mode, you must clear the filter expression and start over.  
  
> [!WARNING]  
>  You cannot use the LIKE operator in a nested table filter.  
  
## Next Task in Lesson  
 [Predicting Associations &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/predicting-associations-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Model Filter Syntax and Examples &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/model-filter-syntax-and-examples-analysis-services-data-mining.md)   
 [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md)  
  
  
