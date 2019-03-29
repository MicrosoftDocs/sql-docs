---
title: "Globalization scenarios for Analysis Services | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Globalization scenarios for Analysis Services
[!INCLUDE[ssas-appliesto-sqlas-aas](../includes/ssas-appliesto-sqlas-aas.md)]

  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] stores and manipulates multilingual data and metadata for both tabular and multidimensional data models. Data storage is Unicode (UTF-16), in character sets that use Unicode encoding. If you load ANSI data into a data model, characters are stored using Unicode equivalent code points.  
  
 The implications of Unicode support means that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] can store data in any of the languages supported by the Windows client and server operating systems, allowing read, write, sort, and comparison of data in any character set used on a Windows computer. BI client applications consuming [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data can represent data in the user's language of choice, assuming the data exists in that language in the model.  
  
 Language support can mean different things to different people. The following list addresses a few common questions related to how Analysis Services supports languages.  
  
-   Data, as already noted, is stored in any Unicode-encoded character set found on a Windows client operating system.  
  
-   Metadata, such as object names, can be translated. Although support varies by model type, both multidimensional and tabular models support the addition of translated strings inside the model. You can define multiple translations, and then use a locale identifier to determine which translation is returned to the client. See [Features](#bkmk_features) below for more details  
  
-   Error, warning, and informational messages returned by the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] engine (msmdsrv) are localized into the 43 languages supported by Office and Office 365. No configuration is required to get messages in a specific language. The locale of the client application determines which strings are returned.  
  
-   Configuration file (msmdsrv.ini) and AMO PowerShell are in English only.  
  
-   Log files will contain a mix of English and localized messages, assuming you have installed a language pack on the Windows server that Analysis Services runs on.  
  
-   Documentation and tools, such as [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] and [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], are translated into these languages: Chinese Simplified, Chinese Traditional, French, German, Italian, Japanese, Korean, Portuguese (Brazil), Russian, and Spanish. Culture is specified during installation.  
  
 For multidimensional models, Analysis Services lets you set language, collation, and translations independently throughout the object hierarchy.  For tabular models, you can only add translations: language and collation are inherited by the host operating system.  
  
 Scenarios enabled through Analysis Services globalization features include:  
  
-   One data model provides multiple translated captions so that field names and values appear in the user's language of choice. For companies operating in bi-lingual countries such as Canada, Belgium, or Switzerland, supporting multiple languages across client and server applications is a standard coding requirement. This scenario is enabled through translations and currency conversions. See [Features](#bkmk_features) below for details and links.  
  
-   Development and production environments are geo-located in different countries. It's increasingly common to develop a solution in one country and then deploy it another. Knowing how to set language and collation properties is essential if you are tasked with preparing a solution developed in one language, for deployment on a server that uses a different language pack. Setting these properties allows you to override the inherited defaults that you get from the original host system. See [Languages and Collations &#40;Analysis Services&#41;](../analysis-services/languages-and-collations-analysis-services.md) for details about setting properties.  
  
##  <a name="bkmk_features"></a> Features for building a globalized multi-lingual solution  
 At the client level, globalized applications that consume or manipulate [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] multidimensional data can use the multilingual and multicultural features in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 You can retrieve data and metadata from [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] objects on which translations have been defined automatically by providing a locale identifier when connecting to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance.  
  
 See [Globalization Tips and Best Practices &#40;Analysis Services&#41;](../analysis-services/globalization-tips-and-best-practices-analysis-services.md) for design and coding practices that can help you avoid problems related to multi-language data.  
  
||||  
|-|-|-|  
|**Capability**|**Tabular**|**Multidimensional**|  
|[Languages and Collations &#40;Analysis Services&#41;](../analysis-services/languages-and-collations-analysis-services.md)|Inherited from the operating system.|Inherited, but with the ability to override both language and collation for major objects in the model hierarchy.|  
|Scope of translation support|Captions and descriptions.|Translations can be created for object names, captions,  identifiers, and descriptions, can also be in any Unicode language and script. This is true even when the tools and environment are in another language. For example, in a development environment that uses English language and a Latin collation throughout the stack, you can include in your model an object that uses Cyrillic characters in its name.|  
|Implementing translation support|Create using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to generate translation files that you fill in and then import back into the model.<br /><br /> See [Translations in Tabular models &#40;Analysis Services&#41;](../analysis-services/tabular-models/translations-in-tabular-models-analysis-services.md) for details.|Create using[!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to define the translations for the caption, description, and account types for cubes and measures, dimension and attributes.<br /><br /> See [Translations in Multidimensional Models &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/translations-in-multidimensional-models-analysis-services.md) for more information. A lesson on how to use this feature can be found in [Lesson 9: Defining Perspectives and Translations](../analysis-services/lesson-9-defining-perspectives-and-translations.md) of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] tutorial.|  
|Currency conversion|Not available.|Currency conversion is through specialized MDX scripts that convert measures containing currency data. You can use the Business Intelligence Wizard in [!INCLUDE[ss_dtbi](../includes/ss-dtbi-md.md)] to generate an MDX script that uses a combination of data and metadata from dimensions, attributes, and measure groups to convert measures containing currency data. See [Currency Conversions &#40;Analysis Services&#41;](../analysis-services/currency-conversions-analysis-services.md).|  
  
## See Also  
 [Translation support in Analysis Services](../analysis-services/translation-support-in-analysis-services.md)   
 [Internationalization for Windows Applications](http://msdn.microsoft.com/library/windows/desktop/dd318661%28v=vs.85%29.aspx)   
 [Globalization](/globalization/)   
 [Writing Windows Store apps with locale-based adaptive design](https://blogs.windows.com/buildingapps/2014/03/06/writing-windows-store-apps-with-locale-based-adaptive-design/)   
 [Developing Universal Windows Apps with C# and XAML](http://www.microsoftvirtualacademy.com/training-courses/developing-universal-windows-apps-with-c-and-xaml)  
  
  
