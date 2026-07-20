---
title: "DROP EXTERNAL FILE FORMAT (Transact-SQL)"
description: "DROP EXTERNAL FILE FORMAT drops an external file format object defining external data."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: hudequei
ms.date: 05/13/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP EXTERNAL FILE FORMAT"
  - "DROP_EXTERNAL_FILE_FORMAT"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# DROP EXTERNAL FILE FORMAT (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  Removes a PolyBase external file format.  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
-- Drop an external file format  
DROP EXTERNAL FILE FORMAT external_file_format_name  
[;]  
```  

## Arguments

#### *external_file_format_name*

 The name of the external file format to drop.  

## Metadata

 To view a list of external file formats use the [sys.external_file_formats](../../relational-databases/system-catalog-views/sys-external-file-formats-transact-sql.md) system view.  

```sql
SELECT * FROM sys.external_file_formats;  
```  

## Permissions

 Requires ALTER ANY EXTERNAL FILE FORMAT.  

## Remarks

 Dropping an external file format does not remove the external data.  

## Locking

 Takes a shared lock on the external file format object.  

## Examples

<a id="a-using-basic-syntax"></a>

### A. Use basic syntax

```sql
DROP EXTERNAL FILE FORMAT myfileformat;  
```  

## Related content

- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)](create-external-file-format-transact-sql.md)
