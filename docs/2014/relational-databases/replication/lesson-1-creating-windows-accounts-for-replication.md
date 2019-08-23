---
title: "Lesson 1: Creating Windows Accounts for Replication | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
  - "replication [SQL Server], administering"
ms.assetid: 65c3816b-47f0-448c-a4a4-ebd3e2a58820
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 1: Creating Windows Accounts for Replication
  In this lesson, you will create Windows accounts to run replication agents. You will create a separate Windows account on the local server for the following agents:  
  
|Agent|Location|Account name|  
|-----------|--------------|------------------|  
|Snapshot Agent|Publisher|\<*machine_name*>\repl_snapshot|  
|Log Reader Agent|Publisher|\<*machine_name*>\repl_logreader|  
|Distribution Agent|Publisher and Subscriber|\<*machine_name*>\repl_distribution|  
|Merge Agent|Publisher and Subscriber|\<*machine_name*>\repl_merge|  
  
> [!NOTE]  
>  In the replication tutorials, the Publisher and Distributor share the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The Publisher and Subscriber may share the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but it is not a requirement. If the Publisher and Subscriber share the same instance, the steps that are used to create accounts at the Subscriber are not required.  
  
### To create local Windows accounts for replication agents at the Publisher  
  
1.  At the Publisher, open **Computer Management** from **Administrative Tools** in Control Panel.  
  
2.  In **System Tools**, expand **Local Users and Groups**.  
  
3.  Right-click **Users** and then click **New User**.  
  
4.  Enter `repl_snapshot` in the **User name** box, provide the password and other relevant information, and then click **Create** to create the repl_snapshot account.  
  
5.  Repeat the previous step to create the repl_logreader, repl_distribution, and repl_merge accounts.  
  
6.  Click **Close**.  
  
### To create local Windows accounts for replication agents at the Subscriber  
  
1.  At the Subscriber, open **Computer Management** from **Administrative Tools** in Control Panel.  
  
2.  In **System Tools**, expand **Local Users and Groups**.  
  
3.  Right-click **Users** and then click **New User**.  
  
4.  Enter `repl_distribution` in the **User name** box, provide the password and other relevant information, and then click **Create** to create the repl_distribution account.  
  
5.  Repeat the previous step to create the repl_merge account.  
  
6.  Click **Close**.  
  
## Next Steps  
 You have successfully created Windows accounts for replication agents. Next, you will configure the snapshot folder. See [Lesson 2: Preparing the Snapshot Folder](lesson-2-preparing-the-snapshot-folder.md).  
  
## See Also  
 [Replication Agents Overview](agents/replication-agents-overview.md)  
  
  
