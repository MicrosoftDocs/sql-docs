---
title: "Microsoft Connector for Oracle"
description: "Microsoft Connector for Oracle"
author: chugugrace
ms.author: chugu
ms.date: "01/18/2024"
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
---
# Microsoft Connector for Oracle

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver.md)]

Microsoft Connector for Oracle enables the ability to export data from and load data into Oracle data source in an SSIS package.

## Version support

The following Microsoft SQL Server products are supported by Microsoft Connector for Oracle:

- Since SQL Server 2019 CU1
- Microsoft SQL Server 2022
- SQL Server Integration Services Projects for Visual Studio 2019
- SQL Server Integration Services Projects for Visual Studio 2022

The following Oracle database versions of data source are supported:

- Oracle 10.x
- Oracle 11.x
- Oracle 12c
- Oracle 18c (without Windows Authentication support)
- Oracle 19c (without Windows Authentication support)

The Oracle database is supported on all operating systems and platforms.

## Installation

To install the connector for Oracle database, download and run the installer from [the latest version of Microsoft connector for Oracle](https://aka.ms/SSISMSOracleConnector). Then follow the directions in the installation wizard.  

Visual Studio will expect the x86 version of the connector to be installed on the machine being used to develop SSIS packages. By default, packages in the SSIS catalog will run in 64-bit mode and therefore use the x64 version, but they can be configured to run in 32-bit mode. To accommodate both modes, you will need to install both the 32-bit and 64-bit versions of the connector.   

After you install the connector, you must restart the SQL Server Integration Service to be sure that the Oracle source and destination can work correctly.

> [!NOTE]
>
> To design packages with SQL Server Integration Services Projects, you will need to install the appropriate connector for both the target and the latest SQL Server version.
> 
> For example, if you upgrade to Visual Studio 2022 but deploy to SQL Server Integration Services 2019, you need to install MicrosoftSSISOracleConnector-SQL22 and MicrosoftSSISOracleConnector-SQL19. 

To execute SSIS packages targeting SQL Server 2019 and above, you do not need to install an **Oracle client** to use the Microsoft Connector for Oracle. To execute SSIS packages targeting SQL Server 2017 and below, in addition to **Microsoft Connector for Oracle**, you will need to install an **Oracle client** and **Microsoft Connector for Oracle by Attunity** with the corresponding version from below links:

- [SQL Server 2017: Microsoft Connector Version 5.0 for Oracle by Attunity](https://www.microsoft.com/download/details.aspx?id=55179)
- [SQL Server 2016: Microsoft Connector Version 4.0 for Oracle by Attunity](https://www.microsoft.com/download/details.aspx?id=52950)
- [SQL Server 2014: Microsoft Connector Version 3.0 for Oracle by Attunity](https://www.microsoft.com/download/details.aspx?id=44582)
- [SQL Server 2012: Microsoft Connector Version 2.0 for Oracle by Attunity](https://www.microsoft.com/download/details.aspx?id=29283)

## Limitations and known issues

- Views are not listed under Oracle source *Name of the table or the view*. As work-around, use the SQL command and do a select * from view, or set view name to property [Oracle Source].[TableName] in Advanced Editor.
- The Microsoft Connector for Oracle can only be used by implementing an [Oracle Source](oracle-source.md) or [Oracle Destination](oracle-destination.md) in a data flow task. It is not available for other SSIS tasks, including Execute SQL tasks. 

## Uninstallation

You can run uninstall wizard to remove Microsoft Connector for Oracle database from SQL Server.

## Next steps

- Configure [Oracle Connection Manager](oracle-connection-manager.md).
- Configure [Oracle Source](oracle-source.md).
- Configure [Oracle Destination](oracle-destination.md).
- If you have questions, visit [TechCommunity](https://aka.ms/AA5u35j).
