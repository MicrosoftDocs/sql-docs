---
title: "Configure DQS to Use Reference Data"
description: "Configure DQS to Use Reference Data"
author: swinarko
ms.author: sawinark
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dqs.administration.rdsconfiguration.f1"
  - "sql13.dqs.administration.configuration.createDirectRDS.f1"
  - "sql13.dqs.admin.config.rds.f1"
---
# Configure DQS to Use Reference Data

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes how to configure [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) to use reference data for cleansing your data. You could either use reference data from Azure Marketplace or from direct online third-party reference data providers.  

> [!IMPORTANT]
> This article mentions third-party reference data services that were previously available from the Azure DataMarket. DataMarket and Data Services - including Melissa address data, for example - were discontinued after 12/31/2016. As a result, you can no longer run the examples in this article with the specified services from DataMarket. You can still use reference data services that are available directly online from third-party reference data providers.

## Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To use reference data from Marketplace, you must have a valid Marketplace account key. For detailed information about creating a Marketplace account key, see [Create Your Account](/previous-versions/azure/ff717655(v=azure.100)) (https://go.microsoft.com/fwlink/?LinkId=212936). You can also create a Marketplace account key from within [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] by clicking **Configuration** under **Administration** in the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, and then clicking **Create a DataMarket Account ID** under the **Reference Data** tab.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_administrator role on the DQS_MAIN database to configure reference data service settings in DQS.  
  
##  <a name="Marketplace"></a> Configure DQS to Use Reference Data from Marketplace  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, under **Administration**, click **Configuration**.  
  
3.  In the **Reference Data** tab, under the **Network Settings** area, type appropriate values in the **Proxy Server** and **Port** boxes if you or your organization uses proxy server to connect to the Internet.  
  
4.  Specify the Marketplace account key in the **DataMarket Account ID** box, and click the **Validate DataMarket Account ID** icon to validate the account key. A message appears to display whether the specified Marketplace account key is valid.  
  
 You are now ready to use the reference data services from Marketplace in DQS that are subscribed for the specified Marketplace account key.  
  
##  <a name="ThirdParty"></a> Configure DQS to Use Reference Data from Direct Online Third-Party Reference Data Providers  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, under **Administration**, click **Configuration**.  
  
3.  In the **Reference Data** tab, under the **Network Settings** area, type appropriate values in the **Proxy Server** and **Port** boxes if you or your organization uses proxy server to connect to the Internet.  
  
4.  In the **Direct Online 3rd Party Reference Data Service Settings** area, click the **Add new reference data service provider** icon.  
  
5.  In the **Create New Direct Online 3rd Party Reference Data Service Provider** dialog box, specify the following details:  
  
    1.  In the **Name** box, type a name of the new direct reference data service provider.  
  
    2.  (Optional) In the **Description** box, type a description of the new direct reference data service provider.  
  
    3.  In the **Category** box, type the category of the data provided by the new direct reference data service provider.  
  
    4.  In the Schema box, specify the schema that defines the string of fields (column names) to be used from the direct reference data service provider. A field name should not contain a space, and the fields should be separated by commas. For example: `FirstName, LastName, City, State`.  
  
    5.  In the **URI** box, type the URI of the direct reference data service provider. Only secure URIs (address starting with "https://") are allowed in DQS.  
  
    6.  In the **Max Batch Size** box, type the maximum number of records per batch that will be sent to the reference data service provider for cleansing. A maximum of 100 records per batch can be specified for the cleansing activity.  
  
    7.  In the **Account ID** box, type the account ID of the subscriber with the reference data service provider.  
  
6.  Click **OK** to save the data, and close the **Create New Direct Online 3rd Party Reference Data Service Provider** dialog box. The newly added direct online third party reference data provider becomes available in the **Direct Reference Data Service Providers Grid** in DQS.  
  
 You are now ready to use the reference data services from the newly configured direct online third-party reference data service provider in DQS.  
  
##  <a name="FollowUp"></a> Follow Up: After Configuring DQS to use Reference Data  
 You must now map the required knowledge base domains to the reference data available from the data providers you just configured. To do so, see [Attach Domain or Composite Domain to Reference Data](../data-quality-services/attach-domain-or-composite-domain-to-reference-data.md).  
  
