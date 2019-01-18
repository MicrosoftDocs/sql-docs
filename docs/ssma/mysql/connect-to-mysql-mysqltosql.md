---
title: "Connect to MySQL (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 94099d01-ab19-4d58-a172-340c86b4a0f3
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connect to MySQL (MySQLToSQL)
Use the **Connect to MySQL** dialog box to connect to the MySQL database that you want to migrate.  
  
To access this dialog box, on the **File** menu, select **Connect to MySQL**. If you have previously connected, the command is **Reconnect to MySQL**.  
  
## Options  
**Provider**  
  
Available MySQL provider is MySQL ODBC 5.1 Driver (trusted).  
  
**Mode**  
  
The default mode is Standard mode. In Standard mode you enter or select values for the MySQL, server name, server port, user name, and password.  
  
**Server name**  
  
Enter the MySQL server name. This is a Standard mode option.  
  
**Server port**  
  
Enter the Server port. The default server port is 3306. This is a Standard mode option.  
  
**User name**  
  
Enter the user name that SSMA will use to connect to the MySQL database.  
  
**Password**  
  
Enter the password for the user name.  
  
**SSL**  
  
If you want to securely connect to MySQL, make use of Secure Socket Layer (SSL) by checking the **SSL** checkbox.  
  
**Configure**  
  
It provides an option to configure the connection to MySQL through Secure Socket Layer (SSL).  
  
> [!NOTE]  
> To enable **Configure**, SSL must be set to **True**.  
  
On clicking the button "Configure", a dialog-box appears. To use encryption while connecting to MySQL Database, path to the following three certificate files present in the dialog-box must be defined [Privacy Enhanced Mail Certificates (PEM)]:  
  
-   **SSL Certificate Authority:** Specifies the path to a file with a list of trust SSL CAs'.  
  
-   **SSL Certificate:** Specifies the name of the SSL certificate file to use for establishing a secure connection.  
  
-   **SSL KEY:** Specifies the name of the SSL key file to use for establishing a secure connection.  
  
> [!NOTE]  
> -   The **OK** button is enabled when the required information has been provided. If any of the file paths are invalid, the "OK" button will remain disabled.  
> -   The **Cancel** button closes the dialog box and **turns off** the SSL option from the main Connection Form.  
  
