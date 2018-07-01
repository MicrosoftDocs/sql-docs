---
title: "Cube Translations | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Cube Translations
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A translation is a simple mechanism to change the displayed labels and captions from one language to another. Each translation is defined as a pair of values: a string with the translated text, and a number with the language ID. Translations are available for all objects in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Dimensions can also have the attribute values translated. The client application is responsible for finding the language setting that the user has defined, and switch to display all captions and labels to that language. An object can have as many translations as you want.  
  
 A simple <xref:Microsoft.AnalysisServices.Translation> object is composed of: language ID number, and translated caption. The language ID number is an **Integer** with the language ID. The translated caption is the translated text.  
  
 In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a cube translation is a language-specific representation of the name of a cube object, such as a caption or a display folder. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] also supports translations of dimension and member names.  
  
 Translations provide server support for client applications that can support multiple languages. Frequently, users from different countries view cube data. It is useful to be able to translate various elements of a cube into a different language so that these users can view and understand the cube's metadata. For example, a business user in France can access a cube from a workstation with a French locale setting, and view the object property values in French. Similarly, a business user in Germany can access the same cube from a workstation with a German locale setting and view the object property values in German.  
  
 The collation and language information for the client computer is stored in the form of a locale identifier (LCID). Upon connection, the client passes the LCID to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The instance uses the LCID to determine which set of translations to use when providing metadata for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects to each business user. If an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object does not contain the specified translation, the default language is used to return the content back to the client.  
  
## See Also  
 [Dimension Translations](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/dimension-translations.md)   
 [Translation support in Analysis Services](../../analysis-services/translation-support-in-analysis-services.md)   
 [Globalization Tips and Best Practices &#40;Analysis Services&#41;](../../analysis-services/globalization-tips-and-best-practices-analysis-services.md)  
  
  
