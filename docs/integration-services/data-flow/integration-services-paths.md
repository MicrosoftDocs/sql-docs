---
title: "Integration Services Paths | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.patheditor.general.f1"
  - "sql13.dts.designer.patheditor.metadata.f1"
  - "sql13.dts.designer.patheditor.visualizers.f1"
helpviewer_keywords: 
  - "paths [Integration Services], about paths"
  - "data flow [Integration Services], paths"
  - "paths [Integration Services]"
  - "destinations [Integration Services], paths"
  - "sources [Integration Services], paths"
ms.assetid: 6c4629a9-2ede-4011-9101-3b342249640e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Integration Services Paths
  A path connects two components in a data flow by connecting the output of one data flow component to the input of another component. A path has a source and a destination. For example, if a path connects an OLE DB source and a Sort transformation, the OLE DB source is the source of the path, and the Sort transformation is the destination of the path. The source is the component where the path starts, and the destination is the component where the path ends.  
  
 If you run a package in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you can view the data in a data flow by attaching data viewers to a path. A data viewer can be configured to display data in a grid. A data viewer is a useful debugging tool. For more information, see [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md).  
  
## Configure the path  
 The [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides the **Data Flow Path Editor** dialog box for setting path properties, viewing the metadata of the data columns that pass through the path, and configuring data viewers.  
  
 The configurable path properties include the name, description, and annotation of the path. You can also configure paths programmatically. For more information, see [Connecting Data Flow Components Programmatically](../../integration-services/building-packages-programmatically/connecting-data-flow-components-programmatically.md).  
  
 A path annotation displays the name of the path source or the path name on the design surface of the **Data Flow** tab in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. Path annotations are similar to the annotations you can add to data flows, control flows, and event handlers. The only difference is that a path annotation is attached to a path, whereas other annotations appear on the **Data Flow**, **Control Flow**, and **Event Handle**r tabs of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 The metadata shows the name, data type, precision, scale, length, code page, and source component of each column in the output of the previous component. The source component is the data flow component that created the column. This may or may not be the first component in the data flow. For example, the Union All and Sort transformations create their own columns, and they are the sources of their output columns. In contrast, a Copy Column transformation can pass through columns without changing them or can create new columns by copying input columns. The Copy Column transformation is the source component only of the new columns.  

## Set the properties of a path with the Data Flow Path Editor
Paths connect two data flow components. Before you can set path properties, the data flow must contain at least two connected data flow components.
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then double-click a path.  
  
4.  In **Data Flow Path Editor**, click **General**. You can then edit the default name of the path and provide a description of the path. You can also modify the PathAnnotation property.  
  
5.  Click **OK**.  
  
6.  To save the updated package, click **Save Selected Items** on the **File** menu.  

## General Page - Data Flow Path Editor
Use the **Data Flow Path Editor** dialog box to set path properties, view column metadata, and manage the data viewers attached to the path.  
  
 Use the **General** node of the **Data Flow Path Editor** dialog box to name and describe the path, and to specify the options for path annotation.  
  
### Options  
 **Name**  
 Provide a unique name for the path.  
  
 **ID**  
 The lineage identifier of the path. This property is read-only.  
  
 **IdentificationString**  
 The string that identifies the path. Automatically generated from the name entered above.  
  
 **Description**  
 Describe the path.  
  
 **PathAnnotation**  
 Specify the type of annotation to use. Choose **Never** to disable annotations, **AsNeeded** to enable annotation on demand, **SourceName** to automatically annotate using the value of the **SourceName** option, and **PathName** to automatically annotate using the value of the **Name** property.  
  
 **DestinationName**  
 Displays the input that is the end of the path.  
  
 **SourceName**  
 Displays the output that is the start of the path.  
 
## Metadata Page - Data Flow Path Editor
Use the **Metadata** page of the **Data Flow Path Editor** dialog box to view the metadata of the path columns.  
  
### Options  
 **Path metadata**  
 Lists column metadata. Click the column headings to sort column data.  
  
 **Name**  
 Lists the column name.  
  
 **Data Type**  
 Lists the data type of the column.  
  
 **Precision**  
 Lists the number of digits in a numeric value.  
  
 **Scale**  
 List the number of digits to the right of the decimal point in a numeric value.  
  
 **Length**  
 Lists the current length of the column.  
  
 **Code Page**  
 Lists the code page of the column. The value **0** indicates that the column does not use a code page. This occurs when data is in Unicode, or has a numeric, date, or time data type.  
  
 **Sort Key Position**  
 Lists the sort key position of the column. The value **0** indicates the column is not sorted.  
  
> [!NOTE]  
>  A minus (-) prefix indicates the column is sorted in descending order.  
  
 **Comparison Flags**  
 Lists the comparison flags that apply to the column.  
  
 **Source Component**  
 Lists the data flow component that is the source of the column.  
  
 **Copy to Clipboard**  
 Copy the column metadata to the clipboard. By default, all metadata rows are copied as sorted in the order currently displayed.  
 
## Data Viewers Page - Data Flow Path Editor
Use the **Data Viewers** page of the **Data Flow Path Editor** dialog box to manage the data viewers that are attached to the path.  
  
### Options  
 **Name**  
 Lists the data viewers.  
  
 **Data Viewer Type**  
 Lists the type of data viewer.  
  
 **Add**  
 Click to add a data viewer by using the **Configure Data Viewer** dialog box.  
  
 **Delete**  
 Click to delete the selected data viewer.  
  
 **Configure**  
 Click to configure a selected data viewer by using the **Configure Data Viewer** dialog box.  
 
## Path properties
The data flow objects in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model have common properties and custom properties at the level of the component, inputs and outputs, and input columns and output columns. Many properties have read-only values that are assigned at run time by the data flow engine.  
  
 This topic lists and describes the custom properties of the paths that connect data flow objects.  
  
### Custom properties of a path  
 In the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model, a path that connects components in the data flow implements the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPath100> interface.  
  
 The following table describes the configurable properties of the paths in a data flow. The data flow engine also assigns values to additional read-only properties that are not listed here.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|PathAnnotation|Integer (enumeration)|A value that indicates whether an annotation should be displayed with the path on the designer surface. The possible values are **AsNeeded**, **SourceName**, **PathName**, and **Never**. The default value is **AsNeeded**.|  
|DestinationName|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100>|The input associated with the path.|  
|SourceName|<xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100>|The output associated with the path.|  
