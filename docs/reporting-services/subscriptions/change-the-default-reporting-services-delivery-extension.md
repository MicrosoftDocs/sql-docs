---
title: "Change the default Reporting Services delivery extension"
description: "Learn to configure Reporting Services settings to reorder the delivery extensions shown in the Delivered by list, and to set the default delivery extension."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/20/2017
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Report Manager [Reporting Services], default delivery extension"
---
# Change the default Reporting Services delivery extension
  You can modify [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration settings to change the default delivery extension that appears in the **Delivered by** list of a subscription definition page. For example you can modify the configuration so that when users create a new subscription, file share delivery is selected by default instead of e-mail delivery. You can also change the order the delivery extensions are listed in the user interface.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode | [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes E-mail and Windows File Share delivery are extensions. Your report server might have other delivery extensions if you deployed custom or third-party extensions to support custom delivery. The availability of a delivery extension depends on whether you deploy it on a report server.  
  
## Default native mode report server configuration  
 The order of a delivery extension appears in Report Manager in the **Delivered by** list is based on the order of the delivery extension entries in the **RSReportServer.config** file. For example, the following image shows e-mail first in the list and is the default.  
  
 :::image type="content" source="../../reporting-services/subscriptions/media/ssrs-default-delivery.png" alt-text="Screenshot of the default list of delivery extensions."::: 
  
 The following is the default section of **RSReportServer.config** that controls the default delivery extension and the order they're displayed in Report Manager. Email appears first in the file and is the default.  
  
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
  
#### Configure file share delivery as the default delivery extension in Report Manager  
  
1.  The steps in this procedure modify the configuration so that file share delivery is listed as the first option in the UI and it's the default selection.  
  
     Open the RSReportServer.config file in a text editor. For more information on the configuration file, see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md). After the configuration changes, the UI will look like the following image:  
  
     :::image type="content" source="../../reporting-services/subscriptions/media/ssrs-modified-delivery.png" alt-text="Screenshot of a modified list of delivery extensions.":::
  
1.  Modify the DeliveryUI section to look like the following sample and note the key changes of:  
  
    1.  The FileShare extension is before the email extension. This setting changes the order the extensions are listed in Report Manager.  
  
    1.  The File share extension contains DefaultExtension tag `<DefaultDeliveryExtension>True</DefaultDeliveryExtension>` and the extension end tag was added `</Extension>`.  
  
    1.  The email extension is no longer configured as the default. `<DefaultDeliveryExtension>False</DefaultDeliveryExtension>`  
  
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
  
1.  Save the configuration file.  
  
1.  Within a few minutes the report server reloads the configuration file, and the new settings take effect. You can restart the report server service to force the loading of the configuration file.  
  
     The following event is written to the Windows event log when the configuration is read.  
  
     **Event ID:** 109  
  
     **Source:** Report Server Windows Service (instance name)  
  
     The RSReportServer.config file is modified  
  
## SharePoint mode report servers  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode stores extensions information in the SharePoint service application databases and not in the RsrReportServer.config file. In SharePoint mode, delivery extension configuration is modified by using PowerShell.  
  
#### Configure the default delivery extension  
  
1.  Open the **SharePoint Management Shell**.  
  
1.  You can skip this step if you already know the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. Use the following PowerShell to list the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications in your SharePoint farm.  
  
    ```  
    get-sprsserviceapplication | format-list *  
    ```  
  
3.  Run the following PowerShell to verify the current default delivery extension for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application "ssrsapp".  
  
    ```  
    $app=get-sprsserviceapplication | where {$_.name -like "ssrsapp*"};Get-SPRSExtension -identity $app | where{$_.ServerDirectivesXML -like "<DefaultDelivery*"} | format-list *  
  
    ```
  
## Related content  
 [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [File share delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md)   
 [E-Mail delivery in Reporting Services](../../reporting-services/subscriptions/e-mail-delivery-in-reporting-services.md)   
