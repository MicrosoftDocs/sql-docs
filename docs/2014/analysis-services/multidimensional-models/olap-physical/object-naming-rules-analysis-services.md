---
title: "Object Naming Rules (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "objects [Analysis Services], naming"
ms.assetid: b338a60d-4802-4b68-862a-6dc6a3f75e48
author: minewiskan
ms.author: owend
manager: craigg
---
# Object Naming Rules (Analysis Services)
  This topic describes object naming conventions, as well as the reserved words and characters that cannot be used in any object name, in code or script in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
##  <a name="bkmk_Names"></a> Naming Conventions  
 Every object has a `Name` and `ID` property that must be unique within the scope of the parent collection. For example, two dimensions can have same name as long as each one resides in a different database.  
  
 Although you can specify it manually, the `ID` is typically auto-generated when the object is created. You should never change the `ID` once you have begun building a model. All object references throughout a model are based on the `ID`. Thus, changing an `ID` can easily result in model corruption.  
  
 `DataSource` and `DataSourceView` objects have notable exceptions to naming conventions. `DataSource` ID can be set to a single dot (.), which is not unique, as a reference to the current database. A second exception is `DataSourceView`, which adheres to the naming conventions defined for `DataSet` objects in the .NET Framework, where the `Name` is used as the identifier.  
  
 The following rules apply to `Name` and `ID` properties.  
  
-   Names are case insensitive. You cannot have a cube named "sales" and another named "Sales" in the same database.  
  
-   No leading or trailing spaces allowed in an object name, although you can embed spaces within a name. Leading and trailing spaces are implicitly trimmed. This applies to both the `Name` and `ID` of an object.  
  
-   The maximum number of characters is 100.  
  
-   There is no special requirement for the first character of an identifier. The first character may be any valid character.  
  
##  <a name="bkmk_reserved"></a> Reserved Words and Characters  
 Reserved words are in English, and apply to object names, not Captions. If you inadvertently use a reserved word in an object name, a validation error will occur. For multidimensional and data mining models, the reserved words described below cannot be used in any object name, at any time.  
  
 For tabular models, where the database compatibility is set to 1103, validation rules have been relaxed for certain objects, out of compliance for the extended character requirements and naming conventions of certain client applications. Databases that meet these criteria are subject to less stringent validation rules. In this case, it's possible for an object name to include a restricted character and still pass validation.  
  
 **Reserved Words**  
  
-   AUX  
  
-   CLOCK$  
  
-   COM1 through COM9 (COM1, COM2, COM3, and so on)  
  
-   CON  
  
-   LPT1 through LPT9 (LPT1, LPT2, LPT3, and so on)  
  
-   NUL  
  
-   PRN  
  
-   NULL is not allowed as a character in any string within the XML  
  
 **Reserved Characters**  
  
 The following table lists invalid characters for specific objects.  
  
|Object|Invalid characters|  
|------------|------------------------|  
|`Server`|Follow Windows server naming conventions when naming a server object. See [Naming Conventions (Windows)](/windows/desktop/DNS/naming-conventions) for details.|  
|`DataSource`|: / \ * &#124; ? " () [] {} <>|  
|`Level` or `Attribute`|. , ; ' ` : / \ * &#124; ? " & % $ ! + = [] {} \< >|  
|`Dimension` or `Hierarchy`|. , ; ' ` : / \ * &#124; ? " & % $ ! + = () [] {} \<,>|  
|All other objects|. , ; ' ` : / \ * &#124; ? " & % $ ! + = () [] {} \< >|  
  
 **Exceptions: When Reserved Characters are Allowed**  
  
 As noted, databases of a specific modality and compatibility level can have object names that include reserved characters. Dimension attribute, hierarchy, level, measure and KPI object names can include reserved characters, for tabular databases (1103 or higher) that allow the use of extended characters:  
  
|Server mode and database compatibility level|Reserved characters allowed?|  
|--------------------------------------------------|----------------------------------|  
|MOLAP (all versions)|No|  
|Tabular - 1050|No|  
|Tabular - 1100|No|  
|Tabular - 1130 and higher|Yes|  
  
 Databases can have a ModelType of default. Default is equivalent to multidimensional, and thus does not support the use of reserved characters in column names.  
  
## See Also  
 [MDX Reserved Words](/sql/mdx/mdx-reserved-words)   
 [Translations &#40;Analysis Services&#41;](../../../analysis-services/translations-analysis-services.md)   
 [XML for Analysis Compliance &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-for-analysis-compliance-xmla)  
  
  
