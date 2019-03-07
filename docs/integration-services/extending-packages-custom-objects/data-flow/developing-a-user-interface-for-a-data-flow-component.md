---
title: "Developing a User Interface for a Data Flow Component | Microsoft Docs"
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
  - "data flow components [Integration Services], custom editors"
  - "user interfaces [Integration Services]"
  - "custom data flow components [Integration Services], custom editors"
  - "custom component editors [Integration Services]"
  - "IDtsComponentUI interface"
  - "UITypeName property"
  - "custom user interface [Integration Services], custom data flow component"
  - "editors [Integration Services]"
ms.assetid: 10b829a1-609b-42e3-9070-cfe5a2bb698c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Developing a User Interface for a Data Flow Component
  Component developers can provide a custom user interface for a component, which is displayed in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)] when the component is edited. Implementing a custom user interface provides you with notification when the component is added to or deleted from a data flow task, and when help is requested for the component.  
  
 If you do not provide a custom user interface for your component, users can still configure the component and its custom properties by using the Advanced Editor. You can ensure that the Advanced Editor allows users to edit custom property values appropriately by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100.TypeConverter%2A> and <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100.UITypeEditor%2A> properties of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100> when appropriate. For more information, see "Creating Custom Properties" in [Design-time Methods of a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/design-time-methods-of-a-data-flow-component.md).  
  
## Setting the UITypeName Property  
 To provide a custom user interface, the developer must set the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.UITypeName%2A> property of the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute> to the name of a class that implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI> interface. When this property is set by the component, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] loads and calls the custom user interface when the component is edited in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
 The <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.UITypeName%2A> property is a comma-delimited string that identifies the fully qualified name of the type. The following list shows, in order, the elements that identify the type:  
  
-   Type name  
  
-   Assembly name  
  
-   File version  
  
-   Culture  
  
-   Public key token  
  
 The following code example shows a class that derives from the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent> base class and specifies the <xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute.UITypeName%2A> property.  
  
```csharp  
[DtsPipelineComponent(  
DisplayName="SampleComponent",  
UITypeName="MyNamespace.MyComponentUIClassName,MyAssemblyName,Version=1.0.0.0,Culture=neutral,PublicKeyToken=abcd...",  
ComponentType = ComponentType.Transform)]  
public class SampleComponent : PipelineComponent  
{  
//TODO: Implement the component here.  
}  
```  
  
```vb  
<DtsPipelineComponent(DisplayName="SampleComponent", _  
UITypeName="MyNamespace.MyComponentUIClassName,MyAssemblyName,Version=1.0.0.0,Culture=neutral,PublicKeyToken=abcd...", ComponentType=ComponentType.Transform)> _   
Public Class SampleComponent   
 Inherits PipelineComponent   
End Class  
```  
  
## Implementing the IDtsComponentUI Interface  
 The <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI> interface contains methods that [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer calls when a component is added, deleted, and edited. Component developers can provide code in their implementation of these methods to interact with users of the component.  
  
 This class is typically implemented in an assembly separate from the component itself. Although use of a separate assembly is not required, this lets the developer build and deploy the component and the user interface independently of each other, and keeps the binary footprint of the component small.  
  
 Implementing a custom user interface gives the component developer more control over the component as it is edited in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer. For example, a component can add code to the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.New%2A> method, which is called when a component is initially added to a data flow task, and display a wizard that guides the user through the initial configuration of the component.  
  
 After you have created a class that implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI> interface, you must add code to respond to user interaction with the component. The <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.Initialize%2A> method provides the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface of the component, and is called before the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.New%2A> and <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.Edit%2A> methods. This reference should be stored in a private member variable and used to modify the component's metadata thereafter.  
  
## Modifying a Component and Persisting Changes  
 The <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface is provided as a parameter to the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.Initialize%2A> method. This reference should be cached in a member variable by the user interface code, and then used to modify the component in response to user interaction with the user interface.  
  
 Although you can modify the component directly through the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface, it is better to create an instance of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.CManagedComponentWrapper> by using the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100.Instantiate%2A> method. When you edit the component directly by using the interface, you bypass the component's validation safeguards. The advantage of using the design-time instance of the component through the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.CManagedComponentWrapper> is that you ensure that the component has control over the changes made to it.  
  
 The return value of the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.Edit%2A> method determines whether changes made to a component are persisted or discarded. When this method returns **false**, all changes are discarded; **true** persists the changes to the component and marks the package as needing to be saved.  
  
### Using the Services of the SSIS Designer  
 The **IServiceProvider** parameter of the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.Initialize%2A> method provides access to the following services of [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer:  
  
|Service|Description|  
|-------------|-----------------|  
|<xref:Microsoft.SqlServer.Dts.Design.IDtsClipboardService>|Used to determine whether the component was generated as part of a copy/paste or cut/paste operation.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsConnectionService>|Used to access existing connections or to create new connections in the package.|  
|<xref:Microsoft.SqlServer.Dts.Design.IErrorCollectionService>|Used to capture events from data flow components when you need to capture all the errors and warnings raised by the component instead of receiving only the last error or warning.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsVariableService>|Used to access existing variables or to create new variables in the package.|  
|<xref:Microsoft.SqlServer.Dts.Design.IDtsPipelineEnvironmentService>|Used by data flow components to access the parent Data Flow task and other components in the data flow. This feature could be used to develop a component like the Slowly Changing Dimension Wizard that creates and connects additional data flow components as needed.|  
  
 These services provide component developers the ability to access and create objects in the package in which the component is loaded.  
  
## Sample  
 The following code example shows the integration of a custom user interface class that implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI> interface, and a Windows form that serves as the editor for a component.  
  
### Custom User Interface Class  
 The following code shows the class that implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI> interface. The <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.Edit%2A> method creates the component editor and then displays the form. The return value of the form determines whether changes to the component are persisted.  
  
```csharp  
using System;  
using System.Windows.Forms;  
using Microsoft.SqlServer.Dts.Runtime;  
using Microsoft.SqlServer.Dts.Pipeline.Design;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
  
namespace Microsoft.Samples.SqlServer.Dts  
{  
    public class SampleComponentUI : IDtsComponentUI  
    {  
        IDTSComponentMetaData100 md;  
        IServiceProvider sp;  
  
        public void Help(System.Windows.Forms.IWin32Window parentWindow)  
        {  
        }  
        public void New(System.Windows.Forms.IWin32Window parentWindow)  
        {  
        }  
        public void Delete(System.Windows.Forms.IWin32Window parentWindow)  
        {  
        }  
        public bool Edit(System.Windows.Forms.IWin32Window parentWindow, Variables vars, Connections cons)  
        {  
            // Create and display the form for the user interface.  
            SampleComponentUIForm componentEditor = new SampleComponentUIForm(cons, vars, md);  
  
            DialogResult result  = componentEditor.ShowDialog(parentWindow);  
  
            if (result == DialogResult.OK)  
                return true;  
  
            return false;  
        }  
        public void Initialize(IDTSComponentMetaData100 dtsComponentMetadata, IServiceProvider serviceProvider)  
        {  
            // Store the component metadata.  
            this.md = dtsComponentMetadata;  
        }  
    }  
}  
```  
  
```vb  
Imports System   
Imports System.Windows.Forms   
Imports Microsoft.SqlServer.Dts.Runtime   
Imports Microsoft.SqlServer.Dts.Pipeline.Design   
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper   
  
Namespace Microsoft.Samples.SqlServer.Dts   
  
 Public Class SampleComponentUI   
 Implements IDtsComponentUI   
   Private md As IDTSComponentMetaData100   
   Private sp As IServiceProvider   
  
   Public Sub Help(ByVal parentWindow As System.Windows.Forms.IWin32Window)   
   End Sub   
  
   Public Sub New(ByVal parentWindow As System.Windows.Forms.IWin32Window)   
   End Sub   
  
   Public Sub Delete(ByVal parentWindow As System.Windows.Forms.IWin32Window)   
   End Sub   
  
   Public Function Edit(ByVal parentWindow As System.Windows.Forms.IWin32Window, ByVal vars As Variables, ByVal cons As Connections) As Boolean   
     ' Create and display the form for the user interface.  
     Dim componentEditor As SampleComponentUIForm = New SampleComponentUIForm(cons, vars, md)   
     Dim result As DialogResult = componentEditor.ShowDialog(parentWindow)   
     If result = DialogResult.OK Then   
       Return True   
     End If   
     Return False   
   End Function   
  
   Public Sub Initialize(ByVal dtsComponentMetadata As IDTSComponentMetaData100, ByVal serviceProvider As IServiceProvider)   
     Me.md = dtsComponentMetadata   
   End Sub   
 End Class   
  
End Namespace  
```  
  
### Custom Editor  
 The following code shows the implementation of the Windows form that is shown during the call to the <xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI.Edit%2A> method.  
  
```csharp  
using System;  
using System.Drawing;  
using System.Collections;  
using System.ComponentModel;  
using System.Windows.Forms;  
using System.Data;  
  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.Samples.SqlServer.Dts  
{  
    public partial class SampleComponentUIForm : System.Windows.Forms.Form  
    {  
        private Connections connections;  
        private Variables variables;  
        private IDTSComponentMetaData100 metaData;  
        private CManagedComponentWrapper designTimeInstance;  
        private System.ComponentModel.IContainer components = null;  
  
        public SampleComponentUIForm( Connections cons, Variables vars, IDTSComponentMetaData100 md)  
        {  
            variables = vars;  
            connections = cons;  
            metaData = md;  
        }  
  
        private void btnOk_Click(object sender, System.EventArgs e)  
        {  
            if (designTimeInstance == null)  
                designTimeInstance = metaData.Instantiate();  
  
            designTimeInstance.SetComponentProperty( "CustomProperty", txtCustomPropertyValue.Text);  
  
            this.Close();  
        }  
  
        private void btnCancel_Click(object sender, System.EventArgs e)  
        {  
            this.Close();  
        }  
    }  
}  
```  
  
```vb  
Imports System   
Imports System.Drawing   
Imports System.Collections   
Imports System.ComponentModel   
Imports System.Windows.Forms   
Imports System.Data   
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper   
Imports Microsoft.SqlServer.Dts.Runtime   
  
Namespace Microsoft.Samples.SqlServer.Dts   
  
 Public Partial Class SampleComponentUIForm   
  Inherits System.Windows.Forms.Form   
   Private connections As Connections   
   Private variables As Variables   
   Private metaData As IDTSComponentMetaData100   
   Private designTimeInstance As CManagedComponentWrapper   
   Private components As System.ComponentModel.IContainer = Nothing   
  
   Public Sub New(ByVal cons As Connections, ByVal vars As Variables, ByVal md As IDTSComponentMetaData100)   
     variables = vars   
     connections = cons   
     metaData = md   
   End Sub   
  
   Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs)   
     If designTimeInstance Is Nothing Then   
       designTimeInstance = metaData.Instantiate   
     End If   
     designTimeInstance.SetComponentProperty("CustomProperty", txtCustomPropertyValue.Text)   
     Me.Close   
   End Sub   
  
   Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)   
     Me.Close   
   End Sub   
 End Class   
  
End Namespace  
```
  
## See Also  
 [Creating a Custom Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/creating-a-custom-data-flow-component.md)  
  
  
