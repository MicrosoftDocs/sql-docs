---
title: "Create the RSExecRole"
description: "Create the RSExecRole"
author: maggiesMSFT
ms.author: maggies
ms.date: 06/12/2019
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "RSExecRole"
---

# Create the RSExecRole

  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses a predefined database role called **RSExecRole** to grant report server permissions to the report server database. The **RSExecRole** role is created automatically with the report server database. As a rule, you should never modify it or assign other users to the role. However, when you move a report server database to a new or different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)], you must re-create the role in the Master and MSDB system databases.  
  
 Use the following instructions to perform the following steps:  
  
-   Create and provision the **RSExecRole** in the Master system database.  
  
-   Create and provision the **RSExecRole** in the MSDB system database.  
  
> [!NOTE]  
> The instructions in this topic are intended for users who do not want to run a script or write WMI code to provision the report server database. If you manage a large deployment and will be moving databases routinely, you should write a script to automate these steps. For more information, see [Access the Reporting Services WMI Provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md).  
  
## Before you start  
  
-   Back up the encryption keys so that you can restore them after the database is moved. This step doesn't directly affect your ability to create and provision the **RSExecRole**, but you must have a backup of the keys in order to verify your work. For more information, see [Back Up and Restore Reporting Services Encryption Keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).  
  
-   Verify you're logged on as a user account that has **sysadmin** permissions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   Verify [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is installed and running on the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that you plan to use.  
  
-   Attach the ReportServerTempDB and ReportServer databases. You aren't required to attach the databases to create the actual role, but they must be attached before you can test your work.  
  
 The instructions for manually creating the **RSExecRole** are intended to be used within the context of migrating a report server installation. Important tasks such as backing up and moving the report server database aren't addressed in this article, but are documented in the Database Engine documentation.  
  
## Create RSExecRole in master  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses extended stored procedures for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to support scheduled operations. The following steps explain how to grant Execute permissions for the procedures to the **RSExecRole** role.  
  
### Create RSExecRole in the master system database by using Management Studio  
  
1.  Start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that hosts the report server database.  
  
2.  Open **Databases**.  
  
3.  Open **System Databases**.  
  
4.  Open **Master**.  
  
5.  Open **Security**.  
  
6.  Open **Roles**.  
  
7.  Right-click **Database Roles**, and select **New Database Role**. The **Database Role - New** page appears.  
  
8.  In **Role name**, enter **RSExecRole**.  
  
9. In **Owner**, enter **dbo**.  
  
10. Select page **Securables**.  
  
11. Select **Search**. The **Add Objects** dialog box appears. The **Specific Objects** option is selected by default.  
  
12. Select **OK**. The **Select Objects** dialog box appears.  
  
13. Select **Object Types**.  
  
14. Select **Extended Stored Procedures**.  
  
15. Select **OK**.  
  
16. Select **Browse**.  
  
17. Scroll down the list and select the following procedures:  
  
    1.  xp_sqlagent_enum_jobs  
  
    2.  xp_sqlagent_is_starting  
  
    3.  xp_sqlagent_notify  
  
18. Select **OK**, and the select **OK** again.  
  
19. In the **Execute** row, in the **Grant** column, select the check box.  
  
20. Repeat for each of the remaining stored procedures. **RSExecRole** must be granted Execute permissions for all three stored procedures.  

21. Select **OK** to finish.  
  
:::image type="content" source="../../reporting-services/security/media/rsexecroledbproperties.gif" alt-text="Screenshot that shows the Database Role Properties page.":::
  
## Create RSExecRole in MSDB  
 Reporting Services uses stored procedures for SQL Server Agent service and retrieves job information from system tables to support scheduled operations. The following steps explain how to grant Execute permissions for the procedures and Select permissions on the tables to the RSExecRole.  
  
### Create RSExecRole in the MSDB system database  
  
1.  Repeat similar steps for granting permissions to stored procedures and tables in MSDB. To simplify the steps, you provision the stored procedures and tables separately.  
  
2.  Open **MSDB**.  
  
3.  Open **Security**.  
  
4.  Open **Roles**.  
  
5.  Right-click **Database Roles**, and select **New Database Role**. The General page appears.  
  
6.  In Role name, enter **RSExecRole**.  
  
7.  In Owner, enter **dbo**.  
  
8.  Select the **Securables** page.  
  
9.  Select **Search**. The **Add Objects** dialog box appears. The **Specify Objects** option is selected by default.  
  
10. Select **OK**.  
  
11. Select **Object Types**.  
  
12. Select **Stored Procedures.**  
  
13. Select **OK**.  
  
14. Select **Browse**.  
  
15. Scroll down the list of items and select the following stored procedures:  
  
    1.  sp_add_category  
  
    2.  sp_add_job  
  
    3.  sp_add_jobschedule  
  
    4.  sp_add_jobserver  
  
    5.  sp_add_jobstep  
  
    6.  sp_delete_job  
  
    7.  sp_help_category  
  
    8.  sp_help_job  
  
    9. sp_help_jobschedule  
  
    10. sp_verify_job_identifiers  
  
16. Select **OK**, and then choose **OK** again.  
  
17. Select the first stored procedure: sp_add_category.  
  
18. In the **Execute** row, in the **Grant** column, select the checkbox.  
  
19. Repeat for each of the remaining stored procedures. RSExecRole must be granted Execute permissions for all 10 stored procedures.  
  
20. Still on the **Securables** page, select **Search** again. The **Add Objects** dialog box appears. The **Specify Objects** option is selected by default.  
  
21. Select **OK**.  
  
22. Select **Object Types**.  
  
23. Select **Tables.**  
  
24. Select **OK**.  
  
25. Select **Browse**.  
  
26. Scroll down the list of items and select the following tables:  
  
    1.  syscategories  
  
    2.  sysjobs  
  
27. Select **OK**, and the select **OK** again.  
  
28. Select the first table: syscategories.  
  
29. In the **Select** row, in the **Grant** column, select the checkbox.  
  
30. Repeat for the sysjobs table. RSExecRole must be granted Select permissions for both tables.  
  
31. Select **OK** to finish.  
  
## Move the report server database  
 After you create the roles, you can move the report server database to new SQL Server instance. For more information, see [Move the report server databases to another computer](../../reporting-services/report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md).  
  
 If you're upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to SQL Server 2016 or later, you can upgrade it either before or after moving the database.  
  
 The report server database is upgraded automatically when the report server connects to it. There are no specific steps required for upgrading the database.  
  
## Restore encryption keys and verify your work  
 If you attached the report server databases, you should now be able to complete the following steps to verify your work.  
  
### Verify report server operability after a database move  
  
1.  Start the Reporting Services Configuration tool and connect to the report server.  
  
2.  Select **Database**.  
  
3.  Select **Change Database**.  
  
4.  Select **Choose an existing report server database**.  
  
5.  Enter the server name of the Database Engine. If you attached the report server databases to a named instance, you must enter the instance name in this format: \<servername>\\<instancename\>.  
  
6.  Select **Test Connection**. You should see a dialog box that states, "Test Connection Succeeded."
  
7.  Select **Ok** to close the dialog box and then select **Next**.  
  
8.  On the Database, select the report server database.  
  
9.  Select **Next** and complete the wizard.  
  
10. Select **Encryption Keys**.  
  
11. Select **Restore**.  
  
12. Select the strong file (.snk) that has the backup copy of the symmetric key used to decrypt stored credentials and connection information in the report server database.  
  
13. Enter the password and select **OK**.  
  
14. Select **Web Portal URL**.  
  
15. Select the link to open the web portal. You should see the report server items from the report server database.  

## Create the RSExecRole role and permissions by using T-SQL
The role can also be created, and applicable permissions granted, on the system databases by using the following T-SQL script:
```sql
USE master;
GO
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE [type] = 'R' AND [name] = 'RSExecRole') BEGIN
    CREATE ROLE [RSExecRole];
END
GRANT EXECUTE ON dbo.xp_sqlagent_enum_jobs TO [RSExecRole];
GRANT EXECUTE ON dbo.xp_sqlagent_is_starting TO [RSExecRole];
GRANT EXECUTE ON dbo.xp_sqlagent_notify TO [RSExecRole];
GO
USE msdb;
GO
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE [type] = 'R' AND [name] = 'RSExecRole') BEGIN
    CREATE ROLE [RSExecRole];
END
GRANT EXECUTE ON dbo.sp_add_category TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_add_job TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_add_jobschedule TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_add_jobserver TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_add_jobstep TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_delete_job TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_help_category TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_help_job TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_help_jobschedule TO [RSExecRole];
GRANT EXECUTE ON dbo.sp_verify_job_identifiers TO [RSExecRole];
GRANT SELECT ON dbo.syscategories TO [RSExecRole];
GRANT SELECT ON dbo.sysjobs TO [RSExecRole];
GO
```

## Related content

[Move the report server databases to another computer &#40;SSRS native mode&#41;](../../reporting-services/report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md)   
[Create a native mode report server database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md)   
[Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
[Back up and restore Reporting Services encryption keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
