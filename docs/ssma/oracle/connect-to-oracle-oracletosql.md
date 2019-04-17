---
title: "Connect To Oracle (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 23a48cb6-ff30-49bb-b4a7-603ebcab336f
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Connect To Oracle (OracleToSQL)
Use the **Connect to Oracle** dialog box to connect to the Oracle database that you want to migrate.  
  
To access this dialog box, on the **File** menu, select **Connect to Oracle**. If you have previously connected, the command is **Reconnect to Oracle**.  
  
## Options  
**Provider**  
Select the data access provider for your connection to the Oracle database. Available providers are the Oracle Client Provider and the OLE DB Provider. The default is Oracle Client Provider.  
  
**Mode**  
Select either Standard, TNSNAME, or Connection String mode.  
  
-   In Standard mode, you enter or select values for the provider, server name, server port, Oracle SID, user name, and password.  
  
-   In TNSNAME mode, you enter the connect identifier (TNS alias) of the Oracle database, user name, and password.  
  
-   In Connection String mode, you provide a connection string.  
  
    > [!IMPORTANT]  
    > We do not recommend that you use Connection String mode because the text might include passwords, and it is sent as clear text.  
  
The default is Standard mode.  
  
**Server name**  
Enter the Oracle server name. The default server name is the same as the computer name. This is a Standard mode option.  
  
**Server port**  
If you are using a port number other than 1521 (default) for connections to Oracle, enter the port number. This is a Standard mode option.  
  
**Connect identifier**  
Enter the Oracle connect identifier. This is the alias of the database as defined in local tnsnames.ora file.  
  
This is a TNSNAME mode option.  
  
**Oracle SID**  
Enter the SID for the database. The SID is an identifier that distinguishes the Oracle database on a computer. The default SID for a database is the first eight characters of the database name.  
  
This is a Standard mode option.  
  
**User name**  
Enter the user name that SSMA will use to connect to the Oracle database.  
  
**Password**  
Enter the password for the user name.  
  
**Connection string**  
> [!IMPORTANT]  
> We do not recommend that you use Connection String mode because the text might include passwords, and it is sent as clear text.  
  
If you use the Connection String mode, enter the full connection string for the connection to Oracle.  
  
Connection strings consist of parameter name and value pairs.  
  
-   For OLE DB connection string information, see [Microsoft OLE DB Provider for Oracle](https://go.microsoft.com/fwlink/?LinkId=85640) article at the MSDN Library.  
  
For SSMA connection strings, always include the Provider parameter. Also, make sure that you include the Port parameter when you connect to Oracle.  
  
