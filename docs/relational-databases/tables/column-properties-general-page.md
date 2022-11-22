---
description: "Column Properties (General Page)"
title: "Column Properties (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.columnproperties.general.f1"
ms.assetid: a745890b-994e-4c23-8028-5c83751e60c4
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Column Properties (General Page)
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  Use this page to view properties for the selected column.  
  
 Information on this page is read-only. To modify the column, close the **Column Properties** dialog box, expand the table and columns in Object Explorer, right-click the column, and then click **Design**.  
  
## Options  
 **Name**  
 The name of the column.  
  
 **Data Type**  
 The type of data that the column can hold. If the data type is a user-defined data type, the user-defined data type is displayed. If the data type is not a user-defined data type, then the system data type is displayed. For more information, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
 **System Type**  
 The type of data that the column can hold. If the data type is a system data type, then the system data type is displayed. If the data type is a user-defined data type, the system data type that makes up the user-defined data type is displayed.  
  
 **Primary Key**  
 Indicates whether the column is a primary key. Possible values are **True** and **False**.  
  
 **Allow Nulls**  
 Indicates whether the column accepts null values. Possible values are **True** and **False**.  
  
 **Is Computed**  
 Indicates whether the column value is the result of a computed expression.  
  
 **Computed text**  
 Indicates the statement used to compute the column text. For more information, [Specify Computed Columns in a Table](../../relational-databases/tables/specify-computed-columns-in-a-table.md).  
  
 **Identity**  
 Indicates whether the column is the identity column for the table. Possible values are **True** and **False**.  
  
 **Identity Seed**  
 Indicates the initial row value for an identity column.  
  
 **Identity Increment**  
 The **Identity Increment** property specifies the value [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adds to the greatest existing row identity value as it generates an identity value for a row being inserted.  
  
 **Default Binding**  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default bound to the column. This option is blank if no default is bound.  
  
 **Default Schema**  
 Identifies the database schema owning the default bound to the referenced column. This option is blank if no default is bound.  
  
 **Rule**  
 Identifies the data integrity constraint that is bound to the column. This option is blank if no rule is bound.  
  
 **Rule Schema**  
 Displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database schema that owns the rule bound to the referenced column. This option is blank if no rule is bound.  
  
 **Length**  
 Indicates the maximum number of characters or bytes accepted by the column.  
  
 **Collation**  
 Displays the current collation for the column. If blank, the collation property is inherited from the object.  
  
 **Numeric Precision**  
 Indicates the maximum number of digits in a fixed-precision, numeric data type.  
  
 **Numeric Scale**  
 Indicates the number of digits to the right of the decimal point in a fixed-precision, numeric data type.  
  
 **XML Schema Namespace**  
 Defines the type of the XML column by way of XML Schema Definition (XSD) Language validation.  
  
 **XML Schema Namespace schema**  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schema that owns the XML Schema Namespace.  
  
> [!NOTE]  
>  There are several common but different meanings of the word schema. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses schema to organize database objects. It is similar to ownership. XML uses the schema to define the organization of XML information into a series of namespaces. It is a way to group related XML code together.  
  
 **Is Sparse**  
 Indicates whether the column is a sparse column. Possible values are **True** and **False**. For more information, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).  
  
 **Is Column Set**  
 Indicates whether the column is a column set. Possible values are **True** and **False**. For more information, see [Use Column Sets](../../relational-databases/tables/use-column-sets.md).  
  
 **ANSI Padding Status**  
 Indicates whether ANSI padding is on or off. For more information, see [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md).  
  
 **Full Text**  
 Displays whether the column participates in full-text queries.  
  
 **Statistical Semantics**  
 Indicates whether statistical semantic search is enabled for the column. For more information, see [Semantic Search &#40;SQL Server&#41;](../../relational-databases/search/semantic-search-sql-server.md).  
  
 **Not For Replication**  
 Indicates whether the column is available for replication. Possible values are **True** and **False**.  
  
  
