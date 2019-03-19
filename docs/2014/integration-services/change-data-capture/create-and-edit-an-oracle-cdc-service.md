---
title: "Create and Edit an Oracle CDC Service | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "createSrv"
ms.assetid: 10cd612e-d8f1-4af2-97d3-a0c22e1e2326
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create and Edit an Oracle CDC Service
  You create and edit a new Oracle CDC Windows Service from the CDC Service Configuration Console.  
  
 To create a new Oracle CDC Windows service, select **Local CDC Services** from the left pane, then click **New Service** from the **Actions** pane. You can also right-click **Local CDC Services** and select **New Service**. The New Oracle CDC Windows Service dialog box opens.  
  
 **OR**  
  
 To edit the CDC service properties, select the service that you want to edit the properties for and click **Properties** from the **Actions** pane. You can also right-click the service you are working with and select **Properties**. The CDC Service Properties dialog box opens.  
  
 Enter the following information in the New Oracle CDC Windows Service dialog box or the CDC Service Properties dialog box.  
  
 Service Name  
 Type the name of the new Oracle CDC Windows Service. You should not use long names, if possible. The characters / and \ cannot be used in the service name.  
  
> [!NOTE]  
> This option is not available when editing the service. You cannot change the name of a Windows service that already exists.  
  
 **Description**  
 Type a description of the service to help identify it.  
  
 **Service Account**  
 Select one of the following to determine under which account to run the service:  
  
-   **Local System Account**  
  
     This is not recommended because it gives too many permissions to the service.  
  
-   **This Account**  
  
     On Windows Vista or Windows Server 2008, the default service account is the NETWORK SERVICE account.  
  
     On Windows 7, Windows Server 2008 R2 and later, the default service account is NT Service\\<service-name>.  
  
     Using these accounts lets you work without using passwords because a password is not necessary for these accounts. In addition these accounts provide only the necessary permissions required for the Oracle CDC Service to run.  
  
     You can use a local or domain Windows account for the service account. In this case, you must enter the **Password** for that account. This account can be for the local host or a domain account. Be sure to update the password when it is changed using Local Services in the Windows Control Panel.  
  
 **Server name**: Select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to connect to (for example, **\\\\<computer_name>\\<instance_name>**). The server instance last connected to is displayed by default.  
  
 **Authentication**  
 Select one of the following:  
  
-   **Windows Authentication**: If you select this option, the Oracle CDC service connects to the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance using the service account identity. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is running on a different computer, Windows Authentication must be used with domain accounts.  
  
-   **SQL Server Authentication**: If you select this option, you must type the **User Name** and **Password** for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login you want to use. The Oracle CDC service uses these credentials when connecting to the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login used by the Oracle CDC Service only needs to be a member of the public fixed-server role, no other privileges are needed. Once new Oracle CDC Instances are added, that login will gain **db_owner** access to the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC databases.  
  
 To create the Oracle CDC Windows Service definition, the program needs update access to the MSXDBCDC database in the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you click **OK**, a dialog box prompts the user to enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login with an update access to the MSXDBCDC database.  
  
 For information about the data you must type into the Connect to SQL Server dialog box, see [Connection to SQL Server](connection-to-sql-server.md).  
  
 **Options**  
 Click the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:  
  
-   **Connection Timeout**: Type the time (in seconds) that the CDC Service for Oracle waits for a connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before timing out. The default value is **15**.  
  
-   **Execution Timeout**: Type the time (in seconds) that the Oracle CDC Windows Service waits for a command to execute before timing out. The default value is **30**.  
  
-   **Encrypt Connection**: Select **Encrypt Connection** for communication between the Oracle CDC Service and the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance using an encrypted connection.  
  
-   **Advanced**: Type any additional connection properties, if necessary.  
  
 **Master Password**  
 Enter a password to be used by the Oracle CDC Windows Service to protect the Oracle log-mining credentials.  
  
 The same master password must also be used when other instances of the same service are configured on other nodes on a cluster in high-availability configuration. If you lose or modify the master password, all log mining passwords stored in Oracle CDC Instance databases must be re-entered using the CDC Designer console.  
  
## See Also  
 [How to Create and Edit a CDC Service](how-to-create-and-edit-a-cdc-service.md)  
  
  
