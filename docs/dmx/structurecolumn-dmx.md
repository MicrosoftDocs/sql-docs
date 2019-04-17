---
title: "StructureColumn (DMX) | Microsoft Docs"
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
# StructureColumn (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns the value of the structure column corresponding to the specified case, or the table value for a nested table in the specified case.  
  
## Syntax  
  
```  
  
StructureColumn('structure column name')  
```  
  
## Arguments  
 structure-column-name.  
 The name of a case or nested table mining structure column.  
  
## Result Type  
 The type that is returned depends on the type of the column that is referenced in the \<structure column name> parameter. For example, if the mining structure column that is referenced contains a scalar value, the function returns a scalar value.  
  
 If the mining structure column that is referenced is a nested table, the function returns a table value. The returned table value can be used in the FROM clause of a sub-SELECT statement.  
  
## Remarks  
 This function is polymorphic and can be used anywhere in a statement that allows expressions, including a SELECT expression list, a WHERE condition expression, and an ORDER BY expression.  
  
 The name of the column in the mining structure is a string value and as such must be enclosed in single quotation marks: for example, `StructureColumn('`**column 1**`')`. If there are multiple columns that have the same name, the name is resolved in the context of the enclosing SELECT statement.  
  
 The results that are returned from a query using the **StructureColumn** function are affected by the presence of any filters on the model. That is to say, the model filter controls the cases that are included in the mining model. Therefore, a query on the structure column can return only those cases that were used in the mining model. See the Examples section of this topic for a code sample that shows the effect of mining model filters on both case tables and a nested table.  
  
 For more information about how to use this function in a DMX SELECT statement, see [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md) or [SELECT FROM &#60;structure&#62;.CASES](../dmx/select-from-structure-cases.md).  
  
## Error Messages  
 The following security error is raised if the user does not have drillthrough permission on the parent mining structure:  
  
 The '%{user/}' user does not have permission to drill through to the parent mining structure of the '%{model/}' mining model.  
  
 The following error message is raised if an invalid structure column name is specified:  
  
 The '%{structure-column-name/}' mining structure column was not found in the '%{structure/}' parent mining structure in the current context (line %{line/}, column %{column/}).  
  
## Examples  
 We will use the following mining structure for these examples. Note that the mining structure contains two nested table columns, `Products` and `Hobbies`. The nested table in the `Hobbies` column has a single column that is used as the key for the nested table. The nested table in the `Products` column is a complex nested table that has both a key column and other columns used for input. The following examples illustrate how a data mining structure can be designed to include many different columns, even though a model may not use every column. Some of these columns may not be useful at the model level for generalizing patterns, but may be very useful for drillthrough.  
  
```  
CREATE MINING STRUCTURE [MyStructure]   
(  
CustomerName TEXT KEY,  
Occupation TEXT DISCRETE,  
Age LONG CONTINUOUS,  
MaritalStatus TEXT DISCRETE,  
Income LONG CONTINUOUS,  
Products  TABLE  
 (  
    ProductNameTEXT KEY,  
    Quantity LONG CONTINUOUS,  
    OnSale BOOLEAN  DISCRETE  
)  
 Hobbies  TABLE  
 (  
    Hobby KEY  
 ))  
```  
  
 Next, create a mining model based on the structure you just created, using the following example code:  
  
```  
ALTER MINING STRUCTURE [MyStructure] ADD MINING MODEL [MyModel] (  
CustomerName,  
Age,  
MaritalStatus,  
Income PREDICT,  
Products   
(  
ProductName  
) WITH FILTER(NOT OnSale)  
) USING Microsoft_Decision_Trees   
WITH FILTER(EXISTS (Products))  
```  
  
### Sample Query 1: Returning a Column from the Mining Structure  
 The following sample query returns the columns `CustomerName` and `Age`, which are defined as being part of the mining model. However, the query also returns the column `Age`, which is part of the structure but not part of the mining model.  
  
```  
SELECT CustomerName, Age, StructureColumn('Occupation') FROM MyModel.CASES   
WHERE Age > 30  
```  
  
 Note that the filtering of rows to restrict cases to customers over the age of 30 takes place at the level of the model. Therefore, this expression would not return cases that are included in the structure data but are not used by the model. Because the filter condition used to create the model (`EXISTS (Products)`) restricts cases to only those customers who purchased products, there might be cases in the structure that are not returned by this query.  
  
### Sample Query 2: Applying a Filter to the Structure Column  
 The following sample query not only returns the model columns `CustomerName` and `Age`, and the nested table `Products`, but also returns the value of the column `Quantity` in the nested table, which is not part of the model.  
  
```  
SELECT CustomerName, Age,  
(SELECT ProductName, StructureColumn('Quantity') FROM Products) FROM MA.CASES   
WHERE StructureColumn('Occupation') = 'Architect'  
```  
  
 Note that, in this example, a filter is applied to the structure column to restrict the cases to customers whose occupation is 'Architect' (`WHERE StructureColumn('Occupation') = 'Architect'`). Because the model filter condition is always applied to the cases when the model is created, only cases that contain at least one qualifying row in the `Products` table are included in the model cases. Therefore, both the filter on the nested table `Products` and the filter on the case `('Occupation')`are applied.  
  
### Sample Query 3: Selecting Columns from a Nested Table  
 The following sample query returns the names of customers who were used as training cases from the model. For each customer, the query also returns a nested table that contains the purchase details. Although the model includes the `ProductName` column, the model does not use the value of the `ProductName` column. The model only checks if the product was purchased at regular (`NOT``OnSale`) price. This query not only returns the product name, but also returns the quantity purchased, which is not included in the model.  
  
```  
SELECT CustomerName,    
(SELECT ProductName, StructureColumn('Quantity')FROM Products)   
FROM MyModel.CASES  
```  
  
 Note that you cannot return either the `ProductName` column or the `Quantity` column unless drillthrough is enabled on the mining model.  
  
### Sample Query 4: Filtering on and Returning Nested Table Columns  
 Thee following sample query returns the case and nested table columns that are included in the mining structure but not in the model. The model is already filtered on the presence of `OnSale` products, but this query adds a filter on the mining structure column, `Quantity`:  
  
```  
SELECT CustomerName, Age, StructureColumn('Occupation'),   
(SELECT ProductName, StructureColumn('Quantity') FROM Products)   
FROM MyModel.CASES   
WHERE EXISTS (SELECT * FROM Products WHERE StructureColumn('Quantity')>1)  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
