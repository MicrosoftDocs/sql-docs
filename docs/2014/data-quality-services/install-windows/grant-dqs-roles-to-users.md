---
title: "Grant DQS Roles to Users | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: afb445b5-bdbe-4bfe-844f-344766cdc2b2
author: leolimsft
ms.author: lle
manager: craigg
---
# Grant DQS Roles to Users
  This topic describes how to create SQL logins based on a Windows principal, and grant [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) roles on the DQS_MAIN database.  
  
## Prerequisites  
  
-   You must have completed the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation by running the DQSInstaller.exe file. For more information, see [Run DQSInstaller.exe to Complete Data Quality Server Installation](run-dqsinstaller-exe-to-complete-data-quality-server-installation.md).  
  
-   Your Windows user account must be a member of the appropriate fixed server role (such as securityadmin, serveradmin, or sysadmin) to create SQL login, and grant them DQS roles.  
  
### To create SQL login and grant DQS roles  
  
1.  Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
2.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand your SQL Server instance, and then expand **Security**.  
  
3.  Right-click the **Security** folder, point to **New**, and then click **Login**.  
  
4.  In the **Login - New** dialog box, specify the name of a Windows user in the **Login name** box, specify the type of authentication as **Windows authentication**, and click **Search** to validate the user.  
  
5.  After the user is validated, click the **User Mapping** page in the left pane.  
  
6.  In the right pane, select the check box under the **Map** column for the **DQS_MAIN** database, and then select the **dqs_administrator**, **dqs_kb_editor**, or **dqs_kb_operator** check box in the **Database role membership for: DQS_MAIN** pane, depending on the access level needed for the user. For information about the three DQS roles, see [DQS Security](../dqs-security.md).  
  
7.  In the **Login - New** dialog box, click **OK** to apply the changes.  
  
    > [!NOTE]  
    >  If you grant the **dqs_administrator** role to a user, apply the changes, and then recheck the user permissions, the other two DQS roles check boxes (**dq_kb_editor** and **dqs_kb_operator**) are also selected.  
  
## Next Steps  
 Try logging on to [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] by using the Windows user account for which you just created a SQL login, and granted a DQS role.  
  
## See Also  
 [Install Data Quality Services](install-data-quality-services.md)   
 [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)  
  
  
