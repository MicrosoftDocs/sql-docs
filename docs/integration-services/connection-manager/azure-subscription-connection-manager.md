---
title: "Azure Subscription Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.afpsubscrconn.f1"
  - "sql14.dts.designer.afpsubscrconn.f1"
ms.assetid: e1225327-c308-4c50-8f44-c411f52ef378
author: janinezhang
ms.author: janinez
manager: craigg
---
# Azure Subscription Connection Manager
  The **Azure Subscription connection manager** enables an SSIS package to connect to an Azure subscription by using the values you specify for the properties: Azure Subscription ID and Management Certificate.  
  
 The **Azure Subscription connection manager** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).
  
1.  In the **Add SSIS Connection Manager** dialog box shown above, you select **Azure Subscription**, and click **Add**.  You should see the following **Azure Subscription Connection Manager Editor** dialog box.  
  
    ![SSIS-AzureSubscriptionConnectionManager](../../integration-services/connection-manager/media/ssis-azuresubscriptionconnectionmanager.png)
  
2.  Enter your Azure subscription ID, which uniquely identifies an Azure subscription, for the **Azure subscription ID**.  The value can be found on the [Azure Management Portal](https://manage.windowsazure.com) under **Settings** page:  
  
    ![SSIS-AzureSettings-SubscriptionID](../../integration-services/connection-manager/media/ssis-azuresettings-subscriptionid.png "SSIS-AzureSettings-SubscriptionID")  
  
3.  Choose **Management certificate store location** and **Management certificate store name** from the drop-down lists.  
  
4.  Enter **Management certificate thumbprint** or click the **Browse...** to choose a certificate from the selected store. The certificate must be uploaded as a management certificate for the subscription. To do so, click **Upload** on the following page of the Azure Portal (see this [MSDN post](https://msdn.microsoft.com/library/azure/gg551722.aspx) for more detail).  
  
     ![SSIS-AzureSettings-ManagementCertificate](../../integration-services/connection-manager/media/ssis-azuresettings-managementcertificate.png "SSIS-AzureSettings-ManagementCertificate")  
  
5.  Click **Test Connection** to test the connection.  
  
6.  Click **OK** to close the dialog box.  
  
  
