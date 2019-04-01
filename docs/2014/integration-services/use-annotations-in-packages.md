---
title: "Use Annotations in Packages | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "self-documenting packages"
  - "adding annotations"
  - "annotations [Integration Services]"
ms.assetid: 48c8ed9a-b10d-490c-9ba7-4b77aa44e3dd
author: janinezhang
ms.author: janinez
manager: craigg
---
# Use Annotations in Packages
  The [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer provides annotations, which you can use to make packages self-documenting and easier to understand and maintain. You can add annotations to the control flow, data flow, and event handler design surfaces of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. The annotations can contain any type of text, and they are useful for adding labels, comments, and other descriptive information to a package. Annotations are a design-time feature only. For example, they are not written to logs.  
  
 When you press ENTER, the text wraps to the next line. The annotation box automatically increases in size as you add additional lines of text. Package annotations are persisted as clear text in the CDATA section of the package file.  
  
 For more information about changes to the format of the package file, see [SSIS Package Format](../../2014/integration-services/ssis-package-format.md).  
  
 When you save the package, [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer saves the annotations in the package.  
  
### To add an annotation to a package  
  
-   [Add an Annotation to a Package](../../2014/integration-services/add-an-annotation-to-a-package.md)  
  
  
