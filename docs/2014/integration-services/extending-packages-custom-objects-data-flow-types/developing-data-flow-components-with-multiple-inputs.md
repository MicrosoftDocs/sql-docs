---
title: "Developing Data Flow Components with Multiple Inputs | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
ms.assetid: 3c7b50e8-2aa6-4f6a-8db4-e8293bc21027
author: janinezhang
ms.author: janinez
manager: craigg
---
# Developing Data Flow Components with Multiple Inputs
  A data flow component with multiple inputs may consume excessive memory if its multiple inputs produce data at uneven rates. When you develop a custom data flow component that supports two or more inputs, you can manage this memory pressure by using the following members in the Microsoft.SqlServer.Dts.Pipeline namespace:  
  
-   The <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.SupportsBackPressure%2A> property of the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute> class. Set the value of this property to `true` if you want to implement the code that is necessary for your custom data flow component to manage data flowing at uneven rates.  
  
-   The <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> class. You must provide an implementation of this method if you set the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.SupportsBackPressure%2A> property to `true`. If you do not provide an implementation, the data flow engine raises an exception at run time.  
  
-   The <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> class. You must also provide an implementation of this method if you set the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.SupportsBackPressure%2A> property to `true` and your custom component supports more than two inputs. If you do not provide an implementation, the data flow engine raises an exception at run time if the user attaches more than two inputs.  
  
 Together, these members enable you to develop a solution for memory pressure that is similar to the solution that Microsoft developed for the Merge and Merge Join transformations.  
  
## Setting the SupportsBackPressure Property  
 The first step in implementing better memory management for a custom data flow component that supports multiple inputs is to set the value of the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.SupportsBackPressure%2A> property to `true` in the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute>. When the value of <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.SupportsBackPressure%2A> is `true`, the data flow engine calls the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method and, when there are more than two inputs, also calls the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> method at run time.  
  
### Example  
 In the following example, the implementation of the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute> sets the value of <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.SupportsBackPressure%2A> to `true`.  
  
```csharp  
[DtsPipelineComponent(ComponentType = ComponentType.Transform,  
        DisplayName = "Shuffler",  
        Description = "Shuffle the rows from input.",  
        SupportsBackPressure = true,  
        LocalizationType = typeof(Localized),  
        IconResource = "Microsoft.Samples.SqlServer.Dts.MIBPComponent.ico")  
]  
public class Shuffler : Microsoft.SqlServer.Dts.Pipeline.PipelineComponent  
        {  
          ...  
        }  
```  
  
## Implementing the IsInputReady Method  
 When you set the value of the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.SupportsBackPressure%2A> property to `true` in the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute> object, you must also provide an implementation for the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> class.  
  
> [!NOTE]  
>  Your implementation of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method should not call the implementations in the base class. The default implementation of this method in the base class simply raises a `NotImplementedException`.  
  
 When you implement this method, you set the status of an element in the Boolean *canProcess* array for each of the component's inputs. (The inputs are identified by their ID values in the *inputIDs* array.) When you set the value of an element in the *canProcess* array to `true` for an input, the data flow engine calls the component's <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> method and provides more data for the specified input.  
  
 While more upstream data is available, the value of the *canProcess* array element for at least one input must always be `true`, or processing stops.  
  
 The data flow engine calls the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method before sending each buffer of data to determine which inputs are waiting to receive more data. When the return value indicates that an input is blocked, the data flow engine temporarily caches additional buffers of data for that input instead of sending them to the component.  
  
> [!NOTE]  
>  You do not call the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> or <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> methods in your own code. The data flow engine calls these methods, and the other methods of the `PipelineComponent` class that you override, when the data flow engine runs your component.  
  
### Example  
 In the following example, the implementation of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method indicates that an input is waiting to receive more data when the following conditions are true:  
  
-   More upstream data is available for the input (`!inputEOR`).  
  
-   The component does not currently have data available to process for the input in the buffers that the component has already received (`inputBuffers[inputIndex].CurrentRow() == null`).  
  
 If an input is waiting to receive more data, the data flow component indicates this by setting to `true` the value of the element in the *canProcess* array that corresponds to that input.  
  
 Conversely, when the component still has data available to process for the input, the example suspends the processing of the input. The example does this by setting to `false` the value of the element in the *canProcess* array that corresponds to that input.  
  
```csharp  
public override void IsInputReady(int[] inputIDs, ref bool[] canProcess)  
{  
    for (int i = 0; i < inputIDs.Length; i++)  
    {  
        int inputIndex = ComponentMetaData.InputCollection.GetObjectIndexByID(inputIDs[i]);  
  
        canProcess[i] = (inputBuffers[inputIndex].CurrentRow() == null)  
            && !inputEOR[inputIndex];  
    }  
}  
```  
  
 The preceding example uses the Boolean `inputEOR` array to indicate whether more upstream data is available for each input. `EOR` in the name of the array represents "end of rowset" and refers to the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineBuffer.EndOfRowset%2A> property of data flow buffers. In a portion of the example that is not included here, the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A> method checks the value of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineBuffer.EndOfRowset%2A> property for each buffer of data that it receives. When a value of `true` indicates that there is no more upstream data available for an input, the example sets the value of the `inputEOR` array element for that input to `true`. This example of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method sets the value of the corresponding element in the *canProcess* array to `false` for an input when the value of the `inputEOR` array element indicates that there is no more upstream data available for the input.  
  
## Implementing the GetDependentInputs Method  
 When your custom data flow component supports more than two inputs, you must also provide an implementation for the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> method of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> class.  
  
> [!NOTE]  
>  Your implementation of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> method should not call the implementations in the base class. The default implementation of this method in the base class simply raises a `NotImplementedException`.  
  
 The data flow engine only calls the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> method when the user attaches more than two inputs to the component. When a component has only two inputs, and the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method indicates that one input is blocked (*canProcess* = `false`), the data flow engine knows that the other input is waiting to receive more data. However, when there are more than two inputs, and the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> method indicates that one input is blocked, the additional code in the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> identifies which inputs are waiting to receive more data.  
  
> [!NOTE]  
>  You do not call the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.IsInputReady%2A> or <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> methods in your own code. The data flow engine calls these methods, and the other methods of the `PipelineComponent` class that you override, when the data flow engine runs your component.  
  
### Example  
 For a specific input that is blocked, the following implementation of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> method returns a collection of the inputs that are waiting to receive more data, and are therefore blocking the specified input. The component identifies the blocking inputs by checking for inputs other than the blocked input that do not currently have data available to process in the buffers that the component has already received (`inputBuffers[i].CurrentRow() == null`). The <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.GetDependentInputs%2A> method then returns the collection of blocking inputs as a collection of input IDs.  
  
```csharp  
public override Collection<int> GetDependentInputs(int blockedInputID)  
{  
    Collection<int> currentDependencies = new Collection<int>();  
    for (int i = 0; i < ComponentMetaData.InputCollection.Count; i++)  
    {  
        if (ComponentMetaData.InputCollection[i].ID != blockedInputID  
            && inputBuffers[i].CurrentRow() == null)  
        {  
            currentDependencies.Add(ComponentMetaData.InputCollection[i].ID);  
        }  
    }  
  
    return currentDependencies;  
}  
```  
  
  
