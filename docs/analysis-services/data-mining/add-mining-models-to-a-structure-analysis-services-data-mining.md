---
title: "Add Mining Models to a Structure (Analysis Services - Data Mining) | Microsoft Docs"
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
# Add Mining Models to a Structure (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A mining structure is intended to support multiple mining models. Therefore, after you finish the wizard, you can open the structure and add new mining models. Each time that you create a model, you can use a different algorithm, change the parameters, or apply filters to use a different subset of the data.  
  
## Adding New Mining Models  
 When you use the Data Mining Wizard to create a new mining model, by default you must always create a mining structure first. The wizard then gives you the option to add an initial mining model to the structure. However, you don't need to create a model right away. If you create the structure only, you do not need to make a decision about which column to use as the predictable attribute, or how to use the data in a particular model. Instead, you just set up the general data structure that you want to use in future, and later you can use [Data Mining Designer](../../analysis-services/data-mining/data-mining-designer.md) to add new mining models that are based on the structure.  
  
> [!NOTE]  
>  In DMX, the CREATE MINING MODEL statement  begins with the mining model. That is, you define your choice of mining model, and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] automatically generates the underlying structure. Later you can continue to add new mining models to that structure, by using the ALTER STRUCTURE... ADD MODEL statement.  
  
## Choosing an Algorithm  
 When you add a new model to an existing structure, the first thing you should do is select a data mining algorithm to use in that model. Choosing the algorithm is important because each algorithm performs a different type of analysis and has different requirements.  
  
 When you select an algorithm that is incompatible with your data, you will get a warning. In some cases, you might need to ignore columns that cannot be processed by the algorithm. In other cases, the algorithm will automatically make the adjustments for you. For example, if your structure contains numeric data, and the algorithm can only work with discrete values, it will group the numeric values into discrete ranges for you. In some cases, you might need to manually fix the data first, by choosing a key or choosing a predictable attribute.  
  
 You do not need to change the algorithm when you create a new model. Often you can get very different results by using the same algorithm, but filtering the data, or changing a parameter such as the clustering method or the minimum itemset size. We recommend that you experiment with multiple models to see which parameters produce the best results.  
  
 Note that all new models need to be processed before you can use them.  
  
## Specifying the Usage of Columns in a New Mining Model  
 When you add new mining models to an existing mining structure, you must specify how each column of data should be used by the model. Depending on the type of algorithm you choose for the model, some of these choices may be made by default. If you do not specify a usage type for a column, the column will not be included in the mining structure. However, the data in the column can still be available for drillthrough, if the model supports it.  
  
 Columns from the mining structure that are used by the model (if not set to Ignore) must be a key, an input column, a predictable column, or a predictable column the values of which are also used as inputs to the model.  
  
-   Key columns contain a unique identifier for each row in a table. Some mining models, such as those based on the sequence clustering or time series algorithms, can contain multiple key columns. However, these multiple keys are not compound keys in the relational sense, but instead must be selected so as to provide support for time series and sequence clustering analysis.  
  
-   Input columns provide the information from which predictions are made. The Data Mining Wizard provides the **Suggest** feature, which is enabled when you select a predictable column. If you click this button, the wizard will sample the predictable values and determine which of the other columns in the structure make good variables. It will reject key columns or other columns with many unique values, and suggest columns that appear to be correlated with the outcome.  
  
     This feature is particularly handy when datasets contain more columns than you really need to build a mining model. The **Suggest** feature calculates a numeric score, from 0 to 1, that describes the relationship between each column in the dataset and the predictable column. Based on this score, the feature suggests columns to use as input for the mining model. If you use the **Suggest** feature, you can use the suggested columns, modify the selections to fit your needs, or ignore the suggestions.  
  
-   Predictable columns contain the information that you try to predict in the mining model. You can select multiple columns as the predictable attributes. Clustering models are the exception in that a predictable attribute is optional.  
  
     Depending on the model type, the predictable column might need to be a specific data type: for example, a linear regression model requires a numeric column as the predicted value; Naïve Bayes algorithm requires a discrete value (and all the inputs must be discrete as well).  
  
## Specifying Column Content  
 For some columns, you might also need to specify the *column content*. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data mining, the Content Type property of each data columns tells the algorithm how it should process the data in that column. For example, if your data has an Income column, you must specify that the column contains continuous numbers by setting the content type to Continuous. However, you could also specify that the numbers in the Income column be grouped into buckets by setting the content type to Discretized and optionally specifying the exact number of buckets. You can create different models that handle columns differently: for example, you might try one model that buckets customers into three age groups, and another model that buckets customers into 10 age groups.  
  
## See Also  
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)   
 [Create a Relational Mining Structure](../../analysis-services/data-mining/create-a-relational-mining-structure.md)   
 [Mining Model Properties](../../analysis-services/data-mining/mining-model-properties.md)   
 [Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md)  
  
  
