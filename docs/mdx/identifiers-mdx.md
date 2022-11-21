---
description: "Identifiers (MDX)"
title: "Identifiers (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Identifiers (MDX)


  An identifier is the name of an Analysis Services object. Every object can and must have an identifier. This includes cubes, dimensions, hierarchies, levels, members, and so on. You use the identifier of an object to reference the object in Multidimensional Expressions (MDX) statements.  
  
 Depending on how you name the object, the identifier of the object identifier will be either a regular or delimited identifier.  
  
> [!NOTE]  
>  Both regular and delimited identifiers must contain from 1 through 100 characters.  
  
## Using Regular Identifiers  
 A regular identifier is an object name that complies with the following formatting rules for regular identifiers. A regular identifier can be used with or without delimiters.  
  
### Formatting Rules for Regular Identifiers  
  
1.  The first character must be one of the following:  
  
    -   A letter as defined by the Unicode Standard 2.0. Besides letter characters from other languages, the Unicode definition of letters includes Latin characters from a through z and from A through Z.  
  
    -   The underscore (_).  
  
2.  Subsequent characters can be:  
  
    -   Letters as defined in the Unicode Standard 2.0.  
  
    -   Decimal numbers from either Basic Latin or other national scripts.  
  
    -   The underscore (_).  
  
3.  The identifier must not be an MDX reserved keyword. Reserved keywords are case-insensitive in MDX. For more information, see [Reserved Keywords &#40;MDX Syntax&#41;](../mdx/reserved-keywords-mdx-syntax.md).  
  
4.  Embedded spaces or special characters are not allowed.  
  
### Examples of Regular Identifiers  
 In the following MDX statement, the identifiers, `Measures`, `Product`, and `Style`, comply with the formatting rules for regular identifiers. These regular identifiers do not need delimiters.  
  
 `SELECT Measures.MEMBERS ON COLUMNS,`  
  
 `Product.Style.CHILDREN ON ROWS`  
  
 `FROM [Adventure Works]`  
  
 ``  
  
 Although not required, you could also use delimiters with regular identifiers. In the following MDX statement, the `Measures`, `Product`, and `Style` regular identifiers have been correctly delimited by using brackets.  
  
 `SELECT [Measures].MEMBERS ON COLUMNS,`  
  
 `[Product].[Style].CHILDREN ON ROWS`  
  
 `FROM [Adventure Works]`  
  
 ``  
  
## Using Delimited Identifiers  
 An identifier that does not comply with the formatting rules for regular identifiers must always be delimited by using brackets ([]).  
  
> [!NOTE]  
>  Delimiters are for identifiers only. Delimiters cannot be used for keywords, whether or not the keywords are marked as reserved in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 You use a delimited identifier in the following situations:  
  
-   When the name of an object or part of the name uses reserved words.  
  
     We recommend that reserved keywords not be used as object names. Databases upgraded from earlier versions of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] may contain identifiers that include words not reserved in the earlier version, but are now reserved. Until you can change the identifier for the object, you can reference the object using a delimited identifier.  
  
-   When the name of an object uses characters not listed as qualified identifiers.  
  
     [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] allows a delimited identifier to use any character in the current code page. However, indiscriminate use of special characters in an object name may make MDX statements and scripts difficult to read and maintain.  
  
### Formatting Rules for Delimited Identifiers  
 The body of a delimited identifier can contain any combination of characters in the current code page, including the delimiting characters themselves. If the body of the delimited identifier contains delimiting characters, special handling is required:  
  
-   If the body of the identifier contains only a left bracket ([), no additional handling is required.  
  
-   If the body of the identifier contains a right bracket (]), you must specify two right brackets (]]).  
  
### Examples of Delimited Identifiers  
 In the following hypothetical MDX statement, `Sales Volume`, `Sales Cube`, and `select` are delimited identifiers:  
  
 `-- The [Sales Volume] and [Sales Cube] identifiers contain a space.`  
  
 `SELECT Measures.[Sales Volume]`  
  
 `FROM [Sales Cube]`  
  
 `WHERE Product.[select]`  
  
 `-- The [select] identifier is a reserved keyword.`  
  
 In this next example, the name of an object is `Total Profit [Domestic]`. To reference this object, you must use the following delimited identifier:  
  
 `[Total Profit [Domestic]]]`  
  
 Notice that the left bracket before `Domestic` did not have to be changed to create the delimited identifier. However, the right bracket following `Domestic` had to be replaced with two right brackets.  
  
### Delimiting Identifiers with Multiple Parts  
 When you use qualified object names you may have to delimit more than one of the identifiers that make up the object name. For example, the Front Brakes identifier in the following code needs delimiting.  
  
 SELECT [Measures].MEMBERS ON COLUMNS,  
  
 [Product].[Product].[Front Brakes] ON ROWS  
  
 FROM [Adventure Works]  
  
 In addition, the Measures identifier in the previous example was delimited to demonstrate delimiting more than one identifier.  
  
## See Also  
 [MDX Language Reference &#40;MDX&#41;](../mdx/mdx-language-reference-mdx.md)   
 [MDX Query Fundamentals &#40;Analysis Services&#41;](/analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services)   
 [MDX Syntax Elements &#40;MDX&#41;](../mdx/mdx-syntax-elements-mdx.md)  
  
