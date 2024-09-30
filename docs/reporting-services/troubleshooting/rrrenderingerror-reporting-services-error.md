---
title: "rrRenderingError - Reporting Services error"
description: "In this error reference page, learn about event ID 'rrRenderingError': An error occurred during rendering of the report."
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "rrRenderingError"
---
# rrRenderingError - Reporting Services error
    
## Details  
  
|Category|Value|  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rrRenderingError|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings.resources.Strings|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|An error occurred during rendering of the report. (rrRenderingError) %1|  
  
## Explanation  
 This message is returned when [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] can't render or export the report.  
  
 A message that indicates that the size isn't supported is typically caused when the specified RDL page size isn't valid. Specify a valid RDL page size and then try again.  
  
 A message that indicates that an unsupported RDL size measurement is typically caused when an unsupported unit type is specified. Valid unit types are cm, in, mm, pc, and pt. Specify a valid unit type and then try again.  
  
 A message that indicates that a negative size isn't supported is typically caused when a negative measurement for the page size, for example -5 cm, is specified. Specify a positive number for the page size and then try again.  
  
 A message that indicates an out of range RDL size is typically caused when a measurement for the page size is outside of the valid page margin size. Specify a measurement for the page size that is within the valid page margin sizes.  
  
 A message that indicates an unsupported color specified is typically caused when a color specified in the RDL isn't valid. Choose a color supported by RDL and then try again.  
  
A message indicates that the action label is only optional when you use a single action. It also notes that adding multiple actions requires labels for each action when an action label isn't valid. Specify a valid action label for each action.  
  
 A message that indicates the style argument has to be of a specific type is typically caused when an incorrect style value for the data type is specified. The RDL specification identifies the valid types that you can use in the style values of different RDL elements. For example, an incorrect style value for background color is `2pt` and incorrect value for height is `true`. Specify a correct value and then try again.  
  
 A message that indicates that the border style isn't supported is typically caused when the border style specified isn't valid. Specify a supported border style and then try again.  
  
 A message that indicates an unsupported image mime type is typically caused when the specified mime type for an image report item isn't valid. Specify a supported mime type for the report item and then try again.  
  
 A message that indicates that the number of rows exceeds the maximum possible rows per sheet occurs when the number of rows in an Excel worksheet is exceeded. Excel supports up to 65,000 rows.  
  
 A message that indicates that the number of columns exceeds the maximum possible columns per sheet occurs when the number of columns in an Excel worksheet is exceeded.  
  
## User action  

## Internal-only  
  
