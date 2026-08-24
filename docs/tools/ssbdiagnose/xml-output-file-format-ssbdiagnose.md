---
title: XML Output File Format
description: In SQL Server, the ssbdiagnose utility can deliver its output as an XML file. Create an application to analyze or report errors or view them in an XML editor.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
ms.collection:
  - data-tools
helpviewer_keywords:
  - "XML output file format [ssbdiagnose]"
  - "ssbdiagnose"
---

# XML output file format (ssbdiagnose)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The **ssbdiagnose** utility delivers its output as an XML file when you run it with the `-XML` switch. The XML output file lists header information and the errors that it found in the [!INCLUDE [ssSB](../../includes/sssb-md.md)] configuration or conversation that was analyzed. You can write an application to analyze or report on the errors listed in the file. Or, you can view the XML file in a general XML editor, such as XML Notepad.

An **ssbdiagnose** output file contains a `DiagnosticInformation` root element with two child types:

- A `Banner` element with header information.
- An `Issue` element for each issue that is reported by **ssbdiagnose**.

## Root element

- [DiagnosticInformation element (ssbdiagnose)](diagnosticinformation-element-ssbdiagnose.md)

## Child elements

- [Banner element (ssbdiagnose)](banner-element-ssbdiagnose.md)
- [Issue element (ssbdiagnose)](issue-element-ssbdiagnose.md)

## Related content

- [ssbdiagnose utility (Service Broker)](ssbdiagnose-utility-service-broker.md)
