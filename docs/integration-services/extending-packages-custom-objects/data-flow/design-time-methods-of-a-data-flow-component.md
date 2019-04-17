---
title: "Design-time Methods of a Data Flow Component | Microsoft Docs"
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
  - "custom data flow components [Integration Services], method execution sequence"
  - "ProcessInput method"
  - "method execution sequence [Integration Services]"
  - "PrimeOutput method"
  - "data flow components [Integration Services], method execution sequence"
ms.assetid: b5a121a1-b87c-441b-a42c-2cec628dc81c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Design-time Methods of a Data Flow Component
  Before execution, the data flow task is said to be in a design-time state, as it undergoes incremental changes. Changes may include the addition or removal of components, the addition or removal of the path objects that connect components, and changes to the metadata of the components. When metadata changes occur, the component can monitor and react to the changes. For example, a component can disallow certain changes or make additional changes in response to a change. At design time, the designer interacts with a component through the design-time <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSDesigntimeComponent100> interface.  
  
## Design-Time Implementation  
 The design-time interface of a component is described by the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSDesigntimeComponent100> interface. Although you do not explicitly implement this interface, a familiarity with the methods defined in this interface will help you understand which methods of the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> base class correspond to the design-time instance of a component.  
  
 When a component is loaded in the [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], the design-time instance of the component is instantiated and the methods of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSDesigntimeComponent100> interface are called as the component is edited. The implementation of the base class lets you override only those methods that your component requires. In many cases, you may override these methods to prevent inappropriate edits to a component. For example, to prevent users from adding an output to a component, override the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.InsertOutput%2A> method. Otherwise, when the implementation of this method by the base class is called, it adds an output to the component.  
  
 Regardless of the purpose or functionality of your component, you should override the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A>, <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.Validate%2A>, and <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ReinitializeMetaData%2A> methods. For more information about <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.Validate%2A> and <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ReinitializeMetaData%2A>, see [Validating a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/validating-a-data-flow-component.md).  
  
## ProvideComponentProperties Method  
 The initialization of a component occurs in the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A> method. This method is called by [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer when a component is added to the data flow task, and is similar to a class constructor. Component developers should create and initialize their inputs, outputs, and custom properties during this method call. The <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A> method differs from a constructor because it is not called every time that the design-time instance or run-time instance of the component is instantiated.  
  
 The base class implementation of the method adds an input and an output to the component and assigns the ID of the input to the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100.SynchronousInputID%2A> property. However, in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the input and output objects added by the base class are not named. Packages that contain a component with input or output objects whose Name property is not set will not successfully load. Therefore, when you use the base implementation, you must assign values explicitly to the Name property of the default input and output.  
  
```csharp  
public override void ProvideComponentProperties()  
{  
    /// TODO: Reset the component.  
    /// TODO: Add custom properties.  
    /// TODO: Add input objects.  
    /// TODO: Add output objects.  
}  
```  
  
```vb  
Public Overrides Sub ProvideComponentProperties()  
    ' TODO: Reset the component.  
    ' TODO: Add custom properties.  
    ' TODO: Add input objects.  
    ' TODO: Add output objects.  
End Sub  
```  
  
### Creating Custom Properties  
 The call to the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A> method is where component developers should add custom properties (<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100>) to the component. Custom properties do not have a data type property. The data type of a custom property is set by the data type of the value that you assign to its <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100.Value%2A> property. However, after you have assigned an initial value to the custom property, you cannot assign a value with a different data type.  
  
> [!NOTE]  
>  The <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100> interface has limited support for property values of type **Object**. The only object that you can store as the value of a custom property is an array of simple types such as strings or integers.  
  
 You can indicate that your custom property supports property expressions by setting the value of its <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100.ExpressionType%2A> property to **CPET_NOTIFY** from the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSCustomPropertyExpressionType> enumeration, as shown in the following example. You do not have to add any code to handle or to validate the property expression entered by the user. You can set a default value for the property, validate its value, and read and use its value normally.  
  
```csharp  
IDTSCustomProperty100 myCustomProperty;  
...  
myCustomProperty.ExpressionType = DTSCustomPropertyExpressionType.CPET_NOTIFY;  
```  
  
```vb  
Dim myCustomProperty As IDTSCustomProperty100  
...  
myCustomProperty.ExpressionType = DTSCustomPropertyExpressionType.CPET_NOTIFY  
```  
  
 You can limit users to selecting a custom property value from an enumeration by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100.TypeConverter%2A> property, as shown in the following example, which assumes that you have defined a public enumeration named **MyValidValues**.  
  
```csharp  
IDTSCustomProperty100 customProperty = outputColumn.CustomPropertyCollection.New();  
customProperty.Name = "My Custom Property";  
// This line associates the type with the custom property.  
customProperty.TypeConverter = typeof(MyValidValues).AssemblyQualifiedName;  
// Now you can use the enumeration values directly.  
customProperty.Value = MyValidValues.ValueOne;    
```  
  
```vb  
Dim customProperty As IDTSCustomProperty100 = outputColumn.CustomPropertyCollection.New   
customProperty.Name = "My Custom Property"   
' This line associates the type with the custom property.  
customProperty.TypeConverter = GetType(MyValidValues).AssemblyQualifiedName   
' Now you can use the enumeration values directly.  
customProperty.Value = MyValidValues.ValueOne  
```  
  
 For more information, see "Generalized Type Conversion" and "Implementing a Type Converter" in the [MSDN Library](https://go.microsoft.com/fwlink/?LinkId=7022).  
  
 You can specify a custom editor dialog box for the value of your custom property by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100.UITypeEditor%2A> property, as shown in the following example. First, you must create a custom type editor that inherits from **System.Drawing.Design.UITypeEditor**, if you cannot locate an existing UI type editor class that fits your needs.  
  
```csharp  
public class MyCustomTypeEditor : UITypeEditor  
{  
...  
}  
```  
  
```vb  
Public Class MyCustomTypeEditor  
  Inherits UITypeEditor   
  ...  
End Class  
```  
  
 Next, specify this class as the value of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100.UITypeEditor%2A> property of your custom property.  
  
```csharp  
IDTSCustomProperty100 customProperty = outputColumn.CustomPropertyCollection.New();  
customProperty.Name = "My Custom Property";  
// This line associates the editor with the custom property.  
customProperty.UITypeEditor = typeof(MyCustomTypeEditor).AssemblyQualifiedName;  
```  
  
```vb  
Dim customProperty As IDTSCustomProperty100 = outputColumn.CustomPropertyCollection.New   
customProperty.Name = "My Custom Property"   
' This line associates the editor with the custom property.  
customProperty.UITypeEditor = GetType(MyCustomTypeEditor).AssemblyQualifiedName  
```  
  
 For more information, see "Implementing a UI Type Editor" in the [MSDN Library](https://go.microsoft.com/fwlink/?LinkId=7022).  
  
## See Also  
 [Run-time Methods of a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/run-time-methods-of-a-data-flow-component.md)  
  
  
