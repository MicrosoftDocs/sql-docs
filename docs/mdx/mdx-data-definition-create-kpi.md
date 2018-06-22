---
title: "CREATE KPI Statement (MDX) | Microsoft Docs"
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
# MDX Data Definition - CREATE KPI


  Creates a key performance indicator (KPI). A KPI is a collection of calculations that are associated with a measure group in a cube and are used to evaluate business or scenario success.  
  
## Syntax  
  
```  
  
CREATE KPI CURRENTCUBE | <Cube Name>.KPI_Name AS KPI_Value  
   [,Property_Name = Property_Value, ...n]  
```  
  
## Arguments  
 *KPI_Name*  
 A valid string that provides a KPI name.  
  
 *KPI_Value*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
 *Property_Name*  
 A valid string that provides the name of a KPI property.  
  
 *Property_Value*  
 A valid scalar expression that defines the KPI property's value.  
  
## Remarks  
 Specifying a cube other than the cube that is currently connected causes an error. Therefore, you should use CURRENTCUBE in place of a cube name to denote the current cube.  
  
## KPI Properties  
 The following table lists all KPI properties. None of these properties have a default value. Therefore, until a specific value has been assigned to a KPI property, queries against that properties will return a null value.  
  
|Property identifier|Meaning|  
|-------------------------|-------------|  
|GOAL|A valid MDX expression that returns a numeric value.|  
|STATUS|A valid MDX expression that returns a numeric value.|  
|STATUS_GRAPHIC|A string that defines a set of graphic images that will be used by the client application.|  
|TREND|A valid MDX expression that returns a numeric value.|  
|TREND_GRAPHIC|A string that defines a set of graphic images that will be used by the client application.|  
|WEIGHT|A valid MDX expression that returns a numeric value.|  
|CURRENT_TIME_MEMBER|A valid MDX expression that returns a member in the time dimension. CURRENT_TIME_MEMBER sets the reference point for all relative time functions|  
|PARENT_KPI|A string that specifies the name of the parent KPI.|  
|CAPTION|A string that the client application uses as the caption for the KPI.|  
|DISPLAY_FOLDER|A string that specifies the path of the display folder where the KPI is to be shown by the client application. The folder level separator is defined by the client application. For the tools and clients supplied by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the backslash (\\) is the level separator. To provide multiple display folders for a defined member, use a semicolon (;) to separate the folders|  
|ASSOCIATED_MEASURE_GROUP|A string that specifies the name of the measure group to which all MDX calculations should be referred.|  
  
 The values for the GOAL, STATUS, and TREND properties are MDX expressions that should evaluate between -1 to 1. However, it is the client application that defines the actual range of values for these properties. When you use the tools and clients supplied by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to browse KPIs, values less than -1 are treated as -1, and values larger than 1 are treated as 1.  
  
 Both STATUS_GRAPHIC and TREND_GRAPHIC are string values that the client application uses to identify the correct set of images to display. These strings also define the behavior of the display function. This behavior includes the number of states to display (typically, this is an odd number) and which images to use for each of those states.  
  
### KPI Graphics in SQL Server Data Tools  
 In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], KPI graphics can have either three or five states. The following table defines the values for each of those states.  
  
|Number of states for KPI graphic|Value of those states|  
|--------------------------------------|---------------------------|  
|3|Bad = -1 to -0.5<br /><br /> OK = -0.4999 to 0.4999<br /><br /> Good = 0.50 to 1|  
|5|Bad = -1 to -0.75<br /><br /> Risk = -0.7499 to -0.25<br /><br /> OK = -0.2499 to 0.2499<br /><br /> Rising = 0.25 to 0.7499<br /><br /> Good = 0.75 to 1|  
  
> [!NOTE]  
>  For some graphics, such as the reversed gauge or reversed status arrow, the range is inverted. That is, -1 is good, and 1 is bad.  
  
 In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the name of the KPI graphic determines whether the graphic has three or five states. The following table lists the usage, name, and number of states that [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] associates with its KPI graphics.  
  
|Use of graphic|Name of KPI graphic|Number of states|  
|--------------------|-------------------------|----------------------|  
|Status|Shapes|3|  
|Status|Traffic Light|3|  
|Status|Road Signs|3|  
|Status|Gauge|3|  
|Status|Reversed Gauge|5|  
|Status|Thermometer|3|  
|Status|Cylinder|3|  
|Status|Faces|3|  
|Status|Variance arrow|3|  
|Trend|Standard Arrow|3|  
|Trend|Status Arrow|3|  
|Trend|Reversed status arrow|5|  
|Trend|Faces|3|  
  
## See Also  
 [DROP KPI Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-kpi.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
