---
title: "Table Column Properties (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "08/08/2016"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:65558"
  - "vdtsql.chm:69657"
  - "vdt.ppg.columns"
ms.assetid: 09830897-cc10-46b8-95f5-e0e9681b668c
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Table Column Properties (SQL Server Management Studio)
[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

  These properties appear in the bottom pane of Table Designer. Unless otherwise noted, you can edit these properties in the Properties window when the column is selected. The **Column Properties** can be displayed in categories or alphabetically. Many properties only appear or can only be changed for certain data types.  
  
> [!NOTE]  
>  If the table is published for replication, you must make schema changes using the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
 **General**  
 Expands to show **Name**, **Allow Nulls**, **Data Type**, **Default Value or Binding**, **Length**, **Precision**, and **Scale**.  
  
 **Name**  
 Displays the name of the selected column.  
  
 **Allow Nulls**  
 Indicates whether this column allows nulls. To edit this property, click the Allow Nulls checkbox corresponding to the column in the top pane of Table Designer.  
  
 **Data Type**  
 Displays the data type for the selected column. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
 **Default Value or Binding**  
 Displays the default for this column whenever no value is specified for this column. The value of this field can be either the value of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default constraint or the name of a global constraint to which the column is bound. The drop-down list contains all global defaults defined in the database. To bind the column to a global default, select from the drop-down list. Alternatively, to create a default constraint for the column, type the default value directly as text.  
  
 **Length**  
 Shows the number of characters allowed for character-based data types. This property is only available for character-based data types  
  
 **Scale**  
 Displays the maximum number of digits that can appear to the right of the decimal point for values of this column. This property shows **0** for nonnumeric data types.  
  
 **Precision**  
 Displays the maximum number of digits for values in this column. This property shows **0** for nonnumeric data types.  
  
 **Table Designer**  
 Expands the **Table Designer** section.  
  
 **Collation**  
 Displays the collating sequence that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applies by default to the column whenever the column values are used to sort rows of a query result. To edit the collation, select the property, click the ellipsis (   ) that appears to the right of the property value to bring up the **Collation** dialog box.  
  
 **Computed Column Specification**  
 Displays information about a computed column. The value shown for property is the same as the value of the **Formula** child property and displays the formula for the computed column.  
  
> [!NOTE]  
>  To change the value shown for the **Computed Column Specification** property, you must expand it and edit the **Formula** child property.  
  
-   **Formula** Displays the formula for the computed column. To edit this property, type a new formula directly.  
  
-   **Is Persisted** Indicates whether the results of the formula are stored. If this property is set to **No** then only the formula is stored and the values are calculated every time this column is referenced. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
 For more information, see [Specify Computed Columns in a Table](../../relational-databases/tables/specify-computed-columns-in-a-table.md).  
  
 **Condensed Data Type**  
 Displays information about the field's data type, in the same format as the SQL CREATE TABLE statement. For example, a field containing a variable-length string with a maximum length of 20 characters would be represented as "varchar(20)". To change this property, type the value directly.  
  
 **Description**  
 Displays text describing this column. To edit the description, select the property, click the ellipsis (   ) that appears to the right of the property value and edit the description in the **Description Property** dialog box.  
  
 **Deterministic**  
 Shows whether the data type of the selected column can be determined with certainty.  
  
 **DTS-published**  
 Shows whether the column is DTS-published. ([Data Transformation Services Is Deprecated](https://msdn.microsoft.com/library/cc707786(v=sql.130).aspx#Anchor_0)). 
  
 **Full-text Specification**  
 Displays information about a full-text index. The value of this property is the value of the **Is Full-text Indexed** child property and indicates whether this column is full-text indexed.  
  
> [!NOTE]  
>  To change the value shown for the **Full-text Specification** property, you must expand it and edit the **Is Full-text Indexed** child property.  
  
-   **Is Full-text Indexed** Indicates whether this column is full-text indexed. This property can be set to **Yes** only if the data type for this column is full-text searchable and if the table to which this column belongs has a full-text index specified for it. To edit this property, click its value, expand the drop-down list, and choose a value.  
  
-   **Full-text Type Column** Displays the name of the column on which this column is full-text indexed. This property must be set if the **Datatype** property for this column is either **image** or **varbinary**. The column named in this property must be of type **[n]char, [n]varchar,** or **xml**, and the drop-down list for this property contains only columns that have one of these three data types. Rows in the column named by this property indicate the document type of the corresponding rows in the full-text-searchable column. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
-   **Language** Indicates the language of the word breaker used to index the column. The value stored in the property is actually the locale identifier for the word breaker. For more information about word breakers and LCIDs, see Word Breakers and Stemmers. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
 **Statistical Semantics**  
 Select whether to enable statistical semantic indexing for the selected column. For more information, see [Semantic Search &#40;SQL Server&#41;](../../relational-databases/search/semantic-search-sql-server.md).  
  
 If you select a **Language** prior to selecting **Statistical Semantics**, and the selected language does not have an associated Semantic Language Model, then the **Statistical Semantics** option is set to **No** and cannot be modified. If you select **Yes** for the **Statistical Semantics** option prior to selecting a **Language**, then the languages available in the **Language** column will be restricted to those for which there is Semantic Language Model support.  
  
 **Has Non-SQL Server Subscriber**  
 Indicates if the column is being replicated to a subscriber that is not a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Identity Specification**  
 Displays information about whether and how this column enforces uniqueness on its values. The value of this property indicates whether or not this column is an identity column and is the same as the value of the child property **Is Identity**.  
  
> [!NOTE]  
>  To change the value shown for the **Identity Specification** property, you must expand it and edit the **Is Identity** child property.  
  
-   **Is Identity** Indicates whether or not this column is an identity column. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
-   **Identity Seed** Displays the seed value specified during the creation of this identity column. This value is assigned to the first row in the table. If you leave this cell blank, the value 1 will be assigned by default. To edit this property, type the new value directly.  
  
-   **Identity Increment** Displays the increment value specified during the creation of this identity column. This value is the increment that will be added to the **Identity Seed** for each subsequent row. If you leave this cell blank, the value 1 will be assigned by default. To edit this property, type the new value directly.  
  
 **Indexable**  
 Shows whether the selected column can be indexed. For example, non-deterministic computed columns cannot be indexed.  
  
 **Merge-published**  
 Shows whether the column is merge-published.  
  
 **Not For Replication**  
 Indicates whether original identity values are preserved during replication. For more information on replication see CREATE TABLE. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
 **Replicated**  
 Shows whether this column is replicated in another location.  
  
 **RowGuid**  
 Indicates whether SQL Server uses the column as a ROWGUID. You can set this value to **Yes** only for a unique identity column. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
 **Size**  
 Shows the size in bytes allowed by column's data type. For example, a nchar data type may have a length of 10 (the number of characters) but it would have a size of 20 to account for Unicode character sets.  
  
> [!NOTE]  
>  The length of a **(max)** data types vary for each row. **sp_help** returns (-1) as the length of **(max)** columns. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] displays -1 as the column size.  
  
  
