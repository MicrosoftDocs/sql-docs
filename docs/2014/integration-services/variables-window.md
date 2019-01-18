---
title: "Variables Window | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.variables.f1"
helpviewer_keywords: 
  - "Variables Window dialog box"
ms.assetid: f405e5ce-ef69-4c58-8c7d-a3d44dfe9ab0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Variables Window
  Use the **Variables** window to create and modify user-defined variables and view system variables.  
  
 By default, the **Variables** window is located below the **Connection Managers** area in the SSIS Designer, in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. If you don't see the **Variables** window, click **Variables** on the **SSIS** menu to display the window.  
  
 You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
> [!NOTE]
>  The values of the `Name` and `Namespace` properties must begin with an alphabetic character letter as defined by the Unicode Standard 2.0, or an underscore (_). Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, or the underscore (\_).  
  
## Options  
 **Add Variable**  
 Add a user-defined variable.  
  
 **Move Variable**  
 Click a variable in the list, and then click **Move Variable** to change the variable scope. In the **Select New Scope** dialog box, select the package or a container, task, or event handler in the package, to change the variable scope.  
  
 For more information about variable scope, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md).  
  
 **Delete Variable**  
 Select a variable from the list, and then click **Delete Variable**.  
  
 **Grid Options**  
 Click to open the **Variable Grid Options** dialog box where you can change the column selection and apply filters to the **Variables** window. For more information, see [Variable Grid Options](../../2014/integration-services/variable-grid-options.md).  
  
 `Name`  
 View the variable name. You can update the name for user-defined variables.  
  
 **Scope**  
 View the scope of the variable. A variable has either the scope of the entire package, or the scope of a container or task. The scope of the variable must be sufficient so that the variable is visible to any other tasks or components that need to read or set its value.  
  
 You can change the scope by clicking the variable and then clicking **Move Variable** in the **Variables** window.  
  
 **Data Type**  
 View the data type of the variable. You can select a data type from the list for user-defined variables.  
  
> [!NOTE]  
>  If you assign an expression to the variable, you cannot change the data type.  
  
 **Value**  
 View the variable value. You can update the value for user-defined variables. This value can be a literal or an expression, and the value can be a multi-line string. To assign an expression to the variable, click the ellipse button that is next to the **Expression** column in the **Variables** window.  
  
 `Namespace`  
 View the namespace name. User-defined variables are initially created in the **User** namespace, but you can change the namespace name in the `Namespace` field. To display this column, click **Grid Options**.  
  
 **Raise Change Event**  
 Indicate whether to raise the `OnVariableValueChanged` event when a value changes. You can update the value for user-defined and system variables. By default, the **Variables** window does not list this column. To display this column, click **Grid Options**.  
  
 **Description**  
 View the variable description. You can change the description for user-defined variables. By default, the **Variables** window does not list this column. To display this column, click **Grid Options**.  
  
 **Expression**  
 View the expression assigned to the variable. To assign an expression, click the ellipse button.  
  
 If you assign an expression to a variable, a special icon marker displays next to the variable. This special icon marker also displays next to connection managers and tasks that have expressions set on them.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)   
 [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)   
 [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md)   
 [Generating Dump Files for Package Execution](troubleshooting/generating-dump-files-for-package-execution.md)  
  
  
