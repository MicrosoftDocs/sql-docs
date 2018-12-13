---
title: "Custom Report Item Class Libraries | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "custom report items, RDL"
  - "RDL [Reporting Services], custom report items"
ms.assetid: f18c5d8f-1d6b-4f0b-8657-c14896c2ce0d
author: markingmyname
ms.author: maghan
manager: craigg
---
# Custom Report Item Class Libraries
  Custom report items use classes from the `Microsoft.ReportDesigner` namespace. The classes used to implement a custom report item can be grouped into two main categories: unique classes designed to support custom report item infrastructure, and managed wrapper classes that encapsulate the functionality of relevant Report Definition Language (RDL) elements. For a code sample on how to use these classes, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Custom Report Item Infrastructure Classes  
 The following classes are used to implement a custom report item.  
  
> [!NOTE]  
>  The following tables are not complete listings; they include only the most commonly used properties and methods for each class.  
  
### Microsoft.ReportDesigner.CustomReportItemDesigner  
 This is the main custom report item class. The main class of your custom report item implementation must inherit from this class.  
  
#### Public Properties  
  
|||  
|-|-|  
|`Name`|The name of the custom report item.|  
|`Type`|The type of the custom report item.|  
|`CustomData`|A <xref:Microsoft.ReportingServices.RdlObjectModel.CustomData> object that encapsulates the custom report item data properties specified at design time.|  
|`CustomProperties`|A collection of custom properties for the custom report item.|  
|`Height`|The height of the custom report item control.|  
|`Width`|The width of the custom report item control.|  
|`Report`|A container for the report-level properties, such as the list of datasets in the report.|  
|`AltReportItem`|The alternate report item object, to be used where the custom report item run-time control is not supported.|  
|`Style`|The style properties for the custom report item.|  
|`Adornment`|An adornment window used for interactive editing of the control.|  
|`Site`|The `ISite` of the component.|  
|`DesignerVerbCollection`|An array of custom verbs for the control's shortcut menu.|  
  
#### Public Methods  
  
|||  
|-|-|  
|`BeginEdit`|Activates interactive editing for the control.|  
|`DoDefaultAction`|Called in response to double-clicking or pressing Return on the control.|  
|`EndEdit`|Deactivates interactive editing for the control.|  
|`GetService`|Returns an object which represents a service.|  
|`InitializeNewComponent`|Called when a new custom report item is created.|  
|`Invalidate`|Repaints the entire surface of the control.|  
|`OnDragEnter`<br /><br /> `OnDragDrop`|Called when an object is dragged onto the control.|  
|`OnPaint`|Called in response to the `Paint` event.|  
  
### Microsoft.ReportDesigner.CustomReportItemAttribute  
 This is the attribute used to identify the type of the custom report item. The name must match the value of the <`Name`> attribute of the `ReportItem` element in the Report Designer configuration file.  
  
#### Public Methods  
  
|||  
|-|-|  
|`CustomReportItemAttribute`|Constructs the CustomReportItemAttribute object.|  
  
### Microsoft.ReportDesigner.LocalizedNameAttribute  
 This is the attribute used to specify display name to use for the custom report item designer.  
  
#### Public Methods  
  
|||  
|-|-|  
|`LocalizedNameAttribute`|Constructs the LocalizedNameAttribute object.|  
  
### Microsoft.ReportDesigner.Adornment  
 The `Adornment` class is used by the custom report item design-time component to provide areas outside of the main rectangle of the design surface. These areas can handle user interface events, such as mouse clicks and drag-and-drop operations.  
  
#### Public Methods  
  
|||  
|-|-|  
|`OnShow`|Called when the `Adornment` is activated.|  
|`OnHide`|Called when the `Adornment` is deactivated.|  
|`Paint`|Called in response to the `Paint` event.|  
|`OnDragEnter`<br /><br /> `OnDragOver`<br /><br /> `OnDragLeave`<br /><br /> `OnDragDrop`|Called when an object is dragged into the `Adornment`.|  
  
### Microsoft.ReportDesigner.AdornerService  
 This class is used to provide a collection of display services used by the custom report item to support `Adornment` objects for the custom report item design-time component.  
  
#### Public Properties  
  
|||  
|-|-|  
|`AdornerWindowBounds`|The bounds of the Adorner window.|  
|`AdornerWindowRegion`|The region of the Adorner window.|  
|`AdornerWindowGraphics`|A graphics context for the Adorner window.|  
  
#### Public Methods  
  
|||  
|-|-|  
|`ComponentRectInDesignerFrame`|Returns the bounds of the component translated into designer frame coordinates.|  
|`InvalidateAdorner`|Invalidates the Adorner window.|  
|`PointToAdorner`|Returns a point in screen coordinates translated to Adorner window coordinates.|  
  
### Microsoft.ReportDesigner.ExpressionEditor  
 This class can be used from your custom report item design-time control to invoke the Expression Editor.  
  
#### Public Methods  
  
|||  
|-|-|  
|`EditValue`|Invokes the Expression Editor, initialized with the given object value.|  
  
### Microsoft.ReportDesigner.IFieldsDataObject  
 This class is a collection of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] fields, and is used to support drag-and-drop events in the design environment. Inherits from `IReportItemDataObject`.  
  
#### Public Properties  
  
|||  
|-|-|  
|`DataSetName`|The name of the dataset containing the fields to be dropped.|  
|`Fields`|The collection of fields (`Microsoft.ReportDesigner.Field`) to be dropped.|  
  
## See Also  
 [Report Definition Language &#40;SSRS&#41;](../reports/report-definition-language-ssrs.md)   
 [Creating a Custom Report Item Run-Time Component](creating-a-custom-report-item-run-time-component.md)   
 [Creating a Custom Report Item Design-Time Component](creating-a-custom-report-item-design-time-component.md)  
  
  
