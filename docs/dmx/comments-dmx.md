---
title: "Comments (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Comments (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Comments in Data Mining Extensions (DMX) are text strings in program code that [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] does not execute. Comments are also known as remarks. You can use comments to document code or to temporarily disable parts of a DMX statement or script when you are diagnosing the code.  
  
 Using comments to document your program code makes it easier to maintain the code in the future. You can use comments to record details such as the name of the program, the name of the developer who wrote the code, and the dates of major code changes. You can also use comments to describe complex calculations or to a programming method.  
  
 Following are basic guidelines for writing comments:  
  
-   You can use any alphanumeric characters or symbols within a comment. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] ignores all characters that are within a comment.  
  
-   There is no maximum length for a comment within a statement or script. A comment can be made up of one or more lines.  
  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports the following types of comment characters:  
  
-   **// (double forward slashes).** Use these comment characters to write a comment on the same line as code that is to be executed, or to write a comment on a line by itself. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] evaluates everything from the double forward slashes to the end of the line as part of the comment. To create a multiple-line comment, use the double forward slashes at the start of each line of comment. For more information about this comment character, see [Double Slash &#40;Comment&#41; &#40;DMX&#41;](../dmx/double-slash-comment-dmx.md).  
  
-   **-- (double hyphens).** Use these comment characters to write a comment on the same line as code that is to be executed, or to write a comment on a line by itself. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] evaluates everything from the double hyphens to the end of the line as part of the comment. To create a multiple-line comment, use the double hyphens at the start of each line of comment. For more information about this comment character, see [-- &#40;Comment&#41; &#40;DMX&#41; Summary](../dmx/comment-dmx-summary.md).  
  
-   **/\* ... \*/ (forward slash-asterisk character pairs).** Use these comment characters to write a comment on the same line as the code that is to be executed, to write a comment on a line by itself, or even to write comments within executable code. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] evaluates everything from the open comment pair (/*) to the close comment pair (\*/) as part of the comment. To create a multiple-line comment, start the comment with the open-comment character pair (/\*), and end the comment with the close-comment character pair (\*/). No other comment characters should be included on any lines of the comment. For more information about this comment character, see [Slash Star &#40;Comment&#41; &#40;DMX&#41;](../dmx/slash-star-comment-dmx.md).  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
