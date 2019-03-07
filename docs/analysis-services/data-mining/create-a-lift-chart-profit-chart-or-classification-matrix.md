---
title: "Create a Lift Chart, Profit Chart, or Classification Matrix | Microsoft Docs"
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
# Create a Lift Chart, Profit Chart, or Classification Matrix
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can create an accuracy chart for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data mining model in five basic steps:  
  
-   Select the mining structure that contains the mining models that you want to compare.  
  
-   Select the mining models to add to the chart.  
  
-   Specify a source of testing data to use in generating the chart.  
  
-   Choose the chart type.  
  
-   Configure the chart options.  
  
 These basic steps are the same for the lift chart, profit chart, and classification matrix. The following procedures outline the steps to configure the basic chart options for these chart types. For information about how to create a cross-validation report, see [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).  
  
### Open the mining structure in the Accuracy Chart Designer  
  
1.  Open the Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
2.  In Solution Explorer, double-click the structure that contains the mining model or models.  
  
3.  Click the **Mining Accuracy Chart** tab.  
  
### Select mining models for inclusion in the chart  
  
1.  On the **Mining Accuracy Chart** tab of Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Input Selection** tab.  
  
     The list displays all models in the current structure that have the same predictable attribute.  
  
2.  Select the **Show box** for each model that you want to include in the chart.  
  
3.  Click the **Predictable Column Name** text box, and select the name of a predictable column from the list. All models that you put in one chart must have the same predictable column.  
  
4.  If you compare two models and the predictable columns have different values or different data types, clear the **Synchonize prediction columns and values** box to force a comparison.  
  
    > [!NOTE]  
    >  If the **Synchonize prediction columns and values** box is selected, Analysis Services analyzes the data in the predictable columns of the model and the test data, and attempts to find the best match. Therefore, do not clear the box unless absolutely necessary to force a comparison of the columns.  
  
5.  Click the **Predict Value** text box, and select a value from the list. If the predictable column is a continuous data type, you must type a value in the text box.  
  
     For more information, see [Choose the Column to Use for Testing a Mining Model](../../analysis-services/data-mining/choose-the-column-to-use-for-testing-a-mining-model.md).  
  
### Select testing data  
  
1.  On the **Input Selection** tab of the **Mining Accuracy Chart** tab, specify the source of the data that you will use to generate the chart by selecting one of the options in the group, **Select data set to be used for accuracy chart**.  
  
    -   Select the option, **Use Mining Model test cases**, if you want to use the subset of cases that is defined by the intersection of the mining structure test cases and any filters that may have been applied during model creation.  
  
    -   Select the option, **Use mining structure test cases**, to use the full set of testing cases that were defined as part of the mining structures holdout data set.  
  
    -   Select the option, **Specify a different data set**, if you want to use external data.  The data set must be available as a data source view.   Click the browse (**...**) button to choose the data tables to use for the accuracy chart. For more information, see [Choose and Map Model Testing Data](../../analysis-services/data-mining/choose-and-map-model-testing-data.md).  
  
         If you are using an external data set, you can optionally filter the input data set. For more information, see [Apply Filters to Model Testing Data](../../analysis-services/data-mining/apply-filters-to-model-testing-data.md).  
  
> [!NOTE]  
>  You cannot create a filter on the model test cases or the mining structure test cases on the **Input Selection** tab. To create a filter on the mining model, modify the Filter property of the model. For more information, see [Apply a Filter to a Mining Model](../../analysis-services/data-mining/apply-a-filter-to-a-mining-model.md).  
  
### Configure chart settings and generate the chart  
  
1.  In the **Mining Accuracy Chart** tab, click the tab for the chart you want to create.  
  
2.  For a **lift chart**, click the **Lift Chart** tab. The chart is automatically generated based on the model, predictable attributes, and input data that you just selected.  
  
3.  For a **classification matrix**, click the **Classification Matrix** tab. No further settings are needed; the chart is automatically generated based on the input data and model that you selected.  
  
4.  For a **profit chart**, first click the **Lift Chart** tab. Then, from the **Chart type** drop-down list, select **Profit chart**.  
  
     Enter the following settings in the **Profit Chart Settings** dialog box.  
  
     **Population**  
     The number of cases from the data set that you want to use when creating the lift chart.  
  
     The model always chooses the cases in order of decreasing probability; that is, if you are assessing potential customers and you choose a number that represents only half the records in your customer database, the model will measure accuracy on the subset of cases that best fit your model.  
  
     This is because when you use the model to generate a mailing or create a campaign, you will use the prediction probability associated with each case to target only the customers who have the highest probability of making a positive response.  
  
     **Fixed Cost**  
     The fixed cost that is associated with the business problem.  
  
     If this were for a targeted mailing solution, the fixed cost might represent a printer setup fee that covers the initial cost of preparing the promotional mailing.  
  
     This cost applies one time to the entire target population.  
  
     **Individual Cost**  
     Costs that are in addition to the fixed cost, that can be associated with each customer contact. For example, you might enter the postage cost for a promotional mailing or the cost of making telephone calls.  
  
     This cost must be the same for the entire target population. Each value is multiplied by the number of cases that are targeted.  
  
     **Revenue Per Individual**  
     The amount of revenue that is associated with each successful sale.  
  
## See Also  
 [Lift Chart &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/lift-chart-analysis-services-data-mining.md)   
 [Classification Matrix &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/classification-matrix-analysis-services-data-mining.md)  
  
  
