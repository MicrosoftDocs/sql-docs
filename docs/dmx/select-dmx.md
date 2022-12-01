---
title: "SELECT (DMX)"
description: "SELECT (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# SELECT (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  The **SELECT** statement in Data Mining Extensions (DMX) is used for the following tasks in data mining:  
  
-   Browsing the content of an existing mining model  
  
-   Creating predictions from an existing mining model  
  
-   Creating a copy of an existing mining model  
  
-   Browsing the mining structure  
  
 Although the full syntax of this statement is complex, the primary clauses used for browsing a model and its underlying structure can be summarized as follows:  
  
```  
SELECT [FLATTENED] [TOP <n>] <select list>  
FROM <model/structure>[.aspect]  
[WHERE <condition expression>]  
[ORDER BY <expression>[DESC|ASC]]  
```  
  
## FLATTENED  
 Some data mining clients cannot accept result sets in hierarchical format from a data mining provider. The client may lack the ability to handle a hierarchy, or it may have to store the results in a single denormalized table. To convert the data from nested tables to flattened tables, you must request that the query results be flattened.  
  
 To flatten the query results, use the **SELECT** syntax with the **FLATTENED** option, as shown in the following example:  
  
```  
SELECT FLATTENED <select list> FROM ...  
```  
  
## TOP \<n> and ORDER BY  
 You can order the results of a query by using an expression, and can then return a subset of the results by using a combination of the **ORDER BY** and **TOP** clauses. This is useful in a scenario such as targeted mailing where you only want to send results to the most likely respondents. You could order the results of a target mailing prediction query by the prediction probability, and then only return the top \<n> results.  
  
## Select List  
 The *\<select list>* can include scalar column references, prediction functions, and expressions. The options that are available depend on the algorithm, and the following contexts:  
  
-   Whether you are querying a mining structure or a mining model  
  
-   Whether you are querying content or cases  
  
-   Whether source data is a relational table or a cube  
  
-   If you are making predictions  
  
 In many cases, you can use aliases, or create simple expressions based on the items in the select list. For example, the following example shows a simple expression on model columns:  
  
```  
SELECT [CustomerID], [Last Name] + ', ' + [FirstName] AS FullName  
FROM <model>.CASES  
```  
  
 The following example creates an alias for a column that contains the results of a prediction function:  
  
```  
SELECT Predict([Column1], 'Value') as Column1Prediction  
FROM MyModel  
JOIN <source data query>  
```  
  
## WHERE  
 You can limit the cases that are returned by the query by using a **WHERE** clause. The **WHERE** clause specifies that column references in the **WHERE** expression must have the same semantics as column references in the *\<select list>* of the **SELECT** statement, and can only return a Boolean expression. The syntax for the **WHERE** clause is as follows  
  
```  
WHERE < condition expression >  
```  
  
 The select list and **WHERE** clause of a **SELECT** statement must follow the following rules:  
  
-   The select list must contain an expression that does not return a Boolean result. You can modify the expression, but the expression must return non-Boolean results.  
  
-   The **WHERE** clause must contain an expression that returns a Boolean result. You can modify the clause, but it must return a Boolean result.  
  
## Predictions  
 There are two types of syntax that you can use for creating predictions:  
  
-   [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md)  
  
-   [SELECT FROM &#60;model&#62; &#40;DMX&#41;](../dmx/select-from-model-dmx.md)  
  
 The first type of prediction enables you create complex predictions either in real time or as a batch.  
  
 The second prediction type creates an empty prediction join on a predictable column in a mining model, and returns the most likely state of the column. The results of this query are completely based on the content of the mining model.  
  
 You can insert a select statement into the source query of a SELECT FROM PREDICTION JOIN statement by using the following syntax.  
  
```  
SELECT FROM PREDICTION JOIN (<SELECT statement>) AS t, WHERE <SELECT statement>  
```  
  
 For more information about creating prediction queries, see [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md).  
  
## Clause Syntax  
 Because of the complexity of browsing with the **SELECT** statement, detailed syntax elements and arguments are described by clause. For more information about each clause, click a topic in the following list:  
  
 [SELECT DISTINCT FROM &#60;model &#62; &#40;DMX&#41;](../dmx/select-distinct-from-model-dmx.md)  
  
 [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](../dmx/select-from-model-content-dmx.md)  
  
 [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md)  
  
 [SELECT FROM &#60;model&#62;.SAMPLE_CASES &#40;DMX&#41;](../dmx/select-from-model-sample-cases-dmx.md)  
  
 [SELECT FROM &#60;model&#62;.DIMENSION_CONTENT &#40;DMX&#41;](../dmx/select-from-model-dimension-content-dmx.md)  
  
 [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md)  
  
 [SELECT FROM &#60;model&#62; &#40;DMX&#41;](../dmx/select-from-model-dmx.md)  
  
 [SELECT FROM &#60;structure&#62;.CASES](../dmx/select-from-structure-cases.md)  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)  
  
  
