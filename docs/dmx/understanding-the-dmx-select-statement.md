---
title: "Understanding the DMX Select Statement"
description: "Understanding the DMX Select Statement"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Understanding the DMX Select Statement
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  The [SELECT](../dmx/select-dmx.md) statement is the basis for most queries that you create with Data Mining Extensions (DMX) in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. It can perform many different kinds of tasks, such as browsing and predicting against data mining models.  
  
 Following are the tasks that you can complete by using the **SELECT** statement:  
  
-   Browse a data mining model. The schema rowset defines the structure of a model.  
  
-   Discover the possible values of a mining model column.  
  
-   Browse the cases that are assigned to nodes in a mining model, or get a representative case.  
  
-   Create predictions using a variety of inputs.  
  
-   Copy mining models.  
  
 Each of these tasks uses a different set of data, which we'll call a *data domain*. You define the data domain in the **FROM** clause of the statement.  
  
-   You want to find objects in the data mining model itself, such as the rule that defines a set of data, or a formula used to make predictions.  
  
     In that case, you need to look at the metadata that is stored in the model itself. Therefore, your data domain is the columns in the data mining schema rowset.  
  
-   You want to get detailed information from the cases used to build the model.  
  
     In that case, you need to drill through to the mining structure, which is your data domain, and look at individual rows in columns such as Gender, Bike Buyer, and so on.  
  
> [!IMPORTANT]  
> Anything that is included in the expression list or in the **WHERE** clause must come from the data domain that is defined by the **FROM** clause. You cannot mix data domains.  
  
##  <a name="Select_Types"></a> SELECT Types  
 The syntax of **SELECT** statement supports many different tasks. Use the following patterns to perform these tasks:  
  
-   [Predicting](#Predicting)  
  
-   [Browsing](#Browsing)  
  
-   [Copying](#Copying)  
  
-   [Drillthrough](#Drillthrough)  
  
###  <a name="Predicting"></a> Predicting  
 You can perform predictions based on a mining model by using the following query types.  
  
 You can include any one of the browsing or predicting **SELECT** statements within the **FROM** and **WHERE** clauses of a prediction join **SELECT** statement.  
  
|Query Type|Description|  
|----------------|-----------------|  
|SELECT FROM [NATURAL] PREDICTION JOIN|Returns a prediction that is created by joining the columns in the mining model to the columns of an internal data source.<br /><br /> The domain for this query type is the predictable columns from the model and the columns from the input data source.<br /><br /> [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md)<br /><br /> [Prediction Queries &#40;Data Mining&#41;](/analysis-services/data-mining/prediction-queries-data-mining)|  
|SELECT FROM *\<model>*|Returns the most likely state of the predictable column, based only on the mining model. This query type is a shortcut for creating a prediction with an empty prediction join.<br /><br /> The domain for this query type is the predictable columns from the model.<br /><br /> [SELECT FROM &#60;model&#62; &#40;DMX&#41;](../dmx/select-from-model-dmx.md)<br /><br /> [Prediction Queries &#40;Data Mining&#41;](/analysis-services/data-mining/prediction-queries-data-mining)|  
  
 [Back to Select Types](#Select_Types)  
  
###  <a name="Browsing"></a> Browsing  
 You can browse the contents of a mining model by using the following query types.  
  
|Query Type|Description|  
|----------------|-----------------|  
|SELECT DISTINCT FROM *\<model>*|Returns all the state values from the mining model for the specified column.<br /><br /> The data domain for this query type is the data mining model.<br /><br /> [SELECT DISTINCT FROM &#60;model &#62; &#40;DMX&#41;](../dmx/select-distinct-from-model-dmx.md)<br /><br /> [Content Queries &#40;Data Mining&#41;](/analysis-services/data-mining/content-queries-data-mining)|  
|SELECT FROM *\<model>*.CONTENT|Returns content that describes the mining model.<br /><br /> The data domain for this query type is the content schema rowset.<br /><br /> [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](../dmx/select-from-model-content-dmx.md)<br /><br /> [Content Queries &#40;Data Mining&#41;](/analysis-services/data-mining/content-queries-data-mining)|  
|SELECT FROM *\<model>*.DIMENSION_CONTENT|Returns content that describes the mining model.<br /><br /> The data domain for this query type is the content schema rowset.<br /><br /> [SELECT FROM &#60;model&#62;.DIMENSION_CONTENT &#40;DMX&#41;](../dmx/select-from-model-dimension-content-dmx.md)|  
|SELECT FROM *\<model>*.PMML|Returns the Predictive Model Markup Language (PMML) representation of the mining model, for algorithms that support this functionality.<br /><br /> The domain for this query type is the PMML schema rowset.<br /><br /> [DMSCHEMA_MINING_MODEL_CONTENT_PMML Rowset](/previous-versions/sql/sql-server-2012/ms126283(v=sql.110))|  
  
 [Back to Select Types](#Select_Types)  
  
###  <a name="Copying"></a> Copying  
 You can copy a mining model and its associated mining structure into a new model, and then rename the model within the statement.  
  
|Query Type|Description|  
|----------------|-----------------|  
|SELECT INTO *\<new model>*|Creates a copy of the mining model.<br /><br /> The domain for this query type is the data mining model.<br /><br /> [SELECT INTO &#40;DMX&#41;](../dmx/select-into-dmx.md)|  
  
 [Back to Select Types](#Select_Types)  
  
###  <a name="Drillthrough"></a> Drillthrough  
 You can browse the cases, or a representation of the cases, that were used to train the model, by using the following query types.  
  
|Query Type|Description|  
|----------------|-----------------|  
|SELECT FROM *\<model>*.CASES|Returns the cases used to train the mining model.<br /><br /> The domain for this query type is the data mining model.<br /><br /> [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md)<br /><br /> [Create Drillthrough Queries using DMX](/analysis-services/data-mining/create-drillthrough-queries-using-dmx)|  
|SELECT FROM *\<model>*.SAMPLE_CASES|Returns a sample case, representative of the cases used to train the mining model.<br /><br /> The domain for this query type is the data mining model.<br /><br /> [SELECT FROM &#60;model&#62;.SAMPLE_CASES &#40;DMX&#41;](../dmx/select-from-model-sample-cases-dmx.md)|  
|SELECT FROM *\<structure>*. CASES|Returns the detailed data rows from the underlying mining structure, even if some details were not used in training the mining model.<br /><br /> [SELECT FROM &#60;structure&#62;.CASES](../dmx/select-from-structure-cases.md)<br /><br /> [Drillthrough Queries &#40;Data Mining&#41;](/analysis-services/data-mining/drillthrough-queries-data-mining)|  
  
 [Back to Select Types](#Select_Types)  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)  
  
