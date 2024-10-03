---
title: ADD SENSITIVITY CLASSIFICATION (Transact-SQL)
description: Adds metadata about the sensitivity classification to one or more database columns.
author: Madhumitatripathy
ms.author: matripathy
ms.reviewer: vanto, randolphwest
ms.date: 08/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ADD SENSITIVITY CLASSIFICATION"
  - "ADD_SENSITIVITY_CLASSIFICATION"
helpviewer_keywords:
  - "ADD SENSITIVITY CLASSIFICATION statement"
  - "add labels"
  - "adding labels"
  - "classification [SQL]"
  - "labels [SQL]"
  - "information types"
  - "data classification"
  - "rank"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-linux-ver15||>=sql-server-ver15||=azuresqldb-current||=azure-sqldw-latest||=azuresqldb-mi-current"
---

# ADD SENSITIVITY CLASSIFICATION (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi-asa.md)]

Adds metadata about the sensitivity classification to one or more database columns. The classification can include a sensitivity label and an information type.

Classifying sensitive data in your database environment helps achieve extended visibility and better protection. For more information, see [Data Discovery & Classification](/azure/azure-sql/database/data-discovery-and-classification-overview).

## Syntax

```syntaxsql
    ADD SENSITIVITY CLASSIFICATION TO
    <object_name> [ , ...n ]
    WITH ( <sensitivity_option> [ , ...n ] )

<object_name> ::=
{
    [ schema_name. ] table_name.column_name
}

<sensitivity_option> ::=
{
    LABEL = string |
    LABEL_ID = guidOrString |
    INFORMATION_TYPE = string |
    INFORMATION_TYPE_ID = guidOrString |
    RANK = NONE | LOW | MEDIUM | HIGH | CRITICAL
}
```

## Arguments

#### *object_name* [ schema_name. ] table_name.column_name

The name of the database column to be classified. Currently only column classification is supported.

- `schema_name` *(optional)* - The name of the schema to which the classified column belongs.
- `table_name` - The name of the table to which the classified column belongs.
- `column_name` - The name of the column being classified.

#### LABEL

The human readable name of the sensitivity label. Sensitivity labels represent the sensitivity of the data stored in the database column.

#### LABEL_ID

An identifier associated with the sensitivity label. Often used by centralized information protection platforms to uniquely identify labels in the system.

#### INFORMATION_TYPE

The human readable name of the information type. Information types are used to describe the type of data being stored in the database column.

#### INFORMATION_TYPE_ID

An identifier associated with the information type. Often used by centralized information protection platforms to uniquely identify information types in the system.

#### RANK

An identifier based on a predefined set of values that define sensitivity rank. Used by other services like Advanced Threat Protection to detect anomalies based on their rank.

## Remarks

Only one classification can be added to a single object. Adding a classification to an object that is already classified overwrites the existing classification.

Multiple objects can be classified using a single `ADD SENSITIVITY CLASSIFICATION` statement.

The system view [sys.sensitivity_classifications](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md) can be used to retrieve the sensitivity classification information for a database.

`ADD SENSITIVITY CLASSIFICATION` works if any one of the parameters is supplied. If you supply only `LABEL` or `INFORMATION_TYPE`, without corresponding IDs, the command succeeds. However, you should have a one-to-one mapping between `LABEL_ID` and `LABEL` name.

The `RANK` parameter isn't currently used, so just having a rank value against a column without a `LABEL` or `INFORMATION_TYPE` doesn't add any value and should be avoided. If you provide just the `RANK` parameter, the command succeeds, but this is a known issue.

## Permissions

Requires `ALTER ANY SENSITIVITY CLASSIFICATION` permission. The `ALTER ANY SENSITIVITY CLASSIFICATION` is also provided by the database permission `CONTROL`, or by the server permission `CONTROL SERVER`.

## Examples

### A. Classify two columns

The following example classifies the columns `dbo.sales.price` and `dbo.sales.discount` with the sensitivity label **Highly Confidential**, rank **Critical** and the Information Type **Financial**.

```sql
ADD SENSITIVITY CLASSIFICATION TO dbo.sales.price,
    dbo.sales.discount
WITH (
    LABEL = 'Highly Confidential',
    INFORMATION_TYPE = 'Financial',
    RANK = CRITICAL
);
```

### B. Classify only a label

The following example classifies the column `dbo.customer.comments` with the label **Confidential** and label ID `643f7acd-776a-438d-890c-79c3f2a520d6`. Information type isn't classified for this column.

```sql
ADD SENSITIVITY CLASSIFICATION TO dbo.customer.comments
WITH (
    LABEL = 'Confidential',
    LABEL_ID = '643f7acd-776a-438d-890c-79c3f2a520d6'
);
```

## Related content

- [DROP SENSITIVITY CLASSIFICATION (Transact-SQL)](drop-sensitivity-classification-transact-sql.md)
- [sys.sensitivity_classifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md)
- [Permissions (Database Engine)](../../relational-databases/security/permissions-database-engine.md)
- [Getting started with SQL Information Protection](/azure/azure-sql/database/data-discovery-and-classification-overview)
