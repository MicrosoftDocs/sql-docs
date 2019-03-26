---
title: "Working with Variables Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "System namespace"
  - "scope [Integration Services]"
  - "variables [Integration Services], programmatically"
  - "configuration files [Integration Services]"
  - "Variables collection"
  - "User namespace"
  - "custom variables [Integration Services]"
  - "variables [Integration Services], customizing"
ms.assetid: c4b76a3d-94ca-4a8e-bb45-cb8bd0ea3ec1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Working with Variables Programmatically
  Variables are a way to dynamically set values and control processes in packages, containers, tasks, and event handlers. Variables can also be used by precedence constraints to control the direction of the flow of data to different tasks. Variables have a variety of uses:  
  
-   Update properties of a package at run time.  
  
-   Populate parameter values for Transact-SQL statements at run time.  
  
-   Control the flow of a Foreach loop. For more information, see [Add Enumeration to a Control Flow](../control-flow/control-flow.md).  
  
-   Control a precedence constraint by its use in an expression. A precedence constraint can include variables in the constraint definition. For more information, see [Add Expressions to Precedence Constraints](../control-flow/precedence-constraints.md).  
  
-   Control the conditional repeat of a For Loop container. For more information, see [Add Iteration to a Control Flow](../add-iteration-to-a-control-flow.md).  
  
-   Build expressions that include variable values.  
  
-   You can create custom variables for all container types: packages, **Foreach Loop** containers, **For Loop** containers, **Sequence** containers, TaskHosts, and event handlers. For more information, see [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md) and [Use Variables in Packages](../use-variables-in-packages.md).  
  
## Scope  
 Each container has its own <xref:Microsoft.SqlServer.Dts.Runtime.Variables> collection. When a new variable is created, it is within the scope of its parent container. Because the package container is at the top of the container hierarchy, variables with package scope function like global variables, and are visible to all containers within the package. The collection of variables for the container can also be accessed by the children of the container through the <xref:Microsoft.SqlServer.Dts.Runtime.Variables> collection, by using either the variable name or the variable's index in the collection.  
  
 Because the visibility of a variable is scoped from the top down, variables declared at the package level are visible to all the containers in the package. Therefore, the <xref:Microsoft.SqlServer.Dts.Runtime.Variables> collection on a container includes all the variables that belong to its parent in addition to its own variables  
  
 Conversely, the variables that are contained in a task are limited in scope and visibility, and are only visible to the task.  
  
 If a package runs other packages, the variables defined in the scope of the calling package are available to the called package. The only exception occurs when a same-named variable exists in the called package. When this collision occurs, the variable value in the called package overrides the value from the calling package. Variables defined in the scope of the called package are never available back to the calling package.  
  
 The following code example programmatically creates a variable, `myCustomVar`, at the package scope, and then iterates through all the variables visible to the package, printing their name, data type, and value.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Application app = new Application();  
      // Load a sample package that contains a variable that sets the file name.  
      Package pkg = app.LoadPackage(  
        @"C:\Program Files\Microsoft SQL Server\100\Samples\Integration Services" +  
        @"\Package Samples\CaptureDataLineage Sample\CaptureDataLineage\CaptureDataLineage.dtsx",  
        null);  
      Variables pkgVars = pkg.Variables;  
      Variable myVar = pkg.Variables.Add("myCustomVar", false, "User", "3");  
      foreach (Variable pkgVar in pkgVars)  
      {  
        Console.WriteLine("Variable: {0}, {1}, {2}", pkgVar.Name,  
          pkgVar.DataType, pkgVar.Value.ToString());  
      }  
      Console.Read();  
    }  
  }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Module Module1  
  
  Sub Main()  
  
    Dim app As Application = New Application()  
    ' Load a sample package that contains a variable that sets the file name.  
    Dim pkg As Package = app.LoadPackage( _  
      "C:\Program Files\Microsoft SQL Server\100\Samples\Integration Services" & _  
      "\Package Samples\CaptureDataLineage Sample\CaptureDataLineage\CaptureDataLineage.dtsx", _  
      Nothing)  
    Dim pkgVars As Variables = pkg.Variables  
    Dim myVar As Variable = pkg.Variables.Add("myCustomVar", False, "User", "3")  
    Dim pkgVar As Variable  
    For Each pkgVar In pkgVars  
      Console.WriteLine("Variable: {0}, {1}, {2}", pkgVar.Name, _  
        pkgVar.DataType, pkgVar.Value.ToString())  
    Next  
    Console.Read()  
  
  End Sub  
  
End Module  
```  
  
 **Sample Output:**  
  
 `Variable: CancelEvent, Int32, 0`  
  
 `Variable: CreationDate, DateTime, 4/18/2003 11:57:00 AM`  
  
 `Variable: CreatorComputerName, String,`  
  
 `Variable: CreatorName, String,`  
  
 `Variable: ExecutionInstanceGUID, String, {237AB5A4-7E59-4FC9-8D61-E8F20363DF25}`  
  
 `Variable: FileName, String, Junk`  
  
 `Variable: InteractiveMode, Boolean, False`  
  
 `Variable: LocaleID, Int32, 1033`  
  
 `Variable: MachineName, String, MYCOMPUTERNAME`  
  
 `Variable: myCustomVar, String, 3`  
  
 `Variable: OfflineMode, Boolean, False`  
  
 `Variable: PackageID, String, {F0D2E396-A6A5-42AE-9467-04CE946A810C}`  
  
 `Variable: PackageName, String, DTSPackage1`  
  
 `Variable: StartTime, DateTime, 1/28/2005 7:55:39 AM`  
  
 `Variable: UserName, String, <domain>\<userid>`  
  
 `Variable: VersionBuild, Int32, 198`  
  
 `Variable: VersionComments, String,`  
  
 `Variable: VersionGUID, String, {90E105B4-B4AF-4263-9CBD-C2050C2D6148}`  
  
 `Variable: VersionMajor, Int32, 1`  
  
 `Variable: VersionMinor, Int32, 0`  
  
 Notice that all the variables scoped in the **System** namespace are available to the package. For more information, see [System Variables](../system-variables.md).  
  
## Namespaces  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] ([!INCLUDE[ssIS](../../includes/ssis-md.md)]) provides two default namespaces where variables reside; **User** and **System** namespaces. By default, any custom variable created by the developer is added to the **User** namespace. System variables reside in the **System** namespace. You can create additional namespaces other than the **User** namespace to hold custom variables, and you can change the name of the **User** namespace, but you cannot add or modify variables in the **System** namespace, or assign system variables to a different namespace.  
  
 The system variables that are available differ depending on the container type. For a list of the system variables available to packages, containers, tasks, and event handlers, see [System Variables](../system-variables.md).  
  
## Value  
 The value of a custom variable can be a literal or an expression:  
  
-   If you want the variable to contain a literal value, set the value of its <xref:Microsoft.SqlServer.Dts.Runtime.Variable.Value%2A> property.  
  
-   If you want the variable to contain an expression, so that you can use the results of the expression as its value, set the <xref:Microsoft.SqlServer.Dts.Runtime.Variable.EvaluateAsExpression%2A> property of the variable to `true`, and provide an expression in the <xref:Microsoft.SqlServer.Dts.Runtime.Variable.Expression%2A> property. At run time, the expression is evaluated, and the result of the expression is used as the value of the variable. For example, if the expression property of a variable is `"100 * 2""100 * 2"`, the variable evaluates to a value of 200.  
  
 For a variable, you cannot explicitly set the value of its <xref:Microsoft.SqlServer.Dts.Runtime.Variable.DataType%2A>. The <xref:Microsoft.SqlServer.Dts.Runtime.Variable.DataType%2A> value is inferred from the initial value assigned to the variable, and cannot be changed afterward. For more information about variable data types, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
 The following code example creates a new variable, sets <xref:Microsoft.SqlServer.Dts.Runtime.Variable.EvaluateAsExpression%2A> to `true`, assigns the expression `"100 * 2"` to the expression property of the variable, and then outputs the value of the variable.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Package pkg = new Package();  
      Variable v100 = pkg.Variables.Add("myVar", false, "", 1);  
      v100.EvaluateAsExpression = true;  
      v100.Expression = "100 * 2";  
      Console.WriteLine("Expression for myVar: {0}",   
        v100.Properties["Expression"].GetValue(v100));  
      Console.WriteLine("Value of myVar: {0}", v100.Value.ToString());  
      Console.Read();  
    }  
  }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Module Module1  
  
  Sub Main()  
  
    Dim pkg As Package = New Package  
    Dim v100 As Variable = pkg.Variables.Add("myVar", False, "", 1)  
    v100.EvaluateAsExpression = True  
    v100.Expression = "100 * 2"  
    Console.WriteLine("Expression for myVar: {0}", _  
      v100.Properties("Expression").GetValue(v100))  
    Console.WriteLine("Value of myVar: {0}", v100.Value.ToString)  
    Console.Read()  
  
  End Sub  
  
End Module  
```  
  
 **Sample Output:**  
  
 `Expression for myVar: 100 * 2`  
  
 `Value of myVar: 200`  
  
 The expression must be a valid expression that uses the [!INCLUDE[ssIS](../../includes/ssis-md.md)] expression syntax. Literals are permitted in variable expressions, in addition to the operators and functions that the expression syntax provides, but expressions cannot reference other variables or columns. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../expressions/integration-services-ssis-expressions.md).  
  
## Configuration Files  
 If a configuration file includes a custom variable, the variable can be updated at run time. What this means is that when the package runs, the value of the variable originally in the package is replaced with a new value from the configuration file. This replacement technique is useful when a package is deployed to multiple servers that require different variable values. For example, a variable can specify the number of times a **Foreach Loop** container repeats its workflow, or list the recipients that an event handler sends e-mail to when an error is raised, or change the number of errors that can occur before the package fails. These variables are dynamically provided in configuration files for each environment. Therefore, only variables that are read/write are allowed in configuration files. For more information, see [Create Package Configurations](../create-package-configurations.md).  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md)   
 [Use Variables in Packages](../use-variables-in-packages.md)  
  
  
