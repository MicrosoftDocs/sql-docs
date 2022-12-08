---
title: "Connecting to SAP ASE (SybaseToSQL) | Microsoft Docs"
description: Learn how to connect to an Adaptive Server to migrate a SAP Adaptive Server Enterprise (ASE) database to SQL Server or Azure SQL Database.
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Connecting to Sybase"
ms.assetid: a45a2330-9175-4c9e-af38-ef920e350614
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.sybase.connectsybaseform.f1"
---

# Connecting to SAP ASE (SybaseToSQL)

To migrate SAP Adaptive Server Enterprise (ASE) databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you must connect to the Adaptive Server that contains the databases that you want to migrate. When you connect, SSMA obtains metadata about all databases on the Adaptive Server and displays database metadata in the Sybase Metadata Explorer pane. SSMA stores information about the database server, but does not store passwords.  
  
Your connection to ASE stays active until you close the project. When you reopen the project, you must reconnect to ASE if you want an active connection to the server.  
  
Metadata about the Adaptive Server is not automatically updated. Instead, if you want to update the metadata in Sybase Metadata Explorer, you must manually update the metadata, as described in the "Refreshing Sybase ASE Metadata" section later in this topic.  
  
## Required ASE Permissions

The account that is used to connect to ASE must have at least **public** access to the master database and to any source databases to be migrated to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. In addition, to select permissions on tables that are being migrated, the user must have SELECT permissions on the following system tables:  
  
- [source_db].dbo.sysobjects  
- [source_db].dbo.syscolumns  
- [source_db].dbo.sysusers  
- [source_db].dbo.systypes  
- [source_db].dbo.sysconstraints  
- [source_db].dbo.syscomments  
- [source_db].dbo.sysindexes  
- [source_db].dbo.sysreferences  
- master.dbo.sysdatabases  
  
## Establishing a Connection to ASE

When you connect to an Adaptive Server, SSMA reads the database metadata on the database server, and then adds this metadata to the project file. This metadata  is used by SSMA when it converts the objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure syntax, and when it migrates data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can browse this metadata in the Sybase Metadata Explorer pane and review properties of individual database objects.  
  
> [!IMPORTANT]  
> Before you try to connect to the database server, make sure that the database server is running and can accept connections.  
  
**To connect to Sybase ASE**
  
1. On the **File** menu, select **Connect to Sybase**.  
  
   If you previously connected to Sybase, the command name will be **Reconnect to Sybase**.  
  
2. In the **Provider** box, select any of the installed providers on the machine to connect to Sybase server.  
  
3. In the **Mode** box, select either **Standard mode** or **Advanced mode**.  
  
   Use standard mode to specify the server name, port, user name, and password. Use advanced mode to provide a connection string. This mode is usually only used for troubleshooting or working with technical support.  
  
4. If you select **Standard mode**, provide the following values:  
  
    1. In the **Server name** box, enter or select the name or IP address of the database server.  
    2. If the database server is not configured to accept connections on the default port (5000), enter the port number that is used for Sybase connections in the **Server port** box.  
    3. In the **User name** box, enter a Sybase account that has the necessary permissions.  
    4. In the **Password** box, enter the password for the specified user name.  
  
5. If you select **Advanced mode**, provide a connection string in the **Connection string** box.  
  
    Examples of different connection strings are as follows:  
  
    1. **Connection strings for Sybase OLE DB Provider:**  
  
        For Sybase ASE OLE DB 12.5, an example connection string is as follows:  
  
        `Server Name=sybserver;User ID=MyUserID;Password=MyP@$$word;Provider=Sybase.ASEOLEDBProvider;`  
  
        For Sybase ASE OLE DB 15, an example connection string is as follows:  
  
        `Server=sybserver;User ID=MyUserID;Password=MyP@$$word;Provider= ASEOLEDB;Port=5000;`  
  
    2. **Connection string for Sybase ODBC Provider:**  
  
       `Driver=Adaptive Server Enterprise;Server=sybserver;uid=MyUserID;pwd=MyP@$$word;Port=5000;`  
  
    3. **Connection string for Sybase ADO.NET Provider:**  
  
       `Server=sybserver;Port=5000;uid=MyUserID;pwd=MyP@$$word;`  
  
    For more information, see [Connect to Sybase &#40;SybaseToSQL&#41;](../../ssma/sybase/connect-to-sybase-sybasetosql.md).  
  
## Reconnecting to Sybase ASE

Your connection to the database server stays active until you close the project. When you reopen the project, you must reconnect if you want an active connection to the Adaptive Server. You can work offline until you want to update metadata, load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, and migrate data.  
  
## Refreshing Sybase ASE Metadata

Metadata about the ASE databases is not automatically refreshed. The metadata in Sybase Metadata Explorer is a snapshot of the metadata when you first connected to the Adaptive Server, or the last time that you manually refreshed metadata. You can manually update metadata for a single database, a single database schema, or all databases.  
  
**To refresh metadata**
  
1. Make sure that you are connected to the Adaptive Server.  
  
2. In Sybase Metadata Explorer, select the check box next to the database or database schema that you want to update.  
  
3. Right-click Databases or the individual database or database schema, and then select **Refresh from Database**.  
  
4. If you are asked to check the current object, click **Yes**.  
  
## Next Step  
  
- The next step in the migration process is to [Connect to an instance of SQL Server](connecting-to-sql-server-sybasetosql.md) / [Connecting to an instance of SQL Azure](connecting-to-azure-sql-db-sybasetosql.md)  
  
## See Also

[Migrating Sybase ASE Databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
