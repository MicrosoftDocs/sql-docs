---
title: "Connecting Tasks Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services 
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "tasks [Integration Services], precedence constraints"
  - "precedence constraints [Integration Services], connecting tasks"
  - "constraints [Integration Services]"
ms.assetid: 23668e88-cef4-4009-a9cf-38e607eab7a2
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Connecting Tasks Programmatically
  A precedence constraint, represented in the object model by the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint> class, establishes the order in which <xref:Microsoft.SqlServer.Dts.Runtime.Executable> objects run in a package. The precedence constraint allows the execution of the containers and tasks in a package to be dependent on the result of the execution of a previous task or container. Precedence constraints are established between pairs of <xref:Microsoft.SqlServer.Dts.Runtime.Executable> objects by calling the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraints.Add%2A> method of the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraints> collection on the container object. After you create a constraint between two executable objects, you set the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.Value%2A> property to establish the criteria for executing the second executable defined in the constraint.  
  
 You can use both a constraint and an expression in a single precedence constraint, depending on the value that you specify for the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.EvalOp%2A> property, as described in the following table:  
  
|Value of the EvalOp property|Description|  
|----------------------------------|-----------------|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DTSPrecedenceEvalOp.Constraint>|Specifies that the execution outcome determines whether the constrained container or task runs. Set the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.Value%2A> property of the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint> to the desired value from the <xref:Microsoft.SqlServer.Dts.Runtime.DTSExecResult> enumeration.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DTSPrecedenceEvalOp.Expression>|Specifies that the value of an expression determines whether the constrained container or task runs. Set the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.Expression%2A> property of the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint>.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DTSPrecedenceEvalOp.ExpressionAndConstraint>|Specifies that the constraint outcome must occur and the expression must evaluate for the constrained container or task to run. Set both the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.Value%2A> and the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.Expression%2A> properties of the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint>, and set its <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.LogicalAnd%2A> property to `true`.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DTSPrecedenceEvalOp.ExpressionOrConstraint>|Specifies that either the constraint outcome must occur, or the expression must evaluate, for the constrained container or task to run. Set both the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.Value%2A> and the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.Expression%2A> properties of the <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint>, and set its <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint.LogicalAnd%2A> property to `false`.|  
  
 The following code sample demonstrates adding two tasks to a package. A <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint> is created between them that prevents the second task from running until the first task finishes.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Package p = new Package();  
  
      // Add a File System task.  
      Executable eFileTask1 = p.Executables.Add("STOCK:FileSystemTask");  
      TaskHost thFileHost1 = eFileTask1 as TaskHost;  
  
      // Add a second File System task.  
      Executable eFileTask2 = p.Executables.Add("STOCK:FileSystemTask");  
      TaskHost thFileHost2 = eFileTask2 as TaskHost;  
  
      // Put a precedence constraint between the tasks.  
      // Set the constraint to specify that the second File System task cannot run  
      // until the first File System task finishes.  
      PrecedenceConstraint pcFileTasks =   
        p.PrecedenceConstraints.Add((Executable)thFileHost1, (Executable)thFileHost2);  
      pcFileTasks.Value = DTSExecResult.Completion;  
    }  
  }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Module Module1  
  
  Sub Main()  
  
    Dim p As Package = New Package()  
    ' Add a File System task.  
    Dim eFileTask1 As Executable = p.Executables.Add("STOCK:FileSystemTask")  
    Dim thFileHost1 As TaskHost = CType(eFileTask1, TaskHost)  
  
    ' Add a second File System task.  
    Dim eFileTask2 As Executable = p.Executables.Add("STOCK:FileSystemTask")  
    Dim thFileHost2 As TaskHost = CType(eFileTask2, TaskHost)  
  
    ' Put a precedence constraint between the tasks.  
    ' Set the constraint to specify that the second File System task cannot run  
    ' until the first File System task finishes.  
    Dim pcFileTasks As PrecedenceConstraint = _  
      p.PrecedenceConstraints.Add(CType(thFileHost1, Executable), CType(thFileHost2, Executable))  
    pcFileTasks.Value = DTSExecResult.Completion  
  
  End Sub  
  
End Module  
```  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Adding the Data Flow Task Programmatically](../building-packages-programmatically/adding-the-data-flow-task-programmatically.md)  
  
  
