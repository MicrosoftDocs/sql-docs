---
title: "Import a Knowledge Base from a .dqs File | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 9b9786fe-9e80-429a-afcb-dc3b3dd6f0b0
author: leolimsft
ms.author: lle
manager: craigg
---
# Import a Knowledge Base from a .dqs File

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes how to import an entire knowledge base from a .dqs data file in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). You create the data file by exporting an existing knowledge base from within the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application (see [Export a Knowledge Base to a .dqs File](../data-quality-services/export-a-knowledge-base-to-a-dqs-file.md)).  
  
 Using a .dqs data file to export the contents of a knowledge base and then import the contents into another knowledge base on the same [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] or a different [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] simplifies the knowledge generation process, saving time and effort. It enables you to share a knowledge base and its knowledge with others, saving them time. The .dqs file will contain all knowledge base information, including domains and the matching policy, except for the attached reference data information. Published and unpublished data will be imported.  
  
 A .dqs data file is encrypted, so cannot be viewed.  
  
 When you import a knowledge base, you can use the same name, unless the knowledge base name already exists in the client application, in which case you must rename it.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To import a knowledge base from a .dqs file, you must have already exported the knowledge base into the .dqs file.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to import a knowledge base from a .dqs data file.  
  
##  <a name="Import"></a> Import a knowledge base from a .dqs file  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **New knowledge base**.  
  
3.  Enter a name for the knowledge base.  
  
4.  Click the down arrow for **Create knowledge base from**, and then select **Import from DQS file**.  
  
5.  For **Select data file**, click **Browse**.  
  
6.  In the **Import from Data File** dialog box, move to the folder that contains the .dqs file that you want to import, and then click the name of the file. Click **Open**.  
  
7.  Verify that the correct knowledge base and domains are displayed in the **Domain** list.  
  
8.  Select the activity that you want to perform, and then click **Create**.  
  
9. In the **Import Knowledge Base** dialog box, verify that the status line indicates that the import completed. Click **OK**.  
  
10. Complete the knowledge discovery, domain management, or matching policy tasks that you need to perform, and then click **Finish**.  
  
11. Click **Publish** to publish the knowledge in the knowledge base, or **No** not to.  
  
12. If you published the knowledge base, click **OK**.  
  
13. In the Data Quality Services home page, verify that the knowledge base is listed under **Recent knowledge bases**.  
  
##  <a name="FollowUp"></a> Follow Up: After Importing a Knowledge Base from a .dqs File  
 After you import a knowledge base from a .dqs file, you can add knowledge to the knowledge base or use the knowledge base in a cleansing or matching project, depending on the contents of the knowledge base. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), [Managing a Composite Domain](../data-quality-services/managing-a-composite-domain.md), [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md), [Data Cleansing](../data-quality-services/data-cleansing.md), or [Data Matching](../data-quality-services/data-matching.md).  
  
  
