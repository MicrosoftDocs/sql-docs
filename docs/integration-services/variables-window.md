---
title: "Variables Window | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.variables.f1"
  - "sql13.dts.designer.variableoptionswindow.f1"
helpviewer_keywords: 
  - "Variables Window dialog box"
ms.assetid: f405e5ce-ef69-4c58-8c7d-a3d44dfe9ab0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Variables Window
  Use the **Variables** window to create and modify user-defined variables and view system variables.  
  
 By default, the **Variables** window is located below the **Connection Managers** area in the SSIS Designer, in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. If you don't see the **Variables** window, click **Variables** on the **SSIS** menu to display the window.  
  
 You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
> [!NOTE]
>  The values of the **Name** and **Namespace** properties must begin with an alphabetic character letter as defined by the Unicode Standard 2.0, or an underscore (_). Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, or the underscore (\_).  
  
## Options  
 **Add Variable**  
 Add a user-defined variable.  
  
 **Move Variable**  
 Click a variable in the list, and then click **Move Variable** to change the variable scope. In the **Select New Scope** dialog box, select the package or a container, task, or event handler in the package, to change the variable scope.  
  
 For more information about variable scope, see [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md).  
  
 **Delete Variable**  
 Select a variable from the list, and then click **Delete Variable**.  
  
 **Grid Options**  
 Click to open the **Variable Grid Options** dialog box where you can change the column selection and apply filters to the **Variables** window. For more information, see [Variable Grid Options](../integration-services/variable-grid-options.md).  
  
 **Name**  
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
  
 **Namespace**  
 View the namespace name. User-defined variables are initially created in the **User** namespace, but you can change the namespace name in the **Namespace** field. To display this column, click **Grid Options**.  
  
 **Raise Change Event**  
 Indicate whether to raise the **OnVariableValueChanged** event when a value changes. You can update the value for user-defined and system variables. By default, the **Variables** window does not list this column. To display this column, click **Grid Options**.  
  
 **Description**  
 View the variable description. You can change the description for user-defined variables. By default, the **Variables** window does not list this column. To display this column, click **Grid Options**.  
  
 **Expression**  
 View the expression assigned to the variable. To assign an expression, click the ellipse button.  
  
 If you assign an expression to a variable, a special icon marker displays next to the variable. This special icon marker also displays next to connection managers and tasks that have expressions set on them.  

## Variable Grid Options dialog box
 Use the **Variable Grid Options** dialog box to select the columns that will display in the **Variables** window and to select the filters to apply to the list of variables. For more information about the corresponding variable properties, see [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md).  
  
### Options for Filter  
 **Show system variables**  
 Select to list system variables in the **Variables** window. System variables are predefined. You cannot add or delete system variables. You can modify the **RaiseChangedEvent** property setting.  
  
 This list is color coded. System variables are gray, and user-defined variables are black.  
  
 **Show variables of all scopes**  
 Select to show variables within the scope the package, and within the scope of containers, tasks, and event handlers in the package. Clear this option to show only variables within the scope of the package and within the scope of a selected container, task, or event handler.  
  
 For more information about variable scope, see [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md).  
  
### Options for Columns  
 Select the columns that you want to appear in the **Variables** window.  
  
-   **Scope**  
  
-   **Data type**  
  
-   **Value**  
  
-   **Namespace**  
  
-   **Raise event when variable value changes**  
  
-   **Description**  
  
-   **Expression**  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md)   
 [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)   
 [Integration Services &#40;SSIS&#41; Expressions](../integration-services/expressions/integration-services-ssis-expressions.md)   
 [Generating Dump Files for Package Execution](../integration-services/troubleshooting/generating-dump-files-for-package-execution.md)  
  
  
