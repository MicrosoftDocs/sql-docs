---
title: "Modeling Flags (Data Mining) | Microsoft Docs"
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
# Modeling Flags (Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can use modeling flags in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to provide additional information to a data mining algorithm about the data that is defined in a case table. The algorithm can use this information to build a more accurate data mining model.  
  
 Some modeling flags are defined at the level of the mining structure, whereas others are defined at the level of the mining model column. For example, the **NOT NULL** modeling flag is used with mining structure columns. You can define additional modeling flags on the mining model columns, depending on the algorithm you use to create the model.  
  
> [!NOTE]  
>  Third-party plug-ins might have other modeling flags, in addition to those pre-defined by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
## List of Modeling Flags  
 The following list describes the modeling flags that are supported in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For information about modeling flags that are supported by specific algorithms, see the technical reference topic for the algorithm that was used to create the model.  
  
 **NOT NULL**  
 Indicates that the values for the attribute column should never contain a null value. An error will result if [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] encounters a null value for this attribute column during the model training process.  
  
 **MODEL_EXISTENCE_ONLY**  
 Indicates that the column will be treated as having two states: **Missing** and **Existing**. If the value is **NULL**, it is treated as Missing. The MODEL_EXISTENCE_ONLY flag is applied to the predictable attribute and is supported by most algorithms.  
  
 In effect, setting the MODEL_EXISTENCE_ONLY flag to **True** changes the representation of the values such that there are only two states: **Missing** and **Existing**. All the non-missing states are combined into a single **Existing** value.  
  
 A typical use for this modeling flag would be in attributes for which the **NULL** state has an implicit meaning, and the explicit value of the **NOT NULL** state might not be as important as the fact that the column has any value. For example, a [DateContractSigned] column might be **NULL** if a contract was never signed and **NOT NULL** if the contract was signed. Therefore, if the purpose of the model is to predict whether a contract will be signed, you can use the MODEL_EXISTENCE_ONLY flag to ignore the exact date value in the **NOT NULL** cases and distinguish only between cases where a contract is **Missing** or **Existing**.  
  
> [!NOTE]  
>  Missing is a special state used by the algorithm, and differs from the text value "Missing" in a column. For more information, see [Missing Values &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/missing-values-analysis-services-data-mining.md).  
  
 **REGRESSOR**  
 Indicates that the column is a candidate for used as a regressor during processing. This flag is defined on a mining model column, and can only be applied to columns that have a continuous numeric data type. For more information about the use of this flag, see the section in this topic, [Uses of the REGRESSOR Modeling Flag](#bkmk_UseRegressors).  
  
## Viewing and Changing Modeling Flags  
 You can view the modeling flags associated with a mining structure column or model column in Data Mining Designer by viewing the properties of the structure or model.  
  
 To determine which modeling flags have been applied to the current mining structure, you can create a query against the data mining schema rowset that returns the modeling flags for just the structure columns, by using a query like the following:  
  
```  
SELECT COLUMN_NAME, MODELING_FLAG  
FROM $system.DMSCHEMA_MINING_STRUCTURE_COLUMNS  
WHERE STRUCTURE_NAME = '<structure name>'  
```  
  
 You can add or change the modeling flags used in a model by using the Data Mining Designer and editing the properties of the associated columns. Such changes require that the structure or model be reprocessed.  
  
 You can specify modeling flags in a new mining structure or mining model by using DMX, or by using AMO or XMLA scripts. However, you cannot change the modeling flags used in an existing mining model and structure by using DMX. You must create a new mining model by using the syntax, `ALTER MINING STRUCTURE....ADD MINING MODEL`.  
  
##  <a name="bkmk_UseRegressors"></a> Uses of the REGRESSOR Modeling Flag  
 When you set the REGRESSOR modeling flag on a column, you are indicating to the algorithm that the column contains potential regressors. The actual regressors that are used in the model are determined by the algorithm. A potential regressor can be discarded if it does not model the predictable attribute.  
  
 When you build a model by using the Data Mining wizard, all continuous input columns are flagged as possible regressors. Therefore, even if you do not explicitly set the REGRESSOR flag on a column, the column might be used as a regressor in the model.  
  
 You can determine the regressors that were actually used in the processed model by performing a query against the schema rowset for the mining model, as shown in the following example:  
  
```  
SELECT COLUMN_NAME, MODELING_FLAG  
FROM $system.DMSCHEMA_MINING_COLUMNS  
WHERE MODEL_NAME = '<model name>'  
```  
  
 **Note** If you modify a mining model and change the content type of a column from continuous to discrete, you must manually change the flag on the mining column and then reprocess the model.  
  
### Regressors in Linear Regression Models  
 Linear regression models are based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm. Even if you do not use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm, any decision tree model can contain a tree or nodes that represents a regression on a continuous attribute.  
  
 Therefore, in these models you do not need to specify that a continuous column represents a regressor. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm will partition the dataset into regions with meaningful patterns even if you do not set the REGRESSOR flag on the column. The difference is that when you set the modeling flag, the algorithm will try to find regression equations of the following form to fit the patterns in the nodes of  the tree.  
  
 a*C1 + b\*C2 + ...  
  
 Then, the sum of the residuals is calculated, and if the deviation is too great, a split is forced in the tree.  
  
 For example, if you are predicting customer purchasing behavior using **Income** as an attribute, and set the REGRESSOR modeling flag on the column, the algorithm would first try to fit the **Income** values by using a standard regression formula. If the deviation is too great, the regression formula is abandoned and the tree would be split on some other attribute. The decision tree algorithm would then try fit a regressor for income in each of the branches after the split.  
  
 You can use the FORCE_REGRESSOR parameter to guarantee that the algorithm will use a particular regressor. This parameter can be used with the Decision Trees algorithm and Linear Regression algorithm.  
  
## Related Tasks  
 Use the following links to learn more about using modeling flags.  
  
|Task|Topic|  
|----------|-----------|  
|Edit modeling flags by using the Data Mining Designer|[View or Change Modeling Flags &#40;Data Mining&#41;](../../analysis-services/data-mining/view-or-change-modeling-flags-data-mining.md)|  
|Specify a hint to the algorithm to recommend likely regressors|[Specify a Column to Use as Regressor in a Model](../../analysis-services/data-mining/specify-a-column-to-use-as-regressor-in-a-model.md)|  
|See the modeling flags supported by specific algorithms (in the Modeling Flags section for each algorithm reference topic)|[Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)|  
|Learn more about mining structure columns and the properties that you can set on them|[Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md)|  
|Learn about mining model columns and modeling flags that can be applied at the model level|[Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md)|  
|See syntax for  working with modeling  flags in DMX statements|[Modeling Flags &#40;DMX&#41;](../../dmx/modeling-flags-dmx.md)|  
|Understand missing values and how to work with  them|[Missing Values &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/missing-values-analysis-services-data-mining.md)|  
|Learn about managing models and structures and setting usage properties|[Moving Data Mining Objects](../../analysis-services/data-mining/moving-data-mining-objects.md)|  
  
  
