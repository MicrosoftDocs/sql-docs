---
title: "ADD SENSITIVITY CLASSIFICATION (Transact-SQL) | Microsoft Docs"
ms.date: 06/17/2018
ms.reviewer: ""
ms.prod: sql
ms.technology: t-sql
ms.topic: "language-reference"
ms.custom: ""
ms.manager: craigg
ms.author: giladm
author: giladmit
f1_keywords:
  - "ADD SENSITIVITY CLASSIFICATION"
  - "ADD_SENSITIVITY_CLASSIFICATION"
dev_langs:
  - "TSQL"
helpviewer_keywords:
  - "ADD SENSITIVITY CLASSIFICATION statement"
  - "add labels"
  - "adding labels"
  - "adding labels"
  - "classification [SQL]"
  - "labels [SQL]"
  - "information types"
  - "data classification"
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---

# ADD SENSITIVITY CLASSIFICATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

Adds metadata about the sensitivity classification to one or more database columns. The classification can include a sensitivity label and an information type.  

Classifying sensitive data in your database environment helps achieve extended visibility and better protection. Additional information can be found in [Getting started with SQL Information Protection](https://aka.ms/sqlip)

## Syntax  

```sql
ADD SENSITIVITY CLASSIFICATION TO
    <object_name> [, ...n ]
    WITH ( <sensitivity_label_option> [, ...n ] )     

<object_name> ::=
{
    [schema_name.]table_name.column_name
}

<sensitivity_label_option> ::=  
{   
    LABEL = string |
    LABEL_ID = guidOrString |
    INFORMATION_TYPE = string |
    INFORMATION_TYPE_ID = guidOrString  
}
```  

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


## Remarks  

- Only one classification can be added to a single object. Adding a classification to an object that is already classified will overwrite the existing classification.
- Multiple objects can be classified using a single `ADD SENSITIVITY CLASSIFICATION` statement.
- The system view [sys.sensitivity_classifications](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md) can be used to retrieve the sensitivity classification information for a database.


## Permissions

Requires ALTER ANY SENSITIVITY CLASSIFICATION permission. The ALTER ANY SENSITIVITY CLASSIFICATION is implied by the database permission ALTER, or by the server permission CONTROL SERVER.


## Examples  

### A. Classifying two columns

The following example classifies the columns **dbo.sales.price** and **dbo.sales.discount** with the sensitivity label **Highly Confidential** and the Information Type **Financial**.

```sql
ADD SENSITIVITY CLASSIFICATION TO
    dbo.sales.price, dbo.sales.discount
    WITH ( LABEL='Highly Confidential', INFORMATION_TYPE='Financial' )
```  

### B. Classifying only a label
The following example classifies the column **dbo.customer.comments** with the label **Confidential** and label ID **643f7acd-776a-438d-890c-79c3f2a520d6**. Information type isn't classified for this column.

```sql
ADD SENSITIVITY CLASSIFICATION TO
    dbo.customer.comments
    WITH ( LABEL='Confidential', LABEL_ID='643f7acd-776a-438d-890c-79c3f2a520d6' )
```  

## See Also  

[DROP SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/drop-sensitivity-classification-transact-sql.md)

[sys.sensitivity_classifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sensitivity-classifications-transact-sql.md)

[Permissions (Database Engine)](https://docs.microsoft.com/sql/relational-databases/security/permissions-database-engine)

[Getting started with SQL Information Protection](https://aka.ms/sqlip)
