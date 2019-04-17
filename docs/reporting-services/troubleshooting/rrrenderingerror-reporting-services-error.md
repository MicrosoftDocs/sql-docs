---
title: "rrRenderingError - Reporting Services Error | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: troubleshooting


ms.topic: conceptual
helpviewer_keywords: 
  - "rrRenderingError"
ms.assetid: 0751efc3-b81b-44ee-8aac-8560f86ca322
author: markingmyname
ms.author: maghan
---
# rrRenderingError - Reporting Services Error
    
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rrRenderingError|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings.resources.Strings|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|An error occurred during rendering of the report. (rrRenderingError) %1|  
  
## Explanation  
 This message is returned when [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] cannot render or export the report.  
  
 A message that indicates that the size is not supported is typically caused when the specified RDL page size is not valid. Specify a valid RDL page size and then try again.  
  
 A message that indicates that an RDL size measurement is not supported is typically caused when an unsupported unit type is specified. Valid unit types are cm, in, mm, pc, and pt. Specify a valid unit type and then try again.  
  
 A message that indicates that a negative size is not supported is typically caused when a negative measurement for the page size, for example -5 cm, is specified. Specify a positive number for the page size and then try again.  
  
 A message that indicates that an RDL size is specified out of range is typically caused when a measurement for the page size is outside of the valid page margin size. Specify a measurement for the page size that is within the valid page margin sizes.  
  
 A message that indicates that a color specified is not supported is typically caused when a color specified in the RDL is not valid. Choose a color supported by RDL and then try again.  
  
 A message that indicates that the action label is only optional when using a single action and that adding multiple actions requires labels for each action is typically caused when an action label is specified and it is not valid. Specify a valid action label for each action.  
  
 A message that indicates the style argument has to be of a specific type is typically caused when an incorrect style value for the data type is specified. The RDL specification identifies the valid types that you can use in the style values of different RDL elements. For example, an incorrect style value for background color is "2pt" and incorrect value for height is "true". Specify a correct value and then try again.  
  
 A message that indicates that the border style is not supported is typically caused when the border style specified is not valid. Specify a supported border style and then try again.  
  
 A message that indicates that the image mimetype is not supported is typically caused when the specified mimetype for an image report item is not valid. Specify a supported mimetype for the report item and then try again.  
  
 A message that indicates that the number of rows exceeds the maximum possible rows per sheet is typically caused when the number of rows in an Excel worksheet is exceeded. Excel supports up to 65,000 rows.  
  
 A message that indicates that the number of columns exceeds the maximum possible columns per sheet is typically caused when the number of columns in an Excel worksheet is exceeded.  
  
## User Action  
  
## Internal-Only  
  
