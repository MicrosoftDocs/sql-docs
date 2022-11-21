---
title: "ALTER MINING STRUCTURE (DMX)"
description: "ALTER MINING STRUCTURE (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# ALTER MINING STRUCTURE (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Creates a new mining model that is based on an existing mining structure.  When you use the **ALTER MINING STRUCTURE** statement to create a new mining model, the structure must already exist. In contrast, when you use the statement, [CREATE MINING MODEL &#40;DMX&#41;](../dmx/create-mining-model-dmx.md), you create a model and automatically generate its underlying mining structure at the same time.  
  
## Syntax  
  
```  
  
ALTER MINING STRUCTURE <structure>  
ADD MINING MODEL <model>  
(  
    <column definition list>  
  [(<nested column definition list>) [WITH FILTER (<nested filter criteria>)]]  
)  
USING <algorithm> [(<parameter list>)]   
[WITH DRILLTHROUGH]  
[,FILTER(<filter criteria>)]  
```  
  
## Arguments  
 *structure*  
 The name of the mining structure to which the mining model will be added.  
  
 *model*  
 A unique name for the mining model.  
  
 *column definition list*  
 A comma-separated list of column definitions.  
  
 *nested column definition list*  
 A comma-separated list of columns from a nested table, if applicable.  
  
 *nested filter criteria*  
 A filter expression that is applied to the columns in a nested table.  
  
 *algorithm*  
 The name of a data mining algorithm, as defined by the provider.  
  
> [!NOTE]  
>  A list of the algorithms supported by the current provider can be retrieved by using [DMSCHEMA_MINING_SERVICES Rowset](/previous-versions/sql/sql-server-2012/ms126251(v=sql.110)). To view the algorithms supported in the current instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], see [Data Mining Properties](/analysis-services/server-properties/data-mining-properties).  
  
 *parameter list*  
 Optional. A comma-separated list of provider-defined parameters for the algorithm.  
  
 *filter criteria*  
 A filter expression that is applied to the columns in the case table.  
  
## Remarks  
 If the mining structure contains composite keys, the mining model must include all the key columns that are defined in the structure.  
  
 If the model does not require a predictable column, for example, models that are built by using the [!INCLUDE[msCoName](../includes/msconame-md.md)] Clustering and [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Clustering algorithms, you do not have to include a column definition in the statement. All the attributes in the resulting model will be treated as inputs.  
  
 In the **WITH** clause that applies to the case table, you can specify options for both filtering and drillthrough:  
  
-   Add the **FILTER** keyword and a filter condition. The filter applies to the cases in the mining model.  
  
-   Add the **DRILLTHROUGH** keyword to enable users of the mining model to drill down from model results to the case data. In Data Mining Extensions (DMX), drillthrough can be enabled only when you create the model.  
  
 To use both case filtering and drillthrough, you combine the keywords in a single **WITH** clause by using the syntax shown in the following example:  
  
 `WITH DRILLTHROUGH, FILTER(Gender = 'Male')`  
  
## Column Definition List  
 You define the structure of a model by specifying a column definition list that includes the following information for each column:  
  
-   Name (mandatory)  
  
-   Alias (optional)  
  
-   Modeling flags  
  
-   Prediction request, which indicates to the algorithm whether the column contains a predictable value, indicated by the **PREDICT** or **PREDICT_ONLY** clause  
  
 Use the following syntax for the column definition list to define a single column:  
  
```  
<structure column name>  [AS <model column name>]  [<modeling flags>]    [<prediction>]  
```  
  
### Column Name and Alias  
 The column name that you use in the column definition list must be the name of the column as it is used in the mining structure. However, you can optionally define an alias to represent the structure column in the mining model. You can also create multiple column definitions for the same structure column, and assign a different alias and prediction usage to each copy of the column. By default, the structure column name is used if you do not define an alias. For more information, see [Create an Alias for a Model Column](/analysis-services/data-mining/create-an-alias-for-a-model-column).  
  
 For nested table columns, you specify the name of the nested table, specify the data type as **TABLE**, and then provide the list of nested columns to include in the model, enclosed in parentheses.  
  
 You can define a filter expression that is applied to the nested table by affixing a filter criteria expression after the nested table column definition.  
  
### Modeling Flags  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports the following modeling flags for use in mining model columns:  
  
> [!NOTE]  
>  The NOT_NULL modeling flag applies to the mining structure column. For more information, see [CREATE MINING STRUCTURE &#40;DMX&#41;](../dmx/create-mining-structure-dmx.md).  
  
|Term|Definition|  
|-|-|  
|**REGRESSOR**|Indicates that the algorithm can use the specified column in the regression formula of regression algorithms.|  
|**MODEL_EXISTENCE_ONLY**|Indicates that the values for the attribute column are less important than the presence of the attribute.|  
  
 You can define multiple modeling flags for a column. For more information about how to use modeling flags, see [Modeling Flags &#40;DMX&#41;](../dmx/modeling-flags-dmx.md).  
  
### Prediction Clause  
 The prediction clause describes how the prediction column is used. The following table lists the possible clauses.  
  
|Clause|Description|  
|-|-|  
|**PREDICT**|This column can be predicted by the model, and its values can be used as input to predict the value of other predictable columns.|  
|**PREDICT_ONLY**|This column can be predicted by the model, but its values cannot be used in input cases to predict the value of other predictable columns.|  
  
## Filter Criteria Expressions  
 You can define a filter that restricts the cases that are used in the mining model. The filter can be applied to either the columns in the case table or the rows in the nested table, or to both.  
  
 Filter criteria expressions are simplified DMX predicates, similar to a WHERE clause. Filter expressions are restricted to formulas that use basic mathematical operators, scalars, and column names. The exception is the EXISTS operator; it evaluates to true if at least one row is returned for the subquery. Predicates can be combined by using the common logical operators: AND, OR, and NOT.  
  
 For more information about filters used with mining models, see [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining).  
  
> [!NOTE]  
>  Columns in a filter must be mining structure columns. You cannot create a filter on a model column or an aliased column.  
  
 For more information about DMX operators and syntax, see [Mining Model Columns](/analysis-services/data-mining/mining-model-columns).  
  
## Parameter Definition List  
 You can adjust the performance and functionality of a model by adding algorithm parameters to the parameter list. The parameters that you can use depend on the algorithm that you specify in the USING clause. For a list of parameters that are associated with each algorithm, see [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining).  
  
 The syntax of the parameter list is as follows:  
  
```  
[<parameter> = <value>, <parameter> = <value>,...]  
```  
  
## Example 1: Add a Model to a Structure  
 The following example adds a Naive Bayes mining model to the **New Mailing** mining structure and limits the maximum number of attribute states to 50.  
  
```  
ALTER MINING STRUCTURE [New Mailing]  
ADD MINING MODEL [Naive Bayes]  
(  
    CustomerKey,   
    Gender,  
    [Number Cars Owned],  
    [Bike Buyer] PREDICT  
)  
USING Microsoft_Naive_Bayes (MAXIMUM_STATES = 50)  
```  
  
## Example 2: Add a Filtered Model to a Structure  
 The following example adds a mining model, `Naive Bayes Women`, to the **New Mailing** mining structure. The new model has the same basic structure as the mining model that was added in example 1; however, this model restricts the cases from the mining structure to female customers over the age of 50.  
  
```  
ALTER MINING STRUCTURE [New Mailing]  
ADD MINING MODEL [Naive Bayes Women]  
(  
    CustomerKey,   
    Gender,  
    [Number Cars Owned],  
    [Bike Buyer] PREDICT  
)  
USING Microsoft_Naive_Bayes  
WITH FILTER([Gender] = 'F' AND [Age] >50)  
```  
  
## Example 3: Add a Filtered Model to a Structure with a Nested Table  
 The following example adds a mining model to a modified version of the market basket mining structure. The mining structure used in the example has been modified to add a **Region** column, which contains attributes for customer region, and an **Income Group** column, which categorizes customer income by using the values **High**, **Moderate**, or **Low**.  
  
 The mining structure also includes a nested table that lists the items that the customer has purchased.  
  
 Because the mining structure contains a nested table, you can define a filter on the case table, the nested table, or both. This example combines a case filter and nested row filter to restrict the cases to wealthy European customers who purchased one of the road tire models.  
  
```  
ALTER MINING STRUCTURE [Market Basket with Region and Income]  
ADD MINING MODEL [Decision Trees]  
(  
    CustomerKey,   
    Region,  
    [Income Group],  
    [Product] PREDICT (Model)   
WITH FILTER (EXISTS (SELECT * FROM [v Assoc Seq Line Items] WHERE   
 [Model] = 'HL Road Tire' OR  
 [Model] = 'LL Road Tire' OR  
 [Model] = 'ML Road Tire' )  
)  
) WITH FILTER ([Income Group] = 'High' AND [Region] = 'Europe')  
USING Microsoft_Decision Trees  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
