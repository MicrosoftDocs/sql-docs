---
title: "End the Domain Management Activity | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: ab6505ad-3090-453b-bb01-58435e7fa7c0
author: leolimsft
ms.author: lle
manager: craigg
---
# End the Domain Management Activity
  This topic describes how to complete, close, or cancel the domain management activity in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). Domain management is not performed by a wizard, so the controls described below can used from any of the pages of the domain management activity.  
  
## End Domain Management  
 **Finish**  
 Click to complete domain management. A popup will be displayed enabling you to do the following:  
  
-   **Yes - Publish the knowledge base and exit**: The knowledge base will be published for the current user or others to use. The knowledge base will not be locked, the state of the knowledge base (in the knowledge base table) will be set to empty, and both the Domain Management and Knowledge Discovery activities will be available. You will be returned to the Open Knowledge Base screen.  
  
-   **No - Save the work on the knowledge base and exit**: Your work will be saved, the knowledge base will remained locked, and the state of the knowledge base will be set to In work. Both the Domain Management and Knowledge Discovery activities will be available. You will be returned to the home page.  
  
-   **Cancel - Stay on the current screen**: The popup will be closed and you will be returned to the Domain Management screen.  
  
 **Cancel**  
 Click to terminate the Domain Management activity, losing your work, and return to the DQS home page.  
  
 **Close**  
 Click to save your work, and return to the DQS home page. The knowledge base will be locked, and the state of the knowledge base in the knowledge base table in the **Open Knowledge Base** screen will be **Domain Management**. After clicking **Close**, to perform the Knowledge Discovery activity, you would have to return to the **Domain Management** screen, click **Finish**, and then click either **Yes** to publish the knowledge base or **No** to save the work on the knowledge base and exit.  For more information on opening a locked knowledge base, see [Open a Knowledge Base](../../2014/data-quality-services/open-a-knowledge-base.md).  
  
  
