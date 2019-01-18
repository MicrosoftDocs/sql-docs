---
title: "Create a Cross-Validation Report | Microsoft Docs"
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
# Create a Cross-Validation Report
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This topic walks you through creation of a cross-validation report using the Accuracy Chart tab in Data Mining Designer. For general information about what a cross-validation report looks like, and the statistical measures it includes, see [Cross-Validation &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/cross-validation-analysis-services-data-mining.md).  
  
 A cross-validation report is fundamentally different from an accuracy chart, such as a lift chart or classification matrix.  
  
-   Cross-validation assesses the overall distribution of data that is used in a model or structure; therefore, you do not specify a testing data set. Cross-validation always uses only the original data that was used to train the model or the mining structure.  
  
-   Cross-validation can only be performed with respect to a single predictable outcome. If the structure supports models that have different predictable attributes, you must create separate reports for each predictable output.  
  
-   Only models that are related to the currently selected structure are available for cross-validation.  
  
-   If the structure that is currently selected supports a combination of clustering and non-clustering models, when you click **Get Results**, the cross-validation stored procedure will automatically load models that have the same predicted column, and ignore clustering models that do not share the same predictable attribute.  
  
-   You can create a cross-validation report on a clustering model that does not have a predictable attribute only if the mining structure does not support any other predictable attributes.  
  
### Select a mining structure  
  
1.  Open the Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
2.  In Solution Explorer, open the database that contains the structure or model for which you want to create a report.  
  
3.  Double-click the mining structure to open the structure and its related models in Data Mining Designer.  
  
4.  Click the **Mining Accuracy Chart** tab.  
  
5.  Click the **Cross Validation** tab.  
  
### Set cross-validation options  
  
1.  On the **Cross Validation** tab, for **Fold Count**, click the down arrow to select a number between 1 and 10. The default value is 10.  
  
     The **Fold Count** represents the number of partitions that will be created within the original data set. If you set Fold Count to 1, the training set will be used without partitioning.  
  
2.  For **Target Attribute**, click the down arrow, and select a column from the list. If the model is a clustering model, select **#Cluster** to indicate that the model does not have a predictable attribute. Note that the value, **#Cluster**, is available only when the mining structure does not support other types of predictable attributes.  
  
     You can select only one predictable attribute per report. By default, all related models that have the same predictable attribute are included in the report.  
  
3.  For **Max Cases**, type a number that is large enough to provide a representative sample of data when the data is split among the specified number of folds. If the number is greater than the count of cases in the model training set, all cases will be used.  
  
     If the training data set is very large, setting the value for **Max Cases** limits the total number of cases processed, and lets the report finish faster. However, you should not set **Max Cases** too low or there may be insufficient data for cross-validation.  
  
4.  Optionally, for **Target State**, type the value of the predictable attribute that you want to model. For example, if the column [Bike Buyer] has two possible values, 1 (Yes) and 2 (No), you can enter the value 1 to assess the accuracy of the model for just the desired outcome.  
  
    > [!NOTE]  
    >  If you do not enter a value, the **Target Threshold** option is not available, and the model is assessed for all possible values of the predictable attribute.  
  
5.  Optionally, for **Target Threshold**, type a decimal number between 0 and 1 to specify the minimum probability that a prediction must have to be counted as accurate.  
  
     For additional tips about how to set probability thresholds, see [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).  
  
6.  Click **Get Results**.  
  
### Print the cross-validation report  
  
1.  Right-click the completed report on the **Cross Validation** tab.  
  
2.  In the shortcut menu, select **Print** or **Print Preview** to review the report first.  
  
### Create a copy of the report in Microsoft Excel  
  
1.  Right-click the completed report on the **Cross Validation** tab.  
  
2.  In the shortcut menu, select **Select All**.  
  
3.  Right-click the selected text, and select **Copy**.  
  
4.  Paste the selection into an open Excel workbook. If you use the **Paste** option, the report is pasted into Excel as HTML, which preserves row and column formatting. If you paste the report by using the **Paste Special** options for text or Unicode text, the report is pasted in row-delimited format.  
  
## See Also  
 [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md)  
  
  
