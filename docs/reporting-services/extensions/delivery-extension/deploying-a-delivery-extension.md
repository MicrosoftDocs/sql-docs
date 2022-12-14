---
title: "Deploying a Delivery Extension | Microsoft Docs"
description: Learn how to deploy a delivery extension to a report server. See which entries to add to which configuration files so the report server locates the extension.
ms.date: 03/16/2017
ms.prod: reporting-services
ms.technology: extensions
ms.topic: reference
helpviewer_keywords:
  - "delivery extensions [Reporting Services], deploying"
  - "Extension element"
  - "deploying [Reporting Services], extensions"
ms.assetid: 4436ce48-397d-42c7-9b5d-2a267e2a1b2c
author: maggiesMSFT
ms.author: maggies
ms.custom:
  - intro-deployment
---
# Deploying a Delivery Extension
  Delivery extensions supply their configuration information in the form of an XML configuration file. The XML file conforms to the XML schema defined for delivery extensions. Delivery extensions provide infrastructure for setting and modifying the configuration file.  
  
 If a delivery extension is replaced or upgraded, all subscriptions that reference the delivery extension remain valid.  
  
 After you have written and compiled your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension into a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] library, you must copy the extension to the appropriate directory and add an entry to the appropriate [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration file so the report server can locate it.  
  
## Configuration-File Extension Element  
 Delivery extensions that you deploy to the report server need to be entered as **Extension** elements in the configuration file. The configuration file for the report server is RSReportServer.config.  
  
 The following table describes the attributes for the **Extension** element for delivery extensions.  
  
|Attribute|Description|  
|---------------|-----------------|  
|**Name**|A unique name for the extension (for example, "Report Server E-Mail" for the e-mail delivery extension or "Report Server FileShare" for the file share delivery extension). The maximum length for the **Name** attribute is 255 characters. The name must be unique among all entries within the **Extension** element of a configuration file. If a duplicate name is present, the report server returns an error.|  
|**Type**|A comma-separated list that includes the fully qualified namespace along with the name of the assembly.|  
|**Visible**|A value of **false** indicates that the delivery extension should not be visible in user interfaces. If the attribute is not included, the default value is **true**.|  
  
 For more information about the RSReportServer.config file, see [Reporting Services Configuration Files](../../../reporting-services/report-server/reporting-services-configuration-files.md).  
  
## Deploying the Extension to the Report Server  
 The report server uses delivery extensions for processing and delivering notifications or reports. You should deploy your delivery extension assembly to the report server as a private assembly. You also need to make an entry in the report server configuration file, RSReportServer.config.  
  
#### To deploy a deliver extension assembly to a report server  
  
1.  Copy your assembly from your staging location to the bin directory of the report server on which you want to use the delivery extension. The default location of the report server bin directory is %ProgramFiles%\Microsoft SQL Server\MSRS13.\<InstanceName>\Reporting Services\ReportServer\bin.  
  
    > [!IMPORTANT]  
    >  If you are attempting to overwrite an existing delivery extension assembly, you must first stop the Report Server service before copying the updated assembly. Restart your service after the assembly is through copying.  
  
2.  After the assembly file is copied, open the RSReportServer.config file. The RSReportServer.config file is located in the %ProgramFiles%\Microsoft SQL Server\MSRS13.\<InstanceName>\Reporting Services\ReportServer directory. You need to make an entry in the configuration file for your delivery extension assembly file. You can open the configuration file with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] or a simple text editor, such as Notepad.  
  
3.  Locate the **Delivery** element in the RSReportServer.config file. An entry for your newly created delivery extension should be made in the following location:  
  
    ```  
    <Extensions>  
       <Delivery>  
          <Your extension configuration information goes here>  
       </Delivery>  
    </Extensions>  
    ```  
  
4.  Add an entry for your delivery extension. Your entry should include an **Extension** element with values for **Name** and **Type**, and might look like the following:  
  
    ```  
    <Extension Name="My Delivery Extension Name" Type="CompanyName.ExtensionName.MyDeliveryExtensionClass, AssemblyName" />  
    ```  
  
     The value for **Name** is the unique name of the delivery extension. The value for **Type** is a comma-separated list that includes an entry for the fully qualified namespace of your class that implements the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> interface, followed by the name of your assembly (not including the .dll file extension). By default, delivery extensions are visible. To hide an extension from user interfaces, such as the web portal, add a **Visible** attribute to the **Extension** element, and set it to **false**.  
  
5.  Finally, add a code group for your custom assembly that grants **FullTrust** permission for your delivery extension. You do this by adding the code group to the rssrvpolicy.config file located by default in %ProgramFiles%\Microsoft SQL Server\MSRS13.\<InstanceName>\Reporting Services\ReportServer. Your code group might look like the following:  
  
    ```  
    <CodeGroup class="UnionCodeGroup"  
       version="1"  
       PermissionSetName="FullTrust"  
       Name="MyExtensionCodeGroup"  
       Description="Code group for my delivery extension">  
          <IMembershipCondition class="UrlMembershipCondition"  
             version="1"  
             Url="C:\Program Files\Microsoft SQL Server\MSRS13.<InstanceName>\Reporting Services\ReportServer\bin\MyExtensionAssembly.dll"  
           />  
    </CodeGroup>  
    ```  
  
     URL membership is only one of many membership conditions you might choose for your delivery extension. For more information about code access security in [!INCLUDE[ssRS](../../../includes/ssrs.md)], see.[Secure Development &#40;Reporting Services&#41;](../../../reporting-services/extensions/secure-development/secure-development-reporting-services.md)  
   
## Verifying the Deployment  
 You can verify whether your delivery extension was deployed successfully to the report server by using the Web service <xref:ReportService2010.ReportingService2010.ListExtensions%2A> method. You can also open the web portal and verify that your extension is included in the list of available delivery extensions for a subscription. For more information about the web portal and subscriptions, see [Subscriptions and Delivery &#40;Reporting Services&#41;](../../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md).  
  
## See Also  
 [Implementing a Delivery Extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
