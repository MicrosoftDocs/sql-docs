---
title: "Configure a For Loop Container | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "containers [Integration Services], For Loop"
  - "For Loop containers"
ms.assetid: b9cd7ea7-b198-4a35-8b16-6acf09611ca5
author: janinezhang
ms.author: janinez
manager: craigg
---
# Configure a For Loop Container
  This procedure describes how to configure a For Loop container by using the **For Loop Editor** dialog box.  
  
 For an example of the For Loop container, see [SSIS Loops that do not fail](https://go.microsoft.com/fwlink/?LinkId=240295) on bimonkey.com.  
  
### To configure the For Loop container  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], double-click the For Loop container to open the **For Loop Editor**.  
  
2.  Optionally, modify the name and description of the For Loop container.  
  
3.  Optionally, type an initialization expression in the **InitExpression** text box.  
  
4.  Type an evaluation expression in the **EvalExpression** text box.  
  
    > [!NOTE]  
    >  The expression must evaluate to a Boolean. When the expression evaluates to `false`, the loop stops running.  
  
5.  Optionally, type an assignment expression in the **AssignExpression** text box.  
  
6.  Optionally, click **Expressions** and, on the **Expressions** page, create property expressions for the properties of the For Loop container. For more information, see [Add or Change a Property Expression](expressions/add-or-change-a-property-expression.md).  
  
7.  Click **OK** to close the **For Loop Editor**.  
  
## See Also  
 [For Loop Container](control-flow/for-loop-container.md)   
 [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md)   
 [Use Property Expressions in Packages](expressions/use-property-expressions-in-packages.md)  
  
  
