---
title: "Create a Knowledge Base | Microsoft Docs"
ms.custom: ""
ms.date: "06/04/2013"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.kb.selectkb.f1"
  - "sql13.dqs.kb.newkb.f1"
ms.assetid: 2733a284-975f-4650-abcc-cc2aad074cab
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Create a Knowledge Base

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to create a knowledge base in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS), and prepare it for domain management, knowledge discovery, or adding a matching policy.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To create a knowledge base, you must have installed [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] and [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)].  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to create a knowledge base.  
  
##  <a name="Createaknowledgebase"></a> Create a knowledge base  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **New knowledge base**.  
  
3.  Enter a name and description for the new knowledge base.  
  
4.  In **Create knowledge base from**, select what to base the knowledge base on:  
  
    -   Select **None** if you do not want to base the new knowledge base on an existing knowledge base or data file.  
  
    -   Select **Existing Knowledge Base** to base the new knowledge base on a knowledge base that has already been created on [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)], or on the default knowledge base. Select the knowledge base from the **Select Knowledge Base** drop-down list, or click **Browse** to display the **Select a Knowledge Base** dialog box, select an existing knowledge base to base the new knowledge base on, and then click **OK**. When you select a knowledge base from the tablet, the domains and matching rules in the knowledge base will be displayed in the right-hand pane of the dialog box. You can also select the **DQS Data** knowledge base, which is the default knowledge base that contains common out-of-the-box domains and knowledge related to U.S. company, address, and party data.  
  
    -   Select **Import from DQS File** to base the new knowledge base on a DQS file on [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)]. Click **Browse**, select a DQS data file with an extension of .dqs, and then click **OK**.  
  
5.  In **Select Activity**, select the activity that you want to perform on the new knowledge base:  
  
    -   Select **Domain Management** to create the knowledge base and enter the screens that you use to modify the domains in the knowledge base.  
  
    -   Select **Knowledge Discovery** to create the knowledge base and enter the wizard that you use to analyze a data sample and populate the domains of the knowledge base with the results.  
  
    -   Select **Matching Policy** to create a matching policy and add it to the knowledge base.  
  
6.  Click **Create**.  
  
##  <a name="FollowUp"></a> Follow Up: After Creating a Knowledge Base  
 After you create a knowledge base, you are presented with a wizard that you can use to perform knowledge discovery, a wizard to create a matching policy, or pages to perform domain management. For more information about the knowledge discovery, domain management, or matching policy, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
  
