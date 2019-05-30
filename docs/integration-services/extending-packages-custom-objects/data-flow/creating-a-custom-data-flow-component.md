---
title: "Creating a Custom Data Flow Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "design-time component interface [Integration Services]"
  - "custom data flow components [Integration Services], about data flow components"
  - "custom data flow components [Integration Services]"
  - "data flow components [Integration Services]"
  - "data flow components [Integration Services], developing"
ms.assetid: 9d96bcf5-eba8-44bd-b113-ed51ad0d0521
author: janinezhang
ms.author: janinez
manager: craigg
---
# Creating a Custom Data Flow Component

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], the data flow task exposes an object model that lets developers create custom data flow components-sources, transformations, and destinations-by using the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] and managed code.  
  
 A data flow task consists of components that contain an <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface and a collection of <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPath100> objects that define the movement of data between components.  
  
> [!NOTE]  
>  When you create a custom provider, you need to update the ProviderDescriptors.xml file with the metadata column values.  
  
## Design Time and Run Time  
 Before execution, the data flow task is said to be in a design-time state, as it undergoes incremental changes. Changes may include the addition or removal of components, the addition or removal of the path objects that connect components, and changes to the metadata of the components. When metadata changes occur, the component can monitor and react to the changes. For example, a component can disallow certain changes or to make additional changes in response to a change. At design time, the designer interacts with a component through the design-time <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSDesigntimeComponent100> interface.  
  
 At execution time, the data flow task examines the sequence of components, prepares an execution plan, and manages a pool of worker threads that execute the work plan. Although each worker thread performs some work that is internal to the data flow task, the principal task of the worker thread is to call the methods of the component through the run-time <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeComponent100> interface.  
  
## Creating a Component  
 To create a data flow component, you derive a class from the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> base class, apply the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute> class, and then override the appropriate methods of the base class. The <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSDesigntimeComponent100> and <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeComponent100> interfaces, and exposes their methods for you to override in your component.  
  
 Depending on the objects used by your component, your project will require references to some or all of the following assemblies:  
  
|Feature|Assembly to reference|Namespace to import|  
|-------------|---------------------------|-------------------------|  
|Data flow|Microsoft.SqlServer.PipelineHost|<xref:Microsoft.SqlServer.Dts.Pipeline>|  
|Data flow wrapper|Microsoft.SqlServer.DTSPipelineWrap|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper>|  
|Runtime|Microsoft.SQLServer.ManagedDTS|<xref:Microsoft.SqlServer.Dts.Runtime>|  
|Runtime wrapper|Microsoft.SqlServer.DTSRuntimeWrap|<xref:Microsoft.SqlServer.Dts.Runtime.Wrapper>|  
  
 The following code example shows a simple component that derives from the base class, and applies the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute>. You need to add a reference to the Microsoft.SqlServer.DTSPipelineWrap assembly.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Pipeline;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
  
namespace Microsoft.Samples.SqlServer.Dts  
{  
    [DtsPipelineComponent(DisplayName = "SampleComponent", ComponentType = ComponentType.Transform )]  
    public class BasicComponent: PipelineComponent  
    {  
        // TODO: Override the base class methods.  
    }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Pipeline  
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper  
  
<DtsPipelineComponent(DisplayName:="SampleComponent", ComponentType:=ComponentType.Transform)> _  
Public Class BasicComponent  
  
    Inherits PipelineComponent  
  
    ' TODO: Override the base class methods.  
  
End Class  
```  
  
## See Also  
 [Developing a User Interface for a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/developing-a-user-interface-for-a-data-flow-component.md)  
  
  
