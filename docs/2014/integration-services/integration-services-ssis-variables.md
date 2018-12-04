---
title: "Integration Services (SSIS) Variables | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "variables [Integration Services], passing between packages"
  - "user-defined variables [Integration Services]"
  - "scope [Integration Services]"
  - "system variables [Integration Services]"
  - "variables [Integration Services]"
  - "variables [Integration Services], about variables"
  - "values [Integration Services]"
ms.assetid: c1e81ad6-628b-46d4-9b09-d2866517b6ca
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services (SSIS) Variables
  Variables store values that a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package and its containers, tasks, and event handlers can use at run time. The scripts in the Script task and the Script component can also use variables. The precedence constraints that sequence tasks and containers into a workflow can use variables when their constraint definitions include expressions.  
  
 You can use variables in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages for the following purposes:  
  
-   Updating properties of package elements at run time. For example, you can dynamically set the number of concurrent executables that a Foreach Loop container allows.  
  
-   Including an in-memory lookup table. For example, a package can run an Execute SQL task that loads a variable with data values.  
  
-   Loading variables with data values and then using them to specify a search condition in a WHERE clause. For example, the script in a Script task can update the value of a variable that is used by a Transact-SQL statement in an Execute SQL task.  
  
-   Loading a variable with an integer and then using the value to control looping within a package control flow. For example, you can use a variable in the evaluation expression of a For Loop container to control iteration.  
  
-   Populating parameter values for Transact-SQL statements at run time. For example, a package can run an Execute SQL task and then use variables to dynamically set the parameters in a Transact-SQL statement.  
  
-   Building expressions that include variable values. For example, the Derived Column transformation can populate a column with the result obtained by multiplying a variable value by a column value.  
  
## System and User-Defined Variables  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] supports two types of variables: user-defined variables and system variables. User-defined variables are defined by package developers, and system variables are defined by [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. You can create as many user-defined variables as a package requires, but you cannot create additional system variables.  
  
 All variables-system and user-defined-can be used in the parameter bindings that the Execute SQL task uses to map variables to parameters in SQL statements. For more information, see [Execute SQL Task](control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md).  
  
> [!NOTE]  
>  The names of user-defined and system variables are case sensitive.  
  
 You can create user-defined variables for all [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] container types: packages, Foreach Loop containers, For Loop containers, Sequence containers, tasks, and event handlers. User-defined variables are members of the Variables collection of the container.  
  
 If you create the package using [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, you can see the members of the Variables collections in the **Variables** folders on the **Package Explorer** tab of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. The folders list user-defined variables and system variables.  
  
 You can configure user-defined variables in the following ways:  
  
-   Provide a name and description for the variable.  
  
-   Specify a namespace for the variable.  
  
-   Indicate whether the variable raises an event when its value changes.  
  
-   Indicate whether the variable is read-only or read/write.  
  
-   Use the evaluation result of an expression to set the variable value.  
  
-   Create the variable in the scope of the package or a package object such as a task.  
  
-   Specify the value and data type of the variable.  
  
 The only configurable option on system variables is specifying whether they raise an event when they change value.  
  
 A different set of system variables is available for different container types. For more information about the system variables used by packages and their elements, see [System Variables](system-variables.md).  
  
 For more information about real-life use scenarios for variables, see [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md).  
  
## Variable Properties  
 You can configure user-defined variables by setting the following properties in either the **Variables** window or the **Properties** window. Certain properties are available only in the Properties window.  
  
> [!NOTE]  
>  The only configurable option on system variables is specifying whether they raise an event when they change value.  
  
 Description  
 Specifies the description of the variable.  
  
 EvaluateAsExpression  
 When the property is set to `True`, the expression provided is used to set the variable value.  
  
 Expression  
 Specifies the expression that is assigned to the variable.  
  
 Name  
 Specifies the variable name.  
  
 Namespace  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides two namespaces, **User** and **System**. By default, custom variables are in the **User** namespace, and system variables are in the **System** namespace. You can create additional namespaces for user-defined variables and change the name of the **User** namespace, but you cannot change the name of the **System** namespace, add variables to the **System** namespace, or assign system variables to a different namespace.  
  
 RaiseChangedEvent  
 When the property is set to `True`, the `OnVariableValueChanged` event is raised when the variable changes value.  
  
 ReadOnly  
 When the property is set to `False`, the variable is read\write.  
  
 Scope  
 > [!NOTE]  
>  You can change this property setting only by clicking **Move Variable** in the **Variables** window.  
  
 A variable is created within the scope of a package or within the scope of a container, task, or event handler in the package. Because the package container is at the top of the container hierarchy, variables with package scope function like global variables and can be used by all containers in the package. Similarly, variables defined within the scope of a container such as a For Loop container can be used by all tasks or containers within the For Loop container.  
  
 If a package runs other packages by using the Execute Package task, the variables defined in the scope of the calling package or the Execute Package task can be made available to the called package by using the Parent Package Variable configuration type. For more information, see [Package Configurations](../../2014/integration-services/package-configurations.md).  
  
 IncludeInDebugDump  
 Indicate whether the variable value is included in the debug dump files.  
  
 For user-defined variables and system variables, the default value for the **InclueInDebugDump** option is `true`.  
  
 However, for user-defined variables, the system resets the **IncludeInDebugDump** option to `false` when the following conditions are met:  
  
-   If the **EvaluateAsExpression** variable property is set to `true`, the system resets the **IncludeInDebugDump** option to `false`.  
  
     To include the text of the expression as the variable value in the debug dump files, set the **IncludeInDebugDump** option to `true`.  
  
-   If the variable data type is changed to a string, the system resets the **IncludeInDebugDump** option to `false`.  
  
 When the system resets the **IncludeInDebugDump** option to `false`, this might override the value selected by the user.  
  
 Value  
 The value of a user-defined variable can be a literal or an expression. A variable includes options for setting the variable value and the data type of the value. The two properties must be compatible: for example, the use of a string value together with an integer data type is not valid.  
  
 If the variable is configured to evaluate as an expression, you must provide an expression. At run time, the expression is evaluated, and the variable is set to the evaluation result. For example, if a variable uses the expression `DATEPART("month", GETDATE())` the value of the variable is the number equivalent of the month for the current date. The expression must be a valid expression that uses the [!INCLUDE[ssIS](../includes/ssis-md.md)] expression grammar syntax. When an expression is used with variables, the expression can use literals and the operators and functions that the expression grammar provides, but the expression cannot reference the columns from a data flow in the package. The maximum length of an expression is 4000 characters. For more information, see [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md).  
  
 ValueType  
 > [!NOTE]  
>  The property value appears in the **Data type** column in the **Variables** window.  
  
 Specifies the data type of the variable value.  
  
## Configuring Variables  
 You can set properties through [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, see [Variables Window](../../2014/integration-services/variables-window.md).  
  
 To learn more about variable properties, and for more information about programmatically setting these properties, see <xref:Microsoft.SqlServer.Dts.Runtime.Variable>.  
  
## Related Tasks  
 [Add, Delete, Change Scope of User-Defined Variable in a Package](../../2014/integration-services/add-delete-change-scope-of-user-defined-variable-in-a-package.md)  
  
 [Set the Properties of a User-Defined Variable](../../2014/integration-services/set-the-properties-of-a-user-defined-variable.md)  
  
 [Use the Values of Variables and Parameters in a Child Package](../../2014/integration-services/use-the-values-of-variables-and-parameters-in-a-child-package.md)  
  
 [Map Query Parameters to Variables in a Data Flow Component](data-flow/map-query-parameters-to-variables-in-a-data-flow-component.md)  
  
  
