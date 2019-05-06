---
title: "Set the Properties of a User-Defined Variable | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying variables"
  - "variables [Integration Services], properties"
ms.assetid: f98ddbec-f668-4dba-a768-44ac3ae0536f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Set the Properties of a User-Defined Variable
  To set the properties of a user-defined variable in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], you can use one of the following features:  
  
-   Variables window.  
  
-   Properties window. The **Properties** window lists properties for configuring variables that are not available in the **Variables** window: Description, EvaluateAsExpression, Expression, ReadOnly, ValueType, and IncludeInDebugDump.  
  
> [!NOTE]  
>  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] also provides a set of system variables whose properties cannot be updated, with the exception of the RaiseChangedEvent property.  
  
 **Setting Expressions on Variables**  
  
 When you use the **Properties** window to set expressions on a user-defined variable:  
  
-   The value of a variable can be set by the Value or the Expression property. By default, the EvaluateAsExpression property is set to `False` and the value of the variable is set by the Value property. To use an expression to set the value, you must first set EvaluateAsExpression to `True`, and then provide an expression in the Expression property. The Value property is automatically set to the evaluation result of the expression.  
  
-   The ValueType property contains the data type of the value in the Value property. When Value is set by an expression, ValueType is automatically updated to a data type that is compatible with the evaluation result of the expression. For example, if Value contains 0 and ValueType property contains **Int32** and you then set Expression to GETDATE(), Value contains the current date and time and ValueType is set to `DateTime`.  
  
-   The **Properties** window for the variable provides access to the **Expression Builder** dialog box. You can use this tool to build, validate, and evaluate expressions. For more information, see [Expression Builder](expressions/expression-builder.md) and [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md).  
  
 When you use the **Variables** window to set expressions on a user-defined variable:  
  
-   To use an expression to set the variable value, first confirm that the variable data type is compatible with the evaluation result of the expression and then provide an expression in the `Expression` column of the **Variables** window. The EvaluateAsExpression property in the **Properties** window is automatically set to `True`.  
  
-   When you assign an expression to a variable, a special icon marker displays next to the variable. This special icon marker also displays next to connection managers and tasks that have expressions set on them.  
  
-   The **Variables** window for the variable provides access to the **Expression Builder** dialog box. You can use this tool to build, validate, and evaluate expressions. For more information, see [Expression Builder](expressions/expression-builder.md) and [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md).  
  
 In both the **Variables** and **Properties** window, if you assign an expression to the variable, and `EvaluateAsExpression` is set to `True`, you cannot change the variable data type.  
  
 **Setting the Namespace and Name Properties**  
  
 The values of the `Name` and `Namespace` properties must begin with an alphabetic character letter as defined by the Unicode Standard 2.0, or an underscore (_). Subsequent characters can be letters or numbers as defined in the Unicode Standard 2.0, or the underscore (\_).  
  
## Using the Variables Window to Set Properties  
  
#### To set the properties of a variable by using the Variables window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **SSIS** menu, click **Variables**.  
  
     You can optionally display the **Variables** window by mapping the View.Variables command to a key combination of your choosing on the **Keyboard** page of the **Options** dialog box.  
  
4.  Optionally, in the **Variables** window click **Grid Options**, and then select the columns to appear in the **Variables** window and select the filters to apply to the list of variables.  
  
5.  Select the variable in the list, and then update values in the `Name`, **Data Type**, `Value`, `Namespace`, **Raise Change Event**, **Description,** and `Expression` columns.  
  
6.  Select the variable in the list, and then click **Move Variable** to change the scope.  
  
7.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
## Using the Properties Window to Set Properties  
  
#### To set the properties of a variable by using the Properties window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, right-click the package to open it.  
  
3.  On the **View** menu, click **Properties Window**.  
  
4.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Package Explorer** tab and expand the Package node.  
  
5.  To modify variables with package scope, expand the Variables node; otherwise, expand the Event Handlers or Executables nodes until you locate the Variables node that contains the variable that you want to modify.  
  
6.  Click the variable whose properties you want to modify.  
  
7.  In the **Properties** window, update the read/write variable properties. Some properties are read/read only for user-defined variables.  
  
     For more information about the properties, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md).  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)   
 [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)   
 [Add, Delete, Change Scope of User-Defined Variable in a Package](../../2014/integration-services/add-delete-change-scope-of-user-defined-variable-in-a-package.md)  
  
  
