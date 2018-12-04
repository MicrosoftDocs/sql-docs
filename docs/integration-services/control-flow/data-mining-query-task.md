---
title: "Data Mining Query Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataminingquerytask.f1"
  - "sql13.dts.designer.dmquerytask.miningmodel.f1"
  - "sql13.dts.designer.dmquerytask.query.f1"
  - "sql13.dts.designer.dmquerytask.output.f1"
helpviewer_keywords: 
  - "prediction queries [Integration Services]"
  - "Data Mining Query task [Integration Services]"
ms.assetid: f489348c-2008-4f66-8c2c-c07c3029439a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Data Mining Query Task
  The Data Mining Query task runs prediction queries based on data mining models built in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The prediction query creates a prediction for new data by using mining models. For example, a prediction query can predict how many sailboats are likely to sell during the summer months or generate a list of prospective customers who are likely to buy a sailboat.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides tasks that perform other business intelligence operations, such as running Data Definition Language (DDL) statements and processing analytic objects.  
  
 For more information about other business intelligence tasks, click one of the following topics:  
  
-   [Analysis Services Execute DDL Task](../../integration-services/control-flow/analysis-services-execute-ddl-task.md)  
  
-   [Analysis Services Processing Task](../../integration-services/control-flow/analysis-services-processing-task.md)  
  
## Prediction Queries  
 The query is a Data Mining Extensions (DMX) statement. The DMX language is an extension of the SQL language that provides support for working with mining models. For more information about how to use the DMX language, see [Data Mining Extensions &#40;DMX&#41; Reference](../../dmx/data-mining-extensions-dmx-reference.md).  
  
 The task can query multiple mining models that are built on the same mining structure. A mining model is built using one of the data mining algorithms that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides. The mining structure that the Data Mining Query task references can include multiple mining models, built using different algorithms. For more information, see [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md) and [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md).  
  
 The prediction query that the Data Mining Query task runs returns a result that is a single row or a data set. A query that returns a single row is called a singleton query: for example, the query that predicts how many sailboats will be sold during the summer months returns a number. For more information about prediction queries that return a single row, see [Data Mining Query Tools](../../analysis-services/data-mining/data-mining-query-tools.md).  
  
 The query results are saved to tables. If a table with the name that the Data Mining Query task specifies already exists, the task can create a new table, using the same name with a number appended, or it can overwrite the table content.  
  
 If the results include nesting, the result is flattened before it is saved. Flattening a result changes a nested result set to a table. For example, flattening a nested result with a **Customer** column and a nested **Product** column adds rows to the **Customer** column to make a table that includes product data for each customer. For example, a customer with three different products becomes a table with three rows, repeating the customer in each row and including a different product in each row. If the FLATTENED keyword is omitted, the table contains only the **Customer** column and only one row per customer. For more information, see [SELECT &#40;DMX&#41;](../../dmx/select-dmx.md).  
  
## Configuration of the Data Mining Query Task  
 The Data Mining Query task requires two connections. The first connection is an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager that connects either to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project that contains the mining structure and the mining model. The second connection is an OLE DB connection manager that connects to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the table to which the task writes. For more information, see [Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md) and [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
> [!NOTE]  
>  The Data Mining Query Editor has no Expressions page. Instead, use the **Properties** window to access the tools for creating and managing property expressions for properties of the Data Mining Query task.  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Programmatic Configuration of Data Mining Query Task  
 For more information about programmatically setting these properties, click one of the following topics:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.DMQueryTask.DMQueryTask>  
  
## Data Mining Query Task Editor (Mining Model Tab)
  Use the **Mining Model** tab of the **Data Mining Query Task** dialog box to specify the mining structure and mining model to use.  
  
 To learn about implementing data mining in packages, see [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md) and [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md).  
  
### General Options  
 **Name**  
 Provide a unique name for the Data Mining Query task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Data Mining Query task.  
  
### Mining Model Tab Options  
 **Connection**  
 Select an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager in the list or click **New** to create a new connection manager.  
  
 **Related Topics:**  [Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md)  
  
 **New**  
 Create a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager.  
  
 **Related Topics:** [Add Analysis Services Connection Manager Dialog Box UI Reference](../../integration-services/connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)  
  
 **Mining structure**  
 Select a mining structure in the list.  
  
 **Mining models**  
 Select a mining model built on the selected mining structure.  

## Data Mining Query Task Editor (Query Tab)
  Use the **Query** tab of the **Data Mining Query Task** dialog box to create prediction queries based on a mining model. In this dialog box you can also bind parameters and result sets to variables.  
  
 To learn about implementing data mining in packages, see [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md) and [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md).  
  
### General Options  
 **Name**  
 Provide a unique name for the Data Mining Query task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Data Mining Query task.  
  
### Build Query Tab Options  
 **Data mining query**  
 Type a data mining query.  
  
 **Related Topics:**  [Data Mining Extensions &#40;DMX&#41; Reference](../../dmx/data-mining-extensions-dmx-reference.md)  
  
 **Build New Query**  
 Create the data mining query using a graphical tool.  
  
 **Related Topics:** [Data Mining Query](../../integration-services/control-flow/data-mining-query.md)  
  
### Parameter Mapping Tab Options  
 **Parameter Name**  
 Optionally, update the parameter name. Map the parameter to a variable by selecting a variable in the **Variable Name** list.  
  
 **Variable Name**  
 Select a variable in the list to map it to the parameter.  
  
 **Add**  
 Add to a parameter to the list.  
  
 **Remove**  
 Select a parameter, and then click **Remove**.  
  
### Result Set Tab Options  
 **Result Name**  
 Optionally, update the result set name. Map the result to a variable by selecting a variable in the **Variable Name** list.  
  
 After you have added a result by clicking **Add**, provide a unique name for the result.  
  
 **Variable Name**  
 Select a variable in the list to map it to the result set.  
  
 **Result Type**  
 Indicate whether to return a single row or a full result set.  
  
 **Add**  
 Add a result set to the list.  
  
 **Remove**  
 Select a result, and then click **Remove**.  
## Data Mining Query Task Editor (Output Tab)
  Use the **Output** tab of the **Data Mining Query Task Editor** dialog box to specify the destination of the prediction query.  
  
 To learn about implementing data mining in packages, see [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md) and [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md).  
  
### General Options  
 **Name**  
 Provide a unique name for the Data Mining Query task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Data Mining Query task.  
  
### Output Tab Options  
 **Connection**  
 Select a connection manager in the list or click **New** to create a new connection manager.  
  
 **New**  
 Create a new connection manager. Only the ADO.NET and OLE DB connection manager types can be used.  
  
 **Output table**  
 Specify the table to which the prediction query writes its results.  
  
 **Drop and re-create the output table**  
 Indicate whether the prediction query should overwrite content in the destination table by dropping and then re-creating the table.  
  
