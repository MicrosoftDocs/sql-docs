---
title: "CREATE MINING STRUCTURE (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# CREATE MINING STRUCTURE (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Creates a new mining structure in a database and optionally defines training and testing partitions. After you have created the mining structure, you can use the [ALTER MINING STRUCTURE &#40;DMX&#41;](../dmx/alter-mining-structure-dmx.md) statement to add models to the mining structure.  
  
## Syntax  
  
```  
  
CREATE [SESSION] MINING STRUCTURE <structure>  
(  
    [(<column definition list>)]  
)  
[WITH HOLDOUT (<holdout-specifier> [OR <holdout-specifier>])]  
[REPEATABLE(<holdout seed>)]  
<holdout-specifier>::=  <holdout-maxpercent> PERCENT | <holdout-maxcases> CASES  
```  
  
## Arguments  
 *structure*  
 A unique name for the structure.  
  
 *column definition list*  
 A comma-separated list of column definitions.  
  
 *holdout-maxpercent*  
 An integer between 1 and 100 that indicates the percentage of data to set aside for testing.  
  
 *holdout-maxcases*  
 An integer that indicates the maximum number of cases to use for testing.  
  
 If the value specified for max cases is larger than the number of input cases, all input cases are used for testing and a warning will be raised.  
  
> [!NOTE]  
>  If both percentage and maximum number of cases is specified, the smaller of the two limits is used.  
  
 *holdout seed*  
 An integer used as the seed to start partitioning data.  
  
 If set to 0, the hash of the mining structure ID is used as the seed.  
  
> [!NOTE]  
>  You should specify a seed if you need to ensure that a partition can be reproduced.  
  
 Default: REPEATABLE(0)  
  
## Remarks  
 You define a mining structure by specifying a list of columns, optionally specifying hierarchical relationships between the columns, and then optionally partitioning the mining structure into training and testing data sets.  
  
 The optional SESSION keyword indicates that the structure is a temporary structure that you can use only for the duration of the current session. When the session is terminated, the structure, and any models based on the structure, will be deleted. To create temporary mining structures and models, you must first set the database property, AllowSessionMiningModels. For more information, see [Data Mining Properties](../analysis-services/server-properties/data-mining-properties.md).  
  
## Column Definition List  
 You define a mining structure by including the following information for each column in the column definition list:  
  
-   Name (mandatory)  
  
-   Data type (mandatory)  
  
-   Distribution  
  
-   List of modeling flags  
  
-   Content type (mandatory)  
  
-   Relationship to an attribute column (mandatory only if it applies), indicated by the RELATED TO clause  
  
 Use the following syntax for the column definition list to define a single column:  
  
```  
<column name>    <data type>    [<Distribution>]    [<Modeling Flags>]    <Content Type>    [<column relationship>]  
```  
  
 Use the following syntax for the column definition list to define a nested table column:  
  
```  
<column name>    TABLE    ( <column definition list> )  
```  
  
 For a list of the data types, content types, column distributions, and modeling flags that you can use to define a structure column, see the following topics:  
  
-   [Data Types &#40;Data Mining&#41;](../analysis-services/data-mining/data-types-data-mining.md)  
  
-   [Content Types &#40;Data Mining&#41;](../analysis-services/data-mining/content-types-data-mining.md)  
  
-   [Column Distributions &#40;Data Mining&#41;](../analysis-services/data-mining/column-distributions-data-mining.md)  
  
-   [Modeling Flags &#40;Data Mining&#41;](../analysis-services/data-mining/modeling-flags-data-mining.md)  
  
 You can define multiple modeling flags values for a column. However, you can have only one content type and one data type for a column.  
  
### Column Relationships  
 You can add a clause to any column definition statement to describe the relationship between two columns. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports the use of the following \<column relationship> clause.  
  
 **RELATED TO**  
 Indicates a value hierarchy. The target of a RELATED TO column can be a key column in a nested table, a discretely-valued column in the case row, or another column with a RELATED TO clause, which indicates a deeper hierarchy.  
  
## Holdout Parameters  
 When you specify holdout parameters, you create a partition of the structure data. The amount that you specify for holdout is reserved for testing, and the remaining data is used for training. By default, if you create a mining structure by using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], a holdout partition is created for you that contains 30 percent testing data and 70 percent training data. For more information, see [Training and Testing Data Sets](../analysis-services/data-mining/training-and-testing-data-sets.md).  
  
 If you create a mining structure by using Data Mining Extensions (DMX), you must manually specify that a holdout partition be created.  
  
> [!NOTE]  
>  The **ALTER MINING STRUCTURE** statement does not support holdout.  
  
 You can specify up to three holdout parameters. If you specify both a maximum number of holdout cases and a holdout percentage, a percentage of cases are reserved until the maximum cases limit is reached. You specify the percentage of holdout as an integer followed by the **PERCENT** keyword, and specify the maximum number of cases as an integer followed by the **CASES** keyword. You can combine the conditions in any order, as shown in the following examples:  
  
```  
WITH HOLDOUT (20 PERCENT)   
WITH HOLDOUT (2000 CASES)   
WITH HOLDOUT (20 PERCENT OR 2000 CASES)   
WITH HOLDOUT (2000 CASES OR 20 PERCENT)  
```  
  
 The holdout seed controls the starting point of the process that randomly assigns cases to either the training or testing data sets. By setting a holdout seed, you can ensure that the partition can be repeated. If you do not specify a holdout seed, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses the name of the mining structure to create a seed. If you rename the structure, the seed value will change. The holdout seed parameter can be used with either or both of the other holdout parameters.  
  
> [!NOTE]  
>  Because the partition information is cached with the training data, to use holdout, you must ensure that the **CacheMode** property of the mining structure is set to **KeepTrainingData**. This is the default setting in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] for new mining structures. Changing the **CacheMode** property to **ClearTrainingCases** on an existing mining structure that contains a holdout partition will not affect any mining models that have been processed. However, if <xref:Microsoft.AnalysisServices.MiningStructureCacheMode> is not set to **KeepTrainingData**, holdout parameters will have no effect. This means that all the source data will be used for training and no test set will be available. The definition of the partition is cached with the structure; if you clear the cache of training cases, you also clear the cache of test data, and the definition of the holdout set.  
  
## Examples  
 The following examples demonstrate how to create a mining structure with holdout by using DMX.  
  
### Example 1: Adding a Structure with No Training Set  
 The following example creates a new mining structure called `New Mailing` without creating any associated mining models, and without using holdout. To learn how to add a mining model to the structure, see [ALTER MINING STRUCTURE &#40;DMX&#41;](../dmx/alter-mining-structure-dmx.md).  
  
```  
CREATE MINING STRUCTURE [New Mailing]  
(  
    CustomerKey LONG KEY,   
    Gender TEXT DISCRETE,  
    [Number Cars Owned] LONG DISCRETE,  
    [Bike Buyer] LONG DISCRETE   
)  
```  
  
### Example 2: Specifying Holdout Percentage and Seed  
 The following clause can be added after the column definition list to define a data set that can be used for testing all mining models associated with the mining structure. The statement will create a test set that is 25 percent of the total input cases, without a limit on the maximum number of cases. 5000 is used as the seed for creating the partition. When you specify a seed, the same cases will be chosen for the test set each time you process the mining structure, so long as the underlying data does not change.  
  
```  
CREATE MINING STRUCTURE [New Mailing]  
(  
    CustomerKey LONG KEY,   
    Gender TEXT DISCRETE,  
    [Number Cars Owned] LONG DISCRETE,  
    [Bike Buyer] LONG DISCRETE   
)   
WITH HOLDOUT(25 PERCENT) REPEATABLE(5000)  
```  
  
### Example 3: Specifying Holdout Percentage and Max Cases  
 The following clause will create a test set that contains either 25 percent of the total input cases, or 2000 cases, whichever is less. Because 0 is specified as the seed, the name of the mining structure is used to create the seed that is used to begin sampling of the input cases.  
  
```  
CREATE MINING STRUCTURE [New Mailing]  
(  
    CustomerKey LONG KEY,   
    Gender TEXT DISCRETE,  
    [Number Cars Owned] LONG DISCRETE,  
    [Bike Buyer] LONG DISCRETE   
)   
WITH HOLDOUT(25 PERCENT OR 2000 CASES) REPEATABLE(0)  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
