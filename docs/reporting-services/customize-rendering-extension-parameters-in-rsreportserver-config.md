---
title: "Customize Rendering Extension Parameters in RSReportServer.Config | Microsoft Docs"
ms.date: 03/20/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "configuration options [Reporting Services]"
  - "DeviceInfo settings"
  - "rendering extensions [Reporting Services], overriding behaviors"
  - "parameters [Reporting Services], report rendering"
  - "overriding report rendering behavior"
  - "extensions [Reporting Services], rendering"
ms.assetid: 3bf7ab2b-70bb-41c8-acda-227994d15aed
author: maggiesMSFT
ms.author: maggies
---
# Customize Rendering Extension Parameters in RSReportServer.Config
  You can specify rendering extension parameters in the RSReportServer configuration file to override default report rendering behavior for reports that run on a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server. You can modify rendering extension parameters to achieve the following objectives:  
  
-   Change how the rendering extension name appears in the Export list of the report toolbar (for example, to change "Web archive" to "MHTML"), or localize the name to a different language.  
  
-   Create multiple instances of the same rendering extension to support different report presentation options (for example, a portrait and landscape mode version of the Image rendering extension).  
  
-   Change the default rendering extension parameters to use different values (for example, the Image rendering extension uses TIFF as the default output format; you can modify the extension parameters to use EMF instead).  
  
 Changing the rendering extension parameters only affects rendering operations on the report server. You cannot override rendering extension settings in report preview in Report Designer.  
  
 Specifying rendering extension parameters in the configuration files affects rendering extensions globally. The settings in the configuration files are used in place of default values whenever a particular rendering extension is used. If you want to set rendering extension parameters for a specific report or render operation, you must specify device information programmatically using the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method or by specifying device information settings on a report URL. For more information about specifying device information settings for a render operation, and to view the complete list of device information settings, see [Passing Device Information Settings to Rendering Extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md).  
  
## Finding and Modifying RSReportServer.config  
 Configuration settings for report output formats are specified as rendering extension parameters in the RSReportServer.config file. To specify rendering extension parameters in the configuration files, you must know how to define the XML structures that set rendering parameters. There are two XML structures that you can modify:  
  
-   The **OverrideNames** element defines the display name and language of the rendering extension.  
  
-   The **DeviceInfo** XML structure defines the device information settings that are used by a rendering extension. Most rendering extension parameters are specified as device information settings.  
  
 You can use a text editor to modify the file. The RSReportServer.config file can be found in the \Reporting Services\Report Server\Bin folder. For more information about modifying configuration files, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md).  
  
## Changing the Display Name  
 The display name for a rendering extension appears in the Export list of the report toolbar. Examples of default display names include Web archive, TIFF file, and Acrobat (PDF) file. You can replace the default display name with a custom value by specifying the **OverrideNames** element in the configuration files. In addition, if you are defining two instances of a single rendering extension, you can use the **OverrideNames** element to distinguish each instance in the Export list.  
  
 Because display names are localized, you must set the **Language** attribute if you are replacing the default display name with a custom value. Otherwise, any name that you specify will be ignored. The language value that you set must be valid for the report server computer. For example, if the report server is running on a French operating system, you should specify "fr-FR" as the attribute value.  
  
 The following example illustrates how to provide a custom name on an English report server:  
  
```  
<Extension Name="XML" Type="Microsoft.ReportingServices.Rendering.DataRenderer.XmlDataReport,Microsoft.ReportingServices.DataRendering">  
   <OverrideNames>  
     <Name Language="en-US">My Custom Display Name for XML Rendering</Name>  
   </OverrideNames>  
</Extension>  
```  
  
## Changing Device Information Settings  
 To modify default device information settings that are used by a rendering extension that is already deployed on your report server, you must type the **DeviceInfo** XML structure into the configuration files. Every rendering extension supports device information settings that are unique to that extension. To view the complete list of device information settings, see [Passing Device Information Settings to Rendering Extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md).  
  
 The following example provides an illustration of the XML structure and syntax that modifies the default settings of the Image rendering extension:  
  
```  
<Render>  
    <Extension Name="IMAGE (EMF)" Type="Microsoft.ReportingServices.Rendering.ImageRenderer.ImageRenderer,Microsoft.ReportingServices.ImageRendering">  
        <OverrideNames>  
            <Name Language="en-US">Image (EMF)</Name>  
        </OverrideNames>  
        <Configuration>  
            <DeviceInfo>  
                <ColorDepth>32</ColorDepth>  
                <DpiX>300</DpiX>  
                <DpiY>300</DpiY>  
                <OutputFormat>EMF</OutputFormat>  
            </DeviceInfo>  
        </Configuration>  
    </Extension>  
</Render>  
```  
  
## Configuring Multiple Entries for a Rendering Extension  
 You can create multiple instances of the same rendering extension to support different report presentation options. Each instance that you define can have a different combination of parameter values. When defining new instances of an existing rendering extension, be sure to do the following:  
  
-   Specify a unique name for the extension.  
  
     Each instance must have a unique value for the **Name** attribute. The following example uses the names "IMAGE (EMF Landscape)" and "IMAGE (EMF Portrait)" to distinguish between the two instances.  
  
     Use caution when changing the name of a rendering extension that is already deployed. Developers who specify rendering extensions programmatically use the extension name to identify which instance to use for a particular render operation. If you are running custom [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] applications on your report server, make sure that the developer knows if you modify an existing extension name or add a new one.  
  
-   Specify a unique display name so that users can understand the differences for each output format.  
  
     If you are configuring multiple versions of the same extension, you can give each version a unique name by providing a value for **OverrideNames**. Otherwise, all versions of the extension will appear to have the same name in the Export options list on the report toolbar.  
  
 The following example illustrates how to use the default Image rendering extension (which produces TIFF output) to output EMF in Portrait mode alongside a second instance that outputs reports in EMF in Landscape mode. Notice that each extension name is unique. When testing this example, remember to choose reports that do not contain interactive features such as show/hide options, matrices, or drillthrough links (interactive features do not work in the Image rendering extension):  
  
```  
<Render>  
    <Extension Name="IMAGE (EMF Landscape)" Type="Microsoft.ReportingServices.Rendering.ImageRenderer.ImageRenderer,Microsoft.ReportingServices.ImageRendering">  
        <OverrideNames>  
            <Name Language="en-US">EMF in Landscape Mode</Name>  
        </OverrideNames>  
        <Configuration>  
            <DeviceInfo>  
                <OutputFormat>EMF</OutputFormat>  
                <PageHeight>8.5in</PageHeight>  
                <PageWidth>11in</PageWidth>  
            </DeviceInfo>  
        </Configuration>  
    </Extension>  
    <Extension Name="IMAGE (EMF Portrait)" Type="Microsoft.ReportingServices.Rendering.ImageRenderer.ImageRenderer,Microsoft.ReportingServices.ImageRendering">  
        <OverrideNames>  
            <Name Language="en-US">EMF in Portait Mode</Name>  
        </OverrideNames>  
        <Configuration>  
            <DeviceInfo>  
                <OutputFormat>EMF</OutputFormat>  
                <PageHeight>11in</PageHeight>  
                <PageWidth>8.5in</PageWidth>  
            </DeviceInfo>  
        </Configuration>  
    </Extension>  
</Render>  
```  
  
## See Also  
 [RsReportServer.config Configuration File](../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [RSReportDesigner Configuration File](../reporting-services/report-server/rsreportdesigner-configuration-file.md)   
 [CSV Device Information Settings](../reporting-services/csv-device-information-settings.md)   
 [Excel Device Information Settings](../reporting-services/excel-device-information-settings.md)   
 [HTML Device Information Settings](../reporting-services/html-device-information-settings.md)   
 [Image Device Information Settings](../reporting-services/image-device-information-settings.md)   
 [MHTML Device Information Settings](../reporting-services/mhtml-device-information-settings.md)   
 [PDF Device Information Settings](../reporting-services/pdf-device-information-settings.md)   
 [XML Device Information Settings](../reporting-services/xml-device-information-settings.md)  
  
  
