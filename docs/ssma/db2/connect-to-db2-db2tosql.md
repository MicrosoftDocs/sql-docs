---
title: "Connect to Db2 (Db2ToSQL)"
description: Use the Connect to Db2 dialog box to connect to the database that you want to migrate.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.connectdb2mfform.f1"
---
# Connect to Db2 (Db2ToSQL)

Use the **Connect to Db2** dialog box to connect to the Db2 database that you want to migrate.

To access this dialog box, navigate to **File** > **Connect to Db2**. If you previously connected, the command is **Reconnect to Db2**.

## Options

#### Provider

Select the data access provider for your connection to the Db2 database. Available providers are the Db2 Client Provider and the OLE DB Provider. The default is Db2 Client Provider.

#### Mode

Select either *Standard*, *TNSNAME*, or *Connection String* mode.

- In Standard mode, you enter or select values for the provider, server name, server port, Db2 SID, user name, and password.

- In TNSNAME mode, you enter the connect identifier (TNS alias) of the Db2 database, user name, and password.

- In Connection String mode, you provide a connection string.

  > [!IMPORTANT]  
  > We don't recommend that you use Connection String mode because the text might include passwords, and it's sent as clear text.

The default is Standard mode.

#### Server name

Enter the Db2 server name. The default server name is the same as the computer name. Standard mode option.

#### Server port

If you use a port number other than 1521 (default) for connections to Db2, enter the port number. Standard mode option.

#### Connect identifier

Enter the Db2 connect identifier. This value is the alias of the database.

#### Db2 SID

Enter the SID for the database. The SID is an identifier that distinguishes the Db2 database on a computer. The default SID for a database is the first eight characters of the database name.

Standard mode option.

#### User name

Enter the user name that SSMA can use to connect to the Db2 database.

#### Password

Enter the password for the user name.

#### Connection string

> [!IMPORTANT]  
> We don't recommend that you use Connection String mode because the text might include passwords, and it's sent as clear text.

If you use the Connection String mode, enter the full connection string for the connection to Db2.

Connection strings consist of parameter name and value pairs.

- For OLE DB connection string information, see [Microsoft OLE DB Provider for ODBC Overview](../../ado/guide/appendixes/microsoft-ole-db-provider-for-odbc.md).

For SSMA connection strings, always include the Provider parameter. Also, make sure that you include the Port parameter when you connect to Db2.
