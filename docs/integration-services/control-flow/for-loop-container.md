---
title: "For Loop Container | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.forloopcontainerdetails.f1"
  - "sql13.dts.designer.forloopcontainer.f1"
helpviewer_keywords: 
  - "repeating control flow"
  - "containers [Integration Services], For Loop"
  - "For Loop containers"
ms.assetid: 44cf7355-992b-4bbf-a28c-bfb012de06f6
author: janinezhang
ms.author: janinez
manager: craigg
---
# For Loop Container
  The For Loop container defines a repeating control flow in a package. The loop implementation is similar to the **For** looping structure in programming languages. In each repeat of the loop, the For Loop container evaluates an expression and repeats its workflow until the expression evaluates to **False**.  
  
 The For Loop container usesthe following elements to define the loop:  
  
-   An optional initialization expression that assigns values to the loop counters.  
  
-   An evaluation expression that contains the expression used to test whether the loop should stop or continue.  
  
-   An optional iteration expression that increments or decrements the loop counter.  
  
 The following diagram shows a For Loop container with a Send Mail task. If the initialization expression is `@Counter = 0`, the evaluation expression is `@Counter < 4`, and the iteration expression is `@Counter = @Counter + 1`, the loop repeats four times and sends four e-mail messages.  
  
 ![A For Loop container repeats a task four times](../../integration-services/control-flow/media/ssis-forloop.gif "A For Loop container repeats a task four times")  
  
 The expressions must be valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expressions.  
  
 To create the initialization and assignment expressions, you can use the assignment operator (=). This operator is not otherwise supported by the Integration Services expression grammar and can only be used by the initialization and assignment expression types in the For Loop container. Any expression that uses the assignment operator must have the syntax `@Var = <expression>`, where **Var** is a run-time variable and \<expression> is an expression that follows the rules of the [!INCLUDE[ssIS](../../includes/ssis-md.md)] expression syntax. The expression can include the variables, literals, and any operators and functions that the SSIS expression grammar supports. The expression must evaluate to a data type that can be cast to the data type of the variable.  
  
 A For Loop container can have only one evaluation expression. This means that the For Loop container runs all its control flow elements the same number of times. Because the For Loop container can include other For Loop containers, you can build nested loops and implement complex looping in packages.  
  
 You can set a transaction property on the For Loop container to define a transaction for a subset of the package control flow. In this way, you can manage transactions at a more granular level. For example, if a For Loop container repeats a control flow that updates data in a table multiple times, you can configure the For Loop and its control flow to use a transaction to ensure that if not all data is updated successfully, no data is updated. For more information, see [Integration Services Transactions](../../integration-services/integration-services-transactions.md).  
  
## Add iteration to a control flow with the For Loop container
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the For Loop container, a control flow element that makes it simple to include looping that conditionally repeats a control flow in a package. For more information, see [For Loop Container](../../integration-services/control-flow/for-loop-container.md).  
  
 The For Loop container evaluates a condition on each iteration of the loop, and stops when the condition evaluates to false. The For Loop container includes expressions for initializing the loop, specifying the evaluation condition that stops execution of the repeating control flow, and assigning a value to an expression that updates the value against which the evaluation condition is compared. You must provide an evaluation condition, but initialization and assignment expressions are optional.  
  
 The For Loop container provides no functionality; it provides only the structure in which you build the repeatable control flow. To provide container functionality, you must include at least one task in the For Loop container. For more information, see [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md).  
  
 The For Loop container can include a control flow with multiple tasks, and can include other containers. Adding tasks and containers to a For Loop container is similar to adding them to a package, except you drag the tasks and containers to the For Loop container instead of to the package. If the For Loop container includes more than one task or container, you can connect them using precedence constraints just as you do in a package. For more information, see [Precedence Constraints](../../integration-services/control-flow/precedence-constraints.md).  
  
## Add a For Loop container in a control flow  
  
1.  Add the For Loop container to the package. For more information, see [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
2.  Add tasks and containers to the For Loop container. For more information, see [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
3.  Connect tasks and containers in the For Loop container using precedence constraints. For more information, see [Connect Tasks and Containers by Using a Default Precedence Constraint](https://msdn.microsoft.com/library/8f31f15f-98ff-4c35-b41f-8b8cfd148d75).  
  
4.  Configure the For Loop container. For more information, see [Configure a For Loop Container](https://msdn.microsoft.com/library/b9cd7ea7-b198-4a35-8b16-6acf09611ca5).  

##  Configure the For Loop container
This procedure describes how to configure a For Loop container by using the **For Loop Editor** dialog box.  
  
 For an example of the For Loop container, see [SSIS Loops that do not fail](https://go.microsoft.com/fwlink/?LinkId=240295) on bimonkey.com.  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], double-click the For Loop container to open the **For Loop Editor**.  
  
2.  Optionally, modify the name and description of the For Loop container.  
  
3.  Optionally, type an initialization expression in the **InitExpression** text box.  
  
4.  Type an evaluation expression in the **EvalExpression** text box.  
  
    > [!NOTE]  
    >  The expression must evaluate to a Boolean. When the expression evaluates to **false**, the loop stops running.  
  
5.  Optionally, type an assignment expression in the **AssignExpression** text box.  
  
6.  Optionally, click **Expressions** and, on the **Expressions** page, create property expressions for the properties of the For Loop container. For more information, see [Add or Change a Property Expression](../../integration-services/expressions/add-or-change-a-property-expression.md).  
  
7.  Click **OK** to close the **For Loop Editor**.  

## For Loop Editor dialog box
Use the **For Loop** page of the **For Loop Editor** dialog box to configure a loop that repeats a workflow until a specified condition evaluates to false.  
  
 To learn about the For Loop container and how to use it in packages, see [For Loop Container](../../integration-services/control-flow/for-loop-container.md).  
  
### Options  
 **InitExpression**  
 Optionally, provide an expression that initializes values used by the loop.  
  
 **EvalExpression**  
 Provide an expression to evaluate whether the loop should stop or continue.  
  
 **AssignExpression**  
 Optionally, provide an expression that changes a condition each time that the loop repeats.  
  
 **Name**  
 Provide a unique name for the For Loop container. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Object names must be unique within a package.  
  
 **Description**  
 Provide a description of the For Loop container.  
 
## Use expressions with the For Loop container  
 When you configure the For Loop container by specifying an evaluation condition, initialization value, or assignment value, you can use either literals or expressions.  
  
 The expressions can include variables. The advantage of using variables is that they can be updated at run time, making the packages more flexible and easier to manage. The maximum length of an expression is 4000 characters.  
  
 When you specify a variable in an expression, you must preface the variable name with the at sign (@). For example, for a variable named **Counter**, enter @Counter in the expression that the For Loop container uses. If you include the namespace property on the variable, you must enclose the variable and namespace in brackets. For example, for a **Counter** variable in the **MyNamespace** namespace, type [@MyNamespace::Counter].  
  
 The variables that the For Loop container uses must be defined in the scope of the For Loop container or in the scope of any container that is higher in the package container hierarchy. For example, a For Loop container can use variables defined in its scope and also variables defined in package scope. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
 The [!INCLUDE[ssIS](../../includes/ssis-md.md)] expression grammar provides a complete set of operators and functions for implementing complex expressions used for evaluation, initialization, or assignment. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md).  
  
  
## See Also  
 [Control Flow](../../integration-services/control-flow/control-flow.md)   
 [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md)  
  
  
