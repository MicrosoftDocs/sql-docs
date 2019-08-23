---
title: "Lesson 2: Preparing the Snapshot Folder | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: f286cde9-c0d0-43ef-b7ba-53c3cbb8906c
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 2: Preparing the Snapshot Folder
  In this lesson, you will learn to configure the snapshot folder that is used to create and store the publication snapshot.  
  
### To create a share for the snapshot folder and assign permissions  
  
1.  In Windows Explorer, navigate to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data folder. The default location is C:\Program Files\Microsoft SQL Server\MSSQL.X\MSSQL\Data.  
  
2.  Create a new folder named **repldata**.  
  
3.  Right-click this folder and click **Properties**.  
  
4.  On the **Sharing** tab in the **repldata Properties** dialog box, click **Share**.  
  
5.  In the **File Sharing** dialog box, click **Share**, and then click **Done**.  
  
6.  On the **Security** tab, click **Edit**.  
  
7.  In the **Permissions** dialog box, click **Add.** In the **Select User, Computers, Service Account, or Groups** text box, type the name of the Snapshot Agent account created in Lesson 1, as \<_Machine_Name>_**\repl_snapshot**, where \<*Machine_Name>* is the name of the Publisher. Click **Check Names**, and then click **OK**.  
  
8.  Repeat the previous step to add permissions for the Distribution Agent, as \<_Machine_Name>_**\repl_distribution**, and for the Merge Agent as \<_Machine_Name>_**\repl_merge**.  
  
9. Verify the following permissions are allowed:  
  
    -   repl_snapshot - Full Control  
  
    -   repl_distribution - Read  
  
    -   repl_merge - Read  
  
10. Click **OK** to close the **repldata Properties** dialog box and create the repldata share.  
  
## Next Steps  
 You have successfully configured the share for the snapshot folder. Next, you will configure distribution. See [Lesson 3: Configuring Distribution](lesson-3-configuring-distribution.md).  
  
## See Also  
 [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md)  
  
  
