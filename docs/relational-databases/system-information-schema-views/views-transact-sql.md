---
title: VIEWS (Transact-SQL)
description: "Returns one row for each view that can be accessed by the current user in the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/27/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "VIEWS_TSQL"
  - "VIEWS"
helpviewer_keywords:
  - "VIEWS view"
  - "INFORMATION_SCHEMA.VIEWS view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# VIEWS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns one row for each view that can be accessed by the current user in the current database.

To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**TABLE_CATALOG**|**nvarchar(128)**|View qualifier.|
|**TABLE_SCHEMA**|**nvarchar(128)**|Name of schema that contains the view.<br /><br />**Important:** The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|
|**TABLE_NAME**|**nvarchar(128)**|View name.|
|**VIEW_DEFINITION**|**nvarchar(4000)**|If the length of definition is larger than **nvarchar(4000)**, this column is truncated at 4000. Otherwise, this column is the view definition text.|
|**CHECK_OPTION**|**varchar(7)**|Type of WITH CHECK OPTION. Is CASCADE if the original view was created by using the WITH CHECK OPTION. Otherwise, NONE is returned.|
|**IS_UPDATABLE**|**varchar(2)**|Specifies whether the view is updatable. Always returns NO.|

## See also

- [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)
- [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)
- [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)
- [sys.views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-views-transact-sql.md)
