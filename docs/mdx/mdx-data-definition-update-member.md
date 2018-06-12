---
title: "UPDATE MEMBER Statement (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Data Definition - UPDATE MEMBER


  Updates an existing calculated member.  
  
## Syntax  
  
```  
  
UPDATE MEMBER Cube_Name.Member_Name   
   AS MDX_Expression  
      [,Property_Name = Property_Value, ...n]  
......[,SCOPE_ISOLATION = CUBE]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string that specifies the name of the cube that contains the member.  
  
 *Member_Name*  
 A valid string that specifies the name of an existing member.  
  
 *MDX_Expression*  
 A valid Multidimensional Expressions (MDX) expression to which the member is to be updated.  
  
 *Property_Name*  
 A valid string that specifies the name of a calculated member property.  
  
 *Property_Value*  
 A valid scalar expression that specifies the property value for the calculated member.  
  
## Remarks  
 The UPDATE MEMBER statement updates an existing calculated member while preserving the relative precedence of this member with respect to other calculations. Therefore, you cannot use the UPDATE MEMBER statement to change SOLVEORDER.  
  
 An UPDATE MEMBER statement cannot be specified in the MDX script for a cube.  
  
 Specifying a cube other than the cube that is currently connected causes an error. Therefore, you should use CURRENTCUBE in place of a cube name to denote the current cube.  
  
 For more information about member properties that are defined by OLE DB, see the OLE DB documentation.  
  
## Standard Properties  
 Each member has a set of default properties. The following table lists those default properties.  
  
|Property identifier|Meaning|  
|-------------------------|-------------|  
|FORMAT_STRING|A Office style format string that the client application can use to display cell values.|  
|VISIBLE|A value that indicates whether the calculated member is visible in a schema rowset. Visible calculated members can be added to a set with the [AddCalculatedMembers](../mdx/addcalculatedmembers-mdx.md) function. A nonzero value indicates that the calculated member is visible. The default value for this property is *Visible*.<br /><br /> Calculated members that are not visible are generally used as intermediate steps in more complex calculated members. These calculated members can also be referred to by other types of members, such as measures.|  
|NON_EMPTY_BEHAVIOR|The measure or set that MDX uses to determine the behavior of calculated members when resolving empty cells.|  
|CAPTION|A string value that specifies the caption that the client application uses to display the member.|  
|DISPLAY_FOLDER|A string value that specifies the path of the display folder where the member is to be shown by the client application. The folder level separator is defined by the client application. For the tools and clients supplied by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the backslash (\\) as the level separator. To provide multiple display folders for a defined member, use a semicolon (;) to separate the folders.|  
|ASSOCIATED_MEASURE_GROUP|The name of the measure group to which this member is associated.|  
  
## See Also  
 [DROP MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-member.md)   
 [CREATE MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-member.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
