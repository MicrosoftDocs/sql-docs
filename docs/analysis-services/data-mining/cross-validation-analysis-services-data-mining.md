---
title: "Cross-Validation (Analysis Services - Data Mining) | Microsoft Docs"
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
# Cross-Validation (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  *Cross-validation* is a standard tool in analytics and is an important feature for helping you develop and fine-tune data mining models. You use cross-validation after you have created a mining structure and related mining models to ascertain the validity of the model.  Cross-validation has the following applications:  
  
-   Validating the robustness of a particular mining model.  
  
-   Evaluating multiple models from a single statement.  
  
-   Building multiple models and then identifying the best model based on statistics.  
  
 This section describes how to use the cross-validation features provided for data mining, and how to interpret the results of cross-validation for either a single model or for multiple models based on a single data set.  
  
## Overview of Cross-Validation Process  
 Cross-validation consists of two phases, training and result generation. These phases include the following steps:  
  
-   You select a target mining structure.  
  
-   You specify the models you want to test. This step is optional; you can test just the mining structure as well.  
  
-   You specify the parameters for testing the trained models.  
  
    -   The predictable attribute, predicted value, and accuracy threshold.  
  
    -   The number of folds into which to partition the structure or model data.  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] creates and trains as many models as there are folds.  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns a set of accuracy metrics for each fold in each model, or for the data set as a whole.  
  
## Configuring Cross-Validation  
 You can customize the way that cross-validation works to control the number of cross-sections, the models that are tested, and the accuracy bar for predictions. If you use the cross-validation stored procedures, you can also specify the data set that is used for validating the models. This wealth of choices means that you can easily produce many sets of different results that must then be compared and analyzed.  
  
 This section provides information to help you configure cross-validation appropriately.  
  
### Setting the Number of Partitions  
 When you specify the number of partitions, you determine how many temporary models will be created. For each partition, a cross-section of the data is flagged for use as the test set, and a new model is created by training on the remaining data not in the partition. This process is repeated until [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] has created and tested the specified number of models. The data that you specified as being available for cross-validation is distributed evenly among all partitions.  
  
 The example in the diagram illustrates the usage of data if three folds are specified.  
  
 ![How cross-validation segments data](../../analysis-services/data-mining/media/xvoverviewmain.gif "How cross-validation segments data")  
  
 In the scenario in the diagram, the mining structure contains a holdout data set that is used for testing, but the test data set has not been included for cross-validation. As a result, all the data in the training data set, 70 percent of the data in the mining structure, is used for cross-validation. The cross-validation report shows the total number of cases used in each partition.  
  
 You can also specify the amount of data that is used during cross-validation, by specifying the number of overall cases to use. The cases are distributed evenly across all folds.  
  
 For mining structures stored in an instance of SQL Server [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the maximum value that you can set for the number of folds is 256, or the number of cases, whichever is less. If you are using a session mining structure, the maximum number of folds is 10.  
  
> [!NOTE]  
>  As you increase the number of folds, the time required to perform cross-validation increases accordingly, because a model must be generated and tested for each fold. You may experience performance problems if the number of folds is too high.  
  
### Setting the Accuracy Threshold  
 The state threshold lets you set the accuracy bar for predictions. For each case, the model calculates the *predict probability*, meaning the probability that the predicted state is correct. If the predict probability exceeds the accuracy bar, the prediction is counted as correct; if not, the prediction is counted as incorrect. You control this value by setting **State Threshold** to a number between 0.0 and 1.0, where numbers closer to 1 indicate a strong level of confidence in the predictions, and numbers closer to 0 indicate that the prediction is less likely to be true. The default value for state threshold is NULL, which means that the predicted state with the highest probability is considered the target value.  
  
 You should be aware that the setting for state threshold affects measures of model accuracy. For example, assume that you have three models that you want to test. All are based on the same mining structure and all predict the column [Bike Buyer]. Moreover, you want to predict a single value of 1, meaning "yes, will buy." The three models return predictions with predict probabilities of 0.05, 0.15, and 0.8. If you set the state threshold to 0.10, two of the predictions are counted as correct. If you set the state threshold to 0.5, only one model is counted as having returned a correct prediction. If you use the default value, null, the most probable prediction is counted as correct. In this case, all three predictions would be counted as correct.  
  
> [!NOTE]  
>  You can set a value of 0.0 for the threshold, but the value is meaningless, because every prediction will be counted as correct, even those with zero probability. Be careful not to accidentally set **State Threshold** to 0.0.  
  
### Choosing Models and Columns to Validate  
 When you use the **Cross Validation** tab in Data Mining Designer, you must first select the predictable column from a list. Typically, a mining structure can support many mining models, not all of which use the same predictable column. When you run cross-validation, only those models that use the same predictable column can be included in the report.  
  
 To choose a predictable attribute, click **Target Attribute** and select the column from the list. If the target attribute is a nested column, or a column in a nested table, you must type the name of the nested column using the format \<Nested Table Name>(key).\<Nested Column>. If the only column used from the nested table is the key column, you can use \<Nested Table Name>(key).  
  
 After you select the predictable attribute, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] automatically tests all models that use the same predictable attribute. If the target attribute contains discrete values, after you have selected the predictable column, you can optionally type a target state, if there is a specific value that you want to predict.  
  
 The selection of the target state affects the measures that are returned. If you specify a target attribute-that is, a column name-and do not pick a specific value that you want the model to predict, by default the model will be evaluated on its prediction of the most probable state.  
  
 When you use cross-validation with clustering models, there is no predictable column; instead, you select **#Cluster** from the list in the **Target Attribute** list box. After you have selected this option, other options that are not relevant to clustering models, such as **Target State**, are disabled. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will then test all clustering models that are associated with the mining structure.  
  
## Tools for Cross-Validation  
 You can use cross-validation from the Data Mining Designer, or you can perform cross-validation by running stored procedures.  
  
 If you use the Data Mining Designer tools to perform cross-validation, you can configure the training and accuracy results parameters in a single dialog box. This makes it easier to set up and view results. You can measure the accuracy of all mining models that are related to a single mining structure and then immediately view the results in an HTML report. However, the stored procedures offer some advantages, such as added customizations and the ability to script the process.  
  
### Cross-Validation in Data Mining Designer  
 You can perform cross-validation by using the **Cross-Validation** tab of the Mining Accuracy Chart view in either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or SQL Server Development Studio.  
  
 To see an example of how to create a cross-validation report using the user interface, see [Create a Cross-Validation Report](../../analysis-services/data-mining/create-a-cross-validation-report.md).  
  
### Cross-Validation Stored Procedures  
 For advanced users, cross-validation is also available in the form of fully parameterized system stored procedures. You can run the stored procedures by connecting to an instance from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or from any managed code application.  
  
 The stored procedures are grouped by mining model type. One set of stored procedures works with clustering models only. The other set of stored procedures works with other mining models.  
  
 For each type of mining model, clustered or non-clustered, the stored procedures perform cross-validation in two separate phases.  
  
 **Partition data and generate metrics for partitions**  
  
 For the first phase, you call a system stored procedure that creates as many partitions as you specify within the data set, and returns accuracy results for each partition. For each metric, Analysis Services then calculates the mean and standard deviation for the partitions.  
  
-   [SystemGetCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetcrossvalidationresults-analysis-services-data-mining.md)  
  
-   [SystemGetClusterCrossValidationResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclustercrossvalidationresults-analysis-services-data-mining.md)  
  
 **Generate metrics for entire data set**  
  
 In the second phase, you call a different set of stored procedures. These stored procedures do not partition the data set, but generate accuracy results for the specified data set as a whole. If you have already partitioned and processed a mining structure, you can call this second set of stored procedures to get just the results.  
  
-   [SystemGetAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetaccuracyresults-analysis-services-data-mining.md)  
  
-   [SystemGetClusterAccuracyResults &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/systemgetclusteraccuracyresults-analysis-services-data-mining.md)  
  
#### Defining the Testing Data  
 When you run the cross-validation stored procedures that calculate accuracy (SystemGetAccuracyResults or SystemGetClusterAccuracyResults), you can specify the source of the data that is used for testing during cross-validation. This option is not available in the user interface.  
  
 You can specify as a testing data source any of the following options:  
  
-   Use only the training data.  
  
-   Include an existing testing data set.  
  
-   Use only the testing data set.  
  
-   Apply existing filters to each model.  
  
-   Any combination of the training set, testing set, and model filters.  
  
 To specify a testing data source, you provide an integer value for the **DataSet** parameter of the stored procedure. For a list of the argument values, see the Remarks section of the relevant stored procedure reference topic.  
  
 If you perform cross-validation by using the **Cross-Validation** report in the Data Mining Designer, you cannot change the data set that is used. By default, the training cases for each model are used. If a filter is associated with a model, the filter is applied.  
  
## Results of Cross-Validation  
 If you use the Data Mining Designer, these results are displayed in a grid-like Web viewer. If you use the cross-validation stored procedures, these same results are returned as a table.  
  
 The report contains two types of measures: aggregates that indicate the variability of the data set when divided into folds, and model-specific measures of accuracy for each fold. The following topics provide more information about these metrics:  
  
 [Cross-Validation Formulas](../../analysis-services/data-mining/cross-validation-formulas.md)  
  
 Lists all the measures by test type. Describes in general how the measures can be interpreted.  
  
 [Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md)  
  
 Describes the formulas for calculating each measure, and lists the type of attribute that each measure can be applied to.  
  
## Restrictions on Cross-Validation  
 If you perform cross-validation by using the cross-validation report in SQL Server Development Studio, there are some limitations on the models that you can test and the parameters you can set.  
  
-   By default, all models associated with the selected mining structure are cross-validated. You cannot specify the model or a list of models.  
  
-   Cross-validation is not supported for models that are based on the Microsoft Time Series algorithm or the Microsoft Sequence Clustering algorithm.  
  
-   The report cannot be created if your mining structure does not contain any models that can be tested by cross-validation.  
  
-   If the mining structure contains both clustering and non-clustering models and you do not choose the **#Cluster** option, results for both types of models are displayed in the same report, even though the attribute, state, and threshold settings might not be appropriate for the clustering models.  
  
-   Some parameter values are restricted. For example, a warning is displayed if the number of folds is more than 10, because generating so many models might cause the report to display slowly.  
  
 If you are testing multiple mining models, and the models have filters, each model is filtered separately. You cannot add a filter to a model or change the filter for a model during cross-validation.  
  
 Because cross-validation by defaults tests all mining models that are associated with a structure, you may receive inconsistent results if some models have a filter and others do not. To ensure that you compare only those models that have the same filter, you should use the stored procedures and specify a list of mining models. Or, use only the mining structure test set with no filters to ensure that a consistent set of data is used for all models.  
  
 If you perform cross-validation by using the stored procedures, you have the additional option of choosing the source of testing data. If you perform cross-validation by using the Data Mining Designer, you must use the testing data set that is associated with the model or structure, if any. Generally, if you want to specify advanced settings, you should use the cross-validation stored procedures.  
  
 Cross-validation cannot be used with time series or sequence clustering models. Specifically, no model that contains a KEY TIME column or a KEY SEQUENCE column can be included in cross-validation.  
  
## Related Content  
 See the following topics for more information about cross-validation, or information about related methods for testing mining models, such as accuracy charts.  
  
|Topics|Links|  
|------------|-----------|  
|Describes how to set cross-validation parameters in SQL Server Development Studio.|[Cross-Validation Tab &#40;Mining Accuracy Chart View&#41;](http://msdn.microsoft.com/library/bd215a68-1ad7-4046-9c44-ec8e2be13a64)|  
|Describes the metrics that are provided by cross-validation|[Cross-Validation Formulas](../../analysis-services/data-mining/cross-validation-formulas.md)|  
|Explains the cross-validation report format and defines the statistical measures provided for each model type.|[Measures in the Cross-Validation Report](../../analysis-services/data-mining/measures-in-the-cross-validation-report.md)|  
|Lists the stored procedures for computing cross-validation statistics.|[Data Mining Stored Procedures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-stored-procedures-analysis-services-data-mining.md)|  
|||  
|Describes how to create a testing data set for mining structures and related models.|[Training and Testing Data Sets](../../analysis-services/data-mining/training-and-testing-data-sets.md)|  
|See examples of other accuracy chart types.|[Classification Matrix &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/classification-matrix-analysis-services-data-mining.md)<br /><br /> [Lift Chart &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/lift-chart-analysis-services-data-mining.md)<br /><br /> [Profit Chart &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/profit-chart-analysis-services-data-mining.md)<br /><br /> [Scatter Plot &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/scatter-plot-analysis-services-data-mining.md)|  
|Describes steps for creating various accuracy charts.|[Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)|  
  
## See Also  
 [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md)  
  
  
