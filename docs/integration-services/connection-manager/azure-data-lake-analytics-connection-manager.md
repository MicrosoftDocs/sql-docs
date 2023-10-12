---
title: "Azure Data Lake Analytics connection manager"
description: A SQL Server Integration Services (SSIS) package can use the Azure Data Lake Analytics connection manager to connect to a Data Lake Analytics account.
author: "yanancai"
ms.author: "yanacai"
ms.reviewer: maghan
ms.date: "05/18/2018"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords:
  - "SQL13.DTS.DESIGNER.AFPADLSCM.F1"
  - "sql14.dts.designer.afpadlscm.f1"
---
# Azure Data Lake Analytics connection manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

> [!IMPORTANT]
> Azure Data Lake Analytics will be retired on **29 February 2024**. Learn more [with this announcement](https://azure.microsoft.com/updates/migrate-to-azure-synapse-analytics/).

A SQL Server Integration Services (SSIS) package can use the Azure Data Lake Analytics connection manager to connect to a Data Lake Analytics account with one of the two following authentication types:
-   Microsoft Entra user identity
-   Microsoft Entra service identity 

The Data Lake Analytics connection manager is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).


[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]
 
## Configure the connection manager

1. In the **Add SSIS Connection Manager** dialog box, select **AzureDataLakeAnalytics** > **Add**. The **Azure Data Lake Analytics Connection Manager Editor** dialog box opens.
  
2. In the **Azure Data Lake Analytics Connection Manager Editor** dialog box, in the **ADLA Account Name** field, provide the Data Lake Analytics account name. For example: myadlaaccountname.
  
3. In the **Authentication** field, choose the appropriate authentication type to access the data in Data Lake Analytics.

   1. If you select the **Azure Active Directory User Identity** authentication option:
   
      i. Provide values for the **User Name** and **Password** fields.    
      ii. To test the connection, select **Test Connection**. If you or the tenant administrator didn't previously consent to allow SSIS to access your Data Lake Analytics account, select **Accept** when prompted. For more information about this consent experience, see [Integrating applications with Microsoft Entra ID](/azure/active-directory/manage-apps/plan-an-application-integration#integrating-applications-with-azure-ad).
    
   > [!NOTE] 
   > When you select the **Azure Active Directory User Identity** authentication option, multifactor authentication and Microsoft account authentication are not supported.
    
   1.  If you select the **Azure Active Directory Service Identity** authentication option:
   
      1. Create a Microsoft Entra application and service principal to access the Data Lake Analytics account. For more information about this authentication option, see [Create a Microsoft Entra application and service principal that can access resources](/azure/azure-resource-manager/resource-group-create-service-principal-portal).    
      1.  Assign appropriate permissions to let this Microsoft Entra application access your Data Lake Analytics account. Learn how to grant permissions to your Data Lake Analytics account by using the [Add User wizard](/azure/data-lake-analytics/data-lake-analytics-manage-use-portal#add-a-new-user).    
      1. Provide values for the **Application ID**, **Authentication Key**, and **Tenant ID** fields.    
      1. To test the connection, select **Test Connection**.  

4. Select **OK** to close the **Azure Data Lake Analytics Connection Manager Editor** dialog box.  

## View the properties of the connection manager
You can see the properties of the connection manager you created in the **Properties** window.  
  
