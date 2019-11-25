---
title: "Execute Process Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.executeprocesstask.f1"
helpviewer_keywords: 
  - "Execute Process task [Integration Services]"
ms.assetid: aca5a0b5-34a9-45bc-a234-8e63ea51a1ee
author: janinezhang
ms.author: janinez
manager: craigg
---
# Execute Process Task
  The Execute Process task runs an application or batch file as part of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package workflow. Although you can use the Execute Process task to open any standard application, such as [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] or [!INCLUDE[ofprword](../../includes/ofprword-md.md)], you typically use it to run business applications or batch files that work against a data source. For example, you can use the Execute Process task to expand a compressed text file. Then the package can use the text file as a data source for the data flow in the package. As another example, you can use the Execute Process task to run a custom [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] application that generates a daily sales report. Then you can attach the report to a Send Mail task and forward the report to a distribution list.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes other tasks that perform workflow operations such as executing packages. For more information, see [Execute Package Task](execute-package-task.md)  
  
## Custom Log Entries Available on the Execute Process Task  
 The following table lists the custom log entries for the Execute Process task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`ExecuteProcessExecutingProcess`|Provides information about the process that the task is configured to run.<br /><br /> Two log entries are written. One contains information about the name and location of the executable that the task runs, and the other entry records the exit from the executable.|  
|`ExecuteProcessVariableRouting`|Provides information about which variables are routed to the input and outputs of the executable. Log entries are written for stdin (the input), stdout (the output), and stderr (the error output).|  
  
## Configuration of the Execute Process Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Execute Process Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Execute Process Task Editor &#40;Process Page&#41;](../execute-process-task-editor-process-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
### Property Settings  
 When the Execute Process task runs a custom application, the task provides input to the application through one or both of the following methods:  
  
-   A variable that you specify in the **StandardInputVariable** property setting. For more information about variables, see [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md) and [Use Variables in Packages](../use-variables-in-packages.md).  
  
-   An argument that you specify in the **Arguments** property setting. (For example, if the task opens a document in Word, the argument can name the .doc file.)  
  
 To pass multiple arguments to a custom application in one Execute Process task, use spaces to delimit the arguments. An argument cannot include a space; otherwise, the task will not run. You can use an expression to pass a variable value as the argument. In the following example, the expression passes two variable values as arguments, and uses a space to delimit the arguments:  
  
 `@variable1 + " " + @variable2`  
  
 You can use an expression to set various Execute Process task properties.  
  
 When you use the **StandardInputVariable** property to configure the Execute Process task to provide input, call the `Console.ReadLine` method from the application to read the input. For more information, see [Console.ReadLine Method](https://go.microsoft.com/fwlink/?LinkId=129201)the topic, , in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Class Library.  
  
 When you use the **Arguments** property to configure the Execute Process task to provide input, do one of the following steps to obtain the arguments:  
  
-   If you use Microsoft Visual Basic to write the application, set the `My.Application.CommandLineArgs` property. The following example sets the `My.Application.CommandLineArgs` property is to retrieve two arguments:  
  
    ```  
    Dim variable1 As String = My.Application.CommandLineArgs.Item(0)  
    Dim variable2 As String = My.Application.CommandLineArgs.Item(1)   
    ```  
  
     For more information, see the topic, [My.Application.CommandLineArgs Property](https://go.microsoft.com/fwlink/?LinkId=129200), in the [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] reference.  
  
-   If you use Microsoft Visual C# to write the applicate, use the `Main` method.  
  
     For more information, see the topic, [Command-Line Arguments (C# Programming Guide)](https://go.microsoft.com/fwlink/?LinkId=129406), in the C# Programming Guide.  
  
 The Execute Process task also includes the **StandardOutputVariable** and **StandardErrorVariable** properties for specifying the variables that consume the standard output and error output of the application, respectively.  
  
 Additionally, you can configure the Execute Process task to specify a working directory, a time-out period, or a value to indicate that the executable ran successfully. The task can also be configured to fail if the return code of the executable does not match the value that indicates success, or if the executable is not found at the specified location.  
  
## Programmatic Configuration of the Execute Process Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.ExecuteProcess.ExecuteProcess>  
  
## See Also  
 [Integration Services Tasks](integration-services-tasks.md)   
 [Control Flow](control-flow.md)  
  
  
