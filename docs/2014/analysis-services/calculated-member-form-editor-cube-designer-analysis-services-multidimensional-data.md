---
title: "Calculated Member Form Editor (Calculations Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.calculationexpression.calculatedmember.f1"
ms.assetid: f7719b9e-b1e6-4792-90a6-30d9d8eb1196
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculated Member Form Editor (Calculations Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Calculated Member Form Editor** pane on the **Calculations** tab in Cube Designer to create or modify a calculated member.  
  
 **Note** This pane is displayed only in form view.  
  
## Options  
 **Name**  
 Type the name of the calculated member.  
  
 **Parent Properties**  
 Expand to view the **Parent hierarchy**, **Parent member**, and **Change** options.  
  
 **Parent hierarchy**  
 Select the dimension and hierarchy in the selected cube that is to include the calculated member. Select MEASURES to define a calculated measure.  
  
 **Parent member**  
 Select the member beneath which the calculated member is to appear.  
  
 **Note** This option is available if **Parent hierarchy** specifies a hierarchy other than MEASURES.  
  
 **Change**  
 Select to display the **Select Parent Member** dialog box and choose a member for **Parent member**. For more information about the **Select Parent Member** dialog box, see [Select Parent Member Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](select-parent-member-dialog-box-analysis-services-multidimensional-data.md).  
  
 **Expression**  
 Expand to view or edit the Multidimensional Expressions (MDX) expression for the calculated member.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
> [!NOTE]  
>  It is recommended that this expression evaluate to a string or to a numeric value.  
  
 **Additional Properties**  
 Expand to view the **Format string**, **Visible**, **Non-empty behavior**, **Color Expressions**, and **Font Expressions** options.  
  
 **Format string**  
 Type the MDX format string used to format the value returned by the calculated member, or select a predefined format string.  
  
 For more information about MDX format strings, see [FORMAT_STRING Contents &#40;MDX&#41;](multidimensional-models/mdx/mdx-cell-properties-format-string-contents.md).  
  
 **Visible**  
 Select **True** to allow the calculated member to be visible to client applications.  
  
 **Non-empty behavior**  
 Select the name of the measure used to resolve NON EMPTY queries in MDX for the calculated member. If the **Non Empty Behavior** property is blank, the calculated member must be evaluated repeatedly to determine if a member is empty. If the **Non Empty Behavior** property contains the name of a measure, the calculated member is treated as empty if the specified measure is empty.  
  
> [!WARNING]  
>  This property is deprecated. Avoid setting it. See [Deprecated Analysis Services Features in SQL Server 2014](deprecated-analysis-services-features-in-sql-server-2014.md) for details.  
  
 **Color Expressions**  
 Expand to view the **Fore color** and **Back color** options.  
  
 **Fore color**  
 Type the MDX expression that provides the foreground color of the calculated member.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 Click the color selection button to display the **Color** dialog box and insert the RGB (Red-Green-Blue) value for a specified color into the MDX expression. For more information about RGB values, see [FORE_COLOR and BACK_COLOR Contents &#40;MDX&#41;](multidimensional-models/mdx/mdx-cell-properties-fore-color-and-back-color-contents.md).  
  
 **Back color**  
 Type the MDX expression that provides the background color of the calculated member.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 Click the color selection button to display the **Color** dialog box and insert the RGB (Red-Green-Blue) value for a specified color into the MDX expression. For more information about RGB values, see [FORE_COLOR and BACK_COLOR Contents &#40;MDX&#41;](multidimensional-models/mdx/mdx-cell-properties-fore-color-and-back-color-contents.md).  
  
 **Font Expressions**  
 Expand to view the **Font name**, **Font size**, and **Font flags** options.  
  
 **Font name**  
 Type the MDX expression that provides the name of the font used for the calculated member.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 Click the font selection button to display the **Font** dialog box and insert the property values for a specified font into the MDX expression. For more information about property values, see [Creating and Using Property Values &#40;MDX&#41;](creating-and-using-property-values-mdx.md).  
  
 **Font size**  
 Type the MDX expression that provides the size of the font used for the calculated member.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 Click the font selection button to display the **Font** dialog box and insert the property values for a specified font into the MDX expression. For more information about property values, see [Creating and Using Property Values &#40;MDX&#41;](creating-and-using-property-values-mdx.md).  
  
 **Font flags**  
 Type the MDX expression that provides the bitmapped value containing the font flags, such as underline or bold, of the font used for the calculated member.  
  
 Drag selected elements from the **Calculation Tools** pane to this option to include the MDX syntax for the selected element.  
  
 Click the font selection button to display the **Font** dialog box and insert the property values for a specified font into the MDX expression. For more information about property values, see [Creating and Using Property Values &#40;MDX&#41;](creating-and-using-property-values-mdx.md).  
  
## See Also  
 [Calculations](multidimensional-models-olap-logical-cube-objects/calculations.md)   
 [Create Calculated Members](multidimensional-models/create-calculated-members.md)   
 [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](cube-designer-analysis-services-multidimensional-data.md)   
 [Calculations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](calculations-cube-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-calculations-tab-cube-designer-analysis-services-multidimensional-data.md)   
 [Script Organizer &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](script-organizer-cube-designer-analysis-services-multidimensional-data.md)   
 [Calculation Tools &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](calculation-tools-cube-designer-analysis-services-multidimensional-data.md)   
 [Named Set Form Editor &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](named-set-form-editor-cube-designer-analysis-services-multidimensional-data.md)   
 [Script Editor &#40;Calculations Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](script-editor-calculations-cube-designer-analysis-services-multidimensional-data.md)  
  
  
