---
description: "MSSQLSERVER_6602"
title: MSSQLSERVER_6602
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "6602 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_6602
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|6602|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|XMLERR_PARSEERR2|
|Message Text|The error description is '%.*ls'.|

## Explanation

This error occurs when you try to execute a `sp_xml_preparedocument` stored procedure in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in which the content of the `xmltext` parameter is a complex XML document, An error message similar to the following is reported to the user

> The XML parse error 0x80004005 occurred on line number 1, near the XML text "\<XML document sample>"  
Msg 6602, Level 16, State 2, Procedure sp_xml_preparedocument, Line 1  
The error description is 'Unspecified error'.

## Cause

This issue occurs because of a design limitation of the MSXML parser (Msxmlsql.dll) that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses.

The issue is not strictly related to the size of the XML document but to its complex structure. A combination of the XML element's structure depth, the number and size of attributes, and the number of entities within attributes can cause this issue. However, the level of complexity that is required to reach this limit is found in XML documents that are several megabytes.

## User action

To work around this issue, try to reduce the complexity of the XML document.

> [!NOTE]
> Beware of very large single string attributes that contain many XML \ entities.
