---
title: "Manage DQS Users in SSMS | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 955af01d-00da-4c51-9311-f3848749df54
author: leolimsft
ms.author: lle
manager: craigg
---
# Manage DQS Users in SSMS
  This topic describes how to create additional users in the SQL Server instance using SQL Server Management Studio, and grant them appropriate [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) roles on the DQS_MAIN database.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Your Windows user account must be a member of the appropriate fixed server role (such as securityadmin, serveradmin, or sysadmin) to create SQL login, and grant appropriate DQS roles.  
  
##  <a name="GrantRoles"></a> Create a SQL Login and Grant DQS Role  
  
1.  Start Microsoft SQL Server Management Studio.  
  
2.  In Microsoft SQL Server Management Studio, expand your SQL Server instance, and then expand **Security**.  
  
3.  Right-click the **Security** folder, point to **New**, and then click **Login**.  
  
4.  In the **Login - New** dialog box, specify the name of a Windows user in the **Login name** box, specify the type of authentication as **Windows authentication**, and click **Search** to validate the user.  
  
    > [!NOTE]  
    >  DQS only supports Windows authentication; SQL Server authentication is not supported.  
  
5.  After the user is validated, click the **User Mapping** page in the left pane.  
  
6.  In the right pane, select the check box under the **Map** column for the **DQS_MAIN** database, and then select the **dqs_administrator**, **dqs_kb_editor**, or **dqs_kb_operator** check box in the **Database role membership for: DQS_MAIN** pane, depending on the access level needed for the user.  
  
7.  In the **Login - New** dialog box, click **OK** to apply the changes.  
  
    > [!NOTE]  
    >  If you grant the **dqs_administrator** role to a user, apply the changes, and then recheck the user permissions, the other two DQS roles check boxes (**dq_kb_editor** and **dqs_kb_operator**) are also selected.  
  
  
