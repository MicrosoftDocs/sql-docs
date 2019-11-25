---
title: "Expressions Page | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.expressionspage.f1"
helpviewer_keywords: 
  - "Expressions Page dialog box"
ms.assetid: c9016ec6-11c1-4ebd-b2a7-0fa6631fd9e4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Expressions Page
  Use the **Expressions** page to edit property expressions and to access the **Property Expressions Editor** and **Property Expression Builder** dialog boxes.  
  
 Property expressions update the values of properties when the package is run. Property expressions can be used with the properties of packages, tasks, containers, connection managers, as well as some data flow components. The expressions are evaluated and their results are used instead of the values to which you set the properties when you configured the package and package objects. The expressions can include variables and the functions and operators that the expression language provides. For example, you can generate the subject line for the Send Mail task by concatenating the value of a variable that contains the string "Weather forecast for " and the return results of the GETDATE() function to make the string "Weather forecast for 4/5/2006".  
  
 To learn more about writing expressions and using property expressions, see [Integration Services &#40;SSIS&#41; Expressions](integration-services-ssis-expressions.md) and [Use Property Expressions in Packages](use-property-expressions-in-packages.md).  
  
## Options  
 **Expressions (...)**  
 Click the ellipsis to open the **Property Expressions Editor** dialog box. For more information, see [Property Expressions Editor](property-expressions-editor.md).  
  
 **\<property name>**  
 Click the ellipsis to open the **Expression Builder** dialog box. For more information, see [Expression Builder](expression-builder.md).  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md)   
 [System Variables](../system-variables.md)   
 [Integration Services &#40;SSIS&#41; Expressions](integration-services-ssis-expressions.md)  
  
  
