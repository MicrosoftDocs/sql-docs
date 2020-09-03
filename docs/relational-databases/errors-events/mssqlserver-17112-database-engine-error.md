---
description: "MSSQLSERVER_17112"
title: MSSQLSERVER_17112
ms.custom: ""
ms.date: 08/20/2020
ms.prod: sql
ms.reviewer: ramakoni, pijocoder, suresh-kandoth, Masha
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "17112 (Database Engine error)"
ms.assetid: 
author:
ms.author: ramakoni
---
# MSSQLSERVER_17112

 [!INCLUDE [SQL Server](../../includes/ssnoversion-md.md)]
 [!INCLUDE [SQL Server 2019](../../includes/sssqlv15-md.md)]
 [!INCLUDE [SQL Server 2017](../../includes/sssql17-md.md)]
 [!INCLUDE [SQL Server 2016](../../includes/sssql15-md.md)]
 [!INCLUDE [SQL Server 2014](../../includes/sssql14-md.md)]
 [!INCLUDE [SQL Server 2012](../../includes/sssql11-md.md)]
 [!INCLUDE [SQL Server 2008](../../includes/sskatmai-md.md)]
 [!INCLUDE [Azure SQL DB](../../includes/sssdsfull-md.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|17112|
|Event Source|MSSQLSERVER|
|Component|SQL SQLEngine|
|Symbolic Name|INIT_INVCOMMAND|
|Message Text|An invalid startup option a was supplied, either from the registry or the command prompt. Correct or remove the option.|
||

## Explanation

This error indicates that an invalid startup option (link to Database Engine startup options) was specified. When a startup option is not properly specified, SQL Server either fails to start or may not run as expected. Error 17112 is also raised.

In some cases, the instance might start, but when you review the SQL Server Error log, the startup parameters do not look right:

> \<Datetime> Server Registry startup parameters:  
\<Datetime> Server -d D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\master.mdf  
\<Datetime> Server -e D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\LOG\ERRORLOG  
\<Datetime> Server -l D:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\mastlog.ldf  
\<Datetime> Server -T1118 -g512

Notice how both the last two startup parameters are on the same line.

You may also notice in some cases that adding the necessary startup parameters did not have the intended effect on the server behavior.

## Possible causes

You encounter these problems due to the following reasons:

- Using startup parameters that are not present in the valid list of startup parameters
- Specifying startup parameters without the proper delimiters [;]
- Copy and paste the startup parameters from text editors that introduced some invisible special characters, [for example, a space before the -T]
- Not using the correct case sensitivity for the startup parameter

## User action

Use the SQL Server Configuration Manager tool to supply and validate the startup parameters specified for the instance of SQL Server. Ensure each one of the startup parameters is delimited properly and that no special characters are present.

## More information

Refer to the following topics for more information on this topic:

- [Database Engine Service Startup Options](/sql/database-engine/configure-windows/database-engine-service-startup-options)
- [SCM Services - Configure Server Startup Options](/sql/database-engine/configure-windows/scm-services-configure-server-startup-options)
