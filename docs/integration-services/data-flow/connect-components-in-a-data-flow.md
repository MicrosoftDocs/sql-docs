---
title: "Connect Components in a Data Flow | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "components [Integration Services], connections"
  - "connections [Integration Services], data flow components"
ms.assetid: 70616a58-8921-4218-85bf-f3e90c5a9dbf
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Connect Components in a Data Flow
  This procedure describes how to connect the output of components in a data flow to other components within the same data flow.  
You construct the data flow in a package on the design surface of the **Data Flow** tab in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. If a data flow contains two data flow components, you can connect them by connecting the output of a source or transformation to the input of a transformation or destination. The connector between two data flow components is called a path.  
  
 The following diagram shows a simple data flow with a source component, two transformations, a destination component, and the paths that connect them.  
  
 ![Data flow](../../integration-services/data-flow/media/mw-dts-08.gif "Data flow")  
  
 After two components are connected, you can view the metadata of the data that moves through the path and the properties of the path in **Data Flow Path Editor**. For more information, see [Integration Services Paths](../../integration-services/data-flow/integration-services-paths.md).  
  
 You can also add data viewers to paths. A data viewer makes it possible to view data moving between data flow components when the package is run.  
  
### Connect components in a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the data flow in which you want to connect components.  
  
4.  On the design surface of the **Data Flow** tab, select the transformation or source that you want to connect.  
  
5.  Drag the green output arrow of a transformation or a source to a transformation or to a destination. Some data flow components have error outputs that you can connect in the same way.  
  
    > [!NOTE]  
    >  Some data flow components can have multiple outputs and you can connect each output to a different transformation or destination.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Add or Delete a Component in a Data Flow](../../integration-services/data-flow/add-or-delete-a-component-in-a-data-flow.md)   
 [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md)
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
