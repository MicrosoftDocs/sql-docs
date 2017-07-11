---
title: "-- (Comment) (DMX) Summary | Microsoft Docs"
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
  - "commenting characters"
  - "double hyphens"
  - "-- (comment character)"
ms.assetid: 487b580b-5b81-4e52-8868-4fa809e4ef58
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# -- (Comment) (DMX) Summary
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Indicates a text string that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] should not execute. You can nest comments within a Data Mining Extensions (DMX) statement, include them at the end of a line of code, or insert them on a separate line.  
  
## Syntax  
  
```  
  
-- Comment_Text      
```  
  
#### Parameters  
 *Comment_Text*  
 The string that contains the text of the comment.  
  
## Remarks  
 Use this operator for single-line or nested comments. Comments that are inserted by using -- are delimited by the newline character.  
  
 There is no maximum length for comments.  
  
 For more information about how to use different kinds of comments in DMX, see [Comments &#40;DMX&#41;](../dmx/comments-dmx.md).  
  
## See Also  
 [Slash Star &#40;Comment&#41; &#40;DMX&#41;](../dmx/slash-star-comment-dmx.md)   
 [Double Slash &#40;Comment&#41; &#40;DMX&#41;](../dmx/double-slash-comment-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
