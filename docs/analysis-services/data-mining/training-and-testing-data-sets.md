---
title: "Training and Testing Data Sets | Microsoft Docs"
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
# Training and Testing Data Sets
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Separating data into training and testing sets is an important part of evaluating data mining models. Typically, when you separate a data set into a training set and testing set, most of the data is used for training, and a smaller portion of the data is used for testing. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] randomly samples the data to help ensure that the testing and training sets are similar. By using similar data for training and testing, you can minimize the effects of data discrepancies and better understand the characteristics of the model.  
  
 After a model has been processed by using the training set, you test the model by making predictions against the test set. Because the data in the testing set already contains known values for the attribute that you want to predict, it is easy to determine whether the model's guesses are correct.  
  
## Creating Test and Training Sets for Data Mining Structures  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you separate the original data set at the level of the mining structure. The information about the size of the training and testing data sets, and which row belongs to which set, is stored with the structure, and all the models that are based on that structure can use the sets for training and testing.  
  
 You can define a testing data set on a mining structure in the following ways:  
  
-   Using the Data Mining Wizard to divide the mining structure when you create it.  
  
-   Modifying structure properties in the **Mining Structure** tab of the Data Mining Designer.  
  
-   Creating and modifying structures programmatically by using Analysis Management Objects (AMO) or XML Data Definition Language (DDL).  
  
### Using the Data Mining Wizard to Divide a Mining Structure  
 By default, after you have defined the data sources for a mining structure, the Data Mining Wizard will divide the data into two sets: one with 70 percent of the source data, for training the model, and one with 30 percent of the source data, for testing the model. This default was chosen because a 70-30 ratio is often used in data mining, but with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] you can change this ratio to suit your requirements.  
  
 You can also configure the wizard to set a maximum number of training cases, or you can combine the limits to allow a maximum percentage of cases up to a specified maximum number of cases. When you specify both a maximum percentage of cases and a maximum number of cases, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses the smaller of the two limits as the size of the test set. For example, if you specify 30 percent holdout for the testing cases, and the maximum number of test cases as 1000, the size of the test set will never exceed 1000 cases. This can be useful if you want to ensure that the size of your test set stays consistent even if more training data is added to the model.  
  
 If you use the same data source view for different mining structures, and want to ensure that the data is divided in roughly the same way for all mining structures and their models, you should specify the seed that is used to initialize random sampling. When you specify a value for **HoldoutSeed**, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will use that value to begin sampling. Otherwise, sampling uses a hashing algorithm on the name of the mining structure to create the seed value.  
  
> [!NOTE]  
>  If you create a copy of the mining structure by using the **EXPORT** and **IMPORT** statements, the new mining structure will have the same training and testing data sets, because the export process creates a new ID but uses the same name. However, if two mining structures use the same underlying data source but have different names, the sets that are created for each mining structure will be different.  
  
### Modifying Structure Properties to Create a Test Data Set  
 If you create and process a mining structure, and then later decide that you want to set aside a test data set, you can modify the properties of the mining structure. To change the way that data is partitioned, edit the following properties:  
  
|Property|Description|  
|--------------|-----------------|  
|**HoldoutMaxCases**|Specifies the maximum number of cases to include in the testing set.|  
|**HoldoutMaxPercent**|Specifies the number of cases to include in the testing set as a percentage of the complete data set. To have no data set, you would specify 0.|  
|**HoldoutSeed**|Specifies an integer value to use as seed when randomly selecting data for the partitions. This value does not affect the number of cases in the training set; instead, it ensures that the partition can be repeated.|  
  
 If you add or change a test data set to an existing structure, you must reprocess the structure and all associated models. Also, because dividing the source data causes the model to be trained on a different subset of the data, you might see different results from your model.  
  
### Specifying Holdout Programmatically  
 You can define testing and training data sets on a mining structure by using DMX statements, AMO, or XML DDL. The ALTER MINING STRUCTURE statement does not support the use of holdout parameters.  
  
-   **DMX** In the Data Mining Extensions (DMX) language, the CREATE MINING STRUCTURE statement has been extended to include a WITH HOLDOUT clause..  
  
-   **ASSL** You can either create a new mining structure, or add a testing data set to an existing mining structure, by using the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Scripting Language (ASSL)..  
  
-   **AMO** You can also view and modify holdout data sets  by using AMO..  
  
 You can view information about the holdout data set in an existing mining structure by querying the data mining schema rowset. You can do this by making a DISCOVER ROWSET call, or you can use a DMX query.  
  
## Retrieving Information about Holdout Data  
 By default, all information about the training and test data sets is cached, so that you can use existing data to train and then test new models. You can also define filters to apply to the cached holdout data so that you can evaluate the model on subsets of the data.  
  
 The way that cases are divided into training and testing data sets depends on the way that you configure holdout, and the data that you provide. If you want to determine the number of cases used for training or for testing, or if you want to find additional details about the cases included in the training and test sets, you can query the model structure by creating a DMX query. For example, the following query returns the cases that were used in the training set of the model.  
  
```  
SELECT * from <structure>.CASES WHERE IsTrainingCase()  
```  
  
 To retrieve only the test cases, and additionally filter the test cases on one of the columns in the mining structure, use the following syntax:  
  
```  
SELECT * from <structure>.CASES WHERE IsTestCase() AND <structure column name> = '<value>'  
```  
  
## Limitations on the Use of Holdout Data  
  
-   To use holdout, the <xref:Microsoft.AnalysisServices.MiningStructureCacheMode> property of the mining structure must be set to the default value, **KeepTrainingCases**. If you change the **CacheMode** property to **ClearAfterProcessing**, and then reprocess the mining structure, the partition will be lost.  
  
-   You cannot remove data from a time series model; therefore, you cannot separate the source data into training and testing sets. If you begin to create a mining structure and model and choose the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm, the option to create a holdout data set is disabled. Use of holdout data is also disabled if the mining structure contains a KEY TIME column at either the case or nested table level.  
  
-   It is possible to inadvertently configure the holdout data set such that the complete data set is used for testing, and no data remains for training. However, if you do so, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will raise an error so that you can correct the problem. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] also warns you when the structure is processed if more than 50 percent of the data has been held out for testing.  
  
-   In most cases, the default holdout value of 30 provides a good balance between training and testing data. There is no simple way to determine how large the data set should be to provide sufficient training, or how sparse the training set can be and still avoid overfitting. However, after you have built a model, you can use cross-validation to assess the data set with respect to a particular model.  
  
-   In addition to the properties listed in the previous table, a read-only property, **HoldoutActualSize**, is provided in AMO and XML DDL. However, because the actual size of a partition cannot be determined accurately until after the structure has been processed, you should check whether the model has been processed before you retrieve the value of the **HoldoutActualSize** property.  
  
## Related Content  
  
|Topics|Links|  
|------------|-----------|  
|Describes how filters on a model interact with training and testing data sets.|[Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md)|  
|Describes how the use of training and testing data affects cross-validation.|[Cross-Validation &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/cross-validation-analysis-services-data-mining.md)|  
|Provides information on the programmatic interfaces for working with training and testing sets in a mining structure.|[AMO Concepts and Object Model](https://docs.microsoft.com/bi-reference/amo/amo-concepts-and-object-model)<br /><br /> [MiningStructure Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/objects/miningstructure-element-assl)|  
|Provides DMX syntax for creating holdout sets.|[CREATE MINING STRUCTURE &#40;DMX&#41;](../../dmx/create-mining-structure-dmx.md)|  
|Retrieve information about cases in the training and testing sets.|[Data Mining Schema Rowsets](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/data-mining-schema-rowsets)<br /><br /> [Data Mining Schema Rowsets &#40;SSAs&#41;](../../analysis-services/data-mining/data-mining-schema-rowsets-ssas.md)|  
  
## See Also  
 [Data Mining Tools](../../analysis-services/data-mining/data-mining-tools.md)   
 [Data Mining Concepts](../../analysis-services/data-mining/data-mining-concepts.md)   
 [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md)   
 [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md)  
  
  
