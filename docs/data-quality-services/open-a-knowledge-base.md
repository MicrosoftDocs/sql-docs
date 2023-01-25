---
title: "Open a Knowledge Base"
description: "Open a Knowledge Base"
author: swinarko
ms.author: sawinark
ms.date: "06/04/2013"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dqs.kb.openkb.f1"
---
# Open a Knowledge Base

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes how to open an existing knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS), and prepare it for domain management, knowledge discovery, or adding a matching policy.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To open a knowledge base, the knowledge base must have already been created, and either published (if another person created it) or have been closed (if you created it).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor role or the dqs_administrator on the DQS_MAIN database to open a knowledge base.  
  
##  <a name="Open"></a> Open a knowledge base  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **Open knowledge base**.  
  
3.  Select a knowledge base in the table. The domains and matching rules in the knowledge base will be displayed in the right-hand pane of the page.  
  
    > [!NOTE]  
    >  You can perform operations on a knowledge base by right-clicking it in the table. You can open the knowledge base, save it with another name, unlock it, discard the work, rename it, or display its properties.  
  
4.  In **Select Activity**, select the activity that you want to perform on the knowledge base:  
  
    -   Select **Domain Management** to enter the screens that you use to modify the domains in the knowledge base.  
  
    -   Select **Knowledge Discovery** to enter the wizard that you use to analyze a data sample and populate the domains of the knowledge base with the results.  
  
    -   Select **Matching Policy** to create a matching policy and add it to the knowledge base.  
  
5.  Click **Open**.  
  
    > [!NOTE]  
    >  You can also open the knowledge base by right-clicking it, and then clicking Open. Other commands in the context menu enable you to save it with another name, unlock it, discard the work, rename it, or display its properties.  
  
    > [!NOTE]  
    >  If you cannot open the knowledge base because it is locked, see the section below.  
  
## Open a Recent Knowledge Base  
 The five most recently opened knowledge bases are displayed in the **Recent Knowledge Base** list in the DQS home page. This enables you to open a knowledge base that you recently worked on without going through the **Open Knowledge Base** page.  
  
-   To open a knowledge base in the Recent list that is not locked, click the right arrow for the knowledge base, and then select the activity that you want to open the knowledge base in.  
  
-   To open a knowledge base in the Recent list that you locked, click the knowledge base and it will open in the activity and page indicated in parentheses.  
  
-   To open a knowledge base in the Recent list that has been locked by someone else, contact that person and have them unlock the knowledge base.  
  
##  <a name="FollowUp"></a> Follow Up: After Opening a Knowledge Base  
 After you open a knowledge base, the knowledge base is put into the state indicated in the State column of the Knowledge Base table. For the knowledge discovery and matching policy activities, the knowledge base will be opened in a specific wizard page. For the domain management activity, the knowledge base will be opened in the domain management page. For more information about the states, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
##  <a name="Locked"></a> If the knowledge base is locked  
 The lock icon in the first column shows whether the knowledge base is locked. The name of a locked knowledge base will be in red font. A knowledge base that is being modified by a specific user through a knowledge base activity is marked as Locked. A locked knowledge base cannot be worked on by a second user. The user who is working on the knowledge base can unlock it by right-clicking the knowledge base in the table on the Open Knowledge Base page, and clicking **Unlock**, or by publishing it. When the cursor is positioned on a locked knowledge base, DQS will display a hint showing who locked the knowledge base and when they locked it.  
  
##  <a name="State"></a> State of a Knowledge Base  
 The State field indicates which stage of an activity the knowledge base is at. If you open the knowledge base, it will open to that stage.  
  
-   **\<Empty>**: The State field is empty for a knowledge base if the knowledge base has been published by clicking **Publish** in the Domain Management activity, and clicking **Yes - Publish the knowledge base and exit**.  
  
-   **In Work**: Work on the knowledge base has been saved by clicking **Publish** in the Domain Management activity, and clicking **No - Save the work on the knowledge base and exit**.  
  
-   **Domain Management**: Data has been entered for a domain in the knowledge base, but the knowledge base has not been published and the work remains in the Domain Management activity. The Knowledge Discovery activity is not available. This occurs when you click **Close** in the **Domain Management** screen.  
  
-   **Discovery - Mapping**: The knowledge base was closed on the **Knowledge Base Management: Mapping** page. The knowledge base is locked, and the Domain Management and Matching activities are not available.  
  
-   **Discovery - Discover**: The knowledge base was closed on the **Knowledge Base Management: Analyze** page. The knowledge base is locked, and The Domain Management activity is not available.  
  
-   **Discovery - Value Management**: The knowledge base was closed on the **Knowledge Base Management: Manage Domain Terms** page. The knowledge base is locked, and the Domain Management activity is not available.  
  
-   **Matching Policy - Matching Policy**: The knowledge base was closed on the **Matching Policy - Matching Policy** page. The knowledge base is locked, and the Knowledge Discovery and Domain Management activities are not available.  
  
-   **Matching Policy - Matching Results**: The knowledge base was closed on the **Matching Policy - Matching Results** page. The knowledge base is locked, and the Knowledge Discovery and Domain Management activities are not available.  
  
  
