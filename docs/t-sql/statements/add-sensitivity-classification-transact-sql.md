---
title: ADD SENSITIVITY CLASSIFICATION (Transact-SQL)
description: ADD SENSITIVITY CLASSIFICATION (Transact-SQL)
author: Madhumitatripathy
ms.author: matripathy
ms.reviewer: vanto
ms.date: 04/19/2022
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
monikerRange: ">=sql-server-linux-ver15||>=sql-server-ver15||=azuresqldb-current||=azure-sqldw-latest"
---

# ADD SENSITIVITY CLASSIFICATION (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Adds metadata about the sensitivity classification to one or more database columns. The classification can include a sensitivity label and an information type.

For SQL Server, this was introduced in SQL Server 2019.

Classifying sensitive data in your database environment helps achieve extended visibility and better protection. Additional information can be found in [Getting started with SQL Information Protection](/azure/azure-sql/database/data-discovery-and-classification-overview)

## Syntax

```syntaxsql
    ADD SENSITIVITY CLASSIFICATION TO
    <object_name> [, ...n ]
    WITH ( <sensitivity_option> [, ...n ] )

<object_name> ::=
{
    [schema_name.]table_name.column_name
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

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments  

*object_name* ([schema_name.]table_name.column_name)

Is the name of the database column to be classified. Currently only column classification is supported.
    - *schema_name* (optional) - Is the name of the schema to which the classified column belongs to.
    - *table_name* - Is the name of the table to which the classified column belongs to.
    - *column_name* - Is the name of the column being classified.

*LABEL*

Is the human readable name of the sensitivity label. Sensitivity labels represent the sensitivity of the data stored in the database column.

*LABEL_ID*

Is an identifier associated with the sensitivity label. This is often used by centralized information protection platforms to uniquely identify labels in the system.

*INFORMATION_TYPE*

Is the human readable name of the information type. Information types are used to describe the type of data being stored in the database column.

*INFORMATION_TYPE_ID*

Is an identifier associated with the information type. This is often used by centralized information protection platforms to uniquely identify information types in the system.

*RANK*

Is an identifier based on a predefined set of values which define sensitivity rank. Used by other services like Advanced Threat Protection to detect anomalies based on their rank.

## Remarks  

- Only one classification can be added to a single object. Adding a classification to an object that is already classified will overwrite the existing classification.
- Multiple objects can be classified using a single `ADD SENSITIVITY CLASSIFICATION` statement.
- The system view [sys.sensitivity_classifications](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md) can be used to retrieve the sensitivity classification information for a database.

## Permissions

Requires ALTER ANY SENSITIVITY CLASSIFICATION permission. The ALTER ANY SENSITIVITY CLASSIFICATION is implied by the database permission CONTROL, or by the server permission CONTROL SERVER.

## Examples  

### A. Classifying two columns

The following example classifies the columns **dbo.sales.price** and **dbo.sales.discount** with the sensitivity label **Highly Confidential**, rank **Critical** and the Information Type **Financial**.

```sql
ADD SENSITIVITY CLASSIFICATION TO
    dbo.sales.price, dbo.sales.discount
    WITH ( LABEL='Highly Confidential', INFORMATION_TYPE='Financial', RANK=CRITICAL )
```  

### B. Classifying only a label

The following example classifies the column **dbo.customer.comments** with the label **Confidential** and label ID **643f7acd-776a-438d-890c-79c3f2a520d6**. Information type isn't classified for this column.

```sql
ADD SENSITIVITY CLASSIFICATION TO
    dbo.customer.comments
    WITH ( LABEL='Confidential', LABEL_ID='643f7acd-776a-438d-890c-79c3f2a520d6' )
```  

## See Also

- [DROP SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/drop-sensitivity-classification-transact-sql.md)
- [sys.sensitivity_classifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md)
- [Permissions (Database Engine)](../../relational-databases/security/permissions-database-engine.md)
- [Getting started with SQL Information Protection](/azure/azure-sql/database/data-discovery-and-classification-overview)
