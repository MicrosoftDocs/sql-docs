---
title: "Integration Services (SSIS) Variables | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
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
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services (SSIS) Variables

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Variables store values that a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package and its containers, tasks, and event handlers can use at run time. The scripts in the Script task and the Script component can also use variables. The precedence constraints that sequence tasks and containers into a workflow can use variables when their constraint definitions include expressions.  
  
 You can use variables in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages for the following purposes:  
  
-   Updating properties of package elements at run time. For example, you can dynamically set the number of concurrent executables that a Foreach Loop container allows.  
  
-   Including an in-memory lookup table. For example, a package can run an Execute SQL task that loads a variable with data values.  
  
-   Loading variables with data values and then using them to specify a search condition in a WHERE clause. For example, the script in a Script task can update the value of a variable that is used by a Transact-SQL statement in an Execute SQL task.  
  
-   Loading a variable with an integer and then using the value to control looping within a package control flow. For example, you can use a variable in the evaluation expression of a For Loop container to control iteration.  
  
-   Populating parameter values for Transact-SQL statements at run time. For example, a package can run an Execute SQL task and then use variables to dynamically set the parameters in a Transact-SQL statement.  
  
-   Building expressions that include variable values. For example, the Derived Column transformation can populate a column with the result obtained by multiplying a variable value by a column value.  
  
## System and user-defined variables  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] supports two types of variables: user-defined variables and system variables. User-defined variables are defined by package developers, and system variables are defined by [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. You can create as many user-defined variables as a package requires, but you cannot create additional system variables.  
  
 All variables-system and user-defined-can be used in the parameter bindings that the Execute SQL task uses to map variables to parameters in SQL statements. For more information, see [Execute SQL Task](../integration-services/control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](https://msdn.microsoft.com/library/a3ca65e8-65cf-4272-9a81-765a706b8663).  
  
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
  
 A different set of system variables is available for different container types. For more information about the system variables used by packages and their elements, see [System Variables](../integration-services/system-variables.md).  
  
 For more information about real-life use scenarios for variables, see [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
## Properties of variables  
 You can configure user-defined variables by setting the following properties in either the **Variables** window or the **Properties** window. Certain properties are available only in the Properties window.  
  
> [!NOTE]  
>  The only configurable option on system variables is specifying whether they raise an event when they change value.  
  
 **Description**    
 Specifies the description of the variable.  
  
 **EvaluateAsExpression**    
 When the property is set to **True**, the expression provided is used to set the variable value.  
  
 **Expression**    
 Specifies the expression that is assigned to the variable.  
  
 **Name**    
 Specifies the variable name.  
  
 **Namespace**  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides two namespaces, **User** and **System**. By default, custom variables are in the **User** namespace, and system variables are in the **System** namespace. You can create additional namespaces for user-defined variables and change the name of the **User** namespace, but you cannot change the name of the **System** namespace, add variables to the **System** namespace, or assign system variables to a different namespace.  
  
**RaiseChangedEvent**  
 When the property is set to **True**, the **OnVariableValueChanged** event is raised when the variable changes value.  
  
 **ReadOnly**  
 When the property is set to **False**, the variable is read\write.  
  
**Scope**    
 > [!NOTE]  
>  You can change this property setting only by clicking **Move Variable** in the **Variables** window.  
  
 A variable is created within the scope of a package or within the scope of a container, task, or event handler in the package. Because the package container is at the top of the container hierarchy, variables with package scope function like global variables and can be used by all containers in the package. Similarly, variables defined within the scope of a container such as a For Loop container can be used by all tasks or containers within the For Loop container.  
  
 If a package runs other packages by using the Execute Package task, the variables defined in the scope of the calling package or the Execute Package task can be made available to the called package by using the Parent Package Variable configuration type. For more information, see [Package Configurations](../integration-services/packages/package-configurations.md).  
  
**IncludeInDebugDump**  
 Indicate whether the variable value is included in the debug dump files.  
  
 For user-defined variables and system variables, the default value for the **InclueInDebugDump** option is **true**.  
  
 However, for user-defined variables, the system resets the **IncludeInDebugDump** option to **false** when the following conditions are met:  
  
-   If the **EvaluateAsExpression** variable property is set to **true**, the system resets the **IncludeInDebugDump** option to **false**.  
  
     To include the text of the expression as the variable value in the debug dump files, set the **IncludeInDebugDump** option to **true**.  
  
-   If the variable data type is changed to a string, the system resets the **IncludeInDebugDump** option to **false**.  
  
 When the system resets the **IncludeInDebugDump** option to **false**, this might override the value selected by the user.  
  
**Value**    
The value of a user-defined variable can be a literal or an expression. The value of a variable can't be null. Variables have the following default values:

| Data type | Default value |
|---|---|
| Boolean | False |
| Numeric and binary data types | 0 (zero) |
| Char and string data types | (empty string) |
| Object | System.Object |
| | |

A variable has options for setting the variable value and the data type of the value. The two properties must be compatible: for example, the use of a string value together with an integer data type is not valid.  
  
 If the variable is configured to evaluate as an expression, you must provide an expression. At run time, the expression is evaluated, and the variable is set to the evaluation result. For example, if a variable uses the expression `DATEPART("month", GETDATE())` the value of the variable is the number equivalent of the month for the current date. The expression must be a valid expression that uses the [!INCLUDE[ssIS](../includes/ssis-md.md)] expression grammar syntax. When an expression is used with variables, the expression can use literals and the operators and functions that the expression grammar provides, but the expression cannot reference the columns from a data flow in the package. The maximum length of an expression is 4000 characters. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../integration-services/expressions/integration-services-ssis-expressions.md).  
  
**ValueType**    
 > [!NOTE]  
>  The property value appears in the **Data type** column in the **Variables** window.  
  
 Specifies the data type of the variable value.  

## Scenarios for using variables  
 Variables are used in many different ways in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. You will probably find that package development does not progress far before you have to add a user-defined variable to your package to implement the flexibility and manageability your solution requires. Depending on the scenario, system variables are also commonly used.  
  
 **Property Expressions** Use variables to provide values in the property expressions that set the properties of packages and package objects. For example, the expression, `SELECT * FROM @varTableName` includes the variable `varTableName` that updates the SQL statement that an Execute SQL task runs. The expression, `DATEPART("d", GETDATE()) == 1? @[User::varPackageFirst]:@[User::varPackageOther]`", updates the package that the Execute Package task runs, by running the package specified in the `varPackageFirst` variable on the first day of the month and running the package specified in the `varPackageOther` variable on other days. For more information, see [Use Property Expressions in Packages](../integration-services/expressions/use-property-expressions-in-packages.md).  
  
 **Data Flow Expressions** Use variables to provide values in the expressions that the Derived Column and Conditional Split transformations use to populate columns, or to direct data rows to different transformation outputs. For example, the expression, `@varSalutation + LastName`, concatenates the value in the `VarSalutation` variable and the `LastName` column. The expression, `Income < @HighIncome`, directs data rows in which the value of the `Income` column is less than the value in the `HighIncome` variable to an output. For more information, see [Derived Column Transformation](../integration-services/data-flow/transformations/derived-column-transformation.md), [Conditional Split Transformation](../integration-services/data-flow/transformations/conditional-split-transformation.md), and [Integration Services &#40;SSIS&#41; Expressions](../integration-services/expressions/integration-services-ssis-expressions.md).  
  
 **Precedence Constraint Expressions** Provide values to use in precedence constraints to determine whether a constrained executable runs. The expressions can be used either together with an execution outcome (success, failure, completion), or instead of an execution outcome. For example, if the expression, `@varMax > @varMin`, evaluates to **true**, the executable runs. For more information, see [Add Expressions to Precedence Constraints](https://msdn.microsoft.com/library/5574d89a-a68e-4b84-80ea-da93305e5ca1).  
  
 **Parameters and Return Codes** Provide values to input parameters, or store the values of output parameters and return codes. You do this by mapping the variables to parameters and return values. For example, if you set the variable `varProductId` to 23 and run the SQL statement, `SELECT * from Production.Product WHERE ProductID = ?`, the query retrieves the product with a `ProductID` of 23. For more information, see [Execute SQL Task](../integration-services/control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](https://msdn.microsoft.com/library/a3ca65e8-65cf-4272-9a81-765a706b8663).  
  
 **For Loop Expressions** Provide values to use in the initialization, evaluation, and assignment expressions of the For Loop. For example, if the variable `varCount` is 2 and `varMaxCount` is 10, the initialization expression is `@varCount`, the evaluation expression is  `@varCount < @varMaxCount`, and the assignment expression is `@varCount =@varCount +1`, then the loop repeats 8 times. For more information, see [For Loop Container](../integration-services/control-flow/for-loop-container.md).  
  
 **Parent Package Variable Configurations** Pass values from parent packages to child packages. Child packages can access variables in the parent package by using parent package variable configurations. For example, if the child package must use the same date as the parent package, the child package can define a parent package variable configuration that specifies a variable set by the GETDATE function in the parent package. For more information, see [Execute Package Task](../integration-services/control-flow/execute-package-task.md) and [Package Configurations](../integration-services/packages/package-configurations.md).  
  
 **Script Task and Script Component** Provide a list of read-only and read/write variable to the Script task or Script component, update the read/write variables within the script, and then use the updated values in or outside the script. For example, in the code, `numberOfCars = CType(Dts.Variables("NumberOfCars").Value, Integer)`, the script variable `numberOfCars` is updated by the value in the variable, `NumberOfCars`. For more information, see [Using Variables in the Script Task](../integration-services/extending-packages-scripting/task/using-variables-in-the-script-task.md).  

## Add a variable  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package you want to work with.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, to define the scope of the variable, do one of the following:  
  
    -   To set the scope to the package, click anywhere on the design surface of the **Control Flow** tab.  
  
    -   To set the scope to an event handler, select an executable and an event handler on the design surface of the **Event Handler** tab.  
  
    -   To set the scope to a task or container, on the design surface of the **Control Flow** tab or the **Event Handler** tab, click a task or container.  
  
4.  On the **SSIS** menu, click **Variables**. You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
5.  In the **Variables** window, click the **Add Variable** icon. The new variable is added to the list.  
  
6.  Optionally, click the **Grid Options** icon, select additional columns to show in the **Variables Grid Options** dialog box, and then click **OK**.  
  
7.  Optionally, set the variable properties. For more information, see [Set the Properties of a User-Defined Variable](https://msdn.microsoft.com/library/f98ddbec-f668-4dba-a768-44ac3ae0536f).  
  
8.  To save the updated package, click **Save Selected Items** on the **File** menu.  

### Add Variable dialog box
Use the **Add Variable** dialog box to specify the properties of a new variable.  
  
#### Options  
 **Container**  
 Select a container in the list. The container defines the scope of the variable. The container can be either the package or an executable in the package.  
  
 **Name**  
 Type the variable name.  
  
 **Namespace**  
 Specify the namespace of the variable. By default, user-defined variables are in the **User** namespace.  
  
 **Value type**  
 Select a data type.  
  
 **Value**  
 Type a value. The value must be compatible with the data type specified in the **Value type** option.  
  
 **Read-only**  
 Select to make the variable read-only.  
   
## Delete a variable  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **SSIS** menu, click **Variables**. You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
4.  Select the variable to delete, and then click **Delete Variable**.  
  
     If you don't see the variable in the Variables window, click **Grid Options** and then select **Show variables of all scopes**.  
  
5.  If the **Confirm Deletion of Variables** dialog box opens, click **Yes** to confirm.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## Change the scope of a variable  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **SSIS** menu, click **Variables**. You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
4.  Select the variable and then click **Move Variable**.  
  
     If you don't see the variable in the Variables window, click **Grid Options** and then select **Show variables of all scopes**.  
  
5.  In the **Select New Scope** dialog box, select the package or a container, task, or event handler in the package, to change the variable scope.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  

## Set the properties of a user-defined variable
 To set the properties of a user-defined variable in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], you can use one of the following features:  
  
-   Variables window.  
  
-   Properties window. The **Properties** window lists properties for configuring variables that are not available in the **Variables** window: Description, EvaluateAsExpression, Expression, ReadOnly, ValueType, and IncludeInDebugDump.  
  
> [!NOTE]  
>  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] also provides a set of system variables whose properties cannot be updated, with the exception of the RaiseChangedEvent property.  
  
### Set expressions on variables  
  
 When you use the **Properties** window to set expressions on a user-defined variable:  
  
-   The value of a variable can be set by the Value or the Expression property. By default, the EvaluateAsExpression property is set to **False** and the value of the variable is set by the Value property. To use an expression to set the value, you must first set EvaluateAsExpression to **True**, and then provide an expression in the Expression property. The Value property is automatically set to the evaluation result of the expression.  
  
-   The ValueType property contains the data type of the value in the Value property. When Value is set by an expression, ValueType is automatically updated to a data type that is compatible with the evaluation result of the expression. For example, if Value contains 0 and ValueType property contains **Int32** and you then set Expression to GETDATE(), Value contains the current date and time and ValueType is set to **DateTime**.  
  
-   The **Properties** window for the variable provides access to the **Expression Builder** dialog box. You can use this tool to build, validate, and evaluate expressions. For more information, see [Expression Builder](../integration-services/expressions/expression-builder.md) and [Integration Services &#40;SSIS&#41; Expressions](../integration-services/expressions/integration-services-ssis-expressions.md).  
  
 When you use the **Variables** window to set expressions on a user-defined variable:  
  
-   To use an expression to set the variable value, first confirm that the variable data type is compatible with the evaluation result of the expression and then provide an expression in the **Expression** column of the **Variables** window. The EvaluateAsExpression property in the **Properties** window is automatically set to **True**.  
  
-   When you assign an expression to a variable, a special icon marker displays next to the variable. This special icon marker also displays next to connection managers and tasks that have expressions set on them.  
  
-   The **Variables** window for the variable provides access to the **Expression Builder** dialog box. You can use this tool to build, validate, and evaluate expressions. For more information, see [Expression Builder](../integration-services/expressions/expression-builder.md) and [Integration Services &#40;SSIS&#41; Expressions](../integration-services/expressions/integration-services-ssis-expressions.md).  
  
 In both the **Variables** and **Properties** window, if you assign an expression to the variable, and **EvaluateAsExpression** is set to **True**, you cannot change the variable data type.  
  
### Set the Namespace and Name properties
  
 The values of the **Name** and **Namespace** properties must begin with an alphabetic character letter as defined by the Unicode Standard 2.0, or an underscore (_). Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, or the underscore (\_).  
  
### Set Variable Properties in the Variables Window   
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **SSIS** menu, click **Variables**.  
  
     You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
4.  Optionally, in the **Variables** window click **Grid Options**, and then select the columns to appear in the **Variables** window and select the filters to apply to the list of variables.  
  
5.  Select the variable in the list, and then update values in the **Name**, **Data Type**, **Value**, **Namespace**, **Raise Change Event**, **Description,** and **Expression** columns.  
  
6.  Select the variable in the list, and then click **Move Variable** to change the scope.  
  
7.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
### Set Variable Properties in the Properties Window  

1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **View** menu, click **Properties Window**.  
  
4.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Package Explorer** tab and expand the Package node.  
  
5.  To modify variables with package scope, expand the Variables node; otherwise, expand the Event Handlers or Executables nodes until you locate the Variables node that contains the variable that you want to modify.  
  
6.  Click the variable whose properties you want to modify.  
  
7.  In the **Properties** window, update the read/write variable properties. Some properties are read/read only for user-defined variables.  
  
     For more information about the properties, see [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md).  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  

## Update a variable dynamically with configurations  
 To dynamically update variables, you can create configurations for the variables, deploy the configurations with the package, and then update the variable values in the configuration file when you deploy the packages. At run time, the package uses the updated variable values. For more information, see [Create Package Configurations](../integration-services/packages/create-package-configurations.md).  

## Related Tasks  
 [Use the Values of Variables and Parameters in a Child Package](../integration-services/packages/legacy-package-deployment-ssis.md#child)  
  
 [Map Query Parameters to Variables in a Data Flow Component](../integration-services/data-flow/map-query-parameters-to-variables-in-a-data-flow-component.md)  
