---
title: "Azure Data Lake Store Connection Manager"
description: "Azure Data Lake Store Connection Manager"
author: "Lingxi-Li"
ms.author: "lingxl"
ms.reviewer: maghan
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords:
  - "SQL13.DTS.DESIGNER.AFPADLSCM.F1"
  - "sql14.dts.designer.afpadlscm.f1"
---
# Azure Data Lake Store Connection Manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


A SQL Server Integration Services (SSIS) package can use the Azure Data Lake Store Connection Manager to connect to an Azure Data Lake Storage Gen1 account with one of the two following authentication types:
-   Microsoft Entra user identity
-   Microsoft Entra service identity 

The Azure Data Lake Store Connection Manager is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## Prerequisites 

To ensure that the Azure Data Lake Store Connection Manager and the components that use it - that is, the Data Lake Storage Gen1 source and the Data Lake Storage Gen1 destination - can connect to services, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). 
 
## Configure the Azure Data Lake Store Connection Manager

1.  In the **Add SSIS Connection Manager** dialog box, select **AzureDataLake**, and then select **Add**. The **Azure Data Lake Store Connection Manager Editor** dialog box opens.
  
2.  In the **Azure Data Lake Store Connection Manager Editor** dialog box, in the **ADLS Host** field, provide the Data Lake Storage Gen1 host URL. For example: `https://test.azuredatalakestore.net` or `test.azuredatalakestore.net`.
  
3.  In the **Authentication** field, choose the appropriate authentication type to access the data in Data Lake Storage Gen1.

    1.  If you select the **Azure Active Directory User Identity** authentication option, do the following things:
        1. Provide values for the **User Name** and **Password** fields. 
    
        2. To test the connection, select **Test Connection**. If you or the tenant administrator didn't previously consent to allow SSIS to access your Data Lake Storage Gen1 data, select **Accept** when prompted. For more information about this consent experience, see [Integrating applications with Microsoft Entra ID](/azure/active-directory/manage-apps/plan-an-application-integration#integrating-applications-with-azure-ad).
    
        > [!NOTE] 
        > When you select the **Azure Active Directory User Identity** authentication option, multifactor authentication and Microsoft account authentication are not supported.
    
    2. If you select the **Azure Active Directory Service Identity** authentication option, do the following things:
        1. Create a Microsoft Entra application and service principal to access the Data Lake Storage Gen1 data.
    
        2. Assign appropriate permissions to let this Microsoft Entra application access your Data Lake Storage Gen1 resources. For more information about this authentication option, see [Create a Microsoft Entra application and service principal that can access resources](/azure/azure-resource-manager/resource-group-create-service-principal-portal).
    
        3. Provide values for the **Client Id**, **Secret Key**, and **Tenant Name** fields.
    
        4. To test the connection, select **Test Connection**.  
  
6.  Select **OK** to close the **Azure Data Lake Store Connection Manager Editor** dialog box.  

## View the properties of the connection manager
You can see the properties of the connection manager you created in the **Properties** window.  
  
