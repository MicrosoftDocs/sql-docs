---
title: "SQL Server Migration Assistant"
description: Learn about SQL Server Migration Assistant, a tool that automates database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and SAP ASE.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.custom:
  - sql-migration-content
---
# SQL Server Migration Assistant

Microsoft SQL Server Migration Assistant (SSMA) is a tool designed to automate database migration to SQL Server, Azure SQL Database, Azure SQL Managed Instance and Azure Synapse Analytics from Microsoft Access, DB2, MySQL, Oracle, and SAP ASE.

## Migration sources

- [SQL Server Migration Assistant for Access](access/sql-server-migration-assistant-for-access-accesstosql.md)
- [SQL Server Migration Assistant for DB2](db2/sql-server-migration-assistant-for-db2-db2tosql.md)
- [SQL Server Migration Assistant for MySQL](mysql/sql-server-migration-assistant-for-mysql-mysqltosql.md)
- [SQL Server Migration Assistant for Oracle](oracle/sql-server-migration-assistant-for-oracle-oracletosql.md)
- [SQL Server Migration Assistant for SAP ASE](sybase/sql-server-migration-assistant-for-sybase-sybasetosql.md)

## Supported sources and target versions

For supported sources, review the information on the Download Center for the SSMA download.

The following target versions are supported for SSMA.

- [!INCLUDE [sssql11-md](../includes/sssql11-md.md)]
- [!INCLUDE [sssql14-md](../includes/sssql14-md.md)]
- [!INCLUDE [sssql16-md](../includes/sssql16-md.md)]
- [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] on Windows and Linux
- [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] on Windows and Linux
- [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Windows and Linux
- [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)]
- [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)]
- [!INCLUDE [ssazuresynapse-md](../includes/ssazuresynapse-md.md)] <sup>1</sup>

<sup>1</sup> This target is supported only by SSMA for Oracle.

## Downloads

- [SSMA for Access](https://aka.ms/ssmaforaccess)
- [SSMA for DB2](https://aka.ms/ssmafordb2)
- [SSMA for MySQL](https://aka.ms/ssmaformysql)
- [SSMA for Oracle](https://aka.ms/ssmafororacle)
- [SSMA for SAP ASE](https://aka.ms/ssmaforsybase)

## Get SSMA support

**Help and support for Microsoft SQL Server Migration Assistant (SSMA):**

- **Product help**: To access product support, launch SSMA, and select the Help menu or press the F1 key.

- **SQL Server community forums**: Ask a question in the SQL Server Community

  - **[SQL Server Community](../sql-server/index.yml)**: Newsgroups and forums that are monitored by the SQL Server community. This site also lists community information sources, such as blogs and Web sites.

  - **[SQL Server Developer Center Community](../sql-server/index.yml)**: Newsgroups, forums, and other community resources that are useful to SQL Server developers

- **Consulting services**: For partner assisted migrations, go the [Azure Database Migration Guide](/data-migration/).

## Legal notice (SSMA)

This documentation, including sample applications herein, is provided for information purposes only, and this documentation is provided without any warranties, either express or implied. Information in this documentation, including URL and other Internet Web site references, is subject to change without notice. The entire risk of the use or the results of the use of this documentation remains with the user.

The primary purpose of a sample contained within this documentation is to illustrate a concept, or a reasonable use of a particular statement or clause. Most samples don't include all of the code that may normally be found in a full production system, as some of the usual data validation and error handling is removed to focus the sample on a particular concept or statement. Technical support isn't available for these samples or for any included source code.

Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, person, place, or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this documentation may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this documentation. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this documentation doesn't give you any license to these patents, trademarks, copyrights, or other intellectual property.

&copy; 2019 Microsoft Corporation. All rights reserved.

Microsoft, Windows, Windows NT, Windows Server, Active Directory, ActiveX, BackOffice, bCentral, BizTalk, DirectX, Excel, Hotmail, IntelliSense, J/Direct, JScript, Microsoft Press, MSDN, MS-DOS, Outlook, PivotChart, PivotTable, PowerPoint, SharePoint, SQL Server, Visual Basic, Visual C#, Visual C++, Visual FoxPro, Visual InterDev, Visual J#, Visual J++, Visual SourceSafe, Visual Studio, Win32, Win32s, Windows Mobile, Windows Server System, and WinFX are either registered trademarks or trademarks of Microsoft Corporation in the United States and/or other countries/regions.

SAP NetWeaver is the registered trademark of SAP AG in Germany and in several other countries/regions.

All other trademarks are property of their respective owners.

## Documentation policy for SQL Server support and upgrade

Content that appears in SQL Server documentation is published only after it has been tested sufficiently. Product documentation - SQL Docs, readme files, known issues documents, and Knowledge Base articles - contains content regarding SQL Server features and functionality that is robust enough to be safe for general use by all customers. This policy applies to all SQL Server documentation, including readme files for releases and services packs; a readme file is considered an extension of Books Online.

In some cases, a particular feature isn't something that customers should use directly and, therefore, it isn't documented. Unless a feature is also discussed in SQL Server documentation published by Microsoft, content from third-party books or Web sites isn't supported by Microsoft customer support, and shouldn't be used in production databases or applications.

Customers must not use undocumented APIs, including but not limited to: stored procedures, extended stored procedures, functions, views, tables, columns, properties, or metadata. Microsoft customer support doesn't support databases or applications that use or use undocumented entry points.

Server and database upgrades to future versions of SQL Server aren't guaranteed for applications and databases that use and use undocumented entry points. Use of SQL Server features and functionality must be limited to those that are included in Microsoft SQL Server documentation. If a feature isn't documented in Microsoft SQL Server documentation, it isn't a supported part of SQL Server.
