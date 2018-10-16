---
title: "Create Testing Set (Data Mining Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.dmwizard.holdout.f1"
ms.assetid: d0a44b59-ffbd-45fc-baa8-6b8046b1a2f5
author: minewiskan
ms.author: owend
manager: craigg
---
# Create Testing Set (Data Mining Wizard)
  Use the **Create Testing Set** page to specify how much of the data is to be used for training, and how much is to be reserved for use as a test set. Separating data into a training and testing set when you create a mining structure makes it much easier to assess the accuracy of mining models that you create later.  
  
 You can specify the amount of testing data as a percentage, or you can specify a number to limit the number of cases used for testing. If you specify both a percentage and a maximum number of cases to use for testing, both settings are used, and the testing data set contains the smaller number of cases. By default, 30 percent of data is used for testing, 70 percent for training, and there is no maximum number of test cases.  
  
 By default, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] generates a numeric seed that is used to start partitioning. This seed is based on the name of the mining structure. If you want to ensure that the partition stays the same even if the name of the mining structure is changed, you can specify a value for the seed, by setting the HoldoutSeed property of the mining structure. If you change the holdout seed, you must reprocess the structure.  
  
 If you later want to change the amount of testing or training data, you can modify the `HoldoutMaxCases` and `HoldoutMaxPercent` properties on the data mining structure by using the **Properties** window. However, after you make the change you must reprocess the mining structure and all associated mining models. The following limitations also apply:  
  
-   Partitioning of a data mining structure is supported only when the data mining structure is stored in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. Earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] do not support caching of partition information for mining structures.  
  
-   You cannot partition a mining structure if the mining structure contains a Key Time column, which is required for time series mining models.  
  
-   You cannot partition data if you are trying to predict a value that is stored in a nested table.  
  
 **For More Information:** [Testing and Validation &#40;Data Mining&#41;](data-mining/testing-and-validation-data-mining.md), [Create a Relational Mining Structure](data-mining/create-a-relational-mining-structure.md), [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md)  
  
## Options  
 **Percentage of data for testing**  
 Click the up and down arrows to increase or decrease the percentage of data to use as a training set, or type a value between 0 and 100 in the text box.  
  
 **Maximum number of cases in testing data set**  
 Type a number to limit the number of cases that can be used for testing.  
  
 If you specify a number that is larger than the number of actual cases in the data, all the cases will be used.  
  
 The default is NULL. This means there is no limit.  
  
## See Also  
 [Data Mining Wizard F1 Help &#40;Analysis Services - Data Mining&#41;](data-mining-wizard-f1-help-analysis-services-data-mining.md)   
 [Suggest Related Columns &#40;Data Mining Wizard&#41;](suggest-related-columns-data-mining-wizard.md)   
 [Specify Table Types &#40;Data Mining Wizard&#41;](specify-table-types-data-mining-wizard.md)   
 [Specify the Column's Content and Data Type &#40;Data Mining Wizard&#41;](specify-the-column-s-content-and-data-type-data-mining-wizard.md)  
  
  
