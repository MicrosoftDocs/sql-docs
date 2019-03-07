---
title: "Discovering Data Flow Components Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services 
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "PipelineComponentInfos collection"
  - "data flow task [Integration Services], components"
  - "discovering data flow components"
  - "components [Integration Services], data flow"
  - "data flow [Integration Services], components"
ms.assetid: ff92a96a-8af6-4532-82cc-c0bbff92401b
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Discovering Data Flow Components Programmatically
  After you have added a data flow task to a package, your next step may be to determine what data flow components are available for your use. You can programmatically discover the data flow sources, transformations, and destinations that are installed and available on the local computer. For information about adding a data flow task to the package, see [Adding the Data Flow Task Programmatically](../building-packages-programmatically/adding-the-data-flow-task-programmatically.md).  
  
## Discovering Components  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Application> class provides the <xref:Microsoft.SqlServer.Dts.Runtime.Application.PipelineComponentInfos%2A> collection, which contains a <xref:Microsoft.SqlServer.Dts.Runtime.PipelineComponentInfo> object for each component correctly installed on the local computer. Each <xref:Microsoft.SqlServer.Dts.Runtime.PipelineComponentInfo> contains information about a component such as its name, description, and creation name. You can use the value returned in the <xref:Microsoft.SqlServer.Dts.Runtime.PipelineComponentInfo.CreationName%2A> property to set the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.ComponentClassID%2A> property of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> when you add a component to a package.  
  
## Next Step  
 After discovering available components, the next step is to add and configure the components, which is discussed in the next topic, [Adding Data Flow Components Programmatically](../building-packages-programmatically/adding-data-flow-components-programmatically.md).  
  
## Sample  
 The following code sample shows how to enumerate the <xref:Microsoft.SqlServer.Dts.Runtime.PipelineComponentInfos> collection of the <xref:Microsoft.SqlServer.Dts.Runtime.Application> object to programmatically discover the data flow components available on the local computer. This sample requires a reference to the Microsoft.SqlServer.ManagedDTS assembly.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Application application = new Application();  
      PipelineComponentInfos componentInfos = application.PipelineComponentInfos;  
  
      foreach (PipelineComponentInfo componentInfo in componentInfos)  
      {  
        Console.WriteLine("Name: " + componentInfo.Name + "\n" +  
          " CreationName: " + componentInfo.CreationName + "\n");  
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
  
    Dim application As Application = New Application()  
  
    Dim componentInfos As PipelineComponentInfos = application.PipelineComponentInfos  
  
    For Each componentInfo As PipelineComponentInfo In componentInfos  
      Console.WriteLine("Name: " & componentInfo.Name & vbCrLf & _  
        " CreationName: " & componentInfo.CreationName & vbCrLf)  
    Next  
  
    Console.Read()  
  
  End Sub  
  
End Module  
```  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Adding Data Flow Components Programmatically](../building-packages-programmatically/adding-data-flow-components-programmatically.md)  
  
  
