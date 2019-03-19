---
title: "Manage a Knowledge Base | Microsoft Docs"
ms.custom: ""
ms.date: "06/04/2013"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 27f306f4-d67c-47f5-b35c-4260cc5d36e3
author: leolimsft
ms.author: lle
manager: craigg
---
# Manage a Knowledge Base

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to perform management functions on a knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). You can delete a knowledge base, unlock it, discard your work on it, rename it, and display its properties.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To manage a knowledge base, the knowledge base must have already been created, and either published (if another person created it) or have been closed (if you created it).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor role or the dqs_administrator on the DQS_MAIN database to open a knowledge base.  
  
##  <a name="Manage"></a> Manage a Knowledge Base  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **Open knowledge base**.  
  
3.  Right-click a knowledge base in the knowledge base table.  
  
4.  In the context menu, you can do the following:  
  
    1.  **Open**: Click to open the knowledge base in the activity selected in the **Select Activity** pane.  
  
    2.  **Unlock**: You can unlock the knowledge base if you are the user who was working on the knowledge base in one of the steps of domain management, knowledge discovery, and the matching policy activity, and closed it. If you unload the knowledge base, another person will be able to open it and work on it. This command is not available if the knowledge base is not in a state of an activity. For more information, see [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
    3.  **Discard work**: Click when the knowledge base is in a state of being worked on, as shown with an entry in the State field in the table. This command is not available if the knowledge base is not in a state of an activity, and it is not available if the knowledge base is locked. For more information, see [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
    4.  **Rename**: Click to make the Knowledge Base field of the table editable for the knowledge base that you right-clicked on. Change the name, and then click on that knowledge base and another one in the field to accept the name change.  
  
    5.  **Delete**: Click to remove the knowledge base from the DQS_MAIN database on [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)].  
  
    6.  **Properties**: Click to display properties for the database in a read-only display.  
  
        1.  **Source Knowledge Base**: the knowledge base that this database was based on. This is optional.  
  
        2.  **State**: Indicates if the knowledge base is **In Work** and if it is in a specific knowledge management activity, as determined when it was last closed. The state can be **In Work**, in which the knowledge base is opened in a knowledge management session, but not in a specific activity, or **In Work** plus a knowledge management activity, in which the knowledge base is opened in a knowledge management session, and in a specific activity.  
  
        3.  **Is Locked**: **True** if the knowledge base was locked, **False** if not  
  
        4.  **Contains unpublished content**: True if the knowledge base contains content that has not been saved by publishing, False if not  
  
        5.  **Locked By**: the name of the user who closed the knowledge base, locking it  
  
        6.  **Locked Date**: date when locked  
  
        7.  **Created By**: the name of the user who created the knowledge base, with the network that he or she belongs to  
  
        8.  **Created Date**: date when created  
  
##  <a name="FollowUp"></a> Follow Up: After Managing a Knowledge Base  
 After you manage a knowledge base, your next step depends upon the action you took on the knowledge base:  
  
-   If you opened the knowledge base, you will continue in the activity that you selected.  
  
-   If you unlocked it, it will be available for another person to open and work on, in the state indicated.  
  
-   If you discarded the work on it, the knowledge base will be available in its last published state.  
  
-   If you renamed it, you will have to open the renamed knowledge base to work on it.  
  
-   If you delete it, you will have to select another knowledge base to work on, or create a new one.  
  
  
