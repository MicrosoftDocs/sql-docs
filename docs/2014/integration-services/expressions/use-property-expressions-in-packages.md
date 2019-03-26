---
title: "Use Property Expressions in Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "packages [Integration Services], expressions"
  - "Integration Services packages, expressions"
  - "SQL Server Integration Services packages, expressions"
  - "dynamic properties"
  - "updating package properties"
  - "SSIS packages, expressions"
  - "expressions [Integration Services], property expressions"
  - "property expressions [Integration Services]"
ms.assetid: a4bfc925-3ef6-431e-b1dd-7e0023d3a92d
author: janinezhang
ms.author: janinez
manager: craigg
---
# Use Property Expressions in Packages
  A property expression is an expression that is assigned to a property to enable dynamic update of the property at run time. For example, a property expression can update the To line that a Send Mail task uses by inserting an e-mail address that is stored in a variable.  
  
 An expression can be added to a package, task, Foreach Loop, For Loop, Sequence, Foreach enumerator, event handler, a package or project level connection manager, or log provider. Any property of these objects that is read/write can implement a property expression. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] also supports the use of property expressions in some custom properties of data flow components. Variables and precedence constraints do not support property expressions, but they include special properties in which you can use expressions.  
  
 Property expressions can be updated in different ways:  
  
-   User-defined variables can be included in package configurations and then updated when the package is deployed. At run time, the property expression is evaluated using the updated variable value.  
  
-   System variables that are included in expressions are updated at run time, which changes the results of the property evaluation.  
  
-   Date and time functions are evaluated at run time and provide the updated values to property expressions.  
  
-   Variables in expressions can be updated by the scripts that the Script task and the Script component run.  
  
 The expressions are built using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] expression language. The expressions can use system or user-defined variables, together with the operators, functions, and type casts that the expression language provides.  
  
> [!NOTE]  
>  The names of user-defined and system variables are case-sensitive.  
  
 For more information, see [Integration Services &#40;SSIS&#41; Expressions](integration-services-ssis-expressions.md).  
  
 An important use of property expressions is to customize configurations for each deployed instance of a package. This makes it possible to dynamically update package properties for different environments. For example, you can create a property expression that assigns a variable to the connection string of a connection manager, and then update the variable when the package is deployed, ensuring that the connection string is correct at run time. Package configurations are loaded before the property expressions are evaluated.  
  
 A property can use only one property expression and a property expression can apply only to one property. However, you can build multiple identical property expressions and assign them to different properties.  
  
 Some properties are set by using values from enumerators. When you reference the enumerator member in a property expression, you must use the numeric value that is equivalent to the friendly name of the enumerator member. For example, if a property expression sets the `LoggingMode` property, which uses a value from the `DTSLoggingMode` enumeration, the property expression must use 0, 1, or 2 instead of the friendly names `Enabled`, `Disabled`, or `UseParentSetting`. For more information, see [Enumerated Constants in Property Expressions](enumerated-constants-in-property-expressions.md).  
  
## Property Expression User Interface  
 [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] provides a set of tools for building and managing property expressions.  
  
-   The **Expressions** page, found in the custom editors for tasks, the For Loop container, and the Foreach containers. The **Expressions** page lets you edit expressions and view a list of the property expressions that a task, Foreach Loop, or For Loop uses.  
  
-   The **Properties** window, for editing expressions and viewing a list of the property expressions that a package or package objects use.  
  
-   The **Property Expressions Editor** dialog box, for creating, updating, and deleting property expressions.  
  
-   The **Expression Builder** dialog box, for building an expression using graphical tools. The **Expression Builder** dialog box can evaluate expressions for your review without assigning the evaluation result to the property.  
  
 The following diagram shows the user interfaces that you use to add, change, and remove property expressions.  
  
 ![The user interface for property expressions](../media/ssis-propertyexpressionui.gif "The user interface for property expressions")  
  
 In the **Properties** window and the **Expressions** page, click the browse button **(...)** at the **Expressions** collection level to open the **Property Expressions Editor** dialog box. The Property Expressions Editor allows you to map a property to an expression and to type a property expression. If you want to use the graphical expression tools to create and then validate the expression, click the browse button **(...)** at the expression level to open the **Expression Builder** dialog box, and then create or modify and optionally validate the expression.  
  
 You can also open the **Expression Builder** dialog box from the **Property Expressions Editor** dialog box.  
  
#### To work with property expressions  
  
-   [Add or Change a Property Expression](add-or-change-a-property-expression.md)  
  
### Setting Property Expressions of Data Flow Components  
 If you construct a package in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the properties of data flow components that support property expressions are exposed on the Data Flow task to which they belong. To add, change, and remove the property expressions of data flow components, you right-click the Data Flow task for the data flow to which the data flow components belong and click **Properties**. The Properties window lists the properties of data flow components with which you can use property expressions. For example, to create or modify a property expression for the SamplingValue property of a Row Sampling transformation in a data flow named SampleCustomer, right-click the Data Flow task for the data flow to which the Row Sampling transformation belongs and click **Properties**. The SamplingValue property is listed in the Properties window, and has the format [SampleCustomer].[SamplingValue].  
  
 In the Properties window, you add, change, and remove property expressions for data flow components in the same way as property expressions for other [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] object types. The Properties window also provides access to the various dialog boxes and builders that you use to add, change, or remove property expressions for data flow components. For more information about the properties of data flow components that can be updated by property expressions, see [Transformation Custom Properties](../data-flow/transformations/transformation-custom-properties.md).  
  
## Loading Property Expressions  
 You cannot specify or control when property expressions are loaded. The property expressions are evaluated and loaded when the package and the package objects are validated. Validation occurs when you save the package, open the package in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, and run the package.  
  
 You will therefore not see the updated values of the properties of the package objects that use property expressions in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer until you save the package, run the package, or reopen the package after adding the property expressions.  
  
 The property expressions associated with different types of objects-connection managers, log providers, and enumerators-are also loaded when methods specific to that object type are called. For example, the properties of connection managers are loaded before [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] creates an instance of the connection.  
  
 Property expressions are loaded after package configurations have loaded. For example, variables are updated first by their configurations, and then the property expressions that use the variables are evaluated and loaded. This means that the property expressions always use the values of variables that are set by configurations.  
  
> [!NOTE]  
>  You cannot use the `Set` option of the **dtexec** utility to populate a property expression.  
  
 The following table summarizes when property expressions of [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] are evaluated and loaded.  
  
|Object type|Load and evaluate|  
|-----------------|-----------------------|  
|Package, Foreach loop, For Loop, Sequence, tasks, and data flow components|After loading configurations<br /><br /> Before validation<br /><br /> Before execution|  
|Connection managers|After loading configurations<br /><br /> Before validation<br /><br /> Before execution<br /><br /> Before creating a connection instance|  
|Log providers|After loading configurations<br /><br /> Before validation<br /><br /> Before execution<br /><br /> Before opening logs|  
|Foreach enumerators|After loading configurations<br /><br /> Before validation<br /><br /> Before execution<br /><br /> Before each enumeration of the loop|  
  
## Using Property Expressions in the Foreach Loop  
 It is frequently useful to implement a property expression to set the value of the `ConnectionString` property of connection managers that are used inside the Foreach Loop container. After the enumerator maps its current value to a variable on each iteration of the loop, the property expression can use the value of this variable to update the value of the `ConnectionString` property dynamically .  
  
 If you want to use property expressions with the `ConnectionString` property of File, Multiple Files, Flat Files, and Multiple Flat Files connection managers that a Foreach Loop uses, there are some things that you should consider. A package can be configured to run multiple executables concurrently by setting the `MaxConcurrentExecutables` property either to a value greater than 1 or to the value -1. A value of -1 allows the maximum number of concurrently running executables to equal the number of processors plus two. To avoid negative consequences from the parallel execution of executables, the value `MaxConcurrentExecutables` should be set to 1. If `MaxConcurrentExecutables` is not set to 1, then the value of the `ConnectionString` property cannot be guaranteed and results are unpredictable.  
  
 For example, consider a Foreach Loop that enumerates files in a folder, retrieves the file names, and then uses an Execute SQL task to insert each file name into a table. If `MaxConcurrentExecutables` is not set to 1, then write conflicts could occur if two instances of the Execute SQL task attempted to write to the table at the same time.  
  
## Sample Property Expressions  
 The following sample expressions show how to use system variables, operators, functions, and string literals in property expressions.  
  
### Property Expression for the LoggingMode Property of a Package  
 The following property expression can be used to set the LoggingMode property of a package. The expression uses the DAY and GETDATE functions to get an integer that represents the day datepart of a date. If the day is the 1st or 15th, logging is enabled; otherwise, logging is disabled. The value 1 is the integer equivalent of the LoggingMode enumerator member `Enabled`, and the value 2 is the integer equivalent of the member `Disabled`. You must use the numeric value instead of the enumerator member name in the expression.  
  
 `DAY((DT_DBTIMESTAMP)GETDATE())==1||DAY((DT_DBTIMESTAMP)GETDATE())==15?1:2`  
  
### Property Expression for the Subject of an E-mail Message  
 The following property expression can be used to set the Subject property of a Send Mail task and provide a useful e-mail subject. The expression uses a combination of string literals, system variables, the concatenation (+) and cast operators, and the DATEDIFF and GETDATE functions. The system variables are the `PackageName` and `StartTime` variables.  
  
 `"PExpression-->Package: (" + @[System::PackageName] + ") Started:"+  (DT_WSTR, 30) @[System::StartTime] + " Duration:"  +  (DT_WSTR,10) (DATEDIFF( "ss", @[System::StartTime] , GETDATE()  )) + " seconds"`  
  
 If the package name is EmailRowCountPP, was run on 3/4/2005, and the duration of the run was 9 seconds, the expression evaluates to the following string.  
  
 PExpression-->Package: (EmailRowCountPP) Started:3/4/2005 11:06:18 AM Duration:9 seconds.  
  
### Property Expression for the Message of an E-mail Message  
 The following property expression can be used to set the MessageSource property of a Send Mail task. The expression uses a combination of string literals, user-defined variables, and the concatenation (+) operator. The user-defined variables are named `nasdaqrawrows`, `nyserawrows`, and `amexrawrows`. The string "\n" indicates a carriage return.  
  
 `"Rows Processed: "  +   "\n" +"   NASDAQ: "  +   (dt_wstr,9)@[nasdaqrawrows]   + "\n" + "   NYSE: "  +  (dt_wstr,9)@[nyserawrows]  + "\n" + "   Amex: "  +  (dt_wstr,9)@[amexrawrows]`  
  
 If `nasdaqrawrows` is 7058, `nyserawrows` is 3528, and `amexrawrows` is 1102, the expression evaluates to the following string.  
  
 Rows Processed:  
  
 NASDAQ: 7058  
  
 NYSE: 3528  
  
 AMEX: 1102  
  
### Property Expression for the Executable Property of an Execute Process Task  
 The following property expression can be used to set the Executable property of an Execute Process task. The expression uses a combination of string literals, operators, and functions. The expression uses the DATEPART and GETDATE functions and the conditional operator.  
  
 `DATEPART("weekday", GETDATE()) ==2?"notepad.exe":"mspaint.exe"`  
  
 If it is the second day in the week, the Execute Process task runs notepad.exe, otherwise the task runs mspaint.exe.  
  
### Property Expression for the ConnectionString Property of a Flat File Connection Manager  
 The following property expression can be used to set the ConnectionString property of a Flat File connection manager. The expression uses a single user-defined variable, `myfilenamefull`, which contains the path to a text file.  
  
 `@[User::myfilenamefull]`  
  
> [!NOTE]  
>  Property expressions for connection managers can be accessed only by using the Properties window. To view the properties for a connection manager, you must select the connection manager in the **Connection Managers** area of [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer when the Properties window is open, or right-click the connection manager and select **Properties**.  
  
### Property Expression for the ConfigString Property of a Text File Log Provider  
 The following property expression can be used to set the ConfigString property of a Text File log provider. The expression uses a single user-defined variable, `varConfigString`, which contains the name of the File connection manager to use. The File connection manager specifies the path of the text file to which log entries are written.  
  
 `@[User::varConfigString]`  
  
> [!NOTE]  
>  Property expressions for log providers can be accessed only by using the Properties window. To view the properties of a log provider, you must select the log provider on the **Package Explorer** tab of [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer when the Properties window is open, or right-click the log provider in Package Explorer and click **Properties**.  
  
## External Resources  
  
-   [Expression and Configuration Highlighter (CodePlex Project)](https://go.microsoft.com/fwlink/?LinkId=146625)  
  
-   Technical article, [SSIS Expression Examples](https://go.microsoft.com/fwlink/?LinkId=220761), on social.technet.microsoft.com  
  
## See Also  
 [Use Variables in Packages](../use-variables-in-packages.md)  
  
  
