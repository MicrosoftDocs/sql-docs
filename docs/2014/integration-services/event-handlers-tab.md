---
title: "Event Handlers Tab | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.eventhandlerwindow.f1"
ms.assetid: 94fc8916-8032-490c-b9d5-ded8b6217e49
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Event Handlers Tab
  Use the **Event Handlers** tab of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to build a control flow in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package. An event handler runs in response to an event raised by the package or by a task or container in the package.  
  
## Options  
 **Executable**  
 Select the executable for which you want to build an event handler. The executable can be the package, or a task or containers in the package.  
  
 **Event handler**  
 Select a type of event handler. Create the event handler by dragging items from the **Toolbox**.  
  
 **Delete**  
 Select an event handler, and remove it from the package by clicking **Delete**.  
  
 **Click here to create an \<event handler name> for the executable \<executable name>**  
 Click to create the event handler.  
  
 Create the control flow by dragging graphical objects that represent [!INCLUDE[ssIS](../includes/ssis-md.md)] tasks and containers from the **Toolbox** to the design surface of the **Event Handlers** tab, and then connecting the objects by using precedence constraints to define the sequence in which they run.  
  
 Additionally, to add annotations, right-click the design surface, and then on the menu, click **Add Annotation**.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Event Handlers](integration-services-ssis-event-handlers.md)   
 [Control Flow](control-flow/control-flow.md)   
 [SSIS Designer](ssis-designer.md)   
 [Integration Services &#40;SSIS&#41; Event Handlers](integration-services-ssis-event-handlers.md)  
  
  
