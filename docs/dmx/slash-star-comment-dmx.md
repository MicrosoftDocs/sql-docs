---
title: "Slash Star (Comment) (DMX)"
description: "Slash Star (Comment) (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Slash Star (Comment) (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Indicates a text string that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] should not execute. The server does not evaluate the text between the comment characters /* and \*/. You can nest comments within a Data Mining Extensions (DMX) statement, include them at the end of a line of code, or insert them on a separate line.  
  
## Syntax  
  
```  
  
/* Comment_Text */  
```  
  
#### Parameters  
 *Comment_Text*  
 The string that contains the text of the comment.  
  
## Remarks  
 Multiple-line comments must be indicated by /* and \*/.  
  
 There is no maximum length for comments.  
  
 For more information about how to use different kinds of comments in DMX, see [Comments &#40;DMX&#41;](../dmx/comments-dmx.md).  
  
## See Also  
 [Double Slash &#40;Comment&#41; &#40;DMX&#41;](../dmx/double-slash-comment-dmx.md)   
 [-- &#40;Comment&#41; &#40;DMX&#41; Summary](../dmx/comment-dmx-summary.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
