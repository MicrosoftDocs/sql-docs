---
title: "Text File Format (Text File Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "delimited text lines"
  - "fixed-width text files"
  - "text format [ODBC]"
  - "text file driver [ODBC], text format"
ms.assetid: f53cd4b5-0721-4562-a90f-4c55e6030cb9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Text File Format (Text File Driver)
The ODBC Text driver supports both delimited and fixed-width text files. A text file consists of an optional header line and zero or more text lines.  
  
 Although the header line uses the same format as the other lines in the text file, the ODBC Text driver interprets the header line entries as column names, not data.  
  
 A delimited text line contains one or more data values separated by delimiters: commas, tabs, or a custom delimiter. The same delimiter must be used throughout the file. Null data values are denoted by two delimiters in a row with no data between them. Character strings in a delimited text line can be enclosed in double quotation marks (""). No blanks can occur before or after delimited values.  
  
 The width of each data entry in a fixed-width text line is specified in a schema. Null data values are denoted by blanks.  
  
 Tables are limited to a maximum of 255 fields. Field names are limited to 64 characters, and field widths are limited to 32,766 characters. Records are limited to 65,000 bytes.  
  
 A text file can be opened only for a single user. Multiple users are not supported.  
  
 The following grammar, written for programmers, defines the format of a text file that can be read by the ODBC text driver:  
  
|Format|Representation|  
|------------|--------------------|  
|Non-italics|Characters that must be entered as shown|  
|*italics*|Arguments that are defined elsewhere in the grammar|  
|brackets ([])|Optional items|  
|braces ({})|A list of mutually exclusive choices|  
|vertical bars (&#124;)|Separate mutually exclusive choices|  
|ellipses (...)|Items that can be repeated one or more times|  
  
 The format of a text file is:  
  
```  
text-file ::=  
   [delimited-header-line] [delimited-text-line]... end-of-file |  
   [fixed-width-header-line] [fixed-width-text-line]... end-of-file  
delimited-header-line ::= delimited-text-line  
delimited-text-line ::=  
   blank-line |  
   delimited-data [delimiter delimited-data]... end-of-line  
fixed-width-header-line ::= fixed-width-text-line  
fixed-width-text-line ::=  
   blank-line |  
   fixed-width-data [fixed-width-data]... end-of-line  
end-of-file ::= <EOF>  
blank-line ::= end-of-line  
delimited-data ::= delimited-string | number | date | delimited-null  
fixed-width-data ::= fixed-width-string | number | date | fixed-width-null  
```  
  
> [!NOTE]  
>  The width of each column in a fixed-width text file is specified in the Schema.ini file.  
  
```  
  
      end-of-line ::= <CR> | <LF> | <CR><LF>  
delimited-string ::= unquoted-string | quoted-stringunquoted-string ::= [character | digit] [character | digit | quote-character]...  
quoted-string ::=  
   quote-character  
   [character | digit | delimiter | end-of-line | embedded-quoted-string]...  
   quote-characterembedded-quoted-string ::=   quote-characterquote-character  
   [character | digit | delimiter | end-of-line]  
   quote-characterquote-characterfixed-width-string ::= [character | digit | delimiter | quote-character] ...  
character ::= any character except:  
   delimiterdigitend-of-fileend-of-linequote-characterdigit ::= 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9  
delimiter ::= , | <TAB> |   
custom-delimitercustom-delimiter ::= any character except:  
   end-of-fileend-of-linequote-character  
```  
  
> [!NOTE]  
>  The delimiter in a custom-delimited text file is specified in the Schema.ini file.  
  
```  
quote-character ::= "  
number ::= exact-number | approximate-number  
exact-number ::= [+ | -] {unsigned-integer[.unsigned-integer] |  
   unsigned-integer. |  
   .unsigned-integer}  
approximate-number ::= exact-number{e | E}[+ | -]unsigned-integer  
unsigned-integer ::= {digit}...  
date ::=  
   mm date-separator dd date-separator yy |  
   mmm date-separator dd date-separator yy |  
   dd date-separator mmm date-separator yy |  
   yyyy date-separator mm date-separator dd |  
   yyyy date-separator mmm date-separator dd  
mm ::= digit [digit]  
dd ::= digit [digit]  
yy ::= digit digit  
yyyy ::= digit digit digit digit  
mmm ::= Jan | Feb | Mar | Apr | May | Jun | Jul | Aug | Sep | Oct | Nov | Dec  
date-separator ::= - | / | .  
delimited-null ::=  
```  
  
> [!NOTE]  
>  For delimited files, a NULL is represented by no data between two delimiters.  
  
```  
fixed-width-null ::= <SPACE>...  
```  
  
> [!NOTE]  
>  For fixed-width files, a NULL is represented by spaces.
