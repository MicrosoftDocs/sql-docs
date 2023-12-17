---
title: "SQLDriverConnect (Visual FoxPro ODBC Driver)"
description: "SQLDriverConnect (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLDriverConnect function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLDriverConnect (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 1.

## Remarks

Connects to an existing data source, which can be either a [database](visual-foxpro-terminology.md) or a directory of [free tables](visual-foxpro-terminology.md). The ODBC attribute keywords `UID` and `PWD` are ignored. The following table lists the additional supported attribute keywords.

| ODBC attribute keyword | Attribute value |
| --- | --- |
| `DSN` | |
| `UID` | Ignored by the Visual FoxPro ODBC Driver, but doesn't generate an error. |
| `PWD` | Ignored by the Visual FoxPro ODBC Driver, but doesn't generate an error. |
| `Driver` | The name and location of the Visual FoxPro ODBC Driver; implemented by the Driver Manager. |

| Visual FoxPro ODBC Driver attribute keyword | Attribute value |
| --- | --- |
| `BackgroundFetch` | `Yes` or `No` |
| `Collate` | "Machine" or other collating sequence. For a list of supported collating sequences, see [SET COLLATE Command](set-collate-command.md). |
| `Description` | |
| `Exclusive` | `Yes` or `No` |
| `SourceDB` | A fully qualified path to a directory containing zero or more [free tables](visual-foxpro-terminology.md), or the absolute path and file name for a [database](visual-foxpro-terminology.md). |
| `SourceType` | `DBC` or `DBF` |
| `Version` | |

If the data source name isn't specified, the Driver Manager prompts the user for the information (depending on the setting of the `fDriverCompletion` argument) and then continues. If more information is required, the Visual FoxPro ODBC Driver displays the prompt dialog.

For more information, see [SQLDriverConnect Function](../reference/syntax/sqldriverconnect-function.md) in the *ODBC Programmer's Reference*.
