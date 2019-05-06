---
title: "Use Annotations in Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
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
  
 For more information about changes to the format of the package file, see [SSIS Package Format](https://msdn.microsoft.com/library/cfe0e5dc-5be3-4222-b721-fe83665edd94).  
  
 When you save the package, [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer saves the annotations in the package.  
  
## Add an annotation to a package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package to which you want to add an annotation.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, right-click anywhere on the design surface of the **Control Flow**, **Data Flow**, or **Event Handler** tab, and then click **Add Annotation**. A text block appears on the design surface of the tab.  
  
4.  Add text.  
  
    > [!NOTE]  
    >  If you add no text, the text block is removed when you click outside the block.  
  
5.  To change the size or format of the text in the annotation, right-click the annotation and then click **Set Text Annotation Font**.  
  
6.  To add an additional line of text, press ENTER.  
  
     The annotation box automatically increases in size as you add additional lines of text.  
  
7.  To add an annotation to a group, right-click the annotation and then click **Group**.  
  
8.  To save the updated package, on the **File** menu, click **Save All**.  
