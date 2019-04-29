---
title: "Set the Properties of a Precedence Constraint | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Precedence Constraint Editor dialog box"
  - "precedence constraints [Integration Services], properties"
ms.assetid: d990f600-5c09-4cd5-8528-0a58d79dc9f2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Set the Properties of a Precedence Constraint
  To set properties on precedence constraints, you can use one of these tools:  
  
-   You can use the **Precedence Constraint Editor** dialog box.  
  
-   You can use the Properties window. The Properties window lists properties for configuring precedence constraints that are not available in the **Precedence Constraint Editor** dialog box. In the Properties window, you can provide a description and name of the precedence constraint, and configure the annotation to display for the precedence constraint on the design surface.  
  
 The following procedures describe how to set properties on precedence constraints by using each of these tools.  
  
### To set the properties of a precedence constraint by using the Precedence Constraint Editor  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  Double-click the precedence constraint.  
  
     The **Precedence Constraint Editor** opens.  
  
5.  In the **Evaluation operation** drop-down list, select an evaluation operation.  
  
6.  In the `Value` drop-down list, select the execution result of the precedence executable.  
  
7.  If the evaluation operation uses an expression, in the `Expression` box, type an expression, and click **Test** to evaluate the expression.  
  
    > [!NOTE]  
    >  Variable names are case-sensitive.  
  
8.  If multiple tasks or containers are connected to the constrained executable, select **Logical AND** to specify that the execution results of all preceding executables must evaluate to `true`. Select **Logical OR** to specify that only one execution result must evaluate to `true`.  
  
9. Click **OK** to close the **Precedence Constraint Editor**.  
  
10. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To set the properties of a precedence constraint by using the Properties window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want to modify.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab. On the design surface of the **Control Flow** tab, right-click the precedence constraint, and then click **Properties**. In the Properties window, modify the property values.  
  
4.  In the **Properties** window, set the following read/write properties of precedence constraints:  
  
    |Read/write property|Configuration action|  
    |--------------------------|--------------------------|  
    |Description|Provide a description.|  
    |EvalOp|Select an evaluation operation. If the `Expression`, **ExpressionAndConstant**, or **ExpressionOrConstant** operation is selected, you can specify an expression.|  
    |Expression|If the evaluation operation includes and expression, provide an expression. The expression must evaluate to a Boolean. For more information about the expression language, see [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md).|  
    |LogicalAnd|Set `LogicalAnd` to specify whether the precedence constraint is evaluated in concert with other precedence constraints, when multiple executables precede and are linked to the constrained executable|  
    |Name|Update the name of the precedence constraint.|  
    |ShowAnnotation|Specify the type of annotation to use. Choose **Never** to disable annotations, **AsNeeded** to enable annotation on demand, **ConstraintName** to automatically annotate using the value of the Name property, **ConstraintDescription** to automatically annotate using the value of the Description property, and **ConstraintOptions** to automatically annotate using the values of the Value and Expression properties.|  
    |Value|If the evaluation operation specified in the EvalOP property includes a constraint, select the execution result of the constraining executable.|  
  
5.  Close the Properties window.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Precedence Constraints](control-flow/precedence-constraints.md)   
 [Connect Tasks and Containers by Using a Default Precedence Constraint](../../2014/integration-services/connect-tasks-and-containers-by-using-a-default-precedence-constraint.md)   
 [Set the Value of a Precedence Constraint by Using the Shortcut Menu](../../2014/integration-services/set-the-value-of-a-precedence-constraint-by-using-the-shortcut-menu.md)   
 [Use an Expression in a Precedence Constraint](../../2014/integration-services/use-an-expression-in-a-precedence-constraint.md)  
  
  
