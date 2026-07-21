---
title: Find Property Set GUIDs & Property Integer IDs for Search Properties
description: Find property set GUIDs and property integer IDs for search properties
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "full-text search [SQL Server], search property lists"
  - "search property lists [SQL Server], configuring"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-current || =azuresqldb-mi-current"
---
# Find property set GUIDs and property integer IDs for search properties

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article discusses how to obtain the values that are required before you can add a property to a search property list and make it searchable by full-text search. These values include the property set GUID and property integer identifier of a document property.

You can make document properties that IFilters extract from binary data available for full-text search. This binary data comes from data stored in a **varbinary(max)** (including FILESTREAM) or **image** data type column. To make an extracted property searchable, you must manually add the property to a search property list. You must also associate the search property list with one or more full-text indexes. For more information, see [Search document properties with search property lists](search-document-properties-with-search-property-lists.md).

Before you can add an available property to a property list, you have to find two pieces of information about the property:

- The property set GUID of the property.

- The integer ID of the property.

(When you add a property to a property list, you also have to provide a name and description. However, you don't have to use the canonical name and description of the property.)

This article describes the commonly used methods to find information about available properties, especially about properties that are defined by Microsoft. For information about properties that have been defined by a third party, refer to the third-party documentation or contact the vendor.

<a id="wellknown"></a>

<a id="finding-information-about-widely-used-well-known-microsoft-properties"></a>

## Find information about widely used, well-known Microsoft properties

Microsoft defines hundreds of document properties for use in many contexts, but only a small subset of the available properties are used by each file format. Among the frequently used Windows properties is a small set of generic properties. Some examples of well-known generic properties are shown in the following table. The table shows the well-known name, the Windows canonical name (from the property description published by Microsoft), the property set GUID, the property integer identifier, and a brief description.

| Well-known name | Windows canonical name | Property set GUID | Integer ID | Description |
| --- | --- | --- | --- | --- |
| Authors | **System.Author** | F29F85E0-4FF9-1068-AB91-08002B27B3D9 | 4 | Author or authors of a given item. |
| Tags | **System.Keywords** | F29F85E0-4FF9-1068-AB91-08002B27B3D9 | 5 | Set of keywords (also known as tags) assigned to the item. |
| Type | **System.PerceivedType** | 28636AA6-953D-11D2-B5D6-00C04FD918D0 | 9 | Perceived file type based on its canonical type. |
| Title | **System.Title** | F29F85E0-4FF9-1068-AB91-08002B27B3D9 | 2 | Title of the item. For example, the title of a document, the subject of a message, the caption of a photo, or the name of a music track. |

To encourage consistency among file formats, Microsoft has identified subsets of frequently used, high-priority document properties for several categories of documents. These include communications, contacts, documents, music files, pictures, and videos. For more information about the top-ranked properties for each category, see [system-defined properties for custom file formats](/windows/win32/search/-shell-systemdefinedpropertiesforfileformats) in the Windows Search documentation.

A specific file format might implement properties of three types:

- Generic properties defined by [!INCLUDE [msCoName](../../includes/msconame-md.md)].

- Category-specific properties defined by [!INCLUDE [msCoName](../../includes/msconame-md.md)].

- Custom, application-specific properties defined by the software vendor.

<a id="filtdump"></a>

<a id="finding-information-about-available-properties-by-using-filtdumpexe"></a>

## Find information about available properties by using filtdump.exe

To learn what properties are discovered and extracted by an installed IFilter, you can install and run the `filtdump.exe` utility, which is part of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows SDK.

You run `filtdump.exe` from the command prompt and provide a single argument. This argument is the name of an individual file that has a file type for which an IFilter is installed. The utility displays a list of all the properties discovered by the IFilter in the document, with their property set GUIDs, integer IDs, and additional information.

For information about installing this software, see [Microsoft Windows SDK for Windows 7 and .NET Framework 4](https://www.microsoft.com/download/details.aspx?id=8442). After you download and install the SDK, look in the following folders for the `filtdump.exe` utility.

- For the 64-bit version, look in `C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\x64`.

- For the 32-bit version, look in `C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin`.

<a id="propdesc"></a>

<a id="finding-values-for-a-search-property-from-a-windows-property-description"></a>

## Find values for a search property from a Windows property description

For a well-known Windows search property, you can obtain the information that you need from the **formatID** and **propID** attributes of the property description (**propertyDescription**).

The following example shows the relevant part of a typical Microsoft property description, in this case, of the `System.Author` property. The `formatID` attribute specifies the property set GUID, `F29F85E0-4FF9-1068-AB91-08002B27B3D9`, and the `propID` attribute specifies the property integer ID, `4`. The `name` attribute specifies the Windows canonical property name, `System.Author`. (This example omits portions of the property description that aren't relevant.)

```
.
propertyDescription
name = System.Author
...
formatID = F29F85E0-4FF9-1068-AB91-08002B27B3D9
propID = 4
...
```

For the complete description of this property, see [System.Author](/windows/win32/properties/props-system-author) in the Windows Search documentation.

For a complete list of Windows properties, see [Windows Properties](/windows/win32/properties/props), also in the Windows Search documentation.

<a id="examples"></a>

<a id="adding-a-property-to-a-search-property-list"></a>

## Add a property to a search property list

The following example shows how to add a property to a search property list. The example uses an [ALTER SEARCH PROPERTY LIST](../../t-sql/statements/alter-search-property-list-transact-sql.md) statement to add the `System.Author` property to a search property list named `PropertyList1`, and provides a user-friendly name for the property, `Author`.

```sql
ALTER SEARCH PROPERTY LIST PropertyList1
  ADD 'Author'
    WITH (
          PROPERTY_SET_GUID = 'F29F85E0-4FF9-1068-AB91-08002B27B3D9',
          PROPERTY_INT_ID = 4,
          PROPERTY_DESCRIPTION = 'System.Author - the author or authors of the item'
         )
GO
```

For more information about creating a search property list and associating it with a full-text index, see [Search document properties with search property lists](search-document-properties-with-search-property-lists.md).

## Related content

- [Search document properties with search property lists](search-document-properties-with-search-property-lists.md)
- [Configure and manage filters](configure-and-manage-filters-for-search.md)
