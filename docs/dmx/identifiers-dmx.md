---
title: "Identifiers (DMX) | Microsoft Docs"
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
# Identifiers (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  All objects in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] must have an identifier. An object's name is its identifier. Servers, databases, and database objects such as data sources, data source views, cubes, dimensions, mining models, and so on have identifiers.  
  
 There are two classes of identifiers in Data Mining Extensions (DMX):  
  
-   [Regular identifiers](#RegularIdentifiers)  
  
-   [Delimited identifiers](#DelimitedIdentifiers)  
  
 An object identifier is created when you define the object. You then use the identifier to reference the object. Identifiers must be 100 characters or less.  
  
##  <a name="RegularIdentifiers"></a> Regular Identifiers  
 Regular identifiers in DMX comply with the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] rules for the format of identifiers. Regular identifiers in DMX do not require delimiters. Following is an example of a DMX statement that uses a regular, non-delimited identifier:  
  
```  
SELECT * FROM Clustering.CONTENT;  
```  
  
### Rules for Regular Identifiers  
 Following are the rules for the format of regular identifiers:  
  
1.  The first character of a regular identifier must be one of the following:  
  
    -   A letter as defined by the Unicode Standard 2.0. This includes Latin characters from a through z and from A through Z, and letter characters from other languages.  
  
    -   An underscore (_).  
  
2.  Subsequent characters can be:  
  
    -   Letters as defined in the Unicode Standard 2.0.  
  
    -   Decimal numbers from either Basic Latin or other national scripts.  
  
    -   An underscore (_).  
  
3.  The identifier must not be a DMX reserved word. Reserved words are case-insensitive in DMX. For more information, see [Reserved Keywords &#40;DMX&#41;](../dmx/reserved-keywords-dmx.md).  
  
4.  The identifier cannot contain embedded spaces or special characters.  
  
 You must delimit with brackets any identifiers that do not comply with these rules when you use them in DMX statements.  
  
##  <a name="DelimitedIdentifiers"></a> Delimited Identifiers  
 Delimited identifiers are enclosed in brackets ([ ]).  Following is an example of a DMX statement with a delimited identifier that complies with those rules.  
  
```  
SELECT * FROM [Marketing_Clusters].CONTENT;  
```  
  
 An identifier that does not comply with the rules for the format of regular identifiers must always be delimited. Following is an example of DMX statement with a delimited identifier that contains a space:  
  
```  
SELECT * FROM [Targeted Mailing].CONTENT;  
```  
  
 Use delimited identifiers in the following situations:  
  
-   When you use reserved words for object names or parts of object names.  
  
     We recommend that you do not use reserved keywords as object names. Databases that you upgrade from earlier versions of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] may contain identifiers that include words that were not reserved in the earlier version of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] but that are reserved words for[!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. You can use a delimited identifier to refer to such an object until you can change the object's name.  
  
-   When you use characters that are not listed as qualified identifiers.  
  
     In [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] you can use any character in the current code page in a delimited identifier; however, indiscriminate use of special characters in an object name may make DMX statements difficult to read and maintain.  
  
### Rules for Delimited Identifiers  
 Following are the rules for the format of delimited identifiers:  
  
1.  Delimited identifiers can contain the same number of characters as regular identifiers (from 1 through 100 characters, not including the delimiter characters).  
  
2.  The body of an identifier can contain any combination of characters that are used in the current code page, including the delimiting characters themselves. If the body of the identifier itself contains delimiting characters, special handling is required:  
  
    -   If the body of the identifier contains a left bracket ([), no additional handling is required.  
  
    -   If the body of the identifier contains a right bracket (]), you must specify two right brackets (]]) to represent it within the code page.  
  
### Delimiting Identifiers with Multiple Parts  
 When you use qualified object names, you may have to delimit more than one of the identifiers that make up the object name. You must delimit each identifier individually.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
