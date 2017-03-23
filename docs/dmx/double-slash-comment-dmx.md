---
title: "Double Slash (Comment) (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "double forward slashes"
  - "commenting characters"
  - "// (comment)"
ms.assetid: c2ab9ca1-9fc9-4162-97f9-a9433d189220
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Double Slash (Comment) (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Indicates a text string that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] should not execute. You can nest comments within a Data Mining Extensions (DMX) statement, include them at the end of a line of code, or insert them on a separate line.  
  
## Syntax  
  
```  
  
// Comment_Text   
```  
  
#### Parameters  
 *Comment_Text*  
 The string that contains the text of the comment.  
  
## Remarks  
 Use // for single-line comments only. Comments that are inserted by using // are delimited by the newline character.  
  
 There is no maximum length for comments.  
  
 For more information about how to use different kinds of comments in DMX, see [Comments &#40;DMX&#41;](../dmx/comments-dmx.md).  
  
## See Also  
 [Slash Star &#40;Comment&#41; &#40;DMX&#41;](../dmx/slash-star-comment-dmx.md)   
 [-- &#40;Comment&#41; &#40;DMX&#41; Summary](../dmx/comment-dmx-summary.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
