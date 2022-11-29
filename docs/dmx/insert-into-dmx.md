---
title: "INSERT INTO (DMX)"
description: "INSERT INTO (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# INSERT INTO (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Processes the specified data mining object. For more information about processing mining models and mining structures, see [Processing Requirements and Considerations &#40;Data Mining&#41;](/analysis-services/data-mining/processing-requirements-and-considerations-data-mining).  
  
 If a mining structure is specified, the statement processes the mining structure and all its associated mining models. If a mining model is specified, the statement processes just the mining model.  
  
## Syntax  
  
```  
  
INSERT INTO [MINING MODEL]|[MINING STRUCTURE] <model>|<structure> (<mapped model columns>) <source data query>  
INSERT INTO [MINING MODEL]|[MINING STRUCTURE] <model>|<structure>.COLUMN_VALUES (<mapped model columns>) <source data query>  
```  
  
## Arguments  
 *model*  
 A model identifier.  
  
 *structure*  
 A structure identifier.  
  
 *mapped model columns*  
 A comma-separated list of column identifiers and nested identifiers.  
  
 *source data query*  
 The source query in the provider-defined format.  
  
## Remarks  
 If you do not specify **MINING MODEL** or **MINING STRUCTURE**, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] searches for the object type based on the name, and processes the correct object. If the server contains a mining structure and a mining model that have the same name, an error is returned.  
  
 By using the second syntax form, INSERT INTO*\<object>*.COLUMN_VALUES, you can insert data directly into the model columns without training the model. This method provides column data to the model in a concise, ordered manner that is useful when you work with datasets that contain hierarchies or ordered columns.  
  
 If you use **INSERT INTO** with a mining model or a mining structure, and leave off the \<mapped model columns> and \<source data query> arguments, the statement behaves like **ProcessDefault**, using bindings that already exist. If bindings do not exist, the statement returns an error. For more information about **ProcessDefault**, see [Processing Options and Settings &#40;Analysis Services&#41;](/analysis-services/multidimensional-models/processing-options-and-settings-analysis-services). The following example shows the syntax:  
  
```  
INSERT INTO [MINING MODEL] <model>  
```  
  
 If you specify **MINING MODEL** and provide mapped columns and a source data query, the model and associated structure is processed.  
  
 The following table provides a description of the result of different forms of the statement, depending on the state of the objects.  
  
|Statement|State of objects|Result|  
|---------------|----------------------|------------|  
|INSERT INTO MINING MODEL*\<model>*|Mining structure is processed.|Mining model is processed.|  
||Mining structure is unprocessed.|Mining model and mining structure are processed.|  
||Mining structure contains additional mining models.|Process fails. You must reprocess the structure, and the associated mining models.|  
|INSERT INTO MINING STRUCTURE*\<structure>*|Mining structure is processed or unprocessed.|Mining structure and associated mining models are processed.|  
|INSERT INTO MINING MODEL*\<model>* that contains a source query<br /><br /> or<br /><br /> INSERT INTO MINING STRUCTURE*\<structure>* that contains a source query|Either the structure or the model already contains content.|Process fails. You must clear the objects before you perform this operation, by using [DELETE &#40;DMX&#41;](../dmx/delete-dmx.md).|  
  
## Mapped Model Columns  
 By using the \<mapped model columns> element, you can map the columns from the data source to the columns in your mining model. The \<mapped model columns> element has the following form:  
  
```  
<column identifier> | SKIP | <table identifier> (<column identifier> | SKIP), ...  
```  
  
 By using **SKIP**, you can exclude certain columns that must exist in the source query, but that do not exist in the mining model. SKIP is useful when you do not have control over the columns that are included in the input rowset. If you are writing your own OPENQUERY, the better practice is to omit the column from the SELECT column list instead of using SKIP.  
  
 SKIP is also useful when a column from the input rowset is needed to perform a join, but the column is not used by the mining structure. A typical example of this is a mining structure and mining model that contain a nested table. The input rowset for this structure will have a foreign key column that is used to create a hierarchical rowset using the SHAPE clause, but the foreign key column is almost never used in the model.  
  
 The syntax for SKIP requires that you insert SKIP at the position of the individual column in the input rowset that has no corresponding mining structure column. For example, in the nested table example below, OrderNumber must be selected in the APPEND clause so that it can be used in the RELATE clause to specify the join; however, you do not want to insert the OrderNumber data into the nested table in the mining structure. Therefore, the example uses the SKIP keyword instead of OrderNumber in the INSERT INTO argument.  
  
## Source Data Query  
 The \<source data query> element can include the following data source types:  
  
-   **OPENQUERY**  
  
-   **OPENROWSET**  
  
-   **SHAPE**  
  
-   Any [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] query that returns a rowset  
  
 For more information about data source types, see [&#60;source data query&#62;](../dmx/source-data-query.md).  
  
## Basic Example  
 The following example uses **OPENQUERY** to train a Naive Bayes model based on the targeted mailing data in the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database.  
  
```  
INSERT INTO NBSample (CustomerKey, Gender, [Number Cars Owned],  
    [Bike Buyer])  
OPENQUERY([AdventureWorksDW2012],'Select CustomerKey, Gender, [NumberCarsOwned], [BikeBuyer]   
FROM [vTargetMail]')  
```  
  
## Nested Table Example  
 The following example uses **SHAPE** to train an association mining model that contains a nested table. Note that the fist line contains **SKIP** instead OrderNumber, which is required in the **SHAPE_APPEND** statement but is not used in the mining model.  
  
```  
INSERT INTO MyAssociationModel  
    ([OrderNumber],[Models] (SKIP, [Model])  
    )  
SHAPE {  
    OPENQUERY([AdventureWorksDW2012],'SELECT OrderNumber  
    FROM vAssocSeqOrders ORDER BY OrderNumber')  
} APPEND (  
    {OPENQUERY([AdventureWorksDW2012],'SELECT OrderNumber, model FROM   
    dbo.vAssocSeqLineItems ORDER BY OrderNumber, Model')}  
  RELATE OrderNumber to OrderNumber)   
AS [Models]  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
