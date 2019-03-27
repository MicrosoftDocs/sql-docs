---
title: "Add or Delete a Component in a Data Flow | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "adding components"
  - "components [Integration Services], data flow"
ms.assetid: d99124f9-0994-4f40-a48e-fdca6a4383e7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Add or Delete a Component in a Data Flow
  Data flow components are sources, destinations, and transformations in a data flow. Before you can add components to a data flow, the control flow in the package must include a Data Flow task.  
  
 The following procedures describe how to add or delete a component in the data flow of a package.  
  
### To add a component to a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the data flow to which you want to add a component.  
  
4.  In the Toolbox, expand **Data Flow Sources**, **Data Flow Transformations**, or **Data Flow Destinations**, and then drag a data flow item to the design surface of the **Data Flow** tab.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To delete a component from a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the data flow from which you want to delete a component.  
  
4.  Right-click the data flow component, and then click **Delete**.  
  
5.  Confirm the delete operation.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Connect Components in a Data Flow](../../integration-services/data-flow/connect-components-in-a-data-flow.md)   
 [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
