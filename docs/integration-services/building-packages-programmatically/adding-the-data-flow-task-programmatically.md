---
title: "Adding the Data Flow Task Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services 
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "adding Data Flow task"
  - "SSIS data flow"
  - "data flow task [Integration Services], adding"
  - "MainPipe object"
ms.assetid: 0ca03712-a82e-4aa7-949b-f869a8936ddf
author: janinezhang
ms.author: janinez
manager: craigg
---
# Adding the Data Flow Task Programmatically
  [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] includes a task called the Data Flow task, which is represented by the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper> namespace in the object model. The Data Flow task is a specialized, high-performance task, dedicated to transforming and moving data during package execution. Like other tasks, the Data Flow task is wrapped by the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> object, and from the perspective of the run-time engine, this task is just another task in the package. However, the data flow contains additional objects called data flow components. These components are the components that make data move from a source to a destination, sometimes through a transformation. The components define both the direction of movement and how data is transformed. Configuring the Data Flow task involves adding components to the task, and then connecting them to establish the flow of data and achieve the intended transformation.  
  
 There are three types of components within a Data Flow task: **Data Flow Sources**, **Data Flow Transformations**, and **Data Flow Destinations**, shown in that order within the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer toolbox. These types are also referred to more simply as sources, transformations, or destinations. As implied by the names, data flows from a source to a transformation, and then to a destination. This is a simplistic description of the data flow to illustrate the concept, but the Data Flow task is flexible and powerful enough to handle multiple sources, and to connect together many transformations that send output to multiple destinations.  
  
 The Data Flow task is added to a package the same way other tasks are added. After the task has been added, it is configured by adding components to the data flow task, and configuring and connecting components in the task.  
  
## Sample  
 The following code sample shows how to add a Data Flow task to a package. This example requires a reference to the assemblies Microsoft.SqlServer.PipelineHost, Microsoft.SqlServer.DTSPipelineWrap, and Microsoft.SqlServer.ManagedDTS.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
using Microsoft.SqlServer.Dts.Pipeline;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Package p = new Package();  
      Executable e = p.Executables.Add("STOCK:PipelineTask");  
      TaskHost thMainPipe = e as TaskHost;  
      MainPipe dataFlowTask = thMainPipe.InnerObject as MainPipe;   
    }  
  }  
}  
```  
  
```vb  
Imports System.IO  
Imports Microsoft.SqlServer.Dts.Runtime  
Imports Microsoft.SqlServer.Dts.Pipeline  
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper  
  
Module Module1  
  
  Sub Main()  
  
    Dim p As Package = New Package()  
    Dim e As Executable = p.Executables.Add("STOCK:PipelineTask")  
    Dim thMainPipe As TaskHost = CType(e, TaskHost)  
    Dim dataFlowTask As MainPipe = CType(thMainPipe.InnerObject, MainPipe)  
  
  End Sub  
  
End Module  
```  
  
## External Resources  
 Blog entry, [EzAPI - Updated for SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=243223), on blogs.msdn.com.  
  
## See Also  
 [Discovering Data Flow Components Programmatically](../../integration-services/building-packages-programmatically/discovering-data-flow-components-programmatically.md)  
  
  
