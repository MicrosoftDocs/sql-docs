---
title: "Connect Components with Paths | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data flow [Integration Services], connections"
  - "components [Integration Services], connections"
  - "connections [Integration Services], data flow components"
ms.assetid: 05633e4c-1370-4b05-802b-f36b07dd71c8
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Connect Components with Paths
  You construct the data flow in a package on the design surface of the **Data Flow** tab in the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. If a data flow contains two data flow components, you can connect them by connecting the output of a source or transformation to the input of a transformation or destination. The connector between two data flow components is called a path.  
  
 The following diagram shows a simple data flow with a source component, two transformations, a destination component, and the paths that connect them.  
  
 ![Data flow](media/mw-dts-08.gif "Data flow")  
  
 After two components are connected, you can view the metadata of the data that moves through the path and the properties of the path in **Data Flow Path Editor**. For more information, see [Integration Services Paths](data-flow/integration-services-paths.md).  
  
 You can also add data viewers to paths. A data viewer makes it possible to view data moving between data flow components when the package is run.  
  
### To connect components in a data flow  
  
-   [Connect Components in a Data Flow](data-flow/connect-components-in-a-data-flow.md)  
  
### To set path properties  
  
-   [Set the Properties of a Path by Using the Data Flow Path Editor](../../2014/integration-services/set-the-properties-of-a-path-by-using-the-data-flow-path-editor.md)  
  
### To view path metadata  
  
-   [View Path Metadata in the Data Flow Path Editor](../../2014/integration-services/view-path-metadata-in-the-data-flow-path-editor.md)  
  
### To view path metadata  
  
-   [Add a Data Viewer to a Data Flow](../../2014/integration-services/add-a-data-viewer-to-a-data-flow.md)  
  
## See Also  
 [Data Flow Task](control-flow/data-flow-task.md)   
 [Data Flow](data-flow/data-flow.md)   
 [Transform Data with Transformations](data-flow/transformations/transform-data-with-transformations.md)   
 [Error Handling in Data](data-flow/error-handling-in-data.md)  
  
  
