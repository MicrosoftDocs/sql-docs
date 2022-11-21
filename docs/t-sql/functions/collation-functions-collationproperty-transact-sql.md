---
title: "COLLATIONPROPERTY (Transact-SQL)"
description: "Collation Functions - COLLATIONPROPERTY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "10/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "COLLATIONPROPERTY_TSQL"
  - "COLLATIONPROPERTY"
helpviewer_keywords:
  - "collations [SQL Server], properties"
  - "COLLATIONPROPERTY function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Collation Functions - COLLATIONPROPERTY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the requested property of a specified collation.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
COLLATIONPROPERTY( collation_name , property )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*collation_name*  
The name of the collation. The *collation_name* argument has an **nvarchar(128)** data type, with no default value.
  
*property*  
The collation property. The *property* argument has a **varchar(128)** data type, and can have any one of the following values:
  
|Property name|Description|  
|---|---|
|**CodePage**|Non-Unicode code page of the collation. This is the character set used for **varchar** data. See [Appendix G DBCS/Unicode Mapping Tables](/previous-versions/cc194886(v=msdn.10)) and [Appendix H Code Pages](/previous-versions/cc195051(v=msdn.10)) to translate these values, and to see their character mappings.<br /><br />Base data type: **int**|  
|**LCID**|Windows locale ID of the collation. This is the culture used for sorting and comparison rules. See [LCID Structure](/openspecs/windows_protocols/ms-lcid/63d3d639-7fd2-4afb-abbe-0d5b5551eef8) to translate these values (you will first need to convert to **varbinary**).<br /><br />Base data type: **int**|  
|**ComparisonStyle**|Windows comparison style of the collation. Returns 0 for binary collations - both (\_BIN) and (\_BIN2) - as well as when all properties are sensitive - (\_CS\_AS\_KS\_WS) and (\_CS\_AS\_KS\_WS\_SC) and (\_CS\_AS\_KS\_WS\_VSS). Bitmask values:<br /><br /> Ignore case : 1<br /><br /> Ignore accent : 2<br /><br /> Ignore Kana : 65536<br /><br /> Ignore width : 131072<br /><br /> Note: the variation-selector-sensitive (\_VSS) option is not represented in this value, even though it affects the comparison behavior.<br /><br />Base data type: **int**|  
|**Version**|The version of the collation. Returns a value between 0 and 3.<br /><br /> Collations with "140" in the name return 3.<br /><br /> Collations with "100" in the name return 2.<br /><br /> Collations with "90" in the name return 1.<br /><br /> All other collations return 0.<br /><br />Base data type: **tinyint**|  
  
## Return types
**sql_variant**
  
## Examples  
  
```sql
SELECT COLLATIONPROPERTY('Traditional_Spanish_CS_AS_KS_WS', 'CodePage');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
1252   
```  
  
[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
```sql
SELECT COLLATIONPROPERTY('Traditional_Spanish_CS_AS_KS_WS', 'CodePage')  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
1252   
```  
  
## See also
[sys.fn_helpcollations &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)    
[Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md)  
