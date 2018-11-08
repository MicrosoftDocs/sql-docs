---
title: "Specify a Column to Use as Regressor in a Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d8e0cb8e-302a-4166-9ed0-e2d9e2919b0a
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify a Column to Use as Regressor in a Model
  A linear regression model represents the value of the predictable attribute as the result of a formula that combines the inputs in such a way that the data is fitted as a closely as possible to an estimated regression line. The algorithm accepts only numeric values as inputs, and automatically detects the inputs that provide the best fit.  
  
 However, you can specify that a column be included as a regressor by adding the FORCE_REGRESSOR parameter to the model and specifying the regressors to use. You might want to do this in cases where the attribute has meaning even if the effect is too small to be detected by the model, or when you want to ensure that the attribute is included in the formula.  
  
 The following procedure describes how to create a simple linear regression model, using the same sample data that is used for the [neural networks tutorial](../../tutorials/lesson-5-build-models-intermediate-data-mining-tutorial.md). The model is not necessarily robust, but demonstrates how to use the Data Mining Designer to customize a linear regression model.  
  
### How to create a simple linear regression model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in **Solution Explorer**, expand **Mining Structures**.  
  
2.  Double-click Call Center.dmm to open it in the designer.  
  
3.  From the **Mining Model** menu, select **New Mining Model**.  
  
4.  For the algorithm, select **Microsoft Linear Regression**. For the name, type **Call Center Regression**.  
  
5.  In the **Mining Models** tab, change the column usage as follows. All columns not in the following list should be set to **Ignore**, if they are not already.  
  
     FactCallCenterID**Key**  
  
     ServiceGrade**PredictOnly**  
  
     Total Operators**Input**  
  
     AverageTimePerIssue**Input**  
  
6.  From the **Mining Model** menu, select **Set Model Parameters**.  
  
7.  For the parameter, FORCE_REGRESSOR, in the **Value** column, type the column names enclosed in brackets and separated by a comma, as follows:  
  
    ```  
    [Average Time Per Issue],[Total Operators]  
    ```  
  
    > [!NOTE]  
    >  The algorithm will automatically detect which columns are the best regressors. You only need to force regressors when you want to ensure that a column is included in the final formula.  
  
8.  From the **Mining Model** menu, select **Process Model**.  
  
     In the viewer, the model is represented a single node containing the regression formula. You can view the formula in the **Mining Legend**, or you can extract the coefficients for the formula by using queries.  
  
## See Also  
 [Microsoft Linear Regression Algorithm](microsoft-linear-regression-algorithm.md)   
 [Data Mining Queries](data-mining-queries.md)   
 [Microsoft Linear Regression Algorithm Technical Reference](microsoft-linear-regression-algorithm-technical-reference.md)   
 [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-linear-regression-models-analysis-services-data-mining.md)  
  
  
