---
description: "Connect To DB2 (DB2ToSQL)"
title: "Connect To DB2 (DB2ToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 9d485fd0-ab5d-402a-a59a-e9982a61b7de
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.db2.connectdb2mfform.f1"

---
# Connect To DB2 (DB2ToSQL)
Use the **Connect to DB2** dialog box to connect to the DB2 database that you want to migrate.  
  
To access this dialog box, on the **File** menu, select **Connect to DB2**. If you have previously connected, the command is **Reconnect to DB2**.  
  
## Options  
**Provider**  
Select the data access provider for your connection to the DB2 database. Available providers are the DB2 Client Provider and the OLE DB Provider. The default is DB2 Client Provider.  
  
**Mode**  
Select either Standard, TNSNAME, or Connection String mode.  
  
-   In Standard mode, you enter or select values for the provider, server name, server port, DB2 SID, user name, and password.  
  
-   In TNSNAME mode, you enter the connect identifier (TNS alias) of the DB2 database, user name, and password.  
  
-   In Connection String mode, you provide a connection string.  
  
    > [!IMPORTANT]  
    > We do not recommend that you use Connection String mode because the text might include passwords, and it is sent as clear text.  
  
The default is Standard mode.  
  
**Server name**  
Enter the DB2 server name. The default server name is the same as the computer name. This is a Standard mode option.  
  
**Server port**  
If you are using a port number other than 1521 (default) for connections to DB2, enter the port number. This is a Standard mode option.  
  
**Connect identifier**  
Enter the DB2 connect identifier. This is the alias of the database as defined in local tnsnames.ora file.  
  
This is a TNSNAME mode option.  
  
**DB2 SID**  
Enter the SID for the database. The SID is an identifier that distinguishes the DB2 database on a computer. The default SID for a database is the first eight characters of the database name.  
  
This is a Standard mode option.  
  
**User name**  
Enter the user name that SSMA will use to connect to the DB2 database.  
  
**Password**  
Enter the password for the user name.  
  
**Connection string**  
> [!IMPORTANT]  
> We do not recommend that you use Connection String mode because the text might include passwords, and it is sent as clear text.  
  
If you use the Connection String mode, enter the full connection string for the connection to DB2.  
  
Connection strings consist of parameter name and value pairs.  
  
-   For OLE DB connection string information, see [Microsoft OLE DB Provider for DB2](../../ado/guide/appendixes/microsoft-ole-db-provider-for-oracle.md) article at the MSDN Library.  
  
For SSMA connection strings, always include the Provider parameter. Also, make sure that you include the Port parameter when you connect to DB2.  
