---
title: "Variable Grid Options | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.variableoptionswindow.f1"
helpviewer_keywords: 
  - "Choose Variable Columns dialog box"
ms.assetid: 7cccc230-3b20-4074-804f-3448d9616a83
caps.latest.revision: 26
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Variable Grid Options
  Use the **Variable Grid Options** dialog box to select the columns that will display in the **Variables** window and to select the filters to apply to the list of variables. For more information about the corresponding variable properties, see [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md).  
  
## Options for Filter  
 **Show system variables**  
 Select to list system variables in the **Variables** window. System variables are predefined. You cannot add or delete system variables. You can modify the **RaiseChangedEvent** property setting.  
  
 This list is color coded. System variables are gray, and user-defined variables are black.  
  
 **Show variables of all scopes**  
 Select to show variables within the scope the package, and within the scope of containers, tasks, and event handlers in the package. Clear this option to show only variables within the scope of the package and within the scope of a selected container, task, or event handler.  
  
 For more information about variable scope, see [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md).  
  
## Options for Columns  
 Select the columns that you want to appear in the **Variables** window.  
  
-   **Scope**  
  
-   **Data type**  
  
-   **Value**  
  
-   **Namespace**  
  
-   **Raise event when variable value changes**  
  
-   **Description**  
  
-   **Expression**  
  
## See Also  
 [Variables Window](../integration-services/variables-window.md)   
 [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md)   
 [Use Variables in Packages](http://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)   
 [Integration Services &#40;SSIS&#41; Event Handlers](../integration-services/integration-services-ssis-event-handlers.md)  
  
  