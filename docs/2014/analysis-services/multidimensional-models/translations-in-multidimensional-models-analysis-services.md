---
title: "Translations in Multidimensional Models | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensiondesigner.deletelanguagefirm.f1"
ms.assetid: 5521f8ef-b10a-4861-9df7-1e43e0a1fb3f
author: minewiskan
ms.author: owend
manager: craigg
---
# Translations in Multidimensional Models
  Multilanguage support in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is accomplished by using translations. A translation contains a language identifier and bindings for properties of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects which can be presented in multiple languages. For example, you can define a translation for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database to present the caption and description of that database in a specified language. For more information about translations, see [Cube Translations](../multidimensional-models-olap-logical-cube-objects/cube-translations.md).  
  
## Defining Translations  
 You can define translations in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] by using the appropriate designer for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object to be translated. Defining a translation creates a `Translation` object associated with the appropriate [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object that has the specified explicit literal values, in the specified language, for the properties of the associated [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object.  
  
 The following objects and properties in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] can have translations associated with them:  
  
|Object|Properties|Designer|  
|------------|----------------|--------------|  
|Database|`Caption`, `Description`|[General &#40;Database Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../general-database-designer-analysis-services-multidimensional-data.md)|  
|Cube|`Caption`, `Description`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Measure group|`Caption`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Measure|`Caption`, `DisplayFolder`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Cube dimension|`Caption`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Perspective|`Caption`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Key performance indicator (KPI)|`Caption`, `Description`, `DisplayFolder`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Action|`Caption`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Named set|`Caption`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Calculated member|`Caption`|[Translations &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-cube-designer-analysis-services-multidimensional-data.md)|  
|Database dimension|`Caption`, `AttributeAllMember`|[Translations &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-dimension-designer-analysis-services-multidimensional-data.md)|  
|Attribute|`Caption`, `CaptionColumn`<sup>1</sup>, `AttributeHierarchyDisplayFolder`, `NamingTemplate`, `MembersWithDataCaption`|[Translations &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-dimension-designer-analysis-services-multidimensional-data.md)|  
|Hierarchy|`Caption`, `AllMemberName`|[Translations &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-dimension-designer-analysis-services-multidimensional-data.md)|  
|Level|`Caption`|[Translations &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](../translations-dimension-designer-analysis-services-multidimensional-data.md)|  
  
 <sup>1</sup> The `CaptionColumn` property of an attribute can be bound to a column in a data source view and can use a Windows collation other than that specified for the instance, unlike other translations.  
  
### Defining Attribute Translations  
 Translations associated with attributes in database dimensions are handled differently than other translations in the following ways:  
  
-   A column binding, instead of an explicit literal value, can be associated with the `CaptionColumn` property so that the member names of members for that attribute can be translated.  
  
-   A Windows collation other than the collation specified for the instance can be used so that members in the attribute can be appropriately sorted for the language specified in the translation.  
  
 You can use the **Attribute Data Translation** dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to define translations for attributes in database dimensions. For more information about the **Attribute Data Translation** dialog box, see [Attribute Data Translation Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](../attribute-data-translation-dialog-box-analysis-services-multidimensional-data.md).  
  
## Resolving Translations  
 If a client application requests information in a specified language identifier, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance attempts to resolve data and metadata for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects to the closest possible language identifier. If the client application does not specify a default language, or specifies the neutral locale identifier (0) or process default language identifier (1024), then [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses the default language for the instance to return data and metadata for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects.  
  
 If the client application specifies a language identifier other than the default language identifier, the instance iterates through all available translations for all available objects. If the specified language identifier matches the language identifier of a translation, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns that translation. If a match cannot be found, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] attempts to use one of the following methods to return translations with a language identifier closest to the specified language identifier:  
  
-   For the following language identifiers, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] attempts to use an alternate language identifier if a translation for the specified language identifier is not defined:  
  
    |Specified language identifier|Alternate language identifier|  
    |-----------------------------------|-----------------------------------|  
    |3076 - Chinese (Hong Kong SAR, PRC)|1028 - Chinese (Taiwan)|  
    |5124 - Chinese (Macao SAR)|1028 - Chinese (Taiwan)|  
    |1028 - Chinese (Taiwan)|Default language|  
    |4100 - Chinese (Singapore)|2052 - Chinese (PRC)|  
    |2074 - Croatian|Default language|  
    |3098 - Croatian (Cyrillic)|Default language|  
  
-   For all other specified language identifiers, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] extracts the primary language of the specified language identifier and retrieves the language identifier indicated by Windows as the best match for the primary language. If a translation for the best match language identifier cannot be found, or if the specified language identifier is the best match for the primary language, then the default language is used.  
  
## Deleting Translation Objects  
 You can right-click a translation object in the dimension or cube designer to permanently remove it. You cannot restore or recycle a deleted object, so be sure to review the list of affected objects before continuing.  
  
## See Also  
 [Globalization scenarios for Analysis Services Multiidimensional](../globalization-scenarios-for-analysis-services-multiidimensional.md)   
 [Languages and Collations &#40;Analysis Services&#41;](../languages-and-collations-analysis-services.md)  
  
  
