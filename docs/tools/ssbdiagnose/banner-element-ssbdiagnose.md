---
title: Banner Element
description: "Banner Element (ssbdiagnose)"
author: cwebb-msft
ms.author: chweb
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
ms.collection:
  - data-tools
helpviewer_keywords:
  - "banner element"
  - "XML output file format [ssbdiagnose], banner element"
  - "ssbdiagnose"
diagnose: In SQL Server, the Banner element identifies which utility generated the ssbdiagnose output XML file.
---

# Banner element (ssbdiagnose)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Identifies which utility generated the **ssbdiagnose** output XML file.

## Syntax

```xml
<Banner
    title = "..."
    product = "..."
    version = "..." />
```

## Element attributes

| Attribute | Description |
| --- | --- |
| **title** | Identifies the utility that generated the **ssbdiagnose** XML output file. |
| **product** | Identifies the product that generated the **ssbdiagnose** XML output file. |
| **version** | Identifies which version of the utility generated the XML output file. |

## Element characteristics

| Characteristic | Description |
| --- | --- |
| **Data type and length** | None. |
| **Default value** | None. |
| **Occurrence** | Occurs once per **ssbdiagnose** output XML file. |

## Element relationships

| Relationship | Elements |
| --- | --- |
| **Parent element** | [DiagnosticInformation element (ssbdiagnose)](diagnosticinformation-element-ssbdiagnose.md) |
| **Child elements** | None. |

## Examples

This is an example of a banner element.

```xml
<Banner title="Service Broker Diagnostics Utility" product="Microsoft SQL Server" version="10.0.1073.0" />
```

## Related content

- [ssbdiagnose utility (Service Broker)](ssbdiagnose-utility-service-broker.md)
