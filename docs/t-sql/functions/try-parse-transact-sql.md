---
title: "TRY_PARSE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "TRY_PARSE_TSQL"
  - "TRY_PARSE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TRY_PARSE function"
ms.assetid: 292bac1d-edd8-468c-8ff1-8c7de625bc55
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# TRY_PARSE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns the result of an expression, translated to the requested data type, or null if the cast fails in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use TRY_PARSE only for converting from string to date/time and number types.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
TRY_PARSE ( string_value AS data_type [ USING culture ] )  
```  
  
## Arguments  
 *string_value*  
 **nvarchar(4000)** value representing the formatted value to parse into the specified data type.  
  
 *string_value* must be a valid representation of the requested data type, or TRY_PARSE returns null.  
  
 *data_type*  
 Literal representing the data type requested for the result.  
  
 *culture*  
 Optional string that identifies the culture in which *string_value* is formatted.  
  
 If the *culture* argument is not provided, the language of the current session is used. This language is set either implicitly or explicitly by using the SET LANGUAGE statement. *culture* accepts any culture supported by the .NET Framework; it is not limited to the languages explicitly supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the *culture* argument is not valid, PARSE raises an error.  
  
## Return Types  
 Returns the result of the expression, translated to the requested data type, or null if the cast fails.  
  
## Remarks  
 Use TRY_PARSE only for converting from string to date/time and number types. For general type conversions, continue to use CAST or CONVERT. Keep in mind that there is a certain performance overhead in parsing the string value.  
  
 TRY_PARSE relies on the presence of .the .NET Framework Common Language Runtime (CLR).  
  
 This function will not be remoted since it depends on the presence of the CLR. Remoting a function that requires the CLR would cause an error on the remote server.  
  
 **More information about the data_type parameter**  
  
 The values for the *data_type* parameter are restricted to the types shown in the following table, together with styles. The style information is provided to help determine what types of patterns are allowed. For more information on styles, see the .NET Framework documentation for the **System.Globalization.NumberStyles** and **DateTimeStyles** enumerations.  
  
|Category|Type|.NET type|Styles used|  
|--------------|----------|---------------|-----------------|  
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
  
### A. Simple example of TRY_PARSE  
  
```  
SELECT TRY_PARSE('Jabberwokkie' AS datetime2 USING 'en-US') AS Result;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------  
NULL  
  
(1 row(s) affected)  
```  
  
### B. Detecting nulls with TRY_PARSE  
  
```  
SELECT  
    CASE WHEN TRY_PARSE('Aragorn' AS decimal USING 'sr-Latn-CS') IS NULL  
        THEN 'True'  
        ELSE 'False'  
END  
AS Result;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------  
True  
  
(1 row(s) affected)  
```  
  
### C. Using IIF with TRY_PARSE and implicit culture setting  
  
```  
SET LANGUAGE English;  
SELECT IIF(TRY_PARSE('01/01/2011' AS datetime2) IS NULL, 'True', 'False') AS Result;  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
---------------  
False  
  
(1 row(s) affected)  
```  
  
## See Also  
 [PARSE &#40;Transact-SQL&#41;](../../t-sql/functions/parse-transact-sql.md)   
 [Conversion Functions &#40;Transact-SQL&#41;](../../t-sql/functions/conversion-functions-transact-sql.md)   
 [TRY_CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/try-convert-transact-sql.md)   
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  
