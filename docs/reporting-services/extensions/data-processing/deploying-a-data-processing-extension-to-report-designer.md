---
title: "How to: Deploy a Data Processing Extension to Report Designer | Microsoft Docs"
description: Find out how to deploy a data processing extension to Report Designer by learning which entries to add to which configuration files.
ms.date: 03/14/2017
ms.prod: reporting-services
ms.technology: extensions
ms.topic: reference
helpviewer_keywords:
  - "data processing extensions [Reporting Services], deploying"
  - "assemblies [Reporting Services], data processing extension deployments"
ms.assetid: 3614e601-004e-4a16-8388-836ffd67e9dd
author: maggiesMSFT
ms.author: maggies
ms.custom:
  - intro-deployment
---
# Deploying a Data Processing Extension to Report Designer
  Report Designer uses data processing extensions for retrieving and processing data while you are designing reports. You should deploy your data processing extension assembly to Report Designer as a private assembly. You also need to make an entry in the Report Designer configuration file, RSReportDesigner.config.  
  
#### To deploy a data processing extension assembly  
  
1.  Copy your assembly from your staging location to the Report Designer directory. The default location of the Report Designer directory is C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies.  
  
2.  After the assembly file is copied, open the RSReportDesigner.config file. The RSReportDesigner.config file is also located in the Report Designer directory. You need to make an entry in the configuration file for your data processing extension assembly file. You can open the configuration file with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] or with a simple text editor, such as Notepad.  
  
3.  Locate the **Data** element in the RSReportDesigner.config file. An entry for your newly created data processing extension should be made in the following location:  
  
    ```  
    <Extensions>  
       <Data>  
          <Your extension configuration information goes here>  
       </Data>  
    </Extensions>  
    ```  
  
4.  Add an entry for your data processing extension which includes an **Extension** element with values for the **Name**, **Type**, and **Visible** attributes. Your entry might look like the following:  
  
    ```  
    <Extension Name="ExtensionName" Type="CompanyName.ExtensionName.MyConnectionClass, AssemblyName" />  
    ```  
  
     The value for **Name** is the unique name of the data processing extension. The value for **Type** is a comma-separated list that includes an entry for the fully qualified namespace of your class that implements the <xref:Microsoft.ReportingServices.Interfaces.IExtension> and <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection> interfaces, followed by the name of your assembly (not including the .dll file extension). By default, data processing extensions are visible. To hide an extension from user interfaces, such as Report Designer, add a **Visible** attribute to the **Extension** element, and set it to **false**.  
  
5.  Finally, add a code group for your custom assembly that grants **FullTrust** permission for your extension. You do this by adding the code group to the rspreviewpolicy.config file located by default in C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies. Your code group might look like the following:  
  
    ```  
    <CodeGroup class="UnionCodeGroup"  
       version="1"  
       PermissionSetName="FullTrust"  
       Name="MyExtensionCodeGroup"  
       Description="Code group for my data processing extension">  
          <IMembershipCondition class="UrlMembershipCondition"  
             version="1"  
             Url="C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies\MyExtensionAssembly.dll"  
           />  
    </CodeGroup>  
    ```  
  
 URL membership is only one of many membership conditions you might choose for your data processing extension. For more information about code access security in [!INCLUDE[ssRSversion2005](../../../includes/ssrsversion2005-md.md)], see [Secure Development &#40;Reporting Services&#41;](../../../reporting-services/extensions/secure-development/secure-development-reporting-services.md)  
  
## Generic Query Designer  
 Report Designer provides a generic query designer that you can use with custom data processing extensions. This designer consists of two panes: a query pane and a results pane. You can use the generic designer to write queries that are not supported by the graphical interface. Unlike the graphical query designer, the generic query designer does not check query syntax or restructure the query.  
  
#### To enable the generic query designer for a custom extension  
  
-   Add the following entry to the RSReportDesigner.config file under the **Designer** element, replacing the **Name** attribute with the name that you provided in previous entries.  
  
    ```  
    <Extension Name="ExtensionName" Type="Microsoft.ReportingServices.QueryDesigners.GenericQueryDesigner,Microsoft.ReportingServices.QueryDesigners"/>  
    ```  
  
## Verifying the Deployment  
 Before you can verify deployment, you must close all instances of [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] on your local computer. After you have ended all current sessions, you can verify whether your data processing extension was deployed successfully to Report Designer by creating a new report project in [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)]. Your extension should be included in the list of available data source types when you create a new data set for your report.  
  
## See Also  
 [Deploying a Data Processing Extension](../../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension.md)   
 [Reporting Services Extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
 [Implementing a Data Processing Extension](../../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
