---
title: "SQL Server Migration Assistant | Microsoft Docs"
description: Learn about SQL Server Migration Assistant, a tool that automates database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and SAP ASE.
ms.custom: ""
ms.date: "10/10/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: d0233525-a83b-4279-813e-c554042abd0e
author: cpichuka 
ms.author: cpichuka 
---
# SQL Server Migration Assistant

Microsoft SQL Server Migration Assistant (SSMA) is a tool designed to automate database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and SAP ASE.  
  
## Migration Sources  
  
- [SQL Server Migration Assistant for Access](../ssma/access/sql-server-migration-assistant-for-access-accesstosql.md)  
  
- [SQL Server Migration Assistant for DB2](../ssma/db2/sql-server-migration-assistant-for-db2-db2tosql.md)  
  
- [SQL Server Migration Assistant for MySQL](../ssma/mysql/sql-server-migration-assistant-for-mysql-mysqltosql.md)  
  
- [SQL Server Migration Assistant for Oracle](../ssma/oracle/sql-server-migration-assistant-for-oracle-oracletosql.md)  
  
- [SQL Server Migration Assistant for SAP ASE](../ssma/sybase/sql-server-migration-assistant-for-sybase-sybasetosql.md)  

## Supported Sources and Target Versions

For supported sources, review the information on the Download Center for the SSMA download.

The following target versions are supported for SSMA.

- SQL Server 2012
- SQL Server 2014
- SQL Server 2016
- SQL Server 2017 on Windows and Linux
- SQL Server 2019 on Windows and Linux
- Azure SQL Database
- Azure SQL Managed Instance
- Azure Synapse Analytics**

** This target is supported only by SSMA for Oracle.

## Downloads

- [SSMA for Access](https://aka.ms/ssmaforaccess)
- [SSMA for DB2](https://aka.ms/ssmafordb2)
- [SSMA for MySql](https://aka.ms/ssmaformysql)
- [SSMA for Oracle](https://aka.ms/ssmafororacle)
- [SSMA for SAP ASE](https://aka.ms/ssmaforsybase)
 
## Getting SSMA Support  

**Help and support for Microsoft SQL Server Migration Assistant (SSMA):**  
  
- **Product help** - To access product support, launch SSMA, and select the Help menu or press the F1 key.  
  
- **SQL Server community forums** - Ask a question in the SQL Server Community  
  
  - [SQL Server Community](../sql-server/index.yml) -   Newsgroups and forums that are monitored by the SQL Server community. This site also lists community information sources, such as blogs and Web sites.  
  
  - [SQL Server Developer Center Community](../sql-server/index.yml) -  Newsgroups, forums, and other community resources that are useful to SQL Server developers  
    
- **Premier support** - If you have a Premier Contract, you can get Premier support on the [Premier Online portal](https://premier.microsoft.com/).  
  
- **Consulting services** - For partner assisted migrations, go the [Azure Database Migration Guide](https://datamigration.microsoft.com/).
  
## Legal Notice (SSMA)

This documentation, including sample applications herein, is provided for information purposes only, and this documentation is provided without any warranties, either express or implied. Information in this documentation, including URL and other Internet Web site references, is subject to change without notice. The entire risk of the use or the results of the use of this documentation remains with the user.  
  
The primary purpose of a sample contained within this documentation is to illustrate a concept, or a reasonable use of a particular statement or clause. Most samples don't include all of the code that may normally be found in a full production system, as some of the usual data validation and error handling is removed to focus the sample on a particular concept or statement. Technical support isn't available for these samples or for any included source code.  
  
Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, person, place, or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this documentation may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.  
  
Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this documentation. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this documentation does not give you any license to these patents, trademarks, copyrights, or other intellectual property.  
  
© 2019 Microsoft Corporation. All rights reserved.  
  
Microsoft, Windows, Windows NT, Windows Server, Active Directory, ActiveX, BackOffice, bCentral, BizTalk, DirectX, Excel, Hotmail, IntelliSense, J/Direct, Jscript, Microsoft Press, MSDN, MS-DOS, Outlook, PivotChart, PivotTable, PowerPoint, SharePoint, SQL Server, Visual Basic, Visual C#, Visual C++, Visual FoxPro, Visual InterDev, Visual J#, Visual J++, Visual SourceSafe, Visual Studio, Win32, Win32s, Windows Mobile, Windows Server System, and WinFX are either registered trademarks or trademarks of Microsoft Corporation in the United States and/or other countries/regions.  
  
SAP NetWeaver is the registered trademark of SAP AG in Germany and in several other countries/regions.  
  
All other trademarks are property of their respective owners.  
  
## Documentation Policy for SQL Server Support and Upgrade

Content that appears in SQL Server documentation is published only after it has been tested sufficiently. Product documentation - SQL Server Books Online, readme files, known issues documents, and Knowledge Base articles - contains content regarding SQL Server features and functionality that is robust enough to be safe for general use by all customers. This policy applies to all SQL Server documentation, including readme files for releases and services packs; a readme file is considered an extension of Books Online.  
  
In some cases, a particular feature is not something that customers should use directly and, therefore, it is not documented. Unless a feature is also discussed in SQL Server documentation published by Microsoft, content from third-party books or Web sites is not supported by Microsoft customer support, and should not be used in production databases or applications.  
  
Customers must not use undocumented APIs, including but not limited to: stored procedures, extended stored procedures, functions, views, tables, columns, properties, or metadata. Microsoft customer support does not support databases or applications that leverage or use undocumented entry points.  
  
Server and database upgrades to future versions of SQL Server are not guaranteed for applications and databases that leverage and use undocumented entry points. Use of SQL Server features and functionality must be limited to those that are included in Microsoft SQL Server documentation. If a feature is not documented in Microsoft SQL Server documentation, it is not a supported part of SQL Server.
