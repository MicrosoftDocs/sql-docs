---
title: "The following features are not supported by Excel Services and may not display or may display only partially: Comments, Shapes, or other objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: ade92e15-dfbf-496b-9378-a00bd83ba750
author: minewiskan
ms.author: owend
manager: craigg
---
# The following features are not supported by Excel Services and may not display or may display only partially: Comments, Shapes, or other objects
  This error occurs when you add Slicers to a PowerPivot workbook from a PowerPivot Field List.  
  
## Details  
  
|||  
|-|-|  
|Applies to|PowerPivot for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|Excel Web Access cannot render the Shape object used to control positioning and formatting of Slicers added to a workbook from the PowerPivot Field List.|  
|Message Text|The following features are not supported by Excel Services and may not display or may display only partially:<br /><br /> Comments, Shapes, or other objects<br /><br /> Some features, such as external data queries, display cached data which can only be refreshed in Microsoft Excel.|  
  
## Explanation  
 Excel Web Access shows this error when you open a PowerPivot workbook in a browser and you click the **Details** button for the message, **Unsupported Features This workbook may not display as intended**.  
  
 This error occurs because the PowerPivot workbook contains Slicers whose layout is controlled by a hidden Shape object in Excel. The Shape object controls the formatting and positioning of the Slicers in horizontal and vertical placements.  
  
 Excel Services cannot render a Shape object, but because the object is hidden, the fact that it does not render is not an issue.  
  
## User Action  
 This error can be ignored. Click **OK** to close the error message and proceed to use the workbook and Slicers with no problem.  
  
  
