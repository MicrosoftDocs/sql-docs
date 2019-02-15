---
title: "SELECT FROM &lt;model&gt;.CONTENT (DMX) | Microsoft Docs"
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
# SELECT FROM &lt;model&gt;.CONTENT (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the mining model schema rowset for the specified data mining model.  
  
## Syntax  
  
```  
  
SELECT [FLATTENED] [TOP <n>] <expression list> FROM <model>.CONTENT   
[WHERE <condition expression>]  
[ORDER BY <expression> [DESC|ASC]]  
```  
  
## Arguments  
 *n*  
 Optional. An integer that specifies how many rows to return.  
  
 *expression list*  
 A comma-separated list of columns derived from the Content schema rowset.  
  
 *model*  
 A model identifier.  
  
 *condition expression*  
 Optional. A condition to restrict the values that are returned from the column list.  
  
 *expression*  
 Optional. An expression that returns a scalar value.  
  
## Remarks  
 The **SELECT FROM** _\<model>_**.CONTENT** statement returns content that is specific to each algorithm. For example, you might want to use the descriptions of all the rules of an association rules model in a custom application. You can use a **SELECT FROM \<model>.CONTENT** statement to return values in the NODE_RULE column of the model.  
  
 The following table lists the columns that are included in the mining model content.  
  
> [!NOTE]  
>  Algorithms might interpret the columns differently in order to correctly represent the content. For a description of the mining model content for each algorithm, and tips on how to interpret and query the mining model content for each model type, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
|CONTENT rowset column|Description|  
|---------------------------|-----------------|  
|MODEL_CATALOG|A catalog name. NULL if the provider does not support catalogs.|  
|MODEL_SCHEMA|An unqualified schema name. NULL if the provider does not support schemas.|  
|MODEL_NAME|A model name. This column cannot contain a NULL.|  
|ATTRIBUTE_NAME|The name of the attribute that corresponds to the node.|  
|NODE_NAME|The name of the node.|  
|NODE_UNIQUE_NAME|The unique name of the node within the model.|  
|NODE_TYPE|An integer that represents the type of the node. .|  
|NODE_GUID|The node GUID. NULL if no GUID.|  
|NODE_CAPTION|A label or a caption that is associated with the node. Used primarily for display purposes. If a caption does not exist, NODE_NAME is returned.|  
|CHILDREN_CARDINALITY|The number of children that the node has.|  
|PARENT_UNIQUE_NAME|The unique name of the node's parent.|  
|NODE_DESCRIPTION|A description of the node.|  
|NODE_RULE|An XML fragment that represents the rule embedded in the node. The format of the XML string is based on the PMML standard.|  
|MARGINAL_RULE|An XML fragment that describes the path from the parent to the node.|  
|NODE_PROBABILITY|The probability of the path that ends in the node.|  
|MARGINAL_PROBABILITY|The probability of reaching the node from the parent node.|  
|NODE_DISTRIBUTION|A table that contains statistics that describe the distribution of values in the node.|  
|NODE_SUPPORT|The number of cases in support of this node.|  
  
## Examples  
 The following code returns the ID of the parent node for the decision trees model that was added to the Targeted Mailing mining structure.  
  
```  
SELECT MODEL_NAME, NODE_NAME FROM [TM Decision Tree].CONTENT  
WHERE NODE_TYPE = 1  
```  
  
 Expected results:  
  
|MODEL_NAME|NODE_NAME|  
|-----------------|----------------|  
|TM_DecisionTree|0|  
  
 The following query uses the **IsDescendant** function to return the immediate children of the node that was returned in the previous query.  
  
> [!NOTE]  
>  Because the value of the NODE_NAME is a string, you cannot use a sub-select statement to return the NODE_ID as an argument to the **IsDescendant** function.  
  
```  
SELECT NODE_NAME, NODETYPE, NODE_CAPTION   
FROM [TM Decision Tree].CONTENT  
WHERE ISDESCENDANT('0')  
```  
  
 Expected results:  
  
 Because the model is a decision trees model, the descendants of the model parent node include a single marginal statistics node, a node that represents the predictable attribute, and multiple nodes that contain input attributes and values. For more information, see [Mining Model Content for Decision Tree Models &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/mining-model-content-for-decision-tree-models-analysis-services-data-mining.md).  
  
## Using the FLATTENED Keyword  
 The mining model content frequently contains interesting information about the model in nested table columns. The FLATTENED keyword lets you retrieve data from a nested table column without using a provider that supports hierarchical rowsets.  
  
 The following query returns a single node, the marginal statistics node (NODE_TYPE = 26) from a Naïve Bayes model. However, this node contains a nested table, in the NODE_DISTRIBUTION column. As a result, the nested table column is flattened and a row is returned for every row in the nested table. The value of the scalar column MODEL_NAME is repeated for each row in the nested table.  
  
 Also, notice that if you specify only the name of the nested table column, a new column is returned for each column in the nested table. By default, the name of the nested table is prefixed to the name of each nested table column.  
  
```  
SELECT FLATTENED MODEL_NAME, NODE_DISTRIBUTION  
FROM [TM_NaiveBayes].CONTENT  
WHERE NODE_TYPE = 26  
```  
  
 Example results:  
  
|MODEL_NAME|NODE_DISTRIBUTION.ATTRIBUTE_NAME|NODE_DISTRIBUTION.ATTRIBUTE_VALUE|NODE_DISTRIBUTION.SUPPORT|NODE_DISTRIBUTION.PROBABILITY|NODE_DISTRIBUTION.VARIANCE|NODE_DISTRIBUTION.VALUETYPE|  
|-----------------|----------------------------------------|-----------------------------------------|--------------------------------|------------------------------------|---------------------------------|----------------------------------|  
|TM_NaiveBayes|Bike Buyer|Missing|0|0|0|1|  
|TM_NaiveBayes|Bike Buyer|0|6556|0.506685215240745|0||  
|TM_NaiveBayes|Bike Buyer|1|6383|0.493314784759255|0||  
  
 The following example demonstrates how to return only some of the columns from the nested table by using a sub-select statement. You can simplify the display by aliasing the table name of the nested table, as shown.  
  
```  
SELECT MODEL_NAME,   
(SELECT ATTRIBUTE_NAME, ATTRIBUTE_VALUE, [SUPPORT] AS t  
FROM NODE_DISTRIBUTION)   
FROM TM_NaiveBayes.CONTENT  
WHERE NODE_TYPE = 26  
```  
  
 Example results:  
  
|MODEL_NAME|t.ATTRIBUTE_NAME|t.ATTRIBUTE_VALUE|t.SUPPORT|  
|-----------------|-----------------------|------------------------|---------------|  
|TM_NaiveBayes|Bike Buyer|Missing|0|  
|TM_NaiveBayes|Bike Buyer|0|6556|  
|TM_NaiveBayes|Bike Buyer|1|6383|  
  
## See Also  
 [SELECT &#40;DMX&#41;](../dmx/select-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
