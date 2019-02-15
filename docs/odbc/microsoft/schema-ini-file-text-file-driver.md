---
title: "Schema.ini File (Text File Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "schema.ini file [ODBC]"
  - "text file driver [ODBC], schema.ini file"
ms.assetid: 0c4625c4-c730-4984-b430-9051b7bc0451
author: MightyPen
ms.author: genemi
manager: craigg
---
# Schema.ini File (Text File Driver)
When the Text driver is used, the format of the text file is determined by using a schema information file. The schema information file is always named Schema.ini and always kept in the same directory as the text data source. The schema information file provides the IISAM with information about the general format of the file, the column name and data type information, and several other data characteristics. A Schema.ini file is always required for accessing fixed-length data. You should use a Schema.ini file when your text table contains DateTime, Currency, or Decimal data, or any time that you want more control over the handling of the data in the table.  
  
> [!NOTE]  
>  The Text ISAM will obtain initial values from the registry, not from Schema.ini. The same default file format applies to all new text data tables. All files that were created by the CREATE TABLE statement inherit those same default format values, which are set by selecting file format values in the **Define Text Format** dialog box with \<default> chosen in the **Tables** list. If the values in the registry differ from the values in Schema.ini, the values in the registry will be overwritten by the values from Schema.ini.  
  
## Understanding Schema.ini Files  
 Schema.ini files provide schema information about the records in a text file. Each Schema.ini entry specifies one of five characteristics of the table:  
  
-   The text file name  
  
-   The file format  
  
-   The field names, widths, and types  
  
-   The character set  
  
-   Special data type conversions  
  
 The following sections discuss these characteristics.  
  
## Specifying the File Name  
 The first entry in Schema.ini is always the name of the text source file enclosed in square brackets. The following example illustrates the entry for the file Sample.txt:  
  
```  
[Sample.txt]  
```  
  
## Specifying the File Format  
 The **Format** option in Schema.ini specifies the format of the text file. The Text IISAM can read the format automatically from most character-delimited files. You can use any single character as a delimiter in the file except the double quotation mark ("). The **Format** setting in Schema.ini overrides the setting in the Windows Registry, file by file. The following table lists the valid values for the **Format** option.  
  
|Format specifier|Table format|Schema.ini Format statement|  
|----------------------|------------------|---------------------------------|  
|**Tab Delimited**|Fields in the file are delimited by tabs.|Format=TabDelimited|  
|**CSV Delimited**|Fields in the file are delimited by commas (comma-separated values).|Format=CSVDelimited|  
|**Custom Delimited**|Fields in the file are delimited by any character you choose to input into the dialog box. All except the double quotation marks (") are allowed, including blank.|Format=Delimited(*custom character*)<br /><br /> -or-<br /><br /> With no delimiter specified:<br /><br /> Format=Delimited( )|  
|**Fixed Length**|Fields in the file are of a fixed length.|Format=FixedLength|  
  
## Specifying the Fields  
 You can specify field names in a character-delimited text file in two ways:  
  
-   Include the field names in the first row of the table and set **ColNameHeader** to **True.**  
  
-   Specify each column by number and designate the column name and data type.  
  
 You must specify each column by number and designate the column name, data type, and width for fixed-length files.  
  
> [!NOTE]  
>  The **ColNameHeader** setting in Schema.ini overrides the **FirstRowHasNames** setting in the Windows Registry, file by file.  
  
 The data types of the fields can also be determined. Use the **MaxScanRows** option to indicate how many rows should be scanned when determining the column types. If you set **MaxScanRows** to 0, the whole file is scanned. The **MaxScanRows** setting in Schema.ini overrides the setting in the Windows Registry, file by file.  
  
 The following entry indicates that Microsoft Jet should use the data in the first row of the table to determine field names and should examine the whole file to determine the data types used:  
  
```  
ColNameHeader=True  
MaxScanRows=0  
```  
  
 The next entry designates fields in a table by using the column number (**Col**_n_) option, which is optional for character-delimited files and required for fixed-length files. The example shows the Schema.ini entries for two fields, a 10-character CustomerNumber text field and a 30-character CustomerName text field:  
  
```  
Col1=CustomerNumber Text Width 10  
Col2=CustomerName Text Width 30  
```  
  
 The syntax of **Col**_n_ is:  
  
```  
  
n=ColumnNametype [#]  
```  
  
## Remarks  
 The following table describes each part of the **Col**_n_ entry.  
  
|Parameter|Description|  
|---------------|-----------------|  
|*ColumnName*|The text name of the column. If the column name contains embedded spaces, you must enclose it in double quotation marks.|  
|*type*|Data types are as follows:<br /><br /> **Microsoft Jet data types**<br /><br /> Bit<br /><br /> Byte<br /><br /> Short<br /><br /> Long<br /><br /> Currency<br /><br /> Single<br /><br /> Double<br /><br /> DateTime<br /><br /> Text<br /><br /> Memo<br /><br /> **ODBC data types** Char (same as Text)<br /><br /> Float (same as Double)<br /><br /> Integer (same as Short)<br /><br /> LongChar (same as Memo)<br /><br /> Date *date format*|  
|**Width**|The literal string value `Width`. Indicates that the following number designates the width of the column (optional for character-delimited files; required for fixed-length files).|  
|*#*|The integer value that designates the width of the column (required if **Width** is specified).|  
  
## Selecting a Character Set  
 You can select from two character sets: ANSI and OEM. The **CharacterSet** setting in Schema.ini overrides the setting in the Windows Registry, file by file. The following example shows the Schema.ini entry that sets the character set to ANSI:  
  
```  
CharacterSet=ANSI  
```  
  
## Specifying Data Type Formats and Conversions  
 The Schema.ini file contains several options that you can use to specify how data is converted or displayed. The following table lists each of these options.  
  
|Option|Description|  
|------------|-----------------|  
|**DateTimeFormat**|Can be set to a format string that indicates dates and times. You should specify this entry if all date/time fields in the import/export are handled with the same format. All Microsoft Jet formats except A.M. and P.M. are supported. If there is no format string, the Windows Control Panel short date picture and time options are used.|  
|**DecimalSymbol**|Can be set to any single character that is used to separate the integer from the fractional part of a number.|  
|**NumberDigits**|Indicates the number of decimal digits in the fractional portion of a number.|  
|**NumberLeadingZeros**|Specifies whether a decimal value less than 1 and more than -1 should contain leading zeros; this value can be either False (no leading zeros) or True.|  
|**CurrencySymbol**|Indicates the currency symbol that can be used for currency values in the text file. Examples include the dollar sign ($) and Dm.|  
|**CurrencyPosFormat**|Can be set to any of the following values:<br /><br /> -   Currency symbol prefix with no separation ($1)<br />-   Currency symbol suffix with no separation (1$)<br />-   Currency symbol prefix with one character separation ($ 1)<br />-   Currency symbol suffix with one character separation (1 $)|  
|**CurrencyDigits**|Specifies the number of digits used for the fractional part of a currency amount.|  
|**CurrencyNegFormat**|Can be one of the following values:<br /><br /> -   ($1)<br />-   -$1<br />-   $-1<br />-   $1-<br />-   (1$)<br />-   -1$<br />-   1-$<br />-   1$-<br />-   -1 $<br />-   -$ 1<br />-   1 $-<br />-   $ 1-<br />-   $ -1<br />-   1- $<br />-   ($ 1)<br />-   (1 $)<br /><br /> This example shows the dollar sign, but you should replace it with the appropriate **CurrencySymbol** value in the actual program.|  
|**CurrencyThousandSymbol**|Indicates the single-character symbol that can be used for separating currency values in the text file by thousands.|  
|**CurrencyDecimalSymbol**|Can be set to any single character that is used to separate the whole from the fractional part of a currency amount.|  
  
> [!NOTE]  
>  If you omit an entry, the default value in the Windows Control Panel is used.
