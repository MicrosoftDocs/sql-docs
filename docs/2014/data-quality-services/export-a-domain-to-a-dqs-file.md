---
title: "Export a Domain to a .dqs File | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: eba10d3d-b5c4-447b-8a30-fa07996cb28e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Export a Domain to a .dqs File
  This topic describes how to export a domain to a .dqs file in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). You can export a domain or an entire knowledge base to a data file. For information about exporting a knowledge base, see [Export a Knowledge Base to a .dqs File](../../2014/data-quality-services/export-a-knowledge-base-to-a-dqs-file.md).  
  
 Exporting a domain from one knowledge base to a .dqs data file, and then importing it to another knowledge base simplifies the knowledge generation process, saving time and effort. It enables you to share a domain and its knowledge with others.  
  
 You can export either a single domain or composite domain. A .dqs file containing a single domain includes all domain data including domain properties, values, and rules except for the attached reference data information. A .dqs file containing a composite domain includes all composite domain data, including all domain data for the domains that are contained in the composite domain, and the composite domain properties, relations, and rules, except for the reference data information. You must attach the domain or the composite domain to appropriate reference data services again, if required, after importing the .dqs file. Both published and unpublished data is exported.  
  
 The .dqs data file created by the export process is encrypted, so the contents cannot be viewed.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To export a domain to a .dqs data file, you must have created and selected a single domain or a composite domain containing multiple single domains. You do not need to have a .dqs file to export into; one will be created for you.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to export a domain to a .dqs data file.  
  
##  <a name="Export"></a> Export a domain to a .dqs file  
 You can export from any Domain Management page. The export command is available from both a control in the user interface and from a command in the context menu of the Domain List pane.  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../../2014/data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open a knowledge base in the Domain Management activity.  
  
3.  In the **Domain Management page** (with any tab selected), select a single domain or a composite domain in the **Domain** list.  
  
4.  Click the **Export Knowledge Base data** icon above the domain list, and then click **Export Domain**. Alternatively, you can right-click the domain in the **Domain** list, point to **Export**, and then click **Export Domain**.  
  
5.  In the **Export to Data File** dialog box, move to the folder that you want to save the file in, name the file or keep the default name, keep **DQS Data Files (\*.dqs)** as the **Save as type**, and then click **Save**.  
  
6.  In the **Export Domain** dialog box, verify that the status line in the dialog box indicates that the export completed. Click **OK**.  
  
##  <a name="FollowUp"></a> Follow Up: After Exporting a Domain to a .dqs File  
 After you export a domain to a .dqs file, you can import the domain into another knowledge base.  
  
  
