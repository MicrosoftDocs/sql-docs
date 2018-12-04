---
title: "Azure Resource Manager Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.DTS.DESIGNER.AFPARMCM.F1"
  - "SQL11.DTS.DESIGNER.AFPARMCM.F1"
ms.assetid: a2380258-0418-4a8c-a731-5071a44ddf1e
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Azure Resource Manager Connection Manager
The **Azure Resource Manager Connection Manager** enables an SSIS package to manage Azure resources using a [service principal](https://docs.microsoft.com/azure/azure-resource-manager/resource-group-create-service-principal-portal).

To create and configure an **Azure Resource Manager Connection Manager**, follow the steps below:

1. In the **Add SSIS Connection Manager** dialog box, select **AzureResourceManager**, and click **Add**.
2. In the **Azure Resource Manager Connection Manager Editor** dialog box, specify the **Application ID**, **Application Key**, and **Tenant ID** for the service principal. For details about these properties, please refer to [this](https://docs.microsoft.com/azure/azure-resource-manager/resource-group-create-service-principal-portal) article.
3. Click **OK** to close the dialog box.
4. You can see the properties of the connection manager you created in the **Properties** window.
