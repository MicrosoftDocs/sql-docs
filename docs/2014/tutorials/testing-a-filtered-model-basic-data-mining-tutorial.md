---
title: "Testing a Filtered Model (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: f0d74f8c-600d-4df5-a742-695e6947a071
author: minewiskan
ms.author: owend
manager: kfile
---
# Testing a Filtered Model (Basic Data Mining Tutorial)
  Now that you have determined that the `TM_Decision_Tree` model is the most accurate, you will customize the model to better suit the needs of the [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] targeted mailing campaign. Specifically, the Marketing department would like to know if there are any differences between male and female customers. The information could help them decide which magazines to use for advertising and which products to feature in their mailings.  
  
## Using Filters  
 Filtering enables you to easily create models built on subsets of your data. The filter is applied only to the model and does not change the underlying data source.  
  
 In this lesson, you will create a model that is filtered on gender, to predict the characteristics that most influence buying behavior in males and female.  
  
 First you will make a copy of the `TM_Decision_Tree` model.  
  
#### To copy the Decision Tree Model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], in Solution Explorer, select **BasicDataMining**.  
  
2.  Click the **Mining Models** tab.  
  
3.  Right click the `TM_Decision_Tree` model, and select **New Mining Model.**  
  
4.  In the **Model name** field, type `TM_Decision_Tree_Male`.  
  
5.  Click **OK**.  
  
 Next, create a filter to select customers for the model based on their gender.  
  
#### To create a case filter on a mining model  
  
1.  Right-click the `TM_Decision_Tree_Male` mining model to open the shortcut menu.  
  
     -- or --  
  
     Select the model. On the **Mining Model** menu, select **Set Model Filter**.  
  
2.  In the **Model Filter** dialog box, click the top row in the grid, in the **Mining Structure Column** text box.  
  
     The drop-down list displays only the names of the columns in that table.  
  
3.  In the Mining Structure Column text box, select **Gender**.  
  
     The icon at the left side of the text box changes to indicate that the selected item is a table or a column.  
  
4.  Click the **Operator** text box and select the equal (=) operator from the list.  
  
5.  Click the **Value** text box, and type **M**.  
  
6.  Click the next row in the grid.  
  
7.  Click **OK** to close the **Model Filter** dialog box.  
  
     The filter displays in the **Properties** window. Alternately, you can launch the **Model Filter** dialog from the **Properties** window.  
  
8.  Repeat the above steps, but this time name the model `TM_Decision_Tree_Female` and type **F** in the **Value** text box.  
  
## Process the Filtered Models  
 Models cannot be used until they have been deployed and processed. For more information on processing models, see [Processing Models in the Targeted Mailing Structure &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/processing-models-in-the-targeted-mailing-structure-basic-data-mining-tutorial.md).  
  
#### To process the filtered model  
  
1.  Right-click the `TM_Decision_Tree_Male` model and select **Process Mining Structure and all Model**s  
  
2.  Click **Run** to process the new models.  
  
3.  After processing is complete, click **Close** on both processing windows.  
  
     You now have two new models displayed in the **Mining Models** tab.  
  
## Evaluate the Results  
 View the results and assess the accuracy of the filtered models in much the same way as you did for the previous three models. For more information, see:  
  
 [Exploring the Decision Tree Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-decision-tree-model-basic-data-mining-tutorial.md)  
  
 [Testing Accuracy with Lift Charts &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/testing-accuracy-with-lift-charts-basic-data-mining-tutorial.md)  
  
#### To explore the filtered models  
  
1.  Select the **Mining Model Viewer** tab in **Data Mining Designer**.  
  
2.  In the Mining Model box, select `TM_Decision_Tree_Male`.  
  
3.  Slide **Show Level** to `3`.  
  
4.  Change the **Background** value to `1`.  
  
5.  Place your cursor over the node labeled **All** to see the number of bike buyers versus non-bike buyers.  
  
6.  Repeat steps 1 - 5 for `TM_Decision_Tree_Female`.  
  
7.  Explore the results for the `TM_Decision_Tree` and the models filtered for gender. Compared to all bike buyers, male and female bike buyers share some of the same characteristics as the unfiltered bike buyers but all three have interesting differences as well. This is useful information that [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] can use to develop their marketing campaign.  
  
#### To test the lift of the filtered models  
  
1.  Switch to the **Mining Accuracy Chart** tab in Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and select the **Input Selection** tab.  
  
2.  In the **Select data set to be used for Accuracy Chart** group box, select **Use mining structure test cases**.  
  
3.  On the **Input Selection** tab of Data Mining Designer, under **Select predictable mining model columns to show in the lift chart**, select the checkbox for **Synchronize Prediction Columns and Values**.  
  
4.  In the **Predictable Column Name** column, verify that **Bike Buyer** is selected for each model.  
  
5.  In the **Show** column, select each of the models.  
  
6.  In the **Predict Value** column, select `1`.  
  
7.  Select the **Lift Chart** tab to display the lift chart.  
  
     You will now notice that all three Decision Tree models provide significant lift compared to the random guess model, and also outperform the Clustering and Naive-Bayes models.  
  
## Related Tasks  
 For more information on filters, see [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md).  
  
 For an example of how to apply filters to nested tables, see [Intermediate Data Mining Tutorial &#40;Analysis Services - Data Mining&#41;](../../2014/tutorials/intermediate-data-mining-tutorial-analysis-services-data-mining.md).  
  
## Previous Task in Lesson  
 [Testing Accuracy with Lift Charts &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/testing-accuracy-with-lift-charts-basic-data-mining-tutorial.md)  
  
## Next Lesson  
 [Lesson 6: Creating and Working with Predictions &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/lesson-6-creating-and-working-with-predictions-basic-data-mining-tutorial.md)  
  
## See Also  
 [Intermediate Data Mining Tutorial &#40;Analysis Services - Data Mining&#41;](../../2014/tutorials/intermediate-data-mining-tutorial-analysis-services-data-mining.md)   
 [Mining Model Tasks and How-tos](../../2014/analysis-services/data-mining/mining-model-tasks-and-how-tos.md)   
 [Delete a Filter from a Mining Model](../../2014/analysis-services/data-mining/delete-a-filter-from-a-mining-model.md)   
 [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md)  
  
  
