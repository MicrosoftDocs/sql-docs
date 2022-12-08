---
description: "Microsoft Connectors for Oracle and Teradata by Attunity for Integration Services (SSIS)"
title: "Microsoft Connectors for Oracle and Teradata by Attunity | Microsoft Docs"

ms.date: "08/16/2019"
ms.service: sql
ms.reviewer: ""
ms.custom: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 
author: chugugrace
ms.author: chugu
---
# Microsoft Connectors for Oracle and Teradata by Attunity for Integration Services (SSIS)

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

> [!NOTE]
> Atunity Connectors for Oracle and Teradata support SQL Server 2017 and below.
>
> From SQL Server 2019, get latest connectors for Oracle and Teradata here:
> - [Microsoft connector for Oracle](data-flow/oracle-connector.md)
> - [Microsoft connector for Teradata](data-flow/teradata-connector.md)

You can download connectors for Integration Services by Attunity that optimize performance when loading data to or from Oracle or Teradata in an SSIS package.

## Download the latest Attunity connectors

Get the latest version of the connectors here:  
[Microsoft Connectors v5.0 for Oracle and Teradata](https://www.microsoft.com/download/details.aspx?id=55179)

## Issue - The Attunity connectors aren't visible in the SSIS Toolbox

To see the Attunity connectors in the SSIS Toolbox, you always have to install the version of the connectors that targets the same version of SQL Server as the version of SQL Server Data Tools (SSDT) installed on your computer. (You may also have earlier versions of the connectors installed.) This requirement is independent of the version of SQL Server that you want to target in your SSIS projects and packages.

For example, if you've installed the latest version of SSDT, you have version 17 of SSDT with a build number that starts with 14. This version of SSDT adds support for SQL Server 2017. To see and use the Attunity connectors in SSIS package development - even if you want to target an earlier version of SQL Server - you also have to install the latest version of the Attunity connectors, version 5.0. This version of the connectors also adds support for SQL Server 2017.

Check the installed version of SSDT in Visual Studio from **Help** | **About Microsoft Visual Studio**, or in **Programs and Features** in the Control Panel. Then install the corresponding version of the Attunity connectors from the following table.

|SSDT version|SSDT build number|Target SQL Server version|Required version of Connectors|
|---------|---------|---------|---------|
|17|Starts with 14|SQL Server 2017|[Microsoft Connectors v5.0 for Oracle and Teradata](https://www.microsoft.com/download/details.aspx?id=55179)|
|16|Starts with 13|SQL Server 2016|[Microsoft Connectors v4.0 for Oracle and Teradata](https://www.microsoft.com/download/details.aspx?id=52950)|

## Download the latest SQL Server Data Tools (SSDT)

Get the latest version of SSDT here:  
[Download SQL Server Data Tools (SSDT)](..//ssdt/download-sql-server-data-tools-ssdt.md)
