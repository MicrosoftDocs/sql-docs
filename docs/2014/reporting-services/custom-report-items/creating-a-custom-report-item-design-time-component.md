---
title: "Creating a Custom Report Item Design-Time Component | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "custom report items, creating"
ms.assetid: 323fd58a-a462-4c48-b188-77ebc0b4212e
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Creating a Custom Report Item Design-Time Component
  A custom report item design-time component is a control that can be used in the Visual Studio Report Designer environment. The custom report item design-time component provides an activated design surface that can accept drag-and-drop operations, integration with the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] property browser, and the ability to provide custom property editors.  
  
 With a custom report item design-time component, the user can position a custom report item on a report in the design environment, set custom data properties on the custom report item, and then save the custom report item as part of the report project.  
  
 The properties that are set using the design-time component in the development environment are serialized and deserialized by the host design environment and then stored as elements in the Report Definition Language (RDL) file. When the report is executed by the report processor, the properties that are set using the design-time component are passed by the report processor to a custom report item run-time component, which renders the custom report item and passes it back to the report processor.  
  
> [!NOTE]  
>  The custom report item design-time component is implemented as a [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] component. This document will describe implementation details specific to the custom report item design-time component. For more information about developing components using the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], see [Components in Visual Studio](https://go.microsoft.com/fwlink/?LinkId=116576) in the MSDN library.  
  
 For a sample of a fully implemented custom report item, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Implementing a Design-Time Component  
 The main class of a custom report item design-time component is inherited from the `Microsoft.ReportDesigner.CustomReportItemDesigner` class. In addition to the standard attributes used for a [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] control, your component class should define a `CustomReportItem` attribute. This attribute must correspond to the name of the custom report item as it is defined in the reportserver.config file. For a list of [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] attributes, see Attributes in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK documentation.  
  
 The following code example shows attributes being applied to a custom report item design-time control:  
  
```csharp  
namespace PolygonsCRI  
{  
    [LocalizedName("Polygons")]  
    [Editor(typeof(CustomEditor), typeof(ComponentEditor))]  
        [ToolboxBitmap(typeof(PolygonsDesigner),"Polygons.ico")]  
        [CustomReportItem("Polygons")]  
  
    public class PolygonsDesigner : CustomReportItemDesigner  
    {  
...  
```  
  
### Initializing the Component  
 You pass user-specified properties for a custom report item using a <xref:Microsoft.ReportingServices.RdlObjectModel.CustomData> class. Your implementation of the `CustomReportItemDesigner` class should override the `InitializeNewComponent` method to create a new instance of your component's <xref:Microsoft.ReportingServices.RdlObjectModel.CustomData> class and set it to default values.  
  
 The following code example shows an example of a custom report item design-time component class overriding the `CustomReportItemDesigner.InitializeNewComponent` method to initialize the component's <xref:Microsoft.ReportingServices.RdlObjectModel.CustomData> class:  
  
```csharp  
public override void InitializeNewComponent()  
        {  
            CustomData = new CustomData();  
            CustomData.DataRowHierarchy = new DataHierarchy();  
  
            // Shape grouping  
            CustomData.DataRowHierarchy.DataMembers.Add(new DataMember());  
            CustomData.DataRowHierarchy.DataMembers[0].Group = new Group();  
            CustomData.DataRowHierarchy.DataMembers[0].Group.Name = Name + "_Shape";  
            CustomData.DataRowHierarchy.DataMembers[0].Group.GroupExpressions.Add(new ReportExpression());  
  
            // Point grouping  
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers.Add(new DataMember());  
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group = new Group();  
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group.Name = Name + "_Point";  
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group.GroupExpressions.Add(new ReportExpression());  
  
            // Static column  
            CustomData.DataColumnHierarchy = new DataHierarchy();  
            CustomData.DataColumnHierarchy.DataMembers.Add(new DataMember());  
  
            // Points  
            IList<IList<DataValue>> dataValues = new List<IList<DataValue>>();  
            CustomData.DataRows.Add(dataValues);  
            CustomData.DataRows[0].Add(new List<DataValue>());  
            CustomData.DataRows[0][0].Add(NewDataValue("X", ""));  
            CustomData.DataRows[0][0].Add(NewDataValue("Y", ""));  
        }  
```  
  
### Modifying Component Properties  
 You can modify `CustomData` properties in the design environment in several ways. You can modify any properties that are exposed by the design-time component that are marked with the <xref:System.ComponentModel.BrowsableAttribute> attribute by using the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] property browser. In addition, you can modify properties by dragging items onto the custom report item's design surface, or by right-clicking the control in the design environment and selecting **Properties** on the shortcut menu to display a custom properties window.  
  
 The following code example shows a `Microsoft.ReportDesigner.CustomReportItemDesigner.CustomData` property that has the <xref:System.ComponentModel.BrowsableAttribute> attribute applied:  
  
```csharp  
[Browsable(true), Category("Data")]  
public string DataSetName  
{  
      get  
      {  
         return CustomData.DataSetName;  
      }  
      set  
      {  
         CustomData.DataSetName = value;  
      }  
   }  
  
```  
  
 You can provide your design-time component with a custom properties editor dialog box. The custom property editor implementation should inherit from the <xref:System.ComponentModel.ComponentEditor> class, and it should create an instance of a dialog box that can be used for property editing.  
  
 The following example shows an implementation of a class that inherits from <xref:System.ComponentModel.ComponentEditor> and displays a custom property editor dialog box:  
  
```csharp  
internal sealed class CustomEditor : ComponentEditor  
{  
   public override bool EditComponent(  
      ITypeDescriptorContext context, object component)  
    {  
     PolygonsDesigner designer = (PolygonsDesigner)component;  
     PolygonProperties dialog = new PolygonProperties();  
     dialog.m_designerComponent = designer;  
     DialogResult result = dialog.ShowDialog();  
     if (result == DialogResult.OK)  
     {  
        designer.Invalidate();  
        designer.ChangeService().OnComponentChanged(designer, null, null, null);  
        return true;  
     }  
     else  
        return false;  
    }  
}  
```  
  
 Your custom property editor dialog box can invoke the Report Designer Expression Editor. In the following example, the Expression Editor is invoked when the user selects the first element in the combo box:  
  
```csharp  
private void EditableCombo_SelectedIndexChanged(object sender,   
    EventArgs e)  
{  
   ComboBox combo = (ComboBox)sender;  
   if (combo.SelectedIndex == 0 && m_launchEditor)  
   {  
      m_launchEditor = false;  
      ExpressionEditor editor = new ExpressionEditor();  
      string newValue;  
      newValue = (string)editor.EditValue(null, m_designerComponent.Site, m_oldComboValue);  
      combo.Items[0] = newValue;  
   }  
}  
  
```  
  
### Using Designer Verbs  
 A designer verb is a menu command linked to an event handler. You can add designer verbs that will appear in a component's shortcut menu when your custom report item run-time control is being used in the design environment. You can return the list of available designer verbs from your run-time component by using the `Verbs` property.  
  
 The following code example shows a designer verb and an event handler being added to the <xref:System.ComponentModel.Design.DesignerVerbCollection>, as well as the event handler code:  
  
```csharp  
public override DesignerVerbCollection Verbs  
{  
    get  
    {  
        if (m_verbs == null)  
        {  
            m_verbs = new DesignerVerbCollection();  
            m_verbs.Add(new DesignerVerb("Proportional Scaling", new EventHandler(OnProportionalScaling)));  
         m_verbs[0].Checked = (GetCustomProperty("poly:Proportional") == bool.TrueString);  
        }  
  
        return m_verbs;  
    }  
}  
  
private void OnProportionalScaling(object sender, EventArgs e)  
{  
   bool proportional = !  
        (GetCustomProperty("poly:Proportional") == bool.TrueString);  
   m_verbs[0].Checked = proportional;  
   SetCustomProperty("poly:Proportional", proportional.ToString());  
   ChangeService().OnComponentChanged(this, null, null, null);  
   Invalidate();  
}  
```  
  
### Using Adornments  
 Custom report item classes can also implement a `Microsoft.ReportDesigner.Design.Adornment` class. An adornment allows the custom report item control to provide areas outside the main rectangle of the design surface. These areas can handle user interface events, such as mouse clicks and drag-and-drop operations. The `Adornment` class that is defined in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] `Microsoft.ReportDesigner` namespace is a pass-through implementation of the <xref:System.Windows.Forms.Design.Behavior.Adorner> class found in Windows Forms. For complete documentation on the `Adorner` class, see [Behavior Service Overview](https://go.microsoft.com/fwlink/?LinkId=116673) in the MSDN library. For sample code that implements a `Microsoft.ReportDesigner.Design.Adornment` class, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
 For more information about programming and using Windows Forms in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], see these topics in the MSDN Library:  
  
-   Design-Time Attributes for Components  
  
-   Components in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)]  
  
-   Walkthrough: Creating a Windows Forms Control that Takes Advantage of Visual Studio Design-Time Features  
  
## See Also  
 [Custom Report Item Architecture](custom-report-item-architecture.md)   
 [Creating a Custom Report Item Run-Time Component](creating-a-custom-report-item-run-time-component.md)   
 [Custom Report Item Class Libraries](custom-report-item-class-libraries.md)   
 [How to: Deploy a Custom Report Item](how-to-deploy-a-custom-report-item.md)  
  
  
