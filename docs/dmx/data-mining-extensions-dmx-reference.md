---
title: "Data Mining Extensions (DMX) Reference | Microsoft Docs"
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
# Data Mining Extensions (DMX) Reference
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Data Mining Extensions (DMX) is a language that you can use to create and work with data mining models in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. You can use DMX to create the structure of new data mining models, to train these models, and to browse, manage, and predict against them. DMX is composed of data definition language (DDL) statements, data manipulation language (DML) statements, and functions and operators.  
  
## Microsoft OLE DB for Data Mining Specification  
 The data mining features in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] are built to comply with the [!INCLUDE[msCoName](../includes/msconame-md.md)] OLE DB for Data Mining specification.  
  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] OLE DB for Data Mining specification defines the following:  
  
-   A structure to hold the information that defines a data mining model.  
  
-   A language for creating and working with data mining models.  
  
 The specification defines the basis of data mining as the data mining model virtual object. The data mining model object encapsulates all that is known about a particular mining model. The data mining model object is structured like an SQL table, with columns, data types, and meta information that describe the model. This structure lets you use the DMX language, which is an extension of SQL, to create and work with models.  
  
 **For More Information:** [Mining Structures &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)  
  
##  <a name="BKMK_DMXStatements"></a> DMX Statements  
 You can use DMX statements to create, process, delete, copy, browse, and predict against data mining models. There are two types of statements in DMX: data definition statements and data manipulation statements. You can use each type of statement to perform different kinds of tasks.  
  
 The following sections provide more information about working with DMX statements:  
  
-   [Data Definition Statements](#BKMK_DDL)  
  
-   [Data Manipulation Statements](#BKMK_DML)  
  
-   [Query Fundamentals](#BKMK_Queries)  
  
###  <a name="BKMK_DDL"></a> Data Definition Statements  
 Use data definition statements in DMX to create and define new mining structures and models, to import and export mining models and mining structures, and to drop existing models from a database. Data definition statements in DMX are part of the data definition language (DDL).  
  
 You can perform the following tasks with the data definition statements in DMX:  
  
-   Create a mining structure by using the [CREATE MINING STRUCTURE](../dmx/create-mining-structure-dmx.md) statement, and add a mining model to the mining structure by using the [ALTER MINING STRUCTURE](../dmx/alter-mining-structure-dmx.md) statement.  
  
-   Create a mining model and associated mining structure simultaneously by using the [CREATE MINING MODEL](../dmx/create-mining-model-dmx.md) statement to build an empty data mining model object.  
  
-   Export a mining model and associated mining structure to a file by using the [EXPORT](../dmx/export-dmx.md) statement. Import a mining model and associated mining structure from a file that is created by the EXPORT statement by using the [IMPORT](../dmx/import-dmx.md) statement.  
  
-   Copy the structure of an existing mining model into a new model, and train it with the same data, by using the [SELECT INTO](../dmx/select-into-dmx.md) statement.  
  
-   Completely remove a mining model from a database by using the [DROP MINING MODEL](../dmx/drop-mining-model-dmx.md) statement. Completely remove a mining structure and all its associated mining models from the database by using the [DROP MINING STRUCTURE](../dmx/drop-mining-structure-dmx.md) statement.  
  
 To learn more about the data mining tasks that you can perform by using DMX statements, see [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md).  
  
 [Back to DMX Statements](#BKMK_DMXStatements)  
  
###  <a name="BKMK_DML"></a> Data Manipulation Statements  
 Use data manipulation statements in DMX to work with existing mining models, to browse the models and to create predictions against them. Data manipulation statements in DMX are part of the data manipulation language (DML).  
  
 You can perform the following tasks with the data manipulation statements in DMX:  
  
-   Train a mining model by using the [INSERT INTO](../dmx/insert-into-dmx.md) statement. This does not insert the actual source data into a data mining model object, but instead creates an abstraction that describes the mining model that the algorithm creates. The source query for an INSERT INTO statement is described in [\<source data query>](../dmx/source-data-query.md).  
  
-   Extend the SELECT statement to browse the information that is calculated during model training and stored in the data mining model, such as statistics of the source data. Following are the clauses that you can include to extend the power of the SELECT statement:  
  
    -   [SELECT DISTINCT FROM &#60;model &#62; &#40;DMX&#41;](../dmx/select-distinct-from-model-dmx.md)  
  
    -   [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](../dmx/select-from-model-content-dmx.md)  
  
    -   [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md)  
  
    -   [SELECT FROM &#60;model&#62;.SAMPLE_CASES &#40;DMX&#41;](../dmx/select-from-model-sample-cases-dmx.md)  
  
    -   [SELECT FROM &#60;model&#62;.DIMENSION_CONTENT &#40;DMX&#41;](../dmx/select-from-model-dimension-content-dmx.md)  
  
-   Create predictions that are based on an existing mining model by using the [PREDICTION JOIN](../dmx/select-from-model-prediction-join-dmx.md) clause of the SELECT statement. The source query for a PREDICTION JOIN statement is described in [\<source data query>](../dmx/source-data-query.md).  
  
-   Remove all the trained data from a model or a structure by using the [DELETE &#40;DMX&#41;](../dmx/delete-dmx.md) statement.  
  
 To learn more about the data mining tasks that you can perform by using DMX statements, see [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md).  
  
 [Back to DMX Statements](#BKMK_DMXStatements)  
  
###  <a name="BKMK_Queries"></a> DMX Query Fundamentals  
 The SELECT statement is the basis for most DMX queries. Depending on the clauses that you use with such statements, you can browse, copy, or predict against mining models. The prediction query uses a form of SELECT to create predictions based on existing mining models. Functions extend your ability to browse and query the mining models beyond the intrinsic capabilities of the data mining model.  
  
 You can use DMX functions to obtain information that is discovered during the training of your models, and to calculate new information. You can use these functions for many purposes, including to return statistics that describe the underlying data or the accuracy of a prediction, or to return an expanded explanation of a prediction.  
  
 **For More**  **Information:** [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md), [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md), [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md), [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)  
  
 [Back to DMX Statements](#BKMK_DMXStatements)  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
