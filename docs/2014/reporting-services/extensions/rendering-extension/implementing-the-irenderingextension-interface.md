---
title: "Implementing the IRenderingExtension Interface | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "IRenderingExtension interface"
  - "rendering extensions [Reporting Services], IRenderingExtension interface"
ms.assetid: 74b2f2b7-6796-42da-ab7d-b05891ad4001
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Implementing the IRenderingExtension Interface
  The rendering extension takes the results from a report definition that is combined with the actual data and renders the resulting data to a format that is useable. The transformation of the combined data and formatting is done by using a common language runtime (CLR) class that implements <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension>. This transforms the object model into an output format that is consumable by a viewer, printer, or other output target.  
  
 The <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension> has three methods that must be coded:  
  
-   <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.Render%2A> - renders the report.  
  
-   <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.RenderStream%2A> - renders a specific stream from the report.  
  
-   <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.GetRenderingResource%2A> - gets additional information, such as icons, that are required for the report.  
  
 The following sections discuss these methods in more detail.  
  
## Render Method  
 The <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.Render%2A> method contains arguments that represent the following objects:  
  
-   The *report* that you want to render. This object contains properties, data, and layout information for the report. The report is the root of the report object model tree.  
  
-   The *ServerParameters* that contain the string dictionary object, with the parameters for the report server, if any.  
  
-   The *deviceInfo* parameter that contain the device settings. For more information, see [Passing Device Information Settings to Rendering Extensions](../../report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md).  
  
-   The *clientCapabilities* parameter that contains a <xref:System.Collections.Specialized.NameValueCollection> dictionary object that has information about the client to which you are rendering.  
  
-   The *RenderProperties* that contains information about the rendering result.  
  
-   The *createAndRegisterStream* is a delegate function to be called to get a stream to render into.  
  
### deviceInfo Parameter  
 The *deviceInfo* parameter contains rendering parameters, not report parameters. These rendering parameters are passed to the rendering extension. The *deviceInfo* values are converted into a <xref:System.Collections.Specialized.NameValueCollection> object by the report server. Items in the *deviceInfo* parameter are treated as case-insensitive values. If the render request came as a result of URL access, the URL parameters in the form `rc:key=value` are converted to key/value pairs in the *deviceInfo* dictionary object. The browser detection code also provides the following items in the *clientCapabilities* dictionary: EcmaScriptVersion, JavaScript, MajorVersion, MinorVersion, Win32, Type, and AcceptLanguage. Any name/value pair in the *deviceInfo* parameter that is not understood by the rendering extension is ignored. The following code sample shows a sample <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.GetRenderingResource%2A> method that retrieves icons:  
  
```csharp  
public void GetRenderingResource (CreateStream createStreamCallback, NameValueCollection deviceInfo)  
{  
    string[] iconTagValues = deviceInfo.GetValues("Icon");  
    if ((iconTagValues != null) && (iconTagValues.Length > 0) )  
    {  
        // Create a stream to output to.  
        Stream outputStream = createStreamCallback(m_iconResourceName, "gif", null, "image/gif", false);  
        // Get the GIF image for one of the buttons on the toolbar  
        Image requiredImage = (Image) m_resourcemanager.GetObject(m_iconResourceName  
        // Write the image to the output stream  
        requiredImage.Save(outputStream, requiredImage.RawFormat);  
    }  
    return;  
}  
```  
  
## RenderStream Method  
 The <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.RenderStream%2A> method renders a particular stream from the report. All streams are created during the initial <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.Render%2A> call, but the streams are not returned to the client initially. This method is used for secondary streams, such as images in HTML rendering, or additional pages of a multi-page rendering extension, such as Image/EMF.  
  
## GetRenderingResource Method  
 The <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.GetRenderingResource%2A> method retrieves the information without executing an entire rendering of the report. There are times when the report requires information that does not require the report itself to be rendered. For example, if you need the icon associated with the rendering extension, use the *deviceInfo* parameter containing the single tag **\<Icon>**. In these cases, you can use the <xref:Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.GetRenderingResource%2A> method.  
  
## See Also  
 [Implementing a Rendering Extension](implementing-a-rendering-extension.md)   
 [Rendering Extensions Overview](rendering-extensions-overview.md)  
  
  
