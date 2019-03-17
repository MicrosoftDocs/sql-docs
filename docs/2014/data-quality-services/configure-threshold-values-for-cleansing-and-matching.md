---
title: "Configure Threshold Values for Cleansing and Matching | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dqs.admin.config.general.f1"
helpviewer_keywords: 
  - "cleansing,threshold value"
  - "cleansing threshold values"
  - "matching,threshold value"
ms.assetid: d2305409-7115-45a4-8f60-1213c0a47368
author: leolimsft
ms.author: lle
manager: craigg
---
# Configure Threshold Values for Cleansing and Matching
  This topic describes how to configure threshold values that will be used during the computer-assisted cleansing and matching activities in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS).  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_administrator role on the DQS_MAIN database to configure these threshold values.  
  
##  <a name="Configure"></a> Configuring the Threshold Values  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../../2014/data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, click **Configuration**.  
  
3.  Next, click the **General Settings** tab. This tab enables you to specify threshold values for cleansing as well as matching activities.  
  
4.  To specify threshold values for the cleansing activity, specify appropriate values in the following boxes under the **Interactive Cleansing** area:  
  
    -   **Min score for suggestions**: The minimum score or the confidence level that will be used by DQS for suggesting replacements for a value during the computer-assisted cleansing process. Enter a value in the decimal notation of the corresponding percentage value. For example, type 0.75 for 75%. This value should be less than or equal to the value specified in the **Min score for auto corrections** box. The default value is 0.7.  
  
    -   **Min score for auto corrections**: The minimum score or the confidence level that will be used by DQS for automatically correcting a value during the computer-assisted cleansing process. Enter a value in the decimal notation of the corresponding percentage value. For example, enter 0.9 for 90%. The default value is 0.8.  
  
5.  To specify threshold value for the matching activity, specify a value in the **Min record score** box under the **Matching** area. This value signifies the minimum score for a record to be considered as a match for another record. The default value is 80%.  
  
6.  Click **Close**.  
  
  
