---
title: Microsoft SQL Operations Studio (preview) release notes| Microsoft Docs
description: 'Microsoft SQL Operations Studio (preview) release notes'
ms.custom: "tools|sos"
ms.date: "02/15/2018"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# SQL Operations Studio (preview) release notes

**[Download the February Public Preview](download.md)**

## February 2018 (February Public Preview)

release date: February 15, 2018  
version: 0.26.3

The *February Public Preview* includes some feature suggestions and high-priority bug fixes. This release includes the following enhancements:

- The Connection Dialog 'Database' field is now a dynamically populated drop-down list that will contain a list of databases populated from the specified server.

- Fix [issue 6](https://github.com/Microsoft/sqlopsstudio/issues/6): Keep connection and selected database when opening new query tabs.
- Fix [issue 22](https://github.com/Microsoft/sqlopsstudio/issues/22): 'Server Name' and 'Database Name' - Can these be drop downs instead of text boxes?
- Fix [issue 549](https://github.com/Microsoft/sqlopsstudio/issues/549): Silent/Very Silent Install results in application opening after installation.
- Fix [issue 481](https://github.com/Microsoft/sqlopsstudio/issues/481): Add "Check for updates" option.
- SQL Editor colorization and auto-completion fixes:
   - Fix [issue 584](https://github.com/Microsoft/sqlopsstudio/issues/584): Keyword "FULL" not highlighted by IntelliSense.
   - Fix [issue 345](https://github.com/Microsoft/sqlopsstudio/issues/345): Colorize SQL functions within the editor.
   - Fix [issue 300](https://github.com/Microsoft/sqlopsstudio/issues/300): [#tempData] latest "]" will display green color.
   - Fix [issue 225](https://github.com/Microsoft/sqlopsstudio/issues/225): Keyword color mismatch.
   - Fix [issue 60](https://github.com/Microsoft/sqlopsstudio/issues/60): Invalid sql syntax color highlighting when using temporary table in from clause.
- Introduce Connection extensibility API.
- VS Code Editor 1.19 integration.
- Update JustinPealing/html-query-plan component to pick-up several Query Plan viewer improvements.


## January 2018 (January Public Preview)

release date: January 17, 2018  
version: 0.25.4

The *January Public Preview* includes some feature suggestions and high-priority bug fixes. This release includes the following enhancements:

- Saved Server connections are available in the Connection Dialog.
- Enable Hot exit. Hot exit is off by default, to enable see [Hot exit setting](settings.md#hot-exit).
- Tab-coloring based on Server Group. Tab coloring is off by default, to enable see [Tab color setting](settings.md#tab-color).
- Change *Server name* to *Server* in the Connection Dialog.
- Fix broken *Run Current Query* command.
- Fix drag-and-drop breaking scripting bug.
- Fix incorrect pinned Start Menu icon.
- Fix missing Azure Account branding icon.

For more information, see the [Change Log](https://github.com/Microsoft/sqlopsstudio/blob/master/CHANGELOG.md).


## December 2017 (December Public Preview)

release date: December 19, 2017  
version: 0.24.1

The *December Public Preview* includes several bugs fixes across all feature areas, as well as the following enhancements:

- Create Firewall Rule Dialog is now available to assist connecting to Azure SQL Database and Azure SQL Data Warehouse.
- Added Windows Setup, and Linux DEB and RPM installation packages.
- Manage Dashboard visual layout editor.
- *Script As Alter* and *Script As Execute* commands.
- *Run Current Query with Actual Plan* command.
- Integrate VS Code 1.18.1 editor platform.
- Enable Sideloading of VSIX Extension files.
- Support "GO N" batch iteration syntax.


## November 2017

release date: November 15, 2017  
version: 0.23.6

- Initial release of [!INCLUDE[name-sos](../includes/name-sos-short.md)].


## Next Steps

See one of the following quickstarts to get started:
- [Connect & Query SQL Server](quickstart-sql-server.md)
- [Connect & Query Azure SQL Database](quickstart-sql-database.md)
- [Connect & Query Azure Data Warehouse](quickstart-sql-dw.md)

Contribute to [!INCLUDE[name-sos](../includes/name-sos-short.md)]:
- [https://github.com/Microsoft/sqlopsstudio](https://github.com/Microsoft/sqlopsstudio)
