---
title: "Windows Collation Name (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Windows collations [SQL Server]"
  - "names [SQL Server], collations"
  - "collations [SQL Server], Windows collations"
  - "Collation Designator"
ms.assetid: acceef84-2c68-46e2-a021-be019b7ab14e
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Windows Collation Name (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Specifies the Windows collation name in the COLLATE clause in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The Windows collation name is composed of the collation designator and the comparison styles.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
<Windows_collation_name> :: =   
CollationDesignator_<ComparisonStyle>  
  
<ComparisonStyle> :: =   
{ CaseSensitivity_AccentSensitivity  [ _KanatypeSensitive ] [ _WidthSensitive ]    
}  
| { _BIN | _BIN2 }  
```  
  
## Arguments  
 *CollationDesignator*  
 Specifies the base collation rules used by the Windows collation. The base collation rules cover the following:  
  
-   The sorting rules that are applied when dictionary sorting is specified. Sorting rules are based on alphabet or language.  
  
-   The code page used to store non-Unicode character data.  
  
 Some examples are:  
  
-   Latin1_General or French: both use code page 1252.  
  
-   Turkish: uses code page 1254.  
  
 *CaseSensitivity*  
 **CI** specifies case-insensitive, **CS** specifies case-sensitive.  
  
 *AccentSensitivity*  
 **AI** specifies accent-insensitive, **AS** specifies accent-sensitive.  
  
 *KanatypeSensitive*  
 **Omitted** specifies kanatype-insensitive, **KS** specifies kanatype-sensitive.  
  
 *WidthSensitivity*  
 **Omitted** specifies width-insensitive, **WS** specifies width-sensitive.  
  
 **BIN**  
 Specifies the backward-compatible binary sort order to be used.  
  
 **BIN2**  
 Specifies the binary sort order that uses code-point comparison semantics.  
  
## Remarks  
 Depending on the version of the collations some code points may be undefined. For example compare:  
  
```  
SELECT LOWER(nchar(504) COLLATE Latin1_General_CI_AS);   
SELECT LOWER (nchar(504) COLLATE Latin1_General_100_CI_AS);  
GO  
```  
  
 The first line returns an uppercase character when the collation is Latin1_General_CI_AS, because this code point is undefined in this collation.  
  
 When working with some languages, it can be critical to avoid the older collations. For example, this is true for Telegu.  
  
 In some cases Windows collations and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations can generate different query plans for the same query.  
  
## Examples  
 The following are some examples of Windows collation names:  
  
-   **Latin1_General_100_**  
  
 Collation uses the Latin1 General dictionary sorting rules, code page 1252. Is case-insensitive and accent-sensitive. Collation uses the Latin1 General dictionary sorting rules and maps to code page 1252. Shows the version number of the collation if it is a Windows collation: _90 or _100. Is case-insensitive (CI), and accent-sensitive (AS).  
  
-   **Estonian_CS_AS**  
  
     Collation uses the Estonian dictionary sorting rules, code page 1257. Is case-sensitive and accent-sensitive.  
  
-   **Latin1_General_BIN**  
  
     Collation uses code page 1252 and binary sorting rules. The Latin1 General dictionary sorting rules are ignored.  
  
## Windows Collations  
 To list the Windows collations supported by your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], execute the following query.  
  
```  
SELECT * FROM sys.fn_helpcollations() WHERE name NOT LIKE 'SQL%';  
```  
  
 The following table lists all Windows collations supported in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
|Windows locale|Collation Version 100|Collation Version 90|  
|--------------------|---------------------------|--------------------------|  
|Alsatian (France)|Latin1_General_100_|Not available|  
|Amharic (Ethiopia)|Latin1_General_100_|Not available|  
|Armenian (Armenia)|Cyrillic_General_100_|Not available|  
|Assamese (India)|Assamese_100_ <sup>1</sup>|Not available|  
|Bashkir (Russia)|Bashkir_100_|Not available|  
|Basque (Basque)|Latin1_General_100_|Not available|  
|Bengali (Bangladesh)|Bengali_100_<sup>1</sup>|Not available|  
|Bengali (India)|Bengali_100_<sup>1</sup>|Not available|  
|Bosnian (Bosnia and Herzegovina, Cyrillic)|Bosnian_Cyrillic_100_|Not available|  
|Bosnian (Bosnia and Herzegovina, Latin)|Bosnian_Latin_100_|Not available|  
|Breton (France)|Breton_100_|Not available|  
|Chinese (Macao SAR)|Chinese_Traditional_Pinyin_100_|Not available|  
|Chinese (Macao SAR)|Chinese_Traditional_Stroke_Order_100_|Not available|  
|Chinese (Singapore)|Chinese_Simplified_Stroke_Order_100_|Not available|  
|Corsican (France)|Corsican_100_|Not available|  
|Croatian (Bosnia and Herzegovina, Latin)|Croatian_100_|Not available|  
|Dari (Afghanistan)|Dari_100_|Not available|  
|English (India)|Latin1_General_100_|Not available|  
|English (Malaysia)|Latin1_General_100_|Not available|  
|English (Singapore)|Latin1_General_100_|Not available|  
|Filipino (Philippines)|Latin1_General_100_|Not available|  
|Frisian (Netherlands)|Frisian_100_|Not available|  
|Georgian (Georgia)|Cyrillic_General_100_|Not available|  
|Greenlandic (Greenland)|Danish_Greenlandic_100_|Not available|  
|Gujarati (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Hausa (Nigeria, Latin)|Latin1_General_100_|Not available|  
|Hindi (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Igbo (Nigeria)|Latin1_General_100_|Not available|  
|Inuktitut (Canada, Latin)|Latin1_General_100_|Not available|  
|Inuktitut (Syllabics) Canada|Latin1_General_100_|Not available|  
|Irish (Ireland)|Latin1_General_100_|Not available|  
|Japanese (Japan XJIS)|Japanese_XJIS_100_|Japanese_90_, Japanese_|  
|Japanese (Japan)|Japanese_Bushu_Kakusu_100_|Not available|  
|Kannada (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Khmer (Cambodia)|Khmer_100_<sup>1</sup>|Not available|  
|K'iche (Guatemala)|Modern_Spanish_100_|Not available|  
|Kinyarwanda (Rwanda)|Latin1_General_100_|Not available|  
|Konkani (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Lao (Lao PDR)|Lao_100_<sup>1</sup>|Not available|  
|Lower Sorbian (Germany)|Latin1_General_100_|Not available|  
|Luxembourgish (Luxembourg)|Latin1_General_100_|Not available|  
|Malayalam (India)|Indic_General_100_<sup>1</sup>|Not available|  
|Maltese (Malta)|Maltese_100_|Not available|  
|Maori (New Zealand)|Maori_100_|Not available|  
|Mapudungun (Chile)|Mapudungan_100_|Not available|  
|Marathi (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Mohawk (Canada)|Mohawk_100_|Not available|  
|Mongolian (PRC)|Cyrillic_General_100_|Not available|  
|Nepali (Nepal)|Nepali_100_<sup>1</sup>|Not available|  
|Norwegian (Bokmål, Norway)|Norwegian_100_|Not available|  
|Norwegian (Nynorsk, Norway)|Norwegian_100_|Not available|  
|Occitan (France)|French_100_|Not available|  
|Oriya (India)|Indic_General_100_<sup>1</sup>|Not available|  
|Pashto (Afghanistan)|Pashto_100_<sup>1</sup>|Not available|  
|Persian (Iran)|Persian_100_|Not available|  
|Punjabi (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Quechua (Bolivia)|Latin1_General_100_|Not available|  
|Quechua (Ecuador)|Latin1_General_100_|Not available|  
|Quechua (Peru)|Latin1_General_100_|Not available|  
|Romansh (Switzerland)|Romansh_100_|Not available|  
|Sami (Inari, Finland)|Sami_Sweden_Finland_100_|Not available|  
|Sami (Lule,Norway)|Sami_Norway_100_|Not available|  
|Sami (Lule, Sweden)|Sami_Sweden_Finland_100_|Not available|  
|Sami (Northern, Finland)|Sami_Sweden_Finland_100_|Not available|  
|Sami (Northern,Norway)|Sami_Norway_100_|Not available|  
|Sami (Northern, Sweden)|Sami_Sweden_Finland_100_|Not available|  
|Sami (Skolt, Finland)|Sami_Sweden_Finland_100_|Not available|  
|Sami (Southern, Norway)|Sami_Norway_100_|Not available|  
|Sami (Southern, Sweden)|Sami_Sweden_Finland_100_|Not available|  
|Sanskrit (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Serbian (Bosnia and Herzegovina, Cyrillic)|Serbian_Cyrillic_100_|Not available|  
|Serbian (Bosnia and Herzegovina, Latin)|Serbian_Latin_100_|Not available|  
|Serbian (Serbia, Cyrillic)|Serbian_Cyrillic_100_|Not available|  
|Serbian (Serbia, Latin)|Serbian_Latin_100_|Not available|  
|Sesotho sa Leboa/Northern Sotho (South Africa)|Latin1_General_100_|Not available|  
|Setswana/Tswana (South Africa)|Latin1_General_100_|Not available|  
|Sinhala (Sri Lanka)|Indic_General_100_<sup>1</sup>|Not available|  
|Swahili (Kenya)|Latin1_General_100_|Not available|  
|Syriac (Syria)|Syriac_100_<sup>1</sup>|Syriac_90_|  
|Tajik (Tajikistan)|Cyrillic_General_100_|Not available|  
|Tamazight (Algeria, Latin)|Tamazight_100_|Not available|  
|Tamil (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Telugu (India)|Indic_General_100_<sup>1</sup>|Indic_General_90_|  
|Tibetan (PRC)|Tibetan_100_<sup>1</sup>|Not available|  
|Turkmen (Turkmenistan)|Turkmen_100_|Not available|  
|Uighur (PRC)|Uighur_100_|Not available|  
|Upper Sorbian (Germany)|Upper_Sorbian_100_|Not available|  
|Urdu (Pakistan)|Urdu_100_|Not available|  
|Welsh (United Kingdom)|Welsh_100_|Not available|  
|Wolof (Senegal)|French_100_|Not available|  
|Xhosa/isiXhosa (South Africa)|Latin1_General_100_|Not available|  
|Yakut (Russia)|Yakut_100_|Not available|  
|Yi (PRC)|Latin1_General_100_|Not available|  
|Yoruba (Nigeria)|Latin1_General_100_|Not available|  
|Zulu/isiZulu (South Africa)|Latin1_General_100_|Not available|  
|Deprecated, not available at server level in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later|Hindi|Hindi|  
|Deprecated, not available at server level in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later|Korean_Wansung_Unicode|Korean_Wansung_Unicode|  
|Deprecated, not available at server level in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later|Lithuanian_Classic|Lithuanian_Classic|  
|Deprecated, not available at server level in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later|Macedonian|Macedonian|  
  
 <sup>1</sup>Unicode-only Windows collations can only be applied to column-level or expression-level data. They cannot be used as server or database collations.  
  
 <sup>2</sup>Like the Chinese (Taiwan) collation, Chinese (Macau) uses the rules of Simplified Chinese; unlike Chinese (Taiwan), it uses code page 950.  
  
## See Also  
 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [Constants &#40;Transact-SQL&#41;](../../t-sql/data-types/constants-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md?&tabs=sqlserver)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [table &#40;Transact-SQL&#41;](../../t-sql/data-types/table-transact-sql.md)   
 [sys.fn_helpcollations &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)  
  
  
