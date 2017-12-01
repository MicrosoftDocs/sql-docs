---
title: "Azure Data Lake Store Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "integration-services"
ms.service: ""
ms.component: "connection-manager"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPADLSCM.F1"
  - "sql14.dts.designer.afpadlscm.f1"
ms.assetid: f4c44553-0f08-4731-ac47-7534990b8c8d
caps.latest.revision: 7
author: "Lingxi-Li"
ms.author: "lingxl"
ms.reviewer: "douglasl"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Azure Data Lake Store Connection Manager
A SQL Server Integration Services (SSIS) package can use the Azure Data Lake Store connection manager to connect to an Azure Data Lake Store service with one of the two following authentication types:
-   Azure AD User Identity
-   Azure AD Service Identity 

The Azure Data Lake Store connection manager is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

>   [!NOTE]
> To ensure that the Azure Data Lake Store Connection Manager and the components that use it - that is, the Azure Data Lake Store Source and the Azure Data Lake Store Destination - can connect to services, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). 
 
## Configure the Azure Data Lake Store Connection Manager

1.  In the **Add SSIS Connection Manager** dialog box, select **AzureDataLake**, and then select **Add**. The **Azure Data Lake Store Connection Manager Editor** dialog box opens.
  
2.  In the **Azure Data Lake Store Connection Manager Editor** dialog box, in the **ADLS Host** field, provide the Azure Data Lake Store host URL. For example: `https://test.azuredatalakestore.net` or `test.azuredatalakestore.net`.
  
3.  In the **Authentication** field, choose the appropriate authentication type to access the data in Azure Data Lake Store.

    1.  If you select the **Azure AD User Identity** authentication option, do the following things:
        1. Provide values for the **User Name** and **Password** fields. 
    
        2. To test the connection, select **Test Connection**. If you or the tenant administrator didn't previously consent to allow SSIS to access your Azure Data Lake Store data, select **Accept** when prompted. For more information about this consent experience, see [Integrating applications with Azure Active Directory](https://docs.microsoft.com/azure/active-directory/active-directory-integrating-applications#updating-an-application).
    
        >   [!NOTE] 
        > When you select the **Azure AD User Identity** authentication option, multi-factor authentication and Microsoft account authentication are not supported.
    
    2. If you select the **Azure AD Service Identity** authentication option, do the following things:
        1. Create an Azure Active Directory (AAD) application and service principal to access the Azure Data Lake data.
    
        2. Assign appropriate permissions to let this AAD application access your Azure Data Lake resources. For more information about this authentication option, see [Use portal to create Active Directory application and service principal that can access resources](https://docs.microsoft.com/azure/azure-resource-manager/resource-group-create-service-principal-portal).
    
        3. Provide values for the **Client Id**, **Secret Key**, and **Tenant Name** fields.
    
        4. To test the connection, select **Test Connection**.  
  
6.  Select **OK** to close the **Azure Data Lake Store Connection Manager Editor** dialog box.  

## View the properties of the connection manager
You can see the properties of the connection manager you created in the **Properties** window.  
  
  
