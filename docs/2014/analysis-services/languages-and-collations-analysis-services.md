---
title: "Languages and Collations (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Windows collations [Analysis Services]"
  - "default collations"
  - "languages [Analysis Services]"
  - "sort orders [Analysis Services]"
  - "language identifiers [Analysis Services]"
  - "default languages"
  - "collations [Analysis Services]"
ms.assetid: 666cf8a7-223b-4be5-86c0-7fe2bcca0d09
author: minewiskan
ms.author: owend
manager: craigg
---
# Languages and Collations (Analysis Services)
  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports the languages and collations provided by [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows operating systems. `Language` and `Collation` properties are initially set at the instance level during installation, but can be changed afterwards at different levels of the object hierarchy.  
  
 In a multidimensional model (only), you can set these properties on a database or cube - you can also set them on translations that you create for objects within a cube.  
  
 When setting `Language` and `Collation`, you are either specifying settings used by the data model during processing and query execution, or (for multidimensional models only) you are equipping a model with multiple translations so that foreign language speakers can work with the model in their native language. Explicitly setting `Language` and `Collation` properties on an object (database, model, or cube) is for situations where the development environment and production server are configured for different locales, and you want to be sure that the language and collation matches those of the intended target environment.  
  
 This topic includes these sections:  
  
-   [Objects that support Language and Collation properties](#bkmk_object)  
  
-   [Language support in Analysis Services](#bkmk_lang)  
  
-   [Collation support in Analysis Services](#bkmk_collations)  
  
-   [Change the default language or collation on the instance](#bkmk_defaultLang)  
  
-   [Change the language or collation on a cube](#bkmk_cube)  
  
-   [Change language and collation within a data model using XMLA](#bkmk_XMLA)  
  
-   [Boost performance for English locales through EnableFast1033Locale](#bkmk_enablefast1033)  
  
-   [GB18030 Support in Analysis Services](#bkmk_gb18030)  
  
##  <a name="bkmk_object"></a> Objects that support Language and Collation properties  
 `Language` and `Collation` properties are often exposed together - where you can set `Language`, you can also set `Collation`.  
  
 You can set `Language` and `Collation` on these objects:  
  
-   **Instance**. All projects deployed to the instance will adopt the language and collation of the instance, assuming the language and collation are undefined. By default, a multidimensional model leaves language and collation empty. When the project is deployed, the resulting database and cubes get the language and collation of the instance.  
  
     Initially, language and collation properties are established during setup but an administrator can override them in Management Studio. See [Change the default language or collation on the instance](#bkmk_defaultLang) for details.  
  
-   **Database**. To break inheritance, you can explicitly set language and collation at the project level that is used by all cubes contained in the database. Unless you indicate otherwise, all cubes in the database will get the language and collation you specify at this level. If you routinely code and deploy to different locales (for example, developing the solution on a Chinese computer, but deploying it to a server owned by a French subsidiary), setting language and collation at the database level is the first and most important step to ensuring the solution works in the target environment. The best place to set these properties is inside the project (via the **Edit Database** command on the project).  
  
-   **Database dimension**. Although the designer exposes `Language` and `Collation` properties on a database dimension, setting properties on this object is not useful. Database dimensions are not used as standalone objects, so it could be difficult if not impossible to make use of the properties you define. When in a cube, a dimension always inherits `Language` and `Collation` from its cube parent. Any values you might have set on the standalone database dimension object are ignored.  
  
-   **Cube**. As the primary query structure, you can set language and collation at the cube level. For example, you might want to create multiple language versions of a cube, such as English and Chinese versions, within the same project, where each cube has its own language and collation.  
  
     Whatever language and collation you set on the cube is used by all measures and dimensions contained in the cube. The only way to set collation properties at a finer grain is if you are creating translations on a dimension attribute. Otherwise, assuming no translations at the attribute level, there is one collation per cube.  
  
 Additionally, you can set `Language`, by itself, on a **Translation** object.  
  
 A translation object is created when you add translations to a cube or dimension. `Language` is part of the translation definition. `Collation`, on the other hand, is set on the cube or higher, and shared by all translations. This is evident in the XMLA of a cube containing translations, where you will see multiple language properties (one for each translation), but only one collation. Note that there is one exception for dimension attribute translations, where you can override cube collation to specify an attribute collation that matches the source column (the database engine supports setting collation on individual columns, and it's common to configure individual translations to obtain member data from different source columns). But otherwise, for all other translations, `Language` is used by itself, without a `Collation` corollary. See [Translations &#40;Analysis Services&#41;](translations-analysis-services.md) for details.  
  
##  <a name="bkmk_lang"></a> Language support in Analysis Services  
 The `Language` property sets the locale of an object, used during processing, queries, and with `Captions` and `Translations` to support multi-lingual scenarios. Locales are based on a language identifier, such as English, and a territory, such as United States or Australia, that further refines date and time representations.  
  
 At the instance level, the property is set during installation and is based on the language of the Windows server operating system (one of 37 languages, assuming a language pack is installed). You cannot change the language in Setup.  
  
 Post-installation, you can override `Language` using the server properties page in Management Studio, or in the msmdsrv.ini configuration file. You can choose from many more languages, including all those supported by the Windows client. When set at the instance level, on the server, `Language` determines the locale of all databases that are subsequently deployed. For example, if you set `Language` to German, all databases that are deployed to the instance will have a Language property of 1031, the LCID for German.  
  
###  <a name="bkmk_lcid"></a> Value of the Language property is a Locale Identifier (LCID)  
 Valid values include any LCID that appears in the drop-down list. In Management Studio and SQL Server Data Tools, LCIDs are represented in string equivalents. The same languages show up wherever the `Language` property is exposed, regardless of the tool. Having an identical list of languages ensures that you can implement and test translations consistently throughout the model.  
  
 Although Analysis Services lists the languages by name, the actual value stored for the property is an LCID. When setting a language property programmatically or through the msmdsrv.ini file, use the [locale identifier (LCID)](http://en.wikipedia.org/wiki/Locale) as the value. An LCID is a 32-bit value consisting of a language ID, sort ID, and reserved bits that identify a particular language. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses LCIDs to specify the selected language for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instances and objects.  
  
 You can set the LCID using either hexadecimal or decimal formats. A few examples of valid values for the `Language` property include:  
  
-   0x0409 or 1033 for **English (United States)**  
  
-   0x0411 or 1041 for **Japanese**  
  
-   0x0407 or 1031 for **Germany (German)**  
  
-   0x0416 or 1046 for **Portuguese (Brazil)**.  
  
 To view a longer list, see [Locale IDs Assigned by Microsoft](https://msdn.microsoft.com/goglobal/bb964664.aspx). For more background, see [Encoding and Code Pages](https://msdn.microsoft.com/goglobal/bb688114.aspx).  
  
> [!NOTE]  
>  The `Language` property does not determine the language for returning system messages, or which strings appear in the user interface. Errors, warnings, and messages are localized into all languages supported in Office and Office 365 and are used automatically when the client connection specifies one of the supported locales.  
  
##  <a name="bkmk_collations"></a> Collation support in Analysis Services  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses Windows and binary collations exclusively. It does not use legacy SQL Server collations. Within a cube, a single collation is used throughout, with the exception of translations at the attribute level. For more information about defining attribute translations, see [Translations &#40;Analysis Services&#41;](translations-analysis-services.md).  
  
 Collations control the case-sensitivity of all strings in a bicameral language script, with the exception of object identifiers. If you use upper and lower case characters in an object identifier, be forewarned that the case-sensitivity of object identifiers is not determined by the collation, but by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. For object identifiers composed in English script, object identifiers are always case-insensitive, regardless of collation. Cyrillic and other bicameral languages do the opposite (always case-sensitive). See [Globalization Tips and Best Practices &#40;Analysis Services&#41;](globalization-tips-and-best-practices-analysis-services.md) for details.  
  
 Collation in Analysis Services is compatible with that of the SQL Server relational database engine, assuming you maintain parity in the sort options you select for each service. For example, if the relational database is accent sensitive, you should configure the cube the same way. Problems can occur when collations settings diverge. For an example and workarounds, see [Blanks in a Unicode string have different processing outcomes based on collation](https://social.technet.microsoft.com/wiki/contents/articles/23979.ssas-processing-error-blanks-in-a-unicode-string-have-different-processing-outcomes-based-on-collation-and-character-set.aspx). To learn more about collation and the database engine, see [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
###  <a name="bkmk_collationtype"></a> Collation Types  
 Analysis Services supports two collation types:  
  
-   **Windows collations**  
  
     Windows collations sort characters based on the linguistic and cultural characteristics of the language. In Windows, collations outnumber the locales (or languages) used with them, due to many languages sharing common alphabets and rules for sorting and comparing characters. For example, 33 Windows locales, including all the Portuguese and English Windows locales, use the Latin1 code page (1252) and follow a common set of rules for sorting and comparing characters.  
  
-   **Binary collations (either BIN or BIN2)**  
  
     Binary collations sort on Unicode code points, not on linguistic values. For example, Latin1_General_BIN and Japanese_BIN yield identical sorting results when used on Unicode data. Whereas a linguistic sort might yield results like aAbBcCdD, a binary sort would be ABCDabcd because the code point of all uppercase characters is collectively higher than the code points of the lowercase characters.  
  
###  <a name="bkmk_sortorder"></a> Sort Order Options  
 Sort options are used to refine sorting and comparison rules based on case, accent, kana, and width sensitivity. For example, the default value of the `Collation` configuration property for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] is Latin1_General_AS_CS, specifying that the Latin1_General collation is used, with an accent-sensitive, case-sensitive sort order.  
  
 Note that BIN and BIN2 are mutually exclusive of other sort options, if you want to use BIN or BIN2, clear the sort option for Accent Sensitive. Similarly, when BIN2 is selected, the case-sensitive, case-insensitive, accent-sensitive, accent-insensitive, kana-sensitive, and width-sensitive options are not available.  
  
 The following table describes Windows collation sort order options and associated suffixes for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
|Sort order (suffix)|Sort order description|  
|---------------------------|----------------------------|  
|Binary (_BIN) or BIN2 (_BIN2)|There are two types of binary collations in SQL Server; the older BIN collations and the newer BIN2 collations. In a BIN2 collation all characters are sorted according to their code points. In a BIN collation only the first character is sorted according to the code point, and remaining characters are sorted according to their byte values. (Because the Intel platform is a little endian architecture, Unicode code characters are always stored byte-swapped.)<br /><br /> For binary collations on Unicode data types, the locale is not considered in data sorts. For example, Latin_1_General_BIN and Japanese_BIN yield identical sorting results when they are used on Unicode data.<br /><br /> Binary sort order is case sensitive and accent sensitive. Binary is also the fastest sorting order.|  
|Case-sensitive (_CS)|Distinguishes between uppercase and lowercase letters. If selected, lowercase letters sort ahead of their uppercase versions. You can explicitly set case-insensitivity by specifying _CI. Collation-specific case settings do not apply to object identifiers, such as the ID of a dimension, cube, and other objects. See [Globalization Tips and Best Practices &#40;Analysis Services&#41;](globalization-tips-and-best-practices-analysis-services.md) for details.|  
|Accent-sensitive (_AS)|Distinguishes between accented and unaccented characters. For example, 'a' is not equal to 'ấ'. If this option is not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] considers the accented and unaccented versions of letters to be identical for sorting purposes. You can explicitly set accent-insensitivity by specifying _AI.|  
|Kana-sensitive (_KS)|Distinguishes between the two types of Japanese kana characters: hiragana and katakana. If this option is not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] considers hiragana and katakana characters to be equal for sorting purposes. There is no sort order suffix for kana-insensitive sorting.|  
|Width-sensitive (_WS)|Distinguishes between a single-byte character and the same character when represented as a double-byte character. If this option is not selected, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] considers the single-byte and double-byte representation of the same character to be identical for sorting purposes. There is no sort order suffix for width-insensitive sorting.|  
  
##  <a name="bkmk_defaultLang"></a> Change the default language or collation on the instance  
 Default language and collation is established during setup, but can be changed as part of post-installation configuration. Changing the collation at the instance level is non-trivial and comes with these requirements:  
  
-   A service restart.  
  
-   Update the collation settings of existing objects. Collation settings are inherited once when the object is created. Subsequent changes to collation must be done manually. See [Change language and collation within a data model using XMLA](#bkmk_XMLA) for tips on how to propagate collation changes throughout the model.  
  
-   Reprocess partitions and dimensions after the collation is updated.  
  
 You can use SQL Server Management Studio or AMO PowerShell to change the default language or collation at the server level. Alternatively, you can modify the **\<Language>** and **\<CollationName>** settings in the msmdsrv.ini file, specifying the LCID of the language.  
  
1.  In Management Studio, right-click server name | **Properties** | **Language/Collation**.  
  
2.  Choose the sort options. To select either **Binary** or **Binary 2**, first clear the checkbox for **Accent Sensitive**.  
  
     Note that collation and language are fully independent settings. If you change one, the values of the other are not filtered to show common combinations.  
  
3.  Update the data model to use the new collation (see the following section).  
  
4.  Restart the service.  
  
##  <a name="bkmk_cube"></a> Change the language or collation on a cube  
  
1.  In Solution Explorer, double-click the cube to open it in cube designer.  
  
2.  In either the Measures or Dimensions pane, select the top node. The top-level object for either pane is the cube.  
  
3.  In Properties, set `Language` and `Collation`. The values you choose will be used by all cube objects, including cube dimensions and measures, and affects processing and query operations.  
  
     The only way to embed alternate language and collation properties on objects within the cube is through translations. See [Translations &#40;Analysis Services&#41;](translations-analysis-services.md) for details.  
  
##  <a name="bkmk_XMLA"></a> Change language and collation within a data model using XMLA  
 Language and collation settings are inherited once when the object is created. Subsequent changes to those properties must be done manually. One approach for changing collation multiple objects quickly is to use an ALTER command on an XMLA script.  
  
 By default, collation is set once, at the database level. Inheritance is implied throughout the rest of the object hierarchy. If you explicitly set `Collation` on objects within the cube, which is allowed on individual dimension attributes, it will appear in the XMLA definition. Otherwise, only the top-level collation property exists.  
  
 Before using XMLA to modify an existing database, make sure that you're not introducing discrepancies between the database and the source files used to build it. For example, you might want to use XMLA to quickly change language or collation for proof-of-concept testing, but then follow-up with changes to the source file (see [Change the language or collation on a cube](#bkmk_cube)), redeploying the solution using the existing operating procedures already in place.  
  
1.  In Management Studio, right-click the database | **Script Database as** | **ALTER To** | **New Query Editor Window**.  
  
2.  Search and replace the existing language or collation with an alternative value.  
  
3.  Press F5 to execute the script.  
  
4.  Reprocess the cube.  
  
##  <a name="bkmk_enablefast1033"></a> Boost performance for English locales through EnableFast1033Locale  
 If you use the English (United States) language identifier (0x0409, or 1033) as the default language for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance, you can get additional performance benefits by setting the `EnableFast1033Locale` configuration property, an advanced configuration property available only for that language identifier. Setting the value of this property to **true** enables [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to use a faster algorithm for string hashing and comparison. For more information about setting configuration properties, see [Configure Server Properties in Analysis Services](server-properties/server-properties-in-analysis-services.md).  
  
##  <a name="bkmk_gb18030"></a> GB18030 Support in Analysis Services  
 GB18030 is a separate standard used in the People's Republic of China for encoding Chinese characters. In GB18030, characters can be 1, 2, or 4 bytes in length. In Analysis Services, there is no data conversion when processing data from external sources. The data is simply stored as Unicode. At query time, a GB18030 conversion is performed through the Analysis Services client libraries (specifically, the MSOLAP.dll OLE DB provider) when text data is returned in query results, based on the client OS settings. The database engine also supports GB18030. For details, see [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).  
  
## See Also  
 [Globalization scenarios for Analysis Services Multiidimensional](globalization-scenarios-for-analysis-services-multiidimensional.md)   
 [Globalization Tips and Best Practices &#40;Analysis Services&#41;](globalization-tips-and-best-practices-analysis-services.md)   
 [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md)  
  
  
