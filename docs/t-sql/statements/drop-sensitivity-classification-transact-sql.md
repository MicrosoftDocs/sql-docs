---
title: "DROP SENSITIVITY CLASSIFICATION (Transact-SQL) | Microsoft Docs"
ms.date: 03/25/2019
ms.reviewer: ""
ms.prod: sql
ms.technology: t-sql
ms.topic: "language-reference"
ms.custom: ""
ms.manager: craigg
ms.author: giladm
author: giladmit
f1_keywords:
  - "DROP SENSITIVITY CLASSIFICATION"
  - "DROP_SENSITIVITY_CLASSIFICATION"
dev_langs:
  - "TSQL"
helpviewer_keywords:
  - "DROP SENSITIVITY CLASSIFICATION statement"
  - "dropping labels"
  - "drop labels"
  - "removing labels"
  - "remove labels"
  - "classification [SQL]"
  - "labels [SQL]"
  - "information types"
  - "data classification"
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# DROP SENSITIVITY CLASSIFICATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md)]

Drops sensitivity classification metadata from one or more database columns.

## Syntax

```sql
DROP SENSITIVITY CLASSIFICATION FROM
    <object_name> [, ...n ]

<object_name> ::=
{
    [schema_name.]table_name.column_name
}
```  

## Arguments  

*object_name* ([schema_name.]table_name.column_name)

Is the name of the database column from which to remove the classification. Currently only column classification is supported.
    - *schema_name* (optional) - Is the name of the schema to which the classified column belongs to.
    - *table_name* - Is the name of the table to which the classified column belongs to.
    - *column_name* - Is the name of the column from which to drop the classification.

## Remarks  

- Multiple object classifications can be dropped using a single 'DROP SENSITIVITY CLASSIFICATION' statement.

## Permissions  

Requires ALTER ANY SENSITIVITY CLASSIFICATION permission. The ALTER ANY SENSITIVITY CLASSIFICATION is implied by the database permission ALTER, or by the server permission CONTROL SERVER.


## Examples  


### A. Dropping classification from a single column

The following example removes the classification from the column `dbo.sales.price`.  

```sql
DROP SENSITIVITY CLASSIFICATION FROM
    dbo.sales.price
```

### B. Dropping classification from multiple columns

The following example removes the classification from the columns `dbo.sales.price`, `dbo.sales.discount`, and `SalesLT.Customer.Phone`.  

```sql
DROP SENSITIVITY CLASSIFICATION FROM
    dbo.sales.price, dbo.sales.discount, SalesLT.Customer.Phone  
```

## See Also  

[ADD SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/add-sensitivity-classification-transact-sql.md)

[sys.sensitivity_classifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md)

[Getting started with SQL Information Protection](https://aka.ms/sqlip)
