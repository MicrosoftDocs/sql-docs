---
title: "Dimension Translations | Microsoft Docs"
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
# Dimension Translations
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A translation is a simple mechanism to change the displayed labels and captions from one language to another. Each translation is defined as a pair of values: a string with the translated text, and a number with the language ID. Translations are available for all objects in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Dimensions can also have the attribute values translated. The client application is responsible for finding the language setting that the user has defined, and switch to display all captions and labels to that language. An object can have as many translations as you want.  
  
 A simple <xref:Microsoft.AnalysisServices.Translation> object is composed of: language ID number, and translated caption. The language ID number is an **Integer** with the language ID. The translated caption is the translated text.  
  
 In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a dimension translation is a language-specific representation of the name of a dimension, the name of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object or one of its members, such as a caption, member, or hierarchy level. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] also supports translations of cube objects.  
  
 Translations provide server support for client applications that can support multiple languages. Frequently, users from different countries view a cube and its dimensions. It is useful to be able to translate various elements of a cube and its dimensions into a different language so that these users can view and understand the cube. For example, a business user in France can access a cube from a workstation with a French locale setting, and see the object property values in French. However, a business user in Germany who accesses the same cube from a workstation with a German locale setting sees the same object property values in German.  
  
 The collation and language information for the client computer is stored in the form of a locale identifier (LCID). Upon connection, the client passes the LCID to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The instance uses the LCID to determine which set of translations to use when providing metadata for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects. If an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object does not contain the specified translation, the default language is used to return the content back to the client.  
  
## See Also  
 [Cube Translations](../../analysis-services/multidimensional-models-olap-logical-cube-objects/cube-translations.md)   
 [Translation support in Analysis Services](../../analysis-services/translation-support-in-analysis-services.md)   
 [Globalization Tips and Best Practices &#40;Analysis Services&#41;](../../analysis-services/globalization-tips-and-best-practices-analysis-services.md)  
  
  
