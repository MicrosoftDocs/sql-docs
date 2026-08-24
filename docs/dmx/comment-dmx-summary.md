---
title: "-- (Comment) (DMX) Summary"
description: "-- (Comment) (DMX) Summary"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# -- (Comment) (DMX) Summary
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

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
  
## Related content

- [Slash Star (Comment) (DMX)](slash-star-comment-dmx.md)
- [Double Slash (Comment) (DMX)](double-slash-comment-dmx.md)
- [Data Mining Extensions (DMX) Operator Reference](data-mining-extensions-dmx-operator-reference.md)
- [Operators (DMX)](operators-dmx.md)
