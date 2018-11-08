---
title: "Cube Translations | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "multiple language support [Analysis Services]"
  - "international considerations [Analysis Services]"
  - "global considerations [Analysis Services]"
  - "cubes [Analysis Services], translations"
  - "OLAP objects [Analysis Services], translations"
  - "translations [Analysis Services], cubes"
ms.assetid: 4e4fd6a4-d324-4508-b75a-2a57de9ab8ff
author: minewiskan
ms.author: owend
manager: craigg
---
# Cube Translations
  A translation is a simple mechanism to change the displayed labels and captions from one language to another. Each translation is defined as a pair of values: a string with the translated text, and a number with the language ID. Translations are available for all objects in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Dimensions can also have the attribute values translated. The client application is responsible for finding the language setting that the user has defined, and switch to display all captions and labels to that language. An object can have as many translations as you want.  
  
 A simple <xref:Microsoft.AnalysisServices.Translation> object is composed of: language ID number, and translated caption. The language ID number is an `Integer` with the language ID. The translated caption is the translated text.  
  
 In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a cube translation is a language-specific representation of the name of a cube object, such as a caption or a display folder. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] also supports translations of dimension and member names.  
  
 Translations provide server support for client applications that can support multiple languages. Frequently, users from different countries view cube data. It is useful to be able to translate various elements of a cube into a different language so that these users can view and understand the cube's metadata. For example, a business user in France can access a cube from a workstation with a French locale setting, and view the object property values in French. Similarly, a business user in Germany can access the same cube from a workstation with a German locale setting and view the object property values in German.  
  
 The collation and language information for the client computer is stored in the form of a locale identifier (LCID). Upon connection, the client passes the LCID to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The instance uses the LCID to determine which set of translations to use when providing metadata for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects to each business user. If an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object does not contain the specified translation, the default language is used to return the content back to the client.  
  
## See Also  
 [Dimension Translations](../multidimensional-models-olap-logical-dimension-objects/dimension-translations.md)   
 [Translations &#40;Analysis Services&#41;](../translations-analysis-services.md)   
 [Globalization Tips and Best Practices &#40;Analysis Services&#41;](../globalization-tips-and-best-practices-analysis-services.md)  
  
  
