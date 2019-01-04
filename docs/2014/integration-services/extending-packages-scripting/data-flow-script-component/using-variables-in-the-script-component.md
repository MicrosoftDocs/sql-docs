---
title: "Using Variables in the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Script component [Integration Services], using variables"
ms.assetid: 92d1881a-1ef1-43ae-b1ca-48d0536bdbc2
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Using Variables in the Script Component
  Variables store values that a package and its containers, tasks, and event handlers can use at run time. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services-ssis-variables.md).  
  
 You can make existing variables available for read-only or read/write access by your custom script by entering comma-delimited lists of variables in the `ReadOnlyVariables` and `ReadWriteVariables` fields on the **Script** page of the **Script Transformation Editor**. Keep in mind that variable names are case-sensitive. Use the `Value` property to read from and write to individual variables. The Script component handles any required locking behind the scenes as your script manipulates the variables at run time.  
  
> [!IMPORTANT]  
>  The collection of `ReadWriteVariables` is only available in the `PostExecute` method to maximize performance and minimize the risk of locking conflicts. Therefore you cannot directly increment the value of a package variable as you process each row of data. Increment the value of a local variable instead, and set the value of the package variable to the value of the local variable in the `PostExecute` method after all data has been processed. You can also use the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.VariableDispenser%2A> property to work around this limitation, as described later in this topic. However, writing directly to a package variable as each row is processed will negatively impact performance and increase the risk of locking conflicts.  
  
 For more information about the **Script** page of the **Script Transformation Editor**, see [Configuring the Script Component in the Script Component Editor](configuring-the-script-component-in-the-script-component-editor.md) and [Script Transformation Editor &#40;Script Page&#41;](../../script-transformation-editor-script-page.md).  
  
 The Script component creates a `Variables` collection class in the `ComponentWrapper` project item with a strongly-typed accessor property for the value of each preconfigured variable where the property has the same name as the variable itself. This collection is exposed through the `Variables` property of the `ScriptMain` class. The accessor property provides read-only or read/write permission to the value of the variable as appropriate. For example, if you have added an integer variable named `MyIntegerVariable` to the `ReadOnlyVariables` list, you can retrieve its value in your script by using the following code:  
  
 `Dim myIntegerVariableValue As Integer = Me.Variables.MyIntegerVariable`  
  
 You can also use the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.VariableDispenser%2A> property, accessed by calling `Me.VariableDispenser`, to work with variables in the Script component. In this case you are not using the typed and named accessor properties for variables, but accessing the variables directly. When using the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.VariableDispenser%2A>, you must handle both the locking semantics and the casting of data types for variable values in your own code. You have to use the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.VariableDispenser%2A> property instead of the named and typed accessor properties if you want to work with a variable that is not available at design time but is created programmatically at run time.  
  
![Integration Services icon (small)](../../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Variables](../../integration-services-ssis-variables.md)   
 [Use Variables in Packages](../../use-variables-in-packages.md)  
  
  
