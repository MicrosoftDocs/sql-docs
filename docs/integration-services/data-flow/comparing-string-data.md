---
title: "Comparing String Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "comparing string data"
  - "comparison options [Integration Services]"
  - "locales [Integration Services]"
  - "converting string data"
  - "string comparisons"
ms.assetid: 93aeb5bd-e208-46b7-8979-dea2dcd37d4c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Comparing String Data
  String comparisons are an important part of many of the transformations performed by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], and string comparisons are also used in the evaluation of expressions in variables and property expressions. For example, the Sort transformation compares values in a dataset to sort data in ascending or descending order.  
  
## Configuring Transformations for String Comparisons  
 The Sort, Aggregate, Fuzzy Grouping, and Fuzzy Lookup transformations can be customized to change the way strings are compared at the column level. For example, you can specify that a comparison ignores case, which means that uppercase and lowercase characters are treated as the same character.  
  
 The following transformations use expressions that can include string comparisons.  
  
-   The Conditional Split transformation can use string comparisons in expressions to determine which output to send the data row to. For more information, see [Conditional Split Transformation](../../integration-services/data-flow/transformations/conditional-split-transformation.md).  
  
-   The Derived Column transformation can use string comparisons in expressions to generate new column values. For more information, see [Derived Column Transformation](../../integration-services/data-flow/transformations/derived-column-transformation.md).  
  
 Variables, variable mappings, and precedence constraints also use expressions, which can include string comparisons. For more information about expressions, see [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md).  
  
## Processing during String Comparison  
 Depending on the data and the configuration of the transformation, the following processing may occur during the comparison of string data:  
  
-   Converting data to Unicode. If the source data is not already Unicode, the data is automatically converted to Unicode before the comparison occurs.  
  
-   Using locale to apply locale-specific rules for interpreting date, time, decimal data, and sort order.  
  
-   Applying comparison options at the column level to change the sensitivity of comparisons.  
  
## Converting String Data to Unicode  
 Depending on the operations that the transformation performs and the configuration of the transformation, string data may be converted to the DT_WSTR data type, which is a Unicode representation of string characters.  
  
 String data that has the DT_STR data type is converted to Unicode using the code page of the column. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports code pages at the column level, and each column can be converted by using a different code page.  
  
 In most cases, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] can identify the correct code page from the data source. For example, in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you can set a collation at the database and column levels. The code page is derived from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation, which can be either a Windows or an SQL collation.  
  
 If [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides an unexpected code page, or if the package accesses a data source by using a provider that does not supply sufficient information to determine the correct code page, you can specify a default code page in the OLE DB source and the OLE DB destination. The default code pages are used instead of the code pages that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides.  
  
 Files do not have code pages. Instead, the Flat File and the Multiple Flat Files connection managers that a package uses to connect to file data include a property for specifying the code page of the file. The code page can be set at the file level only, not at the column level.  
  
## Setting Locale  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] does not use the code page to infer locale-specific rules for sorting data or interpreting date, time, and decimal data. Instead, the transformation reads the locale that is set by the LocaleId property on the data flow component, Data Flow task, container, or package. By default, the locale of a transformation is inherited from its Data Flow task, which in turn inherits from the package. If the Data Flow task is in a container such as the For Loop container, it inherits its locale from the container.  
  
 You can also specify a locale for a Flat File connection manager and a Multiple Flat Files connection manager.  
  
## Setting Comparison Options  
 The locale provides the basic rules for comparing string data. For example, the locale specifies the sort position of each letter in the alphabet. However, these rules may not be sufficient for the comparisons that some transformations perform, and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports a set of advanced comparison options that go beyond the comparison rules of a locale. These comparison options are set at the column level. For example, one of the comparison options lets you ignore nonspacing characters. The effect of this option is to ignore diacritics such as the accent, which makes "a" and "á" identical for comparison purposes.  
  
 The following table describes the comparison options and a sort style.  
  
|Comparison option|Description|  
|-----------------------|-----------------|  
|Ignore case|Specifies whether the comparison distinguishes between uppercase and lowercase letters. If this option is set, the string comparison ignores case. For example, "ABC" becomes the same as "abc".|  
|Ignore kana type|Specifies whether the comparison distinguishes between the two types of Japanese kana characters: hiragana and katakana. If this option is set, the string comparison ignores kana type.|  
|Ignore character width|Specifies whether the comparison distinguishes between a single-byte character and the same character when it is represented as a double-byte character. If this option is set, the string comparison treats single-byte and double-byte representations of the same character as identical.|  
|Ignore nonspacing characters|Specifies whether the comparison distinguishes between spacing characters and diacritics. If this option is set, the comparison ignores diacritics. For example, "å" is equal to "a".|  
|Ignore symbols|Specifies whether the comparison distinguishes between letter characters and symbols such as white-space characters, punctuation, currency symbols, and mathematical symbols. If this option is set, the string comparison ignores symbols. For example, " New York" becomes the same as "New York" and "*ABC" is the same as "ABC"'.|  
|Sort punctuation as symbols|Specifies whether the comparison sorts all punctuation symbols, except the hyphen and apostrophe, before the alphanumeric characters. For example, if this option is set, ".ABC" sorts before "ABC".|  
  
 The Sort, Aggregate, Fuzzy Grouping and Fuzzy Lookup transformations include these options for comparing data.  
  
 The **FullySensitive** comparison flag displays in the **Advanced Editor** dialog box for the Fuzzy Grouping and Fuzzy Lookup transformations. Selecting the **FullySensitive** comparison flag means that all the comparison options apply.  
  
## See Also  
 [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md)   
 [Fast Parse](https://msdn.microsoft.com/library/6688707d-3c5b-404e-aa2f-e13092ac8d95)   
 [Standard Parse](https://msdn.microsoft.com/library/dfe835b1-ea52-4e18-a23a-5188c5b6f013)  
  
  
