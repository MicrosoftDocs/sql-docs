---
title: "Change the Default Reporting Services Delivery Extension | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Report Manager [Reporting Services], default delivery extension"
ms.assetid: 5f6fee72-01bf-4f6c-85d2-7863c46c136b
author: markingmyname
ms.author: maghan
manager: kfile
---
# Change the Default Reporting Services Delivery Extension
  You can modify [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration settings to change the default delivery extension that appears in the **Delivered by** list of a subscription definition page. For example you can modify the configuration so that when users create a new subscription, file share delivery is selected by default instead of e-mail delivery. You can also change the order the delivery extensions are listed in the user interface.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Native mode | [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] SharePoint mode  
  
 [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] includes E-mail and Windows File Share delivery are extensions. Your report server might have additional delivery extensions if you have deployed custom or third-party extensions to support custom delivery. The availability of a delivery extension depends on whether it is deployed on a report server.  
  
## Default Native mode report server configuration  
 The order of a delivery extension appears in Report Manager in the **Delivered by** list is based on the order of the delivery extension entries in the **RSReportServer.config** file. For example the following image shows e-mail first in the list and it is selected by default.  
  
 ![default list of delivery extensions](../media/ssrs-default-delivery.png "default list of delivery extensions")  
  
 The following is the default section of **RSReportServer.config** that controls the default delivery extension and the order they are displayed in Report Manager. Note that email appears first in the file and it is set as the default.  
  
```  
<DeliveryUI>  
     <Extension Name="Report Server Email" Type="Microsoft.ReportingServices.EmailDeliveryProvider.EmailDeliveryProviderControl,ReportingServicesEmailDeliveryProvider">  
          <DefaultDeliveryExtension>True</DefaultDeliveryExtension>  
               <Configuration>  
               <RSEmailDPConfiguration>  
                    <DefaultRenderingExtension>MHTML</DefaultRenderingExtension>  
               </RSEmailDPConfiguration>  
               </Configuration>  
     </Extension>  
     <Extension Name="Report Server FileShare" Type="Microsoft.ReportingServices.FileShareDeliveryProvider.FileShareUIControl,ReportingServicesFileShareDeliveryProvider"/>  
</DeliveryUI>  
```  
  
#### Configure File Share Delivery as the default delivery extension in Report Manager  
  
1.  The steps in this procedure modify the configuration so that file share delivery is listed as the first option in the UI and it is the default selection.  
  
     Open the RSReportServer.config file in a text editor. For more information on the configuration file, see [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md). After the configuration changes, the UI will look like the following image:  
  
     ![modified list of delivery extensions](../media/ssrs-modified-delivery.png "modified list of delivery extensions")  
  
2.  Modify the DeliveryUI section to look like the following sample and note the key changes of:  
  
    1.  The FileShare extension is before the email extension. This will change the order the extensions are listed in Report Manager.  
  
    2.  The File share extension contains DefaultExtension tag `<DefaultDeliveryExtension>True</DefaultDeliveryExtension>` and the extension end tag was added `</Extension>`.  
  
    3.  The email extension is no longer configured as the default. `<DefaultDeliveryExtension>False</DefaultDeliveryExtension>`  
  
    ```  
    <DeliveryUI>  
         <Extension Name="Report Server FileShare" Type="Microsoft.ReportingServices.FileShareDeliveryProvider.FileShareUIControl,ReportingServicesFileShareDeliveryProvider">  
              <DefaultDeliveryExtension>True</DefaultDeliveryExtension>  
         </Extension>  
         <Extension Name="Report Server Email" Type="Microsoft.ReportingServices.EmailDeliveryProvider.EmailDeliveryProviderControl,ReportingServicesEmailDeliveryProvider">  
         <DefaultDeliveryExtension>False</DefaultDeliveryExtension>  
         <Configuration>  
              <RSEmailDPConfiguration>  
                   <DefaultRenderingExtension>MHTML</DefaultRenderingExtension>  
              </RSEmailDPConfiguration>  
         </Configuration>  
         </Extension>  
    </DeliveryUI>  
    ```  
  
3.  Save the configuration file.  
  
4.  Within a few minutes the report server will reload the configuration file and the new settings will take effect. You can restart the report server service to force the loading of the configuration file.  
  
     The following event is written to the windows event log when the configuration is read.  
  
     **Event ID:** 109  
  
     **Source:** Report Server Windows Service (instance name)  
  
     The RSReportServer.config file has been modified  
  
## SharePoint mode report servers  
 [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] SharePoint mode stores extensions information in the SharePoint service application databases and not in the RsrReportServer.config file. In SharePoint mode, delivery extension configuration is modified using PowerShell.  
  
#### Configure the default delivery extension  
  
1.  Open the **SharePoint Management Shell**.  
  
2.  You can skip this step if you already know the name of your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service application. Use the following PowerShell to list the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service applications in your SharePoint farm.  
  
    ```  
    get-sprsserviceapplication | format-list *  
    ```  
  
3.  Run the following PowerShell to verify the current default delivery extension for the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service application "ssrsapp".  
  
    ```  
    $app=get-sprsserviceapplication | where {$_.name -like "ssrsapp*"};Get-SPRSExtension -identity $app | where{$_.ServerDirectivesXML -like "<DefaultDelivery*"} | format-list *  
  
    ```  
  
## See Also  
 [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md)   
 [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md)   
 [File Share Delivery in Reporting Services](file-share-delivery-in-reporting-services.md)   
 [E-Mail Delivery in Reporting Services](e-mail-delivery-in-reporting-services.md)   
 [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md)  
  
  
