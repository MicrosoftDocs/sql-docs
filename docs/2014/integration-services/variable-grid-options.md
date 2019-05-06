---
title: "Variable Grid Options | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.variableoptionswindow.f1"
helpviewer_keywords: 
  - "Choose Variable Columns dialog box"
ms.assetid: 7cccc230-3b20-4074-804f-3448d9616a83
author: janinezhang
ms.author: janinez
manager: craigg
---
# Variable Grid Options
  Use the **Variable Grid Options** dialog box to select the columns that will display in the **Variables** window and to select the filters to apply to the list of variables. For more information about the corresponding variable properties, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md).  
  
## Options for Filter  
 **Show system variables**  
 Select to list system variables in the **Variables** window. System variables are predefined. You cannot add or delete system variables. You can modify the **RaiseChangedEvent** property setting.  
  
 This list is color coded. System variables are gray, and user-defined variables are black.  
  
 **Show variables of all scopes**  
 Select to show variables within the scope the package, and within the scope of containers, tasks, and event handlers in the package. Clear this option to show only variables within the scope of the package and within the scope of a selected container, task, or event handler.  
  
 For more information about variable scope, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md).  
  
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
 [Variables Window](../../2014/integration-services/variables-window.md)   
 [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)   
 [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)   
 [Integration Services &#40;SSIS&#41; Event Handlers](integration-services-ssis-event-handlers.md)  
  
  
