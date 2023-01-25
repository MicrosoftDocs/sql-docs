---
title: "Access Data for the DQS Operations"
description: "Access Data for the DQS Operations"
author: swinarko
ms.author: sawinark
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
---
# Access Data for the DQS Operations

[!INCLUDE [SQL Server - Windows only ](../../includes/applies-to-version/sql-windows-only.md)]

  To use your source data for [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) operations, and export your processed data, you can do either of the following:  
  
-   Copy your source data to a table/view in the DQS_STAGING_DATA database, and then use it for DQS operations. You can also export the processed data to a new table in the DQS_STAGING_DATA database. To do so, your Windows user account must be granted read/write access to the DQS_STAGING_DATA database.  
  
-   Use your own database as the source data for the DQS operations, and destination for exporting the processed data. To do so, ensure that your database is in the same SQL Server instance as the Data Quality Server databases. Otherwise, the database will not be available in the Data Quality Client for DQS operations. Also, your Windows user account must be granted access on the DQS_STAGING_DATA database for exporting the matching results because matching results are exported in two phases: first, the matching results are exported to the temporary tables in the DQS_STAGING_DATA database, and then moved to the table in your destination database.  
  
## Prerequisites  
  
-   You must have completed the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation by running the DQSInstaller.exe file. For more information, see [Run DQSInstaller.exe to Complete Data Quality Server Installation](../../data-quality-services/install-windows/run-dqsinstaller-exe-to-complete-data-quality-server-installation.md).  
  
-   Your Windows user account must be a member of the appropriate fixed server role (such as securityadmin, serveradmin, or sysadmin) in the database engine instance to grant/modify access to SQL login on databases.  
  
### To grant read/write access to a User on the DQS_STAGING_DATA Database  
  
1.  Start Microsoft SQL Server Management Studio.  
  
2.  In Microsoft SQL Server Management Studio, expand your SQL Server instance, and expand **Security**, and then expand **Logins**.  
  
3.  Right-click a SQL login, and click **Properties**.  
  
4.  In the **Login Properties** dialog box, click the **User Mapping** page in the left pane.  
  
5.  In the right pane, select the check box under the **Map** column for the **DQS_STAGING_DATA** database, and then select the following roles in the **Database role membership for: DQS_STAGING_DATA** pane:  
  
    -   **db_datareader**: Read data from tables/views.  
  
    -   **db_datawriter**: Add, delete, or change data in tables.  
  
    -   **db_ddladmin**: Create, modify, or delete tables/views.  
  
6.  In the **Login Properties** dialog box, click **OK** to apply the changes.  
  
## Next Steps  
 Try performing DQS operations that accesses the database as data source for DQS operation, and then exports the processed data to the database.  
  
## See Also  
 [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md)  
  
  
