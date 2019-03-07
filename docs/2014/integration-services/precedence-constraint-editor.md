---
title: "Precedence Constraint Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.precedenceconstraint.f1"
helpviewer_keywords: 
  - "Precedence Constraint Editor dialog box"
ms.assetid: b10d4330-6e35-4037-b309-ef56efcd60c5
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Precedence Constraint Editor
  Use the **Precedence Constraint Editor** dialog box to configure precedence constraints.  
  
## Options  
 **Evaluation operation**  
 Specify the evaluation operation that the precedence constraint uses. The operations are: **Constraint**, **Expression**, **Expression and Constraint**, and **Expression or Constraint**.  
  
 **Value**  
 Specify the constraint value: **Success**, **Failure**, or **Completion**.  
  
> [!NOTE]  
>  The precedence constraint line is green for **Success**, highlighted for **Failure**, and blue for **Completion**.  
  
 **Expression**  
 If using the operations **Expression**, **Expression and Constraint**, or **Expression or Constraint**, type an expression or launch the Expression Builder to create the expression. The expression must evaluate to a Boolean.  
  
 **Test**  
 Validate the expression.  
  
 **Logical AND**  
 Select to specify that multiple precedence constraints on the same executable must be evaluated together. All constraints must evaluate to `True`.  
  
> [!NOTE]  
>  This type of precedence constraint appears as a solid green, highlighted or blue line.  
  
 **Logical OR**  
 Select to specify that multiple precedence constraints on the same executable must be evaluated together. At least one constraint must evaluate to `True`.  
  
> [!NOTE]  
>  This type of precedence constraint appears as a dotted green, highlighted, or blue line.  
  
## See Also  
 [Precedence Constraints](control-flow/precedence-constraints.md)   
 [Integration Services Tasks](control-flow/integration-services-tasks.md)   
 [Integration Services Containers](control-flow/integration-services-containers.md)   
 [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md)  
  
  
