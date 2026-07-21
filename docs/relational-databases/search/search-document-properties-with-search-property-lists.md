---
title: Search Document Properties with Search Property Lists
description: Search Document Properties with Search Property Lists
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "full-text search [SQL Server], search property lists"
  - "full-text search [SQL Server], properties"
  - "search property lists [SQL Server]"
  - "property searching [SQL Server], about"
  - "full-text indexes [SQL Server], search property lists"
  - "search property lists [SQL Server], about"
  - "property searching [SQL Server]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-current || =azuresqldb-mi-current"
---
# Search document properties with search property lists

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can configure a full-text index to support property-scoped searching on particular properties, such as Author and Title, for supported document types in a **varbinary(max)** (including FILESTREAM) or **image** binary data column. Without this configuration, full-text queries perform generic searches on the entire document, without distinguishing property content from body content. This form of property-scoped searching is known as *property searching*.

The associated [filter](configure-and-manage-filters-for-search.md) (IFilter) determines whether property searching is possible on a specific type of document. For some document types, the associated IFilter extracts some or all of the properties defined for that type of document, as well as the content of the document body. You can configure a full-text index to support property searching only on properties that are extracted by an IFilter during full-text indexing. Among IFilters that extract a number of document properties are the IFilters for Microsoft Office document types (such as `.docx`, `.xlsx`, and `.pptx`). On the other hand, the XML IFilter doesn't emit properties.

<a id="How_FTS_Works_with_search_properties"></a>

## How Full-Text Search works with search properties

Full-Text Search supports property searching by assigning an internal property ID to each registered property and by storing those IDs in the full-text index.

### Internal property IDs

The Full-Text Engine arbitrarily assigns each registered property an internal property ID, which uniquely identifies the property in that particular search list and which is specific to that search property list. Therefore, if a property is added to multiple search property lists, its internal property ID is likely to differ between different lists.

When a property is registered for a search list, the Full-Text Engine arbitrarily assigns an *internal property ID* to the property. The internal property ID is an integer that uniquely identifies the property in that search property list.

The following illustration shows a logical view of a search property list that specifies two properties, Title and Keywords. The property-list name for Keywords is `Tags`. These properties belong to the same property set, whose GUID is `f29f85e0-4ff9-1068-ab91-08002b27b3d9`. The property integer identifiers are 2 for Title and 5 for Tags (Keywords). The Full-Text Engine arbitrarily maps each property to an internal property ID that is unique to the search property list. The internal property ID for the Title property is 1, and the internal property ID for the Tags property is 2.

:::image type="content" source="media/search-document-properties-with-search-property-lists/title-keywords.png" alt-text="Screenshot of mapping of search property list to internal table.":::

The internal property ID is likely to be different from the property integer identifier of the property. If a given property is registered for multiple search property lists, a different internal property ID might be assigned for each search property list. For example, the internal property ID might be 4 in one search property list, 1 in another, 3 in another, and so on. In contrast, the property integer identifier is intrinsic to the property, and it remains the same wherever the property is used.

### Index of registered properties

After a full-text index is associated with a search property list, the index must be repopulated to index property-specific search terms. During full-text indexing, the contents of all properties are stored in the full-text index along with other content. However, when indexing a search term found in a registered property, the full-text indexer also stores the corresponding internal property ID with the term. In contrast, if a property isn't registered, it's stored in the full-text index as if it were part of the document body, and it has a value of zero for the internal property ID.

The following illustration shows a logical view of how search terms appear in a full-text index that is associated with the search property list shown in the preceding illustration. A sample document, Document 1, contains three properties (Title, Author, and Keywords), as well as the document body. For the properties Title and Keywords, which are specified in the search property list, search terms are associated with their corresponding internal property IDs in the full-text index. In contrast, the content of the Author property is indexed as if it were part of the document body. This means registering a property increases the size of the full-text index somewhat, depending on the amount of content stored in the property.

:::image type="content" source="media/search-document-properties-with-search-property-lists/search-property-list.png" alt-text="Screenshot of full-text index that uses a search property list.":::

Search terms in the Title property (`Favorite`, `Biking`, and `Trails`) are associated with the internal property ID assigned to Title for this index, 1. Search terms in the Keywords property (`biking` and `mountain`) are associated with the internal property ID assigned to Tags for this index, 2. For search terms in the Author property (`Jane` and `Doe`) and search terms in the document body, the internal property ID is 0. The term `biking` occurs in the Title property, in the Keywords (Tags) property, and in the document body. A property search for `biking` in the Title or Keywords (Tags) property would return this document in the results. A generic full-text query for `biking` would also return this document, as if the index weren't configured for property searching. A property search for `biking` in the Author property wouldn't return this document.

A property-scoped full-text query uses the internal property IDs registered for the current search property list of the full-text index.

<a id="impact"></a>

## Impact of enabling property searching

Configuring a full-text index to support searching on one or more properties increases the size of the index somewhat, depending on the number of properties you specify in your search property list and on the content of each property.

In testing typical corpuses of Microsoft Word, Excel, and PowerPoint documents, a full-text index was configured to index typical search properties. Indexing these properties increased the size of the full-text index by approximately 5 percent. This approximate size increase is likely to be typical for most document corpuses. However, ultimately, the size increase depends on the amount of property data in a given document corpus relative to the amount of overall data.

<a id="creating"></a>

## Create a search property list and enable property search

To enable property searching, create a search property list, add one or more properties to the list, and then associate the list with a full-text index.

<a id="creating_sub"></a>

### Create a search property list

Create a search property list by using either Transact-SQL or SQL Server Management Studio.

#### Create a search property list in Transact-SQL

Use the [CREATE SEARCH PROPERTY LIST](../../t-sql/statements/create-search-property-list-transact-sql.md) statement and provide at least a name for the list.

#### Create a search property list in SQL Server Management Studio

1. In Object Explorer, expand the server.

1. Expand **Databases**, and then expand the database in which you want to create the search property list.

1. Expand **Storage**, and then right-click **Search Property Lists**.

1. Select **New Search Property List**.

1. Specify the property list name.

1. Optionally, specify someone else as the property list owner.

1. Select one of the following options:

   - **Create an empty search property list**

   - **Create from an existing search property list**

   For more information, see [CREATE SEARCH PROPERTY LIST](../../t-sql/statements/create-search-property-list-transact-sql.md).

1. Select **OK**.

<a id="adding"></a>

### Add properties to a search property list

Property searching requires creating a *search property list* and specifying one or more properties that you want to make searchable. When you add a property to a search property list, the property is registered for that particular list. To add a property to a search property list you need the following values:

- Property set GUID

  Each search property belongs to single property set that contains a group of related properties. Each property set is identified by a globally unique identifier (GUID).

- Property integer identifier

  Each search property possesses an identifier that is unique within the property set. For a given property, the identifier could be either an integer or a string, however full-text search supports only integer identifiers.

- Property name

  This is the name that users will specify in full-text queries to search on the property. A property name can contain internal spaces. The maximum length is 256 characters.

  The property name can be any of the following:

  - The Windows canonical name of the property, such as `System.Author` or `System.Contact.HomeAddress`.

  - A user-friendly name that is easy for your users to remember. Some properties are associated with a well-known user-friendly name, such as "Author" or "Home Address," but you can specify whatever name is most appropriate to your users.

  > [!NOTE]  
  > A given combination of property set GUID and property identifier must be unique in a given search property list. This means that you can't add the same property more than once with different names or descriptions.

- Property description (optional)

  When adding a search property to a search property list, you can supply an optional description. For example, you might want to provide information about a property that isn't evident from its name, or you might want to describe the property set of the property.

### Obtain values for a search property list

See [Find property set GUIDs and property integer IDs for search properties](find-property-set-guids-and-property-integer-ids-for-search-properties.md).

#### Add a property to a search property list in Transact-SQL

Use the [ALTER SEARCH PROPERTY LIST](../../t-sql/statements/alter-search-property-list-transact-sql.md) statement with the values that you obtained by using one of the methods described in the article, [Find property set GUIDs and property integer IDs for search properties](find-property-set-guids-and-property-integer-ids-for-search-properties.md).

The following example demonstrates the use of these values when adding a property to a search property list:

```sql
ALTER SEARCH PROPERTY LIST DocumentTablePropertyList
ADD 'Title' WITH (
    PROPERTY_SET_GUID = 'F29F85E0-4FF9-1068-AB91-08002B27B3D9',
    PROPERTY_INT_ID = 2,
    PROPERTY_DESCRIPTION = 'System.Title - Title of the item.'
);
```

#### Add a property to a search property list in SQL Server Management Studio

Use the **Search Property List Properties** dialog box to add and remove search properties. You can find **Search Property Lists** in Object Explorer under the **Storage** node of the associated database.

<a id="associating"></a>

### Associate a search property list with a full-text index

For a full-text index to support property searching on the properties that are registered for a search property list, you need to associate the search property list with the index and repopulate the index. Repopulating the full-text index creates property-specific index entries for search terms in each of the registered properties.

As long as the full-text index remains associated with this search property list, a full-text query can use the `PROPERTY` option of the `CONTAINS` predicate to search on properties that are registered for that search property list.

If you change the search property list associated with a full-text index, then the index must be rebuilt to bring it into a consistent state. The index is truncated immediately and is empty until the full population runs. For more information about when changing the search property list causes rebuilding the index, see "Remarks," in [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md).

#### Associate a search property list with a full-text index in Transact-SQL

Use the [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) statement with the `SET SEARCH PROPERTY LIST = <property_list_name>` clause.

#### Associate a search property list with a full-text index in SQL Server Management Studio

Specify a value for **Search Property List** on the **General** page of the **Full-Text Index Properties** dialog box.

<a id="Ov_CONTAINS_using_PROPERTY"></a>

## Query search properties with CONTAINS

The basic [CONTAINS](../../t-sql/queries/contains-transact-sql.md) syntax for a property-scoped full-text query is as follows:

```sql
SELECT column_name
FROM table_name
WHERE CONTAINS (PROPERTY(column_name, 'property_name'), '<contains_search_condition>');
```

For example, the following query searches on an indexed property, `Title`, in the `Document` column of the `Production.Document` table of the `AdventureWorks` database. The query returns only documents whose `Title` property contains the string `Maintenance` or `Repair`.

```sql
USE AdventureWorks2025;
GO

SELECT Document
FROM Production.Document
WHERE CONTAINS (PROPERTY(Document, 'Title'), 'Maintenance OR Repair');
GO
```

This example assumes that the IFilter for the document extracts its Title property, that the Title property is added to the search property list, and that the search property list is associated with the full-text index.

<a id="managing"></a>

## Manage search property lists

After you create a search property list, you can view, change, or delete it.

<a id="viewing"></a>

### View and change a search property list

View and change a search property list by using either Transact-SQL or SQL Server Management Studio.

#### View and change a search property list in Transact-SQL

Use the [ALTER SEARCH PROPERTY LIST](../../t-sql/statements/alter-search-property-list-transact-sql.md) statement to add or remove search properties.

#### View and change a search property list in SQL Server Management Studio

1. In Object Explorer, expand the server.

1. Expand **Databases**, and then expand the database.

1. Expand **Storage**.

1. Expand **Search Property Lists** to display the search property lists.

1. Right-click the property list, and select **Properties**.

1. In the **Search Property List Editor** dialog box, use the Properties grid to add or remove search properties:

   1. To remove a document property, select the row header to the left of the property, and press DEL.

1. To add a document property, select the empty row at the bottom of the list, to the right of the `*`, and enter the values for the new property.

      For information about these values, see [Search Property List Editor](../../t-sql/statements/create-search-property-list-transact-sql.md). For information about how to obtain these values for properties defined by Microsoft, see [Find property set GUIDs and property integer IDs for search properties](find-property-set-guids-and-property-integer-ids-for-search-properties.md). For information about properties defined by an independent software vendor (ISV), see the documentation of that vendor.

1. Select **OK**.

<a id="deleting"></a>

### Delete a search property list

You can't drop a property list from a database while the list is associated with any full-text index.

#### Delete a search property list in Transact-SQL

Use the [DROP SEARCH PROPERTY LIST](../../t-sql/statements/drop-search-property-list-transact-sql.md) statement.

#### Delete a search property list in SQL Server Management Studio

1. In Object Explorer, expand the server.

1. Expand **Databases**, and then expand the database.

1. Expand **Storage**, and then expand the **Search Property Lists** node.

1. Right-click the property list that you want to delete, and select **Delete**.

1. Select **OK**.

## Related content

- [Find property set GUIDs and property integer IDs for search properties](find-property-set-guids-and-property-integer-ids-for-search-properties.md)
- [Configure and manage filters](configure-and-manage-filters-for-search.md)
