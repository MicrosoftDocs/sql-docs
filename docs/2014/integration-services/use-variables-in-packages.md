---
title: "Use Variables in Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined variables [Integration Services]"
  - "variables [Integration Services], use scenarios"
  - "system variables [Integration Services]"
ms.assetid: 7742e92d-46c5-4cc4-b9a3-45b688ddb787
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Use Variables in Packages
  Variables are a useful and flexible addition to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages; they can provide communication among objects in the package, and between parent and child packages. Variables can also be used in expressions and scripts.  
  
## User-Defined Variables and System Variables  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides system variables and supports user-defined variables. When you create a new package, add a container or a task to a package, or create an event handler, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes a set of system variables for the container. System variables contain useful information about a package, container, task, or event handler. For example, at run time the **MachineName** system variable contains the name of the computer on which the package is running and **StartTime** the time the package started to run. System variables are read-only. For more information, see [System Variables](system-variables.md).  
  
 You can create user-defined variables and then use them in packages. User-defined variables can be used in many ways in [!INCLUDE[ssIS](../includes/ssis-md.md)]: in scripts; in the expressions used by precedence constraints, the For Loop container, the Derived Column transformation, and the Conditional Split transformation; and in the property expressions that update property values.  
  
 For example, you can use a user-defined variable in the evaluation condition for the For Loop container. You can also map the enumerator collection value in a Foreach Loop container to a variable, and if an Execute SQL task uses a parameterized SQL statement, you can map the parameters for the statement to variables. For more information, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md).  
  
## Variables Usage Scenarios  
 Variables are used in many different ways in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. You will probably find that package development does not progress far before you have to add a user-defined variable to your package to implement the flexibility and manageability your solution requires. Depending on the scenario, system variables are also commonly used.  
  
 **Property Expressions** Use variables to provide values in the property expressions that set the properties of packages and package objects. For example, the expression, `SELECT * FROM @varTableName` includes the variable `varTableName` that updates the SQL statement that an Execute SQL task runs. The expression, `DATEPART("d", GETDATE()) == 1? @[User::varPackageFirst]:@[User::varPackageOther]`", updates the package that the Execute Package task runs, by running the package specified in the `varPackageFirst` variable on the first day of the month and running the package specified in the `varPackageOther` variable on other days. For more information, see [Use Property Expressions in Packages](expressions/use-property-expressions-in-packages.md).  
  
 **Data Flow Expressions** Use variables to provide values in the expressions that the Derived Column and Conditional Split transformations use to populate columns, or to direct data rows to different transformation outputs. For example, the expression, `@varSalutation + LastName`, concatenates the value in the `VarSalutation` variable and the `LastName` column. The expression, `Income < @HighIncome`, directs data rows in which the value of the `Income` column is less than the value in the `HighIncome` variable to an output. For more information, see [Derived Column Transformation](data-flow/transformations/derived-column-transformation.md), [Conditional Split Transformation](data-flow/transformations/conditional-split-transformation.md), and [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md).  
  
 **Precedence Constraint Expressions** Provide values to use in precedence constraints to determine whether a constrained executable runs. The expressions can be used either together with an execution outcome (success, failure, completion), or instead of an execution outcome. For example, if the expression, `@varMax > @varMin`, evaluates to `true`, the executable runs. For more information, see [Add Expressions to Precedence Constraints](control-flow/precedence-constraints.md).  
  
 **Parameters and Return Codes** Provide values to input parameters, or store the values of output parameters and return codes. You do this by mapping the variables to parameters and return values. For example, if you set the variable `varProductId` to 23 and run the SQL statement, `SELECT * from Production.Product WHERE ProductID = ?`, the query retrieves the product with a `ProductID` of 23. For more information, see [Execute SQL Task](control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md).  
  
 **For Loop Expressions** Provide values to use in the initialization, evaluation, and assignment expressions of the For Loop. For example, if the variable `varCount` is 2 and `varMaxCount` is 10, the initialization expression is `@varCount`, the evaluation expression is  `@varCount < @varMaxCount`, and the assignment expression is `@varCount =@varCount +1`, then the loop repeats 8 times. For more information, see [For Loop Container](control-flow/for-loop-container.md).  
  
 **Parent Package Variable Configurations** Pass values from parent packages to child packages. Child packages can access variables in the parent package by using parent package variable configurations. For example, if the child package must use the same date as the parent package, the child package can define a parent package variable configuration that specifies a variable set by the GETDATE function in the parent package. For more information, see [Execute Package Task](control-flow/execute-package-task.md) and [Package Configurations](../../2014/integration-services/package-configurations.md).  
  
 **Script Task and Script Component** Provide a list of read-only and read/write variable to the Script task or Script component, update the read/write variables within the script, and then use the updated values in or outside the script. For example, in the code, `numberOfCars = CType(Dts.Variables("NumberOfCars").Value, Integer)`, the script variable `numberOfCars` is updated by the value in the variable, `NumberOfCars`. For more information, see [Using Variables in the Script Task](control-flow/script-task.md).  
  
## Configurations and Variables  
 To dynamically update variables, you can create configurations for the variables, deploy the configurations with the package, and then update the variable values in the configuration file when you deploy the packages. At run time, the package uses the updated variable values. For more information, see [Create Package Configurations](../../2014/integration-services/create-package-configurations.md).  
  
### To add, modify, and delete user-defined variables  
  
-   [Add, Delete, Change Scope of User-Defined Variable in a Package](../../2014/integration-services/add-delete-change-scope-of-user-defined-variable-in-a-package.md)  
  
-   [Set the Properties of a User-Defined Variable](../../2014/integration-services/set-the-properties-of-a-user-defined-variable.md)  
  
  
