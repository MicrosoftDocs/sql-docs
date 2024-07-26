---
title: MSSQLSERVER_17112
description: "MSSQLSERVER_17112"
author: suresh-kandoth
ms.author: sureshka
ms.reviewer: jopilov, mathoma, randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17112 (Database Engine error)"
---
# MSSQLSERVER_17112

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 17112 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | INIT_INVCOMMAND |
| Message Text | An invalid startup option a was supplied, either from the registry or the command prompt. Correct or remove the option. |

## Explanation

This error indicates that an invalid [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md) was specified. When a startup option isn't properly specified, SQL Server either fails to start or might not run as expected. Error 17112 is also raised.

In some cases, the instance might start, but when you review the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Error log, the startup parameters don't look right:

> \<Datetime> Server Registry startup parameters:  
\<Datetime> Server -d D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\master.mdf  
\<Datetime> Server -e D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\LOG\ERRORLOG  
\<Datetime> Server -l D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\mastlog.ldf  
\<Datetime> Server -T1118 -g512

Notice how both the last two startup parameters are on the same line.

You might also notice in some cases that adding the necessary startup parameters didn't have the intended effect on the server behavior.

## Possible causes

You encounter these problems due to the following reasons:

- Using startup parameters that aren't present in the valid list of startup parameters
- Specifying startup parameters without the proper delimiters [;]
- Copy and paste the startup parameters from text editors that introduced some invisible special characters, [for example, a space before the -T]
- Not using the correct case sensitivity for the startup parameter

## User action

Use the SQL Server Configuration Manager tool to supply and validate the startup parameters specified for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Ensure each one of the startup parameters is delimited properly and that no special characters are present.

## Related content

- [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md)
- [SQL Server Configuration Manager: Configure server startup options](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md)
