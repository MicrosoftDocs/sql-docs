---
title: "String storage and collation in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# String storage and collation in tabular models
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Strings (text values) are stored in a highly compressed format in tabular models; because of this compression, you can get unexpected results when you retrieve entire or partial strings. Also, because string locale and collations are inherited hierarchically from the closest parent object, if the string language is not explicitly defined, the locale and collation of the parent can affect how each string is stored and whether the string is unique or conflated with similar strings as defined by the parent collation.  
  
 This article describes the mechanism by which strings are compressed and stored, and provides examples of how collation and language affect the results of text formulas in tabular models.  
  
## Storage  
 In tabular models all data is highly compressed to better fit in memory. As a consequence, all strings that can be considered lexically equivalent are stored only once. The first instance of the string is used as the canonical representation and thereafter each equivalent string is indexed to the same compressed value as the first occurrence.  
  
 The key question is: what constitutes a lexically equivalent string? Two strings are considered lexically equivalent if they can be considered as the same word. For example, in English when you search for the word **violin** in a dictionary, you might find the entry **Violin** or **violin**, depending on the editorial policy of the dictionary, but generally you consider both words equivalent, and disregard the difference in capitalization. In a tabular model, the factor that determines whether two strings are lexically equivalent is not editorial policy or even user preference, but the locale and collation order assigned to the column.  
  
 Therefore, the decision of whether uppercase and lowercase letters should be handled as the same or different depends on the collation and locale. For any particular word within that locale, the first occurrence of the word that is found within a particular column therefore serves as the canonical representation of that word and that string is stored in uncompressed format.  All other strings are tested against the first occurrence, and if they match the equivalence test, they are assigned to the compressed value of the first occurrence. Later, when the compressed values are retrieved they are represented using the uncompressed value of the first occurrence of the string.  
  
 An example will help to clarify how this works. The following column "Classification - English" was extracted from a table that contains information about plants and trees. For each plant (the names of the plants are not shown here) the classification column shows the general category of plant.  
  
|Classification - English|  
|-------------------------------|  
|trEE|  
|PlAnT|  
|TREE|  
|PLANT|  
|Plant|  
|Tree|  
|plant|  
|tReE|  
|tree|  
|pLaNt|  
|tREE|  
  
 Perhaps the data came from many different sources, and so the casing and use of accents was inconsistent, and the relational database stored those differences as is. But in general the values are either **Plant** or **Tree**, just with different casing.  
  
 When these values are loaded into a tabular model that uses the default collation and sorting order for American English, case is not important, so only two values would be stored for the entire column:  
  
|Classification - English|  
|-------------------------------|  
|trEE|  
|PlAnT|  
  
 If you use the column, **Classification - English**, in your model, wherever you display plant classification you will see not the original values, with their various uses of upper and lower case, but only the first instance. The reason is that all the uppercase and lowercase variants of **tree** are considered equivalent in this collation and locale; therefore, only one string was preserved and the first instance of that string that is encountered by the system is the one that is saved.  
  
> [!WARNING]  
>  You might decide that you want to define which string will be the first to store, according to what you consider correct, but this could be very hard to so. There is no simple way to determine in advance which row should be processed first by the engine, given that all values are considered to be the same. Instead, if you need to set the standard value, you should cleanse all your strings before loading the model.  
  
## Locale and Collation Order  
 When comparing strings (text values), what defines equivalence is normally the cultural aspect of how such strings are interpreted. In some cultures an accent or the capitalization of a character can completely change the meaning of the string; therefore, typically such differences are considered when determining equivalency for any particular language or region.  
  
 Usually, when you use your computer it is already configured to match your own cultural expectations and linguistic behavior, and string operations such as sorting and comparing text values behaves as you would expect. The settings that control language-specific behavior are defined through the **Locale and Regional** settings in Windows. Applications read those settings and change their behavior accordingly. In some cases, an application might have a feature that allows you to change the cultural behavior of the application or the way in which strings are compared.  
  
 When you are creating a tabular model database, by default the database inherits these cultural and linguistic settings in the form of a language identifier and collation.  
  
-   The language identifier defines the character set you want to use for your strings according to your culture.  
  
-   The collation defines the ordering of the characters and their equivalence.  
  
 It is important to note that a language identifier not only identifies a language but, also the country or region where the language is used. Each language identifier also has a default collation specification. For more information about language identifiers, see [Locale IDs Assigned by Microsoft](http://msdn.microsoft.com/goglobal/bb964664.aspx). You can use the LCID Dec column to get the correct ID when manually inserting a value. For more information about the SQL concept of collations, see [COLLATE &#40;Transact-SQL&#41;](../../t-sql/statements/collations.md). For information about the collation designators and the comparison styles for Windows collation names, see [Windows Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/windows-collation-name-transact-sql.md). The topic, [SQL Server Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/sql-server-collation-name-transact-sql.md), maps the Windows collation names to the names used for SQL.  
  
 Once your tabular model database has been created, all new objects in the model will inherit the language and collation attributes from the database attributes. This is true for all objects. The inheritance path begins at the object, looks at the parent for any language and collation attributes to inherit, and if none are found, continues to the top and finds the language and collation attributes at the database level. In other words, if you do not specify the language and collation attributes for an object, by default, the object inherits the attributes of its closest parent.  
  
 For columns, the language and collation attributes are inherited at creation, according to the following rules:  
  
1.  The parent dimension object is searched for language and collation attributes. If both values exist, they are copied to the column attributes; if only one exists, the other is inferred from the existing one and both are assigned; if none exist, move to next step.  
  
2.  The database object is searched using the same process described in Step 1 for dimensions; if no attributes are found, move to the next step.  
  
3.  The server object is searched using the same process described in Step 1 for dimensions; if no attributes are found, the column uses the Windows language identifier and infers the collation attribute from that value.  
  
 It is important to note that typically the language identifier and collation order in the source database has little to no effect on how values are stored in the tabular model column. The exception is if the source database transforms or filters the requested values.  
  
  
