---
title: "Connect to SQL Server (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 30179b25-409b-4e23-bc73-2f226657098f
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Connect to SQL Server (SybaseToSQL)
Use the **Connect to SQL Server** dialog box to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you want to migrate to. To access the **Connect to SQL Server** dialog box, on the **File** menu, click **Connect to SQL Server**.  
  
## Options  
**Server name**  
Enter or select the instance of SQL Server to connect to. By default, the instance that you connected to most recently is displayed.  
  
-   If you are connecting to the default instance on the local computer, you can enter either **localhost** or a dot (**.**).  
  
-   If you are connecting to the default instance on another computer, enter the name of the computer.  
  
-   If you are connecting to a named instance on another computer, enter the computer name, a backslash, and the instance name, such as *MyServer*\\*MyInstance*.  
  
**Server port**  
If your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not configured to accept connections on the default port (1433), enter the port number. Otherwise, leave this value blank.  
  
**Database**  
Specify the database to migrate objects and data to. This option is not available when reconnecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
**Authentication**  
Select the authentication method that is used to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To use your current Windows account, select Windows Authentication. To specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password, select [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
**User name**  
If you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, enter the login for that instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you are using Windows Authentication, this option is not available.  
  
**Password**  
If you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, enter the password for the login on that instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you are using Windows Authentication, this option is not available.  
  
**Encrypt Connection**  
If you want to securely connect to SQL Server, make use of Encrypt connection by checking the **Encrypt connection** checkbox.  
  
**Trust Server Certificate**  
If you want to use this option, select the **Trust Server Certificate** checkbox.  
  
> [!NOTE]  
> To enable **Trust Server Certificate**, "Encrypt" must be set to **True**.  
  
