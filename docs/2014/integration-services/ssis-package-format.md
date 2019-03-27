---
title: "SSIS Package Format | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: cfe0e5dc-5be3-4222-b721-fe83665edd94
author: janinezhang
ms.author: janinez
manager: craigg
---
# SSIS Package Format
  In the current release of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], significant changes were made to the package format (.dtsx file) to make it easier to read the format and to compare packages. You can also more reliably merge packages that don't contain conflicting changes or changes stored in binary format.  
  
 To view the current DTSX package file format, see [\[MS-DTSX\]: Data Transformation Services Package XML File Format Specification](https://go.microsoft.com/fwlink/?LinkId=233251).  
  
 The following list outlines the file format changes. To view code examples of these changes, see [Package Format Changes in SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=233255).  
  
-   Formatting conventions have been applied to make it easier to read and understand the .dtsx file.  
  
-   The format is more concise. Separate elements for each property have been persisted as attributes, with the exception of the PackageFormatVersion. Attributes are listed alphabetically, and properties that have default values are no longer persisted. Finally, elements that can appear multiple times, are now contained within a parent element.  
  
-   Most objects within a package that can be referred to by other objects now have a `refId` attribute defined in the package XML. Instead of persisting lineage IDs, the `refID` is now persisted. Lineage IDs are still used within the runtime and regenerated when the package is loaded.  
  
     The `refId` value is a unique string that is readable and understandable, compared to GUIDs or integer values. The string is similar to path values used for package configurations in previous releases of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].  
  
     If you are merging changes between two versions of a package, the `refId` can be used in find/replace operations to ensure that all references to that object have been correctly updated.  
  
-   The layout information is contained in a CData section.  
  
-   Annotations are persisted in cleartext. This makes it easier to extract the information for automated generation of documentation.  
  
  
