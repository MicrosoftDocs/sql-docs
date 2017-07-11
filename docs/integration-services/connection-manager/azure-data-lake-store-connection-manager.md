---
title: "Azure Data Lake Store Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPADLSCM.F1"
  - "sql14.dts.designer.afpadlscm.f1"
ms.assetid: f4c44553-0f08-4731-ac47-7534990b8c8d
caps.latest.revision: 7
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Azure Data Lake Store Connection Manager
  The **Azure Data Lake Store connection manager** enables an SSIS package to connect to an Azure Data Lake Store service through two authentication types: Azure AD User Identity and Azure AD Service Identity.  
  
 The **Azure Data Lake Store connection manager** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

>   [!NOTE]
> To ensure that the Azure Data Lake Store Connection Manager and the components that use it - that is, the Azure Data Lake Store Source and the Azure Data Lake Store Destination - can connect to services, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). 
 
## Configure the Azure Data Lake Store Connection Manager

 
1.  In the **Add SSIS Connection Manager** dialog box, select **AzureDataLake**, and click **Add**.  
  
2.  In the Azure Data Lake Store Connection Manager Editor dialog box, type in the Azure Data Lake Store host URL in **ADLS Host** field. For example: `https://test.azuredatalakestore.net` or `test.azuredatalakestore.net`.
  
3.  Choose corresponding authentication type to access Azure Data Lake Store data.

    1.  If you selected **Azure AD User Identity** authentication option, do the following:
        1. Specify values for the **User Name** and **Password** field. 
    
        2. Click **Test Connection** button to test the connection. If you and your tenant administrator didn't consent SSIS to access your Azure Data Lake Store data before, you need click **Accept** button to consent SSIS to access your Azure Data Lake Store data in the pop out dialog. For more information about this consent experience, see [Integrating applications with Azure Active Directory](https://docs.microsoft.com/en-us/azure/active-directory/active-directory-integrating-applications#updating-an-application).
    
        >   [!NOTE] 
        > For Azure AD User Identity authentication option, multi-factor authentication and Microsoft account is NOT supported.
    
    2. If you selected **Azure AD Service Identity** authentication option, do the following:
        1. Create an AAD application and service principal that can access Azure Data Lake resources.
    
        2. Assign this AAD application corresponding permissions to access your Azure Data Lake resources. For more information about this authentication option, see [Use portal to create Active Directory application and service principal that can access resources](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal).
    
        3. Specify values for **Client Id**, **Secret Key** and **Tenant Name** field.
    
        4. Click **Test Connection** button to test the connection.  
  
6.  Click **OK** to close the dialog box.  
  
    You can see the properties of the connection manager you created in the **Properties** window.  
  
  