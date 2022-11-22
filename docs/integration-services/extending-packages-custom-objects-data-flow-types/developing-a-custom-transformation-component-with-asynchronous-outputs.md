---
description: "Developing a Custom Transformation Component with Asynchronous Outputs"
title: "Developing a Custom Transformation Component with Asynchronous Outputs | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "custom data flow components [Integration Services], transformation components"
  - "asynchronous outputs [Integration Services]"
  - "ProcessInput method"
  - "cache [Integration Services]"
  - "buffer allocations [Integration Services]"
  - "transformation components [Integration Services]"
  - "output columns [Integration Services]"
  - "PrimeOutput method"
  - "data flow components [Integration Services], transformation components"
ms.assetid: 1c3e92c7-a4fa-4fdd-b9ca-ac3069536274
author: chugugrace
ms.author: chugu
---
# Developing a Custom Transformation Component with Asynchronous Outputs

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  You use a component with asynchronous outputs when a transform cannot output rows until the component has received all its input rows, or when the transformation does not produce exactly one output row for each row received as input. The Aggregate transformation, for example, cannot calculate a sum across rows until it has read all the rows. In contrast, you can use a component with synchronous outputs any time when you modify each row of data as it passes through. You can modify the data for each row in place, or you can create one or more new columns, each of which has a value for every one of the input rows. For more information about the difference between synchronous and asynchronous components, see [Understanding Synchronous and Asynchronous Transformations](../../integration-services/understanding-synchronous-and-asynchronous-transformations.md).  
  
 Transformation components with asynchronous outputs are unique because they act as both destination and source components. This kind of component receives rows from upstream components, and adds rows that are consumed by downstream components. No other data flow component performs both of these operations.  
  
 The columns from upstream components that are available to a component with synchronous outputs are automatically available to components downstream from the component. Therefore, a component with synchronous outputs does not have to define any output columns to provide columns and rows to the next component. Components with asynchronous outputs, on the other hand, must define output columns and provide rows to downstream components. Therefore a component with asynchronous outputs has more tasks to perform during both design and execution time, and the component developer has more code to implement.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] contains several transformations with asynchronous outputs. For example, the Sort transformation requires all its rows before it can sort them, and achieves this by using asynchronous outputs. After it has received all its rows, it sorts them and adds them to its output.  
  
 This section explains in detail how to develop transformations with asynchronous outputs. For more information about source component development, see [Developing a Custom Source Component](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-source-component.md).  
  
## Design Time  
  
### Creating the Component  
 The <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100.SynchronousInputID%2A> property on the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100> object identifies whether an output is synchronous or asynchronous. To create an asynchronous output, add the output to the component and set the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100.SynchronousInputID%2A> to zero. Setting this property also determines whether the data flow task allocates <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineBuffer> objects for both the input and output of the component, or whether a single buffer is allocated and shared between the two objects.  
  
 The following sample code shows a component that creates an asynchronous output in its <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A> implementation.  
  
```csharp  
using Microsoft.SqlServer.Dts.Pipeline;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.Samples.SqlServer.Dts  
{  
    [DtsPipelineComponent(DisplayName = "AsyncComponent",ComponentType = ComponentType.Transform)]  
    public class AsyncComponent : PipelineComponent  
    {  
        public override void ProvideComponentProperties()  
        {  
            // Call the base class, which adds a synchronous input  
            // and output.  
            base.ProvideComponentProperties();  
  
            // Make the output asynchronous.  
            IDTSOutput100 output = ComponentMetaData.OutputCollection[0];  
            output.SynchronousInputID = 0;  
        }  
    }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Pipeline  
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper  
Imports Microsoft.SqlServer.Dts.Runtime  
  
<DtsPipelineComponent(DisplayName:="AsyncComponent", ComponentType:=ComponentType.Transform)> _  
Public Class AsyncComponent  
    Inherits PipelineComponent  
  
    Public Overrides Sub ProvideComponentProperties()  
  
        ' Call the base class, which adds a synchronous input  
        ' and output.  
        Me.ProvideComponentProperties()  
  
        ' Make the output asynchronous.  
        Dim output As IDTSOutput100 = ComponentMetaData.OutputCollection(0)  
        output.SynchronousInputID = 0  
  
    End Sub  
  
End Class  
```  
  
### Creating and Configuring Output Columns  
 As mentioned earlier, an asynchronous component adds columns to its output column collection to provide columns to downstream components. There are several design-time methods to choose from, depending on the needs of the component. For example, if you want to pass all the columns from the upstream components to the downstream components, you would override the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.OnInputPathAttached%2A> method to add the columns, because this is the first method in which the input columns are available to the component.  
  
 If the component creates output columns based on the columns selected for its input, override the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.SetUsageType%2A> method to select the output columns and to indicate how they will be used.  
  
 If a component with asynchronous outputs creates output columns based on the columns from upstream components, and the available upstream columns change, the component should update its output column collection. These changes should be detected by the component during <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.Validate%2A>, and fixed during <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ReinitializeMetaData%2A>.  
  
> [!NOTE]  
>  When an output column is removed from the output column collection, downstream components in the data flow that reference the column are adversely affected. The output column must be repaired without removing and recreating the column to prevent breaking the downstream components. For example, if the data type of the column has changed, you must update the data type.  
  
 The following code example shows a component that adds an output column to its output column collection for each column available from the upstream component.  
  
```csharp  
public override void OnInputPathAttached(int inputID)  
{  
   IDTSInput100 input = ComponentMetaData.InputCollection.GetObjectByID(inputID);  
   IDTSOutput100 output = ComponentMetaData.OutputCollection[0];  
   IDTSVirtualInput100 vInput = input.GetVirtualInput();  
  
   foreach (IDTSVirtualInputColumn100 vCol in vInput.VirtualInputColumnCollection)  
   {  
      IDTSOutputColumn100 outCol = output.OutputColumnCollection.New();  
      outCol.Name = vCol.Name;  
      outCol.SetDataTypeProperties(vCol.DataType, vCol.Length, vCol.Precision, vCol.Scale, vCol.CodePage);  
   }  
}  
```  
  
```vb  
Public Overrides Sub OnInputPathAttached(ByVal inputID As Integer)  
  
    Dim input As IDTSInput100 = ComponentMetaData.InputCollection.GetObjectByID(inputID)  
    Dim output As IDTSOutput100 = ComponentMetaData.OutputCollection(0)  
    Dim vInput As IDTSVirtualInput100 = input.GetVirtualInput()  
  
    For Each vCol As IDTSVirtualInputColumn100 In vInput.VirtualInputColumnCollection  
  
        Dim outCol As IDTSOutputColumn100 = output.OutputColumnCollection.New()  
        outCol.Name = vCol.Name  
        outCol.SetDataTypeProperties(vCol.DataType, vCol.Length, vCol.Precision, vCol.Scale, vCol.CodePage)  
  
    Next  
End Sub  
```  
  
## Run Time  
 Components with asynchronous outputs also execute a different sequence of methods at run time than other types of components. First, they are the only components that receive a call to both the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PrimeOutput%2A> and the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> methods. Components with asynchronous outputs also require access to all the incoming rows before they can start processing; therefore, they must cache the input rows internally until all rows have been read. Finally, unlike other components, components with asynchronous outputs receive both an input buffer and an output buffer.  
  
### Understanding the Buffers  
 The input buffer is received by the component during <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A>. This buffer contains the rows added to the buffer by upstream components. The buffer also contains the columns of the component's input, in addition to the columns that were provided in the output of an upstream component but were not added to the asynchronous component's input collection.  
  
 The output buffer, which is provided to the component in <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PrimeOutput%2A>, does not initially contain rows. The component adds rows to this buffer and provides the buffer to downstream components when it is full. The output buffer contains the columns defined in the component's output column collection, in addition to any columns that other downstream components have added to their outputs.  
  
 This is different behavior from that of components with synchronous outputs, which receive a single shared buffer. The shared buffer of a component with synchronous outputs contains both the input and output columns of the component, in addition to columns added to the outputs of upstream and downstream components.  
  
### Processing Rows  
  
#### Caching Input Rows  
 When you write a component with asynchronous outputs, you have three options for adding rows to the output buffer. You can add them as input rows are received, you can cache them until the component has received all the rows from the upstream component, or you can add them when it is appropriate to do so for the component. The method that you choose depends on the requirements of the component. For example, the Sort component requires that all the upstream rows be received before they can be sorted. Therefore, it waits until all rows have been read before adding rows to the output buffer.  
  
 The rows that are received in the input buffer must be cached internally by the component until it is ready to process them. The incoming buffer rows can be cached in a data table, a multidimensional array, or any other internal structure.  
  
#### Adding Output Rows  
 Whether you add rows to the output buffer as they are received or after receiving all of the rows, you do so by calling the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineBuffer.AddRow%2A> method on the output buffer. After you have added the row, you set the values of each column in the new row.  
  
 Because there are sometimes more columns in the output buffer than in the output column collection of the component, you must locate the index of the appropriate column in the buffer before you can set its value. The <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBufferManager100.FindColumnByLineageID%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.BufferManager%2A> property returns the index of the column in the buffer row with the specified lineage ID, which is then used to assign the value to the buffer column.  
  
 The <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PreExecute%2A> method, which is called before the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PrimeOutput%2A> method or the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> method, is the first method where the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.BufferManager%2A> property is available, and the first opportunity to locate the indexes of the columns in the input and output buffers.  
  
## Sample  
 The following sample shows a simple transformation component with asynchronous outputs that adds rows to the output buffer as they are received. This sample does not demonstrate all the methods and functionality discussed in this topic. It demonstrates the important methods that every custom transformation component with asynchronous outputs must override, but does not contain code for design-time validation. Also, the code in <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> assumes that the output column collection has one column for each column in the input column collection.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Pipeline;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
using Microsoft.SqlServer.Dts.Runtime.Wrapper;  
  
namespace Microsoft.Samples.SqlServer.Dts  
{  
   [DtsPipelineComponent(DisplayName = "AsynchronousOutput")]  
   public class AsynchronousOutput : PipelineComponent  
   {  
      PipelineBuffer outputBuffer;  
      int[] inputColumnBufferIndexes;  
      int[] outputColumnBufferIndexes;  
  
      public override void ProvideComponentProperties()  
      {  
         // Let the base class add the input and output objects.  
         base.ProvideComponentProperties();  
  
         // Name the input and output, and make the  
         // output asynchronous.  
         ComponentMetaData.InputCollection[0].Name = "Input";  
         ComponentMetaData.OutputCollection[0].Name = "AsyncOutput";  
         ComponentMetaData.OutputCollection[0].SynchronousInputID = 0;  
      }  
      public override void PreExecute()  
      {  
         IDTSInput100 input = ComponentMetaData.InputCollection[0];  
         IDTSOutput100 output = ComponentMetaData.OutputCollection[0];  
  
         inputColumnBufferIndexes = new int[input.InputColumnCollection.Count];  
         outputColumnBufferIndexes = new int[output.OutputColumnCollection.Count];  
  
         for (int col = 0; col < input.InputColumnCollection.Count; col++)  
            inputColumnBufferIndexes[col] = BufferManager.FindColumnByLineageID(input.Buffer, input.InputColumnCollection[col].LineageID);  
  
         for (int col = 0; col < output.OutputColumnCollection.Count; col++)  
            outputColumnBufferIndexes[col] = BufferManager.FindColumnByLineageID(output.Buffer, output.OutputColumnCollection[col].LineageID);  
  
      }  
  
      public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)  
      {  
         if (buffers.Length != 0)  
            outputBuffer = buffers[0];  
      }  
      public override void ProcessInput(int inputID, PipelineBuffer buffer)  
      {  
            // Advance the buffer to the next row.  
            while (buffer.NextRow())  
            {  
               // Add a row to the output buffer.  
               outputBuffer.AddRow();  
               for (int x = 0; x < inputColumnBufferIndexes.Length; x++)  
               {  
                  // Copy the data from the input buffer column to the output buffer column.  
                  outputBuffer[outputColumnBufferIndexes[x]] = buffer[inputColumnBufferIndexes[x]];  
               }  
            }  
         if (buffer.EndOfRowset)  
         {  
            // EndOfRowset on the input buffer is true.  
            // Set EndOfRowset on the output buffer.  
            outputBuffer.SetEndOfRowset();  
         }  
      }  
   }  
}  
```  
  
```vb  
Imports System  
Imports Microsoft.SqlServer.Dts.Pipeline  
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper  
Imports Microsoft.SqlServer.Dts.Runtime.Wrapper  
  
Namespace Microsoft.Samples.SqlServer.Dts  
  
    <DtsPipelineComponent(DisplayName:="AsynchronousOutput")> _  
    Public Class AsynchronousOutput  
  
        Inherits PipelineComponent  
  
        Private outputBuffer As PipelineBuffer  
        Private inputColumnBufferIndexes As Integer()  
        Private outputColumnBufferIndexes As Integer()  
  
        Public Overrides Sub ProvideComponentProperties()  
  
            ' Let the base class add the input and output objects.  
            Me.ProvideComponentProperties()  
  
            ' Name the input and output, and make the  
            ' output asynchronous.  
            ComponentMetaData.InputCollection(0).Name = "Input"  
            ComponentMetaData.OutputCollection(0).Name = "AsyncOutput"  
            ComponentMetaData.OutputCollection(0).SynchronousInputID = 0  
        End Sub  
  
        Public Overrides Sub PreExecute()  
  
            Dim input As IDTSInput100 = ComponentMetaData.InputCollection(0)  
            Dim output As IDTSOutput100 = ComponentMetaData.OutputCollection(0)  
  
            ReDim inputColumnBufferIndexes(input.InputColumnCollection.Count)  
            ReDim outputColumnBufferIndexes(output.OutputColumnCollection.Count)  
  
            For col As Integer = 0 To input.InputColumnCollection.Count  
                inputColumnBufferIndexes(col) = BufferManager.FindColumnByLineageID(input.Buffer, input.InputColumnCollection(col).LineageID)  
            Next  
  
            For col As Integer = 0 To output.OutputColumnCollection.Count  
                outputColumnBufferIndexes(col) = BufferManager.FindColumnByLineageID(output.Buffer, output.OutputColumnCollection(col).LineageID)  
            Next  
  
        End Sub  
        Public Overrides Sub PrimeOutput(ByVal outputs As Integer, ByVal outputIDs As Integer(), ByVal buffers As PipelineBuffer())  
  
            If buffers.Length <> 0 Then  
                outputBuffer = buffers(0)  
            End If  
  
        End Sub  
  
        Public Overrides Sub ProcessInput(ByVal inputID As Integer, ByVal buffer As PipelineBuffer)  
  
                ' Advance the buffer to the next row.  
                While (buffer.NextRow())  
  
                    ' Add a row to the output buffer.  
                    outputBuffer.AddRow()  
                    For x As Integer = 0 To inputColumnBufferIndexes.Length  
  
                        ' Copy the data from the input buffer column to the output buffer column.  
                        outputBuffer(outputColumnBufferIndexes(x)) = buffer(inputColumnBufferIndexes(x))  
  
                    Next  
                End While  
  
            If buffer.EndOfRowset = True Then  
                ' EndOfRowset on the input buffer is true.  
                ' Set the end of row set on the output buffer.  
                outputBuffer.SetEndOfRowset()  
            End If  
        End Sub  
    End Class  
End Namespace  
```  
  
## See Also  
 [Developing a Custom Transformation Component with Synchronous Outputs](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-synchronous-outputs.md)   
 [Understanding Synchronous and Asynchronous Transformations](../../integration-services/understanding-synchronous-and-asynchronous-transformations.md)   
 [Creating an Asynchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-an-asynchronous-transformation-with-the-script-component.md)  
  
  
