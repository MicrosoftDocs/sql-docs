---
title: "Connect Components in a Data Flow | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "components [Integration Services], connections"
  - "connections [Integration Services], data flow components"
ms.assetid: 70616a58-8921-4218-85bf-f3e90c5a9dbf
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Connect Components in a Data Flow
  This procedure describes how to connect the output of components in a data flow to other components within the same data flow.  
  
### To connect components in a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the data flow in which you want to connect components.  
  
4.  On the design surface of the **Data Flow** tab, select the transformation or source that you want to connect.  
  
5.  Drag the green output arrow of a transformation or a source to a transformation or to a destination. Some data flow components have error outputs that you can connect in the same way.  
  
    > [!NOTE]  
    >  Some data flow components can have multiple outputs and you can connect each output to a different transformation or destination.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Add or Delete a Component in a Data Flow](data-flow.md)   
 [Configure an Error Output in a Data Flow Component](../configure-an-error-output-in-a-data-flow-component.md)   
 [Data Flow](data-flow.md)  
  
  
