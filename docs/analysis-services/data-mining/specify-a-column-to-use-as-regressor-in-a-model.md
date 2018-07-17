---
title: "Specify a Column to Use as Regressor in a Model | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Specify a Column to Use as Regressor in a Model
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A linear regression model represents the value of the predictable attribute as the result of a formula that combines the inputs in such a way that the data is fitted as a closely as possible to an estimated regression line. The algorithm accepts only numeric values as inputs, and automatically detects the inputs that provide the best fit.  
  
 However, you can specify that a column be included as a regressor by adding the FORCE_REGRESSOR parameter to the model and specifying the regressors to use. You might want to do this in cases where the attribute has meaning even if the effect is too small to be detected by the model, or when you want to ensure that the attribute is included in the formula.  
  
 The following procedure describes how to create a simple linear regression model, using the same sample data that is used for the [neural networks tutorial](http://msdn.microsoft.com/library/42c3701a-1fd2-44ff-b7de-377345bbbd6b). The model is not necessarily robust, but demonstrates how to use the Data Mining Designer to customize a linear regression model.  
  
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
 [Microsoft Linear Regression Algorithm](../../analysis-services/data-mining/microsoft-linear-regression-algorithm.md)   
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)   
 [Microsoft Linear Regression Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-linear-regression-algorithm-technical-reference.md)   
 [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md)  
  
  
