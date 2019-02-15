---
title: "Column Properties (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.designers.properties.Column.ColumnIdentitySpec"
  - "vdt.designers.properties.Column"
  - "vdt.tablecolumn"
  - "vdt.designers.properties.Column.ColumnComputedColumnSpec"
  - "vdt.designers.properties.Column.ColumnFulltextSpec"
ms.assetid: e549a2a8-4154-4ec8-b146-614564169b39
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Column Properties (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
There are two sets of properties for columns: a full set that you can see in the **Column Properties** tab within Table Designer (available only for [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases) and a subset you can see in the Properties window using Server Explorer.  
  
> [!NOTE]  
> The properties in this topic are ordered by category rather than alphabet.  
  
> [!NOTE]  
> The dialog boxes and menu commands you see might differ from those described in Help depending on your active settings or edition. To change your settings, choose **Import and Export Settings** on the **Tools** menu.  
  
## Properties Window  
These properties appear in the Properties window when you select a column in Server Explorer.  
  
> [!NOTE]  
> These properties, accessed using Server Explorer, are read-only. To edit column properties for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, select the column in Table Designer. Those properties are described later in this topic.  
  
**Identity Category**  
Expands to show the **Name** and **Database** properties.  
  
**Name**  
Shows the name of the column.  
  
**Database**  
Shows the name of the data source for the selected column. (Applies only to OLE DB.)  
  
**Misc Category**  
Expands to show the remaining properties.  
  
**Data Type**  
Shows the data type of the selected column. For more information, see [Data Types (Transact-SQL)](https://msdn.microsoft.com/a54f7373-b247-4d61-8fb8-7f2ec7a8d0a4).  
  
**Identity Increment**  
Shows the increment that will be added to the **Identity Seed** for each subsequent row of the identity column. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].)  
  
**Identity Seed**  
Shows the seed value assigned to the first row in the table for the identity column. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].)  
  
**Is Identity**  
Shows whether the selected column is the identity column for the table. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].)  
  
**Length**  
Shows the number of characters allowed for character-based data types.  
  
**Nullable**  
Shows whether or not the column allows null values.  
  
**Precision**  
Shows the maximum number of digits allowed for numeric data types. This property shows **0** for nonnumeric data types.  
  
**Scale**  
Shows the maximum number of digits that can appear to the right of the decimal point for numeric data types. This value must be less than or equal to the precision. This property shows **0** for nonnumeric data types.  
  
## Column Properties Tab  
To access these properties, in Server Explorer right-click the table to which the column belongs, choose **Open Table Definition**, and select the row in the table grid in Table Designer.  
  
> [!NOTE]  
> These properties apply only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
**General Category**  
Expands to show **Name**, **Allow Nulls**, **Data Type**, **Default Value or Binding**, **Length**, **Precision**, and **Scale**.  
  
**Name**  
Displays the name of the column. To edit the name, type in the text box.  
  
> [!CAUTION]  
> If existing queries, views, user-defined functions, stored procedures, or programs refer to the column, the name modification will make these objects invalid.  
  
**Allow Nulls**  
Shows whether or not the column's data type allows null values.  
  
**Data Type**  
Shows the data type for the selected column. To edit this property, click its value, expand the drop-down list, and choose another value. For more information, see [Data Types (Transact-SQL)](https://msdn.microsoft.com/a54f7373-b247-4d61-8fb8-7f2ec7a8d0a4).  
  
**Default Value or Binding**  
Shows the default for this column when no value is specified for this column. The drop-down list contains all global defaults defined in the data source. To bind the column to a global default, select from the drop-down list. Alternatively, to create a default constraint for the column, type the default value directly as text.  
  
**Length**  
Shows the number of characters allowed for character-based data types. This property is only available for character-based data types.  
  
**Precision**  
Shows the maximum number of digits allowed for numeric data types. This property shows **0** for nonnumeric data types. This property is only available for numeric data types.  
  
**Scale**  
Shows the maximum number of digits that can appear to the right of the decimal point for numeric data types. This value must be less than or equal to the precision. This property shows **0** for nonnumeric data types. This property is only available for numeric data types.  
  
**Table Designer Category**  
Expands to show the remaining properties.  
  
**Collation**  
Shows the collation setting for the selected column. To change this setting, click **Collation** and then click the ellipses **(...)** to the right of the value.  
  
**Computed Column Specification Category**  
Expands to show properties for **Formula** and **Is Persisted**. If the column is computed, the formula will also be displayed. To edit the formula, expand this category and edit it in the **Formula** property.  
  
**Formula**  
Shows the formula that the selected column uses if it is a computed column. In this field you can enter or change a formula.  
  
**Is Persisted**  
Allows you to save the computed column with the data source. A persisted computed column can be indexed.  
  
**Condensed Data Type**  
Displays information about the field's data type, in the same format as the SQL CREATE TABLE statement. For example, a field containing a variable-length string with a maximum length of 20 characters would be represented as "varchar(20)." To change this property, type the value directly.  
  
**Description**  
Shows the description of the column. To see the full description or to edit it, click Description, and then click the ellipses **(...)** to the right of the property.  
  
**Full-text Specification Category**  
Expands to show properties specific to full-text columns.  
  
**Is Full-text Indexed**  
Indicates whether this column is full-text indexed. This property can be set to **Yes** only if the data type for this column is full-text searchable and if the table to which this column belongs has a full-text index specified for it. To change this value, click it, expand the drop-down list, and choose a new value.  
  
**Full-text type column**  
Shows which column is used to define the document type of a column of type image. The image data type can be used to store documents ranging from .doc files to xml files.  
  
**Language**  
Indicates the language used to index the column.  
  
**Statistical Semantics**  
Select whether to enable statistical semantic indexing for the selected column. For more information, see [Semantic Search placeholder](https://msdn.microsoft.com/cd8faa9d-07db-420d-93f4-a2ea7c974b97).  
  
If you select a **Language** prior to selecting **Statistical Semantics**, and the selected language does not have an associated Semantic Language Model, then the **Statistical Semantics** option is set to **No** and cannot be modified. If you select **Yes** for the **Statistical Semantics** option prior to selecting a **Language**, then the languages available in the **Language** column will be restricted to those for which there is Semantic Language Model support.  
  
**Has Non-SQL Server Subscriber**  
Shows whether the column has a non-Microsoft SQL Server subscriber.  
  
**Identity Specification Category**  
Expands to show properties for **Is Identity**, **Identity Increment**, and **Identity Seed**.  
  
**Is Identity**  
Shows whether the selected column is the identity column for the table. To change the property, open the table in Table Designer and edit the properties in the **Properties** window. This setting applies only to columns with a number-based data type, such as *int*.  
  
**Identity Increment**  
Shows the increment that will be added to the **Identity Seed** for each subsequent row. If you leave this cell blank, the value 1 will be assigned by default. To edit this property, type the new value directly.  
  
**Identity Seed**  
Shows the value assigned to the first row in the table. If you leave this cell blank, the value 1 will be assigned by default. To edit this property, type the new value directly.  
  
**Is Deterministic**  
Shows whether the data type of the selected column can be determined with certainty.  
  
**Is DTS-published**  
Shows whether the column is DTS-published.  
  
**Is Indexable**  
Shows whether the selected column can be indexed. For example, non-deterministic computed columns cannot be indexed.  
  
**Is Merge-published**  
Shows whether the column is merge-published.  
  
**Is Not For Replication**  
Indicates whether original identity values are preserved during replication. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
**Is Replicated**  
Shows whether this column is replicated in another location.  
  
**Is RowGuid**  
Indicates whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the column as a ROWGUID. You can set this value to **Yes** only for a column with the data type of **uniqueidentifier**. To edit this property, click its value, expand the drop-down list, and choose another value.  
  
**Size**  
Shows the size in bytes allowed by column's data type. For example, a **nchar** data type may have a length of 10 (the number of characters) but it would have a size of 20 to account for Unicode character sets.  
  
> [!NOTE]  
> The length of a **varchar(max)** data type varies for each row. sp_help returns (-1) as the length of **varchar(max)** column. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] displays -1 as the column size.  
  
