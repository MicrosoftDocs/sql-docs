---
title: "DTAXML Element (DTA)"
description: In the dta utility, the DTAXML element contains all elements that describe tuning input and output that the Database Engine Tuning Advisor generates.
author: cwebb-msft
ms.author: chweb
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
ms.collection:
  - data-tools
helpviewer_keywords:
  - "DTAXML element"
dev_langs:
  - "XML"
---

# DTAXML element (DTA)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The root element of a Database Engine Tuning Advisor XML input or output file, `DTAXML` contains all elements that describe tuning input and the tuning output that Database Engine Tuning Advisor generates.

## Syntax

```xml
<DTAXML
    xmlns:xsi = "http://www.w3.org/2001/XMLSchema-instance"
    xmlns = "http://schemas.microsoft.com/sqlserver/2004/07/dta">
    ...code removed here...
</DTAXML>
```

## Element attributes

| Attribute | Description |
| --- | --- |
| `xmlns:xsi` | Required. Identifies the XML Schema Instance namespace. Attributes from this namespace are used to reference the schema that is used to validate the Database Engine Tuning Advisor XML file.<br /><br />Required value: [http://www.w3.org/2001/XMLSchema-instance](https://www.w3.org/2001/XMLSchema-instance) |
| `xmlns` | Required. Identifies the Database Engine Tuning Advisor namespace.<br /><br />If you edit the Database Engine Tuning Advisor XML file using the XML editor in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], this value is used by F1 Help and Dynamic Help to locate possible reference articles in Microsoft Learn.<br /><br />Required value: [Database Engine Tuning Advisor XML Schema](https://go.microsoft.com/fwlink/?LinkId=43100) namespace. |

## Element characteristics

| Characteristic | Description |
| --- | --- |
| **Data type and length** | None. |
| **Default value** | None. |
| **Occurrence** | Required once per DTA XML file. |

## Element relationships

| Relationship | Elements |
| --- | --- |
| **Parent element** | None |
| **Child elements** | [DTAInput element (DTA)](dtainput-element-dta.md)<br /><br />`DTAOutput` element. For more information, see [Database Engine Tuning Advisor XML schema](https://schemas.microsoft.com/sqlserver/). |

## Remarks

For more information about XML namespaces, see [Namespaces in an XML Document](/dotnet/standard/data/xml/managing-namespaces-in-an-xml-document).

## Examples

For examples of typical `DTAXML` elements, see [XML Input File Samples (DTA)](xml-input-file-samples-dta.md).

## Related content

- [XML Input File Reference (Database Engine Tuning Advisor)](xml-input-file-reference-database-engine-tuning-advisor.md)
- [Start and use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md)
