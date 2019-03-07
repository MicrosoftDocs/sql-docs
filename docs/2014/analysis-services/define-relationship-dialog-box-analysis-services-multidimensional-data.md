---
title: "Define Relationship Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.dimensionusage.definerelationship.f1"
helpviewer_keywords: 
  - "Define Relationship dialog box"
ms.assetid: 0fcee7f1-f138-4c2e-ae8c-245395ee0fe8
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Relationship Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Define Relationship** dialog box to define a relationship between a cube dimension and a measure group in Cube Designer. You can display the **Define Relationship** dialog box by clicking **...** on a cell in the **Grid** pane on the **Dimension Usage** tab in Cube Designer.  
  
## Options  
 **Select relationship type**  
 Select the type of dimension relationship to create between the cube dimension and the measure group. Selecting a dimension relationship type changes the contents of the **Detail** pane.  
  
 If **No Relationship** is chosen, a dimension relationship is not created.  
  
 **Detail**  
 Displays the options available for the dimension relationship type selected in **Select relationship type**.  
  
## Detail Pane Options  
 The following options are displayed in the **Detail** pane, depending on the relationship type selected in **Select relationship type**:  
  
|Relationship type|Description|Option|  
|-----------------------|-----------------|------------|  
|**No Relationship**|No relationship is defined, and no options are displayed in the **Detail** pane.||  
|**Regular**|Specifies a regular dimension relationship. The following options are displayed in the **Detail** pane:|**Granularity attribute**: <br />                      Select the attribute that defines the granularity of the measure group with respect to the dimension. This attribute is usually the key attribute of the dimension.|  
|||**Dimension table** : Displays the main table for the dimension.|  
|||**Measure group table** : Displays the fact table for the measure group.|  
|||**Relationship**: Displays a grid of dimension columns and measure group columns on which the relationship is based. The grid contains the following columns:<br /><br /> **Dimension Columns**: Displays the columns associated with the selected granularity attribute. Note: If the dimension has not yet been generated, this option is set to **Generate**.<br />**Measure Group Columns** :<br />                              Select the columns in the measure group that are related to the dimension columns.|  
|||**Advanced**:<br />                      Click to display the **Measure Group Bindings** dialog box and edit advanced properties, such as null processing, on relationships between attributes and measure groups columns. For more information about the **Measure Group Bindings** dialog box, see [Measure Group Bindings Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](measure-group-bindings-dialog-box-analysis-services-multidimensional-data.md).|  
|**Fact**|Specifies a fact dimension relationship. The following options are displayed in the **Detail** pane:|**Granularity attribute**: Select the attribute that defines the granularity of the measure group with respect to the dimension. This attribute is usually the key attribute of the dimension.|  
|||**Dimension table**: Displays the primary dimension table.|  
|||**Measure group table**: <br />                      Displays the table on which the measure group is based.|  
|**Referenced**|Specifies a referenced dimension relationship. The following options are displayed in the **Detail** pane:|**Reference dimension**: <br />                      Displays the selected dimension.|  
|||**Intermediate dimension**: <br />                      Select the intermediate dimension.|  
|||**Reference dimension attribute**: <br />                      Select the attribute in the reference dimension that is related to the intermediate dimension attribute specified in **Intermediate dimension attribute**.|  
|||**Intermediate dimension attribute**: <br />                      Select the attribute in the intermediate dimension that is related to the attribute in the reference dimension specified in **Reference dimension**.|  
|||**Materialize**: <br />                      Select to store the attribute member in the intermediate dimension that links the attribute in the reference dimension to the fact table in the MOLAP structure. Materializing the relationship is the default behavior to maximize query performance, but at the expense of an increase in processing time and storage space.|  
|**Many-to-Many**|Specifies a many-to-many dimension relationship. The following options are displayed in the **Detail** pane:|**Dimension** : Displays the selected dimension.|  
|||**Intermediate measure group** : <br />                      Select the associated intermediate measure group.<br /><br /> Note: The intermediate measure group must have at least one dimension in common with the selected measure group. Additionally, the granularity of the relationship between the intermediate measure group and the common dimension must be greater than or equal to the granularity between the common dimension and the selected measure group.|  
|**Data Mining**|Specifies a data mining dimension relationship. The following options are displayed in the **Detail** pane:|**Target dimension**: Displays the selected data mining dimension.<br /><br /> Note: You must select a data mining dimension to create a data mining dimension relationship.|  
|||**Source dimension**: Select the dimension on which the data mining dimension provides predictive analysis.|  
  
## See Also  
 [Dimension Relationships](multidimensional-models-olap-logical-cube-objects/dimension-relationships.md)   
 [Dimension Usage &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](dimension-usage-cube-designer-analysis-services-multidimensional-data.md)   
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)  
  
  
