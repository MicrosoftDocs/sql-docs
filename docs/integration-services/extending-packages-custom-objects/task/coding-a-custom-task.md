---
title: "Coding a Custom Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "Validate method"
  - "custom tasks [Integration Services], validating"
  - "validation [Integration Services], design-time tasks"
  - "SSIS custom tasks, validating"
ms.assetid: dc224f4f-b339-4eb6-a008-1b4fe0ea4fd2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Coding a Custom Task

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  After you have created a class that inherits from the <xref:Microsoft.SqlServer.Dts.Runtime.Task> base class, and applied the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute to the class, you must override the implementation of the properties and methods of the base class to provide your custom functionality.  
  
## Configuring the Task  
  
### Validating the Task  
 When you are designing an [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] package, you can use validation to verify settings on each task, so that you can catch incorrect or inappropriate settings as soon as they are set, instead of finding all errors at run-time only. The purpose of validation is to determine whether the task contains invalid settings or connections that will prevent it from running successfully. This makes sure that the package contains tasks that have a good chance of running on their first run.  
  
 You can implement validation by using the **Validate** method in custom code. The run-time engine validates a task by calling the **Validate** method on the task. It is the responsibility of the task developer to define the criteria that give you a successful or failed task validation, and to notify the run-time engine of the result of this evaluation.  
  
#### Task Abstract Base Class  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Task> abstract base class provides the **Validate** method that each task overrides to define its validation criteria. The [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer automatically calls the **Validate** method multiple times during package design, and provides visual cues to the user when warnings or errors occur to help identify problems with the configuration of the task. Tasks provide validation results by returning a value from the <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult> enumeration, and by raising warning and error events. These events contain information that is displayed to the user in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
 Some examples for validation follow:  
  
-   A connection manager validates the specific file name.  
  
-   A connection manager validates that the type of input is the expected type, such as an XML file.  
  
-   A task that expects database input verifies that it cannot receive data from a non-database connection.  
  
-   A task guarantees that none of its properties contradicts other properties set on the same task.  
  
-   A task guarantees that all required resources used by the task at execution time are available.  
  
 Performance is something to consider in determining what is validated and what is not. For example, the input to a task might be a connection over a network that has low bandwidth or heavy traffic. The validation may take several seconds to process if you decide to validate that the resource is available. Another validation may cause a round-trip to a server that is in high demand, and the validation routine might be slow. Although there are many properties and settings that can be validated, not everything should be validated.  
  
-   The code in the **Validate** method is also called by the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> before the task is run, and the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> cancels execution if validation fails.  
  
#### User Interface Considerations during Validation  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Task> includes an <xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents> interface as a parameter to the **Validate** method. The <xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents> interface contains the methods that are called by the task in order to raise events to the run-time engine. The <xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireWarning%2A> and <xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireError%2A> methods are called when a warning or error condition occurs during validation. Both warning methods require the same parameters, which include an error code, source component, description, Help file, and Help context information. The [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer uses this information to display visual cues on the design surface. The visual cues that are provided by the designer include an exclamation icon that appears next to the task on the designer surface. This visual cue signals to the user that the task requires additional configuration before execution can continue.  
  
 The exclamation icon also displays a ToolTip that contains an error message. The error message is provided by the task in the description parameter of the event. Error messages are also displayed in the **Task List** pane of [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], providing the user with a central location for viewing all validation errors.  
  
#### Validation Example  
 The following code example shows a task with a **UserName** property. This property has been specified as required for validation to succeed. If the property is not set, the task posts an error, and returns <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Failure> from the <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult> enumeration. The **Validate** method is wrapped in a try/catch block, and fails validation if an exception occurs.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
public class SampleTask : Task  
{  
  private string userName = "";  
  
  public override DTSExecResult Validate(Connections connections,  
     VariableDispenser variableDispenser, IDTSComponentEvents events,  
     IDTSLogging log)  
  {  
    try  
    {  
      if (this.userName == "")  
      {  
        //   Raise an OnError event.  
        events.FireError(0, "SampleTask", "The UserName property must be configured.", "", 0);  
        //   Fail validation.  
        return DTSExecResult.Failure;  
      }  
      //   Return success.  
      return DTSExecResult.Success;  
    }  
    catch (System.Exception exception)  
    {  
      //   Capture exceptions, post an error, and fail validation.  
      events.FireError(0, "Sampletask", exception.Message, "", 0);  
      return DTSExecResult.Failure;  
    }  
  }  
  public string UserName  
  {  
    get  
    {  
      return this.userName;  
    }  
    set  
    {  
      this.userName = value;  
    }  
  }  
}  
```  
  
```vb  
Imports System  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Public Class SampleTask  
  Inherits Task  
  
  Private _userName As String = ""  
  
  Public Overrides Function Validate(ByVal connections As Connections, _  
     ByVal variableDispenser As VariableDispenser, _  
     ByVal events As IDTSComponentEvents, _  
     ByVal log As IDTSLogging) As DTSExecResult  
  
    Try  
      If Me._userName = "" Then  
        '   Raise an OnError event.  
        events.FireError(0, "SampleTask", "The UserName property must be configured.", "", 0)  
        '   Fail validation.  
        Return DTSExecResult.Failure  
      End If  
      '   Return success.  
      Return DTSExecResult.Success  
    Catch exception As System.Exception  
      '   Capture exceptions, post an error, and fail validation.  
      events.FireError(0, "Sampletask", exception.Message, "", 0)  
      Return DTSExecResult.Failure  
    End Try  
  
  End Function  
  
  Public Property UserName() As String  
    Get  
      Return Me._userName  
    End Get  
    Set(ByVal Value As String)  
      Me._userName = Value  
    End Set  
  End Property  
  
End Class  
```  
  
### Persisting the Task  
 Ordinarily you do not have to implement custom persistence for a task. Custom persistence is required only when the properties of an object use complex data types. For more information, see [Developing Custom Objects for Integration Services](../../../integration-services/extending-packages-custom-objects/developing-custom-objects-for-integration-services.md).  
  
## Executing the Task  
 This section describes how to use the **Execute** method that is inherited and overridden by tasks. This section also explains various ways of providing information about the results of task execution.  
  
### Execute Method  
 Tasks that are contained in a package run when the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] runtime calls their **Execute** method. Tasks implement their core business logic and functionality in this method, and provide the results of execution by posting messages, returning a value from the <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult> enumeration, and overriding the property **get** of the **ExecutionValue** property.  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Task> base class provides a default implementation of the <xref:Microsoft.SqlServer.Dts.Runtime.Task.Execute%2A> method. The custom tasks override this method to define their run-time functionality. The <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> object wraps the task, isolating it from the run-time engine and the other objects in the package. Because of this isolation, the task is unaware of its location in the package with regard to its execution order, and runs only when it is called by the runtime. This architecture prevents problems that can occur when tasks modify the package during execution. The task is provided access to the other objects in the package only through the objects supplied to it as parameters in the <xref:Microsoft.SqlServer.Dts.Runtime.Task.Execute%2A> method. These parameters let tasks raise events, write entries to the event log, access the variables collection, and enlist connections to data sources in transactions, while still maintaining the isolation that is necessary to guarantee the stability and reliability of the package.  
  
 The following table lists the parameters provided to the task in the <xref:Microsoft.SqlServer.Dts.Runtime.Task.Execute%2A> method.  
  
|Parameter|Description|  
|---------------|-----------------|  
|<xref:Microsoft.SqlServer.Dts.Runtime.Connections>|Contains a collection of <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> objects available to the task.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.VariableDispenser>|Contains the variables available to the task. The tasks use variables through the VariableDispenser; the tasks do not use variables directly. The variable dispenser locks and unlocks variables, and prevents deadlocks or overwrites.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents>|Contains the methods called by the task to raise events to the run-time engine.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSLogging>|Contains methods and properties used by the task to write entries to the event log.|  
|Object|Contains the transaction object that the container is a part of, if any. This value is passed as a parameter to the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager.AcquireConnection%2A> method of a <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> object.|  
  
### Providing Execution Feedback  
 Tasks wrap their code in **try/catch** blocks to prevent exceptions from being raised to the run-time engine. This ensures that the package finishes execution and does not stop unexpectedly. However, the run-time engine provides other mechanisms for handling error conditions that can occur during the execution of a task. These include posting error and warning messages, returning a value from the <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult> structure, posting messages, returning the <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult> value, and disclosing information about the results of task execution through the <xref:Microsoft.SqlServer.Dts.Runtime.Task.ExecutionValue%2A> property.  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents> interface contains the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireWarning%2A> and <xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireError%2A> methods, which can be called by the task to post error and warning messages to the run-time engine. Both methods require parameters such as the error code, source component, description, Help file, and help context information. Depending on the configuration of the task, the runtime responds to these messages by raising events and breakpoints, or by writing information to the event log.  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> also provides the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.ExecutionValue%2A> property that can be used to provide additional information about the results of execution. For example, if a task deletes rows from a table as part of its **Execute** method, it might return the number of rows deleted as the value of the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.ExecutionValue%2A> property. In addition, the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> provides the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.ExecValueVariable%2A> property. This property lets the user map the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.ExecutionValue%2A> returned from the task to any variable visible to the task. The specified variable can then be used to establish precedence constraints between tasks.  
  
### Execution Example  
 The following code example demonstrates an implementation of the **Execute** method, and shows an overridden **ExecutionValue** property. The task deletes the file that is specified by the **fileName** property of the task. The task posts a warning if the file does not exist, or if the **fileName** property is an empty string. The task returns a **Boolean** value in the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.ExecutionValue%2A> property to indicate whether the file was deleted.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
public class SampleTask : Task  
{  
  private string fileName = "";  
  private bool fileDeleted = false;  
  
  public override DTSExecResult Execute(Connections cons,  
     VariableDispenser vars, IDTSComponentEvents events,  
     IDTSLogging log, Object txn)  
  {  
    try  
    {  
      if (this.fileName == "")  
      {  
        events.FireWarning(0, "SampleTask", "No file specified.", "", 0);  
        this.fileDeleted = false;  
      }  
      else  
      {  
        if (System.IO.File.Exists(this.fileName))  
        {  
          System.IO.File.Delete(this.fileName);  
          this.fileDeleted = true;  
        }  
        else  
          this.fileDeleted = false;  
      }  
      return DTSExecResult.Success;  
    }  
    catch (System.Exception exception)  
    {  
      //   Capture the exception and post an error.  
      events.FireError(0, "Sampletask", exception.Message, "", 0);  
      return DTSExecResult.Failure;  
    }  
  }  
  public string FileName  
  {  
    get { return this.fileName; }  
    set { this.fileName = value; }  
  }  
  public override object ExecutionValue  
  {  
    get { return this.fileDeleted; }  
  }  
}  
```  
  
```vb  
Imports System  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Public Class SampleTask  
  Inherits Task  
  
  Private _fileName As String = ""  
  Private _fileDeleted As Boolean = False  
  
  Public Overrides Function Execute(ByVal cons As Connections, _  
     ByVal vars As VariableDispenser, ByVal events As IDTSComponentEvents, _  
     ByVal log As IDTSLogging, ByVal txn As Object) As DTSExecResult  
  
    Try  
      If Me._fileName = "" Then  
        events.FireWarning(0, "SampleTask", "No file specified.", "", 0)  
        Me._fileDeleted = False  
      Else  
        If System.IO.File.Exists(Me._fileName) Then  
          System.IO.File.Delete(Me._fileName)  
          Me._fileDeleted = True  
        Else  
          Me._fileDeleted = False  
        End If  
      End If  
      Return DTSExecResult.Success  
    Catch exception As System.Exception  
      '   Capture the exception and post an error.  
      events.FireError(0, "Sampletask", exception.Message, "", 0)  
      Return DTSExecResult.Failure  
    End Try  
  
  End Function  
  
  Public Property FileName() As String  
    Get  
      Return Me._fileName  
    End Get  
    Set(ByVal Value As String)  
      Me._fileName = Value  
    End Set  
  End Property  
  
  Public Overrides ReadOnly Property ExecutionValue() As Object  
    Get  
      Return Me._fileDeleted  
    End Get  
  End Property  
  
End Class  
```  
  
## See Also  
 [Creating a Custom Task](../../../integration-services/extending-packages-custom-objects/task/creating-a-custom-task.md)   
 [Coding a Custom Task](../../../integration-services/extending-packages-custom-objects/task/coding-a-custom-task.md)   
 [Developing a User Interface for a Custom Task](../../../integration-services/extending-packages-custom-objects/task/developing-a-user-interface-for-a-custom-task.md)  
  
  
