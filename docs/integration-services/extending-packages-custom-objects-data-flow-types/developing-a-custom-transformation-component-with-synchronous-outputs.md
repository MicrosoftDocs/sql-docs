---
title: "Developing a Custom Transformation Component with Synchronous Outputs | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "custom data flow components [Integration Services], transformation components"
  - "ProcessInput method"
  - "buffer allocations [Integration Services]"
  - "synchronous outputs [Integration Services]"
  - "transformation components [Integration Services]"
  - "output columns [Integration Services]"
  - "data flow components [Integration Services], transformation components"
ms.assetid: b694d21f-9919-402d-9192-666c6449b0b7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Developing a Custom Transformation Component with Synchronous Outputs

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Transformation components with synchronous outputs receive rows from upstream components, and read or modify the values in the columns of these rows as they pass the rows to downstream components. They may also define additional output columns that are derived from the columns provided by upstream components, but they do not add rows to the data flow. For more information about the difference between synchronous and asynchronous components, see [Understanding Synchronous and Asynchronous Transformations](../../integration-services/understanding-synchronous-and-asynchronous-transformations.md).  
  
 This kind of component is suited for tasks where the data is modified inline as it is provided to the component and where the component does not have to see all the rows before processing them. It is the easiest component to develop because transformations with synchronous outputs typically do not connect to external data sources, manage external metadata columns, or add rows to output buffers.  
  
 Creating a transformation component with synchronous outputs involves adding an <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100> that will contain the upstream columns selected for the component, and a <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100> object that may contain derived columns created by the component. It also includes implementing the design-time methods, and providing code that reads or modifies the columns in the incoming buffer rows during execution.  
  
 This section provides the information that is required to implement a custom transformation component, and provides code examples to help you better understand the concepts.  
  
## Design Time  
 The design-time code for this component involves creating the inputs and outputs, adding any additional output columns that the component generates, and validating the configuration of the component.  
  
### Creating the Component  
 The inputs, outputs, and custom properties of the component are typically created during the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A> method. There are two ways that you can add the input and the output of a transformation component with synchronous outputs. You can use the base class implementation of the method and then modify the default input and output that it creates, or you can explicitly add the input and output yourself.  
  
 The following code example shows an implementation of <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A> that explicitly adds the input and output objects. The call to the base class that would accomplish the same thing is included in a comment.  
  
```csharp  
using Microsoft.SqlServer.Dts.Pipeline;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.Samples.SqlServer.Dts  
{  
    [DtsPipelineComponent(DisplayName = "SynchronousComponent", ComponentType = ComponentType.Transform)]  
    public class SyncComponent : PipelineComponent  
    {  
  
        public override void ProvideComponentProperties()  
        {  
            // Add the input.  
            IDTSInput100 input = ComponentMetaData.InputCollection.New();  
            input.Name = "Input";  
  
            // Add the output.  
            IDTSOutput100 output = ComponentMetaData.OutputCollection.New();  
            output.Name = "Output";  
            output.SynchronousInputID = input.ID;  
  
            // Alternatively, you can let the base class add the input and output  
            // and set the SynchronousInputID of the output to the ID of the input.  
            // base.ProvideComponentProperties();  
        }  
    }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Pipeline  
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper  
Imports Microsoft.SqlServer.Dts.Runtime  
  
<DtsPipelineComponent(DisplayName:="SynchronousComponent", ComponentType:=ComponentType.Transform)> _  
Public Class SyncComponent  
    Inherits PipelineComponent  
  
    Public Overrides Sub ProvideComponentProperties()  
  
        ' Add the input.  
        Dim input As IDTSInput100 = ComponentMetaData.InputCollection.New()  
        input.Name = "Input"  
  
        ' Add the output.  
        Dim output As IDTSOutput100 = ComponentMetaData.OutputCollection.New()  
        output.Name = "Output"  
        output.SynchronousInputID = Input.ID  
  
        ' Alternatively, you can let the base class add the input and output  
        ' and set the SynchronousInputID of the output to the ID of the input.  
        ' base.ProvideComponentProperties();  
  
    End Sub  
  
End Class  
```  
  
### Creating and Configuring Output Columns  
 Although transformation components with synchronous outputs do not add rows to buffers, they may add extra output columns to their output. Typically, when a component adds an output column, the values for the new output column are derived at run time from the data that is contained in one or more of the columns provided to the component by an upstream component.  
  
 After an output column has been created, its data type properties must be set. Setting the data type properties of an output column requires special handling and is performed by calling the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.SetDataTypeProperties%2A> method. This method is required because the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.DataType%2A>, <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.Length%2A>, <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.Precision%2A>, and <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.CodePage%2A> properties are individually read-only, because each depends on the settings of the other. This method guarantees that the values of the properties are set consistently, and the data flow task validates that they are set correctly.  
  
 The <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.DataType%2A> of the column determines the values that are set for the other properties. The following table shows the requirements on the dependent properties for each <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100.DataType%2A>. The data types not listed have their dependent properties set to zero.  
  
|DataType|Length|Scale|Precision|CodePage|  
|--------------|------------|-----------|---------------|--------------|  
|DT_DECIMAL|0|Greater than 0 and less than or equal to 28.|0|0|  
|DT_CY|0|0|0|0|  
|DT_NUMERIC|0|Greater than 0 and less than or equal to 28, and less than Precision.|Greater than or equal to 1 and less than or equal to 38.|0|  
|DT_BYTES|Greater than 0.|0|0|0|  
|DT_STR|Greater than 0 and less than 8000.|0|0|Not 0, and a valid code page.|  
|DT_WSTR|Greater than 0 and less than 4000.|0|0|0|  
  
 Because the restrictions on the data type properties are based on the data type of the output column, you must choose the correct [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type when you work with managed types. The base class provides three helper methods, <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ConvertBufferDataTypeToFitManaged%2A>, <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.BufferTypeToDataRecordType%2A>, and <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.DataRecordTypeToBufferType%2A> that assist managed component developers in selecting an [!INCLUDE[ssIS](../../includes/ssis-md.md)] data type given a managed type. These methods convert managed data types to [!INCLUDE[ssIS](../../includes/ssis-md.md)] data types, and vice versa.  
  
## Run Time  
 Generally, the implementation of the run-time part of the component is categorized into two tasks-locating the input and output columns of the component in the buffer, and reading or writing the values of these columns in the incoming buffer rows.  
  
### Locating Columns in the Buffer  
 The number of columns in the buffers that are provided to a component during execution will likely exceed the number of columns in the input or output collections of the component. This is because each buffer contains all the output columns defined in the components in a data flow. To ensure that the buffer columns are correctly matched to the columns of the input or output, component developers must use the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBufferManager100.FindColumnByLineageID%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.BufferManager%2A>. This method locates a column in the specified buffer by its lineage ID. Typically columns are located during <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PreExecute%2A> because this is the first run-time method where the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.BufferManager%2A> property becomes available.  
  
 The following code example shows a component that locates indexes of the columns in its input and output column collection during <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PreExecute%2A>. The column indexes are stored in an integer array, and can be accessed by the component during <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A>.  
  
```csharp  
int []inputColumns;  
int []outputColumns;  
  
public override void PreExecute()  
{  
    IDTSInput100 input = ComponentMetaData.InputCollection[0];  
    IDTSOutput100 output = ComponentMetaData.OutputCollection[0];  
  
    inputColumns = new int[input.InputColumnCollection.Count];  
    outputColumns = new int[output.OutputColumnCollection.Count];  
  
    for(int col=0; col < input.InputColumnCollection.Count; col++)  
    {  
        IDTSInputColumn100 inputColumn = input.InputColumnCollection[col];  
        inputColumns[col] = BufferManager.FindColumnByLineageID(input.Buffer, inputColumn.LineageID);  
    }  
  
    for(int col=0; col < output.OutputColumnCollection.Count; col++)  
    {  
        IDTSOutputColumn100 outputColumn = output.OutputColumnCollection[col];  
        outputColumns[col] = BufferManager.FindColumnByLineageID(input.Buffer, outputColumn.LineageID);  
    }  
  
}  
```  
  
```vb  
Public Overrides Sub PreExecute()  
  
    Dim input As IDTSInput100 = ComponentMetaData.InputCollection(0)  
    Dim output As IDTSOutput100 = ComponentMetaData.OutputCollection(0)  
  
    ReDim inputColumns(input.InputColumnCollection.Count)  
    ReDim outputColumns(output.OutputColumnCollection.Count)  
  
    For col As Integer = 0 To input.InputColumnCollection.Count  
  
        Dim inputColumn As IDTSInputColumn100 = input.InputColumnCollection(col)  
        inputColumns(col) = BufferManager.FindColumnByLineageID(input.Buffer, inputColumn.LineageID)  
    Next  
  
    For col As Integer = 0 To output.OutputColumnCollection.Count  
  
        Dim outputColumn As IDTSOutputColumn100 = output.OutputColumnCollection(col)  
        outputColumns(col) = BufferManager.FindColumnByLineageID(input.Buffer, outputColumn.LineageID)  
    Next  
  
End Sub  
```  
  
### Processing Rows  
 Components receive <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineBuffer> objects that contain rows and columns in the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> method. During this method the rows in the buffer are iterated, and the columns identified during <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PreExecute%2A> are read and modified. The method is called repeatedly by the data flow task until no more rows are provided from the upstream component.  
  
 An individual column in the buffer is read or written by using the array indexer access method, or by using one of the **Get** or **Set** methods. The **Get** and **Set** methods are more efficient and should be used when the data type of the column in the buffer is known.  
  
 The following code example shows an implementation of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> method that processes incoming rows.  
  
```csharp  
public override void ProcessInput( int InputID, PipelineBuffer buffer)  
{  
       while( buffer.NextRow())  
       {  
            for(int x=0; x < inputColumns.Length;x++)  
            {  
                if(!buffer.IsNull(inputColumns[x]))  
                {  
                    object columnData = buffer[inputColumns[x]];  
                    // TODO: Modify the column data.  
                    buffer[inputColumns[x]] = columnData;  
                }  
            }  
  
      }  
}  
```  
  
```vb  
Public Overrides Sub ProcessInput(ByVal InputID As Integer, ByVal buffer As PipelineBuffer)  
  
        While (buffer.NextRow())  
  
            For x As Integer = 0 To inputColumns.Length  
  
                if buffer.IsNull(inputColumns(x)) = false then  
  
                    Dim columnData As Object = buffer(inputColumns(x))  
                    ' TODO: Modify the column data.  
                    buffer(inputColumns(x)) = columnData  
  
                End If  
            Next  
  
        End While  
End Sub  
```  
  
## Sample  
 The following sample shows a simple transformation component with synchronous outputs that converts the values of all string columns to uppercase. This sample does not demonstrate all the methods and functionality discussed in this topic. It demonstrates the important methods that every custom transformation component with synchronous outputs must override, but does not contain code for design-time validation.  
  
```csharp  
using System;  
using System.Collections;  
using Microsoft.SqlServer.Dts.Pipeline;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
using Microsoft.SqlServer.Dts.Runtime.Wrapper;  
  
namespace Uppercase  
{  
  [DtsPipelineComponent(DisplayName = "Uppercase")]  
  public class Uppercase : PipelineComponent  
  {  
    ArrayList m_ColumnIndexList = new ArrayList();  
  
    public override void ProvideComponentProperties()  
    {  
      base.ProvideComponentProperties();  
      ComponentMetaData.InputCollection[0].Name = "Uppercase Input";  
      ComponentMetaData.OutputCollection[0].Name = "Uppercase Output";  
    }  
  
    public override void PreExecute()  
    {  
      IDTSInput100 input = ComponentMetaData.InputCollection[0];  
      IDTSInputColumnCollection100 inputColumns = input.InputColumnCollection;  
  
      foreach (IDTSInputColumn100 column in inputColumns)  
      {  
        if (column.DataType == DataType.DT_STR || column.DataType == DataType.DT_WSTR)  
        {  
          m_ColumnIndexList.Add((int)BufferManager.FindColumnByLineageID(input.Buffer, column.LineageID));  
        }  
      }  
    }  
  
    public override void ProcessInput(int inputID, PipelineBuffer buffer)  
    {  
      while (buffer.NextRow())  
      {  
        foreach (int columnIndex in m_ColumnIndexList)  
        {  
          string str = buffer.GetString(columnIndex);  
          buffer.SetString(columnIndex, str.ToUpper());  
        }  
      }  
    }  
  }  
}  
```  
  
```vb  
Imports System   
Imports System.Collections   
Imports Microsoft.SqlServer.Dts.Pipeline   
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper   
Imports Microsoft.SqlServer.Dts.Runtime.Wrapper   
Namespace Uppercase   
  
 <DtsPipelineComponent(DisplayName="Uppercase")> _   
 Public Class Uppercase   
 Inherits PipelineComponent   
   Private m_ColumnIndexList As ArrayList = New ArrayList   
  
   Public  Overrides Sub ProvideComponentProperties()   
     MyBase.ProvideComponentProperties   
     ComponentMetaData.InputCollection(0).Name = "Uppercase Input"   
     ComponentMetaData.OutputCollection(0).Name = "Uppercase Output"   
   End Sub   
  
   Public  Overrides Sub PreExecute()   
     Dim input As IDTSInput100 = ComponentMetaData.InputCollection(0)   
     Dim inputColumns As IDTSInputColumnCollection100 = input.InputColumnCollection   
     For Each column As IDTSInputColumn100 In inputColumns   
       If column.DataType = DataType.DT_STR OrElse column.DataType = DataType.DT_WSTR Then   
         m_ColumnIndexList.Add(CType(BufferManager.FindColumnByLineageID(input.Buffer, column.LineageID), Integer))   
       End If   
     Next   
   End Sub   
  
   Public  Overrides Sub ProcessInput(ByVal inputID As Integer, ByVal buffer As PipelineBuffer)   
     While buffer.NextRow   
       For Each columnIndex As Integer In m_ColumnIndexList   
         Dim str As String = buffer.GetString(columnIndex)   
         buffer.SetString(columnIndex, str.ToUpper)   
       Next   
     End While   
   End Sub   
 End Class   
End Namespace  
```  
  
## See Also  
 [Developing a Custom Transformation Component with Asynchronous Outputs](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-asynchronous-outputs.md)   
 [Understanding Synchronous and Asynchronous Transformations](../../integration-services/understanding-synchronous-and-asynchronous-transformations.md)   
 [Creating a Synchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-synchronous-transformation-with-the-script-component.md)  
  
  
