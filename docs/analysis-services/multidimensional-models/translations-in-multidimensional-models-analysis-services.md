---
title: "Translations in Multidimensional Models (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Translations in Multidimensional Models (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can define translations in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] by using the appropriate designer for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object to be translated. Defining a translation creates a **Translation** object associated with the appropriate [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object that has the specified explicit literal values, in the specified language, for the properties of the associated [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object.  
  
## Elements of a multi-lingual data model  
 A data model used in a multi-lingual solution needs more than translated labels (field names and descriptions). It also needs to provide data values that are articulated in various language scripts. Achieving a multi-lingual solution requires that you have individual attributes, bound to columns in an external database that return the data.  
  
 Adventure Works sample databases (multidimensional as well as the relational data warehouse) demonstrate the translation capabilities of Analysis Services. The sample model includes translated captions and descriptions. The sample relational data warehouse contains columns of translated values that provide localized attribute members in the model.  
  
 To view translated data values available to the model:  
  
1.  Open the Adventure Works multidimensional model in the designer.  
  
2.  In Solution Explorer, open Data Source Views and double-click Adventure Works DW\<version>.dsv.  
  
3.  Find dimDate, dimProduct, dimProductCategory, or dimProductSubcateogry. All of these dimensions contain attributes for translated members for month, day of week, product name, category name, and so on.  
  
4.  Right-click any field and select **Explore Data**. You will see English, Spanish, and French translations of each member.  
  
 Formats for date, time, and currency are not implemented through translations. To dynamically provide culturally specific formats based on the client's locale, use the Currency Conversion Wizard and **FormatString** property. See [Currency Conversions &#40;Analysis Services&#41;](../../analysis-services/currency-conversions-analysis-services.md) and [FormatString Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/formatstring-element-assl) for details.  
  
 [Lesson 9: Defining Perspectives and Translations](../../analysis-services/lesson-9-defining-perspectives-and-translations.md) in the Analysis Services Tutorial will walk you through the steps for creating and testing translations.  
  
## Defining Translations  
  
### Add translations to a cube  
 You can add translations to the cube, measure groups, measures, cube dimension, perspectives, KPIs, actions, named sets, and calculated members.  
  
1.  In Solution Explorer, double-click the cube name to open cube designer.  
  
2.  Click the **Translations** tab. All objects that support translations are listed in this page.  
  
3.  For each object, specify the target language (resolves internally to an LCID), translated caption, and translated description. The language list is consistent throughout Analysis Services, whether you are setting the server language in Management Studio, or adding a translation override on a single attribute.  
  
     Remember that you cannot change the collation. A cube essentially uses one collation, even if you're supporting multiple languages through translated captions (there is an exception for dimension attributes, discussed below). If the languages won't sort properly under the shared collation, you will need to make copies of the cube just to accommodate your collation requirements.  
  
4.  Build and deploy the project.  
  
5.  Connect to the database using a client application, such as Excel, modifying the connection string to use the locale identifier. See [Globalization Tips and Best Practices &#40;Analysis Services&#41;](../../analysis-services/globalization-tips-and-best-practices-analysis-services.md) for details.  
  
### Add translations to a dimension and attributes  
 You can add translations to database dimensions, attributes, hierarchies, and levels within a hierarchy.  
  
 Translated captions are added to the model manually using your keyboard or copy-paste, but for dimension attribute members, you can obtain translated values from an external database. Specifically, the **CaptionColumn** property of an attribute can be bound to a column in a data source view.  
  
 At the attribute level, you can override collation settings, for example you might want to adjust width-sensitivity or use a binary sort for a specific attribute. In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], collation is exposed where data bindings are defined. Because you are binding a dimension attribute translation to a different source column in the DSV, a collation setting is available so that you can specify the collation used by the source column. See [Set or Change the Column Collation](../../relational-databases/collations/set-or-change-the-column-collation.md) for details about column collation in the relational database.  
  
1.  In Solution Explorer, double-click the dimension name to open dimension designer.  
  
2.  Click the **Translations** tab. All dimension objects that support translations are listed in this page.  
  
     For each object, specify target language (resolves to an LCID), translated caption, and translated description. The language list is consistent throughout Analysis Services, whether you are setting the server language in Management Studio, or adding a translation override on a single attribute.  
  
3.  To bind an attribute to a column providing translated values:  
  
    1.  Still in Dimension Designer | **Translations**, add a new translation. Choose the language. A new column appears on the page to accept the translated values.  
  
    2.  Place the cursor in an empty cell adjacent to one of the attributes. The attribute cannot be the key, but all other attributes are viable choices. You should see a small button with a dot in it. Click the button to open the **Attribute Data Translation Dialog Box**.  
  
    3.  Enter a translation for the caption. This is used as a data label in the target language, for example as a field name in a PivotTable field list.  
  
    4.  Choose the source column that provides the translated values of attribute members. Only pre-existing columns in the table or query bound to the dimension, are available. If the column does not exist, you need to modify the data source view, dimension, and cube to pick up the column.  
  
    5.  Choose the collation and sort order, if applicable.  
  
4.  Build and deploy the project.  
  
5.  Connect to the database using a client application, such as Excel, modifying the connection string to use the locale identifier. See [Globalization Tips and Best Practices &#40;Analysis Services&#41;](../../analysis-services/globalization-tips-and-best-practices-analysis-services.md) for details.  
  
### Add a translation of the database name  
 At the database level, you can add translations for the database name and description. The translated database name might be visible on client connections that specify the LCID of the language, but that depends on the tool. For example, viewing the database in Management Studio will not show the translated name, even if you specify the locale identifier on the connection. The API used by Management Studio to connect to Analysis Services does not read the **Language** property.  
  
1.  In Solution Explorer, right-click project name | **Edit Database** to open the database designer.  
  
2.  In Translations, specify target language (resolves to an LCID), translated caption, and translated description. The language list is consistent throughout Analysis Services, whether you are setting the server language in Management Studio, or adding a translation override on a single attribute.  
  
3.  In the Properties page of the database, set **Language** to the same LCID you specified for the translation. Optionally, set the **Collation** as well if the default no longer makes sense.  
  
4.  Build and deploy the database.  
  
## Deleting Translation Objects  
 You can right-click a translation object in the dimension or cube designer to permanently remove it. You cannot restore or recycle a deleted object, so be sure to review the list of affected objects before continuing.  
  
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
  
## See Also  
 [Globalization scenarios for Analysis Services](../../analysis-services/globalization-scenarios-for-analysis-services.md)   
 [Languages and Collations &#40;Analysis Services&#41;](../../analysis-services/languages-and-collations-analysis-services.md)  
  
  
