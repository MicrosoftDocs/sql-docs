---
title: "Mapping MySQL and SQL Server Character Set (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 20b3f22e-16a2-4a87-b4eb-c277be6bf5c8
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Mapping MySQL and SQL Server Character Set (MySQLToSQL)
Character set (Charset) can be specified for MySQL character data types, expressions and literals.  
  
## Charset Mapping  
Charset mapping is defined for each MySQL charset and used during character data type conversion. It specifies how to convert character string data types of a particular character set:  
  
-   To national SQL Server character types (NCHAR/NVARCHAR), or  
  
-   To regular SQL Server character types (CHAR/VARCHAR)  
  
1.  **national** target database character data types are:  
  
    1.  **nchar**  
  
    2.  **nvarchar**  
  
2.  **regular** target database character data types are:  
  
    1.  **char**  
  
    2.  **varchar**  
  
3.  Type mapping only allows mapping to **national** character data types. After MySQL character data type is converted according to type mapping, charset mapping is applied.  
  
> [!NOTE]  
> Charset mapping can be defined on each node level of metadata object explorer and represent all charsets read from MySQL.  
  
## Charset Mapping on different node levels  
Charset Mapping varies at different node levels, namely:  
  
1.  On Root Metadata Node Level  
  
2.  On Database, Category and Object Nodes Level  
  
> [!NOTE]  
> The tab selected for editing the Charset Mapping, contains three buttons, irrespective of the mapping on the different node levels.  
>   
> They are:  
>   
> 1.  **Apply:** Applies changes made by the user, enabled only when charset mapping is edited and not saved yet.  
> 2.  **Cancel:** Cancels changes made by the user. The button gets enabled when charset mapping is edited but not saved.  
> 3.  **Reset to Default:** Resets all mappings to default values.  
  
1.  **On Root Metadata Node Level:**  Charset mapping grid contains charset grid with a separate column for each charset. The columns of the grid are:  
  
    1.  The first column of the grid named **Charset Name** contains charset name.  
  
    2.  The second one named **Charset Description** contains charset description.  
  
    3.  The third column titled, **Target Charset Type** contains mapping settings for particular charset. Values for this column are:  
  
        -   CHAR/VARCHAR  
  
        -   NCHAR/NVARCHAR  
  
    > [!IMPORTANT]  
    > The default values for a particular charset have the prefix '(default)' after CHAR/VARCHAR or NCHAR/NVARCHAR.  
  
    The Charset mapping between MySQL database and the target database on Root Metadata Node Level is given below:  
  
    ||||  
    |-|-|-|  
    |**Charset Name**|**Charset Description**|**Target Charset Type (Default)**|  
    |big5|Big5 Traditional Chinese|NCHAR/NVARCHAR (Default)|  
    |dec8|DEC West European|CHAR/VARCHAR (Default)|  
    |cp850|DOS West European|CHAR/VARCHAR (Default)|  
    |hp8|HP West European|CHAR/VARCHAR (Default)|  
    |koi8r|KOI8-R Relcom Russian|CHAR/VARCHAR (Default)|  
    |latin 1|cp1252 West European|CHAR/VARCHAR (Default)|  
    |latin2|ISO 8859-2 Central European|CHAR/VARCHAR (Default)|  
    |swe7|7bit Swedish|CHAR/VARCHAR (Default)|  
    |ascii|US ASCII|CHAR/VARCHAR (Default)|  
    |ujis|EUC-JP Japanese|NCHAR/NVARCHAR (Default)|  
    |sjis|Shift-JIS Japanese|NCHAR/NVARCHAR (Default)|  
    |hebrew|ISO 8859-8 Hebrew|CHAR/VARCHAR (Default)|  
    |tis620|TIS620 Thai|CHAR/VARCHAR (Default)|  
    |euckr|EUC-KR Korean|NCHAR/NVARCHAR (Default)|  
    |koi8u|KOI8-U Ukrainian|CHAR/VARCHAR (Default)|  
    |gb2312|GB2312 Simplified Chinese|NCHAR/NVARCHAR (Default)|  
    |greek|ISO 8859-7 Greek|CHAR/VARCHAR (Default)|  
    |cp 1250|Windows Central European|CHAR/VARCHAR (Default)|  
    |gbk|GBK Simplified Chinese|NCHAR/NVARCHAR (Default)|  
    |latin5|ISO 8859-9 Turkish|CHAR/VARCHAR (Default)|  
    |armscii8|ARMSCII-8 Armenian|CHAR/VARCHAR (Default)|  
    |utf8|UTF-8 Unicode|NCHAR/NVARCHAR (Default)|  
    |ucs2|UCS-2 Unicode|NCHAR/NVARCHAR (Default)|  
    |cp866|DOS Russian|CHAR/VARCHAR (Default)|  
    |keybcs2|DOS Kamenicky Czech-Slovak|CHAR/VARCHAR (Default)|  
    |macce|Mac Central European|CHAR/VARCHAR (Default)|  
    |macroman|Mac West European|CHAR/VARCHAR (Default)|  
    |cp852|DOS Central European|CHAR/VARCHAR (Default)|  
    |latin7|ISO 8859-13 Baltic|CHAR/VARCHAR (Default)|  
    |cp 1251|Windows Cyrillic|CHAR/VARCHAR (Default)|  
    |cp 1256|Windows Arabic|CHAR/VARCHAR (Default)|  
    |cp 1257|Windows Baltic|CHAR/VARCHAR (Default)|  
    |binary|Binary pseudo charset|CHAR/VARCHAR (Default)|  
    |geostd8|GEOSTD8 Georgian|CHAR/VARCHAR (Default)|  
    |cp932|SJIS for Windows Japanese|NCHAR/NVARCHAR (Default)|  
    |eucjpms|UJIS for Windows Japanese|NCHAR/NVARCHAR (Default)|  
  
2.  **On the Database, Category or Object Node Levels:** On the Database, Category or Object Nodes level, charset mapping grid contains the same rows as the one on root metadata node level, viz.:  
  
    1.  The first column of the grid titled, **Character Set Name** contains charset name.  
  
    2.  The second column titled, **Character Set Description** contains charset description.  
  
    3.  The only difference is the values in the third column of the grid. The third column titled, **Target Data Type** contains mapping settings for particular charset. The values for the column are:  
  
        -   Inherited (CHAR/VARCHAR or NCHAR/NVARCHAR)  
  
        -   CHAR/VARCHAR  
  
        -   NCHAR/NVARCHAR  
  
> [!IMPORTANT]  
> -   In the Charset mapping between MySQL database and target database on Database, Category, and Object Node Levels, the default values for a particular charset on each level other than root for the column **Target Data Type** should be 'Inherited'.  
> -   In the grid, the value **Inherited** is suffixed with either '(CHAR/VARCHAR)' or '(NCHAR/NVARCHAR)' depending on which value was inherited from parent by this particular charset.  
  
