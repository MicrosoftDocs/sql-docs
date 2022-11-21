---
title: "Use Value Relations in a Composite Domain"
description: "Use Value Relations in a Composite Domain"
author: swinarko
ms.author: sawinark
ms.date: "11/22/2011"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dqs.dm.cdvaluerelations.f1"
---
# Use Value Relations in a Composite Domain

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes how to view value combinations found for the composite domain during the knowledge discovery process in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). This page shows the number of occurrences of the value combinations. Value management is not supported for composite domains, so you cannot perform any operations on these values.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To view value relations, you must have created and opened a composite domain.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to view value relations in a composite domain.  
  
##  <a name="Use"></a> View Value Relations  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open or create a knowledge base. Select **Domain Management** as the activity, and then click **Open** or **Create**. For more information, see [Create a Knowledge Base](../data-quality-services/create-a-knowledge-base.md) or [Open a Knowledge Base](../data-quality-services/open-a-knowledge-base.md).  
  
3.  From the **Domain list** on the **Domain Management** page, select the composite domain that you want to create a domain rule for, or create a new composite domain. If you have to create a new domain, see [Create a Composite Domain](../data-quality-services/create-a-composite-domain.md).  
  
4.  Click the **Value Relations** tab.  
  
5.  View the frequencies displayed for each value combination.  
  
    > [!NOTE]  
    >  The **Value** table shows each combination of values that exists in the composite domain. Each value is shown in the single domain that it applies to. The default sorting of the value relations table is by frequency, but you can click on another column to sort by that column. Only those values with a frequency greater than or equal to 20 are displayed.  
  
6.  You cannot change any of the values in the table. If you have performed other operations, click **Finish** to complete the domain management activity. Otherwise, click **Cancel**.  
  
##  <a name="FollowUp"></a> Follow Up: After Viewing Value Relations  
 After you view value relations, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../data-quality-services/create-a-matching-policy.md).  
  
  
