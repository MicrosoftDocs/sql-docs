---
title: "How to: Deploy a Data Processing Extension to a Report Server | Microsoft Docs"
ms.date: 03/06/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "data processing extensions [Reporting Services], deploying"
  - "assemblies [Reporting Services], data processing extension deployments"
ms.assetid: e00dface-70f8-434b-9763-8ebee18737d2
author: maggiesMSFT
ms.author: maggies
---
# Deploying a Data Processing Extension to a Report Server
  Report servers use data processing extensions for retrieving and processing data in rendered reports. You should deploy your data processing extension assembly to a report server as a private assembly. You also need to make an entry in the report server configuration file, RSReportServer.config.  
  
## Procedures  
  
#### To deploy a data processing extension assembly  
  
1.  Copy your assembly from your staging location to the bin directory of the report server on which you want to use the data processing extension. The default location of the report server bin directory is %ProgramFiles%\Microsoft SQL Server\MSRS10_50.\<*Instance Name*>\Reporting Services\ReportServer\bin.  
  
    > [!NOTE]  
    >  This step will prevent an upgrade to a newer instance of SQL Server. For more information, see [Upgrade and Migrate Reporting Services](../../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md).  
  
2.  After the assembly file is copied, open the RSReportServer.config file. The RSReportServer.config file is located in the ReportServer directory. You need to make an entry in the configuration file for your data processing extension assembly file. You can open the configuration file with Visual Studio or a simple text editor, such as Notepad.  
  
3.  Locate the **Data** element in the RSReportServer.config file. An entry for your newly created data processing extension should be made in the following location:  
  
    ```  
    <Extensions>  
       <Data>  
          <Your extension configuration information goes here>  
       </Data>  
    </Extensions>  
    ```  
  
4.  Add an entry for your data processing extension. Your entry should include an **Extension** element with values for **Name** and **Type** and might look like the following:  
  
    ```  
    <Extension Name="ExtensionName" Type="CompanyName.ExtensionName.MyConnectionClass, MyExtensionAssembly" />  
    ```  
  
     The value for **Name** is the unique name of the data processing extension. The value for **Type** is a comma-separated list that includes an entry for the fully qualified namespace of your class that implements the <xref:Microsoft.ReportingServices.Interfaces.IExtension> and <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection> interfaces, followed by the name of your assembly (not including the .dll file extension). By default, data processing extensions are visible. To hide an extension from user interfaces, such as Report Manager, add a **Visible** attribute to the **Extension** element, and set it to **false**.  
  
5.  Add a code group for your custom assembly that grants **FullTrust** permission for your extension. You do this by adding the code group to the rssrvpolicy.config file located by default in %ProgramFiles%\Microsoft SQL Server\\<MSRS10_50.\<*Instance Name*>\Reporting Services\ReportServer. Your code group might look like the following:  
  
    ```  
    <CodeGroup class="UnionCodeGroup"  
       version="1"  
       PermissionSetName="FullTrust"  
       Name="MyExtensionCodeGroup"  
       Description="Code group for my data processing extension">  
          <IMembershipCondition class="UrlMembershipCondition"  
             version="1"  
             Url="C:\Program Files\Microsoft SQL Server\MSRS10_50.<Instance Name>\Reporting Services\ReportServer\bin\MyExtensionAssembly.dll"  
           />  
    </CodeGroup>  
    ```  
  
 URL membership is only one of many membership conditions you might choose for your data processing extension. For more information about code access security in [!INCLUDE[ssCurrentUI](../../../includes/sscurrentui-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], see [Secure Development &#40;Reporting Services&#41;](../../../reporting-services/extensions/secure-development/secure-development-reporting-services.md).  
  
## Verifying the Deployment  
 You can verify whether your data processing extension was deployed successfully to the report server by using the Web service <xref:ReportService2010.ReportingService2010.ListExtensions%2A> method. You can also open Report Manager and verify that your extension is included in the list of available data sources. For more information about Report Manager and data sources, see [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](../../../reporting-services/report-data/create-modify-and-delete-shared-data-sources-ssrs.md).  
  
## See Also  
 [Deploying a Data Processing Extension](../../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension.md)   
 [Reporting Services Extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
 [Implementing a Data Processing Extension](../../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
