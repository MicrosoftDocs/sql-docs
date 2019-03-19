---
title: "Globalization scenarios for Analysis Services Multiidimensional | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "multiple language support [Analysis Services]"
  - "languages [Analysis Services]"
  - "SSAS, international considerations"
  - "international considerations [Analysis Services]"
  - "global considerations [Analysis Services]"
  - "SQL Server Analysis Services, international considerations"
  - "Analysis Services, international considerations"
ms.assetid: e8af85ff-ef33-4659-a003-bb34578eb2a2
author: minewiskan
ms.author: owend
manager: craigg
---
# Globalization scenarios for Analysis Services Multiidimensional
  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] stores and manipulates multilingual data and metadata in both tabular and multidimensional data models. Data storage is Unicode (UTF-16), in character sets that use Unicode encoding. If you load ANSI data into a data model, characters are stored using Unicode equivalent code points.  
  
 The implications of Unicode support means that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] can store data in any of the languages supported by the Windows client and server operating systems, allowing read, write, sort, and comparison of data in any character set used on a Windows computer. BI client applications consuming [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data can represent data in the user's language of choice, assuming the data exists in that language in the model.  
  
 Language support can mean different things to different people. The following list addresses a few common questions related to how Analysis Services supports languages.  
  
-   Data, as already noted, is stored in any Unicode-encoded character set found on a Windows client operating system.  
  
-   Metadata, such as object names, identifiers, and descriptions, can also be in any Unicode language and script. This is true even when the tools and environment are in another language. For example, in a development environment that uses English language and a Latin collation throughout the stack, you can include in your model an object that uses Cyrillic characters in its name.  
  
     For multidimensional models only, captions and attribute members can be expressed as translations. You can define one or more translations, and then use a locale identifier to determine which translation is returned to the client. See [Features](#bkmk_features) below for more details.  
  
-   Error, warning, and informational messages returned from the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] engine (msmdsrv) are localized into the 43 languages supported by Office and Office 365. No configuration is required to get messages in a specific language. The locale of the client application determines which strings are returned.  
  
-   Configuration file (msmdsrv.ini) and AMO PowerShell are in English only.  
  
-   Log files will contain a mix of English and localized messages, assuming you have installed a language pack on the Windows server that Analysis Services runs on.  
  
-   Documentation and tools, such as [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] and [!INCLUDE[ss_dtbi](../includes/ss-dtbi-md.md)], are translated into these languages: Chinese Simplified, Chinese Traditional, French, German, Italian, Japanese, Korean, Portuguese (Brazil), Russian, and Spanish. To use a language-specific version of the tools, install a language-specific version of SQL Server (for example, install the German version of SQL Server to get Management Studio in German) or run the standalone setup in the target language for [!INCLUDE[ss_dtbi](../includes/ss-dtbi-md.md)].  
  
 Analysis Services lets you set language, collation, and translations independently throughout the object hierarchy.  
  
 Scenarios enabled through language, collations, and translations include:  
  
-   One data model provides multiple translated captions so that field names and values appear in the user's language of choice. For companies operating in bi-lingual countries such as Canada, Belgium, or Switzerland, supporting multiple languages across client and server applications is a standard coding requirement. This scenario is enabled through translations and currency conversions. See [Features](#bkmk_features) below for details and links.  
  
-   Development and production environments are geo-located in different countries. It's increasingly common to develop a solution in one country and then deploy it another. Knowing how to set language and collation properties is essential if you are tasked with preparing a solution developed in one language, for deployment on a server that uses a different language pack. Setting these properties allows you to override the inherited defaults that you get from the original host system. See [Languages and Collations &#40;Analysis Services&#41;](languages-and-collations-analysis-services.md) for details about setting properties.  
  
##  <a name="bkmk_features"></a> Features for building a globalized multidimensional solution  
 [!INCLUDE[applies](../includes/applies-md.md)] Multidimensional data models only  
  
 At the client level, globalized applications that consume or manipulate [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] multidimensional data can use the multilingual and multicultural features in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]:  
  
-   [Translations &#40;Analysis Services&#41;](translations-analysis-services.md) are used to embed multiple captions for a single object, where each translated string can exist alongside other translations. You can use the [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to define the translations for the caption, description, and account types for cubes and measures, dimension and attributes. You can retrieve data and metadata from [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] objects on which translations have been defined automatically by providing a locale identifier when connecting to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.  
  
     A lesson on how to use this feature can be found in [Lesson 9: Defining Perspectives and Translations](lesson-9-defining-perspectives-and-translations.md) of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] tutorial.  
  
-   [Currency Conversions &#40;Analysis Services&#41;](currency-conversions-analysis-services.md) is through specialized MDX scripts that convert measures containing currency data. You can use the Business Intelligence Wizard in [!INCLUDE[ss_dtbi](../includes/ss-dtbi-md.md)] to generate an MDX script that uses a combination of data and metadata from dimensions, attributes, and measure groups to convert measures containing currency data.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Languages and Collations &#40;Analysis Services&#41;](languages-and-collations-analysis-services.md)|Specify default language and Windows collation for an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance. Your choices affect data and metadata managed by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].|  
|[Translations &#40;Analysis Services&#41;](translations-analysis-services.md)|Define translations for an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database and objects contained by the database. This topic explains how [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] resolves requests for translated data and metadata from client applications.|  
|[Currency Conversions &#40;Analysis Services&#41;](currency-conversions-analysis-services.md)|Define a currency conversion using the Business Intelligence Wizard.|  
|[Globalization Tips and Best Practices &#40;Analysis Services&#41;](globalization-tips-and-best-practices-analysis-services.md)|Reviews several design and coding practices that can help you avoid problems related to multi-language data.|  
  
## See Also  
 [Internationalization for Windows Applications](/windows/desktop/Intl/international-support)   
 [Microsoft Globalization Documentation](https://docs.microsoft.com/globalization/)   
 [Writing Windows Store apps with locale-based adaptive design](http://blogs.windows.com/buildingapps/2014/03/06/writing-windows-store-apps-with-locale-based-adaptive-design/)   
 [Developing Universal Windows Apps with C# and XAML](http://www.microsoftvirtualacademy.com/training-courses/developing-universal-windows-apps-with-c-and-xaml)  
  
  
