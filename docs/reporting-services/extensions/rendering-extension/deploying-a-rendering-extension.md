---
title: "Deploying a Rendering Extension | Microsoft Docs"
description: Find out how to deploy a report rendering extension. See which configuration file entries to add so the report server and Report Designer locate the extension.
ms.date: 03/20/2017
ms.prod: reporting-services
ms.technology: extensions
ms.topic: reference
helpviewer_keywords:
  - "deploying [Reporting Services], extensions"
  - "rendering extensions [Reporting Services], deploying"
ms.assetid: 9fb8c887-5cb2-476e-895a-7b0e2dd11398
author: maggiesMSFT
ms.author: maggies
ms.custom:
  - intro-deployment
---
# Deploying a Rendering Extension
  After you have written and compiled your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report rendering extension into a [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] library, you need to make it discoverable by the report server and by Report Designer. To do so, copy the extension to the appropriate directory and add entries to the appropriate [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration files.  
  
## Configuration File Rendering Extension Element  
 Once a rendering extension has been compiled into a .DLL, you add an entry into the rsreportserver.config file. By default, the location is %ProgramFiles%\Microsoft SQL Server\MSRS10_50.\<InstanceName>\Reporting Services\ReportServer. The parent element is \<Render>. Under the Render element is an Extension element for each rendering extension. The **Extension** element contains two attributes, Name and Type.  
  
 The following table describes the attributes for the **Extension** element for rendering extensions:  
  
|Attribute|Description|  
|---------------|-----------------|  
|**Name**|A unique name for the extension. The maximum length for the **Name** attribute is 255 characters. The name must be unique among all entries within the **Extensions** element of a configuration file. If a duplicate name is present, the report server returns an error.|  
|**Type**|A comma-separated list that includes the fully qualified namespace along with the name of the assembly.|  
|**Visible**|A value of **false** indicates that the rendering extension should not be visible in user interfaces. If the attribute is not included, the default value is **true**.|  
|**LogAllExecutionRequests**|A value of **false** indicates that an entry is logged for only the first report execution in a session. If the attribute is not included, the default value is **true**.<br /><br /> For example, this setting determines whether to log an entry for only the first page rendered in a report (when **false**) or an entry for each page rendered in the report (when **true**).|  
  
 For more information, see [RsReportServer.config Configuration File](../../../reporting-services/report-server/rsreportserver-config-configuration-file.md).  
  
## Deploying the Extension to the Report Server  
 The report server uses rendering extensions to export reports to other formats. You should deploy your rendering extension assembly to the report server as a private assembly. You also need to make an entry in the report server configuration file, rsreportserver.config.  
  
### To deploy the assembly  
  
1.  Copy your assembly from your staging location to the bin directory of the report server on which you want to use the rendering extension. The default location of the report server Bin directory is %ProgramFiles%\Microsoft SQL Server\MSRS10_50.\<InstanceName>\Reporting Services\ReportServer\Bin.  
  
2.  After the assembly file is copied, open the rsreportserver.config file. The rsreportserver.config file is also located in the report server bin directory. You need to make an entry in the configuration file for your extension assembly file. You can open the file with [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] or a simple text editor.  
  
     For more information, see [RsReportServer.config Configuration File](../../../reporting-services/report-server/rsreportserver-config-configuration-file.md).  
  
3.  Locate the **Render** element in the Rsreportserver.config file. An entry for your newly created extension should be made in the following location:  
  
    ```  
    <Extensions>  
       <Render>  
          <extension configuration>  
       </Render>  
    </Extensions>  
    ```  
  
4.  Add an entry for your rendering extension. Your entry should include an element that has values for **Name** and **Type**, and might look like the following:  
  
    ```  
    <Extension Name="My Rendering Extension Name" Type="CompanyName.ExtensionName.MyRenderingProvider, AssemblyName" />  
    ```  
  
     The value for **Name** is the unique name of the rendering extension. The value for **Type** is a comma-separated list that includes an entry for the fully qualified namespace of your <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension> implementation, followed by the name of your assembly (not including the .dll file extension). By default, rendering extensions are visible. To hide an extension from user interfaces, such as Report Manager, add a **Visible** attribute to the **Extension** element, and set it to **false**.  
  
## Verifying the deployment  
 You can also open Report Manager and verify that your extension is included in the list of available export types for a report.  
  
## See Also  
 [Implementing a Rendering Extension](../../../reporting-services/extensions/rendering-extension/implementing-a-rendering-extension.md)   
 [Rendering Extensions Overview](../../../reporting-services/extensions/rendering-extension/rendering-extensions-overview.md)   
 [Implementing the IRenderingExtension Interface](../../../reporting-services/extensions/rendering-extension/implementing-the-irenderingextension-interface.md)   
 [Security Considerations for Extensions](../../../reporting-services/extensions/security-considerations-for-extensions.md)  
  
  
