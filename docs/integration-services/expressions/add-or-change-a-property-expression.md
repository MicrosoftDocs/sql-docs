---
title: "Add or Change a Property Expression | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "expressions [Integration Services], creating"
  - "expressions [Integration Services], property expressions"
ms.assetid: cb5da499-065f-4fa6-9f6d-5bc5f385241e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Add or Change a Property Expression
  You can create property expressions for packages, tasks, Foreach Loop containers, For Loop containers, Sequence containers, event handlers, package and project level connection managers, and log providers.  
  
 To create or change property expressions, you can use either the **Property Expressions Editor** or **Expression Builder**. The **Property Expressions Editor** can be accessed from either the custom editors that are available for tasks and containers, or from the **Properties** window. **Expression Builder** can be accessed from inside the **Property Expressions Editor**. While you can write expressions in either the **Property Expressions Editor** or **Expression Builder**, **Expression Builder** provides a graphical set of tools that makes it simple to build complex expressions.  
  
 To learn more about the syntax, operators, and functions that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides, see [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md) and [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md). The topic for each operator or function includes examples of using that operator or function in an expression. For examples of more complex expressions, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).  
  
### To create or change a property expression  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project that contains the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package you want.  
  
2.  In Solution Explorer, double-click the package to open it, and then do one of the following:  
  
    -   If the item is a task or a container, double-click the item, and then click **Expressions** in the editor.  
  
    -   Right-click the item and then click **Properties**.  
  
3.  Click in the **Expressions** box and then click the ellipsis (...).  
  
4.  In the **Property Expressions Editor**, select a property in the **Property** list, and then do one of the following:  
  
    -   Type or change the property expression directly in the **Expression** column, and then click **OK**.  
  
         -or-  
  
    -   Click the ellipsis (...) in the expression row of the property to open the **Expression Builder**.  
  
5.  (Optional) In the **Expression Builder**, do any of the following tasks:  
  
    -   To access system and user-defined variables, expand **Variables**.  
  
    -   To access the functions, the casts, and the operators that the [!INCLUDE[ssIS](../../includes/ssis-md.md)] expression language provides, expand **Mathematical Functions**, **String Functions**, **Date/Time Functions**, **NULL Functions**, **Type Casts**, and **Operators**.  
  
    -   To build or change an expression in the **Expression Builder**, drag variables, columns, functions, operators, and casts to the **Expression** box, or type the expression in the box.  
  
    -   To view the evaluation result of the expression, click **Evaluate Expression** in the **Expression Builder**.  
  
         If the expression is not valid, an alert appears that describes the syntax errors in the expression.  
  
        > [!NOTE]  
        >  Evaluating an expression does not assign the evaluation result to the property.  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md)   
 [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md)   
 [Integration Services &#40;SSIS&#41; Packages](../../integration-services/integration-services-ssis-packages.md)   
 [Integration Services Containers](../../integration-services/control-flow/integration-services-containers.md)   
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Integration Services &#40;SSIS&#41; Event Handlers](../../integration-services/integration-services-ssis-event-handlers.md)   
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)   
 [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md)  
  
  
