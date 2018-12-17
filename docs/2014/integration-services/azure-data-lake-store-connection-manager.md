---
title: "Azure Data Lake Store Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "06/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.DTS.DESIGNER.AFPADLSCM.F1"
  - "SQL11.DTS.DESIGNER.AFPADLSCM.F1"
ms.assetid: 7f1323f9-9dc3-4378-9c70-bbc65bfeabfd
author: yualan
ms.author: douglasl
manager: craigg
---
# Azure Data Lake Store Connection Manager
  The **Azure Data Lake Store connection manager** enables an SSIS package to connect to an Azure Data Lake Store service through two authentication types: Azure AD User Identity and Azure AD Service Identity.  

## Configure the Azure Data Lake Store Connection Manager 
  
1.  In the **Add SSIS Connection Manager** dialog box, select **AzureDataLake**, and click **Add**.   
  
2.  In the Azure Data Lake Store Connection Manager Editor dialog box, type in the Azure Data Lake Store host URL in **ADLS Host** field. For example: https://test.azuredatalakestore.net or test.azuredatalakestore.net.
  
3.  Choose corresponding authentication type to access Azure Data Lake Store data.

    1.  If you selected **Azure AD User Identity** authentication option, do the following:

        1. Specify values for the **User Name** and **Password** field. 
    
        2. Click **Test Connection** button to test the connection. If you and your tenant administrator didn't consent SSIS to access your Azure Data Lake Store data before, you need click **Accept** button to consent SSIS to access your Azure Data Lake Store data in the pop out dialog. For more information about this consent experience, see [Integrating applications with Azure Active Directory](https://docs.microsoft.com/azure/active-directory/active-directory-integrating-applications#updating-an-application).
    
        > [!NOTE] 
        > For Azure AD User Identity authentication option, multi-factor authentication and Microsoft account is NOT supported.
    
    2.  If you selected **Azure AD Service Identity** authentication option, do the following:
        1. Create an AAD application and service principal that can access Azure Data Lake resources.
    
        2. Assign this AAD application corresponding permissions to access your Azure Data Lake resources. For more information about this authentication option, see [Use portal to create Active Directory application and service principal that can access resources](https://docs.microsoft.com/azure/azure-resource-manager/resource-group-create-service-principal-portal).
    
        3. Specify values for **Client Id**, **Secret Key** and **Tenant Name** field.
    
        4. Click **Test Connection** button to test the connection.  
  
4.  Click **OK** to close the dialog box.  
  
    You can see the properties of the connection manager you created in the **Properties** window.  
  
  
