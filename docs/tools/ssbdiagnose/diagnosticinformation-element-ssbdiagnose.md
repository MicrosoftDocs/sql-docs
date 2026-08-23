---
title: DiagnosticInformation Element
description: In SQL Server, the DiagnosticInformation element is the root element of a ssbdiagnostic XML output file.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
ms.collection:
  - data-tools
helpviewer_keywords:
  - "XML output file format [ssbdiagnose], diagnosticinformation element"
  - "diagnosticinformation element"
  - "ssbdiagnose"
---

# DiagnosticInformation element (ssbdiagnose)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `DiagnosticInformation` element contains all elements that report the diagnostic information found by the utility. `DiagnosticInformation` is the root element of a **ssbdiagnostic** XML output file.

## Syntax

```xml
<DiagnosticInformation>
    ...
</DiagnosticInformation>
```

## Element attributes

| Attribute | Description |
| --- | --- |
| **None** | N/A |

## Element characteristics

| Characteristic | Description |
| --- | --- |
| **Data type and length** | None. |
| **Default value** | None. |
| **Occurrence** | Once per **ssbdiagnose** XML output file. |

## Element relationships

| Relationship | Elements |
| --- | --- |
| **Parent element** | None. |
| **Child elements** | [Banner element (ssbdiagnose)](banner-element-ssbdiagnose.md)<br />[Issue element (ssbdiagnose)](issue-element-ssbdiagnose.md) |

## Remarks

For more information about XML namespaces, see [Namespaces in an XML Document](/dotnet/standard/data/xml/managing-namespaces-in-an-xml-document).

## Related content

- [ssbdiagnose utility (Service Broker)](ssbdiagnose-utility-service-broker.md)
