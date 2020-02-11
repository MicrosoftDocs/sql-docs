---
title: "Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.scripttask.f1"
helpviewer_keywords: 
  - "scripts [Integration Services], tasks"
  - "Script task [Integration Services], about Script task"
  - "Script task [Integration Services]"
ms.assetid: f6cce7df-4bd6-4b75-9f89-6c37b4bb5558
author: janinezhang
ms.author: janinez
manager: craigg
---
# Script Task
  The Script task provides code to perform functions that are not available in the built-in tasks and transformations that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides. The Script task can also combine functions in one script instead of using multiple tasks and transformations. You use the Script task for work that must be done once in a package (or once per enumerated object), instead than once per data row.  
  
 You can use the Script task for the following purposes:  
  
-   Access data by using other technologies that are not supported by built-in connection types. For example, a script can use Active Directory Service Interfaces (ADSI) to access and extract user names from Active Directory.  
  
-   Create a package-specific performance counter. For example, a script can create a performance counter that is updated while a complex or poorly performing task runs.  
  
-   Identify whether specified files are empty or how many rows they contain, and then based on that information affect the control flow in a package. For example, if a file contains zero rows, the value of a variable set to 0, and a precedence constraint that evaluates the value prevents a File System task from copying the file.  
  
 If you have to use the script to do the same work for each row of data in a set, you should use the Script component instead of the Script task. For example, if you want to assess the reasonableness of a postage amount and skip data rows that have very high or low amounts, you would use a Script component. For more information, see [Script Component](../data-flow/transformations/script-component.md).  
  
 If more than one package uses a script, consider writing a custom task instead of using the Script task. For more information, see [Developing a Custom Task](../extending-packages-custom-objects/task/developing-a-custom-task.md).  
  
 After you decide that the Script task is the appropriate choice for your package, you have to both develop the script that the task uses and configure the task itself.  
  
## Writing and Running the Script that the Task Uses  
 The Script task uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) as the environment in which you write the scripts and the engine that runs those scripts.  
  
 VSTA provides all the standard features of the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] environment, such as the color-coded [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] editor, IntelliSense, and **Object Explorer**. VSTA also uses the same debugger that other [!INCLUDE[msCoName](../../includes/msconame-md.md)] development tools use. Breakpoints in the script work seamlessly with breakpoints on [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tasks and containers. VSTA supports both the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C# programming languages.  
  
 To run a script, you must have VSTA installed on the computer where the package runs. When the package runs, the task loads the script engine and runs the script. You can access external .NET assemblies in scripts by adding references to the assemblies in the project.  
  
> [!NOTE]  
>  Unlike earlier versions where you could indicate whether the scripts were precompiled, all scripts are precompiled in [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] and later versions. When a script is precompiled, the language engine is not loaded at run time and the package runs more quickly. However, precompiled binary files consume significant disk space.  
  
## Configuring the Script Task  
 You can configure the Script task in the following ways:  
  
-   Provide the custom script that the task runs.  
  
-   Specify the method in the VSTA project that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] runtime calls as the entry point into the Script task code.  
  
-   Specify the script language.  
  
-   Optionally, provide lists of read-only and read/write variables for use in the script.  
  
 You can set these properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
### Configuring the Script Task in the Designer  
 The following table describes the `ScriptTaskLogEntry` event that can be logged for Script task. The `ScriptTaskLogEntry` event is selected for logging on the **Details** tab of the **Configure SSIS Logs** dialog box. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`ScriptTaskLogEntry`|Reports the results of implementing logging in the script. The task writes a log entry for each call to the `Log` method of the `Dts` object. The task writes these entries when the code is run. For more information, see [Logging in the Script Task](../extending-packages-scripting/task/logging-in-the-script-task.md).|  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see the following topics:  
  
-   [Script Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Script Task Editor &#40;Script Page&#41;](../script-task-editor-script-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
### Configuring the Script Task Programmatically  
 For more information about programmatically setting these properties, see the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptTask>  
  
## Related Content  
  
-   Technical article, [How to send email with delivery notification in C#](https://go.microsoft.com/fwlink/?LinkId=237625), on shareourideas.com  
  
  
