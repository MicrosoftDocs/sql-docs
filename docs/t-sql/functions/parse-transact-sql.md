---
title: "PARSE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/05/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "PARSE"
  - "PARSE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PARSE function"
ms.assetid: 6a2dbf10-f692-471b-9458-24d246963049
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# PARSE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns the result of an expression, translated to the requested data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
PARSE ( string_value AS data_type [ USING culture ] )  
```  
  
## Arguments  
 *string_value*  
 **nvarchar**(4000) value representing the formatted value to parse into the specified data type.  
  
 *string_value* must be a valid representation of the requested data type, or PARSE raises an error.  
  
 *data_type*  
 Literal value representing the data type requested for the result.  
  
 *culture*  
 Optional string that identifies the culture in which *string_value* is formatted.  
  
 If the *culture* argument is not provided, then the language of the current session is used. This language is set either implicitly, or explicitly by using the SET LANGUAGE statement. *culture* accepts any culture supported by the .NET Framework; it is not limited to the languages explicitly supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the *culture* argument is not valid, PARSE raises an error.  
  
## Return Types  
 Returns the result of the expression, translated to the requested data type.  
  
## Remarks  
 Null values passed as arguments to PARSE are treated in two ways:  
  
1.  If a null constant is passed, an error is raised. A null value cannot be parsed into a different data type in a culturally aware manner.  
  
2.  If a parameter with a null value is passed at run time, then a null is returned, to avoid canceling the whole batch.  
  
 Use PARSE only for converting from string to date/time and number types. For general type conversions, continue to use CAST or CONVERT. Keep in mind that there is a certain performance overhead in parsing the string value.  
  
 PARSE relies on the presence of the .NET Framework Common Language Runtime (CLR).  
  
 This function will not be remoted since it depends on the presence of the CLR. Remoting a function that requires the CLR would cause an error on the remote server.  
  
 **More information about the data_type parameter**  
  
 The values for the *data_type* parameter are restricted to the types shown in the following table, together with styles. The style information is provided to help determine what types of patterns are allowed. For more information on styles, see the .NET Framework documentation for the **System.Globalization.NumberStyles** and **DateTimeStyles** enumerations.  
  
|Category|Type|.NET Framework type|Styles used|  
|--------------|----------|-------------------------|-----------------|  
|Numeric|bigint|Int64|NumberStyles.Number|  
|Numeric|int|Int32|NumberStyles.Number|  
|Numeric|smallint|Int16|NumberStyles.Number|  
|Numeric|tinyint|Byte|NumberStyles.Number|  
|Numeric|decimal|Decimal|NumberStyles.Number|  
|Numeric|numeric|Decimal|NumberStyles.Number|  
|Numeric|float|Double|NumberStyles.Float|  
|Numeric|real|Single|NumberStyles.Float|  
|Numeric|smallmoney|Decimal|NumberStyles.Currency|  
|Numeric|money|Decimal|NumberStyles.Currency|  
|Date and Time|date|DateTime|DateTimeStyles.AllowWhiteSpaces &#124; DateTimeStyles.AssumeUniversal|  
|Date and Time|time|TimeSpan|DateTimeStyles.AllowWhiteSpaces &#124; DateTimeStyles.AssumeUniversal|  
|Date and Time|datetime|DateTime|DateTimeStyles.AllowWhiteSpaces &#124; DateTimeStyles.AssumeUniversal|  
|Date and Time|smalldatetime|DateTime|DateTimeStyles.AllowWhiteSpaces &#124; DateTimeStyles.AssumeUniversal|  
|Date and Time|datetime2|DateTime|DateTimeStyles.AllowWhiteSpaces &#124; DateTimeStyles.AssumeUniversal|  
|Date and Time|datetimeoffset|DateTimeOffset|DateTimeStyles.AllowWhiteSpaces &#124; DateTimeStyles.AssumeUniversal|  
  
 **More information about the culture parameter**  
  
 The following table shows the mappings from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] languages to .NET Framework cultures.  
  
|Full name|Alias|LCID|Specific culture|  
|---------------|-----------|----------|----------------------|  
|us_english|English|1033|en-US|  
|Deutsch|German|1031|de-DE|  
|Français|French|1036|fr-FR|  
|日本語|Japanese|1041|ja-JP|  
|Dansk|Danish|1030|da-DK|  
|Español|Spanish|3082|es-ES|  
|Italiano|Italian|1040|it-IT|  
|Nederlands|Dutch|1043|nl-NL|  
|Norsk|Norwegian|2068|nn-NO|  
|Português|Portuguese|2070|pt-PT|  
|Suomi|Finnish|1035|fi|  
|Svenska|Swedish|1053|sv-SE|  
|čeština|Czech|1029|Cs-CZ|  
|magyar|Hungarian|1038|Hu-HU|  
|polski|Polish|1045|Pl-PL|  
|română|Romanian|1048|Ro-RO|  
|hrvatski|Croatian|1050|hr-HR|  
|slovenčina|Slovak|1051|Sk-SK|  
|slovenski|Slovenian|1060|Sl-SI|  
|ελληνικά|Greek|1032|El-GR|  
|български|Bulgarian|1026|bg-BG|  
|русский|Russian|1049|Ru-RU|  
|Türkçe|Turkish|1055|Tr-TR|  
|British|British English|2057|en-GB|  
|eesti|Estonian|1061|Et-EE|  
|latviešu|Latvian|1062|lv-LV|  
|lietuvių|Lithuanian|1063|lt-LT|  
|Português (Brasil)|Brazilian|1046|pt-BR|  
|繁體中文|Traditional Chinese|1028|zh-TW|  
|한국어|Korean|1042|Ko-KR|  
|简体中文|Simplified Chinese|2052|zh-CN|  
|Arabic|Arabic|1025|ar-SA|  
|ไทย|Thai|1054|Th-TH|  
  
## Examples  
  
### A. PARSE into datetime2  
  
```  
SELECT PARSE('Monday, 13 December 2010' AS datetime2 USING 'en-US') AS Result;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------  
2010-12-13 00:00:00.0000000  
  
(1 row(s) affected)  
```  
  
### B. PARSE with currency symbol  
  
```  
SELECT PARSE('€345,98' AS money USING 'de-DE') AS Result;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------  
345.98  
  
(1 row(s) affected)  
```  
  
### C. PARSE with implicit setting of language  
  
```  
-- The English language is mapped to en-US specific culture  
SET LANGUAGE 'English';  
SELECT PARSE('12/16/2010' AS datetime2) AS Result;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------  
2010-12-16 00:00:00.0000000  
  
(1 row(s) affected)  
```  
  
  
