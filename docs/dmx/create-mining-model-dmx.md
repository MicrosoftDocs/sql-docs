---
title: "CREATE MINING MODEL (DMX) | Microsoft Docs"
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
# CREATE MINING MODEL (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Creates both a new mining model and a mining structure in the database. You can create a model either by defining the new model in the statement, or by using the Predictive Model Markup Language (PMML). This second option is for advanced users only.  
  
 The mining structure is named by appending "_structure" to the model name, which ensures that the structure name is unique from the model name.  
  
 To create a mining model for an existing mining structure, use the [ALTER MINING STRUCTURE &#40;DMX&#41;](../dmx/alter-mining-structure-dmx.md) statement.  
  
## Syntax  
  
```  
  
CREATE [SESSION] MINING MODEL <model>  
(  
    [(<column definition list>)]  
)  
USING <algorithm> [(<parameter list>)] [WITH DRILLTHROUGH]  
CREATE MINING MODEL <model> FROM PMML <xml string>  
```  
  
## Arguments  
 *model*  
 A unique name for the model.  
  
 *column definition list*  
 A comma-separated list of column definitions.  
  
 *algorithm*  
 The name of a data mining algorithm, as defined by the current provider.  
  
> [!NOTE]  
>  A list of the algorithms supported by the current provider can be retrieved by using [DMSCHEMA_MINING_SERVICES Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-services-rowset). To view the algorithms supported in the current instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], see [Data Mining Properties](../analysis-services/server-properties/data-mining-properties.md).  
  
 *parameter list*  
 Optional. A comma-separated list of provider-defined parameters for the algorithm.  
  
 *XML string*  
 (For advanced use only.) An XML-encoded model (PMML). The string must be enclosed in single quotation marks (').  
  
 The **SESSION** clause lets you create a mining model that is automatically removed from the server when the connection closes or the session times out. **SESSION** mining models are useful because they do not require the user to be a database administrator, and they only use disk space for as long as the connection is open.  
  
 The **WITH DRILLTHROUGH** clause enables drill through on the new mining model. Drillthrough can only be enabled when you create the model. For some model types, drillthrough is required in order to browse the model in the custom viewer. Drillthrough is not required for prediction or for browsing the model by using the Microsoft Generic Content Tree Viewer.  
  
 The **CREATE MINING MODEL** statement creates a new mining model that is based on the column definition list, the algorithm, and the algorithm parameter list.  
  
### Column Definition List  
 You define the structure of a model that uses the column definition list by including the following information for each column:  
  
-   Name (mandatory)  
  
-   Data type (mandatory)  
  
-   Distribution  
  
-   List of modeling flags  
  
-   Content type (mandatory)  
  
-   Prediction request, which indicates to the algorithm to predict this column, indicated by the **PREDICT** or **PREDICT_ONLY** clause  
  
-   Relationship to an attribute column (mandatory only if it applies), indicated by the **RELATED TO** clause  
  
 Use the following syntax for the column definition list, to define a single column:  
  
```  
<column name>    <data type>    [<Distribution>]    [<Modeling Flags>]    <Content Type>    [<prediction>]    [<column relationship>]   
```  
  
 Use the following syntax for the column definition list, to define a nested table column:  
  
```  
<column name>    TABLE    [<prediction>] ( <non-table column definition list> )  
```  
  
 Except for modeling flags, you can use no more than one clause from a particular group to define a column. You can define multiple modeling flags for a column.  
  
 For a list of the data types, content types, column distributions, and modeling flags that you can use to define a column, see the following topics:  
  
-   [Data Types &#40;Data Mining&#41;](../analysis-services/data-mining/data-types-data-mining.md)  
  
-   [Content Types &#40;Data Mining&#41;](../analysis-services/data-mining/content-types-data-mining.md)  
  
-   [Column Distributions &#40;Data Mining&#41;](../analysis-services/data-mining/column-distributions-data-mining.md)  
  
-   [Modeling Flags &#40;Data Mining&#41;](../analysis-services/data-mining/modeling-flags-data-mining.md)  
  
 You can add a clause to the statement to describe the relationship between two columns. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports the use of the following \<Column relationship> clause.  
  
 **RELATED TO**  
 This form indicates a value hierarchy. The target of a RELATED TO column can be a key column in a nested table, a discretely-valued column in the case row, or another column with a RELATED TO clause, which indicates a deeper hierarchy.  
  
 Use a prediction clause to describe how the prediction column is used. The following table describes the two possible clauses.  
  
|\<prediction> clause|Description|  
|---------------------------|-----------------|  
|**PREDICT**|This column can be predicted by the model, and it can be supplied in input cases to predict the value of other predictable columns.|  
|**PREDICT_ONLY**|This column can be predicted by the model, but its values cannot be used in input cases to predict the value of other predictable columns.|  
  
### Parameter Definition List  
 You can use the parameter list to adjust the performance and functionality of a mining model. The syntax of the parameter list is as follows:  
  
```  
[<parameter> = <value>, <parameter> = <value>,...]  
```  
  
 For a list of the parameters that are associated with each algorithm, see [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md).  
  
## Remarks  
 If you want to create a model that has a built-in testing data set, you should use the statement CREATE MINING STRUCTURE followed by ALTER MINING STRUCTURE. However, not all model types support a holdout data set. For more information, see [CREATE MINING STRUCTURE &#40;DMX&#41;](../dmx/create-mining-structure-dmx.md).  
  
 For a walkthrough of how to create a mining model by using the CREATEMODEL statement, see [Time Series Prediction DMX Tutorial](https://msdn.microsoft.com/library/38ea7c03-4754-4e71-896a-f68cc2c98ce2).  
  
## Naive Bayes Example  
 The following example uses the [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes algorithm to create a new mining model. The Bike Buyer column is defined as the predictable attribute.  
  
```  
CREATE MINING MODEL [NBSample]  
(  
    CustomerKey LONG KEY,   
    Gender TEXT DISCRETE,  
    [Number Cars Owned] LONG DISCRETE,  
    [Bike Buyer] LONG DISCRETE PREDICT  
)  
USING Microsoft_Naive_Bayes  
```  
  
## Association Model Example  
 The following example uses the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm to create a new mining model. The statement takes advantage of the ability to nest a table inside the model definition by using a table column. The model is modified by using the *MINIMUM_PROBABILITY* and *MINIMUM_SUPPORT* parameters.  
  
```  
CREATE MINING MODEL MyAssociationModel (  
    OrderNumber TEXT KEY,  
    [Products] TABLE PREDICT (  
        [Model] TEXT KEY  
    )  
)  
USING Microsoft_Association_Rules (Minimum_Probability = 0.1, MINIMUM_SUPPORT = 0.01)  
```  
  
## Sequence Clustering Example  
 The following example uses the [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Clustering algorithm to create a new mining model. Two keys are used to define the model. The OrderNumber column is used as the case key, and specifies individual orders. The LineNumber column is used as the nested table key, and specifies the sequence in which items were added to an order.  
  
```  
CREATE MINING MODEL BuyingSequence (  
    [Order Number] TEXT KEY,  
    [Products] TABLE   
     (  
        [Line Number] LONG KEY SEQUENCE,  
        [Model] TEXT DISCRETE PREDICT  
    )  
)  
USING Microsoft_Sequence_Clustering  
```  
  
## Time Series Example  
 The following example uses the [!INCLUDE[msCoName](../includes/msconame-md.md)] Times Series algorithm to create a new mining model by using the ARTxp algorithm. ReportingDate is the key column for the time series and ModelRegion is the key column for the data series. In this example, it is assumed that the periodicity of the data is every 12 months. Therefore, the *PERIODICITY_HINT* parameter is set to 12.  
  
> [!NOTE]  
>  You must specify the *PERIODICITY_HINT* parameter by using brace characters. Moreover, because the value is a string, it must be enclosed in single quotation marks:  "{\<numeric value>}".  
  
```  
CREATE MINING MODEL SalesForecast (  
        ReportingDate DATE KEY TIME,  
        ModelRegion TEXT KEY,  
        Amount LONG CONTINUOUS PREDICT,  
        Quantity LONG CONTINUOUS PREDICT  
)  
USING Microsoft_Time_Series (PERIODICITY_HINT = '{12}', FORECAST_METHOD = 'ARTXP')  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
