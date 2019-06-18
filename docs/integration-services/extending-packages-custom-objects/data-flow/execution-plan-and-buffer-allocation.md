---
title: "Execution Plan and Buffer Allocation | Microsoft Docs"
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
  - "custom data flow components [Integration Services], buffer allocations"
  - "custom data flow components [Integration Services], execution plans"
  - "buffer allocations [Integration Services]"
  - "data flow components [Integration Services], buffer allocations"
  - "data flow components [Integration Services], execution plans"
  - "execution plans [Integration Services]"
ms.assetid: 679d9ff0-641e-47c3-abb8-d1a7dcb279dd
author: janinezhang
ms.author: janinez
manager: craigg
---
# Execution Plan and Buffer Allocation

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Before execution, the data flow task examines its components and generates an execution plan for each sequence of components. This section provides details about the execution plan, how to view the plan, and how input and output buffers are allocated based on the execution plan.  
  
## Understanding the Execution Plan  
 An execution plan contains source threads and work threads, and each thread contains work lists that specify output work lists for source threads or input and output work lists for work threads. The source threads in an execution plan represent the source components in the data flow and are identified in the execution plan by *SourceThreadn*, where *n* is the zero-based number of the source thread.  
  
 Each source thread creates a buffer, sets a listener, and calls the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PrimeOutput%2A> method on the source component. This is where execution starts and data originates, as the source component starts adding rows to the output buffers that are provided to it by the data flow task. After the source threads are running, the balance of work is distributed among work threads.  
  
 A work thread may contain both input and output work lists and is identified in the execution plan as *WorkThreadn*, where *n* is the zero-based number of the work thread. These threads contain output work lists when the graph contains a component with asynchronous outputs.  
  
 The following sample execution plan represents a data flow that contains a source component connected to a transformation with an asynchronous output connected to a destination component. In this example, WorkThread0 contains an output work list because the transformation component has an asynchronous output.  
  
```  
SourceThread0   
    Influences: 72 158   
    Output Work List   
        CreatePrimeBuffer of type 1 for output id 10   
        SetBufferListener: "WorkThread0" for input ID 73   
        CallPrimeOutput on component "OLE DB Source" (1)   
    End Output Work List   
    This thread drives 0 distributors   
End SourceThread0   
WorkThread0   
    Influences: 72 158   
    Input Work list, input ID 73   
        CallProcessInput on input ID 73 on component "Sort" (72) for view type 2   
    End Input Work list for input 73   
    Output Work List   
        CreatePrimeBuffer of type 3 for output id 74   
        SetBufferListener: "WorkThread1" for input ID 171with internal handoff   
        CallPrimeOutput on component "Sort" (72)   
    End Output Work List   
    This thread drives 0 distributors   
End WorkThread0   
WorkThread1   
    Influences: 158   
    Input Work list, input ID 171  
        CallProcessInput on input ID 171 on component "OLE DB Destination" (158) for view type 4  
    End Input Work list for input 171   
    Output Work List   
    End Output Work List   
    This thread drives 0 distributors   
End WorkThread1  
```  
  
> [!NOTE]  
>  The execution plan is generated every time a package is executed, and can be captured by adding a log provider to the package, enabling logging, and selecting the **PipelineExecutionPlan** event.  
  
## Understanding Buffer Allocation  
 Based on the execution plan, the data flow task creates buffers that contain the columns defined in the outputs of the data flow components. The buffer is reused as the data flows through the sequence of components, until a component with asynchronous outputs is encountered. Then, a new buffer is created, which contains the output columns of the asynchronous output and the output columns of downstream components.  
  
 During execution, components have access to the buffer in the current source or work thread. The buffer is either an input buffer, provided by the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> method, or an output buffer, provided by the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PrimeOutput%2A> method. The <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineBuffer.Mode%2A> property of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineBuffer> also identifies each buffer as an input or output buffer.  
  
 Transformation components with asynchronous outputs receive the existing input buffer from the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> method, and receive the new output buffer from the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PrimeOutput%2A> method. A transformation component with asynchronous outputs is the only type of data flow component that receives both an input and an output buffer.  
  
 Because the buffer provided to a component is likely to contain more columns than the component has in its input or output column collections, component developers can call the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBufferManager100.FindColumnByLineageID%2A> method to locate a column in the buffer by specifying its **LineageID**.  
  
