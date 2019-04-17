---
title: "SET COLLATE Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "set collate command [ODBC]"
ms.assetid: 00efbcd4-fea8-4061-86a5-82de413cb753
author: MightyPen
ms.author: genemi
manager: craigg
---
# SET COLLATE Command
Specifies a collation sequence for character fields in subsequent indexing and sorting operations.  
  
## Syntax  
  
```  
  
SET COLLATE TO cSequenceName  
```  
  
## Arguments  
 *cSequenceName*  
 Specifies a collation sequence. The available collation sequence options are described in the following table.  
  
|Options|Language|  
|-------------|--------------|  
|DUTCH|Dutch|  
|GENERAL|English, French, German, Modern Spanish, Portuguese, and other Western European languages|  
|GERMAN|German phone book order (DIN)|  
|ICELAND|Icelandic|  
|MACHINE|Machine (the default collation sequence for earlier FoxPro versions)|  
|NORDAN|Norwegian, Danish|  
|SPANISH|Traditional Spanish|  
|SWEFIN|Swedish, Finnish|  
|UNIQWT|Unique Weight|  
  
> [!NOTE]  
>  When you specify the SPANISH option, *ch* is a single letter that sorts between *c* and *d*, and *ll* sorts between *l* and *m*.  
  
 If you specify a collation sequence option as a literal character string, be sure to enclose the option in quotation marks:  
  
```  
SET COLLATE TO "SWEFIN"  
```  
  
 MACHINE is the default collation sequence option and is the sequence Xbase users are familiar with. Characters are ordered as they appear in the current code page.  
  
 GENERAL may be preferable for U.S. and Western European users. Characters are ordered as they appear in the current code page. In FoxPro versions earlier than 2.5, indexes might have been created using the **UPPER**( ) or **LOWER**( ) functions to convert character fields to a consistent case. In FoxPro versions later than 2.5, you can instead specify the GENERAL collation sequence option and omit the **UPPER**( ) conversion.  
  
 If you specify a collation sequence option other than MACHINE and if you create an .idx file, a compact .idx is always created.  
  
 Use SET("COLLATE") to return the current collation sequence.  
  
 You can specify a collating sequence for a data source by using the [ODBC Visual FoxPro Setup Dialog Box](../../odbc/microsoft/odbc-visual-foxpro-setup-dialog-box.md) or by using the Collate keyword in your connection string with [SQLDriverConnect](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md). This is identical to issuing the following command:  
  
```  
SET COLLATE TO cSequenceName  
```  
  
## Remarks  
 SET COLLATE enables you to order tables containing accented characters for any of the supported languages. Changing the setting of SET COLLATE doesn't affect the collating sequence of previously opened indexes. Visual FoxPro automatically maintains existing indexes, providing the flexibility to create many different types of indexes, even for the same field.  
  
 For example, if an index is created with SET COLLATE set to GENERAL and the SET COLLATE setting is later changed to SPANISH, the index retains the GENERAL collation sequence.  
  
## See Also  
 [ODBC Visual FoxPro Setup Dialog Box](../../odbc/microsoft/odbc-visual-foxpro-setup-dialog-box.md)
